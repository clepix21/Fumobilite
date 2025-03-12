using System;
using System.Collections.Generic;

namespace Fumoblilite.Systeme.Modeles
{
    public class Arret
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool EstAccessible { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateModification { get; set; }

        public Arret()
        {
            DateCreation = DateTime.Now;
        }

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

