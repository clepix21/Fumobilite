namespace Fumoblilite.Interface.UserControls
{
    partial class UCConsultationReseau
    {
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Panel pnlReseau;
        private System.Windows.Forms.CheckedListBox lstLignes;
        private System.Windows.Forms.Label lblLignes;

        private void InitializeComponent()
        {
            this.lblTitre = new System.Windows.Forms.Label();
            this.pnlReseau = new System.Windows.Forms.Panel();
            this.lstLignes = new System.Windows.Forms.CheckedListBox();
            this.lblLignes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(15, 15);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(156, 24);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Réseau complet";
            // 
            // pnlReseau
            // 
            this.pnlReseau.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlReseau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlReseau.Location = new System.Drawing.Point(180, 50);
            this.pnlReseau.Name = "pnlReseau";
            this.pnlReseau.Size = new System.Drawing.Size(605, 385);
            this.pnlReseau.TabIndex = 1;
            this.pnlReseau.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlReseau_Paint);
            // 
            // lstLignes
            // 
            this.lstLignes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstLignes.FormattingEnabled = true;
            this.lstLignes.Location = new System.Drawing.Point(15, 70);
            this.lstLignes.Name = "lstLignes";
            this.lstLignes.Size = new System.Drawing.Size(150, 364);
            this.lstLignes.TabIndex = 2;
            this.lstLignes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstLignes_ItemCheck);
            // 
            // lblLignes
            // 
            this.lblLignes.AutoSize = true;
            this.lblLignes.Location = new System.Drawing.Point(15, 50);
            this.lblLignes.Name = "lblLignes";
            this.lblLignes.Size = new System.Drawing.Size(43, 13);
            this.lblLignes.TabIndex = 3;
            this.lblLignes.Text = "Lignes :";
            // 
            // UCConsultationReseau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblLignes);
            this.Controls.Add(this.lstLignes);
            this.Controls.Add(this.pnlReseau);
            this.Controls.Add(this.lblTitre);
            this.Name = "UCConsultationReseau";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.UCConsultationReseau_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
