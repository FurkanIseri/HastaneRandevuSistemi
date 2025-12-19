using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql; // PostgreSQL Kütüphanesi

namespace HastaneKayitSistemi
{
    public partial class FormRandevuListesi : Form
    {
        public FormRandevuListesi()
        {
            InitializeComponent();
        }
       // public string hasta_ID;
        sqlBaglanti bgl = new sqlBaglanti();
        void Listele()
        {
            DataTable dt = new DataTable();
            NpgsqlConnection conn = bgl.baglanti();

            string sorgu = @"
                SELECT 
                    r.randevu_id as ""ID"",
                    r.randevu_tarih as ""Tarih"",
                    r.randevu_saat as ""Saat"",
                    b.brans_ad as ""Branş"",
                    (d.doktor_ad || ' ' || d.doktor_soyad) as ""Doktor"",
                    r.randevu_durum as ""Durum"",
                    r.hasta_id as ""Hasta ID""
                FROM Randevular r
                JOIN Branslar b ON r.brans_id = b.brans_id
                JOIN Doktorlar d ON r.doktor_id = d.doktor_id
                ORDER BY r.randevu_id DESC";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            da.Fill(dt);

            dataGridView1.DataSource = dt;

            // Tablo görünüm ayarı
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            conn.Close();
        }

        private void FormRandevuListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Seçim yapılmış mı kontrolü
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek randevuyu seçiniz.");
                return;
            }

            int secilenIndex = dataGridView1.SelectedCells[0].RowIndex;
            string id = dataGridView1.Rows[secilenIndex].Cells["ID"].Value.ToString();

            DialogResult onay = MessageBox.Show("Bu randevu slotunu silmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay == DialogResult.Yes)
            {
                NpgsqlConnection conn = bgl.baglanti();

                try
                {
                    // Oluşturduğumuz Prosedürü Çağırıyoruz
                    NpgsqlCommand cmd = new NpgsqlCommand("CALL sp_RandevuSlotSil(@p1)", conn);
                    cmd.Parameters.AddWithValue("@p1", int.Parse(id));

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Randevu slotu başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listele(); // Listeyi güncelle
                }
                catch (PostgresException ex)
                {
                    MessageBox.Show("ENGEL: " + ex.MessageText, "İşlem Durduruldu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        public int secilen;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                secilen = dataGridView1.SelectedCells[0].RowIndex;
            }
        }
    }
}