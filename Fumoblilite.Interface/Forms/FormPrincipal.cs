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
        }

        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFichier = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDeconnexion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemQuitter = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGestion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemArrets = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLignes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHoraires = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemUtilisateurs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConsultation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemReseau = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLigneDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHorairesJournee = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRecherche = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemItineraire = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusUtilisateur = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelContenu = new System.Windows.Forms.Panel();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFichier,
            this.menuGestion,
            this.menuConsultation,
            this.menuRecherche});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // menuFichier
            // 
            this.menuFichier.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDeconnexion,
            this.menuItemQuitter});
            this.menuFichier.Name = "menuFichier";
            this.menuFichier.Size = new System.Drawing.Size(54, 20);
            this.menuFichier.Text = "Fichier";
            // 
            // menuItemDeconnexion
            // 
            this.menuItemDeconnexion.Name = "menuItemDeconnexion";
            this.menuItemDeconnexion.Size = new System.Drawing.Size(142, 22);
            this.menuItemDeconnexion.Text = "Déconnexion";
            this.menuItemDeconnexion.Click += new System.EventHandler(this.menuItemDeconnexion_Click);
            // 
            // menuItemQuitter
            // 
            this.menuItemQuitter.Name = "menuItemQuitter";
            this.menuItemQuitter.Size = new System.Drawing.Size(142, 22);
            this.menuItemQuitter.Text = "Quitter";
            this.menuItemQuitter.Click += new System.EventHandler(this.menuItemQuitter_Click);
            // 
            // menuGestion
            // 
            this.menuGestion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemArrets,
            this.menuItemLignes,
            this.menuItemHoraires,
            this.menuItemUtilisateurs});
            this.menuGestion.Name = "menuGestion";
            this.menuGestion.Size = new System.Drawing.Size(59, 20);
            this.menuGestion.Text = "Gestion";
            // 
            // menuItemArrets
            // 
            this.menuItemArrets.Name = "menuItemArrets";
            this.menuItemArrets.Size = new System.Drawing.Size(180, 22);
            this.menuItemArrets.Text = "Arrêts";
            this.menuItemArrets.Click += new System.EventHandler(this.menuItemArrets_Click);
            // 
            // menuItemLignes
            // 
            /*this.menuItemLignes.Name = "menuItemLignes";
            this.menuItemLignes.Size = new System.Drawing.Size(180, 22);
            this.menuItemLignes.Text = "Lignes";
            this.menuItemLignes.Click += new System.EventHandler(this.menuItemLignes_Click);
            // 
            // menuItemHoraires
            // 
            this.menuItemHoraires.Name = "menuItemHoraires";
            this.menuItemHoraires.Size = new System.Drawing.Size(180, 22);
            this.menuItemHoraires.Text = "Horaires";
            this.menuItemHoraires.Click += new System.EventHandler(this.menuItemHoraires_Click);
            // 
            // menuItemUtilisateurs
            // 
            this.menuItemUtilisateurs.Name = "menuItemUtilisateurs";
            this.menuItemUtilisateurs.Size = new System.Drawing.Size(180, 22);
            this.menuItemUtilisateurs.Text = "Utilisateurs";
            this.menuItemUtilisateurs.Click += new System.EventHandler(this.menuItemUtilisateurs_Click);*/
            // 
            // menuConsultation
            // 
            this.menuConsultation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemReseau,
            this.menuItemLigneDetails,
            this.menuItemHorairesJournee});
            this.menuConsultation.Name = "menuConsultation";
            this.menuConsultation.Size = new System.Drawing.Size(87, 20);
            this.menuConsultation.Text = "Consultation";
            // 
            // menuItemReseau
            // 
            this.menuItemReseau.Name = "menuItemReseau";
            this.menuItemReseau.Size = new System.Drawing.Size(180, 22);
            this.menuItemReseau.Text = "Réseau complet";
            this.menuItemReseau.Click += new System.EventHandler(this.menuItemReseau_Click);
            // 
            // menuItemLigneDetails
            // 
            this.menuItemLigneDetails.Name = "menuItemLigneDetails";
            this.menuItemLigneDetails.Size = new System.Drawing.Size(180, 22);
            this.menuItemLigneDetails.Text = "Détails d\'une ligne";
            this.menuItemLigneDetails.Click += new System.EventHandler(this.menuItemLigneDetails_Click);
            // 
            // menuItemHorairesJournee
            // 
            this.menuItemHorairesJournee.Name = "menuItemHorairesJournee";
            this.menuItemHorairesJournee.Size = new System.Drawing.Size(180, 22);
            this.menuItemHorairesJournee.Text = "Horaires du jour";
            this.menuItemHorairesJournee.Click += new System.EventHandler(this.menuItemHorairesJournee_Click);
            // 
            // menuRecherche
            // 
            this.menuRecherche.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemItineraire});
            this.menuRecherche.Name = "menuRecherche";
            this.menuRecherche.Size = new System.Drawing.Size(74, 20);
            this.menuRecherche.Text = "Recherche";
            // 
            // menuItemItineraire
            // 
            this.menuItemItineraire.Name = "menuItemItineraire";
            this.menuItemItineraire.Size = new System.Drawing.Size(180, 22);
            this.menuItemItineraire.Text = "Itinéraire";
            this.menuItemItineraire.Click += new System.EventHandler(this.menuItemItineraire_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusUtilisateur});
            this.statusStrip.Location = new System.Drawing.Point(0, 428);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblStatusUtilisateur
            // 
            this.lblStatusUtilisateur.Name = "lblStatusUtilisateur";
            this.lblStatusUtilisateur.Size = new System.Drawing.Size(118, 17);
            this.lblStatusUtilisateur.Text = "Utilisateur connecté: ";
            // 
            // panelContenu
            // 
            this.panelContenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenu.Location = new System.Drawing.Point(0, 24);
            this.panelContenu.Name = "panelContenu";
            this.panelContenu.Size = new System.Drawing.Size(800, 404);
            this.panelContenu.TabIndex = 2;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelContenu);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion de Transport en Commun";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFichier;
        private System.Windows.Forms.ToolStripMenuItem menuItemDeconnexion;
        private System.Windows.Forms.ToolStripMenuItem menuItemQuitter;
        private System.Windows.Forms.ToolStripMenuItem menuGestion;
        private System.Windows.Forms.ToolStripMenuItem menuItemArrets;
        private System.Windows.Forms.ToolStripMenuItem menuItemLignes;
        private System.Windows.Forms.ToolStripMenuItem menuItemHoraires;
        private System.Windows.Forms.ToolStripMenuItem menuItemUtilisateurs;
        private System.Windows.Forms.ToolStripMenuItem menuConsultation;
        private System.Windows.Forms.ToolStripMenuItem menuItemReseau;
        private System.Windows.Forms.ToolStripMenuItem menuItemLigneDetails;
        private System.Windows.Forms.ToolStripMenuItem menuItemHorairesJournee;
        private System.Windows.Forms.ToolStripMenuItem menuRecherche;
        private System.Windows.Forms.ToolStripMenuItem menuItemItineraire;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusUtilisateur;
        private System.Windows.Forms.Panel panelContenu;

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            // Afficher l'utilisateur connecté dans la barre de statut
            lblStatusUtilisateur.Text = $"Utilisateur connecté: {_utilisateurConnecte.Prenom} {_utilisateurConnecte.Nom} ({_utilisateurConnecte.Role})";

            // Gérer les permissions selon le rôle de l'utilisateur
            if (_utilisateurConnecte.Role != "Admin")
            {
                menuItemUtilisateurs.Visible = false;
            }

            // Afficher l'écran d'accueil
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
            panelContenu.Controls.Clear();
            UCGestionArrets ucGestionArrets = new UCGestionArrets(_connectionString);
            ucGestionArrets.Dock = DockStyle.Fill;
            panelContenu.Controls.Add(ucGestionArrets);
        }
        /*
        private void menuItemLignes_Click(object sender, EventArgs e)
        {
            panelContenu.Controls.Clear();
            UCGestionLignes ucGestionLignes = new UCGestionLignes(_connectionString);
            ucGestionLignes.Dock = DockStyle.Fill;
            panelContenu.Controls.Add(ucGestionLignes);
        }

        private void menuItemHoraires_Click(object sender, EventArgs e)
        {
            panelContenu.Controls.Clear();
            UCGestionHoraires ucGestionHoraires = new UCGestionHoraires(_connectionString);
            ucGestionHoraires.Dock = DockStyle.Fill;
            panelContenu.Controls.Add(ucGestionHoraires);
        }

        private void menuItemUtilisateurs_Click(object sender, EventArgs e)
        {
            panelContenu.Controls.Clear();
            UCGestionUtilisateurs ucGestionUtilisateurs = new UCGestionUtilisateurs(_connectionString);
            ucGestionUtilisateurs.Dock = DockStyle.Fill;
            panelContenu.Controls.Add(ucGestionUtilisateurs);
        }
        */
        private void menuItemReseau_Click(object sender, EventArgs e)
        {
            panelContenu.Controls.Clear();
            UCConsultationReseau ucConsultationReseau = new UCConsultationReseau(_connectionString);
            ucConsultationReseau.Dock = DockStyle.Fill;
            panelContenu.Controls.Add(ucConsultationReseau);
        }

        private void menuItemLigneDetails_Click(object sender, EventArgs e)
        {
            panelContenu.Controls.Clear();
            UCConsultationLigne ucConsultationLigne = new UCConsultationLigne(_connectionString);
            ucConsultationLigne.Dock = DockStyle.Fill;
            panelContenu.Controls.Add(ucConsultationLigne);
        }

        private void menuItemHorairesJournee_Click(object sender, EventArgs e)
        {
            panelContenu.Controls.Clear();
            UCConsultationHoraires ucConsultationHoraires = new UCConsultationHoraires(_connectionString);
            ucConsultationHoraires.Dock = DockStyle.Fill;
            panelContenu.Controls.Add(ucConsultationHoraires);
        }

        private void menuItemItineraire_Click(object sender, EventArgs e)
        {
            panelContenu.Controls.Clear();
            UCRechercheItineraire ucRechercheItineraire = new UCRechercheItineraire(_connectionString);
            ucRechercheItineraire.Dock = DockStyle.Fill;
            panelContenu.Controls.Add(ucRechercheItineraire);
        }
    }
}

