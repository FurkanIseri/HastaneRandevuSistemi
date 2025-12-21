using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Npgsql;

namespace HastaneRandevuSistemi
{
    public partial class FormBilgiDuzenle : Form
    {
        public FormBilgiDuzenle()
        {
            InitializeComponent();
        }
        // Hasta panelinden kullanıcın TC sini almak için koyulan değişken.
        public string TC;
        sqlBaglanti bgl = new sqlBaglanti();
        private void FormBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTxtTC.Text = TC; // Diğer formdan gelen TC'yi yaz
            MskTxtTC.Enabled = false; // TC değiştirilemez.

            // Bilgiyi değiştirirken TC bilgisi önemli onuda hash olarak tutuyoruz.
            string tcHash = SecurityHelper.Hashle(TC);
            NpgsqlConnection conn = bgl.baglanti();

            try
            {
                // Hasta bilgilerini çek.
                NpgsqlCommand command = new NpgsqlCommand("SELECT hasta_ad, hasta_soyad, telefon_no, sifre_sifreli, cinsiyet FROM Hastalar WHERE tc_hash = @p1", conn);
                command.Parameters.AddWithValue("@p1", tcHash);

                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    TxtAd.Text = dr[0].ToString();
                    TxtSoyad.Text = dr[1].ToString();
                    MskTxtTelefon.Text = dr[2].ToString();

                    // Şifreyi çözüp gösteriyoruz ki kullanıcı eski şifresini görsün.
                    TxtSifre.Text = SecurityHelper.Coz(dr[3].ToString());

                    CmbCinsiyet.Text = dr[4].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bilgiler yüklenirken hata oluştu: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            // Boş Alan Kontrolü.
            if (string.IsNullOrWhiteSpace(TxtAd.Text) || string.IsNullOrWhiteSpace(TxtSifre.Text))
            {
                MessageBox.Show("Lütfen ad, soyad ve şifre alanlarını boş bırakmayınız.");
                return;
            }

            string tcHash = SecurityHelper.Hashle(MskTxtTC.Text);

            // Şifre değişmiş olabilir, tekrar hashliyoruz ve şifreliyoruz
            string yeniSifreHash = SecurityHelper.Hashle(TxtSifre.Text);
            string yeniSifreSifreli = SecurityHelper.Sifrele(TxtSifre.Text);

            // Telefon maskesini veritabanına uygun şekilde düzenlemek.
            string telefonNo = MskTxtTelefon.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

            NpgsqlConnection conn = bgl.baglanti();

            try
            {
                // Güncelleme sorgusu
                string sql = @"UPDATE Hastalar SET 
                       hasta_ad = @p1, 
                       hasta_soyad = @p2, 
                       telefon_no = @p3, 
                       sifre_hash = @p4, 
                       sifre_sifreli = @p5, 
                       cinsiyet = @p6::cinsiyet_tipi 
                       WHERE tc_hash = @p7";

                NpgsqlCommand command = new NpgsqlCommand(sql, conn);

                command.Parameters.AddWithValue("@p1", TxtAd.Text);
                command.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                command.Parameters.AddWithValue("@p3", telefonNo); // Temizlenmiş telefon
                command.Parameters.AddWithValue("@p4", yeniSifreHash);
                command.Parameters.AddWithValue("@p5", yeniSifreSifreli);

                command.Parameters.AddWithValue("@p6", CmbCinsiyet.Text.ToUpper());

                command.Parameters.AddWithValue("@p7", tcHash);

                command.ExecuteNonQuery();

                MessageBox.Show("Bilgileriniz başarıyla güncellendi.\nYeni Şifreniz: " + TxtSifre.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close(); // Formu kapat
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Bağlantıyı garanti kapat
                conn.Close();
            }
        }
    }
}
