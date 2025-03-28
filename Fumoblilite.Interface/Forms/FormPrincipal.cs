using System;
using System.Windows.Forms;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Interface.UserControls;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Fumoblilite.Interface.Forms
{
    public partial class FormPrincipal : Form
    {
        private readonly string _connectionString;
        private readonly Utilisateur _utilisateurConnecte;
        
        public FormPrincipal(string connectionString, Utilisateur utilisateur)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _utilisateurConnecte = utilisateur;
            this.FormClosing += FormPrincipal_FormClosing;

            Task.Run(() =>
            {
                Task.Delay(3000).Wait();
                SetSuspendState(false, true, true);
            });
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            // Vérifier si un utilisateur est connecté
            if (_utilisateurConnecte != null)
            {
                // Afficher les informations de l'utilisateur connecté
                lblStatusUtilisateur.Text = $"Utilisateur connecté: {_utilisateurConnecte.Prenom} {_utilisateurConnecte.Nom} ({_utilisateurConnecte.Role})";
                menuItemDeconnexion.Visible = true;
                menuItemConnexion.Visible = false; // Masquer le bouton de connexion
            }
            else
            {
                // Mode invité
                lblStatusUtilisateur.Text = "Mode invité";
                menuItemDeconnexion.Visible = false;
                menuItemConnexion.Visible = true; // Afficher le bouton de connexion
            }

            // Masquer les éléments de menu de gestion pour les utilisateurs non administrateurs
            if (_utilisateurConnecte == null || _utilisateurConnecte.Role != "Admin")
            {
                menuItemUtilisateurs.Visible = false;
                menuItemArrets.Visible = false;
                menuItemLignes.Visible = false;
                menuItemHoraires.Visible = false;
                menuGestion.Visible = false;
            }

            // Afficher le réseau par défaut
            AfficherReseau();
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Demander confirmation avant de fermer l'application
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var message = MessageBox.Show("Êtes-vous sûr de vouloir quitter?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (message == DialogResult.No)
                {
                    e.Cancel = true; // Annuler la fermeture
                }
                else
                {
                    Application.Exit(); // Fermer l'application
                }
            }
        }

        private void AfficherReseau()
        {
            // Afficher le contrôle utilisateur de consultation du réseau
            panelContenu.Controls.Clear();
            UCConsultationReseau ucConsultationReseau = new UCConsultationReseau(_connectionString);
            ucConsultationReseau.Dock = DockStyle.Fill;
            panelContenu.Controls.Add(ucConsultationReseau);
        }

        private void menuItemConnexion_Click(object sender, EventArgs e)
        {
            // Ouvrir le formulaire de connexion
            FormConnexion formConnexion = new FormConnexion(_connectionString);
            formConnexion.FormClosed += (s, args) =>
            {
                // Si un utilisateur s'est connecté, ouvrir une nouvelle instance de FormPrincipal
                if (formConnexion.UtilisateurConnecte != null)
                {
                    this.Hide();
                    FormPrincipal formPrincipal = new FormPrincipal(_connectionString, formConnexion.UtilisateurConnecte);
                    formPrincipal.Show();
                }
            };
            formConnexion.Show();
        }

        private void menuItemDeconnexion_Click(object sender, EventArgs e)
        {
            // Déconnexion de l'utilisateur et ouverture d'une nouvelle instance de FormPrincipal en mode invité
            this.Hide();
            FormPrincipal formPrincipal = new FormPrincipal(_connectionString, null);
            formPrincipal.Show();
        }

        private void menuItemQuitter_Click(object sender, EventArgs e)
        {
            // Quitter l'application
            Application.Exit();
        }

        private void menuItemArrets_Click(object sender, EventArgs e)
        {
            // Charger le contrôle utilisateur de gestion des arrêts
            ChargerUserControl(new UCGestionArrets(_connectionString));
        }

        private void menuItemLignes_Click(object sender, EventArgs e)
        {
            // Charger le contrôle utilisateur de gestion des lignes
            ChargerUserControl(new UCGestionLignes(_connectionString));
        }

        private void menuItemHoraires_Click(object sender, EventArgs e)
        {
            // Charger le contrôle utilisateur de gestion des horaires
            ChargerUserControl(new UCGestionHoraires(_connectionString));
        }

        private void menuItemUtilisateurs_Click(object sender, EventArgs e)
        {
            // Charger le contrôle utilisateur de gestion des utilisateurs
            ChargerUserControl(new UCGestionUtilisateurs(_connectionString));
        }

        private void menuItemReseau_Click(object sender, EventArgs e)
        {
            // Charger le contrôle utilisateur de consultation du réseau
            ChargerUserControl(new UCConsultationReseau(_connectionString));
        }
        [DllImport("powrprof.dll", SetLastError = true)]
        private static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

        private void menuItemLigneDetails_Click(object sender, EventArgs e)
        {
            // Charger le contrôle utilisateur de consultation des détails de ligne
            ChargerUserControl(new UCConsultationLigne(_connectionString));
        }

        private void menuItemHorairesJournee_Click(object sender, EventArgs e)
        {
            // Charger le contrôle utilisateur de consultation des horaires de la journée
            ChargerUserControl(new UCConsultationHoraires(_connectionString));
        }

        private void menuItemItineraire_Click(object sender, EventArgs e)
        {
            // Charger le contrôle utilisateur de recherche d'itinéraire
            ChargerUserControl(new UCRechercheItineraire(_connectionString));
        }

        private void ChargerUserControl(UserControl uc)
        {
            // Charger un contrôle utilisateur dans le panneau de contenu
            panelContenu.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelContenu.Controls.Add(uc);
        }
    }
}