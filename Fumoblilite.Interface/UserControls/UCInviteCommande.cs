using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Systeme.Services;
using Fumoblilite.SQL.Repositories;
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

        // Services
        private readonly ServiceArret _serviceArret;
        private readonly ServiceLigne _serviceLigne;
        private readonly ServiceHoraire _serviceHoraire;
        private readonly ServiceAuthentification _serviceAuth;

        public UCInviteCommande(string connectionString, Utilisateur utilisateur, FormPrincipal formPrincipal)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _utilisateurConnecte = utilisateur;
            _formPrincipal = formPrincipal;
            _historiqueCommandes = new List<string>();
            _indexHistorique = -1;

            // Initialiser les services
            var repositoryArret = new RepositoryArret(_connectionString);
            var repositoryLigne = new RepositoryLigne(_connectionString);
            var repositoryArretLigne = new RepositoryArretLigne(_connectionString, repositoryArret);
            var repositoryHoraire = new RepositoryHoraire(_connectionString);
            var repositoryUtilisateur = new RepositoryUtilisateur(_connectionString);

            _serviceArret = new ServiceArret(repositoryArret);
            _serviceLigne = new ServiceLigne(repositoryLigne, repositoryArretLigne);
            _serviceHoraire = new ServiceHoraire(repositoryHoraire);
            _serviceAuth = new ServiceAuthentification(repositoryUtilisateur);

            InitialiserInterface();
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
            AjouterTexte("Console interactive - Toutes les opérations se font ici", Color.Gray);
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
            TraiterCommande(commande);

            // Réinitialiser le champ de saisie
            txtCommande.Clear();

            // Afficher le nouveau prompt
            AfficherPrompt();
        }

        private void TraiterCommande(string commande)
        {
            try
            {
                string[] parties = commande.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parties.Length == 0) return;

                string commandePrincipale = parties[0].ToLower();

                switch (commandePrincipale)
                {
                    case "help":
                        AfficherAide();
                        break;

                    case "clear":
                        rtbConsole.Clear();
                        InitialiserInterface();
                        return;

                    case "status":
                        AfficherStatus();
                        break;

                    case "exit":
                    case "fermer":
                        FermerConsole();
                        return;

                    case "quitter":
                        Application.Exit();
                        return;

                    // === COMMANDES ARRÊTS ===
                    case "arrets":
                        TraiterCommandeArrets(parties);
                        break;

                    case "arret":
                        TraiterCommandeArret(parties);
                        break;

                    // === COMMANDES LIGNES ===
                    case "lignes":
                        TraiterCommandeLignes(parties);
                        break;

                    case "ligne":
                        TraiterCommandeLigne(parties);
                        break;

                    // === COMMANDES HORAIRES ===
                    case "horaires":
                        TraiterCommandeHoraires(parties);
                        break;

                    // === COMMANDES RÉSEAU ===
                    case "reseau":
                        AfficherReseau();
                        break;

                    // === COMMANDES ITINÉRAIRE ===
                    case "itineraire":
                        TraiterCommandeItineraire(parties);
                        break;

                    // === COMMANDES UTILISATEURS ===
                    case "utilisateurs":
                        TraiterCommandeUtilisateurs(parties);
                        break;

                    default:
                        AjouterTexte($"Commande '{commandePrincipale}' non reconnue. Tapez 'help' pour voir les commandes disponibles.", Color.Red);
                        break;
                }
            }
            catch (Exception ex)
            {
                AjouterTexte($"Erreur lors de l'exécution: {ex.Message}", Color.Red);
            }
        }

        private void TraiterCommandeArrets(string[] parties)
        {
            if (_utilisateurConnecte?.Role != "Admin")
            {
                AjouterTexte("Accès refusé. Droits administrateur requis.", Color.Red);
                return;
            }

            if (parties.Length == 1)
            {
                // Lister tous les arrêts
                var arrets = _serviceArret.ObtenirTous();
                AjouterTexte("=== LISTE DES ARRÊTS ===", Color.Cyan);
                AjouterTexte($"{"ID",-5} {"Nom",-25} {"Adresse",-30} {"Accessible",-10}", Color.Yellow);
                AjouterTexte(new string('-', 75), Color.Gray);

                foreach (var arret in arrets)
                {
                    string accessible = arret.EstAccessible ? "Oui" : "Non";
                    AjouterTexte($"{arret.Id,-5} {arret.Nom,-25} {arret.Adresse,-30} {accessible,-10}", Color.White);
                }
                AjouterTexte($"\nTotal: {arrets.Count} arrêts", Color.Green);
            }
            else
            {
                string sousCommande = parties[1].ToLower();
                switch (sousCommande)
                {
                    case "add":
                    case "ajouter":
                        AjouterTexte("Usage: arret add <nom> <adresse> <latitude> <longitude> [accessible:true/false]", Color.Yellow);
                        AjouterTexte("Exemple: arret add \"Gare Centrale\" \"Place de la Gare\" 46.5197 6.6323 true", Color.Gray);
                        break;
                    default:
                        AjouterTexte("Sous-commandes disponibles: add, ajouter", Color.Yellow);
                        break;
                }
            }
        }

        private void TraiterCommandeArret(string[] parties)
        {
            if (_utilisateurConnecte?.Role != "Admin")
            {
                AjouterTexte("Accès refusé. Droits administrateur requis.", Color.Red);
                return;
            }

            if (parties.Length < 2)
            {
                AjouterTexte("Usage: arret <action> [paramètres]", Color.Yellow);
                AjouterTexte("Actions: add, edit, delete, info", Color.Gray);
                return;
            }

            string action = parties[1].ToLower();

            switch (action)
            {
                case "add":
                case "ajouter":
                    if (parties.Length >= 6)
                    {
                        try
                        {
                            string nom = parties[2].Trim('"');
                            string adresse = parties[3].Trim('"');
                            double latitude = double.Parse(parties[4]);
                            double longitude = double.Parse(parties[5]);
                            bool accessible = parties.Length > 6 ? bool.Parse(parties[6]) : false;

                            var nouvelArret = new Arret
                            {
                                Nom = nom,
                                Adresse = adresse,
                                Latitude = latitude,
                                Longitude = longitude,
                                EstAccessible = accessible,
                                DateCreation = DateTime.Now
                            };

                            int id = _serviceArret.Ajouter(nouvelArret);
                            AjouterTexte($"Arrêt créé avec succès (ID: {id})", Color.Green);
                            AjouterTexte($"Nom: {nom}", Color.White);
                            AjouterTexte($"Adresse: {adresse}", Color.White);
                            AjouterTexte($"Coordonnées: {latitude}, {longitude}", Color.White);
                            AjouterTexte($"Accessible: {(accessible ? "Oui" : "Non")}", Color.White);
                        }
                        catch (Exception ex)
                        {
                            AjouterTexte($"Erreur lors de la création: {ex.Message}", Color.Red);
                        }
                    }
                    else
                    {
                        AjouterTexte("Usage: arret add <nom> <adresse> <latitude> <longitude> [accessible]", Color.Yellow);
                        AjouterTexte("Exemple: arret add \"Gare Centrale\" \"Place de la Gare\" 46.5197 6.6323 true", Color.Gray);
                    }
                    break;

                case "info":
                    if (parties.Length >= 3)
                    {
                        if (int.TryParse(parties[2], out int id))
                        {
                            var arret = _serviceArret.ObtenirParId(id);
                            if (arret != null)
                            {
                                AjouterTexte($"=== INFORMATIONS ARRÊT {id} ===", Color.Cyan);
                                AjouterTexte($"Nom: {arret.Nom}", Color.White);
                                AjouterTexte($"Adresse: {arret.Adresse}", Color.White);
                                AjouterTexte($"Coordonnées: {arret.Latitude}, {arret.Longitude}", Color.White);
                                AjouterTexte($"Accessible: {(arret.EstAccessible ? "Oui" : "Non")}", Color.White);
                                AjouterTexte($"Créé le: {arret.DateCreation:dd/MM/yyyy HH:mm}", Color.Gray);
                                if (arret.DateModification.HasValue)
                                    AjouterTexte($"Modifié le: {arret.DateModification:dd/MM/yyyy HH:mm}", Color.Gray);
                            }
                            else
                            {
                                AjouterTexte($"Arrêt avec l'ID {id} non trouvé.", Color.Red);
                            }
                        }
                        else
                        {
                            AjouterTexte("ID invalide. Utilisez un nombre entier.", Color.Red);
                        }
                    }
                    else
                    {
                        AjouterTexte("Usage: arret info <id>", Color.Yellow);
                    }
                    break;

                case "delete":
                case "supprimer":
                    if (parties.Length >= 3)
                    {
                        if (int.TryParse(parties[2], out int id))
                        {
                            var arret = _serviceArret.ObtenirParId(id);
                            if (arret != null)
                            {
                                bool resultat = _serviceArret.Supprimer(id);
                                if (resultat)
                                {
                                    AjouterTexte($"Arrêt '{arret.Nom}' (ID: {id}) supprimé avec succès.", Color.Green);
                                }
                                else
                                {
                                    AjouterTexte($"Erreur lors de la suppression de l'arrêt {id}.", Color.Red);
                                }
                            }
                            else
                            {
                                AjouterTexte($"Arrêt avec l'ID {id} non trouvé.", Color.Red);
                            }
                        }
                        else
                        {
                            AjouterTexte("ID invalide. Utilisez un nombre entier.", Color.Red);
                        }
                    }
                    else
                    {
                        AjouterTexte("Usage: arret delete <id>", Color.Yellow);
                    }
                    break;

                default:
                    AjouterTexte("Actions disponibles: add, info, delete", Color.Yellow);
                    break;
            }
        }

        private void TraiterCommandeLignes(string[] parties)
        {
            if (parties.Length == 1)
            {
                // Lister toutes les lignes
                var lignes = _serviceLigne.ObtenirToutes();
                AjouterTexte("=== LISTE DES LIGNES ===", Color.Cyan);
                AjouterTexte($"{"ID",-5} {"Numéro",-8} {"Nom",-20} {"Couleur",-10} {"Active",-8}", Color.Yellow);
                AjouterTexte(new string('-', 55), Color.Gray);

                foreach (var ligne in lignes)
                {
                    string active = ligne.EstActif ? "Oui" : "Non";
                    AjouterTexte($"{ligne.Id,-5} {ligne.Numero,-8} {ligne.Nom,-20} {ligne.Couleur,-10} {active,-8}", Color.White);
                }
                AjouterTexte($"\nTotal: {lignes.Count} lignes", Color.Green);
            }
        }

        private void TraiterCommandeLigne(string[] parties)
        {
            if (parties.Length < 2)
            {
                AjouterTexte("Usage: ligne <action> [paramètres]", Color.Yellow);
                AjouterTexte("Actions: add, info, delete, arrets", Color.Gray);
                return;
            }

            string action = parties[1].ToLower();

            switch (action)
            {
                case "info":
                    if (parties.Length >= 3)
                    {
                        if (int.TryParse(parties[2], out int id))
                        {
                            var ligne = _serviceLigne.ObtenirParId(id);
                            if (ligne != null)
                            {
                                AjouterTexte($"=== INFORMATIONS LIGNE {id} ===", Color.Cyan);
                                AjouterTexte($"Numéro: {ligne.Numero}", Color.White);
                                AjouterTexte($"Nom: {ligne.Nom}", Color.White);
                                AjouterTexte($"Couleur: {ligne.Couleur}", Color.White);
                                AjouterTexte($"Active: {(ligne.EstActif ? "Oui" : "Non")}", Color.White);
                                AjouterTexte($"Créée le: {ligne.DateCreation:dd/MM/yyyy HH:mm}", Color.Gray);
                                if (ligne.DateModification.HasValue)
                                    AjouterTexte($"Modifiée le: {ligne.DateModification:dd/MM/yyyy HH:mm}", Color.Gray);
                            }
                            else
                            {
                                AjouterTexte($"Ligne avec l'ID {id} non trouvée.", Color.Red);
                            }
                        }
                    }
                    else
                    {
                        AjouterTexte("Usage: ligne info <id>", Color.Yellow);
                    }
                    break;

                case "arrets":
                    if (parties.Length >= 3)
                    {
                        if (int.TryParse(parties[2], out int id))
                        {
                            var ligne = _serviceLigne.ObtenirParId(id, true); // Inclure les arrêts

                            if (ligne != null)
                            {
                                AjouterTexte($"=== ARRÊTS DE LA LIGNE {ligne.Nom} ===", Color.Cyan);
                                AjouterTexte($"{"Ordre",-6} {"ID",-5} {"Nom",-25} {"Adresse",-30}", Color.Yellow);
                                AjouterTexte(new string('-', 70), Color.Gray);

                                for (int i = 0; i < ligne.Arrets.Count; i++)
                                {
                                    var arretLigne = ligne.Arrets[i];
                                    // Utiliser ArretId au lieu de IdArret
                                    var arret = _serviceArret.ObtenirParId(arretLigne.ArretId);
                                    if (arret != null)
                                    {
                                        AjouterTexte($"{arretLigne.Ordre,-6} {arret.Id,-5} {arret.Nom,-25} {arret.Adresse,-30}", Color.White);
                                    }
                                }
                                AjouterTexte($"\nTotal: {ligne.Arrets.Count} arrêts", Color.Green);
                            }
                            else
                            {
                                AjouterTexte($"Ligne avec l'ID {id} non trouvée.", Color.Red);
                            }
                        }
                    }
                    else
                    {
                        AjouterTexte("Usage: ligne arrets <id>", Color.Yellow);
                    }
                    break;

                default:
                    AjouterTexte("Actions disponibles: info, arrets", Color.Yellow);
                    break;
            }
        }

        private void TraiterCommandeHoraires(string[] parties)
        {
            AjouterTexte("=== GESTION DES HORAIRES ===", Color.Cyan);
            AjouterTexte("Fonctionnalité en cours de développement...", Color.Yellow);
        }

        private void TraiterCommandeItineraire(string[] parties)
        {
            AjouterTexte("=== RECHERCHE D'ITINÉRAIRE ===", Color.Cyan);
            AjouterTexte("Usage: itineraire <arret_depart> <arret_arrivee>", Color.Yellow);
            AjouterTexte("Exemple: itineraire 1 5", Color.Gray);
            AjouterTexte("Fonctionnalité en cours de développement...", Color.Yellow);
        }

        private void TraiterCommandeUtilisateurs(string[] parties)
        {
            if (_utilisateurConnecte?.Role != "Admin")
            {
                AjouterTexte("Accès refusé. Droits administrateur requis.", Color.Red);
                return;
            }

            AjouterTexte("=== GESTION DES UTILISATEURS ===", Color.Cyan);
            AjouterTexte("Fonctionnalité en cours de développement...", Color.Yellow);
        }

        private void AfficherReseau()
        {
            AjouterTexte("=== RÉSEAU DE TRANSPORT FUMOBILITE ===", Color.Cyan);

            try
            {
                var lignes = _serviceLigne.ObtenirToutes();
                var arrets = _serviceArret.ObtenirTous();

                AjouterTexte($"Statistiques du réseau:", Color.Yellow);
                AjouterTexte($"- Lignes actives: {lignes.Where(l => l.EstActif).Count()}", Color.White);
                AjouterTexte($"- Total lignes: {lignes.Count}", Color.White);
                AjouterTexte($"- Arrêts accessibles: {arrets.Where(a => a.EstAccessible).Count()}", Color.White);
                AjouterTexte($"- Total arrêts: {arrets.Count}", Color.White);
                AjouterTexte("", Color.White);

                AjouterTexte("Lignes disponibles:", Color.Yellow);
                foreach (var ligne in lignes.Where(l => l.EstActif))
                {
                    var arretsLigne = ligne.Arrets; // Utiliser la propriété Arrets de la ligne
                    AjouterTexte($"- {ligne.Nom} (#{ligne.Numero}, {ligne.Couleur}) - {arretsLigne.Count} arrêts", Color.White);
                }
            }
            catch (Exception ex)
            {
                AjouterTexte($"Erreur lors du chargement du réseau: {ex.Message}", Color.Red);
            }
        }

        private void AfficherAide()
        {
            AjouterTexte("=== COMMANDES DISPONIBLES ===", Color.Cyan);
            AjouterTexte("", Color.White);

            AjouterTexte("CONSULTATION:", Color.Yellow);
            AjouterTexte("  reseau                    - Afficher le réseau de transport", Color.White);
            AjouterTexte("  lignes                    - Lister toutes les lignes", Color.White);
            AjouterTexte("  ligne info <id>           - Informations sur une ligne", Color.White);
            AjouterTexte("  ligne arrets <id>         - Arrêts d'une ligne", Color.White);
            AjouterTexte("  itineraire <dep> <arr>    - Rechercher un itinéraire", Color.White);
            AjouterTexte("", Color.White);

            if (_utilisateurConnecte?.Role == "Admin")
            {
                AjouterTexte("ADMINISTRATION:", Color.Yellow);
                AjouterTexte("  arrets                    - Lister tous les arrêts", Color.White);
                AjouterTexte("  arret add <nom> <adr> <lat> <lng> [acc] - Créer un arrêt", Color.White);
                AjouterTexte("  arret info <id>           - Informations sur un arrêt", Color.White);
                AjouterTexte("  arret delete <id>         - Supprimer un arrêt", Color.White);
                AjouterTexte("  utilisateurs              - Gestion des utilisateurs", Color.White);
                AjouterTexte("", Color.White);
            }

            AjouterTexte("SYSTÈME:", Color.Yellow);
            AjouterTexte("  status                    - Afficher le statut", Color.White);
            AjouterTexte("  help                      - Afficher cette aide", Color.White);
            AjouterTexte("  clear                     - Effacer la console", Color.White);
            AjouterTexte("  exit/fermer               - Fermer la console", Color.White);
            AjouterTexte("  quitter                   - Quitter l'application", Color.White);
            AjouterTexte("", Color.White);

            AjouterTexte("RACCOURCIS:", Color.Yellow);
            AjouterTexte("  ↑/↓                       - Naviguer dans l'historique", Color.White);
            AjouterTexte("  Tab                       - Auto-complétion", Color.White);
            AjouterTexte("  Échap                     - Fermer la console", Color.White);
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

            try
            {
                var arrets = _serviceArret.ObtenirTous();
                var lignes = _serviceLigne.ObtenirToutes();

                AjouterTexte($"Base de données: Connectée", Color.Green);
                AjouterTexte($"Arrêts en base: {arrets.Count}", Color.White);
                AjouterTexte($"Lignes en base: {lignes.Count}", Color.White);
            }
            catch
            {
                AjouterTexte($"Base de données: Erreur de connexion", Color.Red);
            }

            AjouterTexte($"Heure: {DateTime.Now:HH:mm:ss}", Color.White);
            AjouterTexte($"Date: {DateTime.Now:dd/MM/yyyy}", Color.White);
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

            List<string> commandesBase = new List<string>
            {
                "help", "clear", "status", "exit", "fermer", "quitter",
                "reseau", "lignes", "ligne", "itineraire", "horaires"
            };

            if (_utilisateurConnecte?.Role == "Admin")
            {
                commandesBase.AddRange(new[] { "arrets", "arret", "utilisateurs" });
            }

            var commandesCorrespondantes = commandesBase
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
            // Retourner au mode normal
            _formPrincipal.FermerModeConsole();
        }

        private void UCInviteCommande_Load(object sender, EventArgs e)
        {
            // Donner le focus au champ de saisie
            txtCommande.Focus();
        }
    }
}
