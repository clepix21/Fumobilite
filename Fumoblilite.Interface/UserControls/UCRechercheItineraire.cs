using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using GestionTransport.Systeme.Modeles;
using GestionTransport.Systeme.Services;
using GestionTransport.AccesDonnees.Repositories;
using GestionTransport.Systeme.Interfaces;
using System.Linq;

namespace GestionTransport.Interface.UserControls
{
    public partial class UCRechercheItineraire : UserControl
    {
        private readonly string _connectionString;
        private readonly ServiceItineraire _serviceItineraire;
        private readonly ServiceArret _serviceArret;
        private readonly ServiceLigne _serviceLigne;

        public UCRechercheItineraire(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
            
            // Initialisation des repositories
            IRepositoryArret repositoryArret = new RepositoryArret(_connectionString);
            IRepositoryLigne repositoryLigne = new RepositoryLigne(_connectionString);
            IRepositoryArretLigne repositoryArretLigne = new RepositoryArretLigne(_connectionString, repositoryArret);
            IRepositoryHoraire repositoryHoraire = new RepositoryHoraire(_connectionString);
            
            // Initialisation des services
            _serviceArret = new ServiceArret(repositoryArret);
            _serviceLigne = new ServiceLigne(repositoryLigne, repositoryArretLigne);
            _serviceItineraire = new ServiceItineraire(repositoryArret, repositoryLigne, repositoryArretLigne, repositoryHoraire);
        }

        private void InitializeComponent()
        {
            this.lblTitre = new System.Windows.Forms.Label();
            this.grpRecherche = new System.Windows.Forms.GroupBox();
            this.lblArretDepart = new System.Windows.Forms.Label();
            this.cboArretDepart = new System.Windows.Forms.ComboBox();
            this.lblArretArrivee = new System.Windows.Forms.Label();
            this.cboArretArrivee = new System.Windows.Forms.ComboBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblHeure = new System.Windows.Forms.Label();
            this.dtpHeure = new System.Windows.Forms.DateTimePicker();
            this.rbDepart = new System.Windows.Forms.RadioButton();
            this.rbArrivee = new System.Windows.Forms.RadioButton();
            this.btnRechercher = new System.Windows.Forms.Button();
            this.dgvItineraires = new System.Windows.Forms.DataGridView();
            this.pnlDetailsItineraire = new System.Windows.Forms.Panel();
            this.lblDetailsItineraire = new System.Windows.Forms.Label();
            this.grpRecherche.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItineraires)).BeginInit();
            this.pnlDetailsItineraire.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(15, 15);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(196, 24);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Recherche d\'itinéraire";
            // 
            // grpRecherche
            // 
            this.grpRecherche.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRecherche.Controls.Add(this.btnRechercher);
            this.grpRecherche.Controls.Add(this.rbArrivee);
            this.grpRecherche.Controls.Add(this.rbDepart);
            this.grpRecherche.Controls.Add(this.dtpHeure);
            this.grpRecherche.Controls.Add(this.lblHeure);
            this.grpRecherche.Controls.Add(this.dtpDate);
            this.grpRecherche.Controls.Add(this.lblDate);
            this.grpRecherche.Controls.Add(this.cboArretArrivee);
            this.grpRecherche.Controls.Add(this.lblArretArrivee);
            this.grpRecherche.Controls.Add(this.cboArretDepart);
            this.grpRecherche.Controls.Add(this.lblArretDepart);
            this.grpRecherche.Location = new System.Drawing.Point(15, 50);
            this.grpRecherche.Name = "grpRecherche";
            this.grpRecherche.Size = new System.Drawing.Size(770, 150);
            this.grpRecherche.TabIndex = 1;
            this.grpRecherche.TabStop = false;
            this.grpRecherche.Text = "Critères de recherche";
            // 
            // lblArretDepart
            // 
            this.lblArretDepart.AutoSize = true;
            this.lblArretDepart.Location = new System.Drawing.Point(20, 30);
            this.lblArretDepart.Name = "lblArretDepart";
            this.lblArretDepart.Size = new System.Drawing.Size(83, 13);
            this.lblArretDepart.TabIndex = 0;
            this.lblArretDepart.Text = "Arrêt de départ :";
            // 
            // cboArretDepart
            // 
            this.cboArretDepart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArretDepart.FormattingEnabled = true;
            this.cboArretDepart.Location = new System.Drawing.Point(120, 27);
            this.cboArretDepart.Name = "cboArretDepart";
            this.cboArretDepart.Size = new System.Drawing.Size(250, 21);
            this.cboArretDepart.TabIndex = 1;
            // 
            // lblArretArrivee
            // 
            this.lblArretArrivee.AutoSize = true;
            this.lblArretArrivee.Location = new System.Drawing.Point(20, 60);
            this.lblArretArrivee.Name = "lblArretArrivee";
            this.lblArretArrivee.Size = new System.Drawing.Size(82, 13);
            this.lblArretArrivee.TabIndex = 2;
            this.lblArretArrivee.Text = "Arrêt d\'arrivée :";
            // 
            // cboArretArrivee
            // 
            this.cboArretArrivee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArretArrivee.FormattingEnabled = true;
            this.cboArretArrivee.Location = new System.Drawing.Point(120, 57);
            this.cboArretArrivee.Name = "cboArretArrivee";
            this.cboArretArrivee.Size = new System.Drawing.Size(250, 21);
            this.cboArretArrivee.TabIndex = 3;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(400, 30);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(36, 13);
            this.lblDate.TabIndex = 4;
            this.lblDate.Text = "Date :";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(450, 27);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(120, 20);
            this.dtpDate.TabIndex = 5;
            // 
            // lblHeure
            // 
            this.lblHeure.AutoSize = true;
            this.lblHeure.Location = new System.Drawing.Point(400, 60);
            this.lblHeure.Name = "lblHeure";
            this.lblHeure.Size = new System.Drawing.Size(42, 13);
            this.lblHeure.TabIndex = 6;
            this.lblHeure.Text = "Heure :";
            // 
            // dtpHeure
            // 
            this.dtpHeure.CustomFormat = "HH:mm";
            this.dtpHeure.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHeure.Location = new System.Drawing.Point(450, 57);
            this.dtpHeure.Name = "dtpHeure";
            this.dtpHeure.ShowUpDown = true;
            this.dtpHeure.Size = new System.Drawing.Size(120, 20);
            this.dtpHeure.TabIndex = 7;
            // 
            // rbDepart
            // 
            this.rbDepart.AutoSize = true;
            this.rbDepart.Checked = true;
            this.rbDepart.Location = new System.Drawing.Point(120, 90);
            this.rbDepart.Name = "rbDepart";
            this.rbDepart.Size = new System.Drawing.Size(102, 17);
            this.rbDepart.TabIndex = 8;
            this.rbDepart.TabStop = true;
            this.rbDepart.Text = "Heure de départ";
            this.rbDepart.UseVisualStyleBackColor = true;
            // 
            // rbArrivee
            // 
            this.rbArrivee.AutoSize = true;
            this.rbArrivee.Location = new System.Drawing.Point(250, 90);
            this.rbArrivee.Name = "rbArrivee";
            this.rbArrivee.Size = new System.Drawing.Size(101, 17);
            this.rbArrivee.TabIndex = 9;
            this.rbArrivee.Text = "Heure d\'arrivée";
            this.rbArrivee.UseVisualStyleBackColor = true;
            // 
            // btnRechercher
            // 
            this.btnRechercher.Location = new System.Drawing.Point(450, 90);
            this.btnRechercher.Name = "btnRechercher";
            this.btnRechercher.Size = new System.Drawing.Size(120, 30);
            this.btnRechercher.TabIndex = 10;
            this.btnRechercher.Text = "Rechercher";
            this.btnRechercher.UseVisualStyleBackColor = true;
            this.btnRechercher.Click += new System.EventHandler(this.btnRechercher_Click);
            // 
            // dgvItineraires
            // 
            this.dgvItineraires.AllowUserToAddRows = false;
            this.dgvItineraires.AllowUserToDeleteRows = false;
            this.dgvItineraires.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItineraires.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItineraires.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItineraires.Location = new System.Drawing.Point(15, 210);
            this.dgvItineraires.MultiSelect = false;
            this.dgvItineraires.Name = "dgvItineraires";
            this.dgvItineraires.ReadOnly = true;
            this.dgvItineraires.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItineraires.Size = new System.Drawing.Size(400, 225);
            this.dgvItineraires.TabIndex = 2;
            this.dgvItineraires.SelectionChanged += new System.EventHandler(this.dgvItineraires_SelectionChanged);
            // 
            // pnlDetailsItineraire
            // 
            this.pnlDetailsItineraire.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDetailsItineraire.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetailsItineraire.Controls.Add(this.lblDetailsItineraire);
            this.pnlDetailsItineraire.Location = new System.Drawing.Point(430, 210);
            this.pnlDetailsItineraire.Name = "pnlDetailsItineraire";
            this.pnlDetailsItineraire.Size = new System.Drawing.Size(355, 225);
            this.pnlDetailsItineraire.TabIndex = 3;
            // 
            // lblDetailsItineraire
            // 
            this.lblDetailsItineraire.AutoSize = true;
            this.lblDetailsItineraire.Location = new System.Drawing.Point(10, 10);
            this.lblDetailsItineraire.Name = "lblDetailsItineraire";
            this.lblDetailsItineraire.Size = new System.Drawing.Size(174, 13);
            this.lblDetailsItineraire.TabIndex = 0;
            this.lblDetailsItineraire.Text = "Sélectionnez un itinéraire à gauche";
            // 
            // UCRechercheItineraire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlDetailsItineraire);
            this.Controls.Add(this.dgvItineraires);
            this.Controls.Add(this.grpRecherche);
            this.Controls.Add(this.lblTitre);
            this.Name = "UCRechercheItineraire";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.UCRechercheItineraire_Load);
            this.grpRecherche.ResumeLayout(false);
            this.grpRecherche.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItineraires)).EndInit();
            this.pnlDetailsItineraire.ResumeLayout(false);
            this.pnlDetailsItineraire.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.GroupBox grpRecherche;
        private System.Windows.Forms.Label lblArretDepart;
        private System.Windows.Forms.ComboBox cboArretDepart;
        private System.Windows.Forms.Label lblArretArrivee;
        private System.Windows.Forms.ComboBox cboArretArrivee;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblHeure;
        private System.Windows.Forms.DateTimePicker dtpHeure;
        private System.Windows.Forms.RadioButton rbDepart;
        private System.Windows.Forms.RadioButton rbArrivee;
        private System.Windows.Forms.Button btnRechercher;
        private System.Windows.Forms.DataGridView dgvItineraires;
        private System.Windows.Forms.Panel pnlDetailsItineraire;
        private System.Windows.Forms.Label lblDetailsItineraire;

        private void UCRechercheItineraire_Load(object sender, EventArgs e)
        {
            ChargerArrets();
            dtpDate.Value = DateTime.Today;
            dtpHeure.Value = DateTime.Now;
        }

        private void ChargerArrets()
        {
            try
            {
                List<Arret> arrets = _serviceArret.ObtenirTous();
                
                // Configuration du ComboBox de départ
                cboArretDepart.DisplayMember = "Nom";
                cboArretDepart.ValueMember = "Id";
                cboArretDepart.DataSource = new List<Arret>(arrets);
                
                // Configuration du ComboBox d'arrivée
                cboArretArrivee.DisplayMember = "Nom";
                cboArretArrivee.ValueMember = "Id";
                cboArretArrivee.DataSource = new List<Arret>(arrets);
                
                if (arrets.Count > 1)
                {
                    cboArretArrivee.SelectedIndex = 1; // Sélectionner le deuxième arrêt par défaut
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des arrêts : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboArretDepart.SelectedItem == null || cboArretArrivee.SelectedItem == null)
                {
                    MessageBox.Show("Veuillez sélectionner les arrêts de départ et d'arrivée.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int arretDepartId = (int)cboArretDepart.SelectedValue;
                int arretArriveeId = (int)cboArretArrivee.SelectedValue;
                
                if (arretDepartId == arretArriveeId)
                {
                    MessageBox.Show("Les arrêts de départ et d'arrivée doivent être différents.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Combiner la date et l'heure
                DateTime dateHeure = dtpDate.Value.Date.Add(dtpHeure.Value.TimeOfDay);
                bool estHeureDepart = rbDepart.Checked;

                // Rechercher les itinéraires
                List<Itineraire> itineraires = _serviceItineraire.RechercherItineraires(arretDepartId, arretArriveeId, dateHeure, estHeureDepart);
                
                if (itineraires.Count == 0)
                {
                    MessageBox.Show("Aucun itinéraire trouvé pour les critères spécifiés.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvItineraires.DataSource = null;
                    lblDetailsItineraire.Text = "Aucun itinéraire trouvé";
                    return;
                }

                // Préparer les données pour l'affichage dans le DataGridView
                var itinerairesAffichage = itineraires.Select(i => new
                {
                    Départ = i.HeureDepart.ToShortTimeString(),
                    Arrivée = i.HeureArrivee.ToShortTimeString(),
                    Durée = $"{i.DureeMinutes} min",
                    Changements = i.NombreChangements,
                    ItineraireComplet = i // Pour pouvoir accéder à l'objet complet
                }).ToList();

                dgvItineraires.DataSource = itinerairesAffichage;
                
                // Masquer la colonne de l'objet complet
                dgvItineraires.Columns["ItineraireComplet"].Visible = false;
                
                // Sélectionner le premier itinéraire
                if (dgvItineraires.Rows.Count > 0)
                {
                    dgvItineraires.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la recherche d'itinéraires : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItineraires_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItineraires.SelectedRows.Count > 0 && dgvItineraires.DataSource != null)
            {
                try
                {
                    // Récupérer l'itinéraire sélectionné
                    dynamic row = dgvItineraires.SelectedRows[0].DataBoundItem;
                    Itineraire itineraire = row.ItineraireComplet;

                    // Afficher les détails de l'itinéraire
                    AfficherDetailsItineraire(itineraire);
                }
                catch (Exception ex)
                {
                    lblDetailsItineraire.Text = $"Erreur lors de l'affichage des détails : {ex.Message}";
                }
            }
            else
            {
                lblDetailsItineraire.Text = "Sélectionnez un itinéraire à gauche";
            }
        }

        private void AfficherDetailsItineraire(Itineraire itineraire)
        {
            // Créer un contrôle FlowLayoutPanel pour afficher les étapes
            FlowLayoutPanel flpEtapes = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                WrapContents = false
            };

            // Ajouter un en-tête
            Label lblEnTete = new Label
            {
                Text = $"Départ : {itineraire.HeureDepart.ToShortTimeString()} - Arrivée : {itineraire.HeureArrivee.ToShortTimeString()}",
                Font = new Font(Font.FontFamily, 10, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(5)
            };
            flpEtapes.Controls.Add(lblEnTete);

            Label lblDuree = new Label
            {
                Text = $"Durée : {itineraire.DureeMinutes} minutes - Changements : {itineraire.NombreChangements}",
                AutoSize = true,
                Margin = new Padding(5)
            };
            flpEtapes.Controls.Add(lblDuree);

            // Ajouter une ligne de séparation
            Panel pnlSeparator = new Panel
            {
                Height = 1,
                Width = pnlDetailsItineraire.Width - 20,
                BackColor = Color.Gray,
                Margin = new Padding(5)
            };
            flpEtapes.Controls.Add(pnlSeparator);

            // Ajouter chaque étape
            foreach (var etape in itineraire.Etapes)
            {
                // Créer un panel pour l'étape
                Panel pnlEtape = new Panel
                {
                    Width = pnlDetailsItineraire.Width - 20,
                    Height = 80,
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.FixedSingle
                };

                // Ajouter un indicateur de ligne (couleur)
                Panel pnlLigne = new Panel
                {
                    Width = 10,
                    Height = pnlEtape.Height,
                    Dock = DockStyle.Left,
                    BackColor = ColorTranslator.FromHtml(etape.CouleurLigne ?? "#808080")
                };
                pnlEtape.Controls.Add(pnlLigne);

                // Ajouter les informations de l'étape
                Label lblLigne = new Label
                {
                    Text = $"Ligne {etape.NomLigne}",
                    Font = new Font(Font.FontFamily, 9, FontStyle.Bold),
                    Location = new Point(20, 5),
                    AutoSize = true
                };
                pnlEtape.Controls.Add(lblLigne);

                Label lblArrets = new Label
                {
                    Text = $"De {etape.NomArretDepart} à {etape.NomArretArrivee}",
                    Location = new Point(20, 25),
                    AutoSize = true
                };
                pnlEtape.Controls.Add(lblArrets);

                Label lblHoraires = new Label
                {
                    Text = $"{etape.HeureDepart.ToShortTimeString()} - {etape.HeureArrivee.ToShortTimeString()} ({etape.DureeMinutes} min)",
                    Location = new Point(20, 45),
                    AutoSize = true
                };
                pnlEtape.Controls.Add(lblHoraires);

                flpEtapes.Controls.Add(pnlEtape);
            }

            // Remplacer le contenu du panel de détails
            pnlDetailsItineraire.Controls.Clear();
            pnlDetailsItineraire.Controls.Add(flpEtapes);
        }
    }
}

