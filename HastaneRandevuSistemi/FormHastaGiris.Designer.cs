namespace HastaneRandevuSistemi
{
    partial class FormHastaGiris
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHastaGiris));
            label2 = new Label();
            label3 = new Label();
            TxtSifre = new TextBox();
            BtnLogIn = new Button();
            UyeOl = new LinkLabel();
            MskTxtTC = new MaskedTextBox();
            checkBox1 = new CheckBox();
            BtnHomePage = new Button();
            SifremiUnuttum = new LinkLabel();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(12, 12);
            label2.Name = "label2";
            label2.Size = new Size(200, 32);
            label2.TabIndex = 1;
            label2.Text = "TC Kimlik No :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(126, 54);
            label3.Name = "label3";
            label3.Size = new Size(86, 32);
            label3.TabIndex = 2;
            label3.Text = "Şifre :";
            // 
            // TxtSifre
            // 
            TxtSifre.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            TxtSifre.Location = new Point(227, 54);
            TxtSifre.Name = "TxtSifre";
            TxtSifre.Size = new Size(196, 39);
            TxtSifre.TabIndex = 1;
            TxtSifre.UseSystemPasswordChar = true;
            // 
            // BtnLogIn
            // 
            BtnLogIn.BackColor = Color.Lime;
            BtnLogIn.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            BtnLogIn.ForeColor = SystemColors.ActiveCaptionText;
            BtnLogIn.Location = new Point(147, 153);
            BtnLogIn.Name = "BtnLogIn";
            BtnLogIn.Size = new Size(195, 59);
            BtnLogIn.TabIndex = 2;
            BtnLogIn.Text = "Giriş Yap";
            BtnLogIn.UseVisualStyleBackColor = false;
            BtnLogIn.Click += BtnLogIn_Click;
            // 
            // UyeOl
            // 
            UyeOl.AutoSize = true;
            UyeOl.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            UyeOl.LinkColor = Color.DarkRed;
            UyeOl.Location = new Point(156, 108);
            UyeOl.Name = "UyeOl";
            UyeOl.Size = new Size(97, 32);
            UyeOl.TabIndex = 6;
            UyeOl.TabStop = true;
            UyeOl.Text = "Üye Ol";
            UyeOl.LinkClicked += UyeOl_LinkClicked;
            // 
            // MskTxtTC
            // 
            MskTxtTC.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            MskTxtTC.Location = new Point(227, 9);
            MskTxtTC.Mask = "00000000000";
            MskTxtTC.Name = "MskTxtTC";
            MskTxtTC.Size = new Size(196, 39);
            MskTxtTC.TabIndex = 0;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBox1.ForeColor = Color.Black;
            checkBox1.Location = new Point(429, 60);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(152, 27);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "Şifreyi Göster";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // BtnHomePage
            // 
            BtnHomePage.BackgroundImage = (Image)resources.GetObject("BtnHomePage.BackgroundImage");
            BtnHomePage.BackgroundImageLayout = ImageLayout.Stretch;
            BtnHomePage.FlatAppearance.BorderSize = 0;
            BtnHomePage.FlatStyle = FlatStyle.Flat;
            BtnHomePage.Location = new Point(587, 12);
            BtnHomePage.Name = "BtnHomePage";
            BtnHomePage.Size = new Size(94, 74);
            BtnHomePage.TabIndex = 9;
            BtnHomePage.UseVisualStyleBackColor = true;
            BtnHomePage.Click += BtnHomePage_Click;
            // 
            // SifremiUnuttum
            // 
            SifremiUnuttum.AutoSize = true;
            SifremiUnuttum.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            SifremiUnuttum.LinkColor = Color.DarkRed;
            SifremiUnuttum.Location = new Point(283, 107);
            SifremiUnuttum.Name = "SifremiUnuttum";
            SifremiUnuttum.Size = new Size(201, 33);
            SifremiUnuttum.TabIndex = 10;
            SifremiUnuttum.TabStop = true;
            SifremiUnuttum.Text = "Şifremi Unuttum";
            SifremiUnuttum.LinkClicked += SifremiUnuttum_LinkClicked;
            // 
            // FormHastaGiris
            // 
            AcceptButton = BtnLogIn;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.DarkCyan;
            ClientSize = new Size(693, 228);
            Controls.Add(SifremiUnuttum);
            Controls.Add(BtnHomePage);
            Controls.Add(checkBox1);
            Controls.Add(MskTxtTC);
            Controls.Add(UyeOl);
            Controls.Add(BtnLogIn);
            Controls.Add(TxtSifre);
            Controls.Add(label3);
            Controls.Add(label2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FormHastaGiris";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hasta Girişi";
            FormClosing += FormHastaGiris_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Label label3;
        private TextBox TxtSifre;
        private Button BtnLogIn;
        private LinkLabel UyeOl;
        private MaskedTextBox MskTxtTC;
        private CheckBox checkBox1;
        private Button BtnHomePage;
        private LinkLabel SifremiUnuttum;
    }
}