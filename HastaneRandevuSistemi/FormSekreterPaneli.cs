using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace HastaneRandevuSistemi
{
    public partial class FormSekreterPaneli : Form
    {
        public FormSekreterPaneli()
        {
            InitializeComponent();
        }

        public string TC;
        sqlBaglanti bgl = new sqlBaglanti();
        string secilenSekreterID = "0";

        private void FormSekreterPaneli_Load(object sender, EventArgs e)
        {
            SekreterListesiGetir();
            TemizleAraclari();
        }

        void SekreterListesiGetir()
        {
            string tcHash = SecurityHelper.Hashle(TC);
            int hastaneID = 0;

            NpgsqlConnection conn = bgl.baglanti();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT hastane_id FROM Sekreterler WHERE tc_hash=@p1", conn);
                command.Parameters.AddWithValue("@p1", tcHash);

                object sonuc = command.ExecuteScalar();
                if (sonuc != null)
                {
                    hastaneID = int.Parse(sonuc.ToString());
                }

                DataTable dt = new DataTable();
                string sorgu = @"SELECT 
                                    sekreter_id as ""ID"", 
                                    sekreter_ad as ""Ad"", 
                                    sekreter_soyad as ""Soyad"",
                                    cinsiyet as ""Cinsiyet""
                                 FROM Sekreterler 
                                 WHERE hastane_id = @hastaneID
                                 ORDER BY sekreter_ad ASC";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
                da.SelectCommand.Parameters.AddWithValue("@hastaneID", hastaneID);

                da.Fill(dt);
                dataGridView1.DataSource = dt;

                dataGridView1.Columns["ID"].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private bool TcZatenKayitliMi(string tcHash)
        {
            // Bağlantı sınıfını çağır
            NpgsqlConnection conn = bgl.baglanti();

            bool kayitVarMi = false;

            try
            {
                // HASTALAR TABLOSU KONTROLÜ
                string sqlDoktor = "SELECT COUNT(*) FROM Hastalar WHERE tc_hash = @p1";
                using (NpgsqlCommand command = new NpgsqlCommand(sqlDoktor, conn))
                {
                    command.Parameters.AddWithValue("@p1", tcHash);
                    int sayi = Convert.ToInt32(command.ExecuteScalar());
                    if (sayi > 0) return true;
                }

                // DOKTORLAR TABLOSU KONTROLÜ
                string sqlSekreter = "SELECT COUNT(*) FROM Doktorlar WHERE tc_hash = @p1";
                using (NpgsqlCommand command2 = new NpgsqlCommand(sqlSekreter, conn))
                {
                    command2.Parameters.AddWithValue("@p1", tcHash);
                    int sayi = Convert.ToInt32(command2.ExecuteScalar());
                    if (sayi > 0) return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kontrol sırasında hata: " + ex.Message);
                return true;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }

            return false;
        }
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            // Boş alan kontrolü
            if (MskTxtTC.Text.Length < 11 || string.IsNullOrWhiteSpace(TxtAd.Text) || string.IsNullOrWhiteSpace(TxtSifre.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string tcHash = SecurityHelper.Hashle(MskTxtTC.Text);
            if (TcZatenKayitliMi(tcHash) == true)
            {
                MessageBox.Show("Bu TC Kimlik Numarası sistemde zaten kayıtlı! (Hasta, Doktor)",
                                "Mükerrer Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            NpgsqlConnection conn = bgl.baglanti();

            try
            {
                // İşlemi yapan mevcut sekreterin Hastane ID'sini bulma
                string loginTcHash = SecurityHelper.Hashle(TC);
                int hastaneID = 1;

                NpgsqlCommand cmdIdBul = new NpgsqlCommand("SELECT hastane_id FROM Sekreterler WHERE tc_hash=@p1", conn);
                cmdIdBul.Parameters.AddWithValue("@p1", loginTcHash);
                object idSonuc = cmdIdBul.ExecuteScalar();
                if (idSonuc != null) hastaneID = int.Parse(idSonuc.ToString());

                // STORED PROCEDURE ÇAĞRISI
                string sql = "CALL sp_SekreterEkle(@p1, @p2, @p3, @p4, @p5, @p6, @p7::cinsiyet_tipi, @p8)";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
                cmd.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                cmd.Parameters.AddWithValue("@p3", SecurityHelper.Hashle(MskTxtTC.Text));
                cmd.Parameters.AddWithValue("@p4", SecurityHelper.Sifrele(MskTxtTC.Text));
                cmd.Parameters.AddWithValue("@p5", SecurityHelper.Hashle(TxtSifre.Text));
                cmd.Parameters.AddWithValue("@p6", SecurityHelper.Sifrele(TxtSifre.Text));
                cmd.Parameters.AddWithValue("@p7", CmbCinsiyet.Text.ToUpper());
                cmd.Parameters.AddWithValue("@p8", hastaneID);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Sekreter başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SekreterListesiGetir();
                TemizleAraclari();
            }
            catch (PostgresException ex)
            {
                if (ex.MessageText.Contains("Bu T.C. Kimlik Numarası ile kayıtlı"))
                {
                    MessageBox.Show("Bu T.C. Kimlik Numarası ile kayıtlı bir sekreter zaten mevcut!", "Mükerrer Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Veritabanı Hatası: " + ex.MessageText, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Genel Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (secilenSekreterID == "0" || string.IsNullOrEmpty(secilenSekreterID))
            {
                MessageBox.Show("Lütfen silinecek kaydı seçiniz.");
                return;
            }

            DialogResult onay = MessageBox.Show("Bu sekreteri silmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (onay == DialogResult.Yes)
            {
                NpgsqlConnection conn = bgl.baglanti();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM Sekreterler WHERE sekreter_id = @p1", conn);
                    cmd.Parameters.AddWithValue("@p1", Guid.Parse(secilenSekreterID));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kayıt silindi.");

                    SekreterListesiGetir();
                    TemizleAraclari();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            if (secilenSekreterID == "0" || string.IsNullOrEmpty(secilenSekreterID))
            {
                MessageBox.Show("Lütfen güncellenecek kaydı seçiniz.");
                return;
            }

            // Boş Alan Kontrolü 
            if (string.IsNullOrWhiteSpace(TxtAd.Text) || string.IsNullOrWhiteSpace(TxtSoyad.Text))
            {
                MessageBox.Show("Ad ve Soyad alanları boş bırakılamaz.");
                return;
            }

            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                string sorgu = @"UPDATE Sekreterler SET 
                       sekreter_ad = @p1, 
                       sekreter_soyad = @p2, 
                       cinsiyet = @p3::cinsiyet_tipi";

                bool sifreGuncellenecek = !string.IsNullOrWhiteSpace(TxtSifre.Text);
                if (sifreGuncellenecek)
                {
                    sorgu += ", sifre_hash = @p5, sifre_sifreli = @p6";
                }

                // Sorgunun sonunu (WHERE şartını) ekle
                sorgu += " WHERE sekreter_id = @p4";

                NpgsqlCommand command = new NpgsqlCommand(sorgu, conn);

                // Parametreleri Ekle
                command.Parameters.AddWithValue("@p1", TxtAd.Text);
                command.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                command.Parameters.AddWithValue("@p3", CmbCinsiyet.Text.ToUpper());
                command.Parameters.AddWithValue("@p4", Guid.Parse(secilenSekreterID)); 

                // Eğer şifre güncellenecekse, o parametreleri de ekle
                if (sifreGuncellenecek)
                {
                    command.Parameters.AddWithValue("@p5", SecurityHelper.Hashle(TxtSifre.Text));
                    command.Parameters.AddWithValue("@p6", SecurityHelper.Sifrele(TxtSifre.Text));
                }

                command.ExecuteNonQuery();
                MessageBox.Show("Bilgiler başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SekreterListesiGetir();
                TemizleAraclari();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme hatası: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        // Sekreter bilgilerini ilgili alanlara doldurmak.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                secilenSekreterID = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells["Ad"].Value.ToString();
                TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells["Soyad"].Value.ToString();
                CmbCinsiyet.Text = dataGridView1.Rows[e.RowIndex].Cells["Cinsiyet"].Value.ToString();

                NpgsqlConnection conn = bgl.baglanti();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT tc_sifreli, sifre_sifreli FROM Sekreterler WHERE sekreter_id=@p1", conn);

                cmd.Parameters.AddWithValue("@p1", Guid.Parse(secilenSekreterID));

                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string dbTcSifreli = dr[0].ToString();
                    string dbSifreSifreli = dr[1].ToString();

                    MskTxtTC.Text = SecurityHelper.Coz(dbTcSifreli);
                    TxtSifre.Text = SecurityHelper.Coz(dbSifreSifreli);
                }
                conn.Close();

                MskTxtTC.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri çekme hatası: " + ex.Message);
            }
        }

        private void Temizle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TemizleAraclari();
        }

        void TemizleAraclari()
        {
            secilenSekreterID = "0";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            MskTxtTC.Text = "";
            TxtSifre.Text = "";
            CmbCinsiyet.SelectedIndex = -1;
            CmbCinsiyet.Text = "";
            MskTxtTC.Enabled = true;
            TxtAd.Focus();
        }
    }
}