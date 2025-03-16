using System;
using System.Windows.Forms;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Interface.UserControls;
using GestionTransport.Interface.UserControls;

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
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            lblStatusUtilisateur.Text = $"Utilisateur connecté: {_utilisateurConnecte.Prenom} {_utilisateurConnecte.Nom} ({_utilisateurConnecte.Role})";

            if (_utilisateurConnecte.Role != "Admin")
            {
                menuItemUtilisateurs.Visible = false;
            }

            AfficherAccueil();
        }

        private void AfficherAccueil()
        {
            panelContenu.Controls.Clear();
            UCAccueil ucAccueil = new UCAccueil();
            ucAccueil.Dock = DockStyle.Fill;
            panelContenu.Controls.Add(ucAccueil);
        }

        private void menuItemDeconnexion_Click(object sender, EventArgs e)
        {
            this.Close();
            FormConnexion formConnexion = new FormConnexion(_connectionString);
            formConnexion.Show();
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
