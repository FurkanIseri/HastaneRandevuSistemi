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
    public partial class FormDoktorBilgiDuzenle : Form
    {
        public FormDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        sqlBaglanti bgl = new sqlBaglanti();
        public string tc;
        private void FormDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTxtTC.Text = tc; // Girişten gelen TC
            MskTxtTC.Enabled = false;

            // Bağlantıyı açıyoruz
            NpgsqlConnection conn = bgl.baglanti();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT brans_id, brans_ad FROM Branslar ORDER BY brans_ad", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            CmbBrans.DisplayMember = "brans_ad"; // Görünen İsim
            CmbBrans.ValueMember = "brans_id";   // Arkadaki ID
            CmbBrans.DataSource = dt;

            // DOKTORUN KENDİ BİLGİLERİNİ ÇEK VE YERLEŞTİR
            string tcHash = SecurityHelper.Hashle(MskTxtTC.Text); 

            NpgsqlCommand command = new NpgsqlCommand("SELECT doktor_ad, doktor_soyad, brans_id, sifre_sifreli, cinsiyet FROM Doktorlar WHERE tc_hash=@p1", conn);
            command.Parameters.AddWithValue("@p1", tcHash);

            NpgsqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr["doktor_ad"].ToString();
                TxtSoyad.Text = dr["doktor_soyad"].ToString();

                CmbBrans.SelectedValue = int.Parse(dr["brans_id"].ToString());

                string sifreliSifre = dr["sifre_sifreli"].ToString();
                TxtSifre.Text = SecurityHelper.Coz(sifreliSifre);

                // Cinsiyet (Varsa)
                CmbCinsiyet.Text = dr["cinsiyet"].ToString();
            }

            // Bağlantıyı manuel kapatıyoruz
            conn.Close();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            // 1. GÜVENLİK ADIMI: Verileri hazırlıyoruz
            string tcHash = SecurityHelper.Hashle(MskTxtTC.Text); // Formdaki TC kutusu
            string yeniSifreHash = SecurityHelper.Hashle(TxtSifre.Text);
            string yeniSifreSifreli = SecurityHelper.Sifrele(TxtSifre.Text);

            // GÜNCELLEME SORGUSU
            NpgsqlCommand command = new NpgsqlCommand("UPDATE Doktorlar SET " +
                "doktor_ad = @p1, " +
                "doktor_soyad = @p2, " +
                "brans_id = @p3, " +   
                "sifre_hash = @p4, " +
                "sifre_sifreli = @p5, " +
                "cinsiyet = @p6::cinsiyet_tipi " + // Enum dönüşümü
                "WHERE tc_hash = @p7", bgl.baglanti());

            // PARAMETRELERİ EŞLEŞTİRME
            command.Parameters.AddWithValue("@p1", TxtAd.Text);
            command.Parameters.AddWithValue("@p2", TxtSoyad.Text);

            command.Parameters.AddWithValue("@p3", int.Parse(CmbBrans.SelectedValue.ToString()));

            command.Parameters.AddWithValue("@p4", yeniSifreHash);
            command.Parameters.AddWithValue("@p5", yeniSifreSifreli);
            command.Parameters.AddWithValue("@p6", CmbCinsiyet.Text);

            command.Parameters.AddWithValue("@p7", tcHash);

            // 4. ÇALIŞTIRMA
            command.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Doktor bilgileri başarıyla güncellendi.\nYeni Şifreniz: " + TxtSifre.Text, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

    }
}
