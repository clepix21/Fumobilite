namespace Fumoblilite.Interface.UserControls
{
    partial class UCConsultationHoraires
    {
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.GroupBox grpFiltres;
        private System.Windows.Forms.Label lblLigne;
        private System.Windows.Forms.ComboBox cboLigne;
        private System.Windows.Forms.Label lblJour;
        private System.Windows.Forms.ComboBox cboJour;
        private System.Windows.Forms.Button btnAfficher;
        private System.Windows.Forms.DataGridView dgvHoraires;

        private void InitializeComponent()
        {
            this.lblTitre = new System.Windows.Forms.Label();
            this.grpFiltres = new System.Windows.Forms.GroupBox();
            this.lblLigne = new System.Windows.Forms.Label();
            this.cboLigne = new System.Windows.Forms.ComboBox();
            this.lblJour = new System.Windows.Forms.Label();
            this.cboJour = new System.Windows.Forms.ComboBox();
            this.btnAfficher = new System.Windows.Forms.Button();
            this.dgvHoraires = new System.Windows.Forms.DataGridView();
            this.grpFiltres.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoraires)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(15, 15);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(169, 24);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Horaires du jour";
            // 
            // grpFiltres
            // 
            this.grpFiltres.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFiltres.Controls.Add(this.btnAfficher);
            this.grpFiltres.Controls.Add(this.cboJour);
            this.grpFiltres.Controls.Add(this.lblJour);
            this.grpFiltres.Controls.Add(this.cboLigne);
            this.grpFiltres.Controls.Add(this.lblLigne);
            this.grpFiltres.Location = new System.Drawing.Point(15, 50);
            this.grpFiltres.Name = "grpFiltres";
            this.grpFiltres.Size = new System.Drawing.Size(770, 100);
            this.grpFiltres.TabIndex = 1;
            this.grpFiltres.TabStop = false;
            this.grpFiltres.Text = "Filtres";
            // 
            // lblLigne
            // 
            this.lblLigne.AutoSize = true;
            this.lblLigne.Location = new System.Drawing.Point(20, 30);
            this.lblLigne.Name = "lblLigne";
            this.lblLigne.Size = new System.Drawing.Size(39, 13);
            this.lblLigne.TabIndex = 0;
            this.lblLigne.Text = "Ligne :";
            // 
            // cboLigne
            // 
            this.cboLigne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLigne.FormattingEnabled = true;
            this.cboLigne.Location = new System.Drawing.Point(70, 27);
            this.cboLigne.Name = "cboLigne";
            this.cboLigne.Size = new System.Drawing.Size(200, 21);
            this.cboLigne.TabIndex = 1;
            // 
            // lblJour
            // 
            this.lblJour.AutoSize = true;
            this.lblJour.Location = new System.Drawing.Point(300, 30);
            this.lblJour.Name = "lblJour";
            this.lblJour.Size = new System.Drawing.Size(33, 13);
            this.lblJour.TabIndex = 2;
            this.lblJour.Text = "Jour :";
            // 
            // cboJour
            // 
            this.cboJour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJour.FormattingEnabled = true;
            this.cboJour.Location = new System.Drawing.Point(350, 27);
            this.cboJour.Name = "cboJour";
            this.cboJour.Size = new System.Drawing.Size(200, 21);
            this.cboJour.TabIndex = 3;
            // 
            // btnAfficher
            // 
            this.btnAfficher.Location = new System.Drawing.Point(600, 25);
            this.btnAfficher.Name = "btnAfficher";
            this.btnAfficher.Size = new System.Drawing.Size(120, 25);
            this.btnAfficher.TabIndex = 4;
            this.btnAfficher.Text = "Afficher";
            this.btnAfficher.UseVisualStyleBackColor = true;
            this.btnAfficher.Click += new System.EventHandler(this.btnAfficher_Click);
            // 
            // dgvHoraires
            // 
            this.dgvHoraires.AllowUserToAddRows = false;
            this.dgvHoraires.AllowUserToDeleteRows = false;
            this.dgvHoraires.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHoraires.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoraires.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoraires.Location = new System.Drawing.Point(15, 160);
            this.dgvHoraires.Name = "dgvHoraires";
            this.dgvHoraires.ReadOnly = true;
            this.dgvHoraires.Size = new System.Drawing.Size(770, 275);
            this.dgvHoraires.TabIndex = 2;
            // 
            // UCConsultationHoraires
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvHoraires);
            this.Controls.Add(this.grpFiltres);
            this.Controls.Add(this.lblTitre);
            this.Name = "UCConsultationHoraires";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.UCConsultationHoraires_Load);
            this.grpFiltres.ResumeLayout(false);
            this.grpFiltres.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoraires)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
