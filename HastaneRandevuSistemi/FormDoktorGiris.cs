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
    public partial class FormDoktorGiris : Form
    {
        private bool isReturningToMain = false;

        public FormDoktorGiris()
        {
            InitializeComponent();
        }
        sqlBaglanti bgl = new sqlBaglanti();
        private void BtnLogIn_Click(object sender, EventArgs e)
        {
            // Giriş işlemi için tc ve sifre bilgileri alınıyor.Veritabanına hashleme yöntemiyle atıldığı için hashliyoruz.
            string tcHash = SecurityHelper.Hashle(MskTxtTC.Text);
            string sifreHash = SecurityHelper.Hashle(TxtSifre.Text);
            NpgsqlConnection conn = bgl.baglanti();
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Doktorlar WHERE tc_hash=@par1 AND sifre_hash=@par2", conn);
            command.Parameters.AddWithValue("@par1", tcHash);
            command.Parameters.AddWithValue("@par2", sifreHash);
            NpgsqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                FormDoktorDetay frm = new FormDoktorDetay();
                frm.tc = MskTxtTC.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC ve Şifre Girişi");
            }

            conn.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            TxtSifre.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void BtnHomePage_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Ana menüye dönmek istediğinize emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                isReturningToMain = true;
                FormGirisler frm = new FormGirisler();
                frm.Show();
                this.Close();
            }
        }

        private void SifremiUnuttum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Şifreyi öğrenmek için tc verisi lazım.
            string tcHash = SecurityHelper.Hashle(MskTxtTC.Text);
            NpgsqlConnection conn = bgl.baglanti();
            NpgsqlCommand command2 = new NpgsqlCommand("SELECT sifre_sifreli FROM Doktorlar WHERE tc_hash = @par1", conn);
            command2.Parameters.AddWithValue("@par1", tcHash);
            // Veriyi çekiyoruz.
            object sonuc = command2.ExecuteScalar();
            if (sonuc != null)
            {
                string sifrelenmisVeri = sonuc.ToString();
                string orjinalSifre = SecurityHelper.Coz(sifrelenmisVeri);
                MessageBox.Show("Şifreniz : " + orjinalSifre, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtSifre.Text = orjinalSifre;
            }
            else
            {
                MessageBox.Show("Böyle bir doktor kaydı bulunmamaktadır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            conn.Close();
        }

        private void FormDoktorGiris_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isReturningToMain)
            {
                return;
            }

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
