using System.Collections.Generic;
using GestionTransport.Systeme.Modeles;

namespace GestionTransport.Systeme.Interfaces
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

