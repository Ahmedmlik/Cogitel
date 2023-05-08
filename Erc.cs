using System.Reflection;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using ClosedXML.Excel;
using System.IO;
using System.Collections.Generic;
using System.Configuration;






namespace Cogitel_QT
{
    public partial class Erc : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;
        SqlConnection connection;
        public Erc()
        {
            InitializeComponent();
            
            connection = new SqlConnection(connectionString);
            this.FormClosing += new FormClosingEventHandler(Erc_FormClosing);
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
        }

        private void Erc_Load(object sender, System.EventArgs e)
        {
           
            string sqlQueryYears = "SELECT DISTINCT YEAR(Date_de_réclamtion) as year FROM NCE";
            using (SqlCommand command = new SqlCommand(sqlQueryYears, connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable tableYears = new DataTable();
                adapter.Fill(tableYears);
                // Create an empty row
                DataRow emptyRow = tableYears.NewRow();

                // Set the value of the "year" column to DBNull.Value
                emptyRow["year"] = DBNull.Value;

                // Add the empty row to the DataTable
                tableYears.Rows.InsertAt(emptyRow, 0);

                // Bind the results to the ComboBox
                comboBox1.DataSource = tableYears;
                comboBox1.DisplayMember = "year";
                comboBox1.ValueMember = "year";
                comboBox1.SelectedIndex = 0;

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
                tableclient.Rows.InsertAt(emptyRow, 0);

                // Bind the results to the ComboBox
                comboBox2.DataSource = tableclient;
                comboBox2.DisplayMember = "Client";
                comboBox2.ValueMember = "Client";
                comboBox2.SelectedIndex = 0; // définit la valeur par défaut à vide
            }
            textBox14.Text = "Rechercher...";
            textBox14.ForeColor = System.Drawing.Color.Black;
            LoadData();
            // Charger les données de la source de données


            //Ouvrir une connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //Définir la commande SQL pour récupérer les données de la table PF
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[NCE]", connection);

                //Exécuter la commande SQL et récupérer les résultats dans un objet SqlDataReader
                SqlDataReader dr = cmd.ExecuteReader();

                //Créer des collections d'autocomplétion pour chaque TextBox
                AutoCompleteStringCollection collection1 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection collection4 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection collection5 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection collection7 = new AutoCompleteStringCollection();


                //Parcourir chaque ligne de résultats et ajouter les valeurs appropriées aux collections d'autocomplétion
                while (dr.Read())
                {
                    collection1.Add(dr["N_de_la_NC"].ToString());
                    collection4.Add(dr["N_de_doc"].ToString());
                    collection5.Add(dr["Client"].ToString());
                    DateTime dateValue;
                    if (DateTime.TryParse(dr["Date_de_réclamtion"].ToString(), out dateValue))
                    {
                        collection7.Add(dateValue.ToString("dd/MM/yyyy"));
                    }

                }
                //Lier chaque collection d'autocomplétion à son TextBox correspondant

                string[] combinedArray = collection1.Cast<string>().Concat(collection4.Cast<string>()).Concat(collection7.Cast<string>()).Concat(collection5.Cast<string>()).ToArray();

                textBox14.AutoCompleteCustomSource.AddRange(combinedArray);

                //Fermer le DataReader et la connexion
                dr.Close();

            }
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            this.Close();
            cogitel.Instance.ShowOrHidePanel(true);
        }
        public readonly DataTable allData = new DataTable();
        public readonly int limit = 25;
        private int offset = 0;
        public void LoadData()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Obtenir le nombre total de lignes dans la table PF
                string countQuery = "SELECT COUNT(*) FROM NCE ";
                SqlCommand countCommand = new SqlCommand(countQuery, connection);
                int rowCount = (int)countCommand.ExecuteScalar();

                // Vérifier si le nombre de lignes dans allData est inférieur ou égal au nombre total de lignes
                if (allData.Rows.Count <= rowCount)
                {
                    // Exécuter la requête de sélection
                    string query = "SELECT * FROM NCE ORDER BY id_NCE DESC  OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Offset", offset);
                        command.Parameters.AddWithValue("@Limit", limit);
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable newDataTable = new DataTable();
                        dataAdapter.Fill(newDataTable);
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                        // Parcours de toutes les colonnes pour activer l'enroulement du texte
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        }

                        allData.Merge(newDataTable); // Merge the new data with existing data
                        dataGridView1.DataSource = allData;

                        connection.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Tous les enregistrements ont été récupérés.");
                }
            }
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.SmallIncrement && e.ScrollOrientation == ScrollOrientation.VerticalScroll && e.NewValue >= dataGridView1.Rows.Count - dataGridView1.DisplayedRowCount(true))
            {
                int firstDisplayedRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
                offset += limit;
                LoadData();
                dataGridView1.FirstDisplayedScrollingRowIndex = firstDisplayedRowIndex;
            }
        }
        private ERCAMS formInstance = null;
        private void button2_Click(object sender, EventArgs e)
        {
            if (formInstance == null)
            {
                formInstance = new ERCAMS(this);
                formInstance.FormClosed += (s, args) => formInstance = null;
            }
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_NCE"].Value);
                formInstance.SelectedId = selectedId;
                cogitel mainForm = (cogitel)this.MdiParent;

                // Create a new instance of the form to open

                // Set the MdiParent property to the main form
                formInstance.MdiParent = mainForm;
                formInstance.Dock = DockStyle.Fill;
                formInstance.BringToFront();
                formInstance.WindowState = FormWindowState.Normal;
                for (int i = 1; i <= 70; i++)
                {
                    System.Windows.Forms.Control ctrl = formInstance.Controls.Find("textBox" + i.ToString(), true).FirstOrDefault();
                    if (ctrl is TextBox)
                    {
                        ((TextBox)ctrl).Multiline = true;
                        ((TextBox)ctrl).Size = new System.Drawing.Size(214, 100);
                    }
                }
                formInstance.Show();
                // Show the new form


                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Vérifier si le formulaire ERCAMS a été créé et initialisé
                if (formInstance != null && formInstance.IsHandleCreated)
                {
                    // Récupérer les valeurs de chaque cellule de la ligne sélectionnée et les affecter aux textboxes dans Form2
                    for (int i = 1; i < 71; i++)
                    {
                        System.Windows.Forms.Control ctrl = formInstance.Controls.Find("textBox" + i, true).FirstOrDefault();
                        if (ctrl is TextBox)
                        {
                            ((TextBox)ctrl).Text = selectedRow.Cells[i].Value.ToString().Replace("\n", Environment.NewLine);

                            if (ctrl is TextBox)
                            {
                                if (DateTime.TryParse(ctrl.Text, out DateTime date))
                                {
                                    ctrl.Text = date.ToString("dd/MM/yyyy");
                                }
                            }


                        }
                    }
                }



            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Récupérer l'identifiant unique de la ligne sélectionnée
                int idToDelete = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_NCE"].Value); // Remplacer "Id" par le nom de votre colonne contenant l'identifiant unique

                // Demander une confirmation avant de supprimer
                DialogResult dialogResult = MessageBox.Show("Voulez-vous vraiment supprimer cette ligne ?", "Confirmation de suppression", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Supprimer la ligne correspondante de la base de données

                    string query = "DELETE FROM NCE WHERE id_NCE = @id"; // Remplacer "MaTable" par le nom de votre table et "Id" par le nom de votre colonne d'identifiant unique
                    using (SqlConnection Connexion = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query, Connexion))
                        {
                            command.Parameters.AddWithValue("@id", idToDelete);
                            Connexion.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("La ligne a été supprimée avec succès !");
                            }
                            else
                            {
                                Console.WriteLine("La ligne n'a pas été trouvée.");
                            }
                        }
                    }

                    // Supprimer également la ligne correspondante du DataGridView
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner au moins une ligne.");
            }
        }
        private ERCAMS formInstance1 = null;
        private void button1_Click(object sender, EventArgs e)
        {
            if (formInstance1 == null)
            {
                formInstance1 = new ERCAMS(this);
                formInstance1.FormClosed += (s, args) => formInstance1 = null;
            }
            cogitel mainForm = (cogitel)this.MdiParent;

            // Create a new instance of the form to open
            formInstance1.SetButtonVisible(false);
            // Set the MdiParent property to the main form
            formInstance1.MdiParent = mainForm;
            formInstance1.Dock = DockStyle.Fill;
            formInstance1.BringToFront();
            formInstance1.WindowState = FormWindowState.Normal;
            formInstance1.Show();

        }

        private void Erc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Application.OpenForms.OfType<ERCAMS>().Any())
            {
                // Fermer la Form ERCAMS
                ERCAMS ercams = Application.OpenForms.OfType<ERCAMS>().FirstOrDefault();
                ercams.Close();
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox14.Text = "Rechercher..."; // réinitialise le contenu du texte de recherche
            textBox14.ForeColor = System.Drawing.Color.Black;
            dataGridView1.DataSource = allData;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            string searchTerm = textBox14.Text;
            if (DateTime.TryParse(searchTerm, out DateTime searchDate))
            {
                // Format the search date as a string in a specific format that can be used in the SQL query
                searchTerm = searchDate.ToString("dd/MM/yyyy");
                string sql = "SELECT * FROM NCE WHERE Date_de_réclamtion  = '" + searchTerm + "'";
                using (var adapter = new SqlDataAdapter(sql, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                connection.Close();
            }
            else
            {
                string sql = "SELECT * FROM NCE WHERE N_de_la_NC = '" + searchTerm + "' OR N_de_doc  = '" + searchTerm + "'OR Client  = '" + searchTerm + "'";
                using (var adapter = new SqlDataAdapter(sql, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }

                connection.Close();
            }
        }

        private void textBox14_Enter(object sender, EventArgs e)
        {
            if (textBox14.Text == "Rechercher...")
            {
                textBox14.Text = "";
                textBox14.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void textBox14_Leave(object sender, EventArgs e)
        {
            if (textBox14.Text == "")
            {
                textBox14.Text = "Rechercher...";
                textBox14.ForeColor = System.Drawing.Color.Black;
            }
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
                    if (!string.IsNullOrEmpty(selectedValue1) && string.IsNullOrEmpty(selectedValue2))
                    {
                        // Filtre par année
                        if (int.TryParse(selectedValue1, out int year))
                        {
                            sqlQuery += $"AND YEAR(Date_de_réclamtion) = {year} ";
                        }
                    }
                    else if (string.IsNullOrEmpty(selectedValue1) && !string.IsNullOrEmpty(selectedValue2))
                    {
                        // Filtre par client
                        sqlQuery += $"AND Client = '{selectedValue2}' ";
                    }
                    else if (!string.IsNullOrEmpty(selectedValue1) && !string.IsNullOrEmpty(selectedValue2))
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
                        var worksheet = workbook.Worksheets.Add("NCE");

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
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button1, "Ajouter");
            this.Cursor = Cursors.Hand;
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button5, "Rechercher");
            this.Cursor = Cursors.Hand;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button7_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button7, "Annuler Rechercher");
            this.Cursor = Cursors.Hand;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
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

        private void button3_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button3, "Supprimer");
            this.Cursor = Cursors.Hand;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
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
                    var worksheet = workbook.Worksheet("NCE");


                    // Connexion à la base de données
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        Console.WriteLine("Connexion établie avec succès.");

                        // Récupération des données de la table
                        string sqlQuery = "SELECT * FROM NCE ORDER BY id_NCE ASC";
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

        private void Erc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // Simule un clic sur le bouton "Supprimer"
                button3.PerformClick();
            }
        }
        private ERCAMS formInstance2 = null;
        private void button10_Click(object sender, EventArgs e)
        {
            if (formInstance2 == null)
            {
                formInstance2 = new ERCAMS(this);
                formInstance2.FormClosed += (s, args) => formInstance2 = null;
            }
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_NCE"].Value);
                formInstance2.SelectedId = selectedId;
                cogitel mainForm = (cogitel)this.MdiParent;

                // Create a new instance of the form to open

                // Set the MdiParent property to the main form
                formInstance2.SetButtonVisible1(false);
                formInstance2.MdiParent = mainForm;
                formInstance2.Dock = DockStyle.Fill;
                formInstance2.BringToFront();
                formInstance2.WindowState = FormWindowState.Normal;
                for (int i = 1; i <= 70; i++)
                {
                    System.Windows.Forms.Control ctrl = formInstance2.Controls.Find("textBox" + i.ToString(), true).FirstOrDefault();
                    if (ctrl is TextBox)
                    {
                        ((TextBox)ctrl).Multiline = true;
                        ((TextBox)ctrl).Size = new System.Drawing.Size(214, 100);
                        ((TextBox)ctrl).ReadOnly = true;

                    }
                }
                formInstance2.Show();
                // Show the new form


                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Vérifier si le formulaire ERCAMS a été créé et initialisé
                if (formInstance2 != null && formInstance2.IsHandleCreated)
                {
                    // Récupérer les valeurs de chaque cellule de la ligne sélectionnée et les affecter aux textboxes dans Form2
                    for (int i = 1; i < 71; i++)
                    {
                        System.Windows.Forms.Control ctrl = formInstance2.Controls.Find("textBox" + i, true).FirstOrDefault();
                        if (ctrl is TextBox)
                        {
                            ((TextBox)ctrl).Text = selectedRow.Cells[i].Value.ToString().Replace("\n", Environment.NewLine);

                            if (ctrl is TextBox)
                            {
                                if (DateTime.TryParse(ctrl.Text, out DateTime date))
                                {
                                    ctrl.Text = date.ToString("dd/MM/yyyy");
                                }
                            }


                        }
                    }
                }



            }
        }

        private void button10_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button10, "Consulter");
            this.Cursor = Cursors.Hand;
        }

        private void button10_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
    }
}
