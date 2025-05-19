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
        private System.Windows.Forms.TabPage tabHoraires;
        private System.Windows.Forms.Label lblJour;
        private System.Windows.Forms.ComboBox cboJour;
        private System.Windows.Forms.FlowLayoutPanel flpArrets;
        private System.Windows.Forms.FlowLayoutPanel flpHoraires;
        private System.Windows.Forms.Panel pnlHeaderArrets;
        private System.Windows.Forms.Label lblOrdre;
        private System.Windows.Forms.Label lblNomArret;
        private System.Windows.Forms.Label lblAdresse;
        private System.Windows.Forms.Label lblTempsArret;
        private System.Windows.Forms.Label lblTempsTrajet;
        private System.Windows.Forms.Label lblAccessible;
        private System.Windows.Forms.Panel pnlHeaderHoraires;
        private System.Windows.Forms.Label lblArret;
        private System.Windows.Forms.Label lblHeure;
        private System.Windows.Forms.Label lblActif;

        private void InitializeComponent()
        {
            this.lblTitre = new System.Windows.Forms.Label();
            this.lblLigne = new System.Windows.Forms.Label();
            this.cboLigne = new System.Windows.Forms.ComboBox();
            this.btnAfficher = new System.Windows.Forms.Button();
            this.tabDetails = new System.Windows.Forms.TabControl();
            this.tabArrets = new System.Windows.Forms.TabPage();
            this.flpArrets = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHeaderArrets = new System.Windows.Forms.Panel();
            this.lblOrdre = new System.Windows.Forms.Label();
            this.lblNomArret = new System.Windows.Forms.Label();
            this.lblAdresse = new System.Windows.Forms.Label();
            this.lblTempsArret = new System.Windows.Forms.Label();
            this.lblTempsTrajet = new System.Windows.Forms.Label();
            this.lblAccessible = new System.Windows.Forms.Label();
            this.tabHoraires = new System.Windows.Forms.TabPage();
            this.flpHoraires = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHeaderHoraires = new System.Windows.Forms.Panel();
            this.lblArret = new System.Windows.Forms.Label();
            this.lblHeure = new System.Windows.Forms.Label();
            this.lblActif = new System.Windows.Forms.Label();
            this.lblJour = new System.Windows.Forms.Label();
            this.cboJour = new System.Windows.Forms.ComboBox();
            this.tabDetails.SuspendLayout();
            this.tabArrets.SuspendLayout();
            this.pnlHeaderArrets.SuspendLayout();
            this.tabHoraires.SuspendLayout();
            this.pnlHeaderHoraires.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblTitre.Location = new System.Drawing.Point(15, 15);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(167, 30);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Détails d'une ligne";
            // 
            // lblLigne
            // 
            this.lblLigne.AutoSize = true;
            this.lblLigne.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLigne.Location = new System.Drawing.Point(15, 55);
            this.lblLigne.Name = "lblLigne";
            this.lblLigne.Size = new System.Drawing.Size(39, 15);
            this.lblLigne.TabIndex = 1;
            this.lblLigne.Text = "Ligne :";
            // 
            // cboLigne
            // 
            this.cboLigne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLigne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboLigne.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLigne.FormattingEnabled = true;
            this.cboLigne.Location = new System.Drawing.Point(60, 52);
            this.cboLigne.Name = "cboLigne";
            this.cboLigne.Size = new System.Drawing.Size(250, 23);
            this.cboLigne.TabIndex = 2;
            // 
            // btnAfficher
            // 
            this.btnAfficher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnAfficher.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAfficher.FlatAppearance.BorderSize = 0;
            this.btnAfficher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAfficher.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAfficher.ForeColor = System.Drawing.Color.White;
            this.btnAfficher.Location = new System.Drawing.Point(320, 50);
            this.btnAfficher.Name = "btnAfficher";
            this.btnAfficher.Size = new System.Drawing.Size(100, 28);
            this.btnAfficher.TabIndex = 3;
            this.btnAfficher.Text = "Afficher";
            this.btnAfficher.UseVisualStyleBackColor = false;
            this.btnAfficher.Click += new System.EventHandler(this.btnAfficher_Click);
            // 
            // tabDetails
            // 
            this.tabDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabDetails.Controls.Add(this.tabArrets);
            this.tabDetails.Controls.Add(this.tabHoraires);
            this.tabDetails.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabDetails.Location = new System.Drawing.Point(15, 90);
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.SelectedIndex = 0;
            this.tabDetails.Size = new System.Drawing.Size(770, 345);
            this.tabDetails.TabIndex = 4;
            // 
            // tabArrets
            // 
            this.tabArrets.Controls.Add(this.flpArrets);
            this.tabArrets.Controls.Add(this.pnlHeaderArrets);
            this.tabArrets.Location = new System.Drawing.Point(4, 24);
            this.tabArrets.Name = "tabArrets";
            this.tabArrets.Padding = new System.Windows.Forms.Padding(3);
            this.tabArrets.Size = new System.Drawing.Size(762, 317);
            this.tabArrets.TabIndex = 0;
            this.tabArrets.Text = "Arrêts";
            this.tabArrets.UseVisualStyleBackColor = true;
            // 
            // flpArrets
            // 
            this.flpArrets.AutoScroll = true;
            this.flpArrets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpArrets.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpArrets.Location = new System.Drawing.Point(3, 43);
            this.flpArrets.Name = "flpArrets";
            this.flpArrets.Padding = new System.Windows.Forms.Padding(5);
            this.flpArrets.Size = new System.Drawing.Size(756, 271);
            this.flpArrets.TabIndex = 1;
            this.flpArrets.WrapContents = false;
            // 
            // pnlHeaderArrets
            // 
            this.pnlHeaderArrets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.pnlHeaderArrets.Controls.Add(this.lblAccessible);
            this.pnlHeaderArrets.Controls.Add(this.lblTempsTrajet);
            this.pnlHeaderArrets.Controls.Add(this.lblTempsArret);
            this.pnlHeaderArrets.Controls.Add(this.lblAdresse);
            this.pnlHeaderArrets.Controls.Add(this.lblNomArret);
            this.pnlHeaderArrets.Controls.Add(this.lblOrdre);
            this.pnlHeaderArrets.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeaderArrets.Location = new System.Drawing.Point(3, 3);
            this.pnlHeaderArrets.Name = "pnlHeaderArrets";
            this.pnlHeaderArrets.Size = new System.Drawing.Size(756, 40);
            this.pnlHeaderArrets.TabIndex = 0;
            // 
            // lblOrdre
            // 
            this.lblOrdre.AutoSize = true;
            this.lblOrdre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrdre.ForeColor = System.Drawing.Color.White;
            this.lblOrdre.Location = new System.Drawing.Point(15, 13);
            this.lblOrdre.Name = "lblOrdre";
            this.lblOrdre.Size = new System.Drawing.Size(40, 15);
            this.lblOrdre.TabIndex = 0;
            this.lblOrdre.Text = "Ordre";
            // 
            // lblNomArret
            // 
            this.lblNomArret.AutoSize = true;
            this.lblNomArret.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomArret.ForeColor = System.Drawing.Color.White;
            this.lblNomArret.Location = new System.Drawing.Point(70, 13);
            this.lblNomArret.Name = "lblNomArret";
            this.lblNomArret.Size = new System.Drawing.Size(34, 15);
            this.lblNomArret.TabIndex = 1;
            this.lblNomArret.Text = "Nom";
            // 
            // lblAdresse
            // 
            this.lblAdresse.AutoSize = true;
            this.lblAdresse.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdresse.ForeColor = System.Drawing.Color.White;
            this.lblAdresse.Location = new System.Drawing.Point(200, 13);
            this.lblAdresse.Name = "lblAdresse";
            this.lblAdresse.Size = new System.Drawing.Size(52, 15);
            this.lblAdresse.TabIndex = 2;
            this.lblAdresse.Text = "Adresse";
            // 
            // lblTempsArret
            // 
            this.lblTempsArret.AutoSize = true;
            this.lblTempsArret.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTempsArret.ForeColor = System.Drawing.Color.White;
            this.lblTempsArret.Location = new System.Drawing.Point(400, 13);
            this.lblTempsArret.Name = "lblTempsArret";
            this.lblTempsArret.Size = new System.Drawing.Size(85, 15);
            this.lblTempsArret.TabIndex = 3;
            this.lblTempsArret.Text = "Temps d'arrêt";
            // 
            // lblTempsTrajet
            // 
            this.lblTempsTrajet.AutoSize = true;
            this.lblTempsTrajet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTempsTrajet.ForeColor = System.Drawing.Color.White;
            this.lblTempsTrajet.Location = new System.Drawing.Point(520, 13);
            this.lblTempsTrajet.Name = "lblTempsTrajet";
            this.lblTempsTrajet.Size = new System.Drawing.Size(80, 15);
            this.lblTempsTrajet.TabIndex = 4;
            this.lblTempsTrajet.Text = "Temps trajet";
            // 
            // lblAccessible
            // 
            this.lblAccessible.AutoSize = true;
            this.lblAccessible.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccessible.ForeColor = System.Drawing.Color.White;
            this.lblAccessible.Location = new System.Drawing.Point(650, 13);
            this.lblAccessible.Name = "lblAccessible";
            this.lblAccessible.Size = new System.Drawing.Size(64, 15);
            this.lblAccessible.TabIndex = 5;
            this.lblAccessible.Text = "Accessible";
            // 
            // tabHoraires
            // 
            this.tabHoraires.Controls.Add(this.flpHoraires);
            this.tabHoraires.Controls.Add(this.pnlHeaderHoraires);
            this.tabHoraires.Controls.Add(this.cboJour);
            this.tabHoraires.Controls.Add(this.lblJour);
            this.tabHoraires.Location = new System.Drawing.Point(4, 24);
            this.tabHoraires.Name = "tabHoraires";
            this.tabHoraires.Padding = new System.Windows.Forms.Padding(3);
            this.tabHoraires.Size = new System.Drawing.Size(762, 317);
            this.tabHoraires.TabIndex = 1;
            this.tabHoraires.Text = "Horaires";
            this.tabHoraires.UseVisualStyleBackColor = true;
            // 
            // flpHoraires
            // 
            this.flpHoraires.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpHoraires.AutoScroll = true;
            this.flpHoraires.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpHoraires.Location = new System.Drawing.Point(6, 83);
            this.flpHoraires.Name = "flpHoraires";
            this.flpHoraires.Padding = new System.Windows.Forms.Padding(5);
            this.flpHoraires.Size = new System.Drawing.Size(750, 228);
            this.flpHoraires.TabIndex = 3;
            this.flpHoraires.WrapContents = false;
            // 
            // pnlHeaderHoraires
            // 
            this.pnlHeaderHoraires.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeaderHoraires.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.pnlHeaderHoraires.Controls.Add(this.lblActif);
            this.pnlHeaderHoraires.Controls.Add(this.lblHeure);
            this.pnlHeaderHoraires.Controls.Add(this.lblArret);
            this.pnlHeaderHoraires.Location = new System.Drawing.Point(6, 43);
            this.pnlHeaderHoraires.Name = "pnlHeaderHoraires";
            this.pnlHeaderHoraires.Size = new System.Drawing.Size(750, 40);
            this.pnlHeaderHoraires.TabIndex = 2;
            // 
            // lblArret
            // 
            this.lblArret.AutoSize = true;
            this.lblArret.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArret.ForeColor = System.Drawing.Color.White;
            this.lblArret.Location = new System.Drawing.Point(15, 13);
            this.lblArret.Name = "lblArret";
            this.lblArret.Size = new System.Drawing.Size(37, 15);
            this.lblArret.TabIndex = 0;
            this.lblArret.Text = "Arrêt";
            // 
            // lblHeure
            // 
            this.lblHeure.AutoSize = true;
            this.lblHeure.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeure.ForeColor = System.Drawing.Color.White;
            this.lblHeure.Location = new System.Drawing.Point(200, 13);
            this.lblHeure.Name = "lblHeure";
            this.lblHeure.Size = new System.Drawing.Size(42, 15);
            this.lblHeure.TabIndex = 1;
            this.lblHeure.Text = "Heure";
            // 
            // lblActif
            // 
            this.lblActif.AutoSize = true;
            this.lblActif.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActif.ForeColor = System.Drawing.Color.White;
            this.lblActif.Location = new System.Drawing.Point(650, 13);
            this.lblActif.Name = "lblActif";
            this.lblActif.Size = new System.Drawing.Size(34, 15);
            this.lblActif.TabIndex = 2;
            this.lblActif.Text = "Actif";
            // 
            // lblJour
            // 
            this.lblJour.AutoSize = true;
            this.lblJour.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJour.Location = new System.Drawing.Point(10, 15);
            this.lblJour.Name = "lblJour";
            this.lblJour.Size = new System.Drawing.Size(33, 15);
            this.lblJour.TabIndex = 0;
            this.lblJour.Text = "Jour :";
            // 
            // cboJour
            // 
            this.cboJour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboJour.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboJour.FormattingEnabled = true;
            this.cboJour.Location = new System.Drawing.Point(50, 12);
            this.cboJour.Name = "cboJour";
            this.cboJour.Size = new System.Drawing.Size(200, 23);
            this.cboJour.TabIndex = 1;
            this.cboJour.SelectedIndexChanged += new System.EventHandler(this.cboJour_SelectedIndexChanged);
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
            this.pnlHeaderArrets.ResumeLayout(false);
            this.pnlHeaderArrets.PerformLayout();
            this.tabHoraires.ResumeLayout(false);
            this.tabHoraires.PerformLayout();
            this.pnlHeaderHoraires.ResumeLayout(false);
            this.pnlHeaderHoraires.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
