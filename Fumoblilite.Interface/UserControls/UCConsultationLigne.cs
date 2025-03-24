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
    public partial class UCConsultationLigne : UserControl
    {
        private readonly string _connectionString;
        private readonly ServiceLigne _serviceLigne;
        private readonly ServiceArret _serviceArret;
        private readonly ServiceHoraire _serviceHoraire;

        private Ligne _ligneSelectionnee;

        public UCConsultationLigne(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;

            // Initialisation des repositories
            IRepositoryLigne repositoryLigne = new RepositoryLigne(_connectionString);
            IRepositoryArret repositoryArret = new RepositoryArret(_connectionString);
            IRepositoryArretLigne repositoryArretLigne = new RepositoryArretLigne(_connectionString, repositoryArret);
            IRepositoryHoraire repositoryHoraire = new RepositoryHoraire(_connectionString);

            // Initialisation des services
            _serviceLigne = new ServiceLigne(repositoryLigne, repositoryArretLigne);
            _serviceArret = new ServiceArret(repositoryArret);
            _serviceHoraire = new ServiceHoraire(repositoryHoraire);
        }

        private void UCConsultationLigne_Load(object sender, EventArgs e)
        {
            // Charger les lignes et les jours lors du chargement du contrôle
            ChargerLignes();
            ChargerJours();
        }

        private void ChargerLignes()
        {
            try
            {
                // Obtenir toutes les lignes et les assigner à la source de données du comboBox
                List<Ligne> lignes = _serviceLigne.ObtenirToutes();

                cboLigne.DisplayMember = "Nom";
                cboLigne.ValueMember = "Id";
                cboLigne.DataSource = lignes;
            }
            catch (Exception ex)
            {
                // Afficher un message d'erreur en cas d'exception
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

                cboJour.DisplayMember = "Value";
                cboJour.ValueMember = "Key";
                cboJour.DataSource = jours;

                // Sélectionner le jour actuel
                cboJour.SelectedValue = DateTime.Today.DayOfWeek;
            }
            catch (Exception ex)
            {
                // Afficher un message d'erreur en cas d'exception
                MessageBox.Show($"Erreur lors du chargement des jours : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAfficher_Click(object sender, EventArgs e)
        {
            if (cboLigne.SelectedItem == null)
            {
                // Afficher un message d'avertissement si aucune ligne n'est sélectionnée
                MessageBox.Show("Veuillez sélectionner une ligne.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Obtenir l'ID de la ligne sélectionnée et charger ses détails
                int ligneId = (int)cboLigne.SelectedValue;
                _ligneSelectionnee = _serviceLigne.ObtenirParId(ligneId, true);

                if (_ligneSelectionnee == null)
                {
                    // Afficher un message d'erreur si la ligne sélectionnée n'existe pas
                    MessageBox.Show("La ligne sélectionnée n'existe pas.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Afficher les arrêts et les horaires de la ligne sélectionnée
                AfficherArrets();
                AfficherHoraires();
            }
            catch (Exception ex)
            {
                // Afficher un message d'erreur en cas d'exception
                MessageBox.Show($"Erreur lors de l'affichage des détails de la ligne : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AfficherArrets()
        {
            try
            {
                if (_ligneSelectionnee == null || _ligneSelectionnee.Arrets == null)
                {
                    // Si aucune ligne n'est sélectionnée ou si elle n'a pas d'arrêts, vider la source de données du DataGridView
                    dgvArrets.DataSource = null;
                    return;
                }

                // Récupérer les informations des arrêts
                var arrets = _serviceArret.ObtenirTous().ToDictionary(a => a.Id);

                // Préparer les données pour l'affichage
                var arretsAffichage = _ligneSelectionnee.Arrets.Select(al => new
                {
                    Ordre = al.Ordre,
                    Nom = arrets.ContainsKey(al.ArretId) ? arrets[al.ArretId].Nom : $"Arrêt {al.ArretId}",
                    Adresse = arrets.ContainsKey(al.ArretId) ? arrets[al.ArretId].Adresse : "",
                    TempsArret = $"{al.TempsArretMinutes} min",
                    TempsTrajet = $"{al.TempsTrajetSuivantMinutes} min",
                    Accessible = arrets.ContainsKey(al.ArretId) && arrets[al.ArretId].EstAccessible ? "Oui" : "Non"
                }).OrderBy(a => a.Ordre).ToList();

                // Assigner les données préparées à la source de données du DataGridView
                dgvArrets.DataSource = arretsAffichage;
            }
            catch (Exception ex)
            {
                // Afficher un message d'erreur en cas d'exception
                MessageBox.Show($"Erreur lors de l'affichage des arrêts : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AfficherHoraires()
        {
            try
            {
                if (_ligneSelectionnee == null)
                {
                    // Si aucune ligne n'est sélectionnée, vider la source de données du DataGridView
                    dgvHoraires.DataSource = null;
                    return;
                }

                // Obtenir le jour sélectionné
                DayOfWeek jour = (DayOfWeek)cboJour.SelectedValue;

                // Récupérer les horaires de la ligne pour le jour sélectionné
                var horaires = _serviceHoraire.ObtenirParLigneEtJour(_ligneSelectionnee.Id, jour);

                if (horaires.Count == 0)
                {
                    // Si aucun horaire n'est trouvé, vider la source de données du DataGridView
                    dgvHoraires.DataSource = null;
                    return;
                }

                // Récupérer les informations des arrêts
                var arrets = _serviceArret.ObtenirTous().ToDictionary(a => a.Id);

                // Préparer les données pour l'affichage
                var horairesAffichage = horaires.Select(h => new
                {
                    Arrêt = arrets.ContainsKey(h.ArretId) ? arrets[h.ArretId].Nom : $"Arrêt {h.ArretId}",
                    Heure = h.HeureDepart.ToString(@"hh\:mm"),
                    Actif = h.EstActif ? "Oui" : "Non"
                }).OrderBy(h => h.Arrêt).ThenBy(h => h.Heure).ToList();

                // Assigner les données préparées à la source de données du DataGridView
                dgvHoraires.DataSource = horairesAffichage;
            }
            catch (Exception ex)
            {
                // Afficher un message d'erreur en cas d'exception
                MessageBox.Show($"Erreur lors de l'affichage des horaires : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboJour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ligneSelectionnee != null)
            {
                // Afficher les horaires lorsque le jour sélectionné change
                AfficherHoraires();
            }
        }
    }
}
