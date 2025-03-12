using System;

namespace Fumoblilite.Systeme.Modeles
{
    public class Horaire
    {
        public int Id { get; set; }
        public int LigneId { get; set; }
        public int ArretId { get; set; }
        public DayOfWeek JourSemaine { get; set; }
        public TimeSpan HeureDepart { get; set; }
        public bool EstActif { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateModification { get; set; }

        public Horaire()
        {
            DateCreation = DateTime.Now;
            EstActif = true;
        }

        public Horaire(int id, int ligneId, int arretId, DayOfWeek jourSemaine, TimeSpan heureDepart)
        {
            Id = id;
            LigneId = ligneId;
            ArretId = arretId;
            JourSemaine = jourSemaine;
            HeureDepart = heureDepart;
            EstActif = true;
            DateCreation = DateTime.Now;
        }
    }
}

