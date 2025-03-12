using System;
using System.Security.Cryptography;
using System.Text;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Systeme.Interfaces;

namespace Fumoblilite.Systeme.Services
{
    public class ServiceAuthentification
    {
        private readonly IRepositoryUtilisateur _repositoryUtilisateur;

        public ServiceAuthentification(IRepositoryUtilisateur repositoryUtilisateur)
        {
            _repositoryUtilisateur = repositoryUtilisateur;
        }

        public Utilisateur Authentifier(string nomUtilisateur, string motDePasse)
        {
            if (string.IsNullOrEmpty(nomUtilisateur) || string.IsNullOrEmpty(motDePasse))
                return null;

            var utilisateur = _repositoryUtilisateur.ObtenirParNomUtilisateur(nomUtilisateur);
            if (utilisateur == null || !utilisateur.EstActif)
                return null;

            string motDePasseHash = HashMotDePasse(motDePasse);
            if (utilisateur.MotDePasse != motDePasseHash)
                return null;

            return utilisateur;
        }

        public string HashMotDePasse(string motDePasse)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(motDePasse));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}

