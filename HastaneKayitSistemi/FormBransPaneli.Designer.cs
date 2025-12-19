namespace HastaneKayitSistemi
{
    partial class FormBransPaneli
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBransPaneli));
            BtnGuncelle = new Button();
            BtnSil = new Button();
            BtnEkle = new Button();
            dataGridView1 = new DataGridView();
            TxtBrans = new TextBox();
            TxtBransAd = new Label();
            label1 = new Label();
            LblID = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // BtnGuncelle
            // 
            BtnGuncelle.BackColor = Color.PaleTurquoise;
            BtnGuncelle.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            BtnGuncelle.Location = new Point(52, 150);
            BtnGuncelle.Margin = new Padding(5, 4, 5, 4);
            BtnGuncelle.Name = "BtnGuncelle";
            BtnGuncelle.Size = new Size(164, 36);
            BtnGuncelle.TabIndex = 27;
            BtnGuncelle.Text = "Güncelle";
            BtnGuncelle.UseVisualStyleBackColor = false;
            BtnGuncelle.Click += BtnGuncelle_Click;
            // 
            // BtnSil
            // 
            BtnSil.BackColor = Color.Red;
            BtnSil.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            BtnSil.Location = new Point(188, 106);
            BtnSil.Margin = new Padding(5, 4, 5, 4);
            BtnSil.Name = "BtnSil";
            BtnSil.Size = new Size(164, 36);
            BtnSil.TabIndex = 26;
            BtnSil.Text = "Sil";
            BtnSil.UseVisualStyleBackColor = false;
            BtnSil.Click += BtnSil_Click;
            // 
            // BtnEkle
            // 
            BtnEkle.BackColor = Color.FromArgb(192, 192, 0);
            BtnEkle.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            BtnEkle.Location = new Point(14, 106);
            BtnEkle.Margin = new Padding(5, 4, 5, 4);
            BtnEkle.Name = "BtnEkle";
            BtnEkle.Size = new Size(164, 36);
            BtnEkle.TabIndex = 25;
            BtnEkle.Text = "Ekle";
            BtnEkle.UseVisualStyleBackColor = false;
            BtnEkle.Click += BtnEkle_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(362, 5);
            dataGridView1.Margin = new Padding(5, 4, 5, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(591, 174);
            dataGridView1.TabIndex = 24;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // TxtBrans
            // 
            TxtBrans.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            TxtBrans.Location = new Point(178, 50);
            TxtBrans.Margin = new Padding(5, 4, 5, 4);
            TxtBrans.Name = "TxtBrans";
            TxtBrans.Size = new Size(164, 34);
            TxtBrans.TabIndex = 20;
            // 
            // TxtBransAd
            // 
            TxtBransAd.AutoSize = true;
            TxtBransAd.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            TxtBransAd.Location = new Point(42, 53);
            TxtBransAd.Margin = new Padding(5, 0, 5, 0);
            TxtBransAd.Name = "TxtBransAd";
            TxtBransAd.Size = new Size(126, 25);
            TxtBransAd.TabIndex = 15;
            TxtBransAd.Text = "Branş Ad : ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(44, 9);
            label1.Name = "label1";
            label1.Size = new Size(124, 25);
            label1.TabIndex = 29;
            label1.Text = "Branş ID : ";
            // 
            // LblID
            // 
            LblID.AutoSize = true;
            LblID.ForeColor = Color.Cornsilk;
            LblID.Location = new Point(178, 9);
            LblID.Name = "LblID";
            LblID.Size = new Size(23, 26);
            LblID.TabIndex = 30;
            LblID.Text = "0";
            // 
            // FormBransPaneli
            // 
            AutoScaleDimensions = new SizeF(13F, 26F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(971, 193);
            Controls.Add(LblID);
            Controls.Add(label1);
            Controls.Add(BtnGuncelle);
            Controls.Add(BtnSil);
            Controls.Add(BtnEkle);
            Controls.Add(dataGridView1);
            Controls.Add(TxtBrans);
            Controls.Add(TxtBransAd);
            Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 4, 5, 4);
            MaximizeBox = false;
            Name = "FormBransPaneli";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Branş Paneli";
            Load += FormBransPaneli_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnGuncelle;
        private Button BtnSil;
        private Button BtnEkle;
        private DataGridView dataGridView1;
        private TextBox TxtBrans;
        private Label TxtBransAd;
        private Label label1;
        private Label LblID;
    }
}