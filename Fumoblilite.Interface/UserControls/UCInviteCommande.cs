using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Interface.Forms;

namespace Fumoblilite.Interface.UserControls
{
    public partial class UCInviteCommande : UserControl
    {
        private readonly string _connectionString;
        private readonly Utilisateur _utilisateurConnecte;
        private readonly FormPrincipal _formPrincipal;
        private List<string> _historiqueCommandes;
        private int _indexHistorique;
        private List<string> _commandesDisponibles;

        public UCInviteCommande(string connectionString, Utilisateur utilisateur, FormPrincipal formPrincipal)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _utilisateurConnecte = utilisateur;
            _formPrincipal = formPrincipal;
            _historiqueCommandes = new List<string>();
            _indexHistorique = -1;

            InitialiserCommandesDisponibles();
            InitialiserInterface();
        }

        private void InitialiserCommandesDisponibles()
        {
            _commandesDisponibles = new List<string>
            {
                "reseau", "lignes", "horaires", "itineraire", "credits",
                "connexion", "deconnexion", "quitter", "help", "clear"
            };

            // Ajouter les commandes admin si l'utilisateur est admin
            if (_utilisateurConnecte?.Role == "Admin")
            {
                _commandesDisponibles.AddRange(new[] { "arrets", "utilisateurs" });
            }

            _commandesDisponibles.Sort();
        }

        private void InitialiserInterface()
        {
            // Configuration de l'apparence console
            this.BackColor = Color.Black;

            // Message de bienvenue
            string utilisateur = _utilisateurConnecte?.Prenom ?? "Invité";
            string role = _utilisateurConnecte?.Role ?? "Visiteur";

            AjouterTexte($"=== FUMOBILITE CONSOLE ===", Color.Cyan);
            AjouterTexte($"Utilisateur: {utilisateur} ({role})", Color.Yellow);
            AjouterTexte("Tapez 'help' pour voir les commandes disponibles", Color.Gray);
            AjouterTexte("Appuyez sur Échap pour fermer la console", Color.Gray);
            AjouterTexte("", Color.White);

            AfficherPrompt();
        }

        private void AjouterTexte(string texte, Color couleur)
        {
            rtbConsole.SelectionStart = rtbConsole.TextLength;
            rtbConsole.SelectionLength = 0;
            rtbConsole.SelectionColor = couleur;
            rtbConsole.AppendText(texte + Environment.NewLine);
            rtbConsole.SelectionColor = rtbConsole.ForeColor;
            rtbConsole.ScrollToCaret();
        }

        private void AfficherPrompt()
        {
            string utilisateur = _utilisateurConnecte?.Prenom ?? "invité";
            string prompt = $"[{utilisateur}@fumobilite]$ ";

            rtbConsole.SelectionStart = rtbConsole.TextLength;
            rtbConsole.SelectionLength = 0;
            rtbConsole.SelectionColor = Color.Lime;
            rtbConsole.AppendText(prompt);
            rtbConsole.SelectionColor = rtbConsole.ForeColor;
            rtbConsole.ScrollToCaret();
        }

        private void txtCommande_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    ExecuterCommande();
                    e.Handled = true;
                    break;

                case Keys.Escape:
                    FermerConsole();
                    e.Handled = true;
                    break;

                case Keys.Up:
                    NaviguerHistorique(-1);
                    e.Handled = true;
                    break;

                case Keys.Down:
                    NaviguerHistorique(1);
                    e.Handled = true;
                    break;

                case Keys.Tab:
                    AutoCompleter();
                    e.Handled = true;
                    break;
            }
        }

        private void ExecuterCommande()
        {
            string commande = txtCommande.Text.Trim();

            if (string.IsNullOrEmpty(commande))
            {
                AfficherPrompt();
                return;
            }

            // Afficher la commande dans la console
            AjouterTexte(commande, Color.White);

            // Ajouter à l'historique
            if (!string.IsNullOrEmpty(commande) && (_historiqueCommandes.Count == 0 || _historiqueCommandes.Last() != commande))
            {
                _historiqueCommandes.Add(commande);
            }
            _indexHistorique = _historiqueCommandes.Count;

            // Traiter la commande
            TraiterCommande(commande.ToLower());

            // Réinitialiser le champ de saisie
            txtCommande.Clear();

            // Afficher le nouveau prompt
            AfficherPrompt();
        }

        private void TraiterCommande(string commande)
        {
            string[] parties = commande.Split(' ');
            string commandePrincipale = parties[0];

            switch (commandePrincipale)
            {
                case "help":
                    AfficherAide();
                    break;

                case "clear":
                    rtbConsole.Clear();
                    InitialiserInterface();
                    return; // Ne pas afficher le prompt car InitialiserInterface() le fait

                case "status":
                    AfficherStatus();
                    break;

                case "reseau":
                case "lignes":
                case "arrets":
                case "horaires":
                case "utilisateurs":
                case "itineraire":
                case "credits":
                case "connexion":
                case "deconnexion":
                case "quitter":
                    try
                    {
                        _formPrincipal.ExecuterCommande(commande);
                        AjouterTexte($"Commande '{commandePrincipale}' exécutée avec succès.", Color.Green);

                        // Fermer la console après exécution d'une commande de navigation
                        if (commandePrincipale != "connexion" && commandePrincipale != "deconnexion")
                        {
                            FermerConsole();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        AjouterTexte($"Erreur lors de l'exécution: {ex.Message}", Color.Red);
                    }
                    break;

                default:
                    AjouterTexte($"Commande '{commandePrincipale}' non reconnue. Tapez 'help' pour voir les commandes disponibles.", Color.Red);
                    break;
            }
        }

        private void AfficherAide()
        {
            AjouterTexte("=== COMMANDES DISPONIBLES ===", Color.Cyan);
            AjouterTexte("", Color.White);

            AjouterTexte("NAVIGATION:", Color.Yellow);
            AjouterTexte("  reseau      - Afficher le réseau de transport", Color.White);
            AjouterTexte("  lignes      - Gestion/consultation des lignes", Color.White);
            AjouterTexte("  horaires    - Gestion/consultation des horaires", Color.White);
            AjouterTexte("  itineraire  - Recherche d'itinéraire", Color.White);
            AjouterTexte("  credits     - Afficher les crédits", Color.White);
            AjouterTexte("", Color.White);

            if (_utilisateurConnecte?.Role == "Admin")
            {
                AjouterTexte("ADMINISTRATION:", Color.Yellow);
                AjouterTexte("  arrets      - Gestion des arrêts", Color.White);
                AjouterTexte("  utilisateurs- Gestion des utilisateurs", Color.White);
                AjouterTexte("", Color.White);
            }

            AjouterTexte("SYSTÈME:", Color.Yellow);
            AjouterTexte("  connexion   - Se connecter", Color.White);
            AjouterTexte("  deconnexion - Se déconnecter", Color.White);
            AjouterTexte("  status      - Afficher le statut", Color.White);
            AjouterTexte("  help        - Afficher cette aide", Color.White);
            AjouterTexte("  clear       - Effacer la console", Color.White);
            AjouterTexte("  quitter     - Quitter l'application", Color.White);
            AjouterTexte("", Color.White);

            AjouterTexte("RACCOURCIS:", Color.Yellow);
            AjouterTexte("  ↑/↓         - Naviguer dans l'historique", Color.White);
            AjouterTexte("  Tab         - Auto-complétion", Color.White);
            AjouterTexte("  Échap       - Fermer la console", Color.White);
        }

        private void AfficherStatus()
        {
            AjouterTexte("=== STATUT SYSTÈME ===", Color.Cyan);

            if (_utilisateurConnecte != null)
            {
                AjouterTexte($"Utilisateur: {_utilisateurConnecte.Prenom} {_utilisateurConnecte.Nom}", Color.Green);
                AjouterTexte($"Rôle: {_utilisateurConnecte.Role}", Color.Green);
                AjouterTexte("Statut: Connecté", Color.Green);
            }
            else
            {
                AjouterTexte("Utilisateur: Non connecté", Color.Yellow);
                AjouterTexte("Statut: Mode invité", Color.Yellow);
            }

            AjouterTexte($"Base de données: Connectée", Color.Green);
            AjouterTexte($"Heure: {DateTime.Now:HH:mm:ss}", Color.White);
        }

        private void NaviguerHistorique(int direction)
        {
            if (_historiqueCommandes.Count == 0) return;

            _indexHistorique += direction;

            if (_indexHistorique < 0)
                _indexHistorique = 0;
            else if (_indexHistorique >= _historiqueCommandes.Count)
            {
                _indexHistorique = _historiqueCommandes.Count;
                txtCommande.Text = "";
                return;
            }

            txtCommande.Text = _historiqueCommandes[_indexHistorique];
            txtCommande.SelectionStart = txtCommande.Text.Length;
        }

        private void AutoCompleter()
        {
            string textePartiel = txtCommande.Text.ToLower();

            if (string.IsNullOrEmpty(textePartiel)) return;

            var commandesCorrespondantes = _commandesDisponibles
                .Where(cmd => cmd.StartsWith(textePartiel))
                .ToList();

            if (commandesCorrespondantes.Count == 1)
            {
                txtCommande.Text = commandesCorrespondantes[0];
                txtCommande.SelectionStart = txtCommande.Text.Length;
            }
            else if (commandesCorrespondantes.Count > 1)
            {
                AjouterTexte($"Suggestions: {string.Join(", ", commandesCorrespondantes)}", Color.Cyan);
                AfficherPrompt();
            }
        }

        private void FermerConsole()
        {
            // Retourner au réseau par défaut
            _formPrincipal.ExecuterCommande("reseau");
        }

        private void UCInviteCommande_Load(object sender, EventArgs e)
        {
            // Donner le focus au champ de saisie
            txtCommande.Focus();
        }
    }
}
