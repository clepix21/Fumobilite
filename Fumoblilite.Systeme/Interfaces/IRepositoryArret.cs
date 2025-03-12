using System.Collections.Generic;
using Fumoblilite.Systeme.Modeles;

namespace Fumoblilite.Systeme.Interfaces
{
    public interface IRepositoryArret
    {
        List<Arret> ObtenirTous();
        Arret ObtenirParId(int id);
        List<Arret> ObtenirParLigne(int ligneId);
        int Ajouter(Arret arret);
        bool Modifier(Arret arret);
        bool Supprimer(int id);
    }
}

