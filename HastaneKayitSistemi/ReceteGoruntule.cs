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
    public partial class ReceteGoruntule : Form
    {
        public ReceteGoruntule()
        {
            InitializeComponent();
        }
        sqlBaglanti bgl = new sqlBaglanti();
        public string hasta_ID;
        private void ReceteGoruntule_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = bgl.baglanti();
            DataTable dt = new DataTable();
            try
            {
                string sorgu = @"
                        SELECT 
                            i.ilac_ad AS ""İlaç Adı"",
                            rd.kullanim_sekli AS ""Kullanım Şekli"",
                            rd.adet AS ""Adet"",
                            r.recete_tarih AS ""Reçete Tarihi"",
                            (d.doktor_ad || ' ' || d.doktor_soyad) AS ""Doktor"",
                            r.tani_teshis AS ""Tanı/Teşhis""
                        FROM recetedetay rd
                        JOIN receteler r ON rd.recete_id = r.recete_id
                        JOIN Ilaclar i ON rd.ilac_id = i.ilac_id
                        JOIN Doktorlar d ON r.doktor_id = d.doktor_id
                        WHERE r.hasta_id = @par1
                        ORDER BY r.recete_tarih DESC";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, conn);
                da.SelectCommand.Parameters.AddWithValue("@par1", Guid.Parse(hasta_ID));
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }

            }
            catch (Exception ex) 
            {
                MessageBox.Show("Hata Oluştu: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
