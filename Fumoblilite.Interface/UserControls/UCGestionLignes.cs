using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Systeme.Services;
using Fumoblilite.SQL.Repositories;
using Fumoblilite.Systeme.Interfaces;
using System.Linq;

namespace Fumoblilite.Interface.UserControls
{
    public partial class UCGestionLignes : UserControl
    {
        private readonly string _connectionString;
        private readonly ServiceLigne _serviceLigne;
        private readonly ServiceArret _serviceArret;
        private Ligne _ligneSelectionnee;

        public UCGestionLignes(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;

            // Initialisation des repositories
            IRepositoryLigne repositoryLigne = new RepositoryLigne(_connectionString);
            IRepositoryArret repositoryArret = new RepositoryArret(_connectionString);
            IRepositoryArretLigne repositoryArretLigne = new RepositoryArretLigne(_connectionString, repositoryArret);

            // Initialisation des services
            _serviceLigne = new ServiceLigne(repositoryLigne, repositoryArretLigne);
            _serviceArret = new ServiceArret(repositoryArret);
        }

        private void UCGestionLignes_Load(object sender, EventArgs e)
        {
            ChargerLignes();
            ChargerArrets();
        }

        private void ChargerLignes()
        {
            try
            {
                List<Ligne> lignes = _serviceLigne.ObtenirToutes();
                dgvLignes.DataSource = lignes;

                // Configurer l'affichage des colonnes
                dgvLignes.Columns["Id"].Width = 50;
                dgvLignes.Columns["Numero"].HeaderText = "Numéro";
                dgvLignes.Columns["Numero"].Width = 70;
                dgvLignes.Columns["Nom"].Width = 150;
                dgvLignes.Columns["Couleur"].Visible = false;
                dgvLignes.Columns["EstActif"].HeaderText = "Actif";
                dgvLignes.Columns["EstActif"].Width = 50;
                dgvLignes.Columns["DateCreation"].Visible = false;
                dgvLignes.Columns["DateModification"].Visible = false;
                //dgvLignes.Columns["Arrets"].Visible = false;

                ViderChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des lignes : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerArrets()
        {
            try
            {
                List<Arret> arrets = _serviceArret.ObtenirTous();

                cboArret.DisplayMember = "Nom";
                cboArret.ValueMember = "Id";
                cboArret.DataSource = arrets;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des arrêts : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViderChamps()
        {
            txtId.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtNom.Text = string.Empty;
            txtCouleur.Text = "#FF0000"; // Rouge par défaut
            chkEstActif.Checked = true;
            _ligneSelectionnee = null;
            btnSupprimer.Enabled = false;
            dgvArrets.DataSource = null;
        }

        private void dgvLignes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLignes.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvLignes.SelectedRows[0].Cells["Id"].Value);
                _ligneSelectionnee = _serviceLigne.ObtenirParId(id, true);
                if (_ligneSelectionnee != null)
                {
                    txtId.Text = _ligneSelectionnee.Id.ToString();
                    txtNumero.Text = _ligneSelectionnee.Numero;
                    txtNom.Text = _ligneSelectionnee.Nom;
                    txtCouleur.Text = _ligneSelectionnee.Couleur ?? "#FF0000";
                    chkEstActif.Checked = _ligneSelectionnee.EstActif;
                    btnSupprimer.Enabled = true;

                    // Afficher les arrêts de la ligne
                    AfficherArrets();
                }
            }
        }

        private void AfficherArrets()
        {
            try
            {
                if (_ligneSelectionnee == null || _ligneSelectionnee.Arrets == null)
                {
                    dgvArrets.DataSource = null;
                    return;
                }

                // Récupérer les informations des arrêts
                var arrets = _serviceArret.ObtenirTous().ToDictionary(a => a.Id);

                // Préparer les données pour l'affichage
                var arretsAffichage = _ligneSelectionnee.Arrets.Select(al => new
                {
                    Id = al.Id,
                    Ordre = al.Ordre,
                    Nom = arrets.ContainsKey(al.ArretId) ? arrets[al.ArretId].Nom : $"Arrêt {al.ArretId}",
                    TempsArret = $"{al.TempsArretMinutes} min",
                    TempsTrajet = $"{al.TempsTrajetSuivantMinutes} min"
                }).OrderBy(a => a.Ordre).ToList();

                dgvArrets.DataSource = arretsAffichage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'affichage des arrêts : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNouveau_Click(object sender, EventArgs e)
        {
            ViderChamps();
            tabDetails.SelectedIndex = 0; // Afficher l'onglet "Informations"
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNumero.Text) || string.IsNullOrWhiteSpace(txtNom.Text))
                {
                    MessageBox.Show("Le numéro et le nom de la ligne sont obligatoires.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_ligneSelectionnee == null)
                {
                    // Nouvelle ligne
                    Ligne nouvelleLigne = new Ligne
                    {
                        Numero = txtNumero.Text,
                        Nom = txtNom.Text,
                        Couleur = txtCouleur.Text,
                        EstActif = chkEstActif.Checked
                    };

                    int id = _serviceLigne.Ajouter(nouvelleLigne);
                    MessageBox.Show("Ligne ajoutée avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Modification d'une ligne existante
                    _ligneSelectionnee.Numero = txtNumero.Text;
                    _ligneSelectionnee.Nom = txtNom.Text;
                    _ligneSelectionnee.Couleur = txtCouleur.Text;
                    _ligneSelectionnee.EstActif = chkEstActif.Checked;

                    bool resultat = _serviceLigne.Modifier(_ligneSelectionnee);
                    if (resultat)
                    {
                        MessageBox.Show("Ligne modifiée avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la modification de la ligne.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                ChargerLignes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (_ligneSelectionnee != null)
            {
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette ligne ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bool resultat = _serviceLigne.Supprimer(_ligneSelectionnee.Id);
                        if (resultat)
                        {
                            MessageBox.Show("Ligne supprimée avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ChargerLignes();
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la suppression de la ligne.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCouleur_Click(object sender, EventArgs e)
        {
            try
            {
                // Initialiser la boîte de dialogue avec la couleur actuelle
                if (!string.IsNullOrEmpty(txtCouleur.Text) && txtCouleur.Text.StartsWith("#"))
                {
                    string hexColor = txtCouleur.Text.TrimStart('#');
                    if (hexColor.Length == 6)
                    {
                        int r = Convert.ToInt32(hexColor.Substring(0, 2), 16);
                        int g = Convert.ToInt32(hexColor.Substring(2, 2), 16);
                        int b = Convert.ToInt32(hexColor.Substring(4, 2), 16);
                        colorDialog.Color = Color.FromArgb(r, g, b);
                    }
                }

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color color = colorDialog.Color;
                    txtCouleur.Text = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la sélection de la couleur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAjouterArret_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ligneSelectionnee == null)
                {
                    MessageBox.Show("Veuillez d'abord sélectionner ou créer une ligne.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboArret.SelectedItem == null)
                {
                    MessageBox.Show("Veuillez sélectionner un arrêt.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int arretId = (int)cboArret.SelectedValue;
                int ordre = (int)nudOrdre.Value;
                int tempsArretMinutes = (int)nudTempsArret.Value;
                int tempsTrajetSuivantMinutes = (int)nudTempsTrajet.Value;

                // Vérifier si l'arrêt est déjà dans la ligne
                if (_ligneSelectionnee.Arrets.Any(a => a.ArretId == arretId))
                {
                    MessageBox.Show("Cet arrêt est déjà dans la ligne.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Créer un nouvel arrêt de ligne
                ArretLigne arretLigne = new ArretLigne
                {
                    LigneId = _ligneSelectionnee.Id,
                    ArretId = arretId,
                    Ordre = ordre,
                    TempsArretMinutes = tempsArretMinutes,
                    TempsTrajetSuivantMinutes = tempsTrajetSuivantMinutes
                };

                // Ajouter l'arrêt à la ligne
                bool resultat = _serviceLigne.AjouterArret(arretLigne);
                if (resultat)
                {
                    MessageBox.Show("Arrêt ajouté à la ligne avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Rafraîchir la ligne sélectionnée
                    _ligneSelectionnee = _serviceLigne.ObtenirParId(_ligneSelectionnee.Id, true);
                    AfficherArrets();
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout de l'arrêt à la ligne.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupprimerArret_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvArrets.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner un arrêt à supprimer.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int arretLigneId = Convert.ToInt32(dgvArrets.SelectedRows[0].Cells["Id"].Value);

                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet arrêt de la ligne ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    bool resultat = _serviceLigne.SupprimerArret(arretLigneId);
                    if (resultat)
                    {
                        MessageBox.Show("Arrêt supprimé de la ligne avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Rafraîchir la ligne sélectionnée
                        _ligneSelectionnee = _serviceLigne.ObtenirParId(_ligneSelectionnee.Id, true);
                        AfficherArrets();
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la suppression de l'arrêt de la ligne.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
