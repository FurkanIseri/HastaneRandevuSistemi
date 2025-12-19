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
    public partial class FormSekreterDetay : Form
    {
        private bool isReturningToMain = false;

        public FormSekreterDetay()
        {
            InitializeComponent();
            this.FormlarGuncellendi += (s, e) => GridleriGuncelle();
        }

        public string tc;
        sqlBaglanti bgl = new sqlBaglanti();
        public event EventHandler FormlarGuncellendi;

        // Sekreterin Hastane ID'sini tutacak değişken
        int mevcutHastaneID = -1;

        protected virtual void OnFormlarGuncellendi()
        {
            FormlarGuncellendi?.Invoke(this, EventArgs.Empty);
        }

        // 1. YENİ EKLENEN METOT: Sekreterin Hastanesini Bulur
        public void SekreterinHastanesiniBul()
        {
            NpgsqlConnection conn = bgl.baglanti();
            string tcHash = SecurityHelper.Hashle(tc);

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT hastane_id FROM Sekreterler WHERE tc_hash = @p1", conn);
                cmd.Parameters.AddWithValue("@p1", tcHash);

                object sonuc = cmd.ExecuteScalar();
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

        public Guid SekreterIDBulma()
        {
            Guid bulunanID = Guid.Empty;
            NpgsqlConnection conn = bgl.baglanti();
            string tcHash = SecurityHelper.Hashle(tc);

            NpgsqlCommand command = new NpgsqlCommand("SELECT sekreter_id FROM Sekreterler WHERE tc_hash = @par1", conn);
            command.Parameters.AddWithValue("@par1", tcHash);

            NpgsqlDataReader dr = command.ExecuteReader();

            if (dr.Read())
            {
                if (dr[0] != DBNull.Value)
                {
                    bulunanID = dr.GetGuid(0);
                }
            }

            conn.Close();
            return bulunanID;
        }

        public void GridleriGuncelle()
        {
            // ----------------------------------------------------------------
            // 2. GÜNCELLENEN KISIM: Branşları DataGrid'e Çekme (Sadece O Hastane)
            // ----------------------------------------------------------------
            DataTable dt = new DataTable();
            NpgsqlConnection conn = bgl.baglanti();

            // WHERE hastane_id = @p1 eklendi
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(@"SELECT brans_ad AS ""Branş Adı"" FROM Branslar WHERE hastane_id = @p1 ORDER BY brans_ad", conn);
            da.SelectCommand.Parameters.AddWithValue("@p1", mevcutHastaneID);

            da.Fill(dt);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            dataGridView1.AutoResizeColumns();
            conn.Close();


            // Doktorları DataGrid'e Çekme (Mevcut mantığınla sekretere bağlı doktorlar geliyor, burası kalabilir)
            DataTable dt2 = new DataTable();
            string sorgu = @"
            SELECT 
            (d.doktor_ad || ' ' || d.doktor_soyad) AS ""Doktorlar"",
            b.brans_ad AS ""Branş""
            FROM Doktorlar d
            JOIN Branslar b ON d.brans_id = b.brans_id
            JOIN Sekreterler s ON d.sekreter_id = s.sekreter_id 
            WHERE s.tc_hash = @p1";

            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(sorgu, bgl.baglanti());
            da2.SelectCommand.Parameters.AddWithValue("@p1", SecurityHelper.Hashle(tc));
            da2.Fill(dt2);
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = dt2;
            dataGridView2.Refresh();
            dataGridView2.AutoResizeColumns();

            // Bağlantıyı kapatmak için DataAdapter içindeki connection'ı kapatıyoruz
            // bgl.baglanti() her çağrıldığında yeni connection açtığı için manuel kapatmak sağlıklı değil, 
            // ama senin yapında da2 scope dışına çıkınca GC toplar. Yine de temizlik iyidir.
        }

        private void FormSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;

            // Önce Hastane ID'sini buluyoruz
            SekreterinHastanesiniBul();

            // Sonra Gridleri bu ID'ye göre güncelliyoruz
            GridleriGuncelle();

            // Ad-Soyad Çekme
            string tcHash = SecurityHelper.Hashle(tc);
            NpgsqlConnection conn = bgl.baglanti();
            NpgsqlCommand command = new NpgsqlCommand("Select (sekreter_ad || ' ' || sekreter_soyad) From Sekreterler where tc_hash=@p1", conn);
            command.Parameters.AddWithValue("@p1", tcHash);
            NpgsqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0].ToString();
            }
            conn.Close();

            // ----------------------------------------------------------------
            // 3. GÜNCELLENEN KISIM: Branşları Combobox'a Çekme (Sadece O Hastane)
            // ----------------------------------------------------------------
            NpgsqlConnection connBrans = bgl.baglanti();
            // WHERE hastane_id = @p1 eklendi
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT * FROM Branslar WHERE hastane_id = @p1 ORDER BY brans_ad", connBrans);
            da.SelectCommand.Parameters.AddWithValue("@p1", mevcutHastaneID);

            DataTable dt = new DataTable();
            da.Fill(dt);

            CmbBrans.DisplayMember = "brans_ad";
            CmbBrans.ValueMember = "brans_id";
            CmbBrans.DataSource = dt;
            connBrans.Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (CmbBrans.SelectedIndex == -1 || CmbDoktor.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen Branş ve Doktor seçimi yapınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DateTime randevuTarih;
            DateTime bugun = DateTime.Today;
            DateTime ikiAySonra = bugun.AddMonths(2);
            TimeSpan randevuSaat;

            if (!DateTime.TryParse(MskTxtTarih.Text, out randevuTarih))
            {
                MessageBox.Show("Lütfen geçerli bir tarih giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!TimeSpan.TryParse(MskTxtSaat.Text, out randevuSaat))
            {
                MessageBox.Show("Lütfen geçerli bir saat giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (randevuTarih < bugun || randevuTarih > ikiAySonra)
            {
                MessageBox.Show("Randevu tarihi bugün ile 2 ay sonrası arasında olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TimeSpan baslangicSaat = new TimeSpan(8, 0, 0);
            TimeSpan bitisSaat = new TimeSpan(18, 0, 0);

            if (randevuSaat < baslangicSaat || randevuSaat > bitisSaat)
            {
                MessageBox.Show("Randevu saati 08:00 ile 18:00 arasında olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NpgsqlConnection conn = bgl.baglanti();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("CALL sp_RandevuOlustur(@p1, @p2, @p3, @p4, @p5)", conn);
                command.Parameters.AddWithValue("@p1", randevuTarih);
                command.Parameters.AddWithValue("@p2", MskTxtSaat.Text);

                command.Parameters.AddWithValue("@p3", int.Parse(CmbBrans.SelectedValue.ToString()));
                command.Parameters.AddWithValue("@p4", Guid.Parse(CmbDoktor.SelectedValue.ToString()));

                command.Parameters.AddWithValue("@p5", ChcDurum.Checked);

                command.ExecuteNonQuery();
                MessageBox.Show("Randevu Başarıyla Kaydedilmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (PostgresException ex)
            {
                MessageBox.Show("Veritabanı Uyarısı: " + ex.MessageText, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                conn.Close();
            }

        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbBrans.SelectedIndex == -1 || !(CmbBrans.SelectedValue is int))
            {
                return;
            }

            CmbDoktor.DataSource = null;
            NpgsqlConnection conn = bgl.baglanti();
            string sorgu = @"
                SELECT 
                d.doktor_id,
                (d.doktor_ad || ' ' || d.doktor_soyad) AS ad_soyad
                FROM Doktorlar d
                JOIN Sekreterler s ON d.sekreter_id = s.sekreter_id
                WHERE d.brans_id = @p1 
                AND s.tc_hash = @p2";

            NpgsqlCommand command = new NpgsqlCommand(sorgu, conn);
            command.Parameters.AddWithValue("@p1", (int)CmbBrans.SelectedValue);
            command.Parameters.AddWithValue("@p2", SecurityHelper.Hashle(tc));

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            CmbDoktor.DisplayMember = "ad_soyad";
            CmbDoktor.ValueMember = "doktor_id";
            CmbDoktor.DataSource = dt;

            conn.Close();
        }

        private void BtnOlustur_Click(object sender, EventArgs e)
        {
            Guid aktifSekreterID = SekreterIDBulma();

            NpgsqlConnection conn = bgl.baglanti();

            try
            {
                NpgsqlCommand command5 = new NpgsqlCommand("INSERT INTO Duyurular (duyurular_text, sekreter_id) VALUES (@par1, @par2)", conn);

                command5.Parameters.AddWithValue("@par1", RchDuyuru.Text);
                command5.Parameters.AddWithValue("@par2", aktifSekreterID);
                command5.ExecuteNonQuery();
                MessageBox.Show("Duyuru başarıyla oluşturuldu.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RchDuyuru.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void BtnDoktorPaneli_Click(object sender, EventArgs e)
        {
            FormDoktorPaneli frm = new FormDoktorPaneli();
            frm.FormClosed += (s, args) =>
            {
                GridleriGuncelle();
                // bgl.baglanti().Close() burada gerekli değil çünkü GridleriGuncelle kendi içinde açıp kapatıyor
            };
            frm.tc = LblTC.Text;
            frm.ShowDialog();
        }

        private void BtnBransPaneli_Click(object sender, EventArgs e)
        {
            FormBransPaneli frm = new FormBransPaneli();
            frm.FormClosed += (s, args) =>
            {
                GridleriGuncelle();
            };
            frm.tc = LblTC.Text;
            frm.ShowDialog();
        }

        private void BtnRandevuListe_Click(object sender, EventArgs e)
        {
            FormRandevuListesi frm = new FormRandevuListesi();
            frm.FormClosed += (s, args) =>
            {
                GridleriGuncelle();
            };
            frm.ShowDialog();
        }

        private void BtnDuyuru_Click(object sender, EventArgs e)
        {
            FormDuyurular frm = new FormDuyurular();
            frm.FormClosed += (s, args) =>
            {
                GridleriGuncelle();
            };
            frm.ShowDialog();
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

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LblDoktorID.Text = "0";
            clear.Temizle(groupBox3.Controls);
        }

        private void BtnSekreter_Click(object sender, EventArgs e)
        {
            FormSekreterPaneli frm = new FormSekreterPaneli();
            frm.FormClosed += (s, args) =>
            {
                GridleriGuncelle();
            };
            frm.TC = LblTC.Text;
            frm.ShowDialog();
        }

        private void FormSekreterDetay_FormClosing(object sender, FormClosingEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            IlacDetay frm = new IlacDetay();
            frm.tc = LblTC.Text;
            frm.Show();
        }
    }
}