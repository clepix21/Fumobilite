using System;

namespace Fumoblilite.Systeme.Modeles
{
    // Modèle de données pour un arrêt
    public class Arret
    {
        public int Id { get; set; } // Identifiant de l'arrêt
        public string Nom { get; set; } // Nom de l'arrêt
        public string Adresse { get; set; } // Adresse de l'arrêt
        public double Latitude { get; set; } // Latitude de l'arrêt
        public double Longitude { get; set; } // Longitude de l'arrêt
        public bool EstAccessible { get; set; } // Accessibilité de l'arrêt
        public DateTime DateCreation { get; set; } // Date de création de l'arrêt
        public DateTime? DateModification { get; set; } // Date de modification de l'arrêt

        // Constructeur par défaut
        public Arret()
        {
            DateCreation = DateTime.Now;
        }

        // Constructeur avec paramètres
        public Arret(int id, string nom, string adresse, double latitude, double longitude, bool estAccessible)
        {
            Id = id;
            Nom = nom;
            Adresse = adresse;
            Latitude = latitude;
            Longitude = longitude;
            EstAccessible = estAccessible;
            DateCreation = DateTime.Now;
        }
    }
}

