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
    public partial class UCConsultationHoraires : UserControl
    {
        private readonly string _connectionString;
        private readonly ServiceLigne _serviceLigne;
        private readonly ServiceArret _serviceArret;
        private readonly ServiceHoraire _serviceHoraire;

        public UCConsultationHoraires(string connectionString)
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

        private void UCConsultationHoraires_Load(object sender, EventArgs e)
        {
            // Charger les lignes et les jours lors du chargement du contrôle
            ChargerLignes();
            ChargerJours();
        }

        private void ChargerLignes()
        {
            try
            {
                // Obtenir toutes les lignes
                List<Ligne> lignes = _serviceLigne.ObtenirToutes();

                // Ajouter une option "Toutes les lignes"
                lignes.Insert(0, new Ligne { Id = 0, Numero = "Toutes", Nom = "Toutes les lignes" });

                // Configurer la source de données du comboBox des lignes
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

                // Configurer la source de données du comboBox des jours
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
            try
            {
                // Récupérer les valeurs sélectionnées
                int ligneId = (int)cboLigne.SelectedValue;
                DayOfWeek jour = (DayOfWeek)cboJour.SelectedValue;

                List<Horaire> horaires;

                // Obtenir les horaires en fonction des critères sélectionnés
                if (ligneId == 0) // Toutes les lignes
                {
                    horaires = _serviceHoraire.ObtenirParJour(jour);
                }
                else
                {
                    horaires = _serviceHoraire.ObtenirParLigneEtJour(ligneId, jour);
                }

                // Vérifier si des horaires ont été trouvés
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
                    Ligne = lignes.ContainsKey(h.LigneId) ? $"{lignes[h.LigneId].Numero} - {lignes[h.LigneId].Nom}" : $"Ligne {h.LigneId}",
                    Arrêt = arrets.ContainsKey(h.ArretId) ? arrets[h.ArretId].Nom : $"Arrêt {h.ArretId}",
                    Heure = h.HeureDepart.ToString(@"hh\:mm"),
                    Actif = h.EstActif ? "Oui" : "Non"
                }).OrderBy(h => h.Ligne).ThenBy(h => h.Arrêt).ThenBy(h => h.Heure).ToList();

                // Afficher les horaires dans le DataGridView
                dgvHoraires.DataSource = horairesAffichage;
            }
            catch (Exception ex)
            {
                // Afficher un message d'erreur en cas d'exception
                MessageBox.Show($"Erreur lors de l'affichage des horaires : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
