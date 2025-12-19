namespace HastaneKayitSistemi
{
    partial class FormDoktorGiris
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDoktorGiris));
            MskTxtTC = new MaskedTextBox();
            BtnLogIn = new Button();
            TxtSifre = new TextBox();
            label3 = new Label();
            label2 = new Label();
            BtnHomePage = new Button();
            checkBox1 = new CheckBox();
            SifremiUnuttum = new LinkLabel();
            SuspendLayout();
            // 
            // MskTxtTC
            // 
            MskTxtTC.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            MskTxtTC.Location = new Point(230, 9);
            MskTxtTC.Margin = new Padding(5, 4, 5, 4);
            MskTxtTC.Mask = "00000000000";
            MskTxtTC.Name = "MskTxtTC";
            MskTxtTC.Size = new Size(216, 39);
            MskTxtTC.TabIndex = 0;
            // 
            // BtnLogIn
            // 
            BtnLogIn.BackColor = Color.PaleTurquoise;
            BtnLogIn.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            BtnLogIn.Location = new Point(155, 151);
            BtnLogIn.Margin = new Padding(5, 4, 5, 4);
            BtnLogIn.Name = "BtnLogIn";
            BtnLogIn.Size = new Size(210, 50);
            BtnLogIn.TabIndex = 2;
            BtnLogIn.Text = "Giriş Yap";
            BtnLogIn.UseVisualStyleBackColor = false;
            BtnLogIn.Click += BtnLogIn_Click;
            // 
            // TxtSifre
            // 
            TxtSifre.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            TxtSifre.Location = new Point(230, 58);
            TxtSifre.Margin = new Padding(5, 4, 5, 4);
            TxtSifre.Name = "TxtSifre";
            TxtSifre.Size = new Size(216, 39);
            TxtSifre.TabIndex = 1;
            TxtSifre.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(127, 60);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(93, 32);
            label3.TabIndex = 10;
            label3.Text = "Şifre : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(14, 12);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(200, 32);
            label2.TabIndex = 9;
            label2.Text = "TC Kimlik No :";
            // 
            // BtnHomePage
            // 
            BtnHomePage.BackgroundImage = (Image)resources.GetObject("BtnHomePage.BackgroundImage");
            BtnHomePage.BackgroundImageLayout = ImageLayout.Stretch;
            BtnHomePage.FlatAppearance.BorderSize = 0;
            BtnHomePage.FlatStyle = FlatStyle.Flat;
            BtnHomePage.Location = new Point(612, 4);
            BtnHomePage.Name = "BtnHomePage";
            BtnHomePage.Size = new Size(103, 89);
            BtnHomePage.TabIndex = 14;
            BtnHomePage.UseVisualStyleBackColor = true;
            BtnHomePage.Click += BtnHomePage_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBox1.ForeColor = Color.Black;
            checkBox1.Location = new Point(454, 66);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(152, 27);
            checkBox1.TabIndex = 15;
            checkBox1.Text = "Şifreyi Göster";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // SifremiUnuttum
            // 
            SifremiUnuttum.AutoSize = true;
            SifremiUnuttum.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            SifremiUnuttum.LinkColor = Color.DarkRed;
            SifremiUnuttum.Location = new Point(155, 114);
            SifremiUnuttum.Name = "SifremiUnuttum";
            SifremiUnuttum.Size = new Size(201, 33);
            SifremiUnuttum.TabIndex = 26;
            SifremiUnuttum.TabStop = true;
            SifremiUnuttum.Text = "Şifremi Unuttum";
            SifremiUnuttum.LinkClicked += SifremiUnuttum_LinkClicked;
            // 
            // FormDoktorGiris
            // 
            AcceptButton = BtnLogIn;
            AutoScaleDimensions = new SizeF(14F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(726, 210);
            Controls.Add(SifremiUnuttum);
            Controls.Add(checkBox1);
            Controls.Add(BtnHomePage);
            Controls.Add(MskTxtTC);
            Controls.Add(BtnLogIn);
            Controls.Add(TxtSifre);
            Controls.Add(label3);
            Controls.Add(label2);
            Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            MaximizeBox = false;
            Name = "FormDoktorGiris";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Doktor Giriş";
            FormClosing += FormDoktorGiris_FormClosing;
            Load += FormDoktorGiris_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaskedTextBox MskTxtTC;
        private Button BtnLogIn;
        private TextBox TxtSifre;
        private Label label3;
        private Label label2;
        private Button BtnHomePage;
        private CheckBox checkBox1;
        private LinkLabel SifremiUnuttum;
    }
}