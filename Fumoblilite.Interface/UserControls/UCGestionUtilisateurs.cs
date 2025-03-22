using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
