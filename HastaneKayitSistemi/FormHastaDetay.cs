using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace HastaneKayitSistemi
{
    public partial class FormHastaDetay : Form
    {
        // Ana menüye dönüş kontrolü
        private bool isReturningToMain = false;

        public FormHastaDetay()
        {
            InitializeComponent();
        }

        public string TC; // Giriş formundan gelen TC
        sqlBaglanti bgl = new sqlBaglanti();

        // Seçilen aktif randevunun ID'si
        string secilenRandevuID = "0";

        // ----------------------------------------------------------------
        // 1. FORM YÜKLENİRKEN (BAŞLANGIÇ)
        // ----------------------------------------------------------------
        private void FormHastaDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = TC;

            // İsim Getir
            AdSoyadGetir();

            // Şehirleri Doldur (Silsilenin başı)
            SehirleriGetir();

            // Listeleri Doldur
            RandevuGecmisiListele();
            AktifRandevulariListele();
        }

        // ----------------------------------------------------------------
        // 2. LİSTELEME FONKSİYONLARI
        // ----------------------------------------------------------------

        // A) Aktif Randevular (Filtreye Göre)
        void AktifRandevulariListele()
        {
            DataTable dt = new DataTable();
            NpgsqlConnection conn = bgl.baglanti();

            try
            {
                // SQL: Parametre boşsa filtreleme yapmaz.
                string sorgu = @"
                    SELECT 
                        r.randevu_id AS ""ID"",
                        h.hastane_ad AS ""Hastane"",
                        b.brans_ad AS ""Branş"",
                        (d.doktor_ad || ' ' || d.doktor_soyad) AS ""Doktor"",
                        r.randevu_tarih AS ""Tarih"",
                        r.randevu_saat AS ""Saat""
                    FROM Randevular r
                    JOIN Doktorlar d ON r.doktor_id = d.doktor_id
                    JOIN Branslar b ON r.brans_id = b.brans_id
                    JOIN Hastaneler h ON d.hastane_id = h.hastane_id
                    WHERE r.randevu_durum = FALSE 
                      AND (@p1 = '' OR h.hastane_ad = @p1)
                      AND (@p2 = '' OR b.brans_ad = @p2)
                      AND (@p3 = '' OR (d.doktor_ad || ' ' || d.doktor_soyad) = @p3)
                    ORDER BY r.randevu_tarih, r.randevu_saat";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
                da.SelectCommand.Parameters.AddWithValue("@p1", CmbHastAd.Text);
                da.SelectCommand.Parameters.AddWithValue("@p2", CmbBrans.Text);
                da.SelectCommand.Parameters.AddWithValue("@p3", CmbDoktor.Text);

                da.Fill(dt);
                dataGridView2.DataSource = dt;

                // ID Gizle
                if (dataGridView2.Columns["ID"] != null) dataGridView2.Columns["ID"].Visible = false;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex) { MessageBox.Show("Liste hatası: " + ex.Message); }
            finally { conn.Close(); }
        }

        // B) Geçmiş Randevular
        void RandevuGecmisiListele()
        {
            DataTable dt = new DataTable();
            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                string tcHash = SecurityHelper.Hashle(TC);
                string sql = @"
                    SELECT h.hastane_ad AS ""Hastane"", b.brans_ad AS ""Branş"", 
                    (d.doktor_ad || ' ' || d.doktor_soyad) AS ""Doktor"", 
                    r.randevu_tarih AS ""Tarih"", r.randevu_saat AS ""Saat"", r.hasta_sikayet AS ""Şikayet"" 
                    FROM Randevular r 
                    JOIN Hastalar p ON r.hasta_id = p.hasta_id 
                    JOIN Doktorlar d ON r.doktor_id = d.doktor_id 
                    JOIN Hastaneler h ON d.hastane_id = h.hastane_id 
                    JOIN Branslar b ON r.brans_id = b.brans_id 
                    WHERE p.tc_hash = @p1 ORDER BY r.randevu_tarih DESC";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@p1", tcHash);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex) { MessageBox.Show("Geçmiş hatası: " + ex.Message); }
            finally { conn.Close(); }
        }

        // ----------------------------------------------------------------
        // 3. FİLTRELEME (COMBOBOX ZİNCİRİ)
        // ----------------------------------------------------------------

        // 1. Şehirleri Getir
        void SehirleriGetir()
        {
            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT DISTINCT sehir FROM Hastaneler ORDER BY sehir", conn);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) { CmbSehir.Items.Add(dr[0].ToString()); }
            }
            finally { conn.Close(); }
        }

        // 2. Şehir Seçildi -> Hastaneleri Getir (DÜZELTİLEN KISIM)
        private void CmbSehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbSehir.SelectedIndex == -1) return;

            CmbHastAd.Items.Clear(); CmbBrans.Items.Clear(); CmbDoktor.Items.Clear();
            CmbHastAd.Text = ""; CmbBrans.Text = ""; CmbDoktor.Text = "";

            AktifRandevulariListele(); // Listeyi güncelle

            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                // SQL DÜZELTMESİ: UPPER ve TRIM ile %100 eşleşme garantisi
                string sql = "SELECT hastane_ad FROM Hastaneler WHERE UPPER(TRIM(sehir)) = UPPER(@p1) ORDER BY hastane_ad";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                // Gelen veriyi de Trim'leyip gönderiyoruz
                cmd.Parameters.AddWithValue("@p1", CmbSehir.Text.Trim());

                NpgsqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) { CmbHastAd.Items.Add(dr[0].ToString()); }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
            finally { conn.Close(); }
        }

        // 3. Hastane Seçildi -> Branşları Getir
        private void CmbHastAd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbHastAd.SelectedIndex == -1) return;

            CmbBrans.Items.Clear(); CmbDoktor.Items.Clear();
            CmbBrans.Text = ""; CmbDoktor.Text = "";
            AktifRandevulariListele();

            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                string sql = "SELECT DISTINCT b.brans_ad FROM Branslar b JOIN Hastaneler h ON b.hastane_id = h.hastane_id WHERE h.hastane_ad=@p1 ORDER BY b.brans_ad";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@p1", CmbHastAd.Text);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) { CmbBrans.Items.Add(dr[0].ToString()); }
            }
            finally { conn.Close(); }
        }

        // 4. Branş Seçildi -> Doktorları Getir
        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbBrans.SelectedIndex == -1) return;

            CmbDoktor.Items.Clear(); CmbDoktor.Text = "";
            AktifRandevulariListele();

            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                string sql = "SELECT (d.doktor_ad || ' ' || d.doktor_soyad) FROM Doktorlar d JOIN Branslar b ON d.brans_id = b.brans_id JOIN Hastaneler h ON d.hastane_id = h.hastane_id WHERE h.hastane_ad=@p1 AND b.brans_ad=@p2";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@p1", CmbHastAd.Text);
                cmd.Parameters.AddWithValue("@p2", CmbBrans.Text);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) { CmbDoktor.Items.Add(dr[0].ToString()); }
            }
            finally { conn.Close(); }
        }

        // 5. Doktor Seçildi -> Sadece Filtrele
        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            AktifRandevulariListele();
        }

        // ----------------------------------------------------------------
        // 4. RANDEVU ALMA İŞLEMİ
        // ----------------------------------------------------------------
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView2.Rows[e.RowIndex].Cells[0].Value != null)
            {
                secilenRandevuID = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                LblID.Text = secilenRandevuID;
            }
        }

        private void BtnRandevu_Click(object sender, EventArgs e)
        {
            if (secilenRandevuID == "0" || string.IsNullOrEmpty(secilenRandevuID))
            {
                MessageBox.Show("Lütfen listeden randevu seçiniz.");
                return;
            }

            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                string tcHash = SecurityHelper.Hashle(TC);
                Guid hastaID = Guid.Empty;

                // Hasta ID Bul
                NpgsqlCommand cmdBul = new NpgsqlCommand("SELECT hasta_id FROM Hastalar WHERE tc_hash=@p1", conn);
                cmdBul.Parameters.AddWithValue("@p1", tcHash);
                object res = cmdBul.ExecuteScalar();
                if (res != null) hastaID = Guid.Parse(res.ToString());

                // Güncelle
                string sql = "UPDATE Randevular SET randevu_durum=TRUE, hasta_id=@p1, hasta_sikayet=@p2 WHERE randevu_id=@p3";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@p1", hastaID);
                cmd.Parameters.AddWithValue("@p2", RchSikayet.Text);
                cmd.Parameters.AddWithValue("@p3", int.Parse(secilenRandevuID));
                cmd.ExecuteNonQuery();

                MessageBox.Show("Randevu alındı.");

                // Temizlik ve Yenileme
                AktifRandevulariListele();
                RandevuGecmisiListele();
                secilenRandevuID = "0"; LblID.Text = "0"; RchSikayet.Text = "";
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
            finally { conn.Close(); }
        }

        // ----------------------------------------------------------------
        // 5. YARDIMCI METOTLAR VE BUTONLAR
        // ----------------------------------------------------------------
        void AdSoyadGetir()
        {
            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                string tcHash = SecurityHelper.Hashle(TC);
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT hasta_ad, hasta_soyad FROM Hastalar WHERE tc_hash=@p1", conn);
                cmd.Parameters.AddWithValue("@p1", tcHash);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read()) LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            finally { conn.Close(); }
        }

        private void BtnRecete_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                string tcHash = SecurityHelper.Hashle(TC);
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT hasta_id FROM Hastalar WHERE tc_hash = @p1", conn);
                cmd.Parameters.AddWithValue("@p1", tcHash);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string hID = dr[0].ToString();
                    ReceteGoruntule frm = new ReceteGoruntule();
                    frm.hasta_ID = hID;
                    frm.Show();
                }
                else { MessageBox.Show("Hasta bulunamadı."); }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
            finally { conn.Close(); }
        }

        private void LnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormBilgiDuzenle frm = new FormBilgiDuzenle();
            frm.TC = LblTC.Text; // FormBilgiDuzenle'de değişkenin adı TC olmalı
            frm.Show();
        }

        private void LnkTemizle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LblID.Text = "0"; secilenRandevuID = "0"; RchSikayet.Text = "";CmbSehir.Text = "";CmbHastAd.Text = "";
            CmbDoktor.Text = "";CmbBrans.Text = "";
        }

        private void BtnHomePage_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ana menüye dön?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                isReturningToMain = true;
                FormGirisler frm = new FormGirisler();
                frm.Show();
                this.Close();
            }
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes) Application.Exit();
        }

        private void FormHastaDetay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isReturningToMain) return;
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Çıkmak istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.No) e.Cancel = true;
                else Application.Exit();
            }
        }
    }
}