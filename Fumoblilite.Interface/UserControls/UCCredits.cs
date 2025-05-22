using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fumoblilite.Interface.UserControls
{
    public partial class UCCredits : UserControl
    {
        // Événement déclenché lorsque le UserControl est fermé
        public event EventHandler OnCreditsClosed;

        public UCCredits()
        {
            InitializeComponent();
        }

        private void UCCredits_Load(object sender, EventArgs e)
        {
            // Mettre à jour l'année actuelle
            lblAnnee.Text = "© " + DateTime.Now.Year.ToString();
            
            // Configurer l'image du logo si disponible
            try
            {
                // Tentative de charger le logo depuis les ressources
                // pictureBox.Image = Properties.Resources.LogoApplication;
            }
            catch
            {
                // Ne rien faire si le logo n'est pas disponible
                pictureBox.Visible = false;
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            // Cacher le UserControl
            this.Visible = false;
            
            // Déclencher un événement pour informer le parent que le UserControl a été fermé
            OnCreditsClosed?.Invoke(this, EventArgs.Empty);
        }

        // Méthode publique pour personnaliser les membres de l'équipe
        public void SetMembres(string[] membres)
        {
            if (membres == null || membres.Length == 0) return;

            Label[] lblMembres = { lblMembre1, lblMembre2, lblMembre3, lblMembre4 };
            
            for (int i = 0; i < lblMembres.Length; i++)
            {
                if (i < membres.Length)
                {
                    lblMembres[i].Text = "• " + membres[i];
                    lblMembres[i].Visible = true;
                }
                else
                {
                    lblMembres[i].Visible = false;
                }
            }
        }

        // Méthode publique pour définir la version de l'application
        public void SetVersion(string version)
        {
            lblVersion.Text = "Version " + version;
        }

        // Méthode publique pour définir le nom de l'application
        public void SetApplicationName(string appName)
        {
            lblAppInfo.Text = appName;
        }

        // Méthode publique pour définir le nom du groupe
        public void SetGroupe(string groupe)
        {
            lblGroupe.Text = groupe;
        }
    }
}
