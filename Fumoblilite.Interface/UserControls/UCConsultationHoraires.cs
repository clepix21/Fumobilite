using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
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
                    flpHoraires.Controls.Clear();
                    return;
                }

                // Récupérer les informations des lignes et des arrêts pour l'affichage
                var lignes = _serviceLigne.ObtenirToutes().ToDictionary(l => l.Id);
                var arrets = _serviceArret.ObtenirTous().ToDictionary(a => a.Id);

                // Préparer les données pour l'affichage
                var horairesAffichage = horaires.Select(h => new
                {
                    Horaire = h,
                    Ligne = lignes.ContainsKey(h.LigneId) ? $"{lignes[h.LigneId].Numero} - {lignes[h.LigneId].Nom}" : $"Ligne {h.LigneId}",
                    Arret = arrets.ContainsKey(h.ArretId) ? arrets[h.ArretId].Nom : $"Arrêt {h.ArretId}",
                    Heure = h.HeureDepart.ToString(@"hh\:mm"),
                    Actif = h.EstActif
                }).OrderBy(h => h.Ligne).ThenBy(h => h.Arret).ThenBy(h => h.Heure).ToList();

                // Afficher les horaires dans le FlowLayoutPanel
                AfficherHoraires(horairesAffichage);
            }
            catch (Exception ex)
            {
                // Afficher un message d'erreur en cas d'exception
                MessageBox.Show($"Erreur lors de l'affichage des horaires : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AfficherHoraires<T>(List<T> horairesAffichage)
        {
            // Vider le FlowLayoutPanel
            flpHoraires.Controls.Clear();
            flpHoraires.SuspendLayout();

            // Créer un panel d'en-tête
            Panel headerPanel = new Panel
            {
                Width = flpHoraires.Width - 25,
                Height = 40,
                BackColor = Color.FromArgb(50, 50, 80),
                Margin = new Padding(0, 0, 0, 10)
            };

            // Ajouter les labels d'en-tête
            Label lblHeaderLigne = new Label
            {
                Text = "Ligne",
                ForeColor = Color.White,
                Font = new Font(Font.FontFamily, 10, FontStyle.Bold),
                Location = new Point(20, 10),
                AutoSize = true
            };

            Label lblHeaderArret = new Label
            {
                Text = "Arrêt",
                ForeColor = Color.White,
                Font = new Font(Font.FontFamily, 10, FontStyle.Bold),
                Location = new Point(220, 10),
                AutoSize = true
            };

            Label lblHeaderHeure = new Label
            {
                Text = "Heure",
                ForeColor = Color.White,
                Font = new Font(Font.FontFamily, 10, FontStyle.Bold),
                Location = new Point(420, 10),
                AutoSize = true
            };

            Label lblHeaderActif = new Label
            {
                Text = "Actif",
                ForeColor = Color.White,
                Font = new Font(Font.FontFamily, 10, FontStyle.Bold),
                Location = new Point(520, 10),
                AutoSize = true
            };

            headerPanel.Controls.Add(lblHeaderLigne);
            headerPanel.Controls.Add(lblHeaderArret);
            headerPanel.Controls.Add(lblHeaderHeure);
            headerPanel.Controls.Add(lblHeaderActif);

            flpHoraires.Controls.Add(headerPanel);

            // Ajouter une carte pour chaque horaire
            foreach (dynamic horaire in horairesAffichage)
            {
                // Créer un panel pour la carte
                Panel cardPanel = new Panel
                {
                    Width = flpHoraires.Width - 25,
                    Height = 60,
                    BackColor = horaire.Actif ? Color.White : Color.FromArgb(240, 240, 240),
                    Margin = new Padding(0, 0, 0, 5),
                    BorderStyle = BorderStyle.None
                };

                // Ajouter un effet de survol
                cardPanel.MouseEnter += (s, e) =>
                {
                    cardPanel.BackColor = Color.FromArgb(230, 240, 250);
                };
                cardPanel.MouseLeave += (s, e) =>
                {
                    cardPanel.BackColor = horaire.Actif ? Color.White : Color.FromArgb(240, 240, 240);
                };

                // Ajouter une bordure gauche colorée
                Panel borderPanel = new Panel
                {
                    Width = 5,
                    Height = cardPanel.Height,
                    BackColor = horaire.Actif ? Color.FromArgb(50, 120, 200) : Color.Gray,
                    Dock = DockStyle.Left
                };
                cardPanel.Controls.Add(borderPanel);

                // Ajouter les labels pour les informations
                Label lblLigne = new Label
                {
                    Text = horaire.Ligne,
                    Font = new Font(Font.FontFamily, 9, FontStyle.Bold),
                    Location = new Point(20, 10),
                    Width = 180,
                    AutoEllipsis = true
                };

                Label lblArret = new Label
                {
                    Text = horaire.Arret,
                    Location = new Point(220, 10),
                    Width = 180,
                    AutoEllipsis = true
                };

                Label lblHeure = new Label
                {
                    Text = horaire.Heure,
                    Font = new Font(Font.FontFamily, 10, FontStyle.Bold),
                    Location = new Point(420, 10),
                    AutoSize = true
                };

                // Ajouter un indicateur visuel pour l'état actif
                Panel indicateurActif = new Panel
                {
                    Width = 16,
                    Height = 16,
                    BackColor = horaire.Actif ? Color.FromArgb(50, 180, 50) : Color.FromArgb(180, 50, 50),
                    Location = new Point(520, 10),
                    BorderStyle = BorderStyle.None
                };
                indicateurActif.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.FillEllipse(new SolidBrush(indicateurActif.BackColor), 0, 0, 15, 15);
                };

                Label lblActif = new Label
                {
                    Text = horaire.Actif ? "Actif" : "Inactif",
                    Location = new Point(545, 10),
                    AutoSize = true,
                    ForeColor = horaire.Actif ? Color.FromArgb(50, 180, 50) : Color.FromArgb(180, 50, 50)
                };

                // Ajouter des informations supplémentaires
                Label lblInfos = new Label
                {
                    Text = $"ID: {horaire.Horaire.Id}",
                    ForeColor = Color.Gray,
                    Font = new Font(Font.FontFamily, 8),
                    Location = new Point(20, 35),
                    AutoSize = true
                };

                // Ajouter les contrôles au panel
                cardPanel.Controls.Add(lblLigne);
                cardPanel.Controls.Add(lblArret);
                cardPanel.Controls.Add(lblHeure);
                cardPanel.Controls.Add(indicateurActif);
                cardPanel.Controls.Add(lblActif);
                cardPanel.Controls.Add(lblInfos);

                // Ajouter la carte au FlowLayoutPanel
                flpHoraires.Controls.Add(cardPanel);
            }

            flpHoraires.ResumeLayout();
        }
    }
}
