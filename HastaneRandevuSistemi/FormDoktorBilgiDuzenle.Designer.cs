namespace HastaneRandevuSistemi
{
    partial class FormDoktorBilgiDuzenle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDoktorBilgiDuzenle));
            TxtSifre = new TextBox();
            MskTxtTC = new MaskedTextBox();
            TxtSoyad = new TextBox();
            TxtAd = new TextBox();
            label5 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            label4 = new Label();
            CmbBrans = new ComboBox();
            BtnGuncelle = new Button();
            label6 = new Label();
            CmbCinsiyet = new ComboBox();
            SuspendLayout();
            // 
            // TxtSifre
            // 
            TxtSifre.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            TxtSifre.Location = new Point(206, 188);
            TxtSifre.Margin = new Padding(9, 5, 9, 5);
            TxtSifre.Name = "TxtSifre";
            TxtSifre.Size = new Size(209, 34);
            TxtSifre.TabIndex = 22;
            // 
            // MskTxtTC
            // 
            MskTxtTC.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            MskTxtTC.Location = new Point(206, 112);
            MskTxtTC.Margin = new Padding(9, 5, 9, 5);
            MskTxtTC.Mask = "00000000000";
            MskTxtTC.Name = "MskTxtTC";
            MskTxtTC.Size = new Size(211, 34);
            MskTxtTC.TabIndex = 20;
            // 
            // TxtSoyad
            // 
            TxtSoyad.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            TxtSoyad.Location = new Point(206, 68);
            TxtSoyad.Margin = new Padding(9, 5, 9, 5);
            TxtSoyad.Name = "TxtSoyad";
            TxtSoyad.Size = new Size(211, 34);
            TxtSoyad.TabIndex = 19;
            // 
            // TxtAd
            // 
            TxtAd.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            TxtAd.Location = new Point(207, 26);
            TxtAd.Margin = new Padding(9, 5, 9, 5);
            TxtAd.Name = "TxtAd";
            TxtAd.Size = new Size(211, 34);
            TxtAd.TabIndex = 18;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(98, 191);
            label5.Margin = new Padding(9, 0, 9, 0);
            label5.Name = "label5";
            label5.Size = new Size(79, 25);
            label5.TabIndex = 16;
            label5.Text = "Şifre : ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(28, 115);
            label3.Margin = new Padding(9, 0, 9, 0);
            label3.Name = "label3";
            label3.Size = new Size(149, 25);
            label3.TabIndex = 14;
            label3.Text = "TcKimlikNo : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(84, 71);
            label2.Margin = new Padding(9, 0, 9, 0);
            label2.Name = "label2";
            label2.Size = new Size(93, 25);
            label2.TabIndex = 13;
            label2.Text = "Soyad : ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(116, 29);
            label1.Margin = new Padding(9, 0, 9, 0);
            label1.Name = "label1";
            label1.Size = new Size(61, 25);
            label1.TabIndex = 12;
            label1.Text = "Ad : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(85, 153);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(92, 25);
            label4.TabIndex = 23;
            label4.Text = "Branş : ";
            // 
            // CmbBrans
            // 
            CmbBrans.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            CmbBrans.FormattingEnabled = true;
            CmbBrans.Location = new Point(206, 150);
            CmbBrans.Name = "CmbBrans";
            CmbBrans.Size = new Size(211, 33);
            CmbBrans.TabIndex = 24;
            // 
            // BtnGuncelle
            // 
            BtnGuncelle.BackColor = Color.Lime;
            BtnGuncelle.Location = new Point(125, 274);
            BtnGuncelle.Name = "BtnGuncelle";
            BtnGuncelle.Size = new Size(176, 54);
            BtnGuncelle.TabIndex = 25;
            BtnGuncelle.Text = "Güncelle";
            BtnGuncelle.UseVisualStyleBackColor = false;
            BtnGuncelle.Click += BtnGuncelle_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(66, 226);
            label6.Margin = new Padding(9, 0, 9, 0);
            label6.Name = "label6";
            label6.Size = new Size(111, 25);
            label6.TabIndex = 26;
            label6.Text = "Cinsiyet : ";
            // 
            // CmbCinsiyet
            // 
            CmbCinsiyet.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            CmbCinsiyet.FormattingEnabled = true;
            CmbCinsiyet.Location = new Point(206, 226);
            CmbCinsiyet.Name = "CmbCinsiyet";
            CmbCinsiyet.Size = new Size(209, 33);
            CmbCinsiyet.TabIndex = 27;
            // 
            // FormDoktorBilgiDuzenle
            // 
            AutoScaleDimensions = new SizeF(14F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.DarkCyan;
            ClientSize = new Size(425, 341);
            Controls.Add(CmbCinsiyet);
            Controls.Add(label6);
            Controls.Add(BtnGuncelle);
            Controls.Add(CmbBrans);
            Controls.Add(label4);
            Controls.Add(TxtSifre);
            Controls.Add(MskTxtTC);
            Controls.Add(TxtSoyad);
            Controls.Add(TxtAd);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            MaximizeBox = false;
            Name = "FormDoktorBilgiDuzenle";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Doktor Bilgi Düzenle";
            Load += FormDoktorBilgiDuzenle_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TxtSifre;
        private MaskedTextBox MskTxtTC;
        private TextBox TxtSoyad;
        private TextBox TxtAd;
        private Label label5;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label4;
        private ComboBox CmbBrans;
        private Button BtnGuncelle;
        private Label label6;
        private ComboBox CmbCinsiyet;
    }
}