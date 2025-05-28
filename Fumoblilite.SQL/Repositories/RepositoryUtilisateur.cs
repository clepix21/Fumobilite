using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Systeme.Interfaces;

namespace Fumoblilite.SQL.Repositories
{
    public class RepositoryUtilisateur : IRepositoryUtilisateur
    {
        private readonly string _connectionString;

        public RepositoryUtilisateur(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Utilisateur> ObtenirTous()
        {
            List<Utilisateur> utilisateurs = new List<Utilisateur>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Utilisateurs WHERE EstSupprime = FALSE ORDER BY Nom, Prenom";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            utilisateurs.Add(MapFromReader(reader));
                        }
                    }
                }
            }

            return utilisateurs;
        }

        public Utilisateur ObtenirParId(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Utilisateurs WHERE Id = @Id AND EstSupprime = FALSE";

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

        public Utilisateur ObtenirParNomUtilisateur(string nomUtilisateur)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Utilisateurs WHERE NomUtilisateur = @NomUtilisateur AND EstSupprime = FALSE";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NomUtilisateur", nomUtilisateur);

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

        public int Ajouter(Utilisateur utilisateur)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO Utilisateurs (Nom, Prenom, NomUtilisateur, MotDePasse, Email, Role, EstActif, DateCreation, EstSupprime)
                    VALUES (@Nom, @Prenom, @NomUtilisateur, @MotDePasse, @Email, @Role, @EstActif, @DateCreation, FALSE);
                    SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nom", utilisateur.Nom);
                    command.Parameters.AddWithValue("@Prenom", utilisateur.Prenom);
                    command.Parameters.AddWithValue("@NomUtilisateur", utilisateur.NomUtilisateur);
                    command.Parameters.AddWithValue("@MotDePasse", utilisateur.MotDePasse);
                    command.Parameters.AddWithValue("@Email", utilisateur.Email);
                    command.Parameters.AddWithValue("@Role", utilisateur.Role);
                    command.Parameters.AddWithValue("@EstActif", utilisateur.EstActif);
                    command.Parameters.AddWithValue("@DateCreation", utilisateur.DateCreation);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Modifier(Utilisateur utilisateur)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    UPDATE Utilisateurs 
                    SET Nom = @Nom, 
                        Prenom = @Prenom, 
                        NomUtilisateur = @NomUtilisateur, 
                        Email = @Email, 
                        Role = @Role, 
                        EstActif = @EstActif, 
                        DateModification = @DateModification
                    WHERE Id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", utilisateur.Id);
                    command.Parameters.AddWithValue("@Nom", utilisateur.Nom);
                    command.Parameters.AddWithValue("@Prenom", utilisateur.Prenom);
                    command.Parameters.AddWithValue("@NomUtilisateur", utilisateur.NomUtilisateur);
                    command.Parameters.AddWithValue("@Email", utilisateur.Email);
                    command.Parameters.AddWithValue("@Role", utilisateur.Role);
                    command.Parameters.AddWithValue("@EstActif", utilisateur.EstActif);
                    command.Parameters.AddWithValue("@DateModification", utilisateur.DateModification ?? DateTime.Now);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool ModifierMotDePasse(int id, string motDePasse)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Utilisateurs SET MotDePasse = @MotDePasse, DateModification = NOW() WHERE Id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@MotDePasse", motDePasse);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Supprimer(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Utilisateurs SET EstSupprime = TRUE, DateModification = NOW() WHERE Id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        private Utilisateur MapFromReader(MySqlDataReader reader)
        {
            return new Utilisateur
            {
                Id = Convert.ToInt32(reader["Id"]),
                Nom = reader["Nom"].ToString(),
                Prenom = reader["Prenom"].ToString(),
                NomUtilisateur = reader["NomUtilisateur"].ToString(),
                MotDePasse = reader["MotDePasse"].ToString(),
                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                Role = reader["Role"].ToString(),
                EstActif = Convert.ToBoolean(reader["EstActif"]),
                DateCreation = Convert.ToDateTime(reader["DateCreation"]),
                DateModification = reader["DateModification"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DateModification"]) : null
            };
        }
    }
}
