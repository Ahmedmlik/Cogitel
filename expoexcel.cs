﻿using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cogitel_QT
{
    public partial class expoexcel : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;
       
        public expoexcel()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Fichiers Excel (*.xlsx)|*.xlsx|Tous les fichiers (*.*)|*.*";
            saveFileDialog.Title = "Enregistrer le fichier Excel";

            // Afficher la boîte de dialogue et récupérer le résultat
            DialogResult saveResult = saveFileDialog.ShowDialog();

            // Vérifier si l'utilisateur a sélectionné un emplacement pour enregistrer le fichier
            if (saveResult == DialogResult.OK)
            {
                // Récupérer le chemin complet du fichier sélectionné
                string filePath = saveFileDialog.FileName;
                string selectedValue1 = comboBox1.SelectedValue?.ToString();
                string selectedValue2 = comboBox2.SelectedValue?.ToString();
                // Connexion à la base de données
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connexion établie avec succès.");
                    string sqlQuery = "SELECT * FROM NCE WHERE 1=1 ";

                    // Ajout des conditions de filtrage
                    if (selectedValue1 != "TOUT" && selectedValue2 == "TOUT")
                    {
                        // Filtre par année
                        if (int.TryParse(selectedValue1, out int year))
                        {
                            sqlQuery += $"AND YEAR(Date_de_réclamtion) = {year} ";
                        }
                    }
                    else if (selectedValue1 == "TOUT" && selectedValue2 != "TOUT")
                    {
                        
                        // Filtre par client
                        sqlQuery += $"AND Client = '{selectedValue2}' ";
                    }
                    else if (selectedValue1 != "TOUT" && selectedValue2 != "TOUT")
                    {
                        
                        // Filtre par année et client
                        if (int.TryParse(selectedValue1, out int year))
                        {
                            sqlQuery += $"AND YEAR(Date_de_réclamtion) = {year} ";
                        }
                        sqlQuery += $"AND Client = '{selectedValue2}' ";
                    }

                    sqlQuery += "ORDER BY id_NCE ASC";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(table);


                        // Création du document Excel avec ClosedXML
                        var workbook = new XLWorkbook();
                        var worksheet = workbook.Worksheets.Add("Sheet1");

                        // Ajout des en-têtes
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = table.Columns[i].ColumnName;
                        }

                        // Ajout des données
                        for (int row = 0; row < table.Rows.Count; row++)
                        {
                            for (int col = 0; col < table.Columns.Count; col++)
                            {
                                // Check if the value can be parsed as a number
                                if (double.TryParse(table.Rows[row][col].ToString(), out double numValue))
                                {
                                    // Store the value as a number
                                    worksheet.Cell(row + 2, col + 1).SetValue(numValue);
                                }
                                else
                                {
                                    string value = table.Rows[row][col].ToString().Replace(Environment.NewLine, " ");

                                    // Write the value to the cell
                                    worksheet.Cell(row + 2, col + 1).Value = value;
                                }
                            }
                        }

                        // Enregistrement du document Excel
                        workbook.SaveAs(filePath);
                        MessageBox.Show("Le fichier Excel a été enregistré avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }


                }
            }
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Configurer la boîte de dialogue
            openFileDialog.Filter = "Fichiers Excel (*.xlsx)|*.xlsx|Tous les fichiers (*.*)|*.*";
            openFileDialog.Title = "Sélectionner un fichier Excel existant";

            // Afficher la boîte de dialogue et récupérer le résultat
            DialogResult openResult = openFileDialog.ShowDialog();

            // Vérifier si l'utilisateur a sélectionné un fichier
            if (openResult == DialogResult.OK)
            {
                // Récupérer le chemin complet du fichier sélectionné
                string filePath = openFileDialog.FileName;

                try
                {
                    // Ouvrir le fichier Excel existant avec ClosedXML
                    var workbook = new XLWorkbook(filePath);
                    var worksheet = workbook.Worksheet("Sheet1");

                    string selectedValue1 = comboBox1.SelectedValue?.ToString();
                    string selectedValue2 = comboBox2.SelectedValue?.ToString();
                    // Connexion à la base de données
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        Console.WriteLine("Connexion établie avec succès.");

                        // Récupération des données de la table
                        string sqlQuery = "SELECT * FROM NCE WHERE 1=1 ";

                        // Ajout des conditions de filtrage
                        if (selectedValue1 != "TOUT" && selectedValue2 == "TOUT")
                        {
                            // Filtre par année
                            if (int.TryParse(selectedValue1, out int year))
                            {
                                sqlQuery += $"AND YEAR(Date_de_réclamtion) = {year} ";
                            }
                        }
                        else if (selectedValue1 == "TOUT" && selectedValue2 != "TOUT")
                        {

                            // Filtre par client
                            sqlQuery += $"AND Client = '{selectedValue2}' ";
                        }
                        else if (selectedValue1 != "TOUT" && selectedValue2 != "TOUT")
                        {

                            // Filtre par année et client
                            if (int.TryParse(selectedValue1, out int year))
                            {
                                sqlQuery += $"AND YEAR(Date_de_réclamtion) = {year} ";
                            }
                            sqlQuery += $"AND Client = '{selectedValue2}' ";
                        }

                        sqlQuery += "ORDER BY id_NCE ASC";
                        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            // Trouver le dernier numéro de ligne utilisé dans la feuille de calcul
                            int lastUsedRow = worksheet.LastRowUsed().RowNumber();

                            // Créer une liste pour stocker les valeurs de la colonne "id_NCE" déjà ajoutées
                            List<int> existingIds = new List<int>();
                            for (int i = 2; i <= lastUsedRow; i++) // commencer à partir de la deuxième ligne
                            {
                                int id;
                                if (int.TryParse(worksheet.Cell(i, 1).Value.ToString(), out id))
                                {
                                    existingIds.Add(id);
                                }
                            }

                            // Ajout des données à partir de la dernière ligne utilisée
                            int newRow = lastUsedRow + 1;
                            foreach (DataRow row in table.Rows)
                            {
                                int id = int.Parse(row["id_NCE"].ToString());
                                if (!existingIds.Contains(id))
                                {
                                    // Ajouter la ligne si l'id n'est pas déjà présent
                                    for (int col = 0; col < table.Columns.Count; col++)
                                    {
                                        // Check if the value can be parsed as a number
                                        if (double.TryParse(row[col].ToString(), out double numValue))
                                        {
                                            // Store the value as a number
                                            worksheet.Cell(newRow, col + 1).SetValue(numValue);
                                        }
                                        else
                                        {
                                            // Store the value as a string
                                            worksheet.Cell(newRow, col + 1).Value = row[col].ToString();
                                        }
                                    }
                                    newRow++;
                                }
                            }

                            // Enregistrer les modifications dans le fichier Excel
                            workbook.SaveAs(filePath);

                        }
                        MessageBox.Show("Les nouvelles lignes ont été enregistrées avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (IOException)
                {
                    // Afficher un message d'erreur si le fichier est déjà ouvert dans une autre application
                    MessageBox.Show("Le fichier est déjà ouvert dans une autre application.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            this.Close();
        }

        private void expoexcel_Load(object sender, EventArgs e)
        {
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
                    comboBox1.DataSource = modifiedTableYears;
                    comboBox1.DisplayMember = "year";
                    comboBox1.ValueMember = "year";

                    // Set the default value to "TOUT"
                    comboBox1.SelectedValue = "TOUT";


                }
                string sqlQueryClient = "SELECT DISTINCT Client as Client FROM NCE";
                using (SqlCommand command = new SqlCommand(sqlQueryClient, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable tableclient = new DataTable();
                    adapter.Fill(tableclient);
                    // Add an empty row to the DataTable
                    DataRow emptyRow = tableclient.NewRow();
                    emptyRow["Client"] = "";
                    DataRow newRow = tableclient.NewRow();
                    newRow[0] = "TOUT"; // Supposant que la colonne appropriée est la première colonne (indice 0)
                    tableclient.Rows.InsertAt(newRow, 0);

                    // Bind the results to the ComboBox
                    comboBox2.DataSource = tableclient;
                    comboBox2.DisplayMember = "Client";
                    comboBox2.ValueMember = "Client";
                    comboBox2.SelectedIndex = 0; // définit la valeur par défaut à vide
                    connection.Close();

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button9_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button9, "Exporte les nouvelles lignes vers un fichier Excel");
            this.Cursor = Cursors.Hand;
        }

        private void button9_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        private void button8_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button8, "Exporte Excel");
            this.Cursor = Cursors.Hand;
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        private void button1_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
    }
}
