using System;
using System.Windows.Forms;
using Fumoblilite.Systeme.Services;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.SQL.Repositories;

namespace Fumoblilite.Interface.Forms
{
    public partial class FormConnexion : Form
    {
        private readonly string _connectionString;
        private readonly ServiceAuthentification _serviceAuthentification;
        public Utilisateur UtilisateurConnecte { get; private set; }

        // Constructeur de la classe FormConnexion
        public FormConnexion(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;

            // Initialisation du service d'authentification avec le repository utilisateur
            var repositoryUtilisateur = new RepositoryUtilisateur(_connectionString);
            _serviceAuthentification = new ServiceAuthentification(repositoryUtilisateur);
        }

        // Gestionnaire d'événements pour le clic sur le bouton de connexion
        private void btnConnexion_Click(object sender, EventArgs e)
        {
            string nomUtilisateur = txtNomUtilisateur.Text.Trim();
            string motDePasse = txtMotDePasse.Text;

            // Vérification si les champs nom d'utilisateur et mot de passe sont remplis
            if (string.IsNullOrEmpty(nomUtilisateur) || string.IsNullOrEmpty(motDePasse))
            {
                MessageBox.Show("Veuillez saisir un nom d'utilisateur et un mot de passe.", "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Authentification de l'utilisateur
            Utilisateur utilisateur = _serviceAuthentification.Authentifier(nomUtilisateur, motDePasse);
            if (utilisateur != null)
            {
                UtilisateurConnecte = utilisateur;
                this.Close(); // Fermer le formulaire de connexion si l'authentification réussit
            }
            else
            {
                // Afficher un message d'erreur si l'authentification échoue
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.", "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMotDePasse.Clear();
                txtMotDePasse.Focus();
            }
        }

        // Gestionnaire d'événements pour le clic sur le bouton quitter
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Quitter l'application
        }
    }
}
