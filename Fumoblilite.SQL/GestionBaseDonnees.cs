using System;
using System.Data.SQLite;
using System.IO;

namespace Fumoblilite.SQL
{
    public class GestionBaseDonnees
    {
        private readonly string _connectionString;
        private readonly string _cheminBaseDonnees;

        public GestionBaseDonnees(string cheminBaseDonnees)
        {
            _cheminBaseDonnees = cheminBaseDonnees;
            _connectionString = $"Data Source={cheminBaseDonnees};Version=3;";
        }

        public string ConnectionString => _connectionString;

        public bool CreerBaseDonneesSiNonExistante()
        {
            try
            {
                if (!File.Exists(_cheminBaseDonnees))
                {
                    SQLiteConnection.CreateFile(_cheminBaseDonnees);
                    CreerTables();
                    InsererDonneesInitiales();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création de la base de données : {ex.Message}");
                return false;
            }
        }

        private void CreerTables()
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                // Table Utilisateurs
                string createUtilisateursTable = @"
                    CREATE TABLE Utilisateurs (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nom TEXT NOT NULL,
                        Prenom TEXT NOT NULL,
                        NomUtilisateur TEXT NOT NULL UNIQUE,
                        MotDePasse TEXT NOT NULL,
                        Email TEXT,
                        Role TEXT NOT NULL,
                        EstActif INTEGER NOT NULL,
                        DateCreation TEXT NOT NULL,
                        DateModification TEXT,
                        EstSupprime INTEGER NOT NULL DEFAULT 0
                    );";

                // Table Arrets
                string createArretsTable = @"
                    CREATE TABLE Arrets (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nom TEXT NOT NULL,
                        Adresse TEXT,
                        Latitude REAL NOT NULL,
                        Longitude REAL NOT NULL,
                        EstAccessible INTEGER NOT NULL,
                        DateCreation TEXT NOT NULL,
                        DateModification TEXT,
                        EstSupprime INTEGER NOT NULL DEFAULT 0
                    );";

                // Table Lignes
                string createLignesTable = @"
                    CREATE TABLE Lignes (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Numero TEXT NOT NULL,
                        Nom TEXT NOT NULL,
                        Couleur TEXT,
                        TypeTransport TEXT NOT NULL,
                        EstActif INTEGER NOT NULL,
                        DateCreation TEXT NOT NULL,
                        DateModification TEXT,
                        EstSupprime INTEGER NOT NULL DEFAULT 0
                    );";

                // Table ArretsLignes
                string createArretsLignesTable = @"
                    CREATE TABLE ArretsLignes (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        LigneId INTEGER NOT NULL,
                        ArretId INTEGER NOT NULL,
                        Ordre INTEGER NOT NULL,
                        TempsArretMinutes INTEGER NOT NULL,
                        TempsTrajetSuivantMinutes INTEGER NOT NULL,
                        DateCreation TEXT NOT NULL,
                        DateModification TEXT,
                        EstSupprime INTEGER NOT NULL DEFAULT 0,
                        FOREIGN KEY (LigneId) REFERENCES Lignes(Id),
                        FOREIGN KEY (ArretId) REFERENCES Arrets(Id)
                    );";

                // Table Horaires
                string createHorairesTable = @"
                    CREATE TABLE Horaires (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        LigneId INTEGER NOT NULL,
                        ArretId INTEGER NOT NULL,
                        JourSemaine INTEGER NOT NULL,
                        HeureDepart TEXT NOT NULL,
                        EstActif INTEGER NOT NULL,
                        DateCreation TEXT NOT NULL,
                        DateModification TEXT,
                        EstSupprime INTEGER NOT NULL DEFAULT 0,
                        FOREIGN KEY (LigneId) REFERENCES Lignes(Id),
                        FOREIGN KEY (ArretId) REFERENCES Arrets(Id)
                    );";

                using (SQLiteCommand command = new SQLiteCommand(createUtilisateursTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = new SQLiteCommand(createArretsTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = new SQLiteCommand(createLignesTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = new SQLiteCommand(createArretsLignesTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = new SQLiteCommand(createHorairesTable, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void InsererDonneesInitiales()
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                // Insérer un utilisateur administrateur par défaut
                string insertAdmin = @"
                    INSERT INTO Utilisateurs (Nom, Prenom, NomUtilisateur, MotDePasse, Role, EstActif, DateCreation)
                    VALUES ('Admin', 'Admin', 'admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'Admin', 1, datetime('now'));";
                // Le mot de passe est 'admin' hashé en SHA-256

                using (SQLiteCommand command = new SQLiteCommand(insertAdmin, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Insérer quelques arrêts de démonstration
                string insertArrets = @"
                    INSERT INTO Arrets (Nom, Adresse, Latitude, Longitude, EstAccessible, DateCreation)
                    VALUES 
                    ('Gare Centrale', '1 Place de la Gare', 48.8566, 2.3522, 1, datetime('now')),
                    ('Hôtel de Ville', '5 Rue de la Mairie', 48.8570, 2.3530, 1, datetime('now')),
                    ('Université', '10 Avenue des Sciences', 48.8580, 2.3540, 1, datetime('now')),
                    ('Hôpital', '15 Rue de la Santé', 48.8590, 2.3550, 1, datetime('now')),
                    ('Centre Commercial', '20 Boulevard du Commerce', 48.8600, 2.3560, 1, datetime('now'));";

                using (SQLiteCommand command = new SQLiteCommand(insertArrets, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Insérer quelques lignes de démonstration
                string insertLignes = @"
                    INSERT INTO Lignes (Numero, Nom, Couleur, TypeTransport, EstActif, DateCreation)
                    VALUES 
                    ('1', 'Ligne 1', '#FF0000', 'Bus', 1, datetime('now')),
                    ('2', 'Ligne 2', '#00FF00', 'Bus', 1, datetime('now')),
                    ('A', 'Ligne A', '#0000FF', 'Métro', 1, datetime('now'));";

                using (SQLiteCommand command = new SQLiteCommand(insertLignes, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Associer les arrêts aux lignes
                string insertArretsLignes = @"
                    -- Ligne 1
                    INSERT INTO ArretsLignes (LigneId, ArretId, Ordre, TempsArretMinutes, TempsTrajetSuivantMinutes, DateCreation)
                    VALUES 
                    (1, 1, 1, 1, 5, datetime('now')),
                    (1, 2, 2, 1, 4, datetime('now')),
                    (1, 3, 3, 1, 0, datetime('now'));

                    -- Ligne 2
                    INSERT INTO ArretsLignes (LigneId, ArretId, Ordre, TempsArretMinutes, TempsTrajetSuivantMinutes, DateCreation)
                    VALUES 
                    (2, 1, 1, 1, 3, datetime('now')),
                    (2, 4, 2, 1, 6, datetime('now')),
                    (2, 5, 3, 1, 0, datetime('now'));

                    -- Ligne A
                    INSERT INTO ArretsLignes (LigneId, ArretId, Ordre, TempsArretMinutes, TempsTrajetSuivantMinutes, DateCreation)
                    VALUES 
                    (3, 1, 1, 1, 2, datetime('now')),
                    (3, 3, 2, 1, 2, datetime('now')),
                    (3, 5, 3, 1, 0, datetime('now'));";

                using (SQLiteCommand command = new SQLiteCommand(insertArretsLignes, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Insérer quelques horaires de démonstration
                string insertHoraires = @"
                    -- Ligne 1, Arrêt 1 (Gare Centrale), Lundi
                    INSERT INTO Horaires (LigneId, ArretId, JourSemaine, HeureDepart, EstActif, DateCreation)
                    VALUES 
                    (1, 1, 1, '07:00:00', 1, datetime('now')),
                    (1, 1, 1, '08:00:00', 1, datetime('now')),
                    (1, 1, 1, '09:00:00', 1, datetime('now'));

                    -- Ligne 2, Arrêt 1 (Gare Centrale), Lundi
                    INSERT INTO Horaires (LigneId, ArretId, JourSemaine, HeureDepart, EstActif, DateCreation)
                    VALUES 
                    (2, 1, 1, '07:30:00', 1, datetime('now')),
                    (2, 1, 1, '08:30:00', 1, datetime('now')),
                    (2, 1, 1, '09:30:00', 1, datetime('now'));

                    -- Ligne A, Arrêt 1 (Gare Centrale), Lundi
                    INSERT INTO Horaires (LigneId, ArretId, JourSemaine, HeureDepart, EstActif, DateCreation)
                    VALUES 
                    (3, 1, 1, '07:15:00', 1, datetime('now')),
                    (3, 1, 1, '07:45:00', 1, datetime('now')),
                    (3, 1, 1, '08:15:00', 1, datetime('now')),
                    (3, 1, 1, '08:45:00', 1, datetime('now')),
                    (3, 1, 1, '09:15:00', 1, datetime('now')),
                    (3, 1, 1, '09:45:00', 1, datetime('now'));";

                using (SQLiteCommand command = new SQLiteCommand(insertHoraires, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

