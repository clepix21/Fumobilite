using System;
using System.Collections.Generic;
using GestionTransport.Systeme.Modeles;

namespace GestionTransport.Systeme.Interfaces
{
    public interface IRepositoryHoraire
    {
        List<Horaire> ObtenirParLigne(int ligneId);
        List<Horaire> ObtenirParArret(int arretId);
        List<Horaire> ObtenirParLigneEtJour(int ligneId, DayOfWeek jour);
        List<Horaire> ObtenirParArretEtJour(int arretId, DayOfWeek jour);
        List<Horaire> ObtenirParJour(DayOfWeek jour);
        int Ajouter(Horaire horaire);
        bool Modifier(Horaire horaire);
        bool Supprimer(int id);
    }
}

