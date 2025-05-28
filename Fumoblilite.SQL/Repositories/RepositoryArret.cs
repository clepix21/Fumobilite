using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Systeme.Interfaces;

namespace Fumoblilite.SQL.Repositories
{
    public class RepositoryArret : IRepositoryArret
    {
        private readonly string _connectionString;

        public RepositoryArret(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Arret> ObtenirTous()
        {
            List<Arret> arrets = new List<Arret>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Arrets WHERE EstSupprime = FALSE ORDER BY Nom";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            arrets.Add(MapFromReader(reader));
                        }
                    }
                }
            }

            return arrets;
        }

        public Arret ObtenirParId(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Arrets WHERE Id = @Id AND EstSupprime = FALSE";

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

        public List<Arret> ObtenirParLigne(int ligneId)
        {
            List<Arret> arrets = new List<Arret>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT a.* 
                    FROM Arrets a
                    INNER JOIN ArretsLignes al ON a.Id = al.ArretId
                    WHERE al.LigneId = @LigneId AND a.EstSupprime = FALSE AND al.EstSupprime = FALSE
                    ORDER BY al.Ordre";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LigneId", ligneId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            arrets.Add(MapFromReader(reader));
                        }
                    }
                }
            }

            return arrets;
        }

        public int Ajouter(Arret arret)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO Arrets (Nom, Adresse, Latitude, Longitude, EstAccessible, DateCreation, EstSupprime)
                    VALUES (@Nom, @Adresse, @Latitude, @Longitude, @EstAccessible, @DateCreation, FALSE);
                    SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nom", arret.Nom);
                    command.Parameters.AddWithValue("@Adresse", arret.Adresse);
                    command.Parameters.AddWithValue("@Latitude", arret.Latitude);
                    command.Parameters.AddWithValue("@Longitude", arret.Longitude);
                    command.Parameters.AddWithValue("@EstAccessible", arret.EstAccessible);
                    command.Parameters.AddWithValue("@DateCreation", arret.DateCreation);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Modifier(Arret arret)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    UPDATE Arrets 
                    SET Nom = @Nom, 
                        Adresse = @Adresse, 
                        Latitude = @Latitude, 
                        Longitude = @Longitude, 
                        EstAccessible = @EstAccessible, 
                        DateModification = @DateModification
                    WHERE Id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", arret.Id);
                    command.Parameters.AddWithValue("@Nom", arret.Nom);
                    command.Parameters.AddWithValue("@Adresse", arret.Adresse);
                    command.Parameters.AddWithValue("@Latitude", arret.Latitude);
                    command.Parameters.AddWithValue("@Longitude", arret.Longitude);
                    command.Parameters.AddWithValue("@EstAccessible", arret.EstAccessible);
                    command.Parameters.AddWithValue("@DateModification", arret.DateModification ?? DateTime.Now);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Supprimer(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Arrets SET EstSupprime = TRUE, DateModification = NOW() WHERE Id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        private Arret MapFromReader(MySqlDataReader reader)
        {
            return new Arret
            {
                Id = Convert.ToInt32(reader["Id"]),
                Nom = reader["Nom"].ToString(),
                Adresse = reader["Adresse"] != DBNull.Value ? reader["Adresse"].ToString() : null,
                Latitude = Convert.ToDouble(reader["Latitude"]),
                Longitude = Convert.ToDouble(reader["Longitude"]),
                EstAccessible = Convert.ToBoolean(reader["EstAccessible"]),
                DateCreation = Convert.ToDateTime(reader["DateCreation"]),
                DateModification = reader["DateModification"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DateModification"]) : null
            };
        }
    }
}
