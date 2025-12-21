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
    public partial class FormDoktorPaneli : Form
    {
        public FormDoktorPaneli()
        {
            InitializeComponent();
        }

        public string tc; // Giriş yapan sekreterin TC'si
        sqlBaglanti bgl = new sqlBaglanti();

        string secilenDoktorID = "";

        // Sekreter ve Hastane Bilgileri
        int mevcutHastaneID = -1;
        Guid mevcutSekreterID = Guid.Empty;
        private void FormDoktorPaneli_Load(object sender, EventArgs e)
        {
            
            // Cinsiyet kutusunu doldur.
            CmbCinsiyet.Items.Clear();
            CmbCinsiyet.Items.Add("Erkek");
            CmbCinsiyet.Items.Add("Kadın");
            SekreterBilgileriniGetir();
            GridGuncelle();
        }
        public void SekreterBilgileriniGetir()
        {
            NpgsqlConnection conn = bgl.baglanti();
            string tcHash = SecurityHelper.Hashle(tc);

            try
            {
                string sql = "SELECT sekreter_id, hastane_id FROM Sekreterler WHERE tc_hash = @p1";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                command.Parameters.AddWithValue("@p1", tcHash);

                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    mevcutSekreterID = dr.GetGuid(0);
                    if (dr[1] != DBNull.Value)
                    {
                        mevcutHastaneID = int.Parse(dr[1].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sekreter bilgisi alınırken hata: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void GridGuncelle()
        {
            DataTable dt = new DataTable();
            NpgsqlConnection conn = bgl.baglanti();

            try
            {
                // Doktor Listesi
                string sorgu = @"
                    SELECT 
                        d.doktor_id AS ""ID"",
                        d.doktor_ad AS ""Ad"",
                        d.doktor_soyad AS ""Soyad"",
                        b.brans_ad AS ""Branş"",
                        d.cinsiyet AS ""Cinsiyet""
                    FROM Doktorlar d
                    JOIN Branslar b ON d.brans_id = b.brans_id
                    WHERE d.hastane_id = @hastaneID
                    ORDER BY d.doktor_ad";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
                da.SelectCommand.Parameters.AddWithValue("@hastaneID", mevcutHastaneID);

                da.Fill(dt);
                dataGridView1.DataSource = dt;

                // 
                if (dataGridView1.Columns["ID"] != null)
                    dataGridView1.Columns["ID"].Visible = false;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Branşları Doldur.
                CmbBrans.DataSource = null;
                string sqlBrans = "SELECT brans_id, brans_ad FROM Branslar WHERE hastane_id = @hastaneID ORDER BY brans_ad";
                NpgsqlDataAdapter daBrans = new NpgsqlDataAdapter(sqlBrans, conn);
                daBrans.SelectCommand.Parameters.AddWithValue("@hastaneID", mevcutHastaneID);

                DataTable dtBrans = new DataTable();
                daBrans.Fill(dtBrans);

                CmbBrans.DisplayMember = "brans_ad";
                CmbBrans.ValueMember = "brans_id";
                CmbBrans.DataSource = dtBrans;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Liste yüklenirken hata: " + ex.Message);
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

                // SEKRETER TABLOSU KONTROLÜ
                string sqlSekreter = "SELECT COUNT(*) FROM Sekreterler WHERE tc_hash = @p1";
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
            if (MskTxtTC.Text.Length < 11 || string.IsNullOrWhiteSpace(TxtAd.Text) ||
                string.IsNullOrWhiteSpace(TxtSoyad.Text) || string.IsNullOrWhiteSpace(TxtSifre.Text) ||
                CmbCinsiyet.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen tüm alanları (Cinsiyet dahil) eksiksiz doldurunuz.");
                return;
            }
            string tcHash = SecurityHelper.Hashle(MskTxtTC.Text);
            if (TcZatenKayitliMi(tcHash) == true)
            {
                MessageBox.Show("Bu TC Kimlik Numarası sistemde zaten kayıtlı! (Hasta, Sekreter)",
                                "Mükerrer Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            NpgsqlConnection conn = bgl.baglanti();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("CALL sp_DoktorEkle(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8::cinsiyet_tipi, @p9, @p10)", conn);

                command.Parameters.AddWithValue("@p1", TxtAd.Text);
                command.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                command.Parameters.AddWithValue("@p3", int.Parse(CmbBrans.SelectedValue.ToString()));

                command.Parameters.AddWithValue("@p4", tcHash);
                command.Parameters.AddWithValue("@p5", SecurityHelper.Sifrele(MskTxtTC.Text));
                command.Parameters.AddWithValue("@p6", SecurityHelper.Hashle(TxtSifre.Text));
                command.Parameters.AddWithValue("@p7", SecurityHelper.Sifrele(TxtSifre.Text));

                command.Parameters.AddWithValue("@p8", CmbCinsiyet.Text.ToUpper());

                command.Parameters.AddWithValue("@p9", mevcutSekreterID);
                command.Parameters.AddWithValue("@p10", mevcutHastaneID);

                command.ExecuteNonQuery();
                MessageBox.Show("Doktor başarıyla sisteme eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GridGuncelle();
                TemizleAraclari();
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(ex.MessageText, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        // Güncelleme ve silme işlemi yaparken kullanılıyor.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                secilenDoktorID = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells["Ad"].Value.ToString();
                TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells["Soyad"].Value.ToString();
                CmbBrans.Text = dataGridView1.Rows[e.RowIndex].Cells["Branş"].Value.ToString();

                // Cinsiyet bilgisini doldur
                CmbCinsiyet.Text = dataGridView1.Rows[e.RowIndex].Cells["Cinsiyet"].Value.ToString();

                NpgsqlConnection conn = bgl.baglanti();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT tc_sifreli, sifre_sifreli FROM Doktorlar WHERE doktor_id=@p1", conn);
                cmd.Parameters.AddWithValue("@p1", Guid.Parse(secilenDoktorID));

                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MskTxtTC.Text = SecurityHelper.Coz(dr[0].ToString());
                    TxtSifre.Text = SecurityHelper.Coz(dr[1].ToString());
                }
                conn.Close();

                MskTxtTC.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri çekme hatası: " + ex.Message);
            }
        }
        // Silme İşlemi
        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(secilenDoktorID))
            {
                MessageBox.Show("Lütfen silinecek doktoru seçiniz.");
                return;
            }

            DialogResult secim = MessageBox.Show("Seçilen doktoru silmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secim == DialogResult.Yes)
            {
                NpgsqlConnection conn = bgl.baglanti();
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand("DELETE FROM Doktorlar WHERE doktor_id = @p1", conn);
                    command.Parameters.AddWithValue("@p1", Guid.Parse(secilenDoktorID));

                    command.ExecuteNonQuery();
                    MessageBox.Show("Kayıt silindi.");

                    GridGuncelle();
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
        // Güncelleme İşlemi
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(secilenDoktorID))
            {
                MessageBox.Show("Lütfen güncellenecek doktoru seçiniz.");
                return;
            }

            // Cinsiyet seçili mi kontrolü
            if (CmbCinsiyet.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen cinsiyet seçiniz.");
                return;
            }

            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                string sqlUpdate = @"UPDATE Doktorlar SET 
                                    doktor_ad=@p1, 
                                    doktor_soyad=@p2, 
                                    brans_id=@p3, 
                                    sifre_hash=@p4, 
                                    sifre_sifreli=@p5,
                                    cinsiyet=@p6::cinsiyet_tipi
                                    WHERE doktor_id=@p7";

                NpgsqlCommand command = new NpgsqlCommand(sqlUpdate, conn);

                command.Parameters.AddWithValue("@p1", TxtAd.Text);
                command.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                command.Parameters.AddWithValue("@p3", int.Parse(CmbBrans.SelectedValue.ToString()));

                command.Parameters.AddWithValue("@p4", SecurityHelper.Hashle(TxtSifre.Text));
                command.Parameters.AddWithValue("@p5", SecurityHelper.Sifrele(TxtSifre.Text));

                // Combobox'tan alınan değer
                command.Parameters.AddWithValue("@p6", CmbCinsiyet.Text.ToUpper());

                command.Parameters.AddWithValue("@p7", Guid.Parse(secilenDoktorID));

                command.ExecuteNonQuery();
                MessageBox.Show("Doktor bilgileri güncellendi.");

                GridGuncelle();
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

        private void Temizle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TemizleAraclari();
        }

        void TemizleAraclari()
        {
            secilenDoktorID = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            MskTxtTC.Text = "";
            TxtSifre.Text = "";
            CmbBrans.SelectedIndex = -1;
            CmbCinsiyet.SelectedIndex = -1; // Cinsiyeti de sıfırla
            MskTxtTC.Enabled = true;
            TxtAd.Focus();
        }
    }
}