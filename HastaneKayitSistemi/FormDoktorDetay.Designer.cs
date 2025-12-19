namespace HastaneKayitSistemi
{
    partial class FormDoktorDetay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDoktorDetay));
            groupBox1 = new GroupBox();
            LblAdSoyad = new Label();
            LblTC = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            RchSikayet = new RichTextBox();
            groupBox3 = new GroupBox();
            dataGridView1 = new DataGridView();
            groupBox4 = new GroupBox();
            BtnDuyurular = new Button();
            BtnBilgiDuzenle = new Button();
            BtnHomePage = new Button();
            BtnCikis = new Button();
            BtnReceteOlustur = new Button();
            groupBox5 = new GroupBox();
            label8 = new Label();
            label7 = new Label();
            TxtKullanimSekli = new TextBox();
            label6 = new Label();
            button1 = new Button();
            TxtAdet = new TextBox();
            label5 = new Label();
            CmbIlac = new ComboBox();
            label4 = new Label();
            RchTxtTaniTeshis = new RichTextBox();
            label3 = new Label();
            label9 = new Label();
            label10 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(LblAdSoyad);
            groupBox1.Controls.Add(LblTC);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(23, 19);
            groupBox1.Margin = new Padding(5, 4, 5, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(5, 4, 5, 4);
            groupBox1.Size = new Size(368, 159);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Doktor Bilgi";
            // 
            // LblAdSoyad
            // 
            LblAdSoyad.AutoSize = true;
            LblAdSoyad.ForeColor = Color.Cornsilk;
            LblAdSoyad.Location = new Point(204, 84);
            LblAdSoyad.Margin = new Padding(5, 0, 5, 0);
            LblAdSoyad.Name = "LblAdSoyad";
            LblAdSoyad.Size = new Size(98, 25);
            LblAdSoyad.TabIndex = 7;
            LblAdSoyad.Text = "Null Null";
            // 
            // LblTC
            // 
            LblTC.AutoSize = true;
            LblTC.ForeColor = Color.Cornsilk;
            LblTC.Location = new Point(204, 46);
            LblTC.Margin = new Padding(5, 0, 5, 0);
            LblTC.Name = "LblTC";
            LblTC.Size = new Size(144, 25);
            LblTC.TabIndex = 6;
            LblTC.Text = "00000000000";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(44, 84);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(128, 25);
            label2.TabIndex = 5;
            label2.Text = "Ad Soyad : ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(67, 46);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(105, 25);
            label1.TabIndex = 4;
            label1.Text = "TC NO : ";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(RchSikayet);
            groupBox2.Location = new Point(23, 202);
            groupBox2.Margin = new Padding(5, 4, 5, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(5, 4, 5, 4);
            groupBox2.Size = new Size(368, 283);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Randevu Detay";
            // 
            // RchSikayet
            // 
            RchSikayet.Location = new Point(10, 32);
            RchSikayet.Margin = new Padding(5, 4, 5, 4);
            RchSikayet.Name = "RchSikayet";
            RchSikayet.Size = new Size(338, 234);
            RchSikayet.TabIndex = 1;
            RchSikayet.Text = "";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dataGridView1);
            groupBox3.Location = new Point(416, 19);
            groupBox3.Margin = new Padding(5, 4, 5, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(5, 4, 5, 4);
            groupBox3.Size = new Size(837, 340);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Randevu Listesi";
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(5, 31);
            dataGridView1.Margin = new Padding(5, 4, 5, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(827, 305);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(BtnDuyurular);
            groupBox4.Controls.Add(BtnBilgiDuzenle);
            groupBox4.Location = new Point(23, 493);
            groupBox4.Margin = new Padding(5, 4, 5, 4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(5, 4, 5, 4);
            groupBox4.Size = new Size(368, 195);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Hızlı Erişim";
            // 
            // BtnDuyurular
            // 
            BtnDuyurular.BackColor = Color.PaleTurquoise;
            BtnDuyurular.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            BtnDuyurular.Location = new Point(182, 26);
            BtnDuyurular.Margin = new Padding(5, 4, 5, 4);
            BtnDuyurular.Name = "BtnDuyurular";
            BtnDuyurular.Size = new Size(176, 158);
            BtnDuyurular.TabIndex = 1;
            BtnDuyurular.Text = "Duyurular";
            BtnDuyurular.UseVisualStyleBackColor = false;
            BtnDuyurular.Click += BtnDuyurular_Click;
            // 
            // BtnBilgiDuzenle
            // 
            BtnBilgiDuzenle.BackColor = Color.PaleTurquoise;
            BtnBilgiDuzenle.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            BtnBilgiDuzenle.Location = new Point(21, 26);
            BtnBilgiDuzenle.Margin = new Padding(5, 4, 5, 4);
            BtnBilgiDuzenle.Name = "BtnBilgiDuzenle";
            BtnBilgiDuzenle.Size = new Size(151, 158);
            BtnBilgiDuzenle.TabIndex = 0;
            BtnBilgiDuzenle.Text = "Bilgi Düzenle";
            BtnBilgiDuzenle.UseVisualStyleBackColor = false;
            BtnBilgiDuzenle.Click += BtnBilgiDuzenle_Click;
            // 
            // BtnHomePage
            // 
            BtnHomePage.BackgroundImage = (Image)resources.GetObject("BtnHomePage.BackgroundImage");
            BtnHomePage.BackgroundImageLayout = ImageLayout.Stretch;
            BtnHomePage.FlatAppearance.BorderSize = 0;
            BtnHomePage.FlatStyle = FlatStyle.Flat;
            BtnHomePage.ForeColor = Color.Cornsilk;
            BtnHomePage.Location = new Point(1261, 26);
            BtnHomePage.Name = "BtnHomePage";
            BtnHomePage.Size = new Size(126, 102);
            BtnHomePage.TabIndex = 25;
            BtnHomePage.UseVisualStyleBackColor = true;
            BtnHomePage.Click += BtnHomePage_Click;
            // 
            // BtnCikis
            // 
            BtnCikis.BackColor = Color.SteelBlue;
            BtnCikis.BackgroundImage = (Image)resources.GetObject("BtnCikis.BackgroundImage");
            BtnCikis.BackgroundImageLayout = ImageLayout.Stretch;
            BtnCikis.FlatAppearance.BorderSize = 0;
            BtnCikis.FlatStyle = FlatStyle.Flat;
            BtnCikis.Location = new Point(1261, 582);
            BtnCikis.Name = "BtnCikis";
            BtnCikis.Size = new Size(129, 89);
            BtnCikis.TabIndex = 26;
            BtnCikis.UseVisualStyleBackColor = false;
            BtnCikis.Click += BtnCikis_Click;
            // 
            // BtnReceteOlustur
            // 
            BtnReceteOlustur.BackColor = Color.PaleTurquoise;
            BtnReceteOlustur.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            BtnReceteOlustur.Location = new Point(9, 226);
            BtnReceteOlustur.Name = "BtnReceteOlustur";
            BtnReceteOlustur.Size = new Size(823, 65);
            BtnReceteOlustur.TabIndex = 2;
            BtnReceteOlustur.Text = "Reçete Oluştur";
            BtnReceteOlustur.UseVisualStyleBackColor = false;
            BtnReceteOlustur.Click += BtnReceteOlustur_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(BtnReceteOlustur);
            groupBox5.Controls.Add(label8);
            groupBox5.Controls.Add(label7);
            groupBox5.Controls.Add(TxtKullanimSekli);
            groupBox5.Controls.Add(label6);
            groupBox5.Controls.Add(button1);
            groupBox5.Controls.Add(TxtAdet);
            groupBox5.Controls.Add(label5);
            groupBox5.Controls.Add(CmbIlac);
            groupBox5.Controls.Add(label4);
            groupBox5.Controls.Add(RchTxtTaniTeshis);
            groupBox5.Controls.Add(label3);
            groupBox5.Location = new Point(416, 366);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(839, 322);
            groupBox5.TabIndex = 27;
            groupBox5.TabStop = false;
            groupBox5.Text = "Reçete Oluşturma";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(175, 30);
            label8.Name = "label8";
            label8.Size = new Size(24, 25);
            label8.TabIndex = 10;
            label8.Text = "0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(25, 30);
            label7.Name = "label7";
            label7.Size = new Size(154, 25);
            label7.TabIndex = 9;
            label7.Text = "Randevu ID : ";
            // 
            // TxtKullanimSekli
            // 
            TxtKullanimSekli.Location = new Point(175, 170);
            TxtKullanimSekli.Name = "TxtKullanimSekli";
            TxtKullanimSekli.Size = new Size(575, 34);
            TxtKullanimSekli.TabIndex = 8;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(4, 173);
            label6.Name = "label6";
            label6.Size = new Size(175, 25);
            label6.TabIndex = 7;
            label6.Text = "Kullanım Şekli : ";
            // 
            // button1
            // 
            button1.BackgroundImage = Properties.Resources.add;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = SystemColors.ControlText;
            button1.Location = new Point(524, 127);
            button1.Name = "button1";
            button1.Size = new Size(34, 31);
            button1.TabIndex = 6;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // TxtAdet
            // 
            TxtAdet.Location = new Point(474, 127);
            TxtAdet.Name = "TxtAdet";
            TxtAdet.Size = new Size(33, 34);
            TxtAdet.TabIndex = 5;
            TxtAdet.Text = "0";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(401, 133);
            label5.Name = "label5";
            label5.Size = new Size(80, 25);
            label5.TabIndex = 4;
            label5.Text = "Adet : ";
            // 
            // CmbIlac
            // 
            CmbIlac.FormattingEnabled = true;
            CmbIlac.Location = new Point(175, 128);
            CmbIlac.Name = "CmbIlac";
            CmbIlac.Size = new Size(220, 33);
            CmbIlac.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(111, 133);
            label4.Name = "label4";
            label4.Size = new Size(68, 25);
            label4.TabIndex = 2;
            label4.Text = "İlaç : ";
            // 
            // RchTxtTaniTeshis
            // 
            RchTxtTaniTeshis.Location = new Point(175, 67);
            RchTxtTaniTeshis.Name = "RchTxtTaniTeshis";
            RchTxtTaniTeshis.Size = new Size(649, 55);
            RchTxtTaniTeshis.TabIndex = 1;
            RchTxtTaniTeshis.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 72);
            label3.Name = "label3";
            label3.Size = new Size(170, 25);
            label3.TabIndex = 0;
            label3.Text = "Tanı ve Teşhis : ";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.Cornsilk;
            label9.Location = new Point(204, 121);
            label9.Margin = new Padding(5, 0, 5, 0);
            label9.Name = "label9";
            label9.Size = new Size(98, 25);
            label9.TabIndex = 9;
            label9.Text = "Null Null";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(54, 121);
            label10.Margin = new Padding(5, 0, 5, 0);
            label10.Name = "label10";
            label10.Size = new Size(118, 25);
            label10.TabIndex = 8;
            label10.Text = "Sekreter : ";
            // 
            // FormDoktorDetay
            // 
            AutoScaleDimensions = new SizeF(14F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(1395, 690);
            Controls.Add(groupBox5);
            Controls.Add(BtnCikis);
            Controls.Add(BtnHomePage);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            MaximizeBox = false;
            Name = "FormDoktorDetay";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Doktor Detay";
            FormClosing += FormDoktorDetay_FormClosing;
            Load += FormDoktorDetay_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label LblAdSoyad;
        private Label LblTC;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private RichTextBox RchSikayet;
        private GroupBox groupBox3;
        private DataGridView dataGridView1;
        private GroupBox groupBox4;
        private Button BtnDuyurular;
        private Button BtnBilgiDuzenle;
        private Button BtnHomePage;
        private Button BtnCikis;
        private Button BtnReceteOlustur;
        private GroupBox groupBox5;
        private RichTextBox RchTxtTaniTeshis;
        private Label label3;
        private Button button1;
        private TextBox TxtAdet;
        private Label label5;
        private ComboBox CmbIlac;
        private Label label4;
        private TextBox TxtKullanimSekli;
        private Label label6;
        private Label label8;
        private Label label7;
        private Label label9;
        private Label label10;
    }
}