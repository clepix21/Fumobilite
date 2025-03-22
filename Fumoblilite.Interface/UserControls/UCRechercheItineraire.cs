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
    public partial class UCRechercheItineraire : UserControl
    {
        private readonly string _connectionString;
        private readonly ServiceItineraire _serviceItineraire;
        private readonly ServiceArret _serviceArret;
        private readonly ServiceLigne _serviceLigne;

        public UCRechercheItineraire(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;

            // Initialisation des repositories
            IRepositoryArret repositoryArret = new RepositoryArret(_connectionString);
            IRepositoryLigne repositoryLigne = new RepositoryLigne(_connectionString);
            IRepositoryArretLigne repositoryArretLigne = new RepositoryArretLigne(_connectionString, repositoryArret);
            IRepositoryHoraire repositoryHoraire = new RepositoryHoraire(_connectionString);

            // Initialisation des services
            _serviceArret = new ServiceArret(repositoryArret);
            _serviceLigne = new ServiceLigne(repositoryLigne, repositoryArretLigne);
            _serviceItineraire = new ServiceItineraire(repositoryArret, repositoryLigne, repositoryArretLigne, repositoryHoraire);
        }

        private void UCRechercheItineraire_Load(object sender, EventArgs e)
        {
            ChargerArrets();
            dtpDate.Value = DateTime.Today;
            dtpHeure.Value = DateTime.Now;
        }

        private void ChargerArrets()
        {
            try
            {
                List<Arret> arrets = _serviceArret.ObtenirTous();

                // Configuration du ComboBox de départ
                cboArretDepart.DisplayMember = "Nom";
                cboArretDepart.ValueMember = "Id";
                cboArretDepart.DataSource = new List<Arret>(arrets);

                // Configuration du ComboBox d'arrivée
                cboArretArrivee.DisplayMember = "Nom";
                cboArretArrivee.ValueMember = "Id";
                cboArretArrivee.DataSource = new List<Arret>(arrets);

                if (arrets.Count > 1)
                {
                    cboArretArrivee.SelectedIndex = 1; // Sélectionner le deuxième arrêt par défaut
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des arrêts : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboArretDepart.SelectedItem == null || cboArretArrivee.SelectedItem == null)
                {
                    MessageBox.Show("Veuillez sélectionner les arrêts de départ et d'arrivée.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int arretDepartId = (int)cboArretDepart.SelectedValue;
                int arretArriveeId = (int)cboArretArrivee.SelectedValue;

                if (arretDepartId == arretArriveeId)
                {
                    MessageBox.Show("Les arrêts de départ et d'arrivée doivent être différents.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Combiner la date et l'heure
                DateTime dateHeure = dtpDate.Value.Date.Add(dtpHeure.Value.TimeOfDay);
                bool estHeureDepart = rbDepart.Checked;

                // Rechercher les itinéraires
                List<Itineraire> itineraires = _serviceItineraire.RechercherItineraires(arretDepartId, arretArriveeId, dateHeure, estHeureDepart);

                if (itineraires.Count == 0)
                {
                    MessageBox.Show("Aucun itinéraire trouvé pour les critères spécifiés.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvItineraires.DataSource = null;
                    lblDetailsItineraire.Text = "Aucun itinéraire trouvé";
                    return;
                }

                // Préparer les données pour l'affichage dans le DataGridView
                var itinerairesAffichage = itineraires.Select(i => new
                {
                    Départ = i.HeureDepart.ToShortTimeString(),
                    Arrivée = i.HeureArrivee.ToShortTimeString(),
                    Durée = $"{i.DureeMinutes} min",
                    Changements = i.NombreChangements,
                    ItineraireComplet = i // Pour pouvoir accéder à l'objet complet
                }).ToList();

                dgvItineraires.DataSource = itinerairesAffichage;

                // Masquer la colonne de l'objet complet
                dgvItineraires.Columns["ItineraireComplet"].Visible = false;

                // Sélectionner le premier itinéraire
                if (dgvItineraires.Rows.Count > 0)
                {
                    dgvItineraires.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la recherche d'itinéraires : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItineraires_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItineraires.SelectedRows.Count > 0 && dgvItineraires.DataSource != null)
            {
                try
                {
                    // Récupérer l'itinéraire sélectionné
                    dynamic row = dgvItineraires.SelectedRows[0].DataBoundItem;
                    Itineraire itineraire = row.ItineraireComplet;

                    // Afficher les détails de l'itinéraire
                    AfficherDetailsItineraire(itineraire);
                }
                catch (Exception ex)
                {
                    lblDetailsItineraire.Text = $"Erreur lors de l'affichage des détails : {ex.Message}";
                }
            }
            else
            {
                lblDetailsItineraire.Text = "Sélectionnez un itinéraire à gauche";
            }
        }

        private void AfficherDetailsItineraire(Itineraire itineraire)
        {
            // Créer un contrôle FlowLayoutPanel pour afficher les étapes
            FlowLayoutPanel flpEtapes = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                WrapContents = false
            };

            // Ajouter un en-tête
            Label lblEnTete = new Label
            {
                Text = $"Départ : {itineraire.HeureDepart.ToShortTimeString()} - Arrivée : {itineraire.HeureArrivee.ToShortTimeString()}",
                Font = new Font(Font.FontFamily, 10, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(5)
            };
            flpEtapes.Controls.Add(lblEnTete);

            Label lblDuree = new Label
            {
                Text = $"Durée : {itineraire.DureeMinutes} minutes - Changements : {itineraire.NombreChangements}",
                AutoSize = true,
                Margin = new Padding(5)
            };
            flpEtapes.Controls.Add(lblDuree);

            // Ajouter une ligne de séparation
            Panel pnlSeparator = new Panel
            {
                Height = 1,
                Width = pnlDetailsItineraire.Width - 20,
                BackColor = Color.Gray,
                Margin = new Padding(5)
            };
            flpEtapes.Controls.Add(pnlSeparator);

            // Ajouter chaque étape
            foreach (var etape in itineraire.Etapes)
            {
                // Créer un panel pour l'étape
                Panel pnlEtape = new Panel
                {
                    Width = pnlDetailsItineraire.Width - 20,
                    Height = 80,
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.FixedSingle
                };

                // Ajouter un indicateur de ligne (couleur)
                Panel pnlLigne = new Panel
                {
                    Width = 10,
                    Height = pnlEtape.Height,
                    Dock = DockStyle.Left,
                    BackColor = ColorTranslator.FromHtml(etape.CouleurLigne ?? "#808080")
                };
                pnlEtape.Controls.Add(pnlLigne);

                // Ajouter les informations de l'étape
                Label lblLigne = new Label
                {
                    Text = $"Ligne {etape.NomLigne}",
                    Font = new Font(Font.FontFamily, 9, FontStyle.Bold),
                    Location = new Point(20, 5),
                    AutoSize = true
                };
                pnlEtape.Controls.Add(lblLigne);

                Label lblArrets = new Label
                {
                    Text = $"De {etape.NomArretDepart} à {etape.NomArretArrivee}",
                    Location = new Point(20, 25),
                    AutoSize = true
                };
                pnlEtape.Controls.Add(lblArrets);

                Label lblHoraires = new Label
                {
                    Text = $"{etape.HeureDepart.ToShortTimeString()} - {etape.HeureArrivee.ToShortTimeString()} ({etape.DureeMinutes} min)",
                    Location = new Point(20, 45),
                    AutoSize = true
                };
                pnlEtape.Controls.Add(lblHoraires);

                flpEtapes.Controls.Add(pnlEtape);
            }

            // Remplacer le contenu du panel de détails
            pnlDetailsItineraire.Controls.Clear();
            pnlDetailsItineraire.Controls.Add(flpEtapes);
        }
    }
}
