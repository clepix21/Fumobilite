using System.Collections.Generic;
using GestionTransport.Systeme.Modeles;

namespace GestionTransport.Systeme.Interfaces
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

