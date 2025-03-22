namespace Fumoblilite.Interface.UserControls
{
    partial class UCConsultationLigne
    {
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Label lblLigne;
        private System.Windows.Forms.ComboBox cboLigne;
        private System.Windows.Forms.Button btnAfficher;
        private System.Windows.Forms.TabControl tabDetails;
        private System.Windows.Forms.TabPage tabArrets;
        private System.Windows.Forms.DataGridView dgvArrets;
        private System.Windows.Forms.TabPage tabHoraires;
        private System.Windows.Forms.Label lblJour;
        private System.Windows.Forms.ComboBox cboJour;
        private System.Windows.Forms.DataGridView dgvHoraires;

        private void InitializeComponent()
        {
            this.lblTitre = new System.Windows.Forms.Label();
            this.lblLigne = new System.Windows.Forms.Label();
            this.cboLigne = new System.Windows.Forms.ComboBox();
            this.btnAfficher = new System.Windows.Forms.Button();
            this.tabDetails = new System.Windows.Forms.TabControl();
            this.tabArrets = new System.Windows.Forms.TabPage();
            this.dgvArrets = new System.Windows.Forms.DataGridView();
            this.tabHoraires = new System.Windows.Forms.TabPage();
            this.lblJour = new System.Windows.Forms.Label();
            this.cboJour = new System.Windows.Forms.ComboBox();
            this.dgvHoraires = new System.Windows.Forms.DataGridView();
            this.tabDetails.SuspendLayout();
            this.tabArrets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArrets)).BeginInit();
            this.tabHoraires.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoraires)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(15, 15);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(167, 24);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Détails d'une ligne";
            // 
            // lblLigne
            // 
            this.lblLigne.AutoSize = true;
            this.lblLigne.Location = new System.Drawing.Point(15, 50);
            this.lblLigne.Name = "lblLigne";
            this.lblLigne.Size = new System.Drawing.Size(39, 13);
            this.lblLigne.TabIndex = 1;
            this.lblLigne.Text = "Ligne :";
            // 
            // cboLigne
            // 
            this.cboLigne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLigne.FormattingEnabled = true;
            this.cboLigne.Location = new System.Drawing.Point(60, 47);
            this.cboLigne.Name = "cboLigne";
            this.cboLigne.Size = new System.Drawing.Size(250, 21);
            this.cboLigne.TabIndex = 2;
            // 
            // btnAfficher
            // 
            this.btnAfficher.Location = new System.Drawing.Point(320, 45);
            this.btnAfficher.Name = "btnAfficher";
            this.btnAfficher.Size = new System.Drawing.Size(100, 25);
            this.btnAfficher.TabIndex = 3;
            this.btnAfficher.Text = "Afficher";
            this.btnAfficher.UseVisualStyleBackColor = true;
            this.btnAfficher.Click += new System.EventHandler(this.btnAfficher_Click);
            // 
            // tabDetails
            // 
            this.tabDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabDetails.Controls.Add(this.tabArrets);
            this.tabDetails.Controls.Add(this.tabHoraires);
            this.tabDetails.Location = new System.Drawing.Point(15, 80);
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.SelectedIndex = 0;
            this.tabDetails.Size = new System.Drawing.Size(770, 355);
            this.tabDetails.TabIndex = 4;
            // 
            // tabArrets
            // 
            this.tabArrets.Controls.Add(this.dgvArrets);
            this.tabArrets.Location = new System.Drawing.Point(4, 22);
            this.tabArrets.Name = "tabArrets";
            this.tabArrets.Padding = new System.Windows.Forms.Padding(3);
            this.tabArrets.Size = new System.Drawing.Size(762, 329);
            this.tabArrets.TabIndex = 0;
            this.tabArrets.Text = "Arrêts";
            this.tabArrets.UseVisualStyleBackColor = true;
            // 
            // dgvArrets
            // 
            this.dgvArrets.AllowUserToAddRows = false;
            this.dgvArrets.AllowUserToDeleteRows = false;
            this.dgvArrets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvArrets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvArrets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArrets.Location = new System.Drawing.Point(6, 6);
            this.dgvArrets.Name = "dgvArrets";
            this.dgvArrets.ReadOnly = true;
            this.dgvArrets.Size = new System.Drawing.Size(750, 317);
            this.dgvArrets.TabIndex = 0;
            // 
            // tabHoraires
            // 
            this.tabHoraires.Controls.Add(this.dgvHoraires);
            this.tabHoraires.Controls.Add(this.cboJour);
            this.tabHoraires.Controls.Add(this.lblJour);
            this.tabHoraires.Location = new System.Drawing.Point(4, 22);
            this.tabHoraires.Name = "tabHoraires";
            this.tabHoraires.Padding = new System.Windows.Forms.Padding(3);
            this.tabHoraires.Size = new System.Drawing.Size(762, 329);
            this.tabHoraires.TabIndex = 1;
            this.tabHoraires.Text = "Horaires";
            this.tabHoraires.UseVisualStyleBackColor = true;
            // 
            // lblJour
            // 
            this.lblJour.AutoSize = true;
            this.lblJour.Location = new System.Drawing.Point(10, 15);
            this.lblJour.Name = "lblJour";
            this.lblJour.Size = new System.Drawing.Size(33, 13);
            this.lblJour.TabIndex = 0;
            this.lblJour.Text = "Jour :";
            // 
            // cboJour
            // 
            this.cboJour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJour.FormattingEnabled = true;
            this.cboJour.Location = new System.Drawing.Point(50, 12);
            this.cboJour.Name = "cboJour";
            this.cboJour.Size = new System.Drawing.Size(200, 21);
            this.cboJour.TabIndex = 1;
            this.cboJour.SelectedIndexChanged += new System.EventHandler(this.cboJour_SelectedIndexChanged);
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
            this.dgvHoraires.Location = new System.Drawing.Point(6, 45);
            this.dgvHoraires.Name = "dgvHoraires";
            this.dgvHoraires.ReadOnly = true;
            this.dgvHoraires.Size = new System.Drawing.Size(750, 278);
            this.dgvHoraires.TabIndex = 2;
            // 
            // UCConsultationLigne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabDetails);
            this.Controls.Add(this.btnAfficher);
            this.Controls.Add(this.cboLigne);
            this.Controls.Add(this.lblLigne);
            this.Controls.Add(this.lblTitre);
            this.Name = "UCConsultationLigne";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.UCConsultationLigne_Load);
            this.tabDetails.ResumeLayout(false);
            this.tabArrets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArrets)).EndInit();
            this.tabHoraires.ResumeLayout(false);
            this.tabHoraires.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoraires)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
