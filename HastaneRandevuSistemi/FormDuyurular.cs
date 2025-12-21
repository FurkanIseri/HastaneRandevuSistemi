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
    public partial class FormDuyurular : Form
    {
        public FormDuyurular()
        {
            InitializeComponent();
        }

        sqlBaglanti bgl = new sqlBaglanti();

        public void GridleriGuncelle()
        {
            DataTable dt = new DataTable();
            NpgsqlConnection conn = bgl.baglanti();

            // Duyurular tablosundan ID ve duyurunun metnini çekiyoruz
            string sorgu = @"SELECT 
                                duyurular_id as ""ID"", 
                                duyurular_text as ""Duyuru Metni"" 
                             FROM Duyurular 
                             ORDER BY duyurular_id DESC";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            da.Fill(dt);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;

            // Görünüm ayarları
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Refresh();

            conn.Close();
        }

        private void FormDuyurular_Load(object sender, EventArgs e)
        {
            GridleriGuncelle();
        }
        public void button1_Click(object sender, EventArgs e)
        {
            // Seçim kontrolü
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek duyuruyu seçiniz.");
                return;
            }

            // Seçilen satırın ID'sini al.
            int secilenIndex = dataGridView1.SelectedCells[0].RowIndex;
            string id = dataGridView1.Rows[secilenIndex].Cells["ID"].Value.ToString();

            DialogResult onay = MessageBox.Show("Bu duyuruyu silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (onay == DialogResult.Yes)
            {
                NpgsqlConnection conn = bgl.baglanti();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM Duyurular WHERE duyurular_id = @p1", conn);
                    cmd.Parameters.AddWithValue("@p1", int.Parse(id));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Duyuru başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    GridleriGuncelle(); // Listeyi yenile
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
    }
}