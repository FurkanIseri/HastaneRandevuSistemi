namespace HastaneKayitSistemi
{
    partial class FormGirisler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGirisler));
            BtnDoktor = new Button();
            BtnSekreter = new Button();
            BtnHasta = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            pictureBox1 = new PictureBox();
            BtnCikis = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // BtnDoktor
            // 
            BtnDoktor.BackColor = Color.Honeydew;
            BtnDoktor.BackgroundImage = (Image)resources.GetObject("BtnDoktor.BackgroundImage");
            BtnDoktor.BackgroundImageLayout = ImageLayout.Stretch;
            BtnDoktor.Location = new Point(12, 178);
            BtnDoktor.Name = "BtnDoktor";
            BtnDoktor.Size = new Size(257, 190);
            BtnDoktor.TabIndex = 0;
            BtnDoktor.UseVisualStyleBackColor = false;
            BtnDoktor.Click += BtnDoktor_Click;
            // 
            // BtnSekreter
            // 
            BtnSekreter.BackColor = Color.White;
            BtnSekreter.BackgroundImage = (Image)resources.GetObject("BtnSekreter.BackgroundImage");
            BtnSekreter.BackgroundImageLayout = ImageLayout.Stretch;
            BtnSekreter.Location = new Point(319, 178);
            BtnSekreter.Name = "BtnSekreter";
            BtnSekreter.Size = new Size(257, 190);
            BtnSekreter.TabIndex = 1;
            BtnSekreter.UseVisualStyleBackColor = false;
            BtnSekreter.Click += BtnSekreter_Click;
            // 
            // BtnHasta
            // 
            BtnHasta.BackColor = Color.Honeydew;
            BtnHasta.BackgroundImage = (Image)resources.GetObject("BtnHasta.BackgroundImage");
            BtnHasta.BackgroundImageLayout = ImageLayout.Stretch;
            BtnHasta.Location = new Point(616, 178);
            BtnHasta.Name = "BtnHasta";
            BtnHasta.Size = new Size(257, 190);
            BtnHasta.TabIndex = 2;
            BtnHasta.UseVisualStyleBackColor = false;
            BtnHasta.Click += BtnHasta_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(55, 385);
            label1.Name = "label1";
            label1.Size = new Size(174, 35);
            label1.TabIndex = 3;
            label1.Text = "Doktor Giriş";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(350, 385);
            label2.Name = "label2";
            label2.Size = new Size(189, 35);
            label2.TabIndex = 4;
            label2.Text = "Sekreter Giriş";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(667, 385);
            label3.Name = "label3";
            label3.Size = new Size(159, 35);
            label3.TabIndex = 5;
            label3.Text = "Hasta Giriş";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Unispace", 24F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.Desktop;
            label4.Location = new Point(4, 69);
            label4.Name = "label4";
            label4.Size = new Size(572, 48);
            label4.TabIndex = 6;
            label4.Text = "Hastane Randevu Sistemi";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(568, 18);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(177, 148);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // BtnCikis
            // 
            BtnCikis.BackColor = Color.SteelBlue;
            BtnCikis.BackgroundImage = (Image)resources.GetObject("BtnCikis.BackgroundImage");
            BtnCikis.BackgroundImageLayout = ImageLayout.Stretch;
            BtnCikis.FlatAppearance.BorderSize = 0;
            BtnCikis.FlatStyle = FlatStyle.Flat;
            BtnCikis.Location = new Point(751, 18);
            BtnCikis.Name = "BtnCikis";
            BtnCikis.Size = new Size(129, 99);
            BtnCikis.TabIndex = 8;
            BtnCikis.UseVisualStyleBackColor = false;
            BtnCikis.Click += BtnCikis_Click;
            // 
            // FormGirisler
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(901, 445);
            Controls.Add(BtnCikis);
            Controls.Add(pictureBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(BtnHasta);
            Controls.Add(BtnSekreter);
            Controls.Add(BtnDoktor);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FormGirisler";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hastane Randevu Sistemi";
            FormClosing += FormGirisler_FormClosing;
            Load += FormGirisler_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnDoktor;
        private Button BtnSekreter;
        private Button BtnHasta;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private PictureBox pictureBox1;
        private Button BtnCikis;
    }
}