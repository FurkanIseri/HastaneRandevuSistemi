namespace HastaneKayitSistemi
{
    partial class FormHastaKayit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHastaKayit));
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            TxtAd = new TextBox();
            TxtSoyad = new TextBox();
            MskTxtTelefon = new MaskedTextBox();
            TxtSifre = new TextBox();
            CmbCinsiyet = new ComboBox();
            BtnKayit = new Button();
            label3 = new Label();
            MskTxtTC = new MaskedTextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(119, 23);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(73, 32);
            label1.TabIndex = 0;
            label1.Text = "Ad : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(83, 73);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(111, 32);
            label2.TabIndex = 1;
            label2.Text = "Soyad : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(69, 167);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(123, 32);
            label4.TabIndex = 3;
            label4.Text = "Telefon : ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(100, 211);
            label5.Margin = new Padding(5, 0, 5, 0);
            label5.Name = "label5";
            label5.Size = new Size(93, 32);
            label5.TabIndex = 4;
            label5.Text = "Şifre : ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(57, 259);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(135, 32);
            label6.TabIndex = 5;
            label6.Text = "Cinsiyet : ";
            // 
            // TxtAd
            // 
            TxtAd.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            TxtAd.Location = new Point(202, 25);
            TxtAd.Margin = new Padding(5, 4, 5, 4);
            TxtAd.Name = "TxtAd";
            TxtAd.Size = new Size(201, 34);
            TxtAd.TabIndex = 0;
            // 
            // TxtSoyad
            // 
            TxtSoyad.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            TxtSoyad.Location = new Point(202, 71);
            TxtSoyad.Margin = new Padding(5, 4, 5, 4);
            TxtSoyad.Name = "TxtSoyad";
            TxtSoyad.Size = new Size(201, 34);
            TxtSoyad.TabIndex = 1;
            // 
            // MskTxtTelefon
            // 
            MskTxtTelefon.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            MskTxtTelefon.Location = new Point(202, 165);
            MskTxtTelefon.Margin = new Padding(5, 4, 5, 4);
            MskTxtTelefon.Mask = "(999) 000-0000";
            MskTxtTelefon.Name = "MskTxtTelefon";
            MskTxtTelefon.Size = new Size(201, 34);
            MskTxtTelefon.TabIndex = 3;
            // 
            // TxtSifre
            // 
            TxtSifre.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            TxtSifre.Location = new Point(203, 213);
            TxtSifre.Margin = new Padding(5, 4, 5, 4);
            TxtSifre.Name = "TxtSifre";
            TxtSifre.Size = new Size(201, 34);
            TxtSifre.TabIndex = 4;
            // 
            // CmbCinsiyet
            // 
            CmbCinsiyet.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            CmbCinsiyet.FormattingEnabled = true;
            CmbCinsiyet.Items.AddRange(new object[] { "Erkek", "Kadın" });
            CmbCinsiyet.Location = new Point(203, 261);
            CmbCinsiyet.Margin = new Padding(5, 4, 5, 4);
            CmbCinsiyet.Name = "CmbCinsiyet";
            CmbCinsiyet.Size = new Size(201, 33);
            CmbCinsiyet.TabIndex = 5;
            // 
            // BtnKayit
            // 
            BtnKayit.BackColor = Color.PaleTurquoise;
            BtnKayit.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            BtnKayit.Location = new Point(202, 317);
            BtnKayit.Margin = new Padding(5, 4, 5, 4);
            BtnKayit.Name = "BtnKayit";
            BtnKayit.Size = new Size(153, 38);
            BtnKayit.TabIndex = 6;
            BtnKayit.Text = "Kayıt Yap";
            BtnKayit.UseVisualStyleBackColor = false;
            BtnKayit.Click += BtnKayit_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(10, 117);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(182, 32);
            label3.TabIndex = 2;
            label3.Text = "TcKimlikNo : ";
            // 
            // MskTxtTC
            // 
            MskTxtTC.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            MskTxtTC.Location = new Point(202, 119);
            MskTxtTC.Margin = new Padding(5, 4, 5, 4);
            MskTxtTC.Mask = "00000000000";
            MskTxtTC.Name = "MskTxtTC";
            MskTxtTC.Size = new Size(201, 34);
            MskTxtTC.TabIndex = 2;
            // 
            // FormHastaKayit
            // 
            AcceptButton = BtnKayit;
            AutoScaleDimensions = new SizeF(13F, 26F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(481, 376);
            Controls.Add(BtnKayit);
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
            Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            MaximizeBox = false;
            Name = "FormHastaKayit";
            Text = "Hasta Kayıt";
            Load += FormHastaKayit_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox TxtAd;
        private TextBox TxtSoyad;
        private MaskedTextBox MskTxtTelefon;
        private TextBox TxtSifre;
        private ComboBox CmbCinsiyet;
        private Button BtnKayit;
        private Label label3;
        private MaskedTextBox MskTxtTC;
    }
}