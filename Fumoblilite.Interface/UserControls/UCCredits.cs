using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fumoblilite.Interface.UserControls
{
    public partial class UCCredits : UserControl
    {
        // Événement déclenché lorsque le UserControl est fermé
        public event EventHandler OnCreditsClosed;

        // Structure pour les informations des membres
        public struct MembreInfo
        {
            public string Nom;
            public string Role;
            public Image Photo;
        }

        private MembreInfo[] _membres;

        public UCCredits()
        {
            InitializeComponent();
            InitialiserMembresParDefaut();
        }

        private void InitialiserMembresParDefaut()
        {
            _membres = new MembreInfo[]
            {
                new MembreInfo { Nom = "Alice Dubois", Role = "Chef de projet", Photo = CreerImageMembre("AD", Color.FromArgb(52, 152, 219)) },
                new MembreInfo { Nom = "Bob Martin", Role = "Développeur Backend", Photo = CreerImageMembre("BM", Color.FromArgb(155, 89, 182)) },
                new MembreInfo { Nom = "Claire Rousseau", Role = "Développeuse Frontend", Photo = CreerImageMembre("CR", Color.FromArgb(46, 204, 113)) },
                new MembreInfo { Nom = "David Leroy", Role = "Designer UX/UI", Photo = CreerImageMembre("DL", Color.FromArgb(230, 126, 34)) },
                new MembreInfo { Nom = "Emma Moreau", Role = "Analyste Système", Photo = CreerImageMembre("EM", Color.FromArgb(231, 76, 60)) },
                new MembreInfo { Nom = "François Petit", Role = "Testeur QA", Photo = CreerImageMembre("FP", Color.FromArgb(52, 73, 94)) }
            };
        }

        private Image CreerImageMembre(string initiales, Color couleurFond)
        {
            int taille = 80;
            Bitmap bitmap = new Bitmap(taille, taille);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Fond coloré circulaire
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Brush brush = new SolidBrush(couleurFond))
                {
                    g.FillEllipse(brush, 0, 0, taille, taille);
                }

                // Texte des initiales
                using (Font font = new Font("Segoe UI", 24, FontStyle.Bold))
                using (Brush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    g.DrawString(initiales, font, textBrush,
                        new RectangleF(0, 0, taille, taille), sf);
                }
            }

            return bitmap;
        }

        private void UCCredits_Load(object sender, EventArgs e)
        {
            // Mettre à jour l'année actuelle
            lblAnnee.Text = "© " + DateTime.Now.Year.ToString();

            // Configurer l'image du logo si disponible
            try
            {
                if (Properties.Resources.logo != null)
                {
                    using (var ms = new System.IO.MemoryStream(Properties.Resources.logo))
                    {
                        pictureBoxLogo.Image = Image.FromStream(ms);
                    }
                }
            }
            catch
            {
                // Créer un logo par défaut si pas disponible
                pictureBoxLogo.Image = CreerLogoParDefaut();
            }

            // Afficher les membres
            AfficherMembres();

        }

        private Image CreerLogoParDefaut()
        {
            Bitmap bitmap = new Bitmap(120, 60);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Fond dégradé
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, 0, 120, 60),
                    Color.FromArgb(0, 120, 215),
                    Color.FromArgb(0, 90, 180),
                    45f))
                {
                    g.FillRoundedRectangle(brush, new Rectangle(0, 0, 120, 60), 10);
                }

                // Texte
                using (Font font = new Font("Segoe UI", 14, FontStyle.Bold))
                using (Brush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    g.DrawString("FUMO", font, textBrush,
                        new RectangleF(0, 0, 120, 60), sf);
                }
            }
            return bitmap;
        }

        private void AfficherMembres()
        {
            PictureBox[] pictureBoxes = { pictureBoxMembre1, pictureBoxMembre2, pictureBoxMembre3,
                                        pictureBoxMembre4, pictureBoxMembre5, pictureBoxMembre6 };
            Label[] lblNoms = { lblNomMembre1, lblNomMembre2, lblNomMembre3,
                               lblNomMembre4, lblNomMembre5, lblNomMembre6 };
            Label[] lblRoles = { lblRoleMembre1, lblRoleMembre2, lblRoleMembre3,
                                lblRoleMembre4, lblRoleMembre5, lblRoleMembre6 };

            for (int i = 0; i < pictureBoxes.Length && i < _membres.Length; i++)
            {
                pictureBoxes[i].Image = _membres[i].Photo;
                lblNoms[i].Text = _membres[i].Nom;
                lblRoles[i].Text = _membres[i].Role;

                pictureBoxes[i].Visible = true;
                lblNoms[i].Visible = true;
                lblRoles[i].Visible = true;
            }

            // Cacher les contrôles non utilisés
            for (int i = _membres.Length; i < pictureBoxes.Length; i++)
            {
                pictureBoxes[i].Visible = false;
                lblNoms[i].Visible = false;
                lblRoles[i].Visible = false;
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            // Cacher le UserControl
            this.Visible = false;

            // Déclencher l'événement
            OnCreditsClosed?.Invoke(this, EventArgs.Empty);
        }

        // Méthodes publiques pour personnalisation
        public void SetMembres(MembreInfo[] membres)
        {
            _membres = membres ?? new MembreInfo[0];
            if (this.IsHandleCreated)
            {
                AfficherMembres();
            }
        }

        public void SetVersion(string version)
        {
            lblVersion.Text = "Version " + version;
        }

        public void SetApplicationName(string appName)
        {
            lblAppInfo.Text = appName;
        }

        public void SetGroupe(string groupe)
        {
            lblGroupe.Text = groupe;
        }

        public void SetDescription(string description)
        {
            lblDescription.Text = description;
        }

        // Gestionnaires d'événements pour les photos des membres
        private void PictureBoxMembre_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox pb)
            {
                pb.BackColor = Color.FromArgb(240, 240, 240);
                this.Cursor = Cursors.Hand;
            }
        }

        private void PictureBoxMembre_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox pb)
            {
                pb.BackColor = Color.Transparent;
                this.Cursor = Cursors.Default;
            }
        }
    }

    // Extension pour dessiner des rectangles arrondis
    public static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null) throw new ArgumentNullException(nameof(graphics));
            if (brush == null) throw new ArgumentNullException(nameof(brush));

            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddArc(bounds.X, bounds.Y, cornerRadius, cornerRadius, 180, 90);
                path.AddArc(bounds.X + bounds.Width - cornerRadius, bounds.Y, cornerRadius, cornerRadius, 270, 90);
                path.AddArc(bounds.X + bounds.Width - cornerRadius, bounds.Y + bounds.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                path.AddArc(bounds.X, bounds.Y + bounds.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                path.CloseAllFigures();
                graphics.FillPath(brush, path);
            }
        }
    }
}
