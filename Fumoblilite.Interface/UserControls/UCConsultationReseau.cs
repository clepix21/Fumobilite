using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Systeme.Services;
using Fumoblilite.SQL.Repositories;
using Fumoblilite.Systeme.Interfaces;

namespace Fumoblilite.Interface.UserControls
{
    public partial class UCConsultationReseau : UserControl
    {
        private readonly string _connectionString;
        private readonly ServiceLigne _serviceLigne;
        private readonly ServiceArret _serviceArret;

        private List<Ligne> _lignes;
        private List<Arret> _arrets;
        private Dictionary<int, bool> _lignesVisibles = new Dictionary<int, bool>();

        public UCConsultationReseau(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;

            // Initialisation des repositories
            IRepositoryLigne repositoryLigne = new RepositoryLigne(_connectionString);
            IRepositoryArret repositoryArret = new RepositoryArret(_connectionString);
            IRepositoryArretLigne repositoryArretLigne = new RepositoryArretLigne(_connectionString, repositoryArret);

            // Initialisation des services
            _serviceLigne = new ServiceLigne(repositoryLigne, repositoryArretLigne);
            _serviceArret = new ServiceArret(repositoryArret);
        }

        private void UCConsultationReseau_Load(object sender, EventArgs e)
        {
            ChargerDonnees();
        }

        private void ChargerDonnees()
        {
            try
            {
                // Charger les lignes et les arrêts
                _lignes = _serviceLigne.ObtenirToutes();
                _arrets = _serviceArret.ObtenirTous();

                // Initialiser la liste des lignes visibles
                foreach (var ligne in _lignes)
                {
                    _lignesVisibles[ligne.Id] = true;
                }

                // Remplir la liste des lignes
                lstLignes.Items.Clear();
                foreach (var ligne in _lignes)
                {
                    lstLignes.Items.Add($"{ligne.Numero} - {ligne.Nom}", true);
                }

                // Rafraîchir l'affichage du réseau
                pnlReseau.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des données : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstLignes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Mettre à jour la visibilité de la ligne
            if (e.Index >= 0 && e.Index < _lignes.Count)
            {
                _lignesVisibles[_lignes[e.Index].Id] = (e.NewValue == CheckState.Checked);

                // Rafraîchir l'affichage du réseau
                pnlReseau.Invalidate();
            }
        }

        private void pnlReseau_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (_lignes == null || _arrets == null)
                    return;

                Graphics g = e.Graphics;
                g.Clear(Color.White);

                // Calculer les coordonnées des arrêts sur le panel
                Dictionary<int, Point> coordonneesArrets = CalculerCoordonneesArrets();

                // Dessiner les lignes
                DessinerLignes(g, coordonneesArrets);

                // Dessiner les arrêts
                DessinerArrets(g, coordonneesArrets);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'affichage du réseau : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Dictionary<int, Point> CalculerCoordonneesArrets()
        {
            Dictionary<int, Point> coordonnees = new Dictionary<int, Point>();

            // Trouver les limites des coordonnées géographiques
            double minLat = _arrets.Min(a => a.Latitude);
            double maxLat = _arrets.Max(a => a.Latitude);
            double minLon = _arrets.Min(a => a.Longitude);
            double maxLon = _arrets.Max(a => a.Longitude);

            // Ajouter une marge
            double margeLatitude = (maxLat - minLat) * 0.1;
            double margeLongitude = (maxLon - minLon) * 0.1;
            minLat -= margeLatitude;
            maxLat += margeLatitude;
            minLon -= margeLongitude;
            maxLon += margeLongitude;

            // Calculer les coordonnées de chaque arrêt sur le panel
            int marge = 30; // Marge en pixels
            int largeurUtile = pnlReseau.Width - 2 * marge;
            int hauteurUtile = pnlReseau.Height - 2 * marge;

            foreach (var arret in _arrets)
            {
                // Normaliser les coordonnées géographiques
                double x = (arret.Longitude - minLon) / (maxLon - minLon);
                double y = 1 - (arret.Latitude - minLat) / (maxLat - minLat); // Inverser l'axe Y

                // Convertir en coordonnées du panel
                int pixelX = marge + (int)(x * largeurUtile);
                int pixelY = marge + (int)(y * hauteurUtile);

                coordonnees[arret.Id] = new Point(pixelX, pixelY);
            }

            return coordonnees;
        }

        private void DessinerLignes(Graphics g, Dictionary<int, Point> coordonneesArrets)
        {
            foreach (var ligne in _lignes)
            {
                // Vérifier si la ligne est visible
                if (!_lignesVisibles[ligne.Id])
                    continue;

                // Récupérer les arrêts de la ligne
                var arretsLigne = _serviceLigne.ObtenirParId(ligne.Id, true)?.Arrets;
                if (arretsLigne == null || arretsLigne.Count == 0)
                    continue;

                // Créer un stylo pour dessiner la ligne
                Color couleurLigne = ColorTranslator.FromHtml(ligne.Couleur ?? "#808080");
                Pen styloLigne = new Pen(couleurLigne, 3);

                // Trier les arrêts par ordre
                var arretsOrdonnes = arretsLigne.OrderBy(al => al.Ordre).ToList();

                // Dessiner les segments de la ligne
                for (int i = 0; i < arretsOrdonnes.Count - 1; i++)
                {
                    int arretId1 = arretsOrdonnes[i].ArretId;
                    int arretId2 = arretsOrdonnes[i + 1].ArretId;

                    if (coordonneesArrets.ContainsKey(arretId1) && coordonneesArrets.ContainsKey(arretId2))
                    {
                        Point p1 = coordonneesArrets[arretId1];
                        Point p2 = coordonneesArrets[arretId2];

                        g.DrawLine(styloLigne, p1, p2);
                    }
                }

                styloLigne.Dispose();
            }
        }

        private void DessinerArrets(Graphics g, Dictionary<int, Point> coordonneesArrets)
        {
            // Créer un stylo et une brosse pour dessiner les arrêts
            Pen styloArret = new Pen(Color.Black, 1);
            Brush brosseArret = new SolidBrush(Color.White);
            Brush brosseArretAccessible = new SolidBrush(Color.LightGreen);
            Font policeNom = new Font("Arial", 8);
            Brush brosseTexte = new SolidBrush(Color.Black);

            foreach (var arret in _arrets)
            {
                if (coordonneesArrets.ContainsKey(arret.Id))
                {
                    Point p = coordonneesArrets[arret.Id];

                    // Dessiner le cercle de l'arrêt
                    g.FillEllipse(arret.EstAccessible ? brosseArretAccessible : brosseArret, p.X - 5, p.Y - 5, 10, 10);
                    g.DrawEllipse(styloArret, p.X - 5, p.Y - 5, 10, 10);

                    // Dessiner le nom de l'arrêt
                    g.DrawString(arret.Nom, policeNom, brosseTexte, p.X + 7, p.Y - 7);
                }
            }

            styloArret.Dispose();
            brosseArret.Dispose();
            brosseArretAccessible.Dispose();
            policeNom.Dispose();
            brosseTexte.Dispose();
        }
    }
}
