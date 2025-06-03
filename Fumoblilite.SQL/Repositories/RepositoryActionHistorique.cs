using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Fumoblilite.Systeme.Modeles;

namespace Fumoblilite.SQL.Repositories
{
    public class RepositoryActionHistorique
    {
        private readonly string _connectionString;
        public RepositoryActionHistorique(string connectionString) { _connectionString = connectionString; }

        public void Ajouter(ActionHistorique action)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string q = "INSERT INTO ActionsHistoriques (UtilisateurId, NomUtilisateur, Action, DateAction) VALUES (@uid, @nom, @action, @date)";
                using (var cmd = new MySqlCommand(q, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", (object)action.UtilisateurId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@nom", action.NomUtilisateur ?? "");
                    cmd.Parameters.AddWithValue("@action", action.Action);
                    cmd.Parameters.AddWithValue("@date", action.DateAction);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ActionHistorique> ObtenirParUtilisateur(int? utilisateurId)
        {
            var list = new List<ActionHistorique>();
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string q = "SELECT * FROM ActionsHistoriques WHERE UtilisateurId = @uid OR (@uid IS NULL AND UtilisateurId IS NULL) ORDER BY DateAction DESC";
                using (var cmd = new MySqlCommand(q, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", (object)utilisateurId ?? DBNull.Value);
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new ActionHistorique
                            {
                                Id = Convert.ToInt32(r["Id"]),
                                UtilisateurId = r["UtilisateurId"] != DBNull.Value ? (int?)Convert.ToInt32(r["UtilisateurId"]) : null,
                                NomUtilisateur = r["NomUtilisateur"].ToString(),
                                Action = r["Action"].ToString(),
                                DateAction = Convert.ToDateTime(r["DateAction"])
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}