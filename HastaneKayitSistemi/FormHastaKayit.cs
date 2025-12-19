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
    public partial class FormHastaKayit : Form
    {
        public FormHastaKayit()
        {
            InitializeComponent();
        }

        sqlBaglanti bgl = new sqlBaglanti();

        private void BtnKayit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtAd.Text) ||
                string.IsNullOrWhiteSpace(TxtSoyad.Text) ||
                !MskTxtTC.MaskFull ||       // TC maskesi dolmadıysa
                !MskTxtTelefon.MaskFull ||  // Telefon maskesi dolmadıysa
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

            // DÜZELTME: Telefon numarasındaki ( ) - ve boşlukları temizleyip saf numara elde ediyoruz.
            string safTelefon = MskTxtTelefon.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

            NpgsqlConnection conn = bgl.baglanti();

            try
            {
                // Parametreleri eklerken Stored Procedure sırasına dikkat edelim
                NpgsqlCommand command = new NpgsqlCommand("CALL sp_HastaKayit(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8::cinsiyet_tipi)", conn);

                command.Parameters.AddWithValue("@p1", TxtAd.Text);
                command.Parameters.AddWithValue("@p2", TxtSoyad.Text);

                command.Parameters.AddWithValue("@p3", tcHash);
                command.Parameters.AddWithValue("@p4", tcSifreli);

                // Temizlenmiş telefonu gönderiyoruz
                command.Parameters.AddWithValue("@p5", safTelefon);

                command.Parameters.AddWithValue("@p6", sifreHash);
                command.Parameters.AddWithValue("@p7", sifreSifreli);

                // 3. DÜZELTME: ToUpper() kaldırıldı. Veritabanındaki enum 'Erkek' ise 'Erkek' gitmeli.
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

        private void FormHastaKayit_Load(object sender, EventArgs e)
        {

        }
    }
}