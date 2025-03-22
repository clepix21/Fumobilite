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
