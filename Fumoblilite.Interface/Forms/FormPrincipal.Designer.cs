using System.Windows.Forms;

namespace Fumoblilite.Interface.Forms
{
    partial class FormPrincipal
    {
        private MenuStrip menuStrip;
        private ToolStripMenuItem menuFichier, menuItemDeconnexion, menuItemQuitter;
        private ToolStripMenuItem menuGestion, menuItemArrets, menuItemLignes, menuItemHoraires, menuItemUtilisateurs;
        private ToolStripMenuItem menuConsultation, menuItemReseau, menuItemLigneDetails, menuItemHorairesJournee;
        private ToolStripMenuItem menuRecherche, menuItemItineraire;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatusUtilisateur;
        private Panel panelContenu;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));

            this.menuStrip = new MenuStrip();
            this.menuFichier = new ToolStripMenuItem();
            this.menuItemDeconnexion = new ToolStripMenuItem();
            this.menuItemQuitter = new ToolStripMenuItem();
            this.menuGestion = new ToolStripMenuItem();
            this.menuItemArrets = new ToolStripMenuItem();
            this.menuItemLignes = new ToolStripMenuItem();
            this.menuItemHoraires = new ToolStripMenuItem();
            this.menuItemUtilisateurs = new ToolStripMenuItem();
            this.menuConsultation = new ToolStripMenuItem();
            this.menuItemReseau = new ToolStripMenuItem();
            this.menuItemLigneDetails = new ToolStripMenuItem();
            this.menuItemHorairesJournee = new ToolStripMenuItem();
            this.menuRecherche = new ToolStripMenuItem();
            this.menuItemItineraire = new ToolStripMenuItem();
            this.statusStrip = new StatusStrip();
            this.lblStatusUtilisateur = new ToolStripStatusLabel();
            this.panelContenu = new Panel();

            this.menuStrip.Items.AddRange(new ToolStripItem[] { menuFichier, menuGestion, menuConsultation, menuRecherche });
            this.menuFichier.DropDownItems.AddRange(new ToolStripItem[] { menuItemDeconnexion, menuItemQuitter });
            this.menuGestion.DropDownItems.AddRange(new ToolStripItem[] { menuItemArrets, menuItemLignes, menuItemHoraires, menuItemUtilisateurs });
            this.menuConsultation.DropDownItems.AddRange(new ToolStripItem[] { menuItemReseau, menuItemLigneDetails, menuItemHorairesJournee });
            this.menuRecherche.DropDownItems.AddRange(new ToolStripItem[] { menuItemItineraire });

            this.menuFichier.Text = "Fichier";
            this.menuItemDeconnexion.Text = "Déconnexion";
            this.menuItemQuitter.Text = "Quitter";
            this.menuGestion.Text = "Gestion";
            this.menuItemArrets.Text = "Arrêts";
            this.menuItemLignes.Text = "Lignes";
            this.menuItemHoraires.Text = "Horaires";
            this.menuItemUtilisateurs.Text = "Utilisateurs";
            this.menuConsultation.Text = "Consultation";
            this.menuItemReseau.Text = "Réseau complet";
            this.menuItemLigneDetails.Text = "Détails d'une ligne";
            this.menuItemHorairesJournee.Text = "Horaires du jour";
            this.menuRecherche.Text = "Recherche";
            this.menuItemItineraire.Text = "Itinéraire";

            this.menuItemDeconnexion.Click += new System.EventHandler(this.menuItemDeconnexion_Click);
            this.menuItemQuitter.Click += new System.EventHandler(this.menuItemQuitter_Click);
            this.menuItemArrets.Click += new System.EventHandler(this.menuItemArrets_Click);
            this.menuItemLignes.Click += new System.EventHandler(this.menuItemLignes_Click);
            this.menuItemHoraires.Click += new System.EventHandler(this.menuItemHoraires_Click);
            this.menuItemUtilisateurs.Click += new System.EventHandler(this.menuItemUtilisateurs_Click);
            this.menuItemReseau.Click += new System.EventHandler(this.menuItemReseau_Click);
            this.menuItemLigneDetails.Click += new System.EventHandler(this.menuItemLigneDetails_Click);
            this.menuItemHorairesJournee.Click += new System.EventHandler(this.menuItemHorairesJournee_Click);
            this.menuItemItineraire.Click += new System.EventHandler(this.menuItemItineraire_Click);

            this.statusStrip.Items.Add(this.lblStatusUtilisateur);
            this.lblStatusUtilisateur.Text = "Utilisateur connecté: ";

            this.panelContenu.Dock = DockStyle.Fill;
            this.Controls.Add(this.panelContenu);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Text = "Gestion de Transport en Commun";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
        }
    }
}
