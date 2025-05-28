using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Systeme.Interfaces;

namespace Fumoblilite.SQL.Repositories
{
    public class RepositoryLigne : IRepositoryLigne
    {
        private readonly string _connectionString;

        public RepositoryLigne(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Ligne> ObtenirToutes()
        {
            List<Ligne> lignes = new List<Ligne>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Lignes WHERE EstSupprime = FALSE ORDER BY Numero";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lignes.Add(MapFromReader(reader));
                        }
                    }
                }
            }

            return lignes;
        }

        public Ligne ObtenirParId(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Lignes WHERE Id = @Id AND EstSupprime = FALSE";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapFromReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public int Ajouter(Ligne ligne)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO Lignes (Numero, Nom, Couleur, EstActif, DateCreation, EstSupprime)
                    VALUES (@Numero, @Nom, @Couleur, @EstActif, @DateCreation, FALSE);
                    SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Numero", ligne.Numero);
                    command.Parameters.AddWithValue("@Nom", ligne.Nom);
                    command.Parameters.AddWithValue("@Couleur", ligne.Couleur);
                    command.Parameters.AddWithValue("@EstActif", ligne.EstActif);
                    command.Parameters.AddWithValue("@DateCreation", ligne.DateCreation);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Modifier(Ligne ligne)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    UPDATE Lignes 
                    SET Numero = @Numero, 
                        Nom = @Nom, 
                        Couleur = @Couleur, 
                        EstActif = @EstActif, 
                        DateModification = @DateModification
                    WHERE Id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", ligne.Id);
                    command.Parameters.AddWithValue("@Numero", ligne.Numero);
                    command.Parameters.AddWithValue("@Nom", ligne.Nom);
                    command.Parameters.AddWithValue("@Couleur", ligne.Couleur);
                    command.Parameters.AddWithValue("@EstActif", ligne.EstActif);
                    command.Parameters.AddWithValue("@DateModification", ligne.DateModification ?? DateTime.Now);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Supprimer(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Lignes SET EstSupprime = TRUE, DateModification = NOW() WHERE Id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        private Ligne MapFromReader(MySqlDataReader reader)
        {
            return new Ligne
            {
                Id = Convert.ToInt32(reader["Id"]),
                Numero = reader["Numero"].ToString(),
                Nom = reader["Nom"].ToString(),
                Couleur = reader["Couleur"] != DBNull.Value ? reader["Couleur"].ToString() : null,
                EstActif = Convert.ToBoolean(reader["EstActif"]),
                DateCreation = Convert.ToDateTime(reader["DateCreation"]),
                DateModification = reader["DateModification"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DateModification"]) : null,
                Arrets = new List<ArretLigne>()
            };
        }
    }
}
