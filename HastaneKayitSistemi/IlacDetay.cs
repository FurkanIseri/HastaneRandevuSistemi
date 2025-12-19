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
    public partial class IlacDetay : Form
    {
        public IlacDetay()
        {
            InitializeComponent();
        }

        public string tc; 
        sqlBaglanti bgl = new sqlBaglanti();
        string secilenIlacID = "";

        public int HastaneIDBul()
        {
            int id = 0;
            NpgsqlConnection conn = bgl.baglanti();
            string tcHash = SecurityHelper.Hashle(tc);

            try
            {
                string sql = "SELECT hastane_id FROM Sekreterler WHERE tc_hash = @p1";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                command.Parameters.AddWithValue("@p1", tcHash);

                object sonuc = command.ExecuteScalar();
                if (sonuc != null)
                {
                    id = int.Parse(sonuc.ToString());
                }
            }
            catch
            {
                id = 1;
            }
            finally
            {
                conn.Close();
            }
            return id;
        }

        void Listele()
        {
            int hastaneID = HastaneIDBul();
            DataTable dt = new DataTable();
            NpgsqlConnection conn = bgl.baglanti();

            string sorgu = @"SELECT ilac_id as ""ID"", ilac_ad as ""İlaç Adı"", stok_adet as ""Adet"" 
                             FROM Ilaclar 
                             WHERE hastane_id = @p1 
                             ORDER BY ilac_ad ASC";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            da.SelectCommand.Parameters.AddWithValue("@p1", hastaneID);

            da.Fill(dt);
            dataGridView1.DataSource = dt;

            conn.Close();
        }

        private void IlacDetay_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtIlacAd.Text) || string.IsNullOrEmpty(TxtAdet.Text))
            {
                MessageBox.Show("Lütfen ilaç adı ve adet giriniz.");
                return;
            }

            int hastaneID = HastaneIDBul(); // Otomatik ID bulma
            NpgsqlConnection conn = bgl.baglanti();

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Ilaclar (ilac_ad, stok_adet, hastane_id) VALUES (@p1, @p2, @p3)", conn);
                command.Parameters.AddWithValue("@p1", TxtIlacAd.Text);
                command.Parameters.AddWithValue("@p2", int.Parse(TxtAdet.Text));
                command.Parameters.AddWithValue("@p3", hastaneID);

                command.ExecuteNonQuery();
                MessageBox.Show("İlaç eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Listele();
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


        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(secilenIlacID))
            {
                MessageBox.Show("Lütfen tablodan silinecek ilacı seçiniz.");
                return;
            }

            DialogResult secim = MessageBox.Show("Seçilen ilacı silmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secim == DialogResult.Yes)
            {
                NpgsqlConnection conn = bgl.baglanti();
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand("DELETE FROM Ilaclar WHERE ilac_id = @p1", conn);
                    command.Parameters.AddWithValue("@p1", int.Parse(secilenIlacID));

                    command.ExecuteNonQuery();
                    MessageBox.Show("İlaç silindi.");

                    Listele();
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
            if (string.IsNullOrEmpty(secilenIlacID))
            {
                MessageBox.Show("Lütfen tablodan güncellenecek ilacı seçiniz.");
                return;
            }

            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE Ilaclar SET ilac_ad=@p1, stok_adet=@p2 WHERE ilac_id=@p3", conn);
                command.Parameters.AddWithValue("@p1", TxtIlacAd.Text);
                command.Parameters.AddWithValue("@p2", int.Parse(TxtAdet.Text));
                command.Parameters.AddWithValue("@p3", int.Parse(secilenIlacID));

                command.ExecuteNonQuery();
                MessageBox.Show("İlaç güncellendi.");

                Listele();
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SatirSec(e.RowIndex);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SatirSec(e.RowIndex);
        }

        void SatirSec(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                secilenIlacID = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                TxtIlacAd.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
                TxtAdet.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
            }
        }

        private void Temizle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TemizleAraclari();
        }

        void TemizleAraclari()
        {
            secilenIlacID = "";
            TxtIlacAd.Text = "";
            TxtAdet.Text = "";
            TxtIlacAd.Focus();
        }
    }
}