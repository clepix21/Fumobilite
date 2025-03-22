using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Systeme.Services;
using Fumoblilite.SQL.Repositories;
using Fumoblilite.Systeme.Interfaces;

namespace Fumoblilite.Interface.UserControls
{
    public partial class UCGestionHoraires : UserControl
    {
        private readonly string _connectionString;
        private readonly ServiceHoraire _serviceHoraire;
        private readonly ServiceLigne _serviceLigne;
        private readonly ServiceArret _serviceArret;
        private Horaire _horaireSelectionne;

        public UCGestionHoraires(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;

            // Initialisation des repositories
            IRepositoryHoraire repositoryHoraire = new RepositoryHoraire(_connectionString);
            IRepositoryLigne repositoryLigne = new RepositoryLigne(_connectionString);
            IRepositoryArret repositoryArret = new RepositoryArret(_connectionString);
            IRepositoryArretLigne repositoryArretLigne = new RepositoryArretLigne(_connectionString, repositoryArret);

            // Initialisation des services
            _serviceHoraire = new ServiceHoraire(repositoryHoraire);
            _serviceLigne = new ServiceLigne(repositoryLigne, repositoryArretLigne);
            _serviceArret = new ServiceArret(repositoryArret);
        }

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
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblLigneDetail = new System.Windows.Forms.Label();
            this.cboLigneDetail = new System.Windows.Forms.ComboBox();
            this.lblArret = new System.Windows.Forms.Label();
            this.cboArret = new System.Windows.Forms.ComboBox();
            this.lblJourDetail = new System.Windows.Forms.Label();
            this.cboJourDetail = new System.Windows.Forms.ComboBox();
            this.lblHeure = new System.Windows.Forms.Label();
            this.dtpHeure = new System.Windows.Forms.DateTimePicker();
            this.chkEstActif = new System.Windows.Forms.CheckBox();
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

        private void UCGestionHoraires_Load(object sender, EventArgs e)
        {
            ChargerLignes();
            ChargerJours();
            dtpHeure.Value = DateTime.Now;
        }

        private void ChargerLignes()
        {
            try
            {
                List<Ligne> lignes = _serviceLigne.ObtenirToutes();

                // Configuration du ComboBox de filtre
                var lignesFiltres = new List<Ligne>(lignes);
                lignesFiltres.Insert(0, new Ligne { Id = 0, Numero = "Toutes", Nom = "Toutes les lignes" });

                cboLigne.DisplayMember = "Nom";
                cboLigne.ValueMember = "Id";
                cboLigne.DataSource = lignesFiltres;

                // Configuration du ComboBox de détail
                cboLigneDetail.DisplayMember = "Nom";
                cboLigneDetail.ValueMember = "Id";
                cboLigneDetail.DataSource = new List<Ligne>(lignes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des lignes : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerJours()
        {
            try
            {
                // Créer une liste des jours de la semaine
                var jours = new List<KeyValuePair<DayOfWeek, string>>
                {
                    new KeyValuePair<DayOfWeek, string>(DayOfWeek.Monday, "Lundi"),
                    new KeyValuePair<DayOfWeek, string>(DayOfWeek.Tuesday, "Mardi"),
                    new KeyValuePair<DayOfWeek, string>(DayOfWeek.Wednesday, "Mercredi"),
                    new KeyValuePair<DayOfWeek, string>(DayOfWeek.Thursday, "Jeudi"),
                    new KeyValuePair<DayOfWeek, string>(DayOfWeek.Friday, "Vendredi"),
                    new KeyValuePair<DayOfWeek, string>(DayOfWeek.Saturday, "Samedi"),
                    new KeyValuePair<DayOfWeek, string>(DayOfWeek.Sunday, "Dimanche")
                };

                // Configuration du ComboBox de filtre
                cboJour.DisplayMember = "Value";
                cboJour.ValueMember = "Key";
                cboJour.DataSource = new List<KeyValuePair<DayOfWeek, string>>(jours);

                // Configuration du ComboBox de détail
                cboJourDetail.DisplayMember = "Value";
                cboJourDetail.ValueMember = "Key";
                cboJourDetail.DataSource = new List<KeyValuePair<DayOfWeek, string>>(jours);

                // Sélectionner le jour actuel
                cboJour.SelectedValue = DateTime.Today.DayOfWeek;
                cboJourDetail.SelectedValue = DateTime.Today.DayOfWeek;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des jours : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerArrets(int ligneId)
        {
            try
            {
                if (ligneId <= 0)
                {
                    cboArret.DataSource = null;
                    return;
                }

                // Récupérer les arrêts de la ligne
                var ligne = _serviceLigne.ObtenirParId(ligneId, true);
                if (ligne == null || ligne.Arrets == null || ligne.Arrets.Count == 0)
                {
                    cboArret.DataSource = null;
                    return;
                }

                // Récupérer les informations des arrêts
                var arrets = _serviceArret.ObtenirTous().ToDictionary(a => a.Id);

                // Préparer les données pour l'affichage
                var arretsAffichage = ligne.Arrets
                    .Where(al => arrets.ContainsKey(al.ArretId))
                    .Select(al => new
                    {
                        Id = al.ArretId,
                        Nom = arrets[al.ArretId].Nom
                    })
                    .OrderBy(a => a.Nom)
                    .ToList();

                cboArret.DisplayMember = "Nom";
                cboArret.ValueMember = "Id";
                cboArret.DataSource = arretsAffichage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des arrêts : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAfficher_Click(object sender, EventArgs e)
        {
            try
            {
                int ligneId = (int)cboLigne.SelectedValue;
                DayOfWeek jour = (DayOfWeek)cboJour.SelectedValue;

                List<Horaire> horaires;

                if (ligneId == 0) // Toutes les lignes
                {
                    horaires = _serviceHoraire.ObtenirParJour(jour);
                }
                else
                {
                    horaires = _serviceHoraire.ObtenirParLigneEtJour(ligneId, jour);
                }

                if (horaires.Count == 0)
                {
                    MessageBox.Show("Aucun horaire trouvé pour les critères spécifiés.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvHoraires.DataSource = null;
                    return;
                }

                // Récupérer les informations des lignes et des arrêts pour l'affichage
                var lignes = _serviceLigne.ObtenirToutes().ToDictionary(l => l.Id);
                var arrets = _serviceArret.ObtenirTous().ToDictionary(a => a.Id);

                // Préparer les données pour l'affichage
                var horairesAffichage = horaires.Select(h => new
                {
                    Id = h.Id,
                    Ligne = lignes.ContainsKey(h.LigneId) ? $"{lignes[h.LigneId].Numero} - {lignes[h.LigneId].Nom}" : $"Ligne {h.LigneId}",
                    Arrêt = arrets.ContainsKey(h.ArretId) ? arrets[h.ArretId].Nom : $"Arrêt {h.ArretId}",
                    Jour = ((DayOfWeek)h.JourSemaine).ToString(),
                    Heure = h.HeureDepart.ToString(@"hh\:mm"),
                    Actif = h.EstActif ? "Oui" : "Non",
                    HoraireComplet = h // Pour pouvoir accéder à l'objet complet
                }).OrderBy(h => h.Ligne).ThenBy(h => h.Arrêt).ThenBy(h => h.Heure).ToList();

                dgvHoraires.DataSource = horairesAffichage;

                // Masquer la colonne de l'objet complet
                dgvHoraires.Columns["HoraireComplet"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'affichage des horaires : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHoraires_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoraires.SelectedRows.Count > 0 && dgvHoraires.DataSource != null)
            {
                try
                {
                    // Récupérer l'horaire sélectionné
                    dynamic row = dgvHoraires.SelectedRows[0].DataBoundItem;
                    _horaireSelectionne = row.HoraireComplet;

                    if (_horaireSelectionne != null)
                    {
                        txtId.Text = _horaireSelectionne.Id.ToString();
                        cboLigneDetail.SelectedValue = _horaireSelectionne.LigneId;
                        ChargerArrets(_horaireSelectionne.LigneId);
                        cboArret.SelectedValue = _horaireSelectionne.ArretId;
                        cboJourDetail.SelectedValue = _horaireSelectionne.JourSemaine;
                        dtpHeure.Value = DateTime.Today.Add(_horaireSelectionne.HeureDepart);
                        chkEstActif.Checked = _horaireSelectionne.EstActif;
                        btnSupprimer.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la sélection de l'horaire : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cboLigneDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLigneDetail.SelectedItem != null)
            {
                int ligneId = (int)cboLigneDetail.SelectedValue;
                ChargerArrets(ligneId);
            }
        }

        private void btnNouveau_Click(object sender, EventArgs e)
        {
            ViderChamps();
        }

        private void ViderChamps()
        {
            txtId.Text = string.Empty;
            if (cboLigneDetail.Items.Count > 0)
                cboLigneDetail.SelectedIndex = 0;
            if (cboJourDetail.Items.Count > 0)
                cboJourDetail.SelectedValue = DateTime.Today.DayOfWeek;
            dtpHeure.Value = DateTime.Now;
            chkEstActif.Checked = true;
            _horaireSelectionne = null;
            btnSupprimer.Enabled = false;
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboLigneDetail.SelectedItem == null || cboArret.SelectedItem == null)
                {
                    MessageBox.Show("Veuillez sélectionner une ligne et un arrêt.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int ligneId = (int)cboLigneDetail.SelectedValue;
                int arretId = (int)cboArret.SelectedValue;
                DayOfWeek jour = (DayOfWeek)cboJourDetail.SelectedValue;
                TimeSpan heure = dtpHeure.Value.TimeOfDay;
                bool estActif = chkEstActif.Checked;

                if (_horaireSelectionne == null)
                {
                    // Nouvel horaire
                    Horaire nouvelHoraire = new Horaire
                    {
                        LigneId = ligneId,
                        ArretId = arretId,
                        JourSemaine = jour,
                        HeureDepart = heure,
                        EstActif = estActif
                    };

                    int id = _serviceHoraire.Ajouter(nouvelHoraire);
                    MessageBox.Show("Horaire ajouté avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Modification d'un horaire existant
                    _horaireSelectionne.LigneId = ligneId;
                    _horaireSelectionne.ArretId = arretId;
                    _horaireSelectionne.JourSemaine = jour;
                    _horaireSelectionne.HeureDepart = heure;
                    _horaireSelectionne.EstActif = estActif;

                    bool resultat = _serviceHoraire.Modifier(_horaireSelectionne);
                    if (resultat)
                    {
                        MessageBox.Show("Horaire modifié avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la modification de l'horaire.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Rafraîchir l'affichage
                btnAfficher_Click(sender, e);
                ViderChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (_horaireSelectionne != null)
            {
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet horaire ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bool resultat = _serviceHoraire.Supprimer(_horaireSelectionne.Id);
                        if (resultat)
                        {
                            MessageBox.Show("Horaire supprimé avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Rafraîchir l'affichage
                            btnAfficher_Click(sender, e);
                            ViderChamps();
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la suppression de l'horaire.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
