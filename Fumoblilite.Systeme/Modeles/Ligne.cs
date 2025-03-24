using System;
using System.Collections.Generic;

namespace Fumoblilite.Systeme.Modeles
{
    // Modèle de données pour une ligne
    public class Ligne
    {
        public int Id { get; set; } // Identifiant de la ligne
        public string Numero { get; set; } // Numéro de la ligne
        public string Nom { get; set; } // Nom de la ligne
        public string Couleur { get; set; } // Couleur de la ligne
        public bool EstActif { get; set; } // Statut de la ligne
        public DateTime DateCreation { get; set; } // Date de création de la ligne
        public DateTime? DateModification { get; set; } // Date de modification de la ligne
        public List<ArretLigne> Arrets { get; set; } // Liste des arrêts de la ligne

        // Constructeur par défaut
        public Ligne()
        {
            Arrets = new List<ArretLigne>();
            DateCreation = DateTime.Now;
            EstActif = true;
        }

        // Constructeur avec paramètres
        public Ligne(int id, string numero, string nom, string couleur)
        {
            Id = id;
            Numero = numero;
            Nom = nom;
            Couleur = couleur;
            EstActif = true;
            Arrets = new List<ArretLigne>();
            DateCreation = DateTime.Now;
        }
    }
}

