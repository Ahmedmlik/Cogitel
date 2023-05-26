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
        public class CoutReclamations
        {
            public int Annee { get; set; }
            public double SommeCoutGlobal { get; set; }
        }
        public class QuantiteReclamations
        {
            public int Annee { get; set; }
            public int QuantiteNC { get; set; }
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

        public class ConducteurReclamations
        {
            public string NomConducteur { get; set; }
            public int NombreReclamations { get; set; }
        }
        private void tableau_de_bord_RC_Load(object sender, EventArgs e)
        {
            string query4 = "SELECT DISTINCT Client FROM NCE";

            // Établissez la connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Exécutez la requête SQL
                using (SqlCommand command = new SqlCommand(query4, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        comboBox1.Items.Add("Veuillez sélectionner un client");
                        // Parcourez les résultats de la requête
                        while (reader.Read())
                        {
                            // Ajoutez le client au ComboBox
                            comboBox1.Items.Add(reader["Client"].ToString());
                        }
                    }
                }

                connection.Close();
                comboBox1.SelectedIndex = 0;
            }


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                string sqlQueryYears = "SELECT DISTINCT YEAR(Date_de_réclamtion) as year FROM NCE";
                using (SqlCommand command = new SqlCommand(sqlQueryYears, connection))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable tableYears = new DataTable();
                    adapter.Fill(tableYears);
                    // Create an empty row
                    DataRow emptyRow = tableYears.NewRow();

                    // Set the value of the "year" column to DBNull.Value
                    DataTable modifiedTableYears = new DataTable();
                    modifiedTableYears.Columns.Add("year", typeof(string));

                    // Add the "TOUT" value as the first row
                    modifiedTableYears.Rows.Add("TOUT");

                    // Copy the remaining rows from the original table to the modified table
                    foreach (DataRow row in tableYears.Rows)
                    {
                        modifiedTableYears.ImportRow(row);
                    }

                    // Bind the results to the ComboBox
                    comboBox2.DataSource = modifiedTableYears;
                    comboBox2.DisplayMember = "year";
                    comboBox2.ValueMember = "year";
                    // Set the default value to "TOUT"
                    comboBox2.SelectedValue = "TOUT";
                    comboBox4.DataSource = modifiedTableYears;
                    comboBox4.DisplayMember = "year";
                    comboBox4.ValueMember = "year";
                    // Set the default value to "TOUT"
                    comboBox4.SelectedValue = "TOUT";
                    comboBox5.DataSource = modifiedTableYears;
                    comboBox5.DisplayMember = "year";
                    comboBox5.ValueMember = "year";
                    // Set the default value to "TOUT"
                    comboBox5.SelectedValue = "TOUT";
                    comboBox6.DataSource = modifiedTableYears;
                    comboBox6.DisplayMember = "year";
                    comboBox6.ValueMember = "year";
                    // Set the default value to "TOUT"
                    comboBox6.SelectedValue = "TOUT";

                }
                connection.Close();
            }
            string query5 = "SELECT DISTINCT Nom_et_prénom_du_conducteur_du_défaut FROM NCE";

            // Établissez la connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Exécutez la requête SQL
                using (SqlCommand command = new SqlCommand(query5, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        comboBox3.Items.Add("Veuillez sélectionner un conducteur");
                        comboBox3.Items.Add("TOUS");
                        // Parcourez les résultats de la requête
                        while (reader.Read())
                        {
                            // Ajoutez le client au ComboBox
                            comboBox3.Items.Add(reader["Nom_et_prénom_du_conducteur_du_défaut"].ToString());
                        }
                    }
                }

                connection.Close();
                comboBox3.SelectedIndex = 0;
            }

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
            chart4.ChartAreas[0].AxisX.Title = "Défaut"; // Définir le titre de l'axe X
            chart4.ChartAreas[0].AxisY.Title = "N° R";
            System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
            series.ChartType = SeriesChartType.Pie;
            series.Name = "Réclamations par défaut";
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "{0}%";
            series.LabelToolTip = "#PERCENT";
            series.Points.DataBind(defautReclamations, "Defaut", "NombreReclamations", "");

            // Ajouter la série au graphique
            chart4.Series.Add(series);


            double tempsReponseTotal = 0;
            int nombreReclamations1 = 0;

            string query10 = "SELECT Date_de_réclamtion, Date_réponse_client FROM NCE";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query10, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!Convert.IsDBNull(reader["Date_de_réclamtion"]) && !Convert.IsDBNull(reader["Date_réponse_client"]))
                            {
                                DateTime dateReclamation = Convert.ToDateTime(reader["Date_de_réclamtion"]);
                                DateTime dateReponse = Convert.ToDateTime(reader["Date_réponse_client"]);

                                // Calculer le temps de réponse en excluant les samedis et dimanches
                                int joursOuvrables = 0;
                                DateTime dateTemp = dateReclamation.AddDays(1); // Ignorer la date de réclamation

                                while (dateTemp <= dateReponse)
                                {
                                    if (dateTemp.DayOfWeek != DayOfWeek.Saturday && dateTemp.DayOfWeek != DayOfWeek.Sunday)
                                    {
                                        joursOuvrables++;
                                    }
                                    dateTemp = dateTemp.AddDays(1);
                                }

                                // Ajouter le temps de réponse en jours ouvrables au total
                                tempsReponseTotal += joursOuvrables;
                                nombreReclamations1++;
                            }
                        }
                    }
                }
                connection.Close();
            }

            double tempsReponseMoyen = tempsReponseTotal / nombreReclamations1;

            string descriptionTempsReponseMoyen = string.Empty;
            if (nombreReclamations1 > 0)
            {
                descriptionTempsReponseMoyen = string.Format("Le temps de réponse moyen = {0:F2} jours ouvrables.", tempsReponseMoyen);
            }
            else
            {
                descriptionTempsReponseMoyen = "Aucune donnée de temps de réponse moyen disponible.";
            }

            // Mettre la description du temps de réponse moyen dans le Label
            label1.Text = descriptionTempsReponseMoyen;

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
        public class DefautClient
        {
            public string Client { get; set; }
            public string Defaut { get; set; }
            public int NombreReclamations { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Veuillez sélectionner un client")
            {
                MessageBox.Show("Veuillez sélectionner un client.");
                return;
            }

            // Obtenez le client sélectionné dans le ComboBox
            string selectedClient = comboBox1.SelectedItem.ToString();

            // Exécutez la requête pour récupérer les types de défauts et le nombre de réclamations associés à ce client
            string query = "SELECT Défaut, COUNT(*) AS NombreReclamations " +
                           "FROM NCE " +
                           "WHERE Client = @Client " +
                           "GROUP BY Défaut";

            // Créez une liste pour stocker les résultats
            List<DefautClient> defautClients = new List<DefautClient>();

            // Établissez la connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Exécutez la requête SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Client", selectedClient);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Parcourez les résultats de la requête
                        while (reader.Read())
                        {
                            // Extrayez les valeurs du résultat de la requête
                            string defaut = Convert.ToString(reader["Défaut"]);
                            int nombreReclamations = Convert.ToInt32(reader["NombreReclamations"]);

                            // Créez un objet DefautClient pour stocker les valeurs extraites
                            DefautClient defautClient = new DefautClient
                            {
                                Client = selectedClient,
                                Defaut = defaut,
                                NombreReclamations = nombreReclamations
                            };

                            // Ajoutez l'objet DefautClient à la liste des résultats
                            defautClients.Add(defautClient);
                        }
                    }
                }

                connection.Close();
            }

            // Triez les résultats par nombre de réclamations (du plus grand au plus petit)
            defautClients = defautClients.OrderByDescending(x => x.NombreReclamations).ToList();

            // Effacez les points existants dans la série du graphique
            chart2.Series[0].Points.Clear();

            int totalReclamations = defautClients.Sum(x => x.NombreReclamations);

            foreach (DefautClient defautClient in defautClients)
            {
                // Calculez le pourcentage du nombre de réclamations par rapport au total
                double pourcentage = (defautClient.NombreReclamations / (double)totalReclamations) * 100;
                chart2.Series[0].Points.AddXY(defautClient.Defaut, defautClient.NombreReclamations);
                chart2.Series[0].Points.Last().Label = $"{pourcentage:F2}%";
            }

            // Définir les titres des axes du graphique
            chart2.ChartAreas[0].AxisX.Title = "Défaut";
            chart2.ChartAreas[0].AxisY.Title = "Nombre de réclamations";

            // Redessiner le graphique
            chart2.Refresh();
        }
        private void button1_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button1, "Valider");
            this.Cursor = Cursors.Hand;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button2_Click(object sender, EventArgs e)

        {
            string selectedYear = comboBox2.SelectedValue?.ToString();
            string query = "SELECT Client, COUNT(*) AS NombreReclamations " +
                           "FROM NCE ";

            if (selectedYear != "TOUT")
            {
                int year;
                if (int.TryParse(selectedYear, out year))
                {
                    query += "WHERE YEAR(Date_de_réclamtion) = @Year ";
                }
                else
                {
                    // Handle invalid selectedYear value
                    MessageBox.Show("Invalid selected year.");
                    return;
                }
            }

            query += "GROUP BY Client";


            // Créer une liste pour stocker les résultats
            List<ClientReclamations> clientsReclamations = new List<ClientReclamations>();

            // Établir la connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Exécuter la requête SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (selectedYear != "TOUT" && int.TryParse(selectedYear, out int year))
                    {
                        command.Parameters.AddWithValue("@Year", year);
                    }
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
            chart1.Series[0].Points.Clear();
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
            chart1.Refresh();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "Veuillez sélectionner un conducteur")
            {
                MessageBox.Show("Veuillez sélectionner un conducteur.");
                return;
            }
            string selectedYear1 = comboBox4.SelectedValue?.ToString();
            string selectedConducteur = comboBox3.SelectedItem.ToString();

            string query = "SELECT Nom_et_prénom_du_conducteur_du_défaut AS Conducteur, COUNT(*) AS NombreReclamations " +
                "FROM NCE ";

            if (selectedConducteur != "TOUS" && selectedYear1 != "TOUT")
            {
                int year;
                if (int.TryParse(selectedYear1, out year))
                {
                    query += "WHERE Nom_et_prénom_du_conducteur_du_défaut = @Conducteur ";
                    query += "AND YEAR(Date_de_réclamtion) = @Year ";
                }
                else
                {
                    // Handle invalid selectedYear value
                    MessageBox.Show("Invalid selected year.");
                    return;
                }
            }
            else if (selectedConducteur != "TOUS")
            {
                query += "WHERE Nom_et_prénom_du_conducteur_du_défaut = @Conducteur ";
            }
            else if (selectedYear1 != "TOUT")
            {
                int year;
                if (int.TryParse(selectedYear1, out year))
                {
                    query += "WHERE YEAR(Date_de_réclamtion) = @Year ";
                }
                else
                {
                    // Handle invalid selectedYear value
                    MessageBox.Show("Invalid selected year.");
                    return;
                }
            }

            query += "GROUP BY Nom_et_prénom_du_conducteur_du_défaut";

            List<ConducteurReclamations> conducteurReclamations = new List<ConducteurReclamations>();

            // Établir la connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Exécuter la requête SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (selectedConducteur != "TOUS")
                    {
                        command.Parameters.AddWithValue("@Conducteur", selectedConducteur);
                    }

                    if (selectedYear1 != "TOUT")
                    {
                        int year;
                        if (int.TryParse(selectedYear1, out year))
                        {
                            command.Parameters.AddWithValue("@Year", year);
                        }
                    }
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Parcourir les résultats de la requête
                        while (reader.Read())
                        {
                            // Extraire les valeurs du résultat de la requête
                            string nomConducteur = reader["Conducteur"].ToString();
                            int nombreReclamations = Convert.ToInt32(reader["NombreReclamations"]);

                            // Créer un objet ConducteurReclamations pour stocker les valeurs extraites
                            ConducteurReclamations conducteurReclamationsItem = new ConducteurReclamations
                            {
                                NomConducteur = nomConducteur,
                                NombreReclamations = nombreReclamations
                            };

                            // Ajouter l'objet ConducteurReclamations à la liste des résultats
                            conducteurReclamations.Add(conducteurReclamationsItem);
                        }
                    }
                }

                connection.Close();
            }

            conducteurReclamations.Sort((x, y) => y.NombreReclamations.CompareTo(x.NombreReclamations));

            // Effacer les séries existantes dans le graphique à barres
            chart5.Series[0].Points.Clear();

            // Créer une nouvelle série pour le graphique à barres


            // Parcourir les résultats et ajouter les données au graphique
            foreach (ConducteurReclamations conducteurReclamation in conducteurReclamations)
            {
                // Ajouter un point de données pour chaque barre du graphique
                chart5.Series[0].Points.AddXY(conducteurReclamation.NomConducteur, conducteurReclamation.NombreReclamations);
            }

            // Ajouter la série au graphique


            // Définir les propriétés du graphique
            chart5.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            chart5.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
            chart5.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
            chart5.ChartAreas[0].AxisY.Title = "Nombre de réclamations";
            chart5.ChartAreas[0].AxisX.Title = "Conducteur";
            chart5.Refresh();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string selectedYear = comboBox5.SelectedValue?.ToString();

            string query = "SELECT YEAR(Date_de_réclamtion) AS Annee, SUM(Coût_globale_de_la_NC_DT) AS SommeCoutGlobal " +
                           "FROM NCE ";

            if (selectedYear != "TOUT")
            {
                int year;
                if (int.TryParse(selectedYear, out year))
                {
                    query += "WHERE YEAR(Date_de_réclamtion) = @SelectedYear ";
                }
                else
                {
                    // Gérer une valeur d'année sélectionnée invalide
                    MessageBox.Show("Invalid selected year.");
                    return;
                }
            }

            query += "GROUP BY YEAR(Date_de_réclamtion)";

            List<CoutReclamations> coutReclamations = new List<CoutReclamations>();

            // Établir la connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Exécuter la requête SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (selectedYear != "TOUT")
                    {
                        int year;
                        if (int.TryParse(selectedYear, out year))
                        {
                            command.Parameters.AddWithValue("@SelectedYear", year);
                        }
                    }
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Parcourir les résultats de la requête
                        while (reader.Read())
                        {
                            // Extraire les valeurs du résultat de la requête
                            int year = Convert.ToInt32(reader["Annee"]);
                            double cost = Convert.ToDouble(reader["SommeCoutGlobal"]);

                            // Créer un objet CoutReclamations pour stocker les valeurs extraites
                            CoutReclamations reclamations = new CoutReclamations
                            {
                                Annee = year,
                                SommeCoutGlobal = cost
                            };

                            // Ajouter l'objet CoutReclamations à la liste des coûts de réclamations
                            coutReclamations.Add(reclamations);
                        }
                    }
                }

                connection.Close();
            }

            // Effacer les séries existantes dans le graphique à barres
            chart6.Series[0].Points.Clear();
            // Parcourir les coûts de réclamations et ajouter les données au graphique
            foreach (CoutReclamations reclamations in coutReclamations)
            {
                // Ajouter un point de données pour chaque barre du graphique
                chart6.Series[0].Points.AddXY(reclamations.Annee, reclamations.SommeCoutGlobal);
            }

            // Définir les propriétés du graphique
            chart6.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            chart6.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
            chart6.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
            chart6.ChartAreas[0].AxisY.Title = "Coût global NC DT";
            chart6.ChartAreas[0].AxisX.Title = "Année";
            chart6.Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string selectedYear = comboBox6.SelectedValue?.ToString();
            string query = "SELECT YEAR(Date_de_réclamtion) AS Annee, SUM(Quantité_NC_Kg) AS SommeQuantiteNC " +
                       "FROM NCE ";

            if (selectedYear != "TOUT")
            {
                int year;
                if (int.TryParse(selectedYear, out year))
                {
                    query += "WHERE YEAR(Date_de_réclamtion) = @SelectedYear ";
                }
                else
                {
                    MessageBox.Show("Invalid selected year.");
                    return;
                }
            }

            query += "GROUP BY YEAR(Date_de_réclamtion)";
            List<QuantiteReclamations> quantiteReclamations = new List<QuantiteReclamations>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (selectedYear != "TOUT")
                    {
                        command.Parameters.AddWithValue("@SelectedYear", selectedYear);
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int year = Convert.ToInt32(reader["Annee"]);
                            int quantitySum = Convert.ToInt32(reader["SommeQuantiteNC"]);

                            QuantiteReclamations reclamations = new QuantiteReclamations
                            {
                                Annee = year,
                                QuantiteNC = quantitySum
                            };

                            quantiteReclamations.Add(reclamations);
                        }
                    }
                }

                connection.Close();
            }
            quantiteReclamations.Sort((x, y) => x.Annee.CompareTo(y.Annee));
            chart7.Series[0].Points.Clear();
            foreach (QuantiteReclamations reclamations in quantiteReclamations)
            {
                chart7.Series[0].Points.AddXY(reclamations.Annee, reclamations.QuantiteNC);
            }
            chart7.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            chart7.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
            chart7.ChartAreas[0].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.WordWrap;
            chart7.ChartAreas[0].AxisY.Title = "Quantité non conformes KG";
            chart7.ChartAreas[0].AxisX.Title = "Année";
            chart7.Refresh();
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button2, "Valider");
            this.Cursor = Cursors.Hand;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button3, "Valider");
            this.Cursor = Cursors.Hand;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button5, "Valider");
            this.Cursor = Cursors.Hand;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button7_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button7, "Valider");
            this.Cursor = Cursors.Hand;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
    }
}