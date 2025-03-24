using System;

namespace Fumoblilite.Systeme.Modeles
{
    // Modèle de données pour un utilisateur
    public class Utilisateur
    {
        public int Id { get; set; } // Identifiant de l'utilisateur
        public string Nom { get; set; } // Nom de l'utilisateur
        public string Prenom { get; set; } // Prénom de l'utilisateur
        public string NomUtilisateur { get; set; } // pseudo de l'utilisateur
        public string MotDePasse { get; set; } // Mot de passe de l'utilisateur
        public string Email { get; set; } // Email de l'utilisateur
        public string Role { get; set; } // Admin ou Utilisateur
        public bool EstActif { get; set; } // Statut de l'utilisateur
        public DateTime DateCreation { get; set; } // Date de création de l'utilisateur
        public DateTime? DateModification { get; set; } // Date de modification de l'utilisateur

        // Constructeur par défaut
        public Utilisateur()
        {
            DateCreation = DateTime.Now;
            EstActif = true;
        }

        // Constructeur avec paramètres
        public Utilisateur(int id, string nom, string prenom, string nomUtilisateur, string motDePasse, string email, string role)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            NomUtilisateur = nomUtilisateur;
            MotDePasse = motDePasse;
            Email = email;
            Role = role;
            EstActif = true;
            DateCreation = DateTime.Now;
        }
    }
}

