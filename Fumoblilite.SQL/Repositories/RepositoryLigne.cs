using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using GestionTransport.Systeme.Modeles;
using GestionTransport.Systeme.Interfaces;

namespace GestionTransport.SQL.Repositories
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

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Lignes WHERE EstSupprime = 0 ORDER BY Numero";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
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
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Lignes WHERE Id = @Id AND EstSupprime = 0";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SQLiteDataReader reader = command.ExecuteReader())
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
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO Lignes (Numero, Nom, Couleur, TypeTransport, EstActif, DateCreation, EstSupprime)
                    VALUES (@Numero, @Nom, @Couleur, @TypeTransport, @EstActif, @DateCreation, 0);
                    SELECT last_insert_rowid();";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Numero", ligne.Numero);
                    command.Parameters.AddWithValue("@Nom", ligne.Nom);
                    command.Parameters.AddWithValue("@Couleur", ligne.Couleur ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TypeTransport", ligne.TypeTransport);
                    command.Parameters.AddWithValue("@EstActif", ligne.EstActif);
                    command.Parameters.AddWithValue("@DateCreation", ligne.DateCreation);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Modifier(Ligne ligne)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    UPDATE Lignes 
                    SET Numero = @Numero, 
                        Nom = @Nom, 
                        Couleur = @Couleur, 
                        TypeTransport = @TypeTransport, 
                        EstActif = @EstActif, 
                        DateModification = @DateModification
                    WHERE Id = @Id";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", ligne.Id);
                    command.Parameters.AddWithValue("@Numero", ligne.Numero);
                    command.Parameters.AddWithValue("@Nom", ligne.Nom);
                    command.Parameters.AddWithValue("@Couleur", ligne.Couleur ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TypeTransport", ligne.TypeTransport);
                    command.Parameters.AddWithValue("@EstActif", ligne.EstActif);
                    command.Parameters.AddWithValue("@DateModification", ligne.DateModification ?? DateTime.Now);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Supprimer(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Lignes SET EstSupprime = 1 WHERE Id = @Id";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        private Ligne MapFromReader(SQLiteDataReader reader)
        {
            return new Ligne
            {
                Id = Convert.ToInt32(reader["Id"]),
                Numero = reader["Numero"].ToString(),
                Nom = reader["Nom"].ToString(),
                Couleur = reader["Couleur"] != DBNull.Value ? reader["Couleur"].ToString() : null,
                TypeTransport = reader["TypeTransport"].ToString(),
                EstActif = Convert.ToBoolean(reader["EstActif"]),
                DateCreation = Convert.ToDateTime(reader["DateCreation"]),
                DateModification = reader["DateModification"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DateModification"]) : null,
                Arrets = new List<ArretLigne>()
            };
        }
    }
}

