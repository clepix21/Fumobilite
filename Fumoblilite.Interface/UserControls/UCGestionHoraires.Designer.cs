using System.Windows.Forms;

namespace Fumoblilite.Interface.UserControls
{
    partial class UCGestionHoraires
    {
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.GroupBox grpFiltres;
        private System.Windows.Forms.Label lblLigne;
        private System.Windows.Forms.ComboBox cboLigne;
        private System.Windows.Forms.Label lblJour;
        private System.Windows.Forms.ComboBox cboJour;
        private System.Windows.Forms.Button btnAfficher;
        private System.Windows.Forms.DataGridView dgvHoraires;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblLigneDetail;
        private System.Windows.Forms.ComboBox cboLigneDetail;
        private System.Windows.Forms.Label lblArret;
        private System.Windows.Forms.ComboBox cboArret;
        private System.Windows.Forms.Label lblJourDetail;
        private System.Windows.Forms.ComboBox cboJourDetail;
        private System.Windows.Forms.Label lblHeure;
        private System.Windows.Forms.DateTimePicker dtpHeure;
        private System.Windows.Forms.CheckBox chkEstActif;
        private System.Windows.Forms.Button btnNouveau;
        private System.Windows.Forms.Button btnEnregistrer;
        private System.Windows.Forms.Button btnSupprimer;

        private void InitializeComponent()
        {
            this.lblTitre = new System.Windows.Forms.Label();
            this.grpFiltres = new System.Windows.Forms.GroupBox();
            this.btnAfficher = new System.Windows.Forms.Button();
            this.cboJour = new System.Windows.Forms.ComboBox();
            this.lblJour = new System.Windows.Forms.Label();
            this.cboLigne = new System.Windows.Forms.ComboBox();
            this.lblLigne = new System.Windows.Forms.Label();
            this.dgvHoraires = new System.Windows.Forms.DataGridView();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.chkEstActif = new System.Windows.Forms.CheckBox();
            this.dtpHeure = new System.Windows.Forms.DateTimePicker();
            this.lblHeure = new System.Windows.Forms.Label();
            this.cboJourDetail = new System.Windows.Forms.ComboBox();
            this.lblJourDetail = new System.Windows.Forms.Label();
            this.cboArret = new System.Windows.Forms.ComboBox();
            this.lblArret = new System.Windows.Forms.Label();
            this.cboLigneDetail = new System.Windows.Forms.ComboBox();
            this.lblLigneDetail = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.btnNouveau = new System.Windows.Forms.Button();
            this.btnEnregistrer = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.grpFiltres.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoraires)).BeginInit();
            this.grpDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(15, 15);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(186, 24);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Gestion des horaires";
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
            this.grpFiltres.Size = new System.Drawing.Size(770, 70);
            this.grpFiltres.TabIndex = 1;
            this.grpFiltres.TabStop = false;
            this.grpFiltres.Text = "Filtres";
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
            // cboJour
            // 
            this.cboJour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJour.FormattingEnabled = true;
            this.cboJour.Location = new System.Drawing.Point(350, 27);
            this.cboJour.Name = "cboJour";
            this.cboJour.Size = new System.Drawing.Size(200, 21);
            this.cboJour.TabIndex = 3;
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
            this.dgvHoraires.Location = new System.Drawing.Point(15, 130);
            this.dgvHoraires.MultiSelect = false;
            this.dgvHoraires.Name = "dgvHoraires";
            this.dgvHoraires.ReadOnly = true;
            this.dgvHoraires.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoraires.Size = new System.Drawing.Size(450, 305);
            this.dgvHoraires.TabIndex = 2;
            this.dgvHoraires.SelectionChanged += new System.EventHandler(this.dgvHoraires_SelectionChanged);
            // 
            // grpDetails
            // 
            this.grpDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDetails.Controls.Add(this.chkEstActif);
            this.grpDetails.Controls.Add(this.dtpHeure);
            this.grpDetails.Controls.Add(this.lblHeure);
            this.grpDetails.Controls.Add(this.cboJourDetail);
            this.grpDetails.Controls.Add(this.lblJourDetail);
            this.grpDetails.Controls.Add(this.cboArret);
            this.grpDetails.Controls.Add(this.lblArret);
            this.grpDetails.Controls.Add(this.cboLigneDetail);
            this.grpDetails.Controls.Add(this.lblLigneDetail);
            this.grpDetails.Controls.Add(this.txtId);
            this.grpDetails.Controls.Add(this.lblId);
            this.grpDetails.Location = new System.Drawing.Point(480, 130);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(305, 230);
            this.grpDetails.TabIndex = 3;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Détails de l\'horaire";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(20, 30);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(19, 13);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Id:";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(120, 27);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(160, 20);
            this.txtId.TabIndex = 1;
            // 
            // lblLigneDetail
            // 
            this.lblLigneDetail.AutoSize = true;
            this.lblLigneDetail.Location = new System.Drawing.Point(20, 60);
            this.lblLigneDetail.Name = "lblLigneDetail";
            this.lblLigneDetail.Size = new System.Drawing.Size(39, 13);
            this.lblLigneDetail.TabIndex = 2;
            this.lblLigneDetail.Text = "Ligne :";
            // 
            // cboLigneDetail
            // 
            this.cboLigneDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLigneDetail.FormattingEnabled = true;
            this.cboLigneDetail.Location = new System.Drawing.Point(120, 57);
            this.cboLigneDetail.Name = "cboLigneDetail";
            this.cboLigneDetail.Size = new System.Drawing.Size(160, 21);
            this.cboLigneDetail.TabIndex = 3;
            this.cboLigneDetail.SelectedIndexChanged += new System.EventHandler(this.cboLigneDetail_SelectedIndexChanged);
            // 
            // lblArret
            // 
            this.lblArret.AutoSize = true;
            this.lblArret.Location = new System.Drawing.Point(20, 90);
            this.lblArret.Name = "lblArret";
            this.lblArret.Size = new System.Drawing.Size(35, 13);
            this.lblArret.TabIndex = 4;
            this.lblArret.Text = "Arrêt :";
            // 
            // cboArret
            // 
            this.cboArret.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArret.FormattingEnabled = true;
            this.cboArret.Location = new System.Drawing.Point(120, 87);
            this.cboArret.Name = "cboArret";
            this.cboArret.Size = new System.Drawing.Size(160, 21);
            this.cboArret.TabIndex = 5;
            // 
            // lblJourDetail
            // 
            this.lblJourDetail.AutoSize = true;
            this.lblJourDetail.Location = new System.Drawing.Point(20, 120);
            this.lblJourDetail.Name = "lblJourDetail";
            this.lblJourDetail.Size = new System.Drawing.Size(33, 13);
            this.lblJourDetail.TabIndex = 6;
            this.lblJourDetail.Text = "Jour :";
            // 
            // cboJourDetail
            // 
            this.cboJourDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJourDetail.FormattingEnabled = true;
            this.cboJourDetail.Location = new System.Drawing.Point(120, 117);
            this.cboJourDetail.Name = "cboJourDetail";
            this.cboJourDetail.Size = new System.Drawing.Size(160, 21);
            this.cboJourDetail.TabIndex = 7;
            // 
            // lblHeure
            // 
            this.lblHeure.AutoSize = true;
            this.lblHeure.Location = new System.Drawing.Point(20, 150);
            this.lblHeure.Name = "lblHeure";
            this.lblHeure.Size = new System.Drawing.Size(42, 13);
            this.lblHeure.TabIndex = 8;
            this.lblHeure.Text = "Heure :";
            // 
            // dtpHeure
            // 
            this.dtpHeure.CustomFormat = "HH:mm";
            this.dtpHeure.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHeure.Location = new System.Drawing.Point(120, 147);
            this.dtpHeure.Name = "dtpHeure";
            this.dtpHeure.ShowUpDown = true;
            this.dtpHeure.Size = new System.Drawing.Size(160, 20);
            this.dtpHeure.TabIndex = 9;
            // 
            // chkEstActif
            // 
            this.chkEstActif.AutoSize = true;
            this.chkEstActif.Location = new System.Drawing.Point(120, 180);
            this.chkEstActif.Name = "chkEstActif";
            this.chkEstActif.Size = new System.Drawing.Size(64, 17);
            this.chkEstActif.TabIndex = 10;
            this.chkEstActif.Text = "Est actif";
            this.chkEstActif.UseVisualStyleBackColor = true;
            // 
            // btnNouveau
            // 
            this.btnNouveau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNouveau.Location = new System.Drawing.Point(480, 370);
            this.btnNouveau.Name = "btnNouveau";
            this.btnNouveau.Size = new System.Drawing.Size(90, 30);
            this.btnNouveau.TabIndex = 4;
            this.btnNouveau.Text = "Nouveau";
            this.btnNouveau.UseVisualStyleBackColor = true;
            this.btnNouveau.Click += new System.EventHandler(this.btnNouveau_Click);
            // 
            // btnEnregistrer
            // 
            this.btnEnregistrer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnregistrer.Location = new System.Drawing.Point(580, 370);
            this.btnEnregistrer.Name = "btnEnregistrer";
            this.btnEnregistrer.Size = new System.Drawing.Size(90, 30);
            this.btnEnregistrer.TabIndex = 5;
            this.btnEnregistrer.Text = "Enregistrer";
            this.btnEnregistrer.UseVisualStyleBackColor = true;
            this.btnEnregistrer.Click += new System.EventHandler(this.btnEnregistrer_Click);
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSupprimer.Location = new System.Drawing.Point(680, 370);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(90, 30);
            this.btnSupprimer.TabIndex = 6;
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = true;
            this.btnSupprimer.Click += new System.EventHandler(this.btnSupprimer_Click);
            // 
            // UCGestionHoraires
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.btnEnregistrer);
            this.Controls.Add(this.btnNouveau);
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.dgvHoraires);
            this.Controls.Add(this.grpFiltres);
            this.Controls.Add(this.lblTitre);
            this.Name = "UCGestionHoraires";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.UCGestionHoraires_Load);
            this.grpFiltres.ResumeLayout(false);
            this.grpFiltres.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoraires)).EndInit();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
