using System;
using System.Collections.Generic;
using System.Linq;
using Fumoblilite.Systeme.Modeles;
using Fumoblilite.Systeme.Interfaces;

namespace Fumoblilite.Systeme.Services
{
    public class ServiceHoraire
    {
        private readonly IRepositoryHoraire _repositoryHoraire;

        public ServiceHoraire(IRepositoryHoraire repositoryHoraire)
        {
            _repositoryHoraire = repositoryHoraire;
        }

        public List<Horaire> ObtenirParLigne(int ligneId)
        {
            return _repositoryHoraire.ObtenirParLigne(ligneId);
        }

        public List<Horaire> ObtenirParArret(int arretId)
        {
            return _repositoryHoraire.ObtenirParArret(arretId);
        }

        public List<Horaire> ObtenirParLigneEtJour(int ligneId, DayOfWeek jour)
        {
            return _repositoryHoraire.ObtenirParLigneEtJour(ligneId, jour);
        }

        public List<Horaire> ObtenirParJour(DayOfWeek jour)
        {
            return _repositoryHoraire.ObtenirParJour(jour);
        }

        public List<Horaire> ObtenirParArretEtJour(int arretId, DayOfWeek jour)
        {
            return _repositoryHoraire.ObtenirParArretEtJour(arretId, jour);
        }

        public List<Horaire> ObtenirParCriteres(HoraireCriteres criteres)
        {
            return _repositoryHoraire.ObtenirParCriteres(criteres);
        }

        public List<Horaire> ObtenirProchains(int arretId, DateTime dateHeure, int limite = 10)
        {
            return _repositoryHoraire.ObtenirProchains(arretId, dateHeure, limite);
        }

        public List<Horaire> ObtenirParPlageHoraire(int ligneId, DayOfWeek jour, TimeSpan heureDebut, TimeSpan heureFin)
        {
            return _repositoryHoraire.ObtenirParPlageHoraire(ligneId, jour, heureDebut, heureFin);
        }

        public bool ExisteHoraire(int ligneId, int arretId, DayOfWeek jour, TimeSpan heure)
        {
            return _repositoryHoraire.ExisteHoraire(ligneId, arretId, jour, heure);
        }

        public int Ajouter(Horaire horaire)
        {
            ValiderHoraire(horaire);
            return _repositoryHoraire.Ajouter(horaire);
        }

        public int AjouterEnLot(List<Horaire> horaires)
        {
            foreach (var horaire in horaires)
            {
                ValiderHoraire(horaire);
            }
            return _repositoryHoraire.AjouterEnLot(horaires);
        }

        public bool Modifier(Horaire horaire)
        {
            ValiderHoraire(horaire);
            horaire.DateModification = DateTime.Now;
            return _repositoryHoraire.Modifier(horaire);
        }

        public bool Supprimer(int id)
        {
            return _repositoryHoraire.Supprimer(id);
        }

        public bool SupprimerParCriteres(HoraireCriteres criteres)
        {
            return _repositoryHoraire.SupprimerParCriteres(criteres);
        }

        public Dictionary<DayOfWeek, int> ObtenirStatistiquesParJour(int ligneId)
        {
            return _repositoryHoraire.ObtenirStatistiquesParJour(ligneId);
        }

        public List<HoraireStatistique> ObtenirStatistiquesParLigne()
        {
            return _repositoryHoraire.ObtenirStatistiquesParLigne();
        }

        // Méthodes utilitaires avancées

        public List<Horaire> GenererHorairesRecurrents(int ligneId, int arretId, List<DayOfWeek> jours,
            TimeSpan heureDebut, TimeSpan heureFin, TimeSpan intervalle)
        {
            var horaires = new List<Horaire>();

            foreach (var jour in jours)
            {
                var heureActuelle = heureDebut;
                while (heureActuelle <= heureFin)
                {
                    if (!ExisteHoraire(ligneId, arretId, jour, heureActuelle))
                    {
                        horaires.Add(new Horaire
                        {
                            LigneId = ligneId,
                            ArretId = arretId,
                            JourSemaine = jour,
                            HeureDepart = heureActuelle,
                            EstActif = true
                        });
                    }
                    heureActuelle = heureActuelle.Add(intervalle);
                }
            }

            return horaires;
        }

        public List<Horaire> DupliquerHoraires(int ligneSourceId, int ligneDestinationId, List<DayOfWeek> jours = null)
        {
            var horairesSource = ObtenirParLigne(ligneSourceId);
            var nouveauxHoraires = new List<Horaire>();

            foreach (var horaireSource in horairesSource)
            {
                if (jours == null || jours.Contains(horaireSource.JourSemaine))
                {
                    if (!ExisteHoraire(ligneDestinationId, horaireSource.ArretId, horaireSource.JourSemaine, horaireSource.HeureDepart))
                    {
                        nouveauxHoraires.Add(new Horaire
                        {
                            LigneId = ligneDestinationId,
                            ArretId = horaireSource.ArretId,
                            JourSemaine = horaireSource.JourSemaine,
                            HeureDepart = horaireSource.HeureDepart,
                            EstActif = horaireSource.EstActif
                        });
                    }
                }
            }

            return nouveauxHoraires;
        }

        public void OptimiserHoraires(int ligneId, DayOfWeek jour)
        {
            var horaires = ObtenirParLigneEtJour(ligneId, jour).OrderBy(h => h.HeureDepart).ToList();

            // Supprimer les doublons exacts
            for (int i = horaires.Count - 1; i > 0; i--)
            {
                if (horaires[i].HeureDepart == horaires[i - 1].HeureDepart &&
                    horaires[i].ArretId == horaires[i - 1].ArretId)
                {
                    Supprimer(horaires[i].Id);
                }
            }

            // Identifier les horaires trop rapprochés (moins de 2 minutes)
            var horairesASupprimer = new List<int>();
            for (int i = 1; i < horaires.Count; i++)
            {
                if (horaires[i].ArretId == horaires[i - 1].ArretId)
                {
                    var ecart = horaires[i].HeureDepart - horaires[i - 1].HeureDepart;
                    if (ecart.TotalMinutes < 2)
                    {
                        horairesASupprimer.Add(horaires[i].Id);
                    }
                }
            }

            foreach (var id in horairesASupprimer)
            {
                Supprimer(id);
            }
        }

        public List<string> ValiderCoherenceHoraires(int ligneId)
        {
            var erreurs = new List<string>();
            var horaires = ObtenirParLigne(ligneId);

            // Vérifier les horaires par jour
            foreach (DayOfWeek jour in Enum.GetValues(typeof(DayOfWeek)))
            {
                var horairesJour = horaires.Where(h => h.JourSemaine == jour).OrderBy(h => h.HeureDepart).ToList();

                // Vérifier qu'il n'y a pas de trous importants (plus de 2 heures sans service)
                for (int i = 1; i < horairesJour.Count; i++)
                {
                    var ecart = horairesJour[i].HeureDepart - horairesJour[i - 1].HeureDepart;
                    if (ecart.TotalHours > 2)
                    {
                        erreurs.Add($"{jour}: Écart important entre {horairesJour[i - 1].HeureDepart:hh\\:mm} et {horairesJour[i].HeureDepart:hh\\:mm}");
                    }
                }

                // Vérifier les heures de pointe (7h-9h et 17h-19h)
                var heuresPointe1 = horairesJour.Where(h => h.HeureDepart >= TimeSpan.FromHours(7) && h.HeureDepart <= TimeSpan.FromHours(9)).Count();
                var heuresPointe2 = horairesJour.Where(h => h.HeureDepart >= TimeSpan.FromHours(17) && h.HeureDepart <= TimeSpan.FromHours(19)).Count();

                if (heuresPointe1 < 3)
                    erreurs.Add($"{jour}: Peu de services en heure de pointe matinale (7h-9h): {heuresPointe1}");

                if (heuresPointe2 < 3)
                    erreurs.Add($"{jour}: Peu de services en heure de pointe vespérale (17h-19h): {heuresPointe2}");
            }

            return erreurs;
        }

        private void ValiderHoraire(Horaire horaire)
        {
            if (horaire.LigneId <= 0)
                throw new ArgumentException("L'ID de la ligne doit être positif.");

            if (horaire.ArretId <= 0)
                throw new ArgumentException("L'ID de l'arrêt doit être positif.");

            if (horaire.HeureDepart < TimeSpan.Zero || horaire.HeureDepart >= TimeSpan.FromDays(1))
                throw new ArgumentException("L'heure de départ doit être comprise entre 00:00 et 23:59.");

            if (!Enum.IsDefined(typeof(DayOfWeek), horaire.JourSemaine))
                throw new ArgumentException("Le jour de la semaine n'est pas valide.");
        }
    }
}
