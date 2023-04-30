using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.VariantTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Configuration;

namespace Cogitel_QT
{
    public partial class ERCAMS : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;
        SqlConnection connection; 
        private Erc Erc;
        public ERCAMS(Erc Erc )
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            this.Erc = Erc;
            textBox35.TextChanged += textBox_TextChanged;
            textBox36.TextChanged += textBox_TextChanged;
            textBox37.TextChanged += textBox_TextChanged;
            textBox38.TextChanged += textBox_TextChanged;
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                // Activer le double buffering pour tous les TableLayoutPanel sur le formulaire
                foreach (Control control in this.Controls)
                {
                    if (control is TableLayoutPanel)
                    {
                        Type tlpType = control.GetType();
                        PropertyInfo pi = tlpType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                        pi.SetValue(control, true, null);
                    }
                }
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                // Activer le double buffering pour tous les TableLayoutPanel sur le formulaire
                foreach (Control control in this.Controls)
                {
                    if (control is TextBox)
                    {
                        Type tlpType = control.GetType();
                        PropertyInfo pi = tlpType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                        pi.SetValue(control, true, null);
                    }
                }
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                // Activer le double buffering pour tous les TableLayoutPanel sur le formulaire
                foreach (Control control in this.Controls)
                {
                    if (control is Label)
                    {
                        Type tlpType = control.GetType();
                        PropertyInfo pi = tlpType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                        pi.SetValue(control, true, null);
                    }
                }
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                // Activer le double buffering pour tous les TableLayoutPanel sur le formulaire
                foreach (Control control in this.Controls)
                {
                    if (control is Panel)
                    {
                        Type tlpType = control.GetType();
                        PropertyInfo pi = tlpType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                        pi.SetValue(control, true, null);
                    }
                }
            }

        }
        public void SetButtonVisible(bool isVisible)
        {
            // Définissez la propriété Visible du bouton en fonction de la valeur booléenne passée en paramètre
            button2.Visible = isVisible;
        }
        private void button4_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            // Création de la requête SQL avec des paramètres
            string query = "INSERT INTO NCE VALUES (";
            for (int i = 1; i <= 70; i++)
            {
                TextBox textBox = this.Controls.Find("textBox" + i, true).FirstOrDefault() as TextBox;
                if (textBox != null)
                {
                    query += "@param" + i;
                    if (i < 70)
                    {
                        query += ", ";
                    }
                }
            }
            query += ")";

            // Création de la commande SQL avec les paramètres
            SqlCommand command = new SqlCommand(query, connection);
            for (int i = 1; i <= 70; i++)
            {
                TextBox textBox = this.Controls.Find("textBox" + i, true).FirstOrDefault() as TextBox;
                if (textBox != null)
                {
                    float value;
                    if (float.TryParse(textBox.Text, out value))
                    {
                        command.Parameters.AddWithValue("@param" + i, value);
                    }
                    else if (DateTime.TryParse(textBox.Text, out DateTime dateValue))
                    {
                        command.Parameters.AddWithValue("@param" + i, dateValue);
                    }
                    else if (string.IsNullOrEmpty(textBox.Text))
                    {
                        command.Parameters.AddWithValue("@param" + i, DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@param" + i, textBox.Text);
                    }

                }
            }

            // Exécution de la commande SQL
            command.ExecuteNonQuery();
            // Fermeture de la connexion
            connection.Close();
            Erc.allData.Clear();
            Erc.LoadData();
            this.Close();
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.Text = "";

            }
            else if (!float.TryParse(textBox3.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox3.Text = "";

            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            DateTime result;
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Text = "";

            }
            else

             if (!DateTime.TryParseExact(textBox2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                MessageBox.Show("Vous devez entrer une date valide au format jj/mm/aaaa.");
                textBox2.Text = "";

            }

        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            DateTime result;
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                textBox4.Text = "";

            }
            else
            if (!DateTime.TryParseExact(textBox4.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                MessageBox.Show("Vous devez entrer une date valide au format jj/mm/aaaa.");
                textBox4.Text = "";

            }
        }

        private void textBox20_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox20.Text))
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Définir la requête SQL pour récupérer les informations de la table
                string query = "SELECT * FROM N__des_conds_et_des_aides_Conds WHERE Matricule = @Matricule";

                // Créer un objet de commande pour exécuter la requête SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Ajouter le paramètre ID à la commande
                    command.Parameters.Add("@Matricule", SqlDbType.NVarChar).Value = textBox20.Text.ToString();

                    // Exécuter la commande et récupérer les données
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Vérifier si des données ont été trouvées pour l'ID spécifié
                        if (reader.HasRows)
                        {
                            // Lire les données et les afficher dans les champs de texte
                            while (reader.Read())
                            {
                                textBox21.Text = reader["NOMETPRENOM"].ToString();

                                // etc. pour les autres colonnes de la table
                            }
                        }
                        else
                        {
                            // Réinitialiser les champs de texte
                            textBox20.Text = "";
                            // etc. pour les autres champs de texte
                            MessageBox.Show("Aucune donnée trouvée pour  Matricule spécifié  .");
                        }
                    }
                    connection.Close();
                }

            }   // Fermer la connexion à la base de données
              
                  else if (string.IsNullOrEmpty(textBox20.Text))
                {
                    textBox20.Text = "";


                }
        }



        private void textBox22_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox22.Text))
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Définir la requête SQL pour récupérer les informations de la table
                string query = "SELECT * FROM N__des_conds_et_des_aides_Conds WHERE Matricule = @Matricule";

                // Créer un objet de commande pour exécuter la requête SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Ajouter le paramètre ID à la commande
                    command.Parameters.Add("@Matricule", SqlDbType.NVarChar).Value = textBox22.Text.ToString();

                    // Exécuter la commande et récupérer les données
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Vérifier si des données ont été trouvées pour l'ID spécifié
                        if (reader.HasRows)
                        {
                            // Lire les données et les afficher dans les champs de texte
                            while (reader.Read())
                            {
                                textBox23.Text = reader["NOMETPRENOM"].ToString();

                                // etc. pour les autres colonnes de la table
                            }
                        }
                        else
                        {
                            // Réinitialiser les champs de texte
                            textBox22.Text = "";
                            // etc. pour les autres champs de texte
                            MessageBox.Show("Aucune donnée trouvée pour  Matricule spécifié  .");
                        }
                    }
                    connection.Close();
                }     }

                // Fermer la connexion à la base de données
                
                 else if (string.IsNullOrEmpty(textBox22.Text))
                {
                    textBox22.Text = "";


                }
            
        }

        private void textBox32_Leave(object sender, EventArgs e)
        {
            int result;

            if (string.IsNullOrEmpty(textBox32.Text))
            {
                textBox32.Text = "";

            }
            else
            if (!int.TryParse(textBox32.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox32.Text = "";

            }
        }

        private void textBox35_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox35.Text))
            {
                textBox35.Text = "";

            }
            else
            if (!float.TryParse(textBox35.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox35.Text = "";

            }
        }

        private void textBox36_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox36.Text))
            {
                textBox36.Text = "";

            }
            else
            if (!float.TryParse(textBox36.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox36.Text = "";

            }
        }

        private void textBox37_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox37.Text))
            {
                textBox37.Text = "";

            }
            else
            if (!float.TryParse(textBox37.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox37.Text = "";

            }
        }

        private void textBox38_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox38.Text))
            {
                textBox38.Text = "";

            }
            else
            if (!float.TryParse(textBox38.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox38.Text = "";

            }
        }

        private void textBox39_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox39.Text))
            {
                textBox39.Text = "";

            }
            else
            if (!float.TryParse(textBox39.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox39.Text = "";

            }
        }

        private void textBox40_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox40.Text))
            {
                textBox40.Text = "";

            }
            else
            if (!float.TryParse(textBox40.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox40.Text = "";

            }
        }

        private void textBox41_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox41.Text))
            {
                textBox41.Text = "";

            }
            else
            if (!float.TryParse(textBox41.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox41.Text = "";

            }
        }

        private void textBox42_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox42.Text))
            {
                textBox42.Text = "";

            }
            else
            if (!float.TryParse(textBox42.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox42.Text = "";

            }
        }

        private void textBox43_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox43.Text))
            {
                textBox43.Text = "";

            }
            else
            if (!float.TryParse(textBox43.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox43.Text = "";

            }
        }

        private void textBox44_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox44.Text))
            {
                textBox44.Text = "";

            }
            else
            if (!float.TryParse(textBox44.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox44.Text = "";

            }
        }

        private void textBox45_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox45.Text))
            {
                textBox45.Text = "";

            }
            else
            if (!float.TryParse(textBox45.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox45.Text = "";

            }
        }

        private void textBox46_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox46.Text))
            {
                textBox46.Text = "";

            }
            else
            if (!float.TryParse(textBox46.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox46.Text = "";

            }
        }

        private void textBox47_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox47.Text))
            {
                textBox47.Text = "";

            }
            else
            if (!float.TryParse(textBox47.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox47.Text = "";

            }
        }

        private void textBox48_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox48.Text))
            {
                textBox48.Text = "";

            }
            else
            if (!float.TryParse(textBox48.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox48.Text = "";

            }
        }

        private void textBox49_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox49.Text))
            {
                textBox49.Text = "";

            }
            else
            if (!float.TryParse(textBox49.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox49.Text = "";

            }
        }

        private void textBox59_Leave(object sender, EventArgs e)
        {
            DateTime result;
            if (string.IsNullOrEmpty(textBox59.Text))
            {
                textBox59.Text = "";

            }
            else
            if (!DateTime.TryParseExact(textBox59.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                MessageBox.Show("Vous devez entrer une date valide au format jj/mm/aaaa.");
                textBox59.Text = "";

            }
        }

        private void textBox60_Leave(object sender, EventArgs e)
        {
            DateTime result;
            if (string.IsNullOrEmpty(textBox60.Text))
            {
                textBox60.Text = "";

            }
            else
            if (!DateTime.TryParseExact(textBox60.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                MessageBox.Show("Vous devez entrer une date valide au format jj/mm/aaaa.");
                textBox60.Text = "";

            }
        }

        private void textBox61_Leave(object sender, EventArgs e)
        {
            DateTime result;
            if (string.IsNullOrEmpty(textBox61.Text))
            {
                textBox61.Text = "";

            }
            else
            if (!DateTime.TryParseExact(textBox61.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                MessageBox.Show("Vous devez entrer une date valide au format jj/mm/aaaa.");
                textBox61.Text = "";

            }

        }

        private void textBox62_Leave(object sender, EventArgs e)
        {
            DateTime result;
            if (string.IsNullOrEmpty(textBox62.Text))
            {
                textBox62.Text = "";

            }
            else
            if (!DateTime.TryParseExact(textBox62.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                MessageBox.Show("Vous devez entrer une date valide au format jj/mm/aaaa.");
                textBox62.Text = "";

            }
        }

        private void textBox64_Leave(object sender, EventArgs e)
        {
            DateTime result;
            if (string.IsNullOrEmpty(textBox64.Text))
            {
                textBox64.Text = "";

            }
            else
            if (!DateTime.TryParseExact(textBox64.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                MessageBox.Show("Vous devez entrer une date valide au format jj/mm/aaaa.");
                textBox64.Text = "";

            }
        }

        private void textBox65_Leave(object sender, EventArgs e)
        {
            DateTime result;
            if (string.IsNullOrEmpty(textBox65.Text))
            {
                textBox65.Text = "";

            }
            else
            if (!DateTime.TryParseExact(textBox65.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                MessageBox.Show("Vous devez entrer une date valide au format jj/mm/aaaa.");
                textBox65.Text = "";

            }
            if (DateTime.TryParseExact(textBox65.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                CalculateTextBox66();
            }
        }

        private void textBox66_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox66.Text))
            {
                textBox66.Text = "";

            }
            else
            if (!float.TryParse(textBox66.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox66.Text = "";

            }

        }

        private void textBox67_Leave(object sender, EventArgs e)
        {
            DateTime result;
            if (string.IsNullOrEmpty(textBox67.Text))
            {
                textBox67.Text = "";

            }
            else
            if (!DateTime.TryParseExact(textBox67.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                MessageBox.Show("Vous devez entrer une date valide au format jj/mm/aaaa.");
                textBox67.Text = "";

            }
            if (DateTime.TryParseExact(textBox67.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                CalculateTextBox68();
            }
        }
        private void textBox68_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox68.Text))
            {
                textBox68.Text = "";

            }
            else
            if (!float.TryParse(textBox68.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox68.Text = "";

            }

        }

        private void textBox70_Leave(object sender, EventArgs e)
        {

            float result;
            if (string.IsNullOrEmpty(textBox70.Text))
            {
                textBox70.Text = "";

            }
            else
            if (!float.TryParse(textBox70.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox70.Text = "";

            }
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox10.Text))
            {
                // textBox10 n'est pas vide, exécutez la requête SQL pour récupérer les données
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Définir la requête SQL pour récupérer les informations de la table
                string query = "SELECT * FROM PF WHERE DOCUMENT = @DOCUMENT";

                // Créer un objet de commande pour exécuter la requête SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Ajouter le paramètre ID à la commande
                    command.Parameters.Add("@DOCUMENT", SqlDbType.NVarChar).Value = textBox10.Text.ToString();

                    // Exécuter la commande et récupérer les données
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Vérifier si des données ont été trouvées pour l'ID spécifié
                        if (reader.HasRows)
                        {
                            // Lire les données et les afficher dans les champs de texte
                            while (reader.Read())
                            {
                                textBox11.Text = reader["CLIENT"].ToString();
                                textBox12.Text = reader["REF"].ToString();
                                textBox13.Text = reader["DESIGNTION"].ToString();
                                textBox14.Text = reader["FAMILLE"].ToString();
                                textBox29.Text = reader["GRAMMAGE"].ToString();
                                textBox30.Text = reader["LT"].ToString();
                                textBox31.Text = reader["Nb_Poses"].ToString();
                                // etc. pour les autres colonnes de la table
                            }
                        }
                        else
                        {
                            // Réinitialiser les champs de texte
                            textBox10.Text = "";
                            // etc. pour les autres champs de texte
                            MessageBox.Show("Aucune donnée trouvée pour N° de doc spécifié dans TABLE DOC PF.");
                        }
                    }
                }


                // Fermer la connexion à la base de données

                // Définir la requête SQL pour récupérer les informations de la table
                string query1 = "SELECT * FROM PrixPF WHERE N_de_doc = @N_de_doc";

                // Créer un objet de commande pour exécuter la requête SQL
                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    // Ajouter le paramètre ID à la commande
                    command.Parameters.Add("@N_de_doc", SqlDbType.NVarChar).Value = textBox10.Text.ToString();

                    // Exécuter la commande et récupérer les données
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Vérifier si des données ont été trouvées pour l'ID spécifié
                        if (reader.HasRows)
                        {
                            // Lire les données et les afficher dans les champs de texte
                            while (reader.Read())
                            {
                                textBox46.Text = reader["Prix_Facturation"].ToString();

                                // etc. pour les autres colonnes de la table
                            }
                        }
                        else
                        {
                            // Réinitialiser les champs de texte
                            textBox10.Text = "";
                            // etc. pour les autres champs de texte
                            MessageBox.Show("Aucune donnée trouvée pour N° de doc spécifié dans TABLE PRIX PF   .");
                        }
                    }
                }
            }
            // Fermer la connexion à la base de données

            else if (string.IsNullOrEmpty(textBox10.Text))
            {
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
                textBox14.Text = "";
                textBox29.Text = "";
                textBox30.Text = "";
                textBox31.Text = "";
                textBox46.Text = "";
            }
            
        }

        private void textBox15_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox15.Text))
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Définir la requête SQL pour récupérer les informations de la table
                string query = "SELECT * FROM Thèmes WHERE Défaut = @Défaut";

                // Créer un objet de commande pour exécuter la requête SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Ajouter le paramètre ID à la commande
                    command.Parameters.Add("@Défaut", SqlDbType.NVarChar).Value = textBox15.Text.ToString();

                    // Exécuter la commande et récupérer les données
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Vérifier si des données ont été trouvées pour l'ID spécifié
                        if (reader.HasRows)
                        {
                            // Lire les données et les afficher dans les champs de texte
                            while (reader.Read())
                            {
                                textBox17.Text = reader["Thème"].ToString();

                                // etc. pour les autres colonnes de la table
                            }
                        }
                        else
                        {
                            textBox15.Text = "";
                            // Réinitialiser les champs de texte
                            // etc. pour les autres champs de texte
                            MessageBox.Show("Aucune donnée trouvée pour Défaut spécifié  .");

                        }


                    }
                    connection.Close();
                }
            }
            else if (string.IsNullOrEmpty(textBox15.Text))
            {
                textBox17.Text = "";

                
            }
            
        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            // Calculer la somme
            float sum = 0;
            sum += float.TryParse(textBox35.Text, out float num1) ? num1 : 0;
            sum += float.TryParse(textBox36.Text, out float num2) ? num2 : 0;
            sum += float.TryParse(textBox37.Text, out float num3) ? num3 : 0;
            sum += float.TryParse(textBox38.Text, out float num4) ? num4 : 0;

            // Afficher le résultat
            textBox39.Text = sum.ToString();
        }
        private void CalculateTextBox40()
        {
            float value39 = 0, value29 = 0, value30 = 0, value31 = 0, value32 = 0;

            // Convertir les valeurs des textboxes en nombres à virgule flottante
            float.TryParse(textBox39.Text, out value39);
            float.TryParse(textBox29.Text, out value29);
            float.TryParse(textBox30.Text, out value30);
            float.TryParse(textBox31.Text, out value31);
            float.TryParse(textBox32.Text, out value32);

            // Vérifier si la valeur de TextBox32 est différente de zéro avant de faire la division
            if (value32 != 0)
            {
                // Calculer la valeur de Textbox40
                float result = (value39 / ((value29 / 1000) * (value30 / 1000))) * (value31 / value32);

                // Afficher la valeur de Textbox40
                textBox40.Text = result.ToString();
            }
            else
            {
                // La valeur de TextBox32 est zéro, définir la valeur de TextBox40 sur zéro (0)
                textBox40.Text = "0";
            }
        }
        private void textBox29_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox40();
            CalculateTextBox44();
        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox40();
            CalculateTextBox44();
        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox40();
            CalculateTextBox44();
        }

        private void textBox32_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox40();
            CalculateTextBox44();
        }

        private void textBox39_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox40();
            CalculateTextBox49();
        }
        private void CalculateTextBox43()
        {
            float value38 = 0, value42 = 0, value41 = 0;

            // Convertir les valeurs des textboxes en nombres à virgule flottante
            float.TryParse(textBox38.Text, out value38);
            float.TryParse(textBox42.Text, out value42);
            float.TryParse(textBox41.Text, out value41);

            // Calculer la valeur de Textbox40
            float result = value38 + value42 + value41;

            // Afficher la valeur de Textbox40
            textBox43.Text = result.ToString();
        }
        private void textBox38_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox43();
            CalculateTextBox45();
        }

        private void textBox42_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox43();
        }

        private void textBox41_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox43();
        }
        private void CalculateTextBox44()
        {
            float value43 = 0, value29 = 0, value30 = 0, value31 = 0, value32 = 0;

            // Convertir les valeurs des textboxes en nombres à virgule flottante
            float.TryParse(textBox43.Text, out value43);
            float.TryParse(textBox29.Text, out value29);
            float.TryParse(textBox30.Text, out value30);
            float.TryParse(textBox31.Text, out value31);
            float.TryParse(textBox32.Text, out value32);

            // Vérifier si la valeur de TextBox32 est différente de zéro avant de faire la division
            if (value32 != 0)
            {
                // Calculer la valeur de Textbox40
                float result = (value43 / ((value29 / 1000) * (value30 / 1000))) * (value31 / value32);

                // Afficher la valeur de Textbox40
                textBox44.Text = result.ToString();
            }
            else
            {
                // La valeur de TextBox32 est zéro, définir la valeur de TextBox40 sur zéro (0)
                textBox44.Text = "0";
            }
        }

        private void textBox43_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox44();
            CalculateTextBox45();
            CalculateTextBox47();
        }
        private void CalculateTextBox45()
        {
            float value35 = 0, value36 = 0, value37 = 0, value38 = 0, value43 = 0;

            // Convertir les valeurs des textboxes en nombres à virgule flottante
            float.TryParse(textBox35.Text, out value35);
            float.TryParse(textBox36.Text, out value36);
            float.TryParse(textBox37.Text, out value37);
            float.TryParse(textBox38.Text, out value38);
            float.TryParse(textBox43.Text, out value43);
            // Calculer la valeur de Textbox40
            float result = value35 + value36 + value37 + value38 - value43;

            // Afficher la valeur de Textbox40
            textBox45.Text = result.ToString();
        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox45();
        }

        private void textBox36_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox45();
            CalculateTextBox48();
        }

        private void textBox37_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox45();
        }
        private void CalculateTextBox47()
        {
            float value43 = 0, value46 = 0;

            // Convertir les valeurs des textboxes en nombres à virgule flottante
            float.TryParse(textBox43.Text, out value43);
            float.TryParse(textBox46.Text, out value46);
            // Calculer la valeur de Textbox40
            float result = value43 * value46;

            // Afficher la valeur de Textbox40
            textBox47.Text = result.ToString();
        }

        private void textBox46_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox47();
            CalculateTextBox48();
            CalculateTextBox49();
        }
        private void CalculateTextBox48()
        {
            float value36 = 0, value46 = 0;

            // Convertir les valeurs des textboxes en nombres à virgule flottante
            float.TryParse(textBox36.Text, out value36);
            float.TryParse(textBox46.Text, out value46);
            // Calculer la valeur de Textbox40
            float result = value36 * value46;

            // Afficher la valeur de Textbox40
            textBox48.Text = result.ToString();
        }
        private void CalculateTextBox49()
        {
            float value39 = 0, value46 = 0;

            // Convertir les valeurs des textboxes en nombres à virgule flottante
            float.TryParse(textBox36.Text, out value39);
            float.TryParse(textBox46.Text, out value46);
            // Calculer la valeur de Textbox40
            float result = value39 * value46;

            // Afficher la valeur de Textbox40
            textBox49.Text = result.ToString();
        }
        private void CalculateTextBox66()
        {
            DateTime dateReclamation = DateTime.ParseExact(textBox2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dateReponse = DateTime.ParseExact(textBox65.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            TimeSpan delaiReponse = dateReponse.Subtract(dateReclamation);

            int jours = delaiReponse.Days;
            if (jours > 0)
            {
                textBox66.Text = jours.ToString();
            }
            else
            {
                // Afficher un message d'erreur si les valeurs entrées ne sont pas des dates valides
                MessageBox.Show("Les dates entrées ne sont pas valides.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox66.Text = "";
            }
        }
        private void CalculateTextBox68()
        {
            DateTime dateReclamation = DateTime.ParseExact(textBox2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dateReponse = DateTime.ParseExact(textBox67.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            TimeSpan delaiReponse = dateReponse.Subtract(dateReclamation);

            int jours = delaiReponse.Days;
            if (jours > 0)
            {
                textBox68.Text = jours.ToString();
            }
            else
            {
                // Afficher un message d'erreur si les valeurs entrées ne sont pas des dates valides
                MessageBox.Show("Les dates entrées ne sont pas valides.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox67.Text = "";
            }
        }

        private void ERCAMS_Load(object sender, EventArgs e)
        {
            //Ouvrir une connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //Définir la commande SQL pour récupérer les données de la table PF
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[PrixPF]", connection);

                //Exécuter la commande SQL et récupérer les résultats dans un objet SqlDataReader
                SqlDataReader dr = cmd.ExecuteReader();

                //Créer des collections d'autocomplétion pour chaque TextBox
                AutoCompleteStringCollection collection1 = new AutoCompleteStringCollection();


                //Parcourir chaque ligne de résultats et ajouter les valeurs appropriées aux collections d'autocomplétion
                while (dr.Read())
                {
                    collection1.Add(dr["N_de_doc"].ToString());


                }
                //Lier chaque collection d'autocomplétion à son TextBox correspondant
                textBox10.AutoCompleteCustomSource = collection1;
                //Fermer le DataReader et la connexion
                dr.Close();

            }
            //Ouvrir une connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //Définir la commande SQL pour récupérer les données de la table PF
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Thèmes]", connection);

                //Exécuter la commande SQL et récupérer les résultats dans un objet SqlDataReader
                SqlDataReader dr = cmd.ExecuteReader();

                //Créer des collections d'autocomplétion pour chaque TextBox
                AutoCompleteStringCollection collection2 = new AutoCompleteStringCollection();


                //Parcourir chaque ligne de résultats et ajouter les valeurs appropriées aux collections d'autocomplétion
                while (dr.Read())
                {
                    collection2.Add(dr["Défaut"].ToString());


                }
                //Lier chaque collection d'autocomplétion à son TextBox correspondant
                textBox15.AutoCompleteCustomSource = collection2;
                //Fermer le DataReader et la connexion
                dr.Close();

            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //Définir la commande SQL pour récupérer les données de la table PF
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[N__des_conds_et_des_aides_Conds]", connection);

                //Exécuter la commande SQL et récupérer les résultats dans un objet SqlDataReader
                SqlDataReader dr = cmd.ExecuteReader();

                //Créer des collections d'autocomplétion pour chaque TextBox
                AutoCompleteStringCollection collection3 = new AutoCompleteStringCollection();


                //Parcourir chaque ligne de résultats et ajouter les valeurs appropriées aux collections d'autocomplétion
                while (dr.Read())
                {
                    collection3.Add(dr["Matricule"].ToString());


                }
                //Lier chaque collection d'autocomplétion à son TextBox correspondant
                textBox20.AutoCompleteCustomSource = collection3;
                textBox22.AutoCompleteCustomSource = collection3;
                //Fermer le DataReader et la connexion
                dr.Close();
            }

        }    

        public int SelectedId { get; set; }
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using (SqlConnection connection1 = new SqlConnection(connectionString))
            {
                SqlCommand command1 = new SqlCommand("SELECT TOP 1 * FROM NCE", connection);

                SqlDataReader reader = command1.ExecuteReader();

                List<string> columnNames = new List<string>();
                int i = 0;
                while (i < reader.FieldCount)
                {
                    string columnName = reader.GetName(i);
                    columnNames.Add(columnName);
                    i++;
                }
                reader.Close();

                // Création de la requête SQL avec des paramètres
                string query = "UPDATE NCE SET ";

                for (int j = 1; j <= 70; j++)
                {
                    TextBox textBox = this.Controls.Find("textBox" + j, true).FirstOrDefault() as TextBox;
                    if (textBox != null)
                    {
                        string colName = columnNames[j];
                        query += colName + " = @param" + j;
                        if (j < 70 && textBox != null)
                        {
                            query += ", ";

                        }
                    }
                }


                query += " WHERE id_NCE = @SelectedId";


                // Création de la commande SQL avec les paramètres
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SelectedId", SelectedId); // Remplacez "id" par l'ID de l'enregistrement à mettre à jour
                for (int k = 1; k <= 70; k++)
                {
                    TextBox textBox = this.Controls.Find("textBox" + k, true).FirstOrDefault() as TextBox;
                    if (textBox != null)
                    {
                        float value;
                        if (float.TryParse(textBox.Text, out value))
                        {
                            command.Parameters.AddWithValue("@param" + k, value);
                        }
                        else if (DateTime.TryParse(textBox.Text, out DateTime dateValue))
                        {
                            command.Parameters.AddWithValue("@param" + k, dateValue);
                        }
                        else if (string.IsNullOrEmpty(textBox.Text))
                        {
                            command.Parameters.AddWithValue("@param" + k, DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@param" + k, textBox.Text);
                        }
                    }
                }

                // Exécution de la commande SQL
                command.ExecuteNonQuery();

                // Fermeture de la connexion
                connection.Close();
                Erc.allData.Clear();
                Erc.LoadData();
                this.Close();

            }
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button1, "Ajouter");
            this.Cursor = Cursors.Hand;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button2, "Modifier");
            this.Cursor = Cursors.Hand;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
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

        private void button4_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
    }
}


