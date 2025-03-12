using System;

namespace Fumoblilite.Systeme.Modeles
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomUtilisateur { get; set; }
        public string MotDePasse { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // Admin, Utilisateur, etc.
        public bool EstActif { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateModification { get; set; }

        public Utilisateur()
        {
            DateCreation = DateTime.Now;
            EstActif = true;
        }

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

