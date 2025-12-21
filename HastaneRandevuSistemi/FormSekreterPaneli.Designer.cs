namespace HastaneRandevuSistemi
{
    partial class FormSekreterPaneli
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSekreterPaneli));
            TxtSifre = new TextBox();
            label5 = new Label();
            TxtSoyad = new TextBox();
            TxtAd = new TextBox();
            label2 = new Label();
            label1 = new Label();
            Temizle = new LinkLabel();
            BtnGuncelle = new Button();
            BtnSil = new Button();
            BtnEkle = new Button();
            dataGridView1 = new DataGridView();
            label3 = new Label();
            MskTxtTC = new MaskedTextBox();
            CmbCinsiyet = new ComboBox();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // TxtSifre
            // 
            TxtSifre.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            TxtSifre.Location = new Point(158, 136);
            TxtSifre.Margin = new Padding(5, 4, 5, 4);
            TxtSifre.Name = "TxtSifre";
            TxtSifre.Size = new Size(164, 34);
            TxtSifre.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(69, 139);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(79, 25);
            label5.TabIndex = 6;
            label5.Text = "Şifre : ";
            // 
            // TxtSoyad
            // 
            TxtSoyad.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            TxtSoyad.Location = new Point(158, 51);
            TxtSoyad.Margin = new Padding(5, 4, 5, 4);
            TxtSoyad.Name = "TxtSoyad";
            TxtSoyad.Size = new Size(164, 34);
            TxtSoyad.TabIndex = 11;
            // 
            // TxtAd
            // 
            TxtAd.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            TxtAd.Location = new Point(158, 10);
            TxtAd.Margin = new Padding(5, 4, 5, 4);
            TxtAd.Name = "TxtAd";
            TxtAd.Size = new Size(164, 34);
            TxtAd.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(55, 56);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(93, 25);
            label2.TabIndex = 10;
            label2.Text = "Soyad : ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(87, 13);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(61, 25);
            label1.TabIndex = 8;
            label1.Text = "Ad : ";
            // 
            // Temizle
            // 
            Temizle.AutoSize = true;
            Temizle.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            Temizle.LinkColor = Color.DarkRed;
            Temizle.Location = new Point(265, 285);
            Temizle.Name = "Temizle";
            Temizle.Size = new Size(85, 25);
            Temizle.TabIndex = 33;
            Temizle.TabStop = true;
            Temizle.Text = "Temizle";
            Temizle.LinkClicked += Temizle_LinkClicked;
            // 
            // BtnGuncelle
            // 
            BtnGuncelle.BackColor = Color.PaleTurquoise;
            BtnGuncelle.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            BtnGuncelle.Location = new Point(58, 268);
            BtnGuncelle.Margin = new Padding(5, 4, 5, 4);
            BtnGuncelle.Name = "BtnGuncelle";
            BtnGuncelle.Size = new Size(169, 59);
            BtnGuncelle.TabIndex = 32;
            BtnGuncelle.Text = "Güncelle";
            BtnGuncelle.UseVisualStyleBackColor = false;
            BtnGuncelle.Click += BtnGuncelle_Click;
            // 
            // BtnSil
            // 
            BtnSil.BackColor = Color.Red;
            BtnSil.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            BtnSil.Location = new Point(214, 218);
            BtnSil.Margin = new Padding(5, 4, 5, 4);
            BtnSil.Name = "BtnSil";
            BtnSil.Size = new Size(171, 42);
            BtnSil.TabIndex = 31;
            BtnSil.Text = "Sil";
            BtnSil.UseVisualStyleBackColor = false;
            BtnSil.Click += BtnSil_Click;
            // 
            // BtnEkle
            // 
            BtnEkle.BackColor = Color.Lime;
            BtnEkle.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            BtnEkle.Location = new Point(14, 218);
            BtnEkle.Margin = new Padding(5, 4, 5, 4);
            BtnEkle.Name = "BtnEkle";
            BtnEkle.Size = new Size(167, 42);
            BtnEkle.TabIndex = 30;
            BtnEkle.Text = "Ekle";
            BtnEkle.UseVisualStyleBackColor = false;
            BtnEkle.Click += BtnEkle_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.ControlLightLight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(395, 13);
            dataGridView1.Margin = new Padding(5, 4, 5, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(779, 314);
            dataGridView1.TabIndex = 34;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(84, 97);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(64, 25);
            label3.TabIndex = 35;
            label3.Text = "TC : ";
            // 
            // MskTxtTC
            // 
            MskTxtTC.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            MskTxtTC.Location = new Point(158, 96);
            MskTxtTC.Mask = "00000000000";
            MskTxtTC.Name = "MskTxtTC";
            MskTxtTC.Size = new Size(164, 34);
            MskTxtTC.TabIndex = 36;
            // 
            // CmbCinsiyet
            // 
            CmbCinsiyet.FormattingEnabled = true;
            CmbCinsiyet.Items.AddRange(new object[] { "Erkek", "Kadın" });
            CmbCinsiyet.Location = new Point(158, 177);
            CmbCinsiyet.Name = "CmbCinsiyet";
            CmbCinsiyet.Size = new Size(164, 31);
            CmbCinsiyet.TabIndex = 38;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(37, 178);
            label6.Name = "label6";
            label6.Size = new Size(111, 25);
            label6.TabIndex = 37;
            label6.Text = "Cinsiyet : ";
            // 
            // FormSekreterPaneli
            // 
            AutoScaleDimensions = new SizeF(12F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkCyan;
            ClientSize = new Size(1179, 341);
            Controls.Add(CmbCinsiyet);
            Controls.Add(label6);
            Controls.Add(MskTxtTC);
            Controls.Add(label3);
            Controls.Add(dataGridView1);
            Controls.Add(Temizle);
            Controls.Add(BtnGuncelle);
            Controls.Add(BtnSil);
            Controls.Add(BtnEkle);
            Controls.Add(TxtSoyad);
            Controls.Add(TxtAd);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(TxtSifre);
            Controls.Add(label5);
            Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "FormSekreterPaneli";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sekreter Paneli";
            Load += FormSekreterPaneli_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TxtSifre;
        private Label label5;
        private TextBox TxtSoyad;
        private TextBox TxtAd;
        private Label label2;
        private Label label1;
        private LinkLabel Temizle;
        private Button BtnGuncelle;
        private Button BtnSil;
        private Button BtnEkle;
        private DataGridView dataGridView1;
        private Label label3;
        private MaskedTextBox MskTxtTC;
        private ComboBox CmbCinsiyet;
        private Label label6;
    }
}