namespace HastaneKayitSistemi
{
    partial class FormDoktorPaneli
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDoktorPaneli));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            TxtAd = new TextBox();
            TxtSoyad = new TextBox();
            CmbBrans = new ComboBox();
            TxtSifre = new TextBox();
            dataGridView1 = new DataGridView();
            BtnEkle = new Button();
            button2 = new Button();
            BtnGuncelle = new Button();
            Temizle = new LinkLabel();
            MskTxtTC = new MaskedTextBox();
            label6 = new Label();
            CmbCinsiyet = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(82, 19);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(61, 25);
            label1.TabIndex = 0;
            label1.Text = "Ad : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(50, 66);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(93, 25);
            label2.TabIndex = 1;
            label2.Text = "Soyad : ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(79, 149);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(64, 25);
            label3.TabIndex = 2;
            label3.Text = "TC : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(51, 108);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(92, 25);
            label4.TabIndex = 3;
            label4.Text = "Branş : ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(67, 191);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(79, 25);
            label5.TabIndex = 4;
            label5.Text = "Şifre : ";
            // 
            // TxtAd
            // 
            TxtAd.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            TxtAd.Location = new Point(153, 16);
            TxtAd.Margin = new Padding(5, 4, 5, 4);
            TxtAd.Name = "TxtAd";
            TxtAd.Size = new Size(164, 34);
            TxtAd.TabIndex = 1;
            // 
            // TxtSoyad
            // 
            TxtSoyad.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            TxtSoyad.Location = new Point(153, 63);
            TxtSoyad.Margin = new Padding(5, 4, 5, 4);
            TxtSoyad.Name = "TxtSoyad";
            TxtSoyad.Size = new Size(164, 34);
            TxtSoyad.TabIndex = 2;
            // 
            // CmbBrans
            // 
            CmbBrans.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            CmbBrans.FormattingEnabled = true;
            CmbBrans.Location = new Point(153, 105);
            CmbBrans.Margin = new Padding(5, 4, 5, 4);
            CmbBrans.Name = "CmbBrans";
            CmbBrans.Size = new Size(164, 33);
            CmbBrans.TabIndex = 3;
            // 
            // TxtSifre
            // 
            TxtSifre.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            TxtSifre.Location = new Point(153, 188);
            TxtSifre.Margin = new Padding(5, 4, 5, 4);
            TxtSifre.Name = "TxtSifre";
            TxtSifre.Size = new Size(164, 34);
            TxtSifre.TabIndex = 5;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.ControlLightLight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(406, 13);
            dataGridView1.Margin = new Padding(5, 4, 5, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1148, 344);
            dataGridView1.TabIndex = 10;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // BtnEkle
            // 
            BtnEkle.BackColor = Color.FromArgb(192, 192, 0);
            BtnEkle.Location = new Point(35, 268);
            BtnEkle.Margin = new Padding(5, 4, 5, 4);
            BtnEkle.Name = "BtnEkle";
            BtnEkle.Size = new Size(167, 45);
            BtnEkle.TabIndex = 6;
            BtnEkle.Text = "Ekle";
            BtnEkle.UseVisualStyleBackColor = false;
            BtnEkle.Click += BtnEkle_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Red;
            button2.Location = new Point(225, 268);
            button2.Margin = new Padding(5, 4, 5, 4);
            button2.Name = "button2";
            button2.Size = new Size(171, 45);
            button2.TabIndex = 7;
            button2.Text = "Sil";
            button2.UseVisualStyleBackColor = false;
            // 
            // BtnGuncelle
            // 
            BtnGuncelle.BackColor = Color.PaleTurquoise;
            BtnGuncelle.Location = new Point(79, 315);
            BtnGuncelle.Margin = new Padding(5, 4, 5, 4);
            BtnGuncelle.Name = "BtnGuncelle";
            BtnGuncelle.Size = new Size(169, 42);
            BtnGuncelle.TabIndex = 8;
            BtnGuncelle.Text = "Güncelle";
            BtnGuncelle.UseVisualStyleBackColor = false;
            BtnGuncelle.Click += BtnGuncelle_Click;
            // 
            // Temizle
            // 
            Temizle.AutoSize = true;
            Temizle.LinkColor = Color.DarkRed;
            Temizle.Location = new Point(286, 315);
            Temizle.Name = "Temizle";
            Temizle.Size = new Size(85, 25);
            Temizle.TabIndex = 29;
            Temizle.TabStop = true;
            Temizle.Text = "Temizle";
            Temizle.LinkClicked += Temizle_LinkClicked;
            // 
            // MskTxtTC
            // 
            MskTxtTC.Location = new Point(153, 147);
            MskTxtTC.Mask = "00000000000";
            MskTxtTC.Name = "MskTxtTC";
            MskTxtTC.Size = new Size(164, 34);
            MskTxtTC.TabIndex = 30;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(35, 231);
            label6.Name = "label6";
            label6.Size = new Size(111, 25);
            label6.TabIndex = 31;
            label6.Text = "Cinsiyet : ";
            // 
            // CmbCinsiyet
            // 
            CmbCinsiyet.FormattingEnabled = true;
            CmbCinsiyet.Location = new Point(153, 228);
            CmbCinsiyet.Name = "CmbCinsiyet";
            CmbCinsiyet.Size = new Size(164, 33);
            CmbCinsiyet.TabIndex = 32;
            // 
            // FormDoktorPaneli
            // 
            AutoScaleDimensions = new SizeF(14F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(1568, 363);
            Controls.Add(CmbCinsiyet);
            Controls.Add(label6);
            Controls.Add(MskTxtTC);
            Controls.Add(Temizle);
            Controls.Add(BtnGuncelle);
            Controls.Add(button2);
            Controls.Add(BtnEkle);
            Controls.Add(dataGridView1);
            Controls.Add(TxtSifre);
            Controls.Add(CmbBrans);
            Controls.Add(TxtSoyad);
            Controls.Add(TxtAd);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            MaximizeBox = false;
            Name = "FormDoktorPaneli";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Doktor Paneli";
            Load += FormDoktorPaneli_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox TxtAd;
        private TextBox TxtSoyad;
        private ComboBox CmbBrans;
        private TextBox TxtSifre;
        private DataGridView dataGridView1;
        private Button BtnEkle;
        private Button button2;
        private Button BtnGuncelle;
        private LinkLabel Temizle;
        private MaskedTextBox MskTxtTC;
        private Label label6;
        private ComboBox CmbCinsiyet;
    }
}