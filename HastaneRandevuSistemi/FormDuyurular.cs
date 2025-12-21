using System;
using System.Data;
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

        // Bu formu açan kişi (Doktor veya Sekreter) buraya kendi hastane ID'sini gönderecek.
        public int hastaneID = -1;

        public void GridleriGuncelle()
        {
            // Eğer ID gelmemişse boşuna sorgu yapma
            if (hastaneID == -1) return;

            DataTable dt = new DataTable();
            NpgsqlConnection conn = bgl.baglanti();

            // SORGEYU GÜNCELLEDİK: WHERE hastane_id = @p1 şartı eklendi.
            string sorgu = @"SELECT 
                                duyurular_id as ""ID"", 
                                duyurular_text as ""Duyuru Metni"" 
                             FROM Duyurular 
                             WHERE hastane_id = @p1 
                             ORDER BY duyurular_id DESC";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            // Parametreyi ekliyoruz
            da.SelectCommand.Parameters.AddWithValue("@p1", hastaneID);

            da.Fill(dt);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;

            // Görünüm ayarları
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // ID sütununu gizleyebilirsin istersen:
            // dataGridView1.Columns["ID"].Visible = false;

            dataGridView1.Refresh();
            conn.Close();
        }

        private void FormDuyurular_Load(object sender, EventArgs e)
        {
            // Form yüklenirken listeyi getir
            GridleriGuncelle();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek duyuruyu seçiniz.");
                return;
            }

            int secilenIndex = dataGridView1.SelectedCells[0].RowIndex;
            string id = dataGridView1.Rows[secilenIndex].Cells["ID"].Value.ToString();

            DialogResult onay = MessageBox.Show("Bu duyuruyu silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (onay == DialogResult.Yes)
            {
                NpgsqlConnection conn = bgl.baglanti();
                try
                {
                    // Silme işleminde ID yeterlidir ama güvenlik için hastane kontrolü de eklenebilir
                    NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM Duyurular WHERE duyurular_id = @p1", conn);
                    cmd.Parameters.AddWithValue("@p1", int.Parse(id));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Duyuru başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    GridleriGuncelle();
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