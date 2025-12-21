namespace HastaneRandevuSistemi
{
    partial class IlacDetay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IlacDetay));
            label1 = new Label();
            label2 = new Label();
            TxtIlacAd = new TextBox();
            TxtAdet = new TextBox();
            Temizle = new LinkLabel();
            BtnGuncelle = new Button();
            button2 = new Button();
            BtnEkle = new Button();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 27);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(108, 25);
            label1.TabIndex = 0;
            label1.Text = "İlaç Adı : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(42, 66);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(80, 25);
            label2.TabIndex = 1;
            label2.Text = "Adet : ";
            // 
            // TxtIlacAd
            // 
            TxtIlacAd.Location = new Point(117, 24);
            TxtIlacAd.Name = "TxtIlacAd";
            TxtIlacAd.Size = new Size(188, 34);
            TxtIlacAd.TabIndex = 2;
            // 
            // TxtAdet
            // 
            TxtAdet.Location = new Point(117, 63);
            TxtAdet.Name = "TxtAdet";
            TxtAdet.Size = new Size(54, 34);
            TxtAdet.TabIndex = 3;
            // 
            // Temizle
            // 
            Temizle.AutoSize = true;
            Temizle.LinkColor = Color.DarkRed;
            Temizle.Location = new Point(265, 189);
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
            BtnGuncelle.Location = new Point(58, 172);
            BtnGuncelle.Margin = new Padding(5, 4, 5, 4);
            BtnGuncelle.Name = "BtnGuncelle";
            BtnGuncelle.Size = new Size(169, 59);
            BtnGuncelle.TabIndex = 32;
            BtnGuncelle.Text = "Güncelle";
            BtnGuncelle.UseVisualStyleBackColor = false;
            BtnGuncelle.Click += BtnGuncelle_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Red;
            button2.Location = new Point(204, 104);
            button2.Margin = new Padding(5, 4, 5, 4);
            button2.Name = "button2";
            button2.Size = new Size(171, 60);
            button2.TabIndex = 31;
            button2.Text = "Sil";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // BtnEkle
            // 
            BtnEkle.BackColor = Color.Lime;
            BtnEkle.Location = new Point(14, 104);
            BtnEkle.Margin = new Padding(5, 4, 5, 4);
            BtnEkle.Name = "BtnEkle";
            BtnEkle.Size = new Size(167, 60);
            BtnEkle.TabIndex = 30;
            BtnEkle.Text = "Ekle";
            BtnEkle.UseVisualStyleBackColor = false;
            BtnEkle.Click += BtnEkle_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(383, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(597, 214);
            dataGridView1.TabIndex = 34;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // IlacDetay
            // 
            AutoScaleDimensions = new SizeF(14F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkCyan;
            ClientSize = new Size(990, 248);
            Controls.Add(dataGridView1);
            Controls.Add(Temizle);
            Controls.Add(BtnGuncelle);
            Controls.Add(button2);
            Controls.Add(BtnEkle);
            Controls.Add(TxtAdet);
            Controls.Add(TxtIlacAd);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 3, 5, 3);
            MaximizeBox = false;
            Name = "IlacDetay";
            Text = "İlaç Detay";
            Load += IlacDetay_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox TxtIlacAd;
        private TextBox TxtAdet;
        private LinkLabel Temizle;
        private Button BtnGuncelle;
        private Button button2;
        private Button BtnEkle;
        private DataGridView dataGridView1;
    }
}