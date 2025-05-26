using System;
using System.Collections.Generic;
using Fumoblilite.Systeme.Modeles;

namespace Fumoblilite.Systeme.Interfaces
{
    public interface IRepositoryHoraire
    {
        List<Horaire> ObtenirParLigne(int ligneId);
        List<Horaire> ObtenirParArret(int arretId);
        List<Horaire> ObtenirParLigneEtJour(int ligneId, DayOfWeek jour);
        List<Horaire> ObtenirParArretEtJour(int arretId, DayOfWeek jour);
        List<Horaire> ObtenirParJour(DayOfWeek jour);

        // Nouvelles méthodes optimisées
        List<Horaire> ObtenirParCriteres(HoraireCriteres criteres);
        List<Horaire> ObtenirProchains(int arretId, DateTime dateHeure, int limite = 10);
        List<Horaire> ObtenirParPlageHoraire(int ligneId, DayOfWeek jour, TimeSpan heureDebut, TimeSpan heureFin);
        bool ExisteHoraire(int ligneId, int arretId, DayOfWeek jour, TimeSpan heure);

        int Ajouter(Horaire horaire);
        int AjouterEnLot(List<Horaire> horaires);
        bool Modifier(Horaire horaire);
        bool Supprimer(int id);
        bool SupprimerParCriteres(HoraireCriteres criteres);

        // Méthodes de statistiques
        Dictionary<DayOfWeek, int> ObtenirStatistiquesParJour(int ligneId);
        List<HoraireStatistique> ObtenirStatistiquesParLigne();
    }

    public class HoraireCriteres
    {
        public int? LigneId { get; set; }
        public int? ArretId { get; set; }
        public DayOfWeek? JourSemaine { get; set; }
        public TimeSpan? HeureDebut { get; set; }
        public TimeSpan? HeureFin { get; set; }
        public bool? EstActif { get; set; }
        public DateTime? DateCreationDebut { get; set; }
        public DateTime? DateCreationFin { get; set; }
        public string TrierPar { get; set; } = "HeureDepart";
        public bool OrdreDecroissant { get; set; } = false;
        public int? Limite { get; set; }
        public int? Offset { get; set; }
    }

    public class HoraireStatistique
    {
        public int LigneId { get; set; }
        public string LigneNom { get; set; }
        public int NombreHoraires { get; set; }
        public int NombreHorairesActifs { get; set; }
        public TimeSpan? PremierHoraire { get; set; }
        public TimeSpan? DernierHoraire { get; set; }
        public double FrequenceMoyenne { get; set; }
    }
}
