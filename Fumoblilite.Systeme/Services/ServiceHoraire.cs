using System;
using System.Collections.Generic;
using GestionTransport.Systeme.Modeles;
using GestionTransport.Systeme.Interfaces;

namespace GestionTransport.Systeme.Services
{
    public class ServiceHoraire
    {
        private readonly IRepositoryHoraire _repositoryHoraire;

        public ServiceHoraire(IRepositoryHoraire repositoryHoraire)
        {
            _repositoryHoraire = repositoryHoraire;
        }

        public List<Horaire> ObtenirParLigne(int ligneId)
        {
            return _repositoryHoraire.ObtenirParLigne(ligneId);
        }

        public List<Horaire> ObtenirParArret(int arretId)
        {
            return _repositoryHoraire.ObtenirParArret(arretId);
        }

        public List<Horaire> ObtenirParLigneEtJour(int ligneId, DayOfWeek jour)
        {
            return _repositoryHoraire.ObtenirParLigneEtJour(ligneId, jour);
        }

        public List<Horaire> ObtenirParJour(DayOfWeek jour)
        {
            return _repositoryHoraire.ObtenirParJour(jour);
        }

        public List<Horaire> ObtenirParArretEtJour(int arretId, DayOfWeek jour)
        {
            return _repositoryHoraire.ObtenirParArretEtJour(arretId, jour);
        }

        public int Ajouter(Horaire horaire)
        {
            return _repositoryHoraire.Ajouter(horaire);
        }

        public bool Modifier(Horaire horaire)
        {
            horaire.DateModification = DateTime.Now;
            return _repositoryHoraire.Modifier(horaire);
        }

        public bool Supprimer(int id)
        {
            return _repositoryHoraire.Supprimer(id);
        }
    }
}

