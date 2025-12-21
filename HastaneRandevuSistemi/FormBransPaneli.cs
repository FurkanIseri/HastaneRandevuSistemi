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
    public partial class FormBransPaneli : Form
    {
        public FormBransPaneli()
        {
            InitializeComponent();
        }

        public string tc; // Sekreter Detay formundan gelen TC.
        sqlBaglanti bgl = new sqlBaglanti();

        // Her hastanenin farklı branşları olabileceği için burda değişken olarak belirliyoruz.
        int mevcutHastaneID = -1;
        private void FormBransPaneli_Load(object sender, EventArgs e)
        {
            SekreterinHastanesiniBul();
        }
        // Sekreterin Hastanesini bulma.  
        void SekreterinHastanesiniBul()
        {
            NpgsqlConnection conn = bgl.baglanti();
            string tcHash = SecurityHelper.Hashle(tc);

            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT hastane_id FROM Sekreterler WHERE tc_hash = @p1", conn);
                command.Parameters.AddWithValue("@p1", tcHash);

                object sonuc = command.ExecuteScalar();
                if (sonuc != null)
                {
                    mevcutHastaneID = int.Parse(sonuc.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hastane bilgisi alınırken hata: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        // Tabloyu Güncelleme
        void GridGuncelle()
        {
            DataTable dt = new DataTable();
            NpgsqlConnection conn = bgl.baglanti();

            try
            {
                // WHERE şartı sayesinde sadece oturum açan kişinin hastanesini getiriyoruz.
                string sql = @"SELECT brans_id as ""ID"", brans_ad as ""Branş Adı"" 
                               FROM Branslar 
                               WHERE hastane_id = @p1 
                               ORDER BY brans_ad";

                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                command.Parameters.AddWithValue("@p1", mevcutHastaneID);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                // ID sütununu gizleyelim
                if (dataGridView1.Columns["ID"] != null)
                {
                    dataGridView1.Columns["ID"].Visible = false;
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
        // Branş ekleme işlemi
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBrans.Text))
            {
                MessageBox.Show("Lütfen branş adı giriniz.");
                return;
            }

            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                // Sadece mevcut hastaneye ekleme yapıyoruz.
                string sql = "INSERT INTO Branslar (brans_ad, hastane_id) VALUES (@p1, @p2)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@p1", TxtBrans.Text);
                cmd.Parameters.AddWithValue("@p2", mevcutHastaneID);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Branş başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GridGuncelle();
                Temizle();
            }
            catch (PostgresException ex)
            {
                // Aynı hastanede aynı isimde branş varsa hata verir.
                if (ex.SqlState == "23505")
                    MessageBox.Show("Bu hastanede bu isimde bir branş zaten mevcut!", "Çakışma", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Veritabanı hatası: " + ex.MessageText);
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
        // Silme İşlemi
        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LblID.Text))
            {
                MessageBox.Show("Lütfen silinecek branşı listeden seçiniz.");
                return;
            }

            DialogResult onay = MessageBox.Show("Bu branşı silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (onay == DialogResult.Yes)
            {
                NpgsqlConnection conn = bgl.baglanti();
                try
                {
                    string sql = "DELETE FROM Branslar WHERE brans_id = @p1";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@p1", int.Parse(LblID.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Branş silindi.");

                    GridGuncelle();
                    Temizle();
                }
                catch (PostgresException ex)
                {
                    // Eğer branşa bağlı doktor varsa silinmez.
                    if (ex.SqlState == "23503")
                        MessageBox.Show("Bu branşa kayıtlı doktorlar olduğu için silinemez!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Veritabanı hatası: " + ex.MessageText);
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
            if (string.IsNullOrWhiteSpace(LblID.Text) || string.IsNullOrWhiteSpace(TxtBrans.Text))
            {
                MessageBox.Show("Lütfen güncellenecek branşı seçiniz.");
                return;
            }

            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                string sql = "UPDATE Branslar SET brans_ad = @p1 WHERE brans_id = @p2";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@p1", TxtBrans.Text);
                cmd.Parameters.AddWithValue("@p2", int.Parse(LblID.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Branş güncellendi.");

                GridGuncelle();
                Temizle();
            }
            catch (PostgresException ex)
            {
                // Aynı hastanede aynı isimde branş varsa hata verir.
                if (ex.SqlState == "23505")
                    MessageBox.Show("Bu isimde başka bir branş zaten var.", "Çakışma", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Hata: " + ex.MessageText);
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
        // Hücreye tıklayınca veriyi dolduruyor.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                LblID.Text = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                TxtBrans.Text = dataGridView1.Rows[e.RowIndex].Cells["Branş Adı"].Value.ToString();
            }
        }
        // Temizle butonu
        private void Temizle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Temizle();
        }

        // Temizle Fonksiyonu
        void Temizle()
        {
            TxtBrans.Text = "";
            LblID.Text = "";
            // İmleci o textBox a yönlendirir.
            TxtBrans.Focus();
        }
    }
}