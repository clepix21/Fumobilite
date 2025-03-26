using System;
using System.Windows.Forms;
using System.IO;
using Fumoblilite.SQL;
using Fumoblilite.Interface.Forms;

namespace Fumoblilite.Interface
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Chemin de la base de données
            string dossierApplication = AppDomain.CurrentDomain.BaseDirectory;
            string cheminBaseDonnees = Path.Combine(dossierApplication, "Fumoblilite.db");

            // Initialisation de la base de données
            GestionBaseDonnees gestionBD = new GestionBaseDonnees(cheminBaseDonnees);
            bool nouvelleBaseDonnees = gestionBD.CreerBaseDonneesSiNonExistante();
            //gestionBD.InsererDonneesInitiales();


            if (nouvelleBaseDonnees)
            {
                MessageBox.Show("Une nouvelle base de données a été créée avec des données de démonstration.\n\nIdentifiants par défaut :\nUtilisateur : admin\nMot de passe : admin",
                    "Base de données initialisée",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            Application.Run(new FormPrincipal(gestionBD.ConnectionString, null));
        }
    }
}

