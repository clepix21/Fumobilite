using System;
using System.Collections.Generic;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Systeme.Interfaces;

namespace Fumoblilite.Systeme.Services
{
    public class ServiceLigne
    {
        private readonly IRepositoryLigne _repositoryLigne;
        private readonly IRepositoryArretLigne _repositoryArretLigne;

        public ServiceLigne(IRepositoryLigne repositoryLigne, IRepositoryArretLigne repositoryArretLigne)
        {
            _repositoryLigne = repositoryLigne;
            _repositoryArretLigne = repositoryArretLigne;
        }

        public List<Ligne> ObtenirToutes()
        {
            return _repositoryLigne.ObtenirToutes();
        }

        public Ligne ObtenirParId(int id, bool inclureArrets = false)
        {
            var ligne = _repositoryLigne.ObtenirParId(id);
            if (ligne != null && inclureArrets)
            {
                ligne.Arrets = _repositoryArretLigne.ObtenirParLigne(id);
            }
            return ligne;
        }

        public int Ajouter(Ligne ligne)
        {
            if (string.IsNullOrEmpty(ligne.Numero) || string.IsNullOrEmpty(ligne.Nom))
                throw new ArgumentException("Le numéro et le nom de la ligne sont obligatoires");

            return _repositoryLigne.Ajouter(ligne);
        }

        public bool Modifier(Ligne ligne)
        {
            if (string.IsNullOrEmpty(ligne.Numero) || string.IsNullOrEmpty(ligne.Nom))
                throw new ArgumentException("Le numéro et le nom de la ligne sont obligatoires");

            ligne.DateModification = DateTime.Now;
            return _repositoryLigne.Modifier(ligne);
        }

        public bool Supprimer(int id)
        {
            return _repositoryLigne.Supprimer(id);
        }

        public bool AjouterArret(ArretLigne arretLigne)
        {
            return _repositoryArretLigne.Ajouter(arretLigne) > 0;
        }

        public bool ModifierArret(ArretLigne arretLigne)
        {
            arretLigne.DateModification = DateTime.Now;
            return _repositoryArretLigne.Modifier(arretLigne);
        }

        public bool SupprimerArret(int id)
        {
            return _repositoryArretLigne.Supprimer(id);
        }
    }
}

