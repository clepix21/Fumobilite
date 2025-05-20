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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFichier = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemConnexion = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFichier,
            this.menuGestion,
            this.menuConsultation,
            this.menuRecherche});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(284, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.menuStrip.ForeColor = System.Drawing.Color.White;
            // 
            // menuFichier
            // 
            this.menuFichier.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemConnexion,
            this.menuItemDeconnexion,
            this.menuItemQuitter});
            this.menuFichier.Name = "menuFichier";
            this.menuFichier.Size = new System.Drawing.Size(54, 20);
            this.menuFichier.Text = "Fichier";
            this.menuFichier.ForeColor = System.Drawing.Color.White;
            // 
            // menuItemConnexion
            // 
            this.menuItemConnexion.Name = "menuItemConnexion";
            this.menuItemConnexion.Size = new System.Drawing.Size(144, 22);
            this.menuItemConnexion.Text = "Connexion";
            this.menuItemConnexion.Click += new System.EventHandler(this.menuItemConnexion_Click);
            // 
            // menuItemDeconnexion
            // 
            this.menuItemDeconnexion.Name = "menuItemDeconnexion";
            this.menuItemDeconnexion.Size = new System.Drawing.Size(144, 22);
            this.menuItemDeconnexion.Text = "Dconnexion";
            this.menuItemDeconnexion.Click += new System.EventHandler(this.menuItemDeconnexion_Click);
            // 
            // menuItemQuitter
            // 
            this.menuItemQuitter.Name = "menuItemQuitter";
            this.menuItemQuitter.Size = new System.Drawing.Size(144, 22);
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
            this.menuGestion.ForeColor = System.Drawing.Color.White;
            // 
            // menuItemArrets
            // 
            this.menuItemArrets.Name = "menuItemArrets";
            this.menuItemArrets.Size = new System.Drawing.Size(132, 22);
            this.menuItemArrets.Text = "Arrêt";
            this.menuItemArrets.Click += new System.EventHandler(this.menuItemArrets_Click);
            // 
            // menuItemLignes
            // 
            this.menuItemLignes.Name = "menuItemLignes";
            this.menuItemLignes.Size = new System.Drawing.Size(132, 22);
            this.menuItemLignes.Text = "Lignes";
            this.menuItemLignes.Click += new System.EventHandler(this.menuItemLignes_Click);
            // 
            // menuItemHoraires
            // 
            this.menuItemHoraires.Name = "menuItemHoraires";
            this.menuItemHoraires.Size = new System.Drawing.Size(132, 22);
            this.menuItemHoraires.Text = "Horaires";
            this.menuItemHoraires.Click += new System.EventHandler(this.menuItemHoraires_Click);
            // 
            // menuItemUtilisateurs
            // 
            this.menuItemUtilisateurs.Name = "menuItemUtilisateurs";
            this.menuItemUtilisateurs.Size = new System.Drawing.Size(132, 22);
            this.menuItemUtilisateurs.Text = "Utilisateurs";
            this.menuItemUtilisateurs.Click += new System.EventHandler(this.menuItemUtilisateurs_Click);
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
            this.menuConsultation.ForeColor = System.Drawing.Color.White;
            // 
            // menuItemReseau
            // 
            this.menuItemReseau.Name = "menuItemReseau";
            this.menuItemReseau.Size = new System.Drawing.Size(171, 22);
            this.menuItemReseau.Text = "Réseau complet";
            this.menuItemReseau.Click += new System.EventHandler(this.menuItemReseau_Click);
            // 
            // menuItemLigneDetails
            // 
            this.menuItemLigneDetails.Name = "menuItemLigneDetails";
            this.menuItemLigneDetails.Size = new System.Drawing.Size(171, 22);
            this.menuItemLigneDetails.Text = "Détails d\'une ligne";
            this.menuItemLigneDetails.Click += new System.EventHandler(this.menuItemLigneDetails_Click);
            // 
            // menuItemHorairesJournee
            // 
            this.menuItemHorairesJournee.Name = "menuItemHorairesJournee";
            this.menuItemHorairesJournee.Size = new System.Drawing.Size(171, 22);
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
            this.menuRecherche.ForeColor = System.Drawing.Color.White;
            // 
            // menuItemItineraire
            // 
            this.menuItemItineraire.Name = "menuItemItineraire";
            this.menuItemItineraire.Size = new System.Drawing.Size(120, 22);
            this.menuItemItineraire.Text = "Itinraire";
            this.menuItemItineraire.Click += new System.EventHandler(this.menuItemItineraire_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusUtilisateur});
            this.statusStrip.Location = new System.Drawing.Point(0, 239);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(284, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            // 
            // lblStatusUtilisateur
            // 
            this.lblStatusUtilisateur.Name = "lblStatusUtilisateur";
            this.lblStatusUtilisateur.Size = new System.Drawing.Size(118, 17);
            this.lblStatusUtilisateur.Text = "Utilisateur connect: ";
            this.lblStatusUtilisateur.ForeColor = System.Drawing.Color.White;
            // 
            // panelContenu
            // 
            this.panelContenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenu.Location = new System.Drawing.Point(0, 24);
            this.panelContenu.Name = "panelContenu";
            this.panelContenu.Size = new System.Drawing.Size(284, 215);
            this.panelContenu.TabIndex = 0;
            // 
            // FormPrincipal
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.panelContenu);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fumobilité";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private ToolStripMenuItem menuItemConnexion;
    }
}
