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
    public partial class FormRandevuListesi : Form
    {
        public FormRandevuListesi()
        {
            InitializeComponent();
        }
        public string sekreterTC; // FormSekreterDetay'dan buraya TC gelecek
        sqlBaglanti bgl = new sqlBaglanti();
        int hastaneID = -1; // Sekreterin hastanesini burada tutacağız

        // Sekreterin hangi hastaneye bağlı olduğunu bulan fonksiyon
        void SekreterHastaneBul()
        {
            NpgsqlConnection conn = bgl.baglanti();
            // TC'yi Hashleyip arıyoruz 
            string tcHash = SecurityHelper.Hashle(sekreterTC);

            try
            {
                // Sekreter tablosundan hastane_id'yi çekiyoruz
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT hastane_id FROM Sekreterler WHERE tc_hash = @p1", conn);
                cmd.Parameters.AddWithValue("@p1", tcHash);

                object sonuc = cmd.ExecuteScalar();
                if (sonuc != null)
                {
                    hastaneID = int.Parse(sonuc.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hastane bilgisi alınamadı: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        void Listele()
        {
            // Eğer hastane ID bulunamadıysa listeyi boşuna çekme
            if (hastaneID == -1) return;

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
                WHERE d.hastane_id = @p1  
                ORDER BY r.randevu_id DESC";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
            // Parametre olarak bulduğumuz hastaneID'yi gönderiyoruz
            da.SelectCommand.Parameters.AddWithValue("@p1", hastaneID);

            da.Fill(dt);
            dataGridView1.DataSource = dt;


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            conn.Close();
        }

        private void FormRandevuListesi_Load(object sender, EventArgs e)
        {
            // Önce Sekreterin hastanesini bul, sonra o hastaneye göre listele
            SekreterHastaneBul();
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
                    NpgsqlCommand command = new NpgsqlCommand("CALL sp_RandevuSil(@p1)", conn);
                    command.Parameters.AddWithValue("@p1", int.Parse(id));

                    command.ExecuteNonQuery();

                    MessageBox.Show("Randevu slotu başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listele(); 
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