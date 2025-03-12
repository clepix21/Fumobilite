using System.Collections.Generic;
using Fumoblilite.Systeme.Modeles;

namespace Fumoblilite.Systeme.Interfaces
{
    public interface IRepositoryUtilisateur
    {
        List<Utilisateur> ObtenirTous();
        Utilisateur ObtenirParId(int id);
        Utilisateur ObtenirParNomUtilisateur(string nomUtilisateur);
        int Ajouter(Utilisateur utilisateur);
        bool Modifier(Utilisateur utilisateur);
        bool ModifierMotDePasse(int id, string motDePasse);
        bool Supprimer(int id);
    }
}

