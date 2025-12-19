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

namespace HastaneKayitSistemi
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
                NpgsqlCommand cmdBul = new NpgsqlCommand("SELECT hastane_id FROM Sekreterler WHERE tc_hash=@p1", conn);
                cmdBul.Parameters.AddWithValue("@p1", tcHash);

                object sonuc = cmdBul.ExecuteScalar();
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

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            if (MskTxtTC.Text.Length < 11 || string.IsNullOrWhiteSpace(TxtAd.Text) || string.IsNullOrWhiteSpace(TxtSifre.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                return;
            }

            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                string loginTcHash = SecurityHelper.Hashle(TC);
                int hastaneID = 1;

                NpgsqlCommand cmdIdBul = new NpgsqlCommand("SELECT hastane_id FROM Sekreterler WHERE tc_hash=@p1", conn);
                cmdIdBul.Parameters.AddWithValue("@p1", loginTcHash);
                object idSonuc = cmdIdBul.ExecuteScalar();
                if (idSonuc != null) hastaneID = int.Parse(idSonuc.ToString());

                string sql = @"INSERT INTO Sekreterler 
                               (sekreter_ad, sekreter_soyad, tc_hash, tc_sifreli, sifre_hash, sifre_sifreli, cinsiyet, hastane_id)
                               VALUES 
                               (@p1, @p2, @p3, @p4, @p5, @p6, @p7::cinsiyet_tipi, @p8)";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
                cmd.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                cmd.Parameters.AddWithValue("@p3", SecurityHelper.Hashle(MskTxtTC.Text));
                cmd.Parameters.AddWithValue("@p4", SecurityHelper.Sifrele(MskTxtTC.Text));
                cmd.Parameters.AddWithValue("@p5", SecurityHelper.Hashle(TxtSifre.Text));
                cmd.Parameters.AddWithValue("@p6", SecurityHelper.Sifrele(TxtSifre.Text));
                cmd.Parameters.AddWithValue("@p7", CmbCinsiyet.Text);
                cmd.Parameters.AddWithValue("@p8", hastaneID);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Sekreter başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                    // DÜZELTME: Veritabanı ID tipin UUID olduğu için Guid.Parse kullanıldı
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

            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                string sql = @"UPDATE Sekreterler SET 
                               sekreter_ad = @p1, 
                               sekreter_soyad = @p2, 
                               cinsiyet = @p3::cinsiyet_tipi 
                               WHERE sekreter_id = @p4";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
                cmd.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                cmd.Parameters.AddWithValue("@p3", CmbCinsiyet.Text);

                // DÜZELTME: Guid.Parse kullanıldı
                cmd.Parameters.AddWithValue("@p4", Guid.Parse(secilenSekreterID));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Bilgiler güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                // DÜZELTME: Guid.Parse kullanıldı
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