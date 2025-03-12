using System;

namespace GestionTransport.Systeme.Modeles
{
    public class ArretLigne
    {
        public int Id { get; set; }
        public int LigneId { get; set; }
        public int ArretId { get; set; }
        public int Ordre { get; set; }
        public int TempsArretMinutes { get; set; }
        public int TempsTrajetSuivantMinutes { get; set; }
        public Arret Arret { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateModification { get; set; }

        public ArretLigne()
        {
            DateCreation = DateTime.Now;
        }

        public ArretLigne(int id, int ligneId, int arretId, int ordre, int tempsArretMinutes, int tempsTrajetSuivantMinutes)
        {
            Id = id;
            LigneId = ligneId;
            ArretId = arretId;
            Ordre = ordre;
            TempsArretMinutes = tempsArretMinutes;
            TempsTrajetSuivantMinutes = tempsTrajetSuivantMinutes;
            DateCreation = DateTime.Now;
        }
    }
}

