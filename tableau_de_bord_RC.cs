using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static Cogitel_QT.tableau_de_bord_RC;
using System.Configuration;


namespace Cogitel_QT
{
    public partial class tableau_de_bord_RC : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;
        public tableau_de_bord_RC()
        {
            InitializeComponent();
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type dgvType = dataGridView1.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dataGridView1, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                // Activer le double buffering pour tous les TableLayoutPanel sur le formulaire
                foreach (System.Windows.Forms.Control control in this.Controls)
                {
                    if (control is Panel)
                    {
                        Type tlpType = control.GetType();
                        PropertyInfo pi = tlpType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                        pi.SetValue(control, true, null);
                    }
                }
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type dgvType = dataGridView2.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dataGridView2, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type dgvType = dataGridView3.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dataGridView3, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type chartType = chart1.GetType();
                PropertyInfo pi = chartType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(chart1, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type chartType = chart2.GetType();
                PropertyInfo pi = chartType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(chart2, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type chartType = chart3.GetType();
                PropertyInfo pi = chartType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(chart3, true, null);
            }
        
          
        }
        public class ClientReclamations
        {
            public string NomClient { get; set; }
            public int NombreReclamations { get; set; }
        }
        public class AnneeReclamations
        {
            public int Annee { get; set; }
            public int NombreReclamations { get; set; }
            public float sommeQuantiteRebutée { get; set; }
        }
        public class DefautReclamations
        {
            public string Defaut { get; set; }
            public int NombreReclamations { get; set; }
            
        }


        private void tableau_de_bord_RC_Load(object sender, EventArgs e)
        {

           

            // Créer la requête SQL pour extraire les noms de clients distincts et compter le nombre de réclamations de non-conformité pour chaque nom de client
            string query = "SELECT DISTINCT Client, COUNT(*) AS NombreReclamations " +
                            "FROM NCE " + 
                            "GROUP BY Client";

            // Créer une liste pour stocker les résultats
            List<ClientReclamations> clientsReclamations = new List<ClientReclamations>();

            // Établir la connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Exécuter la requête SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Parcourir les résultats de la requête
                        while (reader.Read())
                        {
                            // Extraire les valeurs du résultat de la requête
                            string nomClient = reader["Client"].ToString();
                            int nombreReclamations = Convert.ToInt32(reader["NombreReclamations"]);

                            // Créer un objet ClientReclamations pour stocker les valeurs extraites
                            ClientReclamations clientReclamations = new ClientReclamations
                            {
                                NomClient = nomClient,
                                NombreReclamations = nombreReclamations
                            };

                            // Ajouter l'objet ClientReclamations à la liste des résultats
                            clientsReclamations.Add(clientReclamations);
                        }
                    }
                }

                connection.Close();
            }
            clientsReclamations.Sort((x, y) => y.NombreReclamations.CompareTo(x.NombreReclamations));

            // Mettre à jour la source de données du DataGridView avec la liste triée
            dataGridView1.DataSource = clientsReclamations;

            // Définir les noms de colonnes dans le DataGridView
            dataGridView1.Columns[0].HeaderText = "Client"; // Nom de colonne 1
            dataGridView1.Columns[1].HeaderText = "N° R"; // Nom de colonne 2
            dataGridView1.Columns[0].Width = 220;
            dataGridView1.Columns[1].Width = 50;

            Color couleurDebut = Color.LightSkyBlue; // Couleur de départ
            Color couleurFin = Color.MidnightBlue; // Couleur de fin

            // Parcourir les résultats et ajouter les données au graphique
            foreach (ClientReclamations clientReclamation in clientsReclamations)
            {
                // Ajouter un point de données pour chaque barre du graphique
                chart1.Series[0].Points.AddXY(clientReclamation.NomClient, clientReclamation.NombreReclamations);
            }

            // Définir la palette de couleurs pour la série du graphique à barres
            chart1.Series[0].Palette = ChartColorPalette.None; // Désactiver la palette par défaut
            chart1.ApplyPaletteColors(); // Appliquer les couleurs personnalisées aux points de données

            // Calculer le pourcentage du nombre de réclamations par rapport au nombre maximum de réclamations
            int maximumReclamations = clientsReclamations.Max(c => c.NombreReclamations);
            foreach (var point in chart1.Series[0].Points)
            {
                double pourcentageReclamations = (double)point.YValues[0] / maximumReclamations;

                // Calculer les valeurs de couleur RVB pour la dégradation en fonction du pourcentage de réclamations
                int r = couleurDebut.R + (int)((couleurFin.R - couleurDebut.R) * pourcentageReclamations);
                int g = couleurDebut.G + (int)((couleurFin.G - couleurDebut.G) * pourcentageReclamations);
                int b = couleurDebut.B + (int)((couleurFin.B - couleurDebut.B) * pourcentageReclamations);

                // Créer une couleur RVB en utilisant les valeurs calculées
                Color couleurBarre = Color.FromArgb(r, g, b);

                // Appliquer la couleur calculée au point de données
                point.Color = couleurBarre;
            }
            chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
            chart1.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
            chart1.ChartAreas[0].AxisY.Title = "Nombre de réclamations";
            chart1.ChartAreas[0].AxisX.Title = "Client";

            string query1 = "SELECT YEAR(Date_de_réclamtion) AS Annee, COUNT(*) AS NombreReclamations, SUM(Quantité_rebutée_totale_Kg) AS SommeQuantiteRebutée " +
                             "FROM NCE " +
                             "GROUP BY YEAR(Date_de_réclamtion)";

            // Créer une liste pour stocker les résultats
            List<AnneeReclamations> anneesReclamations = new List<AnneeReclamations>();

            // Établir la connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Exécuter la requête SQL
                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Parcourir les résultats de la requête
                        while (reader.Read())
                        {
                            // Extraire les valeurs du résultat de la requête
                            object anneeObj = reader["Annee"];
                            int annee = anneeObj != DBNull.Value ? Convert.ToInt32(anneeObj) : 0;
                            int nombreReclamations = Convert.ToInt32(reader["NombreReclamations"]);
                            object sommeQuantiteRebutéeObj = reader["SommeQuantiteRebutée"];
                            int sommeQuantiteRebutée = DBNull.Value.Equals(sommeQuantiteRebutéeObj) ? 0 : Convert.ToInt32(sommeQuantiteRebutéeObj);

                            // Vérifier si la valeur de annee est différente de 0 avant d'ajouter l'objet AnneeReclamations
                            if (annee != 0)
                            {
                                // Créer un objet AnneeReclamations pour stocker les valeurs extraites
                                AnneeReclamations anneeReclamations = new AnneeReclamations
                                {
                                    Annee = annee,
                                    NombreReclamations = nombreReclamations,
                                    sommeQuantiteRebutée = sommeQuantiteRebutée
                                };

                                // Ajouter l'objet AnneeReclamations à la liste des résultats
                                anneesReclamations.Add(anneeReclamations);
                            }
                        }
                    }
                }

                connection.Close();
            }

            anneesReclamations.Sort((x, y) => y.Annee.CompareTo(x.Annee));

            // Mettre à jour la source de données du DataGridView avec la liste triée
            dataGridView2.DataSource = anneesReclamations;

            // Définir les noms de colonnes dans le DataGridView
            dataGridView2.Columns[0].HeaderText = "Année"; // Nom de colonne 1
            dataGridView2.Columns[1].HeaderText = "N° R"; // Nom de colonne 2
            dataGridView2.Columns[2].HeaderText = "Quantité Rebutée";
            dataGridView2.Columns[0].Width = 100;
            dataGridView2.Columns[1].Width = 50;
            dataGridView2.Columns[2].Width = 150;
         

            // Parcourir les résultats et ajouter les données au graphique
            foreach (AnneeReclamations anneesReclamation in anneesReclamations)
            {
                // Ajouter un point de données pour chaque barre du graphique
                chart3.Series[0].Points.AddXY(anneesReclamation.Annee, anneesReclamation.NombreReclamations);
            }

            chart3.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            chart3.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
            chart3.ChartAreas[0].AxisY.Title = "Nombre de réclamations";
            chart3.ChartAreas[0].AxisX.Title = "Année";


            string query2 = "SELECT Défaut, COUNT(*) AS NombreReclamations " +
                 "FROM NCE " +
                 "GROUP BY Défaut";

            // Créer une liste pour stocker les résultats
            List<DefautReclamations> defautReclamations = new List<DefautReclamations>();

            // Établir la connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Exécuter la requête SQL
                using (SqlCommand command = new SqlCommand(query2, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Parcourir les résultats de la requête
                        while (reader.Read())
                        {
                            // Extraire les valeurs du résultat de la requête
                            string defaut = Convert.ToString(reader["Défaut"]);
                            int nombreReclamations = Convert.ToInt32(reader["NombreReclamations"]);

                            // Créer un objet DefautReclamations pour stocker les valeurs extraites
                            DefautReclamations defautReclamation = new DefautReclamations
                            {
                                Defaut = defaut,
                                NombreReclamations = nombreReclamations
                            };

                            // Ajouter l'objet DefautReclamations à la liste des résultats
                            defautReclamations.Add(defautReclamation);
                        }
                    }
                }

                connection.Close();
            }
            defautReclamations = defautReclamations.OrderByDescending(x => x.NombreReclamations).ToList();
            // Mettre à jour la source de données du DataGridView avec la liste des résultats
            dataGridView3.DataSource = defautReclamations;

            // Définir les noms de colonnes dans le DataGridView
            dataGridView3.Columns[0].HeaderText = "Défaut"; // Nom de colonne 1
            dataGridView3.Columns[1].HeaderText = "N° R"; // Nom de colonne 2
            dataGridView3.Columns[0].Width = 100;
            dataGridView3.Columns[1].Width = 100;
            chart2.ChartAreas[0].AxisX.Title = "Défaut"; // Définir le titre de l'axe X
            chart2.ChartAreas[0].AxisY.Title = "N° R";
            System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
            series.ChartType = SeriesChartType.Pie;
            series.Name = "Réclamations par défaut";
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "{0}%";
            series.LabelToolTip = "#PERCENT";
            series.Points.DataBind(defautReclamations, "Defaut", "NombreReclamations", "");

            // Ajouter la série au graphique
            chart2.Series.Add(series);


            double delaiReponseMoyen = 0;
            string query3 = "SELECT AVG(Délais_de_réponse) AS DelaiReponseMoyen " +
                             "FROM NCE";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query3, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && reader["DelaiReponseMoyen"] != DBNull.Value)
                        {
                            delaiReponseMoyen = Convert.ToDouble(reader["DelaiReponseMoyen"]);
                        }
                    }
                }
                connection.Close();
            }
            string descriptionDelaiReponseMoyen = string.Empty;
            if (delaiReponseMoyen > 0)
            {
                descriptionDelaiReponseMoyen = string.Format("Le délai de réponse moyen = {0} jours.", delaiReponseMoyen);
            }
            else
            {
                descriptionDelaiReponseMoyen = "Aucune donnée de délai de réponse moyen disponible.";
            }

            // Mettre la description du délai de réponse moyen dans le Label
            label1.Text = descriptionDelaiReponseMoyen;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
    }
}
