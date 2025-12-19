namespace HastaneKayitSistemi
{
    partial class FormHastaDetay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHastaDetay));
            groupBox1 = new GroupBox();
            LblAdSoyad = new Label();
            LblTC = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            CmbHastAd = new ComboBox();
            label4 = new Label();
            CmbSehir = new ComboBox();
            label3 = new Label();
            LnkTemizle = new LinkLabel();
            LblID = new Label();
            Lbl = new Label();
            LnkBilgiDuzenle = new LinkLabel();
            BtnRandevu = new Button();
            label7 = new Label();
            RchSikayet = new RichTextBox();
            CmbDoktor = new ComboBox();
            label6 = new Label();
            label5 = new Label();
            CmbBrans = new ComboBox();
            groupBox3 = new GroupBox();
            dataGridView1 = new DataGridView();
            groupBox4 = new GroupBox();
            dataGridView2 = new DataGridView();
            BtnHomePage = new Button();
            BtnCikis = new Button();
            BtnRecete = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(LblAdSoyad);
            groupBox1.Controls.Add(LblTC);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.Location = new Point(31, 9);
            groupBox1.Margin = new Padding(5, 4, 5, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(5, 4, 5, 4);
            groupBox1.Size = new Size(447, 141);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Kişi Bilgi";
            // 
            // LblAdSoyad
            // 
            LblAdSoyad.AutoSize = true;
            LblAdSoyad.ForeColor = Color.Cornsilk;
            LblAdSoyad.Location = new Point(200, 87);
            LblAdSoyad.Margin = new Padding(5, 0, 5, 0);
            LblAdSoyad.Name = "LblAdSoyad";
            LblAdSoyad.Size = new Size(98, 25);
            LblAdSoyad.TabIndex = 3;
            LblAdSoyad.Text = "Null Null";
            // 
            // LblTC
            // 
            LblTC.AutoSize = true;
            LblTC.ForeColor = Color.Cornsilk;
            LblTC.Location = new Point(200, 40);
            LblTC.Margin = new Padding(5, 0, 5, 0);
            LblTC.Name = "LblTC";
            LblTC.Size = new Size(144, 25);
            LblTC.TabIndex = 2;
            LblTC.Text = "00000000000";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(46, 87);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(128, 25);
            label2.TabIndex = 1;
            label2.Text = "Ad Soyad : ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(70, 40);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(105, 25);
            label1.TabIndex = 0;
            label1.Text = "TC NO : ";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(CmbHastAd);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(CmbSehir);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(LnkTemizle);
            groupBox2.Controls.Add(LblID);
            groupBox2.Controls.Add(Lbl);
            groupBox2.Controls.Add(LnkBilgiDuzenle);
            groupBox2.Controls.Add(BtnRandevu);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(RchSikayet);
            groupBox2.Controls.Add(CmbDoktor);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(CmbBrans);
            groupBox2.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox2.Location = new Point(14, 167);
            groupBox2.Margin = new Padding(5, 4, 5, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(5, 4, 5, 4);
            groupBox2.Size = new Size(464, 466);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Randevu Paneli";
            // 
            // CmbHastAd
            // 
            CmbHastAd.FormattingEnabled = true;
            CmbHastAd.Location = new Point(141, 113);
            CmbHastAd.Name = "CmbHastAd";
            CmbHastAd.Size = new Size(315, 33);
            CmbHastAd.TabIndex = 14;
            CmbHastAd.SelectedIndexChanged += CmbHastAd_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 121);
            label4.Name = "label4";
            label4.Size = new Size(121, 25);
            label4.TabIndex = 13;
            label4.Text = "Hastane  : ";
            // 
            // CmbSehir
            // 
            CmbSehir.FormattingEnabled = true;
            CmbSehir.Location = new Point(142, 71);
            CmbSehir.Name = "CmbSehir";
            CmbSehir.Size = new Size(244, 33);
            CmbSehir.TabIndex = 12;
            CmbSehir.SelectedIndexChanged += CmbSehir_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(51, 74);
            label3.Name = "label3";
            label3.Size = new Size(84, 25);
            label3.TabIndex = 11;
            label3.Text = "Şehir : ";
            // 
            // LnkTemizle
            // 
            LnkTemizle.AutoSize = true;
            LnkTemizle.LinkColor = Color.DarkRed;
            LnkTemizle.Location = new Point(259, 420);
            LnkTemizle.Name = "LnkTemizle";
            LnkTemizle.Size = new Size(85, 25);
            LnkTemizle.TabIndex = 10;
            LnkTemizle.TabStop = true;
            LnkTemizle.Text = "Temizle";
            LnkTemizle.LinkClicked += LnkTemizle_LinkClicked;
            // 
            // LblID
            // 
            LblID.AutoSize = true;
            LblID.ForeColor = Color.Cornsilk;
            LblID.Location = new Point(141, 31);
            LblID.Name = "LblID";
            LblID.Size = new Size(24, 25);
            LblID.TabIndex = 9;
            LblID.Text = "0";
            // 
            // Lbl
            // 
            Lbl.AutoSize = true;
            Lbl.Location = new Point(77, 31);
            Lbl.Name = "Lbl";
            Lbl.Size = new Size(58, 25);
            Lbl.TabIndex = 8;
            Lbl.Text = "ID : ";
            // 
            // LnkBilgiDuzenle
            // 
            LnkBilgiDuzenle.AutoSize = true;
            LnkBilgiDuzenle.LinkColor = Color.DarkRed;
            LnkBilgiDuzenle.Location = new Point(18, 420);
            LnkBilgiDuzenle.Margin = new Padding(5, 0, 5, 0);
            LnkBilgiDuzenle.Name = "LnkBilgiDuzenle";
            LnkBilgiDuzenle.Size = new Size(189, 25);
            LnkBilgiDuzenle.TabIndex = 7;
            LnkBilgiDuzenle.TabStop = true;
            LnkBilgiDuzenle.Text = "Bilgilerini Düzenle";
            LnkBilgiDuzenle.LinkClicked += LnkBilgiDuzenle_LinkClicked;
            // 
            // BtnRandevu
            // 
            BtnRandevu.BackColor = Color.PaleTurquoise;
            BtnRandevu.Location = new Point(87, 367);
            BtnRandevu.Margin = new Padding(5, 4, 5, 4);
            BtnRandevu.Name = "BtnRandevu";
            BtnRandevu.Size = new Size(230, 49);
            BtnRandevu.TabIndex = 5;
            BtnRandevu.Text = "Randevu Al";
            BtnRandevu.UseVisualStyleBackColor = false;
            BtnRandevu.Click += BtnRandevu_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(31, 253);
            label7.Margin = new Padding(5, 0, 5, 0);
            label7.Name = "label7";
            label7.Size = new Size(104, 25);
            label7.TabIndex = 6;
            label7.Text = "Şikayet : ";
            // 
            // RchSikayet
            // 
            RchSikayet.Location = new Point(142, 253);
            RchSikayet.Margin = new Padding(5, 4, 5, 4);
            RchSikayet.Name = "RchSikayet";
            RchSikayet.Size = new Size(243, 105);
            RchSikayet.TabIndex = 4;
            RchSikayet.Text = "";
            // 
            // CmbDoktor
            // 
            CmbDoktor.FormattingEnabled = true;
            CmbDoktor.Location = new Point(142, 203);
            CmbDoktor.Margin = new Padding(5, 4, 5, 4);
            CmbDoktor.Name = "CmbDoktor";
            CmbDoktor.Size = new Size(243, 33);
            CmbDoktor.TabIndex = 5;
            CmbDoktor.SelectedIndexChanged += CmbDoktor_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(32, 203);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(103, 25);
            label6.TabIndex = 4;
            label6.Text = "Doktor : ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(43, 159);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(92, 25);
            label5.TabIndex = 3;
            label5.Text = "Branş : ";
            // 
            // CmbBrans
            // 
            CmbBrans.FormattingEnabled = true;
            CmbBrans.Location = new Point(142, 156);
            CmbBrans.Margin = new Padding(5, 4, 5, 4);
            CmbBrans.Name = "CmbBrans";
            CmbBrans.Size = new Size(243, 33);
            CmbBrans.TabIndex = 2;
            CmbBrans.SelectedIndexChanged += CmbBrans_SelectedIndexChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dataGridView1);
            groupBox3.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox3.Location = new Point(488, 13);
            groupBox3.Margin = new Padding(5, 4, 5, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(5, 4, 5, 4);
            groupBox3.Size = new Size(695, 283);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Randevu Geçmişi";
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(5, 31);
            dataGridView1.Margin = new Padding(5, 4, 5, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(685, 248);
            dataGridView1.TabIndex = 0;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(dataGridView2);
            groupBox4.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox4.Location = new Point(488, 304);
            groupBox4.Margin = new Padding(5, 4, 5, 4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(5, 4, 5, 4);
            groupBox4.Size = new Size(695, 329);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Aktif Randevular";
            // 
            // dataGridView2
            // 
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.BackgroundColor = SystemColors.Control;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(5, 31);
            dataGridView2.Margin = new Padding(5, 4, 5, 4);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.RowTemplate.Height = 29;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.Size = new Size(685, 294);
            dataGridView2.TabIndex = 0;
            dataGridView2.CellClick += dataGridView2_CellClick;
            // 
            // BtnHomePage
            // 
            BtnHomePage.BackgroundImage = (Image)resources.GetObject("BtnHomePage.BackgroundImage");
            BtnHomePage.BackgroundImageLayout = ImageLayout.Stretch;
            BtnHomePage.FlatAppearance.BorderSize = 0;
            BtnHomePage.FlatStyle = FlatStyle.Flat;
            BtnHomePage.Location = new Point(1191, 13);
            BtnHomePage.Name = "BtnHomePage";
            BtnHomePage.Size = new Size(126, 102);
            BtnHomePage.TabIndex = 24;
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
            BtnCikis.Location = new Point(1188, 544);
            BtnCikis.Name = "BtnCikis";
            BtnCikis.Size = new Size(129, 89);
            BtnCikis.TabIndex = 25;
            BtnCikis.UseVisualStyleBackColor = false;
            BtnCikis.Click += BtnCikis_Click;
            // 
            // BtnRecete
            // 
            BtnRecete.BackgroundImage = (Image)resources.GetObject("BtnRecete.BackgroundImage");
            BtnRecete.BackgroundImageLayout = ImageLayout.Stretch;
            BtnRecete.FlatStyle = FlatStyle.Flat;
            BtnRecete.ForeColor = Color.SteelBlue;
            BtnRecete.Location = new Point(1191, 238);
            BtnRecete.Name = "BtnRecete";
            BtnRecete.Size = new Size(126, 131);
            BtnRecete.TabIndex = 26;
            BtnRecete.UseVisualStyleBackColor = false;
            BtnRecete.Click += BtnRecete_Click;
            // 
            // FormHastaDetay
            // 
            AutoScaleDimensions = new SizeF(13F, 26F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(1327, 646);
            Controls.Add(BtnRecete);
            Controls.Add(BtnCikis);
            Controls.Add(BtnHomePage);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            MaximizeBox = false;
            Name = "FormHastaDetay";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hasta Detay";
            Load += FormHastaDetay_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label LblAdSoyad;
        private Label LblTC;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private Label label5;
        private ComboBox CmbBrans;
        private RichTextBox RchSikayet;
        private Button BtnRandevu;
        private LinkLabel LnkBilgiDuzenle;
        private Label label7;
        private ComboBox CmbDoktor;
        private Label label6;
        private GroupBox groupBox3;
        private DataGridView dataGridView1;
        private GroupBox groupBox4;
        private DataGridView dataGridView2;
        private Label LblID;
        private Label Lbl;
        private LinkLabel LnkTemizle;
        private Button BtnHomePage;
        private Button BtnCikis;
        private ComboBox CmbSehir;
        private Label label3;
        private ComboBox CmbHastAd;
        private Label label4;
        private Button BtnRecete;
    }
}