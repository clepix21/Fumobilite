using System.Collections.Generic;
using GestionTransport.Systeme.Modeles;

namespace GestionTransport.Systeme.Interfaces
{
    public interface IRepositoryArretLigne
    {
        List<ArretLigne> ObtenirParLigne(int ligneId);
        ArretLigne ObtenirParId(int id);
        int Ajouter(ArretLigne arretLigne);
        bool Modifier(ArretLigne arretLigne);
        bool Supprimer(int id);
    }
}

