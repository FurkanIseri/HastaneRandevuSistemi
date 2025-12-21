namespace HastaneRandevuSistemi
{
    partial class FormSekreterGiris
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSekreterGiris));
            BtnLogIn = new Button();
            MskTxtTC = new MaskedTextBox();
            TxtSifre = new TextBox();
            label3 = new Label();
            label2 = new Label();
            BtnHomePage = new Button();
            checkBox1 = new CheckBox();
            SifremiUnuttum = new LinkLabel();
            SuspendLayout();
            // 
            // BtnLogIn
            // 
            BtnLogIn.BackColor = Color.PaleTurquoise;
            BtnLogIn.Location = new Point(195, 149);
            BtnLogIn.Margin = new Padding(6, 5, 6, 5);
            BtnLogIn.Name = "BtnLogIn";
            BtnLogIn.Size = new Size(186, 58);
            BtnLogIn.TabIndex = 2;
            BtnLogIn.Text = "Giriş Yap";
            BtnLogIn.UseVisualStyleBackColor = false;
            BtnLogIn.Click += BtnLogIn_Click;
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
            // TxtSifre
            // 
            TxtSifre.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            TxtSifre.Location = new Point(227, 57);
            TxtSifre.Name = "TxtSifre";
            TxtSifre.Size = new Size(196, 39);
            TxtSifre.TabIndex = 1;
            TxtSifre.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(126, 60);
            label3.Name = "label3";
            label3.Size = new Size(86, 32);
            label3.TabIndex = 20;
            label3.Text = "Şifre :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(12, 12);
            label2.Name = "label2";
            label2.Size = new Size(200, 32);
            label2.TabIndex = 19;
            label2.Text = "TC Kimlik No :";
            // 
            // BtnHomePage
            // 
            BtnHomePage.BackgroundImage = (Image)resources.GetObject("BtnHomePage.BackgroundImage");
            BtnHomePage.BackgroundImageLayout = ImageLayout.Stretch;
            BtnHomePage.FlatAppearance.BorderSize = 0;
            BtnHomePage.FlatStyle = FlatStyle.Flat;
            BtnHomePage.Location = new Point(614, 12);
            BtnHomePage.Name = "BtnHomePage";
            BtnHomePage.Size = new Size(103, 89);
            BtnHomePage.TabIndex = 23;
            BtnHomePage.UseVisualStyleBackColor = true;
            BtnHomePage.Click += BtnHomePage_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBox1.ForeColor = Color.Black;
            checkBox1.Location = new Point(442, 66);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(152, 27);
            checkBox1.TabIndex = 24;
            checkBox1.Text = "Şifreyi Göster";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // SifremiUnuttum
            // 
            SifremiUnuttum.AutoSize = true;
            SifremiUnuttum.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            SifremiUnuttum.LinkColor = Color.DarkRed;
            SifremiUnuttum.Location = new Point(180, 111);
            SifremiUnuttum.Name = "SifremiUnuttum";
            SifremiUnuttum.Size = new Size(201, 33);
            SifremiUnuttum.TabIndex = 25;
            SifremiUnuttum.TabStop = true;
            SifremiUnuttum.Text = "Şifremi Unuttum";
            SifremiUnuttum.LinkClicked += SifremiUnuttum_LinkClicked;
            // 
            // FormSekreterGiris
            // 
            AcceptButton = BtnLogIn;
            AutoScaleDimensions = new SizeF(16F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.DarkCyan;
            ClientSize = new Size(724, 210);
            Controls.Add(SifremiUnuttum);
            Controls.Add(checkBox1);
            Controls.Add(BtnHomePage);
            Controls.Add(MskTxtTC);
            Controls.Add(TxtSifre);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(BtnLogIn);
            Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6, 5, 6, 5);
            MaximizeBox = false;
            Name = "FormSekreterGiris";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sekreter Giriş";
            FormClosing += FormSekreterGiris_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button BtnLogIn;
        private MaskedTextBox MskTxtTC;
        private TextBox TxtSifre;
        private Label label3;
        private Label label2;
        private Button BtnHomePage;
        private CheckBox checkBox1;
        private LinkLabel SifremiUnuttum;
    }
}