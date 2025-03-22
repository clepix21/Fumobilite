using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Systeme.Services;
using Fumoblilite.SQL.Repositories;
using Fumoblilite.Systeme.Interfaces;

namespace Fumoblilite.Interface.UserControls
{
    public partial class UCGestionUtilisateurs : UserControl
    {
        private readonly string _connectionString;
        private readonly ServiceAuthentification _serviceAuthentification;
        private readonly IRepositoryUtilisateur _repositoryUtilisateur;
        private Utilisateur _utilisateurSelectionne;

        public UCGestionUtilisateurs(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
            
            _repositoryUtilisateur = new RepositoryUtilisateur(_connectionString);
            _serviceAuthentification = new ServiceAuthentification(_repositoryUtilisateur);
        }

        private void InitializeComponent()
        {
            this.lblTitre = new System.Windows.Forms.Label();
            this.dgvUtilisateurs = new System.Windows.Forms.DataGridView();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblNom = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.lblPrenom = new System.Windows.Forms.Label();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.lblNomUtilisateur = new System.Windows.Forms.Label();
            this.txtNomUtilisateur = new System.Windows.Forms.TextBox();
            this.lblMotDePasse = new System.Windows.Forms.Label();
            this.txtMotDePasse = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.chkEstActif = new System.Windows.Forms.CheckBox();
            this.btnNouveau = new System.Windows.Forms.Button();
            this.btnEnregistrer = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUtilisateurs)).BeginInit();
            this.grpDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(15, 15);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(211, 24);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Gestion des utilisateurs";
            // 
            // dgvUtilisateurs
            // 
            this.dgvUtilisateurs.AllowUserToAddRows = false;
            this.dgvUtilisateurs.AllowUserToDeleteRows = false;
            this.dgvUtilisateurs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUtilisateurs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUtilisateurs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUtilisateurs.Location = new System.Drawing.Point(15, 50);
            this.dgvUtilisateurs.MultiSelect = false;
            this.dgvUtilisateurs.Name = "dgvUtilisateurs";
            this.dgvUtilisateurs.ReadOnly = true;
            this.dgvUtilisateurs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUtilisateurs.Size = new System.Drawing.Size(450, 385);
            this.dgvUtilisateurs.TabIndex = 1;
            this.dgvUtilisateurs.SelectionChanged += new System.EventHandler(this.dgvUtilisateurs_SelectionChanged);
            // 
            // grpDetails
            // 
            this.grpDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDetails.Controls.Add(this.chkEstActif);
            this.grpDetails.Controls.Add(this.cboRole);
            this.grpDetails.Controls.Add(this.lblRole);
            this.grpDetails.Controls.Add(this.txtEmail);
            this.grpDetails.Controls.Add(this.lblEmail);
            this.grpDetails.Controls.Add(this.txtMotDePasse);
            this.grpDetails.Controls.Add(this.lblMotDePasse);
            this.grpDetails.Controls.Add(this.txtNomUtilisateur);
            this.grpDetails.Controls.Add(this.lblNomUtilisateur);
            this.grpDetails.Controls.Add(this.txtPrenom);
            this.grpDetails.Controls.Add(this.lblPrenom);
            this.grpDetails.Controls.Add(this.txtNom);
            this.grpDetails.Controls.Add(this.lblNom);
            this.grpDetails.Controls.Add(this.txtId);
            this.grpDetails.Controls.Add(this.lblId);
            this.grpDetails.Location = new System.Drawing.Point(480, 50);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(305, 300);
            this.grpDetails.TabIndex = 2;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Détails de l\'utilisateur";
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
            this.txtNom.Location = new System.Drawing.Point(120, 57);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(160, 20);
            this.txtNom.TabIndex = 3;
            // 
            // lblPrenom
            // 
            this.lblPrenom.AutoSize = true;
            this.lblPrenom.Location = new System.Drawing.Point(20, 90);
            this.lblPrenom.Name = "lblPrenom";
            this.lblPrenom.Size = new System.Drawing.Size(46, 13);
            this.lblPrenom.TabIndex = 4;
            this.lblPrenom.Text = "Prénom:";
            // 
            // txtPrenom
            // 
            this.txtPrenom.Location = new System.Drawing.Point(120, 87);
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.Size = new System.Drawing.Size(160, 20);
            this.txtPrenom.TabIndex = 5;
            // 
            // lblNomUtilisateur
            // 
            this.lblNomUtilisateur.AutoSize = true;
            this.lblNomUtilisateur.Location = new System.Drawing.Point(20, 120);
            this.lblNomUtilisateur.Name = "lblNomUtilisateur";
            this.lblNomUtilisateur.Size = new System.Drawing.Size(84, 13);
            this.lblNomUtilisateur.TabIndex = 6;
            this.lblNomUtilisateur.Text = "Nom utilisateur:";
            // 
            // txtNomUtilisateur
            // 
            this.txtNomUtilisateur.Location = new System.Drawing.Point(120, 117);
            this.txtNomUtilisateur.Name = "txtNomUtilisateur";
            this.txtNomUtilisateur.Size = new System.Drawing.Size(160, 20);
            this.txtNomUtilisateur.TabIndex = 7;
            // 
            // lblMotDePasse
            // 
            this.lblMotDePasse.AutoSize = true;
            this.lblMotDePasse.Location = new System.Drawing.Point(20, 150);
            this.lblMotDePasse.Name = "lblMotDePasse";
            this.lblMotDePasse.Size = new System.Drawing.Size(77, 13);
            this.lblMotDePasse.TabIndex = 8;
            this.lblMotDePasse.Text = "Mot de passe:";
            // 
            // txtMotDePasse
            // 
            this.txtMotDePasse.Location = new System.Drawing.Point(120, 147);
            this.txtMotDePasse.Name = "txtMotDePasse";
            this.txtMotDePasse.PasswordChar = '*';
            this.txtMotDePasse.Size = new System.Drawing.Size(160, 20);
            this.txtMotDePasse.TabIndex = 9;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(20, 180);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 13);
            this.lblEmail.TabIndex = 10;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(120, 177);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(160, 20);
            this.txtEmail.TabIndex = 11;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(20, 210);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(32, 13);
            this.lblRole.TabIndex = 12;
            this.lblRole.Text = "Rôle:";
            // 
            // cboRole
            // 
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.FormattingEnabled = true;
            this.cboRole.Items.AddRange(new object[] {
            "Admin",
            "Utilisateur"});
            this.cboRole.Location = new System.Drawing.Point(120, 207);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(160, 21);
            this.cboRole.TabIndex = 13;
            // 
            // chkEstActif
            // 
            this.chkEstActif.AutoSize = true;
            this.chkEstActif.Location = new System.Drawing.Point(120, 240);
            this.chkEstActif.Name = "chkEstActif";
            this.chkEstActif.Size = new System.Drawing.Size(64, 17);
            this.chkEstActif.TabIndex = 14;
            this.chkEstActif.Text = "Est actif";
            this.chkEstActif.UseVisualStyleBackColor = true;
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
            // UCGestionUtilisateurs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.btnEnregistrer);
            this.Controls.Add(this.btnNouveau);
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.dgvUtilisateurs);
            this.Controls.Add(this.lblTitre);
            this.Name = "UCGestionUtilisateurs";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.UCGestionUtilisateurs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUtilisateurs)).EndInit();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.DataGridView dgvUtilisateurs;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.Label lblPrenom;
        private System.Windows.Forms.TextBox txtPrenom;
        private System.Windows.Forms.Label lblNomUtilisateur;
        private System.Windows.Forms.TextBox txtNomUtilisateur;
        private System.Windows.Forms.Label lblMotDePasse;
        private System.Windows.Forms.TextBox txtMotDePasse;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.CheckBox chkEstActif;
        private System.Windows.Forms.Button btnNouveau;
        private System.Windows.Forms.Button btnEnregistrer;
        private System.Windows.Forms.Button btnSupprimer;

        private void UCGestionUtilisateurs_Load(object sender, EventArgs e)
        {
            ChargerUtilisateurs();
            cboRole.SelectedIndex = 1; // Sélectionner "Utilisateur" par défaut
        }

        private void ChargerUtilisateurs()
        {
            try
            {
                List<Utilisateur> utilisateurs = _repositoryUtilisateur.ObtenirTous();
                dgvUtilisateurs.DataSource = utilisateurs;

                // Configurer l'affichage des colonnes
                dgvUtilisateurs.Columns["Id"].Width = 50;
                dgvUtilisateurs.Columns["Nom"].Width = 100;
                dgvUtilisateurs.Columns["Prenom"].HeaderText = "Prénom";
                dgvUtilisateurs.Columns["Prenom"].Width = 100;
                dgvUtilisateurs.Columns["NomUtilisateur"].HeaderText = "Nom d'utilisateur";
                dgvUtilisateurs.Columns["NomUtilisateur"].Width = 120;
                dgvUtilisateurs.Columns["MotDePasse"].Visible = false;
                dgvUtilisateurs.Columns["Email"].Width = 150;
                dgvUtilisateurs.Columns["Role"].HeaderText = "Rôle";
                dgvUtilisateurs.Columns["EstActif"].HeaderText = "Actif";
                dgvUtilisateurs.Columns["DateCreation"].Visible = false;
                dgvUtilisateurs.Columns["DateModification"].Visible = false;

                ViderChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des utilisateurs : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViderChamps()
        {
            txtId.Text = string.Empty;
            txtNom.Text = string.Empty;
            txtPrenom.Text = string.Empty;
            txtNomUtilisateur.Text = string.Empty;
            txtMotDePasse.Text = string.Empty;
            txtEmail.Text = string.Empty;
            cboRole.SelectedIndex = 1; // "Utilisateur" par défaut
            chkEstActif.Checked = true;
            _utilisateurSelectionne = null;
            btnSupprimer.Enabled = false;
        }

        private void dgvUtilisateurs_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUtilisateurs.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvUtilisateurs.SelectedRows[0].Cells["Id"].Value);
                _utilisateurSelectionne = _repositoryUtilisateur.ObtenirParId(id);
                if (_utilisateurSelectionne != null)
                {
                    txtId.Text = _utilisateurSelectionne.Id.ToString();
                    txtNom.Text = _utilisateurSelectionne.Nom;
                    txtPrenom.Text = _utilisateurSelectionne.Prenom;
                    txtNomUtilisateur.Text = _utilisateurSelectionne.NomUtilisateur;
                    txtMotDePasse.Text = string.Empty; // Ne pas afficher le mot de passe
                    txtEmail.Text = _utilisateurSelectionne.Email;
                    cboRole.SelectedItem = _utilisateurSelectionne.Role;
                    chkEstActif.Checked = _utilisateurSelectionne.EstActif;
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
                if (string.IsNullOrWhiteSpace(txtNom.Text) || string.IsNullOrWhiteSpace(txtPrenom.Text) || 
                    string.IsNullOrWhiteSpace(txtNomUtilisateur.Text))
                {
                    MessageBox.Show("Le nom, le prénom et le nom d'utilisateur sont obligatoires.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_utilisateurSelectionne == null && string.IsNullOrWhiteSpace(txtMotDePasse.Text))
                {
                    MessageBox.Show("Le mot de passe est obligatoire pour un nouvel utilisateur.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_utilisateurSelectionne == null)
                {
                    // Nouvel utilisateur
                    Utilisateur nouvelUtilisateur = new Utilisateur
                    {
                        Nom = txtNom.Text,
                        Prenom = txtPrenom.Text,
                        NomUtilisateur = txtNomUtilisateur.Text,
                        MotDePasse = _serviceAuthentification.HashMotDePasse(txtMotDePasse.Text),
                        Email = txtEmail.Text,
                        Role = cboRole.SelectedItem.ToString(),
                        EstActif = chkEstActif.Checked
                    };

                    int id = _repositoryUtilisateur.Ajouter(nouvelUtilisateur);
                    MessageBox.Show("Utilisateur ajouté avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Modification d'un utilisateur existant
                    _utilisateurSelectionne.Nom = txtNom.Text;
                    _utilisateurSelectionne.Prenom = txtPrenom.Text;
                    _utilisateurSelectionne.NomUtilisateur = txtNomUtilisateur.Text;
                    _utilisateurSelectionne.Email = txtEmail.Text;
                    _utilisateurSelectionne.Role = cboRole.SelectedItem.ToString();
                    _utilisateurSelectionne.EstActif = chkEstActif.Checked;

                    bool resultat = _repositoryUtilisateur.Modifier(_utilisateurSelectionne);

                    // Si un nouveau mot de passe a été saisi, le mettre à jour
                    if (!string.IsNullOrWhiteSpace(txtMotDePasse.Text))
                    {
                        string motDePasseHash = _serviceAuthentification.HashMotDePasse(txtMotDePasse.Text);
                        resultat = _repositoryUtilisateur.ModifierMotDePasse(_utilisateurSelectionne.Id, motDePasseHash);
                    }

                    if (resultat)
                    {
                        MessageBox.Show("Utilisateur modifié avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la modification de l'utilisateur.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                ChargerUtilisateurs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (_utilisateurSelectionne != null)
            {
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet utilisateur ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bool resultat = _repositoryUtilisateur.Supprimer(_utilisateurSelectionne.Id);
                        if (resultat)
                        {
                            MessageBox.Show("Utilisateur supprimé avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ChargerUtilisateurs();
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la suppression de l'utilisateur.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

