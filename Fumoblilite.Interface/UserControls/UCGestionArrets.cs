using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Systeme.Services;
using Fumoblilite.SQL.Repositories;

namespace Fumoblilite.Interface.UserControls
{
    public partial class UCGestionArrets : UserControl
    {
        private readonly string _connectionString;
        private readonly ServiceArret _serviceArret;
        private Arret _arretSelectionne;

        public UCGestionArrets(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
            
            var repositoryArret = new RepositoryArret(_connectionString);
            _serviceArret = new ServiceArret(repositoryArret);
        }

        private void InitializeComponent()
        {
            this.lblTitre = new System.Windows.Forms.Label();
            this.dgvArrets = new System.Windows.Forms.DataGridView();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblNom = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.lblAdresse = new System.Windows.Forms.Label();
            this.txtAdresse = new System.Windows.Forms.TextBox();
            this.lblLatitude = new System.Windows.Forms.Label();
            this.txtLatitude = new System.Windows.Forms.TextBox();
            this.lblLongitude = new System.Windows.Forms.Label();
            this.txtLongitude = new System.Windows.Forms.TextBox();
            this.chkEstAccessible = new System.Windows.Forms.CheckBox();
            this.btnNouveau = new System.Windows.Forms.Button();
            this.btnEnregistrer = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArrets)).BeginInit();
            this.grpDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(15, 15);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(159, 24);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Gestion des arrêts";
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
            this.dgvArrets.Location = new System.Drawing.Point(15, 50);
            this.dgvArrets.MultiSelect = false;
            this.dgvArrets.Name = "dgvArrets";
            this.dgvArrets.ReadOnly = true;
            this.dgvArrets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvArrets.Size = new System.Drawing.Size(450, 385);
            this.dgvArrets.TabIndex = 1;
            this.dgvArrets.SelectionChanged += new System.EventHandler(this.dgvArrets_SelectionChanged);
            // 
            // grpDetails
            // 
            this.grpDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDetails.Controls.Add(this.chkEstAccessible);
            this.grpDetails.Controls.Add(this.txtLongitude);
            this.grpDetails.Controls.Add(this.lblLongitude);
            this.grpDetails.Controls.Add(this.txtLatitude);
            this.grpDetails.Controls.Add(this.lblLatitude);
            this.grpDetails.Controls.Add(this.txtAdresse);
            this.grpDetails.Controls.Add(this.lblAdresse);
            this.grpDetails.Controls.Add(this.txtNom);
            this.grpDetails.Controls.Add(this.lblNom);
            this.grpDetails.Controls.Add(this.txtId);
            this.grpDetails.Controls.Add(this.lblId);
            this.grpDetails.Location = new System.Drawing.Point(480, 50);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(305, 300);
            this.grpDetails.TabIndex = 2;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Détails de l\'arrêt";
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
            this.txtId.Location = new System.Drawing.Point(100, 27);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(180, 20);
            this.txtId.TabIndex = 1;
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(20, 60);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(32, 13);
            this.lblNom.TabIndex = 2;
            this.lblNom.Text = "Nom:";
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(100, 57);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(180, 20);
            this.txtNom.TabIndex = 3;
            // 
            // lblAdresse
            // 
            this.lblAdresse.AutoSize = true;
            this.lblAdresse.Location = new System.Drawing.Point(20, 90);
            this.lblAdresse.Name = "lblAdresse";
            this.lblAdresse.Size = new System.Drawing.Size(48, 13);
            this.lblAdresse.TabIndex = 4;
            this.lblAdresse.Text = "Adresse:";
            // 
            // txtAdresse
            // 
            this.txtAdresse.Location = new System.Drawing.Point(100, 87);
            this.txtAdresse.Multiline = true;
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.Size = new System.Drawing.Size(180, 60);
            this.txtAdresse.TabIndex = 5;
            // 
            // lblLatitude
            // 
            this.lblLatitude.AutoSize = true;
            this.lblLatitude.Location = new System.Drawing.Point(20, 160);
            this.lblLatitude.Name = "lblLatitude";
            this.lblLatitude.Size = new System.Drawing.Size(48, 13);
            this.lblLatitude.TabIndex = 6;
            this.lblLatitude.Text = "Latitude:";
            // 
            // txtLatitude
            // 
            this.txtLatitude.Location = new System.Drawing.Point(100, 157);
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.Size = new System.Drawing.Size(180, 20);
            this.txtLatitude.TabIndex = 7;
            // 
            // lblLongitude
            // 
            this.lblLongitude.AutoSize = true;
            this.lblLongitude.Location = new System.Drawing.Point(20, 190);
            this.lblLongitude.Name = "lblLongitude";
            this.lblLongitude.Size = new System.Drawing.Size(57, 13);
            this.lblLongitude.TabIndex = 8;
            this.lblLongitude.Text = "Longitude:";
            // 
            // txtLongitude
            // 
            this.txtLongitude.Location = new System.Drawing.Point(100, 187);
            this.txtLongitude.Name = "txtLongitude";
            this.txtLongitude.Size = new System.Drawing.Size(180, 20);
            this.txtLongitude.TabIndex = 9;
            // 
            // chkEstAccessible
            // 
            this.chkEstAccessible.AutoSize = true;
            this.chkEstAccessible.Location = new System.Drawing.Point(100, 220);
            this.chkEstAccessible.Name = "chkEstAccessible";
            this.chkEstAccessible.Size = new System.Drawing.Size(93, 17);
            this.chkEstAccessible.TabIndex = 10;
            this.chkEstAccessible.Text = "Est accessible";
            this.chkEstAccessible.UseVisualStyleBackColor = true;
            // 
            // btnNouveau
            // 
            this.btnNouveau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNouveau.Location = new System.Drawing.Point(480, 360);
            this.btnNouveau.Name = "btnNouveau";
            this.btnNouveau.Size = new System.Drawing.Size(90, 30);
            this.btnNouveau.TabIndex = 3;
            this.btnNouveau.Text = "Nouveau";
            this.btnNouveau.UseVisualStyleBackColor = true;
            this.btnNouveau.Click += new System.EventHandler(this.btnNouveau_Click);
            // 
            // btnEnregistrer
            // 
            this.btnEnregistrer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnregistrer.Location = new System.Drawing.Point(580, 360);
            this.btnEnregistrer.Name = "btnEnregistrer";
            this.btnEnregistrer.Size = new System.Drawing.Size(90, 30);
            this.btnEnregistrer.TabIndex = 4;
            this.btnEnregistrer.Text = "Enregistrer";
            this.btnEnregistrer.UseVisualStyleBackColor = true;
            this.btnEnregistrer.Click += new System.EventHandler(this.btnEnregistrer_Click);
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSupprimer.Location = new System.Drawing.Point(680, 360);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(90, 30);
            this.btnSupprimer.TabIndex = 5;
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = true;
            this.btnSupprimer.Click += new System.EventHandler(this.btnSupprimer_Click);
            // 
            // UCGestionArrets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.btnEnregistrer);
            this.Controls.Add(this.btnNouveau);
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.dgvArrets);
            this.Controls.Add(this.lblTitre);
            this.Name = "UCGestionArrets";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.UCGestionArrets_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArrets)).EndInit();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.DataGridView dgvArrets;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.Label lblAdresse;
        private System.Windows.Forms.TextBox txtAdresse;
        private System.Windows.Forms.Label lblLatitude;
        private System.Windows.Forms.TextBox txtLatitude;
        private System.Windows.Forms.Label lblLongitude;
        private System.Windows.Forms.TextBox txtLongitude;
        private System.Windows.Forms.CheckBox chkEstAccessible;
        private System.Windows.Forms.Button btnNouveau;
        private System.Windows.Forms.Button btnEnregistrer;
        private System.Windows.Forms.Button btnSupprimer;

        private void UCGestionArrets_Load(object sender, EventArgs e)
        {
            ChargerArrets();
        }

        private void ChargerArrets()
        {
            try
            {
                List<Arret> arrets = _serviceArret.ObtenirTous();
                dgvArrets.DataSource = arrets;

                // Configurer l'affichage des colonnes
                dgvArrets.Columns["Id"].Width = 50;
                dgvArrets.Columns["Nom"].Width = 150;
                dgvArrets.Columns["Adresse"].Width = 200;
                dgvArrets.Columns["Latitude"].Width = 80;
                dgvArrets.Columns["Longitude"].Width = 80;
                dgvArrets.Columns["EstAccessible"].HeaderText = "Accessible";
                dgvArrets.Columns["DateCreation"].Visible = false;
                dgvArrets.Columns["DateModification"].Visible = false;

                ViderChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des arrêts : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViderChamps()
        {
            txtId.Text = string.Empty;
            txtNom.Text = string.Empty;
            txtAdresse.Text = string.Empty;
            txtLatitude.Text = string.Empty;
            txtLongitude.Text = string.Empty;
            chkEstAccessible.Checked = false;
            _arretSelectionne = null;
            btnSupprimer.Enabled = false;
        }

        private void dgvArrets_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArrets.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvArrets.SelectedRows[0].Cells["Id"].Value);
                _arretSelectionne = _serviceArret.ObtenirParId(id);
                if (_arretSelectionne != null)
                {
                    txtId.Text = _arretSelectionne.Id.ToString();
                    txtNom.Text = _arretSelectionne.Nom;
                    txtAdresse.Text = _arretSelectionne.Adresse;
                    txtLatitude.Text = _arretSelectionne.Latitude.ToString();
                    txtLongitude.Text = _arretSelectionne.Longitude.ToString();
                    chkEstAccessible.Checked = _arretSelectionne.EstAccessible;
                    btnSupprimer.Enabled = true;
                }
            }
        }

        private void btnNouveau_Click(object sender, EventArgs e)
        {
            ViderChamps();
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNom.Text))
                {
                    MessageBox.Show("Le nom de l'arrêt est obligatoire.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double latitude, longitude;
                if (!double.TryParse(txtLatitude.Text, out latitude) || !double.TryParse(txtLongitude.Text, out longitude))
                {
                    MessageBox.Show("Les coordonnées doivent être des nombres valides.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_arretSelectionne == null)
                {
                    // Nouvel arrêt
                    Arret nouvelArret = new Arret
                    {
                        Nom = txtNom.Text,
                        Adresse = txtAdresse.Text,
                        Latitude = latitude,
                        Longitude = longitude,
                        EstAccessible = chkEstAccessible.Checked
                    };

                    int id = _serviceArret.Ajouter(nouvelArret);
                    MessageBox.Show("Arrêt ajouté avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Modification d'un arrêt existant
                    _arretSelectionne.Nom = txtNom.Text;
                    _arretSelectionne.Adresse = txtAdresse.Text;
                    _arretSelectionne.Latitude = latitude;
                    _arretSelectionne.Longitude = longitude;
                    _arretSelectionne.EstAccessible = chkEstAccessible.Checked;

                    bool resultat = _serviceArret.Modifier(_arretSelectionne);
                    if (resultat)
                    {
                        MessageBox.Show("Arrêt modifié avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la modification de l'arrêt.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                ChargerArrets();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (_arretSelectionne != null)
            {
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet arrêt ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bool resultat = _serviceArret.Supprimer(_arretSelectionne.Id);
                        if (resultat)
                        {
                            MessageBox.Show("Arrêt supprimé avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ChargerArrets();
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la suppression de l'arrêt.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

