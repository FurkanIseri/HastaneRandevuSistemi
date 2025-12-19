namespace HastaneKayitSistemi
{
    partial class FormBilgiDuzenle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBilgiDuzenle));
            BtnGuncelle = new Button();
            CmbCinsiyet = new ComboBox();
            TxtSifre = new TextBox();
            MskTxtTelefon = new MaskedTextBox();
            MskTxtTC = new MaskedTextBox();
            TxtSoyad = new TextBox();
            TxtAd = new TextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // BtnGuncelle
            // 
            BtnGuncelle.BackColor = Color.PaleTurquoise;
            BtnGuncelle.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            BtnGuncelle.Location = new Point(146, 311);
            BtnGuncelle.Margin = new Padding(5, 4, 5, 4);
            BtnGuncelle.Name = "BtnGuncelle";
            BtnGuncelle.Size = new Size(194, 55);
            BtnGuncelle.TabIndex = 7;
            BtnGuncelle.Text = "Güncelle";
            BtnGuncelle.UseVisualStyleBackColor = false;
            BtnGuncelle.Click += BtnGuncelle_Click;
            // 
            // CmbCinsiyet
            // 
            CmbCinsiyet.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            CmbCinsiyet.FormattingEnabled = true;
            CmbCinsiyet.Items.AddRange(new object[] { "Erkek", "Kadın" });
            CmbCinsiyet.Location = new Point(216, 264);
            CmbCinsiyet.Margin = new Padding(5, 4, 5, 4);
            CmbCinsiyet.Name = "CmbCinsiyet";
            CmbCinsiyet.Size = new Size(201, 39);
            CmbCinsiyet.TabIndex = 6;
            // 
            // TxtSifre
            // 
            TxtSifre.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            TxtSifre.Location = new Point(216, 216);
            TxtSifre.Margin = new Padding(5, 4, 5, 4);
            TxtSifre.Name = "TxtSifre";
            TxtSifre.Size = new Size(201, 39);
            TxtSifre.TabIndex = 5;
            // 
            // MskTxtTelefon
            // 
            MskTxtTelefon.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            MskTxtTelefon.Location = new Point(215, 168);
            MskTxtTelefon.Margin = new Padding(5, 4, 5, 4);
            MskTxtTelefon.Mask = "(999) 000-0000";
            MskTxtTelefon.Name = "MskTxtTelefon";
            MskTxtTelefon.Size = new Size(201, 39);
            MskTxtTelefon.TabIndex = 4;
            // 
            // MskTxtTC
            // 
            MskTxtTC.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            MskTxtTC.Location = new Point(215, 122);
            MskTxtTC.Margin = new Padding(5, 4, 5, 4);
            MskTxtTC.Mask = "00000000000";
            MskTxtTC.Name = "MskTxtTC";
            MskTxtTC.Size = new Size(201, 39);
            MskTxtTC.TabIndex = 3;
            // 
            // TxtSoyad
            // 
            TxtSoyad.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            TxtSoyad.Location = new Point(215, 74);
            TxtSoyad.Margin = new Padding(5, 4, 5, 4);
            TxtSoyad.Name = "TxtSoyad";
            TxtSoyad.Size = new Size(201, 39);
            TxtSoyad.TabIndex = 2;
            // 
            // TxtAd
            // 
            TxtAd.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            TxtAd.Location = new Point(215, 28);
            TxtAd.Margin = new Padding(5, 4, 5, 4);
            TxtAd.Name = "TxtAd";
            TxtAd.Size = new Size(201, 39);
            TxtAd.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(61, 264);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(135, 32);
            label6.TabIndex = 31;
            label6.Text = "Cinsiyet : ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(103, 220);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(93, 32);
            label5.TabIndex = 30;
            label5.Text = "Şifre : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(73, 168);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(123, 32);
            label4.TabIndex = 29;
            label4.Text = "Telefon : ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(14, 122);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(182, 32);
            label3.TabIndex = 28;
            label3.Text = "TcKimlikNo : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(85, 74);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(111, 32);
            label2.TabIndex = 27;
            label2.Text = "Soyad : ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(118, 28);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(73, 32);
            label1.TabIndex = 26;
            label1.Text = "Ad : ";
            // 
            // FormBilgiDuzenle
            // 
            AcceptButton = BtnGuncelle;
            AutoScaleDimensions = new SizeF(13F, 26F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(470, 375);
            Controls.Add(CmbCinsiyet);
            Controls.Add(TxtSifre);
            Controls.Add(MskTxtTelefon);
            Controls.Add(MskTxtTC);
            Controls.Add(TxtSoyad);
            Controls.Add(TxtAd);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(BtnGuncelle);
            Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            MaximizeBox = false;
            Name = "FormBilgiDuzenle";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hasta Bilgi Düzenle";
            Load += FormBilgiDuzenle_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnGuncelle;
        private ComboBox CmbCinsiyet;
        private TextBox TxtSifre;
        private MaskedTextBox MskTxtTelefon;
        private MaskedTextBox MskTxtTC;
        private TextBox TxtSoyad;
        private TextBox TxtAd;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}