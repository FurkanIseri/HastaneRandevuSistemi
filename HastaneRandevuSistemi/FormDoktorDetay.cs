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
    public partial class FormDoktorDetay : Form
    {
        private bool isReturningToMain = false;

        public FormDoktorDetay()
        {
            InitializeComponent();
        }

        // Doktor giriş ekranında yer alan TC'yi almak için oluşturulan değişken.
        public string tc;
        sqlBaglanti bgl = new sqlBaglanti();

        // Tıklanan Randevunun ID'sini burada tutacağız.
        int secilenRandevuID = 0;

        // Doktorun çalıştığı hastane ID'sini burada tutacağız
        public int doktorHastaneID = -1; 

        private void FormDoktorDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;

            // Doktor Ad-Soyad ve HASTANE ID Çekme
            string tcHash = SecurityHelper.Hashle(LblTC.Text);
            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("SELECT doktor_ad, doktor_soyad, hastane_id FROM Doktorlar WHERE tc_hash=@par1", conn);
                command.Parameters.AddWithValue("@par1", tcHash);
                
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    LblAdSoyad.Text = dr[0] + " " + dr[1];
                    
                    if (dr[2] != DBNull.Value)
                    {
                        doktorHastaneID = int.Parse(dr[2].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Doktor bilgisi alınamadı: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            // Randevuları Listeleme ve İlaçları Getirme
            GridGuncelle();
            IlaclariGetir();

            NpgsqlConnection conn2 = bgl.baglanti();
            try
            {
                NpgsqlCommand command2 = new NpgsqlCommand(@"SELECT s.sekreter_ad, s.sekreter_soyad 
                                                           FROM Doktorlar d 
                                                           JOIN Sekreterler s ON s.sekreter_id = d.sekreter_id 
                                                           WHERE d.tc_hash = @par1", conn2);
                command2.Parameters.AddWithValue("@par1", tcHash);
                NpgsqlDataReader dr2 = command2.ExecuteReader();
                while (dr2.Read())
                {
                    label9.Text = dr2[0] + " " + dr2[1];
                }
            }
            finally
            {
                conn2.Close();
            }
        }

        void GridGuncelle()
        {
            string tcHash = SecurityHelper.Hashle(tc);
            DataTable dt = new DataTable();
            NpgsqlConnection conn = bgl.baglanti();

            try
            {
                string sorgu = @"SELECT 
                                    r.randevu_id, 
                                    r.randevu_tarih AS ""Tarih"",
                                    r.randevu_saat AS ""Saat"",
                                    (h.hasta_ad || ' ' || h.hasta_soyad) AS ""Hasta Adı Soyadı"",
                                    h.telefon_no AS ""Hasta Telefon"",
                                    r.hasta_sikayet AS ""Şikayet""
                                FROM Randevular r
                                JOIN Hastalar h ON r.hasta_id = h.hasta_id
                                JOIN Doktorlar d ON r.doktor_id = d.doktor_id
                                WHERE d.tc_hash = @p1 AND r.randevu_durum = TRUE
                                ORDER BY r.randevu_tarih, r.randevu_saat";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
                da.SelectCommand.Parameters.AddWithValue("@p1", tcHash);
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns[0].Visible = false;
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Randevular yüklenirken hata: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void IlaclariGetir()
        {
            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT ilac_id, ilac_ad FROM Ilaclar WHERE stok_adet > 0 ORDER BY ilac_ad", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                CmbIlac.DisplayMember = "ilac_ad";
                CmbIlac.ValueMember = "ilac_id";
                CmbIlac.DataSource = dt;
            }
            finally
            {
                conn.Close();
            }
        }

        // Randevu ID'sini alma
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (row.Cells[0].Value != null)
                {
                    secilenRandevuID = int.Parse(row.Cells[0].Value.ToString());
                    label8.Text = secilenRandevuID.ToString();
                }

                // Şikayeti kutuya aktar
                if (row.Cells["Şikayet"].Value != null)
                {
                    RchSikayet.Text = row.Cells["Şikayet"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seçim hatası: " + ex.Message);
            }
        }

        // Reçete Oluşturma
        private void BtnReceteOlustur_Click(object sender, EventArgs e)
        {
            // Kontroller
            if (secilenRandevuID == 0)
            {
                MessageBox.Show("Lütfen tablodan bir randevu seçiniz!");
                return;
            }
            if (CmbIlac.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir ilaç seçiniz.");
                return;
            }
            if (string.IsNullOrEmpty(TxtAdet.Text) || TxtAdet.Text == "0")
            {
                MessageBox.Show("Lütfen geçerli bir adet giriniz.");
                return;
            }

            NpgsqlConnection conn = bgl.baglanti();
            NpgsqlTransaction tran = null;

            try
            {
                // Bağlantı kontrolü
                if (conn.State != ConnectionState.Open) conn.Open();

                tran = conn.BeginTransaction();

                // PROSEDÜRÜ ÇAĞIRMA 
                NpgsqlCommand commandRecete = new NpgsqlCommand("CALL sp_receteolustur(@pTani, @pRandevuID, @pReceteID)", conn);

                // Giriş Parametreleri
                commandRecete.Parameters.AddWithValue("@pTani", RchTxtTaniTeshis.Text);
                commandRecete.Parameters.AddWithValue("@pRandevuID", secilenRandevuID);

                // Npgsql'de Output parametresi tanımlama:
                NpgsqlParameter outParam = new NpgsqlParameter("@pReceteID", NpgsqlTypes.NpgsqlDbType.Integer);
                outParam.Direction = ParameterDirection.InputOutput; 
                outParam.Value = 0; 
                commandRecete.Parameters.Add(outParam);

                commandRecete.ExecuteNonQuery(); // Prosedürü çalıştır

                // Geri dönen ID'yi yakalıyoruz
                int yeniReceteID = (int)commandRecete.Parameters["@pReceteID"].Value;

                // İlaç Detay Ekleme 
                int ilacID = int.Parse(CmbIlac.SelectedValue.ToString());
                int adet = int.Parse(TxtAdet.Text);
                string kullanim = TxtKullanimSekli.Text;

                string sqlDetay = @"INSERT INTO recetedetay (recete_id, ilac_id, kullanim_sekli, adet) 
                        VALUES (@pRecID, @pIlacID, @pKullanim, @pAdet)";

                NpgsqlCommand cmdDetay = new NpgsqlCommand(sqlDetay, conn);
                cmdDetay.Parameters.AddWithValue("@pRecID", yeniReceteID);
                cmdDetay.Parameters.AddWithValue("@pIlacID", ilacID);
                cmdDetay.Parameters.AddWithValue("@pKullanim", kullanim);
                cmdDetay.Parameters.AddWithValue("@pAdet", adet);
                cmdDetay.ExecuteNonQuery();

                // Stoktan Düşme 
                string sqlStok = "UPDATE Ilaclar SET stok_adet = stok_adet - @pStokAdet WHERE ilac_id = @pStokIlacID";
                NpgsqlCommand cmdStok = new NpgsqlCommand(sqlStok, conn);
                cmdStok.Parameters.AddWithValue("@pStokAdet", adet);
                cmdStok.Parameters.AddWithValue("@pStokIlacID", ilacID);
                cmdStok.ExecuteNonQuery();

                tran.Commit();
                MessageBox.Show("Reçete başarıyla oluşturuldu.");
                label8.Text = "0";
                RchTxtTaniTeshis.Text = "";
                CmbIlac.Text = "";TxtAdet.Text = "0";
                TxtKullanimSekli.Text = "";
            }
            catch (Exception ex)
            {
                if (tran != null) tran.Rollback();
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        private void BtnBilgiDuzenle_Click(object sender, EventArgs e)
        {
            FormDoktorBilgiDuzenle frm = new FormDoktorBilgiDuzenle();
            frm.tc = LblTC.Text;
            frm.Show();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FormDuyurular frm = new FormDuyurular();
            frm.hastaneID = doktorHastaneID; 
            frm.Show();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Uygulamadan çıkmak istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void BtnHomePage_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Ana menüye dönmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                isReturningToMain = true;
                FormGirisler frm = new FormGirisler();
                frm.Show();
                this.Close();
            }
        }

        private void FormDoktorDetay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isReturningToMain) return;

            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult dialogResult = MessageBox.Show("Uygulamadan çıkmak istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
        // Adet arttırma.
        private void button1_Click(object sender, EventArgs e)
        {
            int sayi;
            bool sonuc = int.TryParse(TxtAdet.Text, out sayi);
            if (sonuc)
            {
                TxtAdet.Text = (sayi + 1).ToString();
            }
            else
            {
                TxtAdet.Text = "1";
            }
        }
    }
}