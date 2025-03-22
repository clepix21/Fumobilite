using System;
using System.Windows.Forms;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Interface.UserControls;

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
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            if (_utilisateurConnecte != null)
            {
                lblStatusUtilisateur.Text = $"Utilisateur connecté: {_utilisateurConnecte.Prenom} {_utilisateurConnecte.Nom} ({_utilisateurConnecte.Role})";
                menuItemDeconnexion.Visible = true;
                menuItemConnexion.Visible = false; // Masquer le bouton de connexion
            }
            else
            {
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

            AfficherReseau();
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            var message = MessageBox.Show("Êtes-vous sûr de vouloir quitter?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (message == DialogResult.No) {
                e.Cancel = true;
            }
            else {
                Application.Exit();
            }
        }


        private void AfficherReseau()
        {
            panelContenu.Controls.Clear();
            UCConsultationReseau ucConsultationReseau = new UCConsultationReseau(_connectionString);
            ucConsultationReseau.Dock = DockStyle.Fill;
            panelContenu.Controls.Add(ucConsultationReseau);
        }

        private void menuItemConnexion_Click(object sender, EventArgs e)
        {
            FormConnexion formConnexion = new FormConnexion(_connectionString);
            formConnexion.FormClosed += (s, args) =>
            {
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
            this.Hide();
            FormPrincipal formPrincipal = new FormPrincipal(_connectionString, null);
            formPrincipal.Show();
        }

        private void menuItemQuitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItemArrets_Click(object sender, EventArgs e)
        {
            ChargerUserControl(new UCGestionArrets(_connectionString));
        }

        private void menuItemLignes_Click(object sender, EventArgs e)
        {
            ChargerUserControl(new UCGestionLignes(_connectionString));
        }

        private void menuItemHoraires_Click(object sender, EventArgs e)
        {
            ChargerUserControl(new UCGestionHoraires(_connectionString));
        }

        private void menuItemUtilisateurs_Click(object sender, EventArgs e)
        {
            ChargerUserControl(new UCGestionUtilisateurs(_connectionString));
        }

        private void menuItemReseau_Click(object sender, EventArgs e)
        {
            ChargerUserControl(new UCConsultationReseau(_connectionString));
        }

        private void menuItemLigneDetails_Click(object sender, EventArgs e)
        {
            ChargerUserControl(new UCConsultationLigne(_connectionString));
        }

        private void menuItemHorairesJournee_Click(object sender, EventArgs e)
        {
            ChargerUserControl(new UCConsultationHoraires(_connectionString));
        }

        private void menuItemItineraire_Click(object sender, EventArgs e)
        {
            ChargerUserControl(new UCRechercheItineraire(_connectionString));
        }

        private void ChargerUserControl(UserControl uc)
        {
            panelContenu.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelContenu.Controls.Add(uc);
        }
    }
}
