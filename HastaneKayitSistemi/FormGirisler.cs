using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneKayitSistemi
{
    public partial class FormGirisler : Form
    {
        public FormGirisler()
        {
            InitializeComponent();
        }
        
        private void BtnDoktor_Click(object sender, EventArgs e)
        {
            FormDoktorGiris frm = new FormDoktorGiris();
            frm.Show();
            this.Hide();
        }

        private void BtnSekreter_Click(object sender, EventArgs e)
        {
            FormSekreterGiris frm = new FormSekreterGiris();
            frm.Show();
            this.Hide();
        }

        private void BtnHasta_Click(object sender, EventArgs e)
        {
            FormHastaGiris frm = new FormHastaGiris();
            frm.Show();
            this.Hide();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Uygulamadan çıkmak istediğinize emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FormGirisler_Load(object sender, EventArgs e)
        {
            // 1. Bağlantı sınıfını çağır
            sqlBaglanti bgl = new sqlBaglanti();

            try
            {
                // 2. Bağlantıyı açmaya çalış
                // bgl.baglanti() dediğin anda metodun içindeki .Open() çalışır.
                using (var baglan = bgl.baglanti())
                {
                    // Eğer buraya kadar hata vermediyse bağlantı açılmıştır.
                    // Ekstra Garanti: Veritabanının versiyonunu soralım.
                    using (NpgsqlCommand komut = new NpgsqlCommand("SELECT version()", baglan))
                    {
                        string versiyon = komut.ExecuteScalar().ToString();
                        MessageBox.Show("Bağlantı Başarılı! ✅\n\nPostgreSQL Sürümü: " + versiyon, "Test Sonucu");
                    }
                }
            }
            catch (Exception ex)
            {
                // 3. Hata varsa mesajı göster (Şifre mi yanlış, veritabanı adı mı yanlış anlarız)
                MessageBox.Show("Bağlantı HATASI! ❌\n\nHata Mesajı: " + ex.Message, "Hata");
            }
        }

        private void FormGirisler_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !this.Visible)
            {
                Application.Exit();
            }
            else if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult dialogResult = MessageBox.Show("Uygulamadan çıkmak istediğinize emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}
