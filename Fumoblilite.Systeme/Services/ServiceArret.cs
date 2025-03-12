using System;
using System.Collections.Generic;
using GestionTransport.Systeme.Modeles;
using GestionTransport.Systeme.Interfaces;

namespace GestionTransport.Systeme.Services
{
    public class ServiceArret
    {
        private readonly IRepositoryArret _repositoryArret;

        public ServiceArret(IRepositoryArret repositoryArret)
        {
            _repositoryArret = repositoryArret;
        }

        public List<Arret> ObtenirTous()
        {
            return _repositoryArret.ObtenirTous();
        }

        public Arret ObtenirParId(int id)
        {
            return _repositoryArret.ObtenirParId(id);
        }

        public List<Arret> ObtenirParLigne(int ligneId)
        {
            return _repositoryArret.ObtenirParLigne(ligneId);
        }

        public int Ajouter(Arret arret)
        {
            if (string.IsNullOrEmpty(arret.Nom))
                throw new ArgumentException("Le nom de l'arrêt est obligatoire");

            return _repositoryArret.Ajouter(arret);
        }

        public bool Modifier(Arret arret)
        {
            if (string.IsNullOrEmpty(arret.Nom))
                throw new ArgumentException("Le nom de l'arrêt est obligatoire");

            arret.DateModification = DateTime.Now;
            return _repositoryArret.Modifier(arret);
        }

        public bool Supprimer(int id)
        {
            return _repositoryArret.Supprimer(id);
        }
    }
}

