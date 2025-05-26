using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Systeme.Interfaces;

namespace Fumoblilite.SQL.Repositories
{
    public class RepositoryHoraire : IRepositoryHoraire
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, List<Horaire>> _cache;
        private readonly object _cacheLock = new object();
        private DateTime _lastCacheUpdate = DateTime.MinValue;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);

        public RepositoryHoraire(string connectionString)
        {
            _connectionString = connectionString;
            _cache = new Dictionary<string, List<Horaire>>();
            CreerIndex();
        }

        private void CreerIndex()
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                // Créer des index pour optimiser les performances
                var indexes = new[]
                {
                    "CREATE INDEX IF NOT EXISTS idx_horaires_ligne_jour ON Horaires(LigneId, JourSemaine, HeureDepart)",
                    "CREATE INDEX IF NOT EXISTS idx_horaires_arret_jour ON Horaires(ArretId, JourSemaine, HeureDepart)",
                    "CREATE INDEX IF NOT EXISTS idx_horaires_actif ON Horaires(EstActif, EstSupprime)",
                    "CREATE INDEX IF NOT EXISTS idx_horaires_heure ON Horaires(HeureDepart)",
                    "CREATE INDEX IF NOT EXISTS idx_horaires_date_creation ON Horaires(DateCreation)"
                };

                foreach (var indexQuery in indexes)
                {
                    using (var command = new SQLiteCommand(indexQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void InvaliderCache()
        {
            lock (_cacheLock)
            {
                _cache.Clear();
                _lastCacheUpdate = DateTime.MinValue;
            }
        }

        private List<Horaire> ObtenirDepuisCache(string cacheKey, Func<List<Horaire>> factory)
        {
            lock (_cacheLock)
            {
                if (_cache.ContainsKey(cacheKey) && DateTime.Now - _lastCacheUpdate < _cacheExpiration)
                {
                    return new List<Horaire>(_cache[cacheKey]);
                }

                var result = factory();
                _cache[cacheKey] = new List<Horaire>(result);
                _lastCacheUpdate = DateTime.Now;
                return result;
            }
        }

        public List<Horaire> ObtenirParLigne(int ligneId)
        {
            string cacheKey = $"ligne_{ligneId}";
            return ObtenirDepuisCache(cacheKey, () =>
            {
                List<Horaire> horaires = new List<Horaire>();

                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT * FROM Horaires 
                        WHERE LigneId = @LigneId AND EstSupprime = 0 
                        ORDER BY JourSemaine, HeureDepart";

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
            });
        }

        public List<Horaire> ObtenirParArret(int arretId)
        {
            string cacheKey = $"arret_{arretId}";
            return ObtenirDepuisCache(cacheKey, () =>
            {
                List<Horaire> horaires = new List<Horaire>();

                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT h.*, l.Numero as LigneNumero, l.Nom as LigneNom
                        FROM Horaires h
                        INNER JOIN Lignes l ON h.LigneId = l.Id
                        WHERE h.ArretId = @ArretId AND h.EstSupprime = 0 AND l.EstSupprime = 0
                        ORDER BY h.JourSemaine, h.HeureDepart";

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
            });
        }

        public List<Horaire> ObtenirParLigneEtJour(int ligneId, DayOfWeek jour)
        {
            string cacheKey = $"ligne_{ligneId}_jour_{(int)jour}";
            return ObtenirDepuisCache(cacheKey, () =>
            {
                List<Horaire> horaires = new List<Horaire>();

                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT * FROM Horaires 
                        WHERE LigneId = @LigneId AND JourSemaine = @JourSemaine AND EstSupprime = 0 
                        ORDER BY HeureDepart";

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
            });
        }

        public List<Horaire> ObtenirParArretEtJour(int arretId, DayOfWeek jour)
        {
            List<Horaire> horaires = new List<Horaire>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT h.*, l.Numero as LigneNumero, l.Nom as LigneNom
                    FROM Horaires h
                    INNER JOIN Lignes l ON h.LigneId = l.Id
                    WHERE h.ArretId = @ArretId AND h.JourSemaine = @JourSemaine 
                          AND h.EstSupprime = 0 AND l.EstSupprime = 0
                    ORDER BY h.HeureDepart";

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
            string cacheKey = $"jour_{(int)jour}";
            return ObtenirDepuisCache(cacheKey, () =>
            {
                List<Horaire> horaires = new List<Horaire>();

                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT h.*, l.Numero as LigneNumero, l.Nom as LigneNom, a.Nom as ArretNom
                        FROM Horaires h
                        INNER JOIN Lignes l ON h.LigneId = l.Id
                        INNER JOIN Arrets a ON h.ArretId = a.Id
                        WHERE h.JourSemaine = @JourSemaine AND h.EstSupprime = 0 
                              AND l.EstSupprime = 0 AND a.EstSupprime = 0
                        ORDER BY l.Numero, a.Nom, h.HeureDepart";

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
            });
        }

        public List<Horaire> ObtenirParCriteres(HoraireCriteres criteres)
        {
            List<Horaire> horaires = new List<Horaire>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                var queryBuilder = new StringBuilder(@"
                    SELECT h.*, l.Numero as LigneNumero, l.Nom as LigneNom, a.Nom as ArretNom
                    FROM Horaires h
                    INNER JOIN Lignes l ON h.LigneId = l.Id
                    INNER JOIN Arrets a ON h.ArretId = a.Id
                    WHERE h.EstSupprime = 0 AND l.EstSupprime = 0 AND a.EstSupprime = 0");

                var parameters = new List<SQLiteParameter>();

                if (criteres.LigneId.HasValue)
                {
                    queryBuilder.Append(" AND h.LigneId = @LigneId");
                    parameters.Add(new SQLiteParameter("@LigneId", criteres.LigneId.Value));
                }

                if (criteres.ArretId.HasValue)
                {
                    queryBuilder.Append(" AND h.ArretId = @ArretId");
                    parameters.Add(new SQLiteParameter("@ArretId", criteres.ArretId.Value));
                }

                if (criteres.JourSemaine.HasValue)
                {
                    queryBuilder.Append(" AND h.JourSemaine = @JourSemaine");
                    parameters.Add(new SQLiteParameter("@JourSemaine", (int)criteres.JourSemaine.Value));
                }

                if (criteres.HeureDebut.HasValue)
                {
                    queryBuilder.Append(" AND h.HeureDepart >= @HeureDebut");
                    parameters.Add(new SQLiteParameter("@HeureDebut", criteres.HeureDebut.Value.ToString()));
                }

                if (criteres.HeureFin.HasValue)
                {
                    queryBuilder.Append(" AND h.HeureDepart <= @HeureFin");
                    parameters.Add(new SQLiteParameter("@HeureFin", criteres.HeureFin.Value.ToString()));
                }

                if (criteres.EstActif.HasValue)
                {
                    queryBuilder.Append(" AND h.EstActif = @EstActif");
                    parameters.Add(new SQLiteParameter("@EstActif", criteres.EstActif.Value));
                }

                if (criteres.DateCreationDebut.HasValue)
                {
                    queryBuilder.Append(" AND h.DateCreation >= @DateCreationDebut");
                    parameters.Add(new SQLiteParameter("@DateCreationDebut", criteres.DateCreationDebut.Value));
                }

                if (criteres.DateCreationFin.HasValue)
                {
                    queryBuilder.Append(" AND h.DateCreation <= @DateCreationFin");
                    parameters.Add(new SQLiteParameter("@DateCreationFin", criteres.DateCreationFin.Value));
                }

                // Tri
                queryBuilder.Append($" ORDER BY {criteres.TrierPar}");
                if (criteres.OrdreDecroissant)
                    queryBuilder.Append(" DESC");

                // Pagination
                if (criteres.Limite.HasValue)
                {
                    queryBuilder.Append(" LIMIT @Limite");
                    parameters.Add(new SQLiteParameter("@Limite", criteres.Limite.Value));

                    if (criteres.Offset.HasValue)
                    {
                        queryBuilder.Append(" OFFSET @Offset");
                        parameters.Add(new SQLiteParameter("@Offset", criteres.Offset.Value));
                    }
                }

                using (SQLiteCommand command = new SQLiteCommand(queryBuilder.ToString(), connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());

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

        public List<Horaire> ObtenirProchains(int arretId, DateTime dateHeure, int limite = 10)
        {
            List<Horaire> horaires = new List<Horaire>();
            DayOfWeek jour = dateHeure.DayOfWeek;
            TimeSpan heureActuelle = dateHeure.TimeOfDay;

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT h.*, l.Numero as LigneNumero, l.Nom as LigneNom
                    FROM Horaires h
                    INNER JOIN Lignes l ON h.LigneId = l.Id
                    WHERE h.ArretId = @ArretId AND h.JourSemaine = @JourSemaine 
                          AND h.HeureDepart >= @HeureActuelle AND h.EstActif = 1
                          AND h.EstSupprime = 0 AND l.EstSupprime = 0
                    ORDER BY h.HeureDepart
                    LIMIT @Limite";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ArretId", arretId);
                    command.Parameters.AddWithValue("@JourSemaine", (int)jour);
                    command.Parameters.AddWithValue("@HeureActuelle", heureActuelle.ToString());
                    command.Parameters.AddWithValue("@Limite", limite);

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

        public List<Horaire> ObtenirParPlageHoraire(int ligneId, DayOfWeek jour, TimeSpan heureDebut, TimeSpan heureFin)
        {
            List<Horaire> horaires = new List<Horaire>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT * FROM Horaires 
                    WHERE LigneId = @LigneId AND JourSemaine = @JourSemaine 
                          AND HeureDepart >= @HeureDebut AND HeureDepart <= @HeureFin
                          AND EstSupprime = 0 
                    ORDER BY HeureDepart";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LigneId", ligneId);
                    command.Parameters.AddWithValue("@JourSemaine", (int)jour);
                    command.Parameters.AddWithValue("@HeureDebut", heureDebut.ToString());
                    command.Parameters.AddWithValue("@HeureFin", heureFin.ToString());

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

        public bool ExisteHoraire(int ligneId, int arretId, DayOfWeek jour, TimeSpan heure)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT COUNT(*) FROM Horaires 
                    WHERE LigneId = @LigneId AND ArretId = @ArretId 
                          AND JourSemaine = @JourSemaine AND HeureDepart = @HeureDepart
                          AND EstSupprime = 0";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LigneId", ligneId);
                    command.Parameters.AddWithValue("@ArretId", arretId);
                    command.Parameters.AddWithValue("@JourSemaine", (int)jour);
                    command.Parameters.AddWithValue("@HeureDepart", heure.ToString());

                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        public int Ajouter(Horaire horaire)
        {
            // Vérifier s'il existe déjà un horaire identique
            if (ExisteHoraire(horaire.LigneId, horaire.ArretId, horaire.JourSemaine, horaire.HeureDepart))
            {
                throw new InvalidOperationException("Un horaire identique existe déjà pour cette ligne, cet arrêt, ce jour et cette heure.");
            }

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

                    int id = Convert.ToInt32(command.ExecuteScalar());
                    InvaliderCache();
                    return id;
                }
            }
        }

        public int AjouterEnLot(List<Horaire> horaires)
        {
            int nombreAjoutes = 0;

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = @"
                            INSERT INTO Horaires (LigneId, ArretId, JourSemaine, HeureDepart, EstActif, DateCreation, EstSupprime)
                            VALUES (@LigneId, @ArretId, @JourSemaine, @HeureDepart, @EstActif, @DateCreation, 0)";

                        using (SQLiteCommand command = new SQLiteCommand(query, connection, transaction))
                        {
                            foreach (var horaire in horaires)
                            {
                                // Vérifier les doublons
                                if (!ExisteHoraire(horaire.LigneId, horaire.ArretId, horaire.JourSemaine, horaire.HeureDepart))
                                {
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("@LigneId", horaire.LigneId);
                                    command.Parameters.AddWithValue("@ArretId", horaire.ArretId);
                                    command.Parameters.AddWithValue("@JourSemaine", (int)horaire.JourSemaine);
                                    command.Parameters.AddWithValue("@HeureDepart", horaire.HeureDepart.ToString());
                                    command.Parameters.AddWithValue("@EstActif", horaire.EstActif);
                                    command.Parameters.AddWithValue("@DateCreation", horaire.DateCreation);

                                    if (command.ExecuteNonQuery() > 0)
                                        nombreAjoutes++;
                                }
                            }
                        }

                        transaction.Commit();
                        InvaliderCache();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return nombreAjoutes;
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

                    bool result = command.ExecuteNonQuery() > 0;
                    if (result) InvaliderCache();
                    return result;
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
                    bool result = command.ExecuteNonQuery() > 0;
                    if (result) InvaliderCache();
                    return result;
                }
            }
        }

        public bool SupprimerParCriteres(HoraireCriteres criteres)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                var queryBuilder = new StringBuilder("UPDATE Horaires SET EstSupprime = 1 WHERE EstSupprime = 0");
                var parameters = new List<SQLiteParameter>();

                if (criteres.LigneId.HasValue)
                {
                    queryBuilder.Append(" AND LigneId = @LigneId");
                    parameters.Add(new SQLiteParameter("@LigneId", criteres.LigneId.Value));
                }

                if (criteres.ArretId.HasValue)
                {
                    queryBuilder.Append(" AND ArretId = @ArretId");
                    parameters.Add(new SQLiteParameter("@ArretId", criteres.ArretId.Value));
                }

                if (criteres.JourSemaine.HasValue)
                {
                    queryBuilder.Append(" AND JourSemaine = @JourSemaine");
                    parameters.Add(new SQLiteParameter("@JourSemaine", (int)criteres.JourSemaine.Value));
                }

                using (SQLiteCommand command = new SQLiteCommand(queryBuilder.ToString(), connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    bool result = command.ExecuteNonQuery() > 0;
                    if (result) InvaliderCache();
                    return result;
                }
            }
        }

        public Dictionary<DayOfWeek, int> ObtenirStatistiquesParJour(int ligneId)
        {
            var statistiques = new Dictionary<DayOfWeek, int>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT JourSemaine, COUNT(*) as Nombre
                    FROM Horaires 
                    WHERE LigneId = @LigneId AND EstSupprime = 0 AND EstActif = 1
                    GROUP BY JourSemaine";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LigneId", ligneId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DayOfWeek jour = (DayOfWeek)Convert.ToInt32(reader["JourSemaine"]);
                            int nombre = Convert.ToInt32(reader["Nombre"]);
                            statistiques[jour] = nombre;
                        }
                    }
                }
            }

            return statistiques;
        }

        public List<HoraireStatistique> ObtenirStatistiquesParLigne()
        {
            var statistiques = new List<HoraireStatistique>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT 
                        l.Id as LigneId,
                        l.Nom as LigneNom,
                        COUNT(h.Id) as NombreHoraires,
                        SUM(CASE WHEN h.EstActif = 1 THEN 1 ELSE 0 END) as NombreHorairesActifs,
                        MIN(h.HeureDepart) as PremierHoraire,
                        MAX(h.HeureDepart) as DernierHoraire
                    FROM Lignes l
                    LEFT JOIN Horaires h ON l.Id = h.LigneId AND h.EstSupprime = 0
                    WHERE l.EstSupprime = 0
                    GROUP BY l.Id, l.Nom
                    ORDER BY l.Numero";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var stat = new HoraireStatistique
                            {
                                LigneId = Convert.ToInt32(reader["LigneId"]),
                                LigneNom = reader["LigneNom"].ToString(),
                                NombreHoraires = Convert.ToInt32(reader["NombreHoraires"]),
                                NombreHorairesActifs = Convert.ToInt32(reader["NombreHorairesActifs"])
                            };

                            if (reader["PremierHoraire"] != DBNull.Value)
                                stat.PremierHoraire = TimeSpan.Parse(reader["PremierHoraire"].ToString());

                            if (reader["DernierHoraire"] != DBNull.Value)
                                stat.DernierHoraire = TimeSpan.Parse(reader["DernierHoraire"].ToString());

                            // Calculer la fréquence moyenne (en minutes)
                            if (stat.PremierHoraire.HasValue && stat.DernierHoraire.HasValue && stat.NombreHorairesActifs > 1)
                            {
                                var dureeTotal = stat.DernierHoraire.Value - stat.PremierHoraire.Value;
                                stat.FrequenceMoyenne = dureeTotal.TotalMinutes / (stat.NombreHorairesActifs - 1);
                            }

                            statistiques.Add(stat);
                        }
                    }
                }
            }

            return statistiques;
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
