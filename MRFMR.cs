using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Cogitel_QT
{
    public partial class MRFMR : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;
        SqlConnection connection;
        private RFMR RFMR;
        public MRFMR(RFMR RFMR)
        {
            this.RFMR = RFMR;
            InitializeComponent();
            connection = new SqlConnection(connectionString);
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
        public int SelectedId { get; set; }
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

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            // Création de la requête SQL avec des paramètres
            string query = "INSERT INTO ME VALUES (";
            for (int i = 1; i <= 46; i++)
            {
                TextBox textBox = this.Controls.Find("textBox" + i, true).FirstOrDefault() as TextBox;
                if (textBox != null)
                {
                    query += "@param" + i;
                    if (i < 46)
                    {
                        query += ", ";
                    }
                }
            }
            query += ")";

            // Création de la commande SQL avec les paramètres
            SqlCommand command = new SqlCommand(query, connection);
            for (int i = 1; i <= 46; i++)
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
            RFMR.allData.Clear();
            RFMR.LoadData();
            this.Close();
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox6.Text))
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
                    command.Parameters.Add("@DOCUMENT", SqlDbType.NVarChar).Value = textBox6.Text.ToString();

                    // Exécuter la commande et récupérer les données
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Vérifier si des données ont été trouvées pour l'ID spécifié
                        if (reader.HasRows)
                        {
                            // Lire les données et les afficher dans les champs de texte
                            while (reader.Read())
                            {
                                textBox7.Text = reader["CLIENT"].ToString();
                                textBox8.Text = reader["REF"].ToString();
                                textBox9.Text = reader["DESIGNTION"].ToString();
                                textBox10.Text = reader["FAMILLE"].ToString();
                                // etc. pour les autres colonnes de la table
                            }
                        }
                        else
                        {
                            // Réinitialiser les champs de texte
                            textBox6.Text = "";
                            // etc. pour les autres champs de texte
                            MessageBox.Show("Aucune donnée trouvée pour N° de doc spécifié dans TABLE DOC PF.");
                        }
                    }
                }
                string query1 = "SELECT * FROM PrixPF WHERE N_de_doc = @N_de_doc";
                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    // Ajouter le paramètre ID à la commande
                    command.Parameters.Add("@N_de_doc", SqlDbType.NVarChar).Value = textBox6.Text.ToString();

                    // Exécuter la commande et récupérer les données
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Vérifier si des données ont été trouvées pour l'ID spécifié
                        if (reader.HasRows)
                        {
                            // Lire les données et les afficher dans les champs de texte
                            while (reader.Read())
                            {
                                textBox30.Text = reader["Prix_Facturation"].ToString();

                                // etc. pour les autres colonnes de la table
                            }
                        }
                        else
                        {
                            // Réinitialiser les champs de texte
                            textBox6.Text = "";
                            // etc. pour les autres champs de texte
                            MessageBox.Show("Aucune donnée trouvée pour N° de doc spécifié dans TABLE PRIX PF   .");
                        }
                    }
                }
            }
            else if (string.IsNullOrEmpty(textBox6.Text))
            {
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox30.Text = "";
            }
        }

        private void textBox13_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox13.Text))
            {
                // textBox10 n'est pas vide, exécutez la requête SQL pour récupérer les données
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Définir la requête SQL pour récupérer les informations de la table
                string query = "SELECT * FROM prix_lot WHERE ref = @ref";

                // Créer un objet de commande pour exécuter la requête SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Ajouter le paramètre ID à la commande
                    command.Parameters.Add("@ref", SqlDbType.NVarChar).Value = textBox13.Text.ToString();

                    // Exécuter la commande et récupérer les données
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Vérifier si des données ont été trouvées pour l'ID spécifié
                        if (reader.HasRows)
                        {
                            // Lire les données et les afficher dans les champs de texte
                            while (reader.Read())
                            {
                                textBox14.Text = reader["Désignation"].ToString();
                                // etc. pour les autres colonnes de la table
                            }
                        }
                        else
                        {
                            // Réinitialiser les champs de texte
                            textBox13.Text = "";
                            // etc. pour les autres champs de texte
                            MessageBox.Show("Aucune donnée trouvée pour N° de doc spécifié dans TABLE prix lot.");
                        }
                    }
                }
            }
            else if (string.IsNullOrEmpty(textBox13.Text))
            {
                textBox14.Text = "";
              
            }
        }

        private void textBox18_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox13.Text) && !string.IsNullOrEmpty(textBox18.Text))
            {
                // textBox13 n'est pas vide, exécutez la requête SQL pour récupérer les données
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Définir la requête SQL pour récupérer les informations de la table
                string query = "SELECT * FROM prix_lot WHERE ref = @ref AND Nlot= @entreeMP";

                // Créer un objet de commande pour exécuter la requête SQL
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Ajouter les paramètres ref et Entrée MP à la commande
                    command.Parameters.Add("@ref", SqlDbType.NVarChar).Value = textBox13.Text.ToString();
                    command.Parameters.Add("@entreeMP", SqlDbType.NVarChar).Value = textBox18.Text.ToString();

                    // Exécuter la commande et récupérer les données
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Vérifier si des données ont été trouvées pour la référence et l'entrée MP spécifiées
                        if (reader.HasRows)
                        {
                            // Lire les données et les afficher dans les champs de texte
                            while (reader.Read())
                            {
                                textBox29.Text = reader["prix_lot"].ToString();
                                // etc. pour les autres colonnes de la table
                            }
                        }
                        else
                        {
                            // Réinitialiser les champs de texte
                            textBox18.Text = "";
                            // etc. pour les autres champs de texte
                            MessageBox.Show("Aucune donnée trouvée pour la référence et l'entrée MP spécifiées dans la table prix_lot.");
                        }
                    }
                }
            }
            else if (string.IsNullOrEmpty(textBox18.Text) && string.IsNullOrEmpty(textBox13.Text))
            {
                textBox29.Text = "";

            }
        }
        private void CalculateTextBox31()
        {
            float value24 = 0, value25 = 0, value27 = 0, value28 = 0, value29 = 0, value30 = 0;

            // Convertir les valeurs des textboxes en nombres à virgule flottante
            float.TryParse(textBox24.Text, out value24);
            float.TryParse(textBox25.Text, out value25);
            float.TryParse(textBox27.Text, out value27);
            float.TryParse(textBox28.Text, out value28);
            float.TryParse(textBox29.Text, out value29);
            float.TryParse(textBox30.Text, out value30);

            // Calculer la valeur de Textbox40
            float result = ((value24 + value25) * value29) + (value27 * value30) + value28;

                // Afficher la valeur de Textbox40
                textBox31.Text = result.ToString();
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox31();
            CalculateTextBox26();
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox31();
            CalculateTextBox26();
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox31();
        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox31();
        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox31();
        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox31();
        }
        private void CalculateTextBox26()
        {
            float value22 = 0, value23 = 0, value24 = 0, value25 = 0;

            // Convertir les valeurs des textboxes en nombres à virgule flottante
            float.TryParse(textBox24.Text, out value24);
            float.TryParse(textBox25.Text, out value25);
            float.TryParse(textBox23.Text, out value23);
            float.TryParse(textBox22.Text, out value22);


            // Calculer la valeur de Textbox40
            float result = value22 + value23 + value24 + value25;

            // Afficher la valeur de Textbox40
            textBox26.Text = result.ToString();
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox26();
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            CalculateTextBox26();
        
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

        private void textBox22_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox22.Text))
            {
                textBox22.Text = "";

            }
            else if (!float.TryParse(textBox22.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox22.Text = "";

            }
        }

        private void textBox23_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox23.Text))
            {
                textBox23.Text = "";

            }
            else if (!float.TryParse(textBox23.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox23.Text = "";

            }
        }

        private void textBox24_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox24.Text))
            {
                textBox24.Text = "";

            }
            else if (!float.TryParse(textBox24.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox24.Text = "";

            }
        }

        private void textBox25_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox25.Text))
            {
                textBox25.Text = "";

            }
            else if (!float.TryParse(textBox25.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox25.Text = "";

            }
        }

        private void textBox27_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox27.Text))
            {
                textBox27.Text = "";

            }
            else if (!float.TryParse(textBox27.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox27.Text = "";

            }
        }

        private void textBox32_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox32.Text))
            {
                textBox32.Text = "";

            }
            else if (!float.TryParse(textBox32.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox32.Text = "";

            }
        }

        private void textBox33_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox33.Text))
            {
                textBox33.Text = "";

            }
            else if (!float.TryParse(textBox33.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox33.Text = "";

            }
        }

        private void textBox34_Leave(object sender, EventArgs e)
        {
            DateTime result;
            if (string.IsNullOrEmpty(textBox34.Text))
            {
                textBox34.Text = "";

            }
            else

             if (!DateTime.TryParseExact(textBox34.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                MessageBox.Show("Vous devez entrer une date valide au format jj/mm/aaaa.");
                textBox34.Text = "";

            }
        }

        private void textBox36_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox36.Text))
            {
                textBox36.Text = "";

            }
            else if (!float.TryParse(textBox36.Text, out result))
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
            else if (!float.TryParse(textBox37.Text, out result))
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
            else if (!float.TryParse(textBox38.Text, out result))
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
            else if (!float.TryParse(textBox39.Text, out result))
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
            else if (!float.TryParse(textBox40.Text, out result))
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
            else if (!float.TryParse(textBox41.Text, out result))
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
            else if (!float.TryParse(textBox42.Text, out result))
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
            else if (!float.TryParse(textBox43.Text, out result))
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
            else if (!float.TryParse(textBox44.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox44.Text = "";

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using (SqlConnection connection1 = new SqlConnection(connectionString))
            {
                SqlCommand command1 = new SqlCommand("SELECT TOP 1 * FROM ME", connection);

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
                string query = "UPDATE ME SET ";

                for (int j = 1; j <= 46; j++)
                {
                    TextBox textBox = this.Controls.Find("textBox" + j, true).FirstOrDefault() as TextBox;
                    if (textBox != null)
                    {
                        string colName = columnNames[j];
                        query += colName + " = @param" + j;
                        if (j < 46 && textBox != null)
                        {
                            query += ", ";

                        }
                    }
                }


                query += " WHERE id_ME = @SelectedId";


                // Création de la commande SQL avec les paramètres
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SelectedId", SelectedId); // Remplacez "id" par l'ID de l'enregistrement à mettre à jour
                for (int k = 1; k <= 46; k++)
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
                RFMR.allData.Clear();
                RFMR.LoadData();
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
