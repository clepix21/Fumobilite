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

