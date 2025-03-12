using System;
using System.Collections.Generic;

namespace Fumoblilite.Systeme.Modeles
{
    public class Itineraire
    {
        public int ArretDepartId { get; set; }
        public int ArretArriveeId { get; set; }
        public DateTime HeureDepart { get; set; }
        public DateTime HeureArrivee { get; set; }
        public int DureeMinutes { get; set; }
        public int NombreChangements { get; set; }
        public List<EtapeItineraire> Etapes { get; set; }

        public Itineraire()
        {
            Etapes = new List<EtapeItineraire>();
        }
    }

    public class EtapeItineraire
    {
        public int LigneId { get; set; }
        public string NomLigne { get; set; }
        public string CouleurLigne { get; set; }
        public int ArretDepartId { get; set; }
        public string NomArretDepart { get; set; }
        public int ArretArriveeId { get; set; }
        public string NomArretArrivee { get; set; }
        public DateTime HeureDepart { get; set; }
        public DateTime HeureArrivee { get; set; }
        public int DureeMinutes { get; set; }
    }
}

