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

namespace HastaneKayitSistemi
{
    public partial class FormBilgiDuzenle : Form
    {
        public FormBilgiDuzenle()
        {
            InitializeComponent();
        }
        public string TC;
        sqlBaglanti bgl = new sqlBaglanti();
        private void FormBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTxtTC.Text = TC; // Diğer formdan gelen TC'yi yaz
            MskTxtTC.Enabled = false; // TC değiştirilemez olsun (Genelde değiştirilmez)

            string tcHash = SecurityHelper.Hashle(TC);
            NpgsqlConnection conn = bgl.baglanti();

            try
            {
                // 1. Bilgileri Çek (Tablo adı: Hastalar)
                NpgsqlCommand komut = new NpgsqlCommand("SELECT hasta_ad, hasta_soyad, telefon_no, sifre_sifreli, cinsiyet FROM Hastalar WHERE tc_hash = @p1", conn);
                komut.Parameters.AddWithValue("@p1", tcHash);

                NpgsqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    TxtAd.Text = dr[0].ToString();
                    TxtSoyad.Text = dr[1].ToString();
                    MskTxtTelefon.Text = dr[2].ToString();

                    // Şifreyi çözüp gösteriyoruz ki kullanıcı eski şifresini görsün
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
            // 1. Boş Alan Kontrolü
            if (string.IsNullOrWhiteSpace(TxtAd.Text) || string.IsNullOrWhiteSpace(TxtSifre.Text))
            {
                MessageBox.Show("Lütfen ad, soyad ve şifre alanlarını boş bırakmayınız.");
                return;
            }

            string tcHash = SecurityHelper.Hashle(MskTxtTC.Text);

            // Şifre değişmiş olabilir, tekrar hashliyoruz ve şifreliyoruz
            string yeniSifreHash = SecurityHelper.Hashle(TxtSifre.Text);
            string yeniSifreSifreli = SecurityHelper.Sifrele(TxtSifre.Text);

            // Telefon maskesini temizle ( (555) 555-5555 -> 5555555555 )
            string safTelefon = MskTxtTelefon.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

            NpgsqlConnection conn = bgl.baglanti();

            try
            {
                // 2. Güncelleme Sorgusu (Tablo Adı: Hastalar)
                // Sütun Adları: telefon_no (hasta_telefon değil), cinsiyet_tipi cast işlemi
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
                command.Parameters.AddWithValue("@p3", safTelefon); // Temizlenmiş telefon
                command.Parameters.AddWithValue("@p4", yeniSifreHash);
                command.Parameters.AddWithValue("@p5", yeniSifreSifreli);

                // Cinsiyet seçili değilse varsayılan değer gönder
                string cinsiyet = CmbCinsiyet.Text.ToUpper();
                command.Parameters.AddWithValue("@p6", cinsiyet);

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
