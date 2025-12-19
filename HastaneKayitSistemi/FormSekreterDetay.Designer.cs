namespace HastaneKayitSistemi
{
    partial class FormSekreterDetay
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
            DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle14 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle15 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle16 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle17 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle18 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSekreterDetay));
            groupBox1 = new GroupBox();
            LblAdSoyad = new Label();
            label3 = new Label();
            LblTC = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            BtnOlustur = new Button();
            RchDuyuru = new RichTextBox();
            groupBox3 = new GroupBox();
            linkLabel1 = new LinkLabel();
            LblDoktorID = new Label();
            CmbDoktor = new ComboBox();
            BtnKaydet = new Button();
            ChcDurum = new CheckBox();
            CmbBrans = new ComboBox();
            MskTxtSaat = new MaskedTextBox();
            MskTxtTarih = new MaskedTextBox();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label8 = new Label();
            label5 = new Label();
            groupBox4 = new GroupBox();
            dataGridView2 = new DataGridView();
            groupBox5 = new GroupBox();
            dataGridView1 = new DataGridView();
            groupBox6 = new GroupBox();
            BtnSekreter = new Button();
            BtnDuyuru = new Button();
            BtnRandevuListe = new Button();
            BtnBransPaneli = new Button();
            BtnDoktorPaneli = new Button();
            BtnHomePage = new Button();
            BtnCikis = new Button();
            button1 = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox6.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(LblAdSoyad);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(LblTC);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.Location = new Point(26, 17);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4);
            groupBox1.Size = new Size(375, 144);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Sekreter Bilgi";
            // 
            // LblAdSoyad
            // 
            LblAdSoyad.AutoSize = true;
            LblAdSoyad.ForeColor = Color.Cornsilk;
            LblAdSoyad.Location = new Point(175, 86);
            LblAdSoyad.Margin = new Padding(4, 0, 4, 0);
            LblAdSoyad.Name = "LblAdSoyad";
            LblAdSoyad.Size = new Size(98, 25);
            LblAdSoyad.TabIndex = 3;
            LblAdSoyad.Text = "Null Null";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 86);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(128, 25);
            label3.TabIndex = 2;
            label3.Text = "Ad Soyad : ";
            // 
            // LblTC
            // 
            LblTC.AutoSize = true;
            LblTC.ForeColor = Color.Cornsilk;
            LblTC.Location = new Point(174, 45);
            LblTC.Margin = new Padding(4, 0, 4, 0);
            LblTC.Name = "LblTC";
            LblTC.Size = new Size(144, 25);
            LblTC.TabIndex = 1;
            LblTC.Text = "00000000000";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 45);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(93, 25);
            label1.TabIndex = 0;
            label1.Text = "TC No :";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(BtnOlustur);
            groupBox2.Controls.Add(RchDuyuru);
            groupBox2.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox2.Location = new Point(26, 168);
            groupBox2.Margin = new Padding(4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4);
            groupBox2.Size = new Size(375, 235);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Duyuru Oluştur";
            // 
            // BtnOlustur
            // 
            BtnOlustur.BackColor = Color.PaleTurquoise;
            BtnOlustur.Location = new Point(9, 184);
            BtnOlustur.Margin = new Padding(4);
            BtnOlustur.Name = "BtnOlustur";
            BtnOlustur.Size = new Size(358, 33);
            BtnOlustur.TabIndex = 1;
            BtnOlustur.Text = "Oluştur";
            BtnOlustur.UseVisualStyleBackColor = false;
            BtnOlustur.Click += BtnOlustur_Click;
            // 
            // RchDuyuru
            // 
            RchDuyuru.Location = new Point(9, 29);
            RchDuyuru.Margin = new Padding(4);
            RchDuyuru.Name = "RchDuyuru";
            RchDuyuru.Size = new Size(359, 148);
            RchDuyuru.TabIndex = 0;
            RchDuyuru.Text = "";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(linkLabel1);
            groupBox3.Controls.Add(LblDoktorID);
            groupBox3.Controls.Add(CmbDoktor);
            groupBox3.Controls.Add(BtnKaydet);
            groupBox3.Controls.Add(ChcDurum);
            groupBox3.Controls.Add(CmbBrans);
            groupBox3.Controls.Add(MskTxtSaat);
            groupBox3.Controls.Add(MskTxtTarih);
            groupBox3.Controls.Add(label12);
            groupBox3.Controls.Add(label11);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(label5);
            groupBox3.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox3.Location = new Point(410, 17);
            groupBox3.Margin = new Padding(4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4);
            groupBox3.Size = new Size(353, 386);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Randevu Paneli";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.LinkColor = Color.DarkRed;
            linkLabel1.Location = new Point(205, 331);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(85, 25);
            linkLabel1.TabIndex = 18;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Temizle";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // LblDoktorID
            // 
            LblDoktorID.AutoSize = true;
            LblDoktorID.ForeColor = Color.Cornsilk;
            LblDoktorID.Location = new Point(116, 45);
            LblDoktorID.Name = "LblDoktorID";
            LblDoktorID.Size = new Size(24, 25);
            LblDoktorID.TabIndex = 17;
            LblDoktorID.Text = "0";
            // 
            // CmbDoktor
            // 
            CmbDoktor.FormattingEnabled = true;
            CmbDoktor.Location = new Point(116, 230);
            CmbDoktor.Margin = new Padding(4);
            CmbDoktor.Name = "CmbDoktor";
            CmbDoktor.Size = new Size(207, 33);
            CmbDoktor.TabIndex = 15;
            // 
            // BtnKaydet
            // 
            BtnKaydet.BackColor = Color.PaleTurquoise;
            BtnKaydet.Location = new Point(19, 318);
            BtnKaydet.Margin = new Padding(4);
            BtnKaydet.Name = "BtnKaydet";
            BtnKaydet.Size = new Size(144, 50);
            BtnKaydet.TabIndex = 14;
            BtnKaydet.Text = "Kaydet";
            BtnKaydet.UseVisualStyleBackColor = false;
            BtnKaydet.Click += BtnKaydet_Click;
            // 
            // ChcDurum
            // 
            ChcDurum.AutoSize = true;
            ChcDurum.Location = new Point(116, 281);
            ChcDurum.Margin = new Padding(4);
            ChcDurum.Name = "ChcDurum";
            ChcDurum.Size = new Size(106, 29);
            ChcDurum.TabIndex = 13;
            ChcDurum.Text = "Durum";
            ChcDurum.UseVisualStyleBackColor = true;
            // 
            // CmbBrans
            // 
            CmbBrans.FormattingEnabled = true;
            CmbBrans.Location = new Point(117, 177);
            CmbBrans.Margin = new Padding(4);
            CmbBrans.Name = "CmbBrans";
            CmbBrans.Size = new Size(206, 33);
            CmbBrans.TabIndex = 11;
            CmbBrans.SelectedIndexChanged += CmbBrans_SelectedIndexChanged;
            // 
            // MskTxtSaat
            // 
            MskTxtSaat.Location = new Point(119, 130);
            MskTxtSaat.Margin = new Padding(4);
            MskTxtSaat.Mask = "00:00";
            MskTxtSaat.Name = "MskTxtSaat";
            MskTxtSaat.Size = new Size(105, 34);
            MskTxtSaat.TabIndex = 10;
            MskTxtSaat.ValidatingType = typeof(DateTime);
            // 
            // MskTxtTarih
            // 
            MskTxtTarih.Location = new Point(117, 83);
            MskTxtTarih.Margin = new Padding(4);
            MskTxtTarih.Mask = "00/00/0000";
            MskTxtTarih.Name = "MskTxtTarih";
            MskTxtTarih.Size = new Size(151, 34);
            MskTxtTarih.TabIndex = 9;
            MskTxtTarih.ValidatingType = typeof(DateTime);
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(32, 137);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(77, 25);
            label12.TabIndex = 7;
            label12.Text = "Saat : ";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(19, 180);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(92, 25);
            label11.TabIndex = 6;
            label11.Text = "Branş : ";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(24, 86);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(85, 25);
            label10.TabIndex = 5;
            label10.Text = "Tarih : ";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(8, 233);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(103, 25);
            label8.TabIndex = 3;
            label8.Text = "Doktor : ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(53, 45);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(58, 25);
            label5.TabIndex = 0;
            label5.Text = "ID : ";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(dataGridView2);
            groupBox4.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox4.Location = new Point(775, 298);
            groupBox4.Margin = new Padding(4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(4);
            groupBox4.Size = new Size(503, 274);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Doktorlar";
            // 
            // dataGridView2
            // 
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = SystemColors.Control;
            dataGridViewCellStyle13.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle13.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = DataGridViewTriState.True;
            dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = SystemColors.Window;
            dataGridViewCellStyle14.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle14.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = DataGridViewTriState.False;
            dataGridView2.DefaultCellStyle = dataGridViewCellStyle14;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(4, 27);
            dataGridView2.Margin = new Padding(4);
            dataGridView2.Name = "dataGridView2";
            dataGridViewCellStyle15.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = SystemColors.Control;
            dataGridViewCellStyle15.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle15.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = DataGridViewTriState.True;
            dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.RowTemplate.Height = 29;
            dataGridView2.Size = new Size(495, 243);
            dataGridView2.TabIndex = 0;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(dataGridView1);
            groupBox5.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox5.Location = new Point(771, 17);
            groupBox5.Margin = new Padding(4);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(4);
            groupBox5.Size = new Size(511, 273);
            groupBox5.TabIndex = 4;
            groupBox5.TabStop = false;
            groupBox5.Text = "Branşlar";
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle16.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = SystemColors.Control;
            dataGridViewCellStyle16.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle16.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle17.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = SystemColors.Window;
            dataGridViewCellStyle17.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle17.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle17;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(4, 27);
            dataGridView1.Margin = new Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle18.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = SystemColors.Control;
            dataGridViewCellStyle18.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle18.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(503, 242);
            dataGridView1.TabIndex = 0;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(button1);
            groupBox6.Controls.Add(BtnSekreter);
            groupBox6.Controls.Add(BtnDuyuru);
            groupBox6.Controls.Add(BtnRandevuListe);
            groupBox6.Controls.Add(BtnBransPaneli);
            groupBox6.Controls.Add(BtnDoktorPaneli);
            groupBox6.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox6.Location = new Point(26, 411);
            groupBox6.Margin = new Padding(4);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(4);
            groupBox6.Size = new Size(737, 161);
            groupBox6.TabIndex = 5;
            groupBox6.TabStop = false;
            groupBox6.Text = "Hızlı Erişim";
            // 
            // BtnSekreter
            // 
            BtnSekreter.BackColor = Color.PaleTurquoise;
            BtnSekreter.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnSekreter.Location = new Point(476, 34);
            BtnSekreter.Name = "BtnSekreter";
            BtnSekreter.Size = new Size(120, 121);
            BtnSekreter.TabIndex = 4;
            BtnSekreter.Text = "Sekreter Detay";
            BtnSekreter.UseVisualStyleBackColor = false;
            BtnSekreter.Click += BtnSekreter_Click;
            // 
            // BtnDuyuru
            // 
            BtnDuyuru.BackColor = Color.PaleTurquoise;
            BtnDuyuru.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnDuyuru.Location = new Point(336, 34);
            BtnDuyuru.Name = "BtnDuyuru";
            BtnDuyuru.Size = new Size(134, 120);
            BtnDuyuru.TabIndex = 3;
            BtnDuyuru.Text = "Duyuru Paneli";
            BtnDuyuru.UseVisualStyleBackColor = false;
            BtnDuyuru.Click += BtnDuyuru_Click;
            // 
            // BtnRandevuListe
            // 
            BtnRandevuListe.BackColor = Color.PaleTurquoise;
            BtnRandevuListe.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnRandevuListe.Location = new Point(106, 33);
            BtnRandevuListe.Margin = new Padding(4);
            BtnRandevuListe.Name = "BtnRandevuListe";
            BtnRandevuListe.Size = new Size(97, 119);
            BtnRandevuListe.TabIndex = 2;
            BtnRandevuListe.Text = "Randevu Liste";
            BtnRandevuListe.UseVisualStyleBackColor = false;
            BtnRandevuListe.Click += BtnRandevuListe_Click;
            // 
            // BtnBransPaneli
            // 
            BtnBransPaneli.BackColor = Color.PaleTurquoise;
            BtnBransPaneli.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnBransPaneli.Location = new Point(211, 33);
            BtnBransPaneli.Margin = new Padding(4);
            BtnBransPaneli.Name = "BtnBransPaneli";
            BtnBransPaneli.Size = new Size(118, 119);
            BtnBransPaneli.TabIndex = 1;
            BtnBransPaneli.Text = "Branş Detay";
            BtnBransPaneli.UseVisualStyleBackColor = false;
            BtnBransPaneli.Click += BtnBransPaneli_Click;
            // 
            // BtnDoktorPaneli
            // 
            BtnDoktorPaneli.BackColor = Color.PaleTurquoise;
            BtnDoktorPaneli.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            BtnDoktorPaneli.Location = new Point(9, 35);
            BtnDoktorPaneli.Margin = new Padding(4);
            BtnDoktorPaneli.Name = "BtnDoktorPaneli";
            BtnDoktorPaneli.Size = new Size(89, 118);
            BtnDoktorPaneli.TabIndex = 0;
            BtnDoktorPaneli.Text = "Doktor Detay";
            BtnDoktorPaneli.UseVisualStyleBackColor = false;
            BtnDoktorPaneli.Click += BtnDoktorPaneli_Click;
            // 
            // BtnHomePage
            // 
            BtnHomePage.BackgroundImage = (Image)resources.GetObject("BtnHomePage.BackgroundImage");
            BtnHomePage.BackgroundImageLayout = ImageLayout.Stretch;
            BtnHomePage.FlatAppearance.BorderSize = 0;
            BtnHomePage.FlatStyle = FlatStyle.Flat;
            BtnHomePage.ForeColor = Color.Cornsilk;
            BtnHomePage.Location = new Point(1285, 26);
            BtnHomePage.Name = "BtnHomePage";
            BtnHomePage.Size = new Size(126, 102);
            BtnHomePage.TabIndex = 26;
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
            BtnCikis.ForeColor = Color.Cornsilk;
            BtnCikis.Location = new Point(1285, 483);
            BtnCikis.Name = "BtnCikis";
            BtnCikis.Size = new Size(129, 89);
            BtnCikis.TabIndex = 27;
            BtnCikis.UseVisualStyleBackColor = false;
            BtnCikis.Click += BtnCikis_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.PaleTurquoise;
            button1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(602, 35);
            button1.Name = "button1";
            button1.Size = new Size(121, 119);
            button1.TabIndex = 5;
            button1.Text = "İlaç Detay";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // FormSekreterDetay
            // 
            AutoScaleDimensions = new SizeF(12F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(1415, 577);
            Controls.Add(BtnCikis);
            Controls.Add(BtnHomePage);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "FormSekreterDetay";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sekreter Detay";
            FormClosing += FormSekreterDetay_FormClosing;
            Load += FormSekreterDetay_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox6.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label LblAdSoyad;
        private Label label3;
        private Label LblTC;
        private Label label1;
        private GroupBox groupBox2;
        private Button BtnOlustur;
        private RichTextBox RchDuyuru;
        private GroupBox groupBox3;
        private ComboBox CmbBrans;
        private MaskedTextBox MskTxtSaat;
        private MaskedTextBox MskTxtTarih;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label8;
        private Label label5;
        private ComboBox CmbDoktor;
        private Button BtnKaydet;
        private CheckBox ChcDurum;
        private GroupBox groupBox4;
        private DataGridView dataGridView2;
        private GroupBox groupBox5;
        private DataGridView dataGridView1;
        private GroupBox groupBox6;
        private Button BtnRandevuListe;
        private Button BtnBransPaneli;
        private Button BtnDoktorPaneli;
        private Button BtnDuyuru;
        private Button BtnHomePage;
        private Button BtnCikis;
        private Label LblDoktorID;
        private LinkLabel linkLabel1;
        private Button BtnSekreter;
        private Button button1;
    }
}