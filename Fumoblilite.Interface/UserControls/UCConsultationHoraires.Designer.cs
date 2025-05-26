namespace Fumoblilite.Interface.UserControls
{
    partial class UCConsultationHoraires
    {
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.GroupBox grpFiltres;
        private System.Windows.Forms.Label lblLigne;
        private System.Windows.Forms.ComboBox cboLigne;
        private System.Windows.Forms.Label lblArret;
        private System.Windows.Forms.ComboBox cboArret;
        private System.Windows.Forms.Label lblJour;
        private System.Windows.Forms.ComboBox cboJour;
        private System.Windows.Forms.Label lblHeureDebut;
        private System.Windows.Forms.DateTimePicker dtpHeureDebut;
        private System.Windows.Forms.Label lblHeureFin;
        private System.Windows.Forms.DateTimePicker dtpHeureFin;
        private System.Windows.Forms.Label lblRecherche;
        private System.Windows.Forms.TextBox txtRecherche;
        private System.Windows.Forms.CheckBox chkSeulementActifs;
        private System.Windows.Forms.CheckBox chkProchainsDeparts;
        private System.Windows.Forms.Button btnAfficher;
        private System.Windows.Forms.Button btnExporter;
        private System.Windows.Forms.FlowLayoutPanel flpHoraires;
        private System.Windows.Forms.Label lblStatistiques;
        private System.Windows.Forms.Label lblProchainsDeparts;

        private void InitializeComponent()
        {
            this.lblTitre = new System.Windows.Forms.Label();
            this.grpFiltres = new System.Windows.Forms.GroupBox();
            this.lblLigne = new System.Windows.Forms.Label();
            this.cboLigne = new System.Windows.Forms.ComboBox();
            this.lblArret = new System.Windows.Forms.Label();
            this.cboArret = new System.Windows.Forms.ComboBox();
            this.lblJour = new System.Windows.Forms.Label();
            this.cboJour = new System.Windows.Forms.ComboBox();
            this.lblHeureDebut = new System.Windows.Forms.Label();
            this.dtpHeureDebut = new System.Windows.Forms.DateTimePicker();
            this.lblHeureFin = new System.Windows.Forms.Label();
            this.dtpHeureFin = new System.Windows.Forms.DateTimePicker();
            this.lblRecherche = new System.Windows.Forms.Label();
            this.txtRecherche = new System.Windows.Forms.TextBox();
            this.chkSeulementActifs = new System.Windows.Forms.CheckBox();
            this.chkProchainsDeparts = new System.Windows.Forms.CheckBox();
            this.btnAfficher = new System.Windows.Forms.Button();
            this.btnExporter = new System.Windows.Forms.Button();
            this.flpHoraires = new System.Windows.Forms.FlowLayoutPanel();
            this.lblStatistiques = new System.Windows.Forms.Label();
            this.lblProchainsDeparts = new System.Windows.Forms.Label();
            this.grpFiltres.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblTitre.Location = new System.Drawing.Point(15, 15);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(250, 26);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Consultation des horaires";

            // 
            // grpFiltres
            // 
            this.grpFiltres.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFiltres.Controls.Add(this.lblLigne);
            this.grpFiltres.Controls.Add(this.cboLigne);
            this.grpFiltres.Controls.Add(this.lblArret);
            this.grpFiltres.Controls.Add(this.cboArret);
            this.grpFiltres.Controls.Add(this.lblJour);
            this.grpFiltres.Controls.Add(this.cboJour);
            this.grpFiltres.Controls.Add(this.lblHeureDebut);
            this.grpFiltres.Controls.Add(this.dtpHeureDebut);
            this.grpFiltres.Controls.Add(this.lblHeureFin);
            this.grpFiltres.Controls.Add(this.dtpHeureFin);
            this.grpFiltres.Controls.Add(this.lblRecherche);
            this.grpFiltres.Controls.Add(this.txtRecherche);
            this.grpFiltres.Controls.Add(this.chkSeulementActifs);
            this.grpFiltres.Controls.Add(this.chkProchainsDeparts);
            this.grpFiltres.Controls.Add(this.btnAfficher);
            this.grpFiltres.Controls.Add(this.btnExporter);
            this.grpFiltres.Location = new System.Drawing.Point(15, 50);
            this.grpFiltres.Name = "grpFiltres";
            this.grpFiltres.Size = new System.Drawing.Size(970, 120);
            this.grpFiltres.TabIndex = 1;
            this.grpFiltres.TabStop = false;
            this.grpFiltres.Text = "Filtres de recherche";

            // Première ligne de filtres
            this.lblLigne.AutoSize = true;
            this.lblLigne.Location = new System.Drawing.Point(20, 30);
            this.lblLigne.Name = "lblLigne";
            this.lblLigne.Size = new System.Drawing.Size(39, 13);
            this.lblLigne.TabIndex = 0;
            this.lblLigne.Text = "Ligne :";

            this.cboLigne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLigne.FormattingEnabled = true;
            this.cboLigne.Location = new System.Drawing.Point(70, 27);
            this.cboLigne.Name = "cboLigne";
            this.cboLigne.Size = new System.Drawing.Size(150, 21);
            this.cboLigne.TabIndex = 1;

            this.lblArret.AutoSize = true;
            this.lblArret.Location = new System.Drawing.Point(240, 30);
            this.lblArret.Name = "lblArret";
            this.lblArret.Size = new System.Drawing.Size(35, 13);
            this.lblArret.TabIndex = 2;
            this.lblArret.Text = "Arrêt :";

            this.cboArret.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArret.FormattingEnabled = true;
            this.cboArret.Location = new System.Drawing.Point(290, 27);
            this.cboArret.Name = "cboArret";
            this.cboArret.Size = new System.Drawing.Size(150, 21);
            this.cboArret.TabIndex = 3;

            this.lblJour.AutoSize = true;
            this.lblJour.Location = new System.Drawing.Point(460, 30);
            this.lblJour.Name = "lblJour";
            this.lblJour.Size = new System.Drawing.Size(33, 13);
            this.lblJour.TabIndex = 4;
            this.lblJour.Text = "Jour :";

            this.cboJour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJour.FormattingEnabled = true;
            this.cboJour.Location = new System.Drawing.Point(510, 27);
            this.cboJour.Name = "cboJour";
            this.cboJour.Size = new System.Drawing.Size(120, 21);
            this.cboJour.TabIndex = 5;

            // Deuxième ligne de filtres
            this.lblHeureDebut.AutoSize = true;
            this.lblHeureDebut.Location = new System.Drawing.Point(20, 60);
            this.lblHeureDebut.Name = "lblHeureDebut";
            this.lblHeureDebut.Size = new System.Drawing.Size(42, 13);
            this.lblHeureDebut.TabIndex = 6;
            this.lblHeureDebut.Text = "De :";

            this.dtpHeureDebut.CustomFormat = "HH:mm";
            this.dtpHeureDebut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHeureDebut.Location = new System.Drawing.Point(70, 57);
            this.dtpHeureDebut.Name = "dtpHeureDebut";
            this.dtpHeureDebut.ShowUpDown = true;
            this.dtpHeureDebut.Size = new System.Drawing.Size(80, 20);
            this.dtpHeureDebut.TabIndex = 7;

            this.lblHeureFin.AutoSize = true;
            this.lblHeureFin.Location = new System.Drawing.Point(170, 60);
            this.lblHeureFin.Name = "lblHeureFin";
            this.lblHeureFin.Size = new System.Drawing.Size(18, 13);
            this.lblHeureFin.TabIndex = 8;
            this.lblHeureFin.Text = "À :";

            this.dtpHeureFin.CustomFormat = "HH:mm";
            this.dtpHeureFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHeureFin.Location = new System.Drawing.Point(200, 57);
            this.dtpHeureFin.Name = "dtpHeureFin";
            this.dtpHeureFin.ShowUpDown = true;
            this.dtpHeureFin.Size = new System.Drawing.Size(80, 20);
            this.dtpHeureFin.TabIndex = 9;

            this.lblRecherche.AutoSize = true;
            this.lblRecherche.Location = new System.Drawing.Point(300, 60);
            this.lblRecherche.Name = "lblRecherche";
            this.lblRecherche.Size = new System.Drawing.Size(59, 13);
            this.lblRecherche.TabIndex = 10;
            this.lblRecherche.Text = "Recherche :";

            this.txtRecherche.Location = new System.Drawing.Point(370, 57);
            this.txtRecherche.Name = "txtRecherche";
            this.txtRecherche.Size = new System.Drawing.Size(200, 20);
            this.txtRecherche.TabIndex = 11;
            this.txtRecherche.TextChanged += new System.EventHandler(this.txtRecherche_TextChanged);

            // Troisième ligne - Cases à cocher et boutons
            this.chkSeulementActifs.AutoSize = true;
            this.chkSeulementActifs.Location = new System.Drawing.Point(20, 90);
            this.chkSeulementActifs.Name = "chkSeulementActifs";
            this.chkSeulementActifs.Size = new System.Drawing.Size(110, 17);
            this.chkSeulementActifs.TabIndex = 12;
            this.chkSeulementActifs.Text = "Seulement actifs";
            this.chkSeulementActifs.UseVisualStyleBackColor = true;

            this.chkProchainsDeparts.AutoSize = true;
            this.chkProchainsDeparts.Location = new System.Drawing.Point(150, 90);
            this.chkProchainsDeparts.Name = "chkProchainsDeparts";
            this.chkProchainsDeparts.Size = new System.Drawing.Size(120, 17);
            this.chkProchainsDeparts.TabIndex = 13;
            this.chkProchainsDeparts.Text = "Prochains départs";
            this.chkProchainsDeparts.UseVisualStyleBackColor = true;
            this.chkProchainsDeparts.CheckedChanged += new System.EventHandler(this.chkProchainsDeparts_CheckedChanged);

            this.btnAfficher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnAfficher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAfficher.ForeColor = System.Drawing.Color.White;
            this.btnAfficher.Location = new System.Drawing.Point(600, 85);
            this.btnAfficher.Name = "btnAfficher";
            this.btnAfficher.Size = new System.Drawing.Size(100, 25);
            this.btnAfficher.TabIndex = 14;
            this.btnAfficher.Text = "Afficher";
            this.btnAfficher.UseVisualStyleBackColor = false;
            this.btnAfficher.Click += new System.EventHandler(this.btnAfficher_Click);

            this.btnExporter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnExporter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExporter.ForeColor = System.Drawing.Color.White;
            this.btnExporter.Location = new System.Drawing.Point(710, 85);
            this.btnExporter.Name = "btnExporter";
            this.btnExporter.Size = new System.Drawing.Size(80, 25);
            this.btnExporter.TabIndex = 15;
            this.btnExporter.Text = "Exporter";
            this.btnExporter.UseVisualStyleBackColor = false;
            this.btnExporter.Click += new System.EventHandler(this.btnExporter_Click);

            // 
            // lblProchainsDeparts
            // 
            this.lblProchainsDeparts.AutoSize = true;
            this.lblProchainsDeparts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic);
            this.lblProchainsDeparts.Location = new System.Drawing.Point(15, 180);
            this.lblProchainsDeparts.Name = "lblProchainsDeparts";
            this.lblProchainsDeparts.Size = new System.Drawing.Size(0, 15);
            this.lblProchainsDeparts.TabIndex = 16;

            // 
            // flpHoraires
            // 
            this.flpHoraires.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.flpHoraires.AutoScroll = true;
            this.flpHoraires.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.flpHoraires.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpHoraires.Location = new System.Drawing.Point(15, 200);
            this.flpHoraires.Name = "flpHoraires";
            this.flpHoraires.Padding = new System.Windows.Forms.Padding(10);
            this.flpHoraires.Size = new System.Drawing.Size(970, 300);
            this.flpHoraires.TabIndex = 17;
            this.flpHoraires.WrapContents = false;

            // 
            // lblStatistiques
            // 
            this.lblStatistiques.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatistiques.AutoSize = true;
            this.lblStatistiques.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblStatistiques.ForeColor = System.Drawing.Color.Gray;
            this.lblStatistiques.Location = new System.Drawing.Point(15, 510);
            this.lblStatistiques.Name = "lblStatistiques";
            this.lblStatistiques.Size = new System.Drawing.Size(0, 13);
            this.lblStatistiques.TabIndex = 18;

            // 
            // UCConsultationHoraires
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblStatistiques);
            this.Controls.Add(this.flpHoraires);
            this.Controls.Add(this.lblProchainsDeparts);
            this.Controls.Add(this.grpFiltres);
            this.Controls.Add(this.lblTitre);
            this.Name = "UCConsultationHoraires";
            this.Size = new System.Drawing.Size(1000, 530);
            this.Load += new System.EventHandler(this.UCConsultationHoraires_Load);
            this.grpFiltres.ResumeLayout(false);
            this.grpFiltres.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
