using System.Collections.Generic;
using Fumoblilite.Systeme.Modeles;

namespace Fumoblilite.Systeme.Interfaces
{
    public interface IRepositoryLigne
    {
        List<Ligne> ObtenirToutes();
        Ligne ObtenirParId(int id);
        int Ajouter(Ligne ligne);
        bool Modifier(Ligne ligne);
        bool Supprimer(int id);
    }
}

