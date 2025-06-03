using System;

namespace Fumoblilite.Systeme.Modeles
{
    public class ActionHistorique
    {
        public int Id { get; set; }
        public int? UtilisateurId { get; set; }
        public string NomUtilisateur { get; set; }
        public string Action { get; set; }
        public DateTime DateAction { get; set; }
    }
}
