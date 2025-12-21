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
    public partial class FormHastaKayit : Form
    {
        public FormHastaKayit()
        {
            InitializeComponent();
        }

        sqlBaglanti bgl = new sqlBaglanti();
        // Bu fonksiyonu Class'ın içine, diğer void'lerin yanına ekle
        private bool TcZatenKayitliMi(string tcHash)
        {
            // Bağlantı sınıfını çağır
            NpgsqlConnection conn = bgl.baglanti();

            bool kayitVarMi = false;

            try
            {
                // DOKTORLAR TABLOSU KONTROLÜ
                string sorguDoktor = "SELECT COUNT(*) FROM Doktorlar WHERE tc_hash = @p1";
                using (NpgsqlCommand command = new NpgsqlCommand(sorguDoktor, conn))
                {
                    command.Parameters.AddWithValue("@p1", tcHash);
                    int sayi = Convert.ToInt32(command.ExecuteScalar());
                    if (sayi > 0) return true; 
                }

                // SEKRETER TABLOSU KONTROLÜ
                string sorguSekreter = "SELECT COUNT(*) FROM Sekreterler WHERE tc_hash = @p1";
                using (NpgsqlCommand command = new NpgsqlCommand(sorguSekreter, conn))
                {
                    command.Parameters.AddWithValue("@p1", tcHash);
                    int sayi = Convert.ToInt32(command.ExecuteScalar());
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
        private void BtnKayit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtAd.Text) ||
                string.IsNullOrWhiteSpace(TxtSoyad.Text) ||
                !MskTxtTC.MaskFull ||       // TC Kısmı dolmadıysa
                !MskTxtTelefon.MaskFull ||  // Telefon Kısmı dolmadıysa
                string.IsNullOrWhiteSpace(TxtSifre.Text) ||
                CmbCinsiyet.SelectedIndex == -1) // Cinsiyet seçilmediyse
            {
                MessageBox.Show("Lütfen tüm alanları eksiksiz doldurunuz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Güvenlik işlemleri
            string tcHash = SecurityHelper.Hashle(MskTxtTC.Text);
            string tcSifreli = SecurityHelper.Sifrele(MskTxtTC.Text);
            string sifreHash = SecurityHelper.Hashle(TxtSifre.Text);
            string sifreSifreli = SecurityHelper.Sifrele(TxtSifre.Text);

            // Telefon numarasını düzeltme.
            string telefonNo = MskTxtTelefon.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            if (TcZatenKayitliMi(tcHash) == true)
            {
                MessageBox.Show("Bu TC Kimlik Numarası sistemde zaten kayıtlı! (Doktor, Sekreter)",
                                "Mükerrer Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            NpgsqlConnection conn = bgl.baglanti();

            try
            {
                // Parametreleri eklerken procedure kullanılmıştır.
                NpgsqlCommand command = new NpgsqlCommand("CALL sp_HastaKayit(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8::cinsiyet_tipi)", conn);

                command.Parameters.AddWithValue("@p1", TxtAd.Text);
                command.Parameters.AddWithValue("@p2", TxtSoyad.Text);

                command.Parameters.AddWithValue("@p3", tcHash);
                command.Parameters.AddWithValue("@p4", tcSifreli);

                // Temizlenmiş telefonu gönderiyoruz
                command.Parameters.AddWithValue("@p5", telefonNo);

                command.Parameters.AddWithValue("@p6", sifreHash);
                command.Parameters.AddWithValue("@p7", sifreSifreli);

                command.Parameters.AddWithValue("@p8", CmbCinsiyet.Text.ToUpper());

                command.ExecuteNonQuery();

                MessageBox.Show("Kaydınız başarıyla oluşturuldu.\nŞifreniz: " + TxtSifre.Text, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (PostgresException ex)
            {
                MessageBox.Show("Kayıt Başarısız: " + ex.MessageText, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void FormHastaKayit_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}