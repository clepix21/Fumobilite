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

        public FormConnexion(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;

            var repositoryUtilisateur = new RepositoryUtilisateur(_connectionString);
            _serviceAuthentification = new ServiceAuthentification(repositoryUtilisateur);
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            string nomUtilisateur = txtNomUtilisateur.Text.Trim();
            string motDePasse = txtMotDePasse.Text;

            if (string.IsNullOrEmpty(nomUtilisateur) || string.IsNullOrEmpty(motDePasse))
            {
                MessageBox.Show("Veuillez saisir un nom d'utilisateur et un mot de passe.", "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Utilisateur utilisateur = _serviceAuthentification.Authentifier(nomUtilisateur, motDePasse);

            if (utilisateur != null)
            {
                this.Hide();
                FormPrincipal formPrincipal = new FormPrincipal(_connectionString, utilisateur);
                formPrincipal.FormClosed += (s, args) => this.Close();
                formPrincipal.Show();
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.", "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMotDePasse.Clear();
                txtMotDePasse.Focus();
            }
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
