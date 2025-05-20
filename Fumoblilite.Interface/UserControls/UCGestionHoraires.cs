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
    public partial class UCGestionHoraires : UserControl
    {
        private readonly string _connectionString;
        private readonly ServiceHoraire _serviceHoraire;
        private readonly ServiceLigne _serviceLigne;
        private readonly ServiceArret _serviceArret;
        private Horaire _horaireSelectionne;
        private List<dynamic> _horairesAffichage;
        private Panel _selectedPanel;

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
                    flpHoraires.Controls.Clear();
                    _horairesAffichage = null;
                    return;
                }

                // Récupérer les informations des lignes et des arrêts pour l'affichage
                var lignes = _serviceLigne.ObtenirToutes().ToDictionary(l => l.Id);
                var arrets = _serviceArret.ObtenirTous().ToDictionary(a => a.Id);

                // Préparer les données pour l'affichage
                // Replace this line:
                // With this line:
                _horairesAffichage = horaires.Select(h => (dynamic)new
                {
                    Id = h.Id,
                    Ligne = lignes.ContainsKey(h.LigneId) ? $"{lignes[h.LigneId].Numero} - {lignes[h.LigneId].Nom}" : $"Ligne {h.LigneId}",
                    LigneCouleur = lignes.ContainsKey(h.LigneId) ? lignes[h.LigneId].Couleur : "#808080",
                    Arret = arrets.ContainsKey(h.ArretId) ? arrets[h.ArretId].Nom : $"Arrêt {h.ArretId}",
                    Jour = GetJourSemaine((DayOfWeek)h.JourSemaine),
                    Heure = h.HeureDepart.ToString(@"hh\:mm"),
                    Actif = h.EstActif,
                    HoraireComplet = h // Pour pouvoir accéder à l'objet complet
                }).OrderBy(h => h.Ligne).ThenBy(h => h.Arret).ThenBy(h => h.Heure).ToList();

                AfficherHoraires();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'affichage des horaires : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AfficherHoraires()
        {
            flpHoraires.SuspendLayout();
            flpHoraires.Controls.Clear();
            _selectedPanel = null;

            if (_horairesAffichage == null || _horairesAffichage.Count == 0)
            {
                flpHoraires.ResumeLayout();
                return;
            }

            foreach (var horaire in _horairesAffichage)
            {
                Panel panel = CreerCarteHoraire(horaire);
                flpHoraires.Controls.Add(panel);
            }

            flpHoraires.ResumeLayout();
        }

        private Panel CreerCarteHoraire(dynamic horaire)
        {
            // Créer un panel pour la carte
            Panel panel = new Panel
            {
                Width = 420,
                Height = 100,
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle,
                Tag = horaire
            };

            // Ajouter une bordure colorée à gauche selon la couleur de la ligne
            Panel bordureGauche = new Panel
            {
                Width = 10,
                Height = panel.Height,
                Dock = DockStyle.Left
            };

            try
            {
                if (!string.IsNullOrEmpty(horaire.LigneCouleur))
                {
                    string hexColor = horaire.LigneCouleur.TrimStart('#');
                    if (hexColor.Length == 6)
                    {
                        int r = Convert.ToInt32(hexColor.Substring(0, 2), 16);
                        int g = Convert.ToInt32(hexColor.Substring(2, 2), 16);
                        int b = Convert.ToInt32(hexColor.Substring(4, 2), 16);
                        bordureGauche.BackColor = Color.FromArgb(r, g, b);
                    }
                    else
                    {
                        bordureGauche.BackColor = Color.Gray;
                    }
                }
                else
                {
                    bordureGauche.BackColor = Color.Gray;
                }
            }
            catch
            {
                bordureGauche.BackColor = Color.Gray;
            }

            panel.Controls.Add(bordureGauche);

            // Ajouter les informations de l'horaire
            Label lblLigne = new Label
            {
                Text = horaire.Ligne,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 10)
            };
            panel.Controls.Add(lblLigne);

            Label lblArret = new Label
            {
                Text = $"Arrêt: {horaire.Arret}",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(20, 35)
            };
            panel.Controls.Add(lblArret);

            Label lblJour = new Label
            {
                Text = $"Jour: {horaire.Jour}",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(20, 55)
            };
            panel.Controls.Add(lblJour);

            Label lblHeure = new Label
            {
                Text = $"Heure: {horaire.Heure}",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(20, 75)
            };
            panel.Controls.Add(lblHeure);

            // Indicateur d'état actif/inactif
            Panel indicateurActif = new Panel
            {
                Width = 15,
                Height = 15,
                BackColor = horaire.Actif ? Color.Green : Color.Red,
                Location = new Point(panel.Width - 25, 10)
            };
            panel.Controls.Add(indicateurActif);

            Label lblActif = new Label
            {
                Text = horaire.Actif ? "Actif" : "Inactif",
                Font = new Font("Segoe UI", 8),
                AutoSize = true,
                Location = new Point(panel.Width - 70, 10)
            };
            panel.Controls.Add(lblActif);

            // Ajouter un gestionnaire d'événements pour la sélection
            panel.Click += (sender, e) => SelectionnerHoraire(panel);
            foreach (Control control in panel.Controls)
            {
                control.Click += (sender, e) => SelectionnerHoraire(panel);
            }

            return panel;
        }

        private void SelectionnerHoraire(Panel panel)
        {
            // Désélectionner le panel précédemment sélectionné
            if (_selectedPanel != null)
            {
                _selectedPanel.BackColor = SystemColors.Control;
            }

            // Sélectionner le nouveau panel
            _selectedPanel = panel;
            _selectedPanel.BackColor = Color.FromArgb(230, 240, 250);

            // Récupérer l'horaire associé au panel
            dynamic item = panel.Tag;
            _horaireSelectionne = item.HoraireComplet;

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

        private string GetJourSemaine(DayOfWeek jour)
        {
            switch (jour)
            {
                case DayOfWeek.Monday: return "Lundi";
                case DayOfWeek.Tuesday: return "Mardi";
                case DayOfWeek.Wednesday: return "Mercredi";
                case DayOfWeek.Thursday: return "Jeudi";
                case DayOfWeek.Friday: return "Vendredi";
                case DayOfWeek.Saturday: return "Samedi";
                case DayOfWeek.Sunday: return "Dimanche";
                default: return jour.ToString();
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

            // Désélectionner le panel précédemment sélectionné
            if (_selectedPanel != null)
            {
                _selectedPanel.BackColor = SystemColors.Control;
                _selectedPanel = null;
            }
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
