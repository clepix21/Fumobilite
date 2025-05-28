using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;

namespace Fumoblilite.SQL
{
    public class GestionBaseDonnees
    {
        private string _connectionString;

        public string ConnectionString => _connectionString;

        public GestionBaseDonnees(string serveur, string login, string motDePasse, string baseDonnees)
        {
            _connectionString = $"Server={serveur};Database={baseDonnees};Uid={login};Pwd={motDePasse};CharSet=utf8;";
        }

        public bool TesterConnexion()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool CreerBaseDonneesSiNonExistante()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    // Vérifier si les tables existent
                    bool tablesExistent = VerifierExistenceTables(connection);

                    if (!tablesExistent)
                    {
                        CreerTables(connection);
                        InsererDonneesInitiales(connection);
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la création de la base de données : {ex.Message}");
            }
        }

        private bool VerifierExistenceTables(MySqlConnection connection)
        {
            string query = "SHOW TABLES LIKE 'Utilisateurs'";
            using (var command = new MySqlCommand(query, connection))
            {
                var result = command.ExecuteScalar();
                return result != null;
            }
        }

        private void CreerTables(MySqlConnection connection)
        {
            string[] scripts = {
                @"CREATE TABLE IF NOT EXISTS Utilisateurs (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    NomUtilisateur VARCHAR(50) NOT NULL UNIQUE,
                    MotDePasse VARCHAR(255) NOT NULL,
                    EstAdministrateur BOOLEAN NOT NULL DEFAULT FALSE,
                    DateCreation DATETIME DEFAULT CURRENT_TIMESTAMP
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8;",

                @"CREATE TABLE IF NOT EXISTS Arrets (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Nom VARCHAR(100) NOT NULL,
                    Latitude DECIMAL(10,8),
                    Longitude DECIMAL(11,8),
                    INDEX idx_nom (Nom)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8;",

                @"CREATE TABLE IF NOT EXISTS Lignes (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Nom VARCHAR(50) NOT NULL,
                    Couleur VARCHAR(7),
                    INDEX idx_nom (Nom)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8;",

                @"CREATE TABLE IF NOT EXISTS ArretLignes (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    IdArret INT NOT NULL,
                    IdLigne INT NOT NULL,
                    Ordre INT NOT NULL,
                    FOREIGN KEY (IdArret) REFERENCES Arrets(Id) ON DELETE CASCADE,
                    FOREIGN KEY (IdLigne) REFERENCES Lignes(Id) ON DELETE CASCADE,
                    INDEX idx_ligne_ordre (IdLigne, Ordre),
                    INDEX idx_arret (IdArret)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8;",

                @"CREATE TABLE IF NOT EXISTS Horaires (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    IdLigne INT NOT NULL,
                    IdArret INT NOT NULL,
                    Heure TIME NOT NULL,
                    JourSemaine INT NOT NULL,
                    FOREIGN KEY (IdLigne) REFERENCES Lignes(Id) ON DELETE CASCADE,
                    FOREIGN KEY (IdArret) REFERENCES Arrets(Id) ON DELETE CASCADE,
                    INDEX idx_ligne_arret (IdLigne, IdArret),
                    INDEX idx_jour_heure (JourSemaine, Heure)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8;"
            };

            foreach (string script in scripts)
            {
                using (var command = new MySqlCommand(script, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void InsererDonneesInitiales(MySqlConnection connection)
        {
            // Insérer un utilisateur administrateur par défaut
            string insertAdmin = @"INSERT INTO Utilisateurs (NomUtilisateur, MotDePasse, EstAdministrateur) 
                                  VALUES ('admin', 'admin', TRUE)";

            using (var command = new MySqlCommand(insertAdmin, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
