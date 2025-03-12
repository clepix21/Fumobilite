using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Systeme.Interfaces;

namespace Fumoblilite.SQL.Repositories
{
    public class RepositoryHoraire : IRepositoryHoraire
    {
        private readonly string _connectionString;

        public RepositoryHoraire(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Horaire> ObtenirParLigne(int ligneId)
        {
            List<Horaire> horaires = new List<Horaire>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Horaires WHERE LigneId = @LigneId AND EstSupprime = 0 ORDER BY JourSemaine, HeureDepart";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LigneId", ligneId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            horaires.Add(MapFromReader(reader));
                        }
                    }
                }
            }

            return horaires;
        }

        public List<Horaire> ObtenirParArret(int arretId)
        {
            List<Horaire> horaires = new List<Horaire>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Horaires WHERE ArretId = @ArretId AND EstSupprime = 0 ORDER BY JourSemaine, HeureDepart";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ArretId", arretId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            horaires.Add(MapFromReader(reader));
                        }
                    }
                }
            }

            return horaires;
        }

        public List<Horaire> ObtenirParLigneEtJour(int ligneId, DayOfWeek jour)
        {
            List<Horaire> horaires = new List<Horaire>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Horaires WHERE LigneId = @LigneId AND JourSemaine = @JourSemaine AND EstSupprime = 0 ORDER BY HeureDepart";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LigneId", ligneId);
                    command.Parameters.AddWithValue("@JourSemaine", (int)jour);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            horaires.Add(MapFromReader(reader));
                        }
                    }
                }
            }

            return horaires;
        }

        public List<Horaire> ObtenirParArretEtJour(int arretId, DayOfWeek jour)
        {
            List<Horaire> horaires = new List<Horaire>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Horaires WHERE ArretId = @ArretId AND JourSemaine = @JourSemaine AND EstSupprime = 0 ORDER BY HeureDepart";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ArretId", arretId);
                    command.Parameters.AddWithValue("@JourSemaine", (int)jour);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            horaires.Add(MapFromReader(reader));
                        }
                    }
                }
            }

            return horaires;
        }

        public List<Horaire> ObtenirParJour(DayOfWeek jour)
        {
            List<Horaire> horaires = new List<Horaire>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Horaires WHERE JourSemaine = @JourSemaine AND EstSupprime = 0 ORDER BY LigneId, ArretId, HeureDepart";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@JourSemaine", (int)jour);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            horaires.Add(MapFromReader(reader));
                        }
                    }
                }
            }

            return horaires;
        }

        public int Ajouter(Horaire horaire)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO Horaires (LigneId, ArretId, JourSemaine, HeureDepart, EstActif, DateCreation, EstSupprime)
                    VALUES (@LigneId, @ArretId, @JourSemaine, @HeureDepart, @EstActif, @DateCreation, 0);
                    SELECT last_insert_rowid();";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LigneId", horaire.LigneId);
                    command.Parameters.AddWithValue("@ArretId", horaire.ArretId);
                    command.Parameters.AddWithValue("@JourSemaine", (int)horaire.JourSemaine);
                    command.Parameters.AddWithValue("@HeureDepart", horaire.HeureDepart.ToString());
                    command.Parameters.AddWithValue("@EstActif", horaire.EstActif);
                    command.Parameters.AddWithValue("@DateCreation", horaire.DateCreation);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Modifier(Horaire horaire)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    UPDATE Horaires 
                    SET LigneId = @LigneId, 
                        ArretId = @ArretId, 
                        JourSemaine = @JourSemaine, 
                        HeureDepart = @HeureDepart, 
                        EstActif = @EstActif, 
                        DateModification = @DateModification
                    WHERE Id = @Id";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", horaire.Id);
                    command.Parameters.AddWithValue("@LigneId", horaire.LigneId);
                    command.Parameters.AddWithValue("@ArretId", horaire.ArretId);
                    command.Parameters.AddWithValue("@JourSemaine", (int)horaire.JourSemaine);
                    command.Parameters.AddWithValue("@HeureDepart", horaire.HeureDepart.ToString());
                    command.Parameters.AddWithValue("@EstActif", horaire.EstActif);
                    command.Parameters.AddWithValue("@DateModification", horaire.DateModification ?? DateTime.Now);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Supprimer(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Horaires SET EstSupprime = 1 WHERE Id = @Id";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        private Horaire MapFromReader(SQLiteDataReader reader)
        {
            return new Horaire
            {
                Id = Convert.ToInt32(reader["Id"]),
                LigneId = Convert.ToInt32(reader["LigneId"]),
                ArretId = Convert.ToInt32(reader["ArretId"]),
                JourSemaine = (DayOfWeek)Convert.ToInt32(reader["JourSemaine"]),
                HeureDepart = TimeSpan.Parse(reader["HeureDepart"].ToString()),
                EstActif = Convert.ToBoolean(reader["EstActif"]),
                DateCreation = Convert.ToDateTime(reader["DateCreation"]),
                DateModification = reader["DateModification"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DateModification"]) : null
            };
        }
    }
}

