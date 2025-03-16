using System;
using System.Collections.Generic;

namespace Fumoblilite.Systeme.Modeles
{
    public class Ligne
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Nom { get; set; }
        public string Couleur { get; set; }
        public bool EstActif { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateModification { get; set; }
        public List<ArretLigne> Arrets { get; set; }

        public Ligne()
        {
            Arrets = new List<ArretLigne>();
            DateCreation = DateTime.Now;
            EstActif = true;
        }

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

