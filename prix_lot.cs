using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Configuration;

namespace Cogitel_QT
{
    public partial class prix_lot : Form
    {
        SqlConnection connection;
        string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;
        public prix_lot()
        {
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
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type dgvType = dataGridView1.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dataGridView1, true, null);
            }
        }

        private void prix_lot_Load(object sender, EventArgs e)
        {
            timer1.Interval = 2000;
            // Démarrer le timer
            timer1.Start();
            textBox1.Text = "Saisie RÉFÉRENCE";
            textBox1.ForeColor = System.Drawing.Color.Gray;
            textBox2.Text = "Saisie DÉSIGNATION";
            textBox2.ForeColor = System.Drawing.Color.Gray;
            textBox3.Text = "Saisie N°LOT";
            textBox3.ForeColor = System.Drawing.Color.Gray;
            textBox4.Text = "Saisie DATE";
            textBox4.ForeColor = System.Drawing.Color.Gray;
            textBox5.Text = "Saisie QUANTITÉ";
            textBox5.ForeColor = System.Drawing.Color.Gray;
            textBox6.Text = "Saisie UNITÉ";
            textBox6.ForeColor = System.Drawing.Color.Gray;
            textBox7.Text = "Saisie PRIX LOT";
            textBox7.ForeColor = System.Drawing.Color.Gray;
            textBox14.Text = "Rechercher...";
            textBox14.ForeColor = System.Drawing.Color.Black;
            LoadData();
            //Ouvrir une connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //Définir la commande SQL pour récupérer les données de la table PF
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[prix_lot]", connection);

                //Exécuter la commande SQL et récupérer les résultats dans un objet SqlDataReader
                SqlDataReader dr = cmd.ExecuteReader();

                //Créer des collections d'autocomplétion pour chaque TextBox
                AutoCompleteStringCollection collection1 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection collection2 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection collection3 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection collection5 = new AutoCompleteStringCollection();

                //Parcourir chaque ligne de résultats et ajouter les valeurs appropriées aux collections d'autocomplétion
                while (dr.Read())
                {
                    collection1.Add(dr["ref"].ToString());
                    collection2.Add(dr["Désignation"].ToString());
                    collection3.Add(dr["Nlot"].ToString());
                    DateTime dateValue;
                    if (DateTime.TryParse(dr["date"].ToString(), out dateValue))
                    {
                        collection5.Add(dateValue.ToString("dd/MM/yyyy"));
                    }

                }
                //Lier chaque collection d'autocomplétion à son TextBox correspondant
                string[] combinedArray = collection1.Cast<string>().Concat(collection2.Cast<string>()).Concat(collection3.Cast<string>()).Concat(collection5.Cast<string>()).ToArray();

                textBox14.AutoCompleteCustomSource.AddRange(combinedArray);

                //Fermer le DataReader et la connexion
                dr.Close();

            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Vérifier les nouvelles modifications
            CheckForChanges();
        }
        private DateTime lastCheckDate = DateTime.Now;
        private void CheckForChanges()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Récupérer les nouvelles modifications depuis la table de suivi
                string query = "SELECT * FROM [Cogitel].[dbo].[ChangePrix_lot] WHERE [Timestamp] > @LastCheckDate AND [Id] = (SELECT MAX([Id]) FROM [Cogitel].[dbo].[ChangePrix_lot]);";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LastCheckDate", lastCheckDate); // lastCheckDate est la date de la dernière vérification
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable changesTable = new DataTable();
                dataAdapter.Fill(changesTable);

                if (changesTable.Rows.Count > 0)
                {
                    allData.Clear();

                    // Restaurer la valeur de offset
                    offset = 0;

                    // Des modifications ont été détectées, appeler la méthode LoadData
                    LoadData();
                }

                // Mettre à jour la date de la dernière vérification avec la date et l'heure actuelles
                lastCheckDate = DateTime.Now;

                connection.Close();
            }
        }
        private readonly DataTable allData = new DataTable();
        private readonly int limit = 35;
        private int offset = 0;
        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Obtenir le nombre total de lignes dans la table PF
                string countQuery = "SELECT COUNT(*) FROM prix_lot";
                SqlCommand countCommand = new SqlCommand(countQuery, connection);
                int rowCount = (int)countCommand.ExecuteScalar();

                // Vérifier si le nombre de lignes dans allData est inférieur ou égal au nombre total de lignes
                if (allData.Rows.Count <= rowCount)
                {
                    // Exécuter la requête de sélection
                    string query = "SELECT * FROM prix_lot ORDER BY id_lot DESC  OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Offset", offset);
                        command.Parameters.AddWithValue("@Limit", limit);
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable newDataTable = new DataTable();
                        dataAdapter.Fill(newDataTable);

                        allData.Merge(newDataTable); // Merge the new data with existing data
                        dataGridView1.DataSource = allData;
                      
                    }

                }
                else
                {
                    MessageBox.Show("Tous les enregistrements ont été récupérés.");
                }
                connection.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text) ||
                (textBox1.Text == "Saisie RÉFÉRENCE" || textBox2.Text == "Saisie DÉSIGNATION" || textBox3.Text == "Saisie N°LOT" || textBox4.Text == "Saisie DATE" || textBox5.Text == "Saisie QUANTITÉ" || textBox6.Text == "Saisie UNITÉ" || textBox7.Text == "Saisie PRIX LOT"))
            { 
                
                MessageBox.Show("Veuillez remplir toutes les cases .", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

               
                    string query = "INSERT INTO prix_lot (ref, Désignation, Nlot, date, qté, unité, prix_lot) VALUES (@valeur1, @valeur2, @valeur3, @valeurDate, @valeur5, @valeur6, @valeur7)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@valeur1", textBox1.Text);
                    command.Parameters.AddWithValue("@valeur2", textBox2.Text);
                    command.Parameters.AddWithValue("@valeur3", textBox3.Text);
                    command.Parameters.AddWithValue("@valeur6", textBox6.Text);
                    command.Parameters.AddWithValue("@valeur5", float.Parse(textBox5.Text));
                    command.Parameters.AddWithValue("@valeur7", float.Parse(textBox7.Text));
                    DateTime? nullableDate = null;
                    if (!string.IsNullOrWhiteSpace(textBox4.Text) && DateTime.TryParse(textBox4.Text, out DateTime date))
                    {
                        nullableDate = date;
                    }
                    SqlParameter valeurDateParam = new SqlParameter("@valeurDate", nullableDate ?? (object)DBNull.Value);
                    command.Parameters.Add(valeurDateParam);
                    command.ExecuteNonQuery();
                    connection.Close();
                    
                
            }
            textBox1.Text = "Saisie RÉFÉRENCE";
            textBox1.ForeColor = System.Drawing.Color.Gray;
            textBox2.Text = "Saisie DÉSIGNATION";
            textBox2.ForeColor = System.Drawing.Color.Gray;
            textBox3.Text = "Saisie N°LOT";
            textBox3.ForeColor = System.Drawing.Color.Gray;
            textBox4.Text = "Saisie DATE";
            textBox4.ForeColor = System.Drawing.Color.Gray;
            textBox5.Text = "Saisie QUANTITÉ";
            textBox5.ForeColor = System.Drawing.Color.Gray;
            textBox6.Text = "Saisie UNITÉ";
            textBox6.ForeColor = System.Drawing.Color.Gray;
            textBox7.Text = "Saisie PRIX LOT";
            textBox7.ForeColor = System.Drawing.Color.Gray;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && !textBox1.Text.Contains("Saisie RÉFÉRENCE") && !textBox2.Text.Contains("Saisie DÉSIGNATION") && !textBox3.Text.Contains("Saisie N°LOT")  && !textBox5.Text.Contains("Saisie QUANTITÉ") && !textBox6.Text.Contains("Saisie UNITÉ") && !textBox7.Text.Contains("Saisie PRIX LOT") )
            {
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text) ||
              (textBox1.Text == "Saisie RÉFÉRENCE" || textBox2.Text == "Saisie DÉSIGNATION" || textBox3.Text == "Saisie N°LOT" || textBox4.Text == "Saisie DATE" || textBox5.Text == "Saisie QUANTITÉ" || textBox6.Text == "Saisie UNITÉ" || textBox7.Text == "Saisie PRIX LOT"))
                {

                    MessageBox.Show("Veuillez remplir toutes les cases .", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Voulez-vous vraiment modifier cette ligne ?", "Confirmation de modification", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                        connection = new SqlConnection(connectionString);
                        // Mettre à jour les données dans la base de données pour la ligne modifiée
                        string reff = textBox1.Text;
                        string Désignation = textBox2.Text;
                        string Nlot = textBox3.Text;
                        string unité = textBox6.Text;
                        float qté = float.Parse(textBox5.Text);
                        float prix_lot = float.Parse(textBox7.Text);
                        DateTime? nullableDate = null;
                        DateTime date;

                        if (string.IsNullOrWhiteSpace(textBox4.Text))
                        {
                            // If the TextBox is empty or contains only white spaces, set nullableDate to null
                            nullableDate = null;
                        }
                        else if (DateTime.TryParse(textBox4.Text, out date))
                        {
                            // If the TextBox contains a valid date, assign the value to nullableDate
                            nullableDate = date;
                        }

                        // Récupérer l'id de la ligne sélectionnée dans le DataGridView
                        int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_lot"].Value);

                        connection.Open();
                        string updateQuery = "UPDATE [prix_lot] SET [ref] = @reff, [Désignation] = @Désignation, [Nlot] = @Nlot, [date] = @nullableDate, [qté] = @qté, [unité] = @unité, [prix_lot] = @prix_lot  WHERE [id_lot] = @id";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@reff", reff);
                        updateCommand.Parameters.AddWithValue("@Désignation", Désignation);
                        updateCommand.Parameters.AddWithValue("@Nlot", Nlot);
                        updateCommand.Parameters.AddWithValue("@unité", unité);
                        updateCommand.Parameters.AddWithValue("@qté", qté);
                        updateCommand.Parameters.AddWithValue("@prix_lot", prix_lot);
                        updateCommand.Parameters.AddWithValue("@nullableDate", nullableDate.HasValue ? (object)nullableDate : DBNull.Value);
                        updateCommand.Parameters.AddWithValue("@id", id);
                        updateCommand.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Les données ont été modifiées avec succès .");
                        textBox1.Text = "Saisie RÉFÉRENCE";
                        textBox1.ForeColor = System.Drawing.Color.Gray;
                        textBox2.Text = "Saisie DÉSIGNATION";
                        textBox2.ForeColor = System.Drawing.Color.Gray;
                        textBox3.Text = "Saisie N°LOT";
                        textBox3.ForeColor = System.Drawing.Color.Gray;
                        textBox4.Text = "Saisie DATE";
                        textBox4.ForeColor = System.Drawing.Color.Gray;
                        textBox5.Text = "Saisie QUANTITÉ";
                        textBox5.ForeColor = System.Drawing.Color.Gray;
                        textBox6.Text = "Saisie UNITÉ";
                        textBox6.ForeColor = System.Drawing.Color.Gray;
                        textBox7.Text = "Saisie PRIX LOT";
                        textBox7.ForeColor = System.Drawing.Color.Gray;
                        textBox1.Multiline = false;
                        textBox2.Multiline = false;
                        textBox3.Multiline = false;
                        textBox4.Multiline = false;
                        textBox5.Multiline = false;
                        textBox6.Multiline = false;
                        textBox7.Multiline = false;



                    }
                }
            }

            else
            {
                MessageBox.Show("Veuillez sélectionner par double Click une ligne pour l'éditer.", "Aucune ligne sélectionnée", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Récupérer l'identifiant unique de la ligne sélectionnée
                int idToDelete = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_lot"].Value); // Remplacer "Id" par le nom de votre colonne contenant l'identifiant unique

                // Demander une confirmation avant de supprimer
                DialogResult dialogResult = MessageBox.Show("Voulez-vous vraiment supprimer cette ligne ?", "Confirmation de suppression", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Supprimer la ligne correspondante de la base de données
                    string query = "DELETE FROM prix_lot WHERE id_lot = @id"; // Remplacer "MaTable" par le nom de votre table et "Id" par le nom de votre colonne d'identifiant unique
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
                            connection.Close();
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

        private void button4_Click(object sender, EventArgs e)
        {
            cogitel.Instance.ShowOrHidePanel(true);
            allData.Clear();
            this.Close();
        }
        private void button1_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button1, "Ajouter");
            this.Cursor = Cursors.Hand;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button2, "Modifier");
            this.Cursor = Cursors.Hand;

        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button3, "Supprimer");
            this.Cursor = Cursors.Hand;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
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
        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Saisie RÉFÉRENCE")
            {
                textBox1.Text = "";
                textBox1.ForeColor = System.Drawing.Color.Black;
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Saisie RÉFÉRENCE";
                textBox1.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Saisie DÉSIGNATION")
            {
                textBox2.Text = "";
                textBox2.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Saisie DÉSIGNATION";
                textBox2.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Saisie N°LOT")
            {
                textBox3.Text = "";
                textBox3.ForeColor = System.Drawing.Color.Black; ;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Saisie N°LOT";
                textBox3.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Saisie DATE")
            {
                textBox4.Text = "";
                textBox4.ForeColor = System.Drawing.Color.Black;
            }
        }
        private void textBox4_Leave(object sender, EventArgs e)
        {
            DateTime result;
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                textBox4.Text = "Saisie DATE";
                textBox4.ForeColor = System.Drawing.Color.Gray;
            }
            else if (!DateTime.TryParseExact(textBox4.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                MessageBox.Show("Vous devez entrer une date valide au format jj/mm/aaaa.");
                textBox4.Text = "Saisie DATE";
                textBox4.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Saisie QUANTITÉ")
            {
                textBox5.Text = "";
                textBox5.ForeColor = System.Drawing.Color.Black;
            }
        }
        private void textBox5_Leave(object sender, EventArgs e)
        {

            float result;
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                textBox5.Text = "Saisie QUANTITÉ";
                textBox5.ForeColor = System.Drawing.Color.Gray;
            }
            else if (!float.TryParse(textBox5.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox5.Text = "Saisie QUANTITÉ";
                textBox5.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Saisie UNITÉ")
            {
                textBox6.Text = "";
                textBox6.ForeColor = System.Drawing.Color.Black; ;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = "Saisie UNITÉ";
                textBox6.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "Saisie PRIX LOT")
            {
                textBox7.Text = "";
                textBox7.ForeColor = System.Drawing.Color.Black;
            }
        }
        private void textBox7_Leave(object sender, EventArgs e)
        {

            float result;
            if (string.IsNullOrEmpty(textBox7.Text))
            {
                textBox7.Text = "Saisie PRIX LOT";
                textBox7.ForeColor = System.Drawing.Color.Gray;
            }
            else if (!float.TryParse(textBox7.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox7.Text = "Saisie PRIX LOT";
                textBox7.ForeColor = System.Drawing.Color.Gray;
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                textBox1.Text = selectedRow.Cells["refDataGridViewTextBoxColumn"].Value.ToString().Replace("\n", Environment.NewLine);
                textBox1.ForeColor = System.Drawing.Color.Black; ;
                textBox1.Multiline = true;

                textBox2.Text = selectedRow.Cells["désignationDataGridViewTextBoxColumn"].Value.ToString().Replace("\n", Environment.NewLine);
                textBox2.ForeColor = System.Drawing.Color.Black;
                textBox2.Multiline = true;

                textBox3.Text = selectedRow.Cells["nlotDataGridViewTextBoxColumn"].Value.ToString().Replace("\n", Environment.NewLine);
                textBox3.ForeColor = System.Drawing.Color.Black;
                textBox3.Multiline = true;

                textBox5.Text = selectedRow.Cells["qtéDataGridViewTextBoxColumn"].Value.ToString().Replace("\n", Environment.NewLine);
                textBox5.ForeColor = System.Drawing.Color.Black;
                textBox5.Multiline = true;

                textBox6.Text = selectedRow.Cells["unitéDataGridViewTextBoxColumn"].Value.ToString().Replace("\n", Environment.NewLine);
                textBox6.ForeColor = System.Drawing.Color.Black;
                textBox6.Multiline = true;

                textBox7.Text = selectedRow.Cells["prixlotDataGridViewTextBoxColumn"].Value.ToString().Replace("\n", Environment.NewLine);
                textBox7.ForeColor = System.Drawing.Color.Black;
                textBox7.Multiline = true;
                if (selectedRow.Cells["dateDataGridViewTextBoxColumn"].Value != DBNull.Value)
                {
                    DateTime dateValue = (DateTime)selectedRow.Cells["dateDataGridViewTextBoxColumn"].Value;
                    textBox4.Text = dateValue.ToString("dd/MM/yyyy");
                    textBox4.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    textBox4.Text = "";
                }
                textBox4.ForeColor = System.Drawing.Color.Black;
                textBox4.Multiline = true;
                textBox1.Size = new System.Drawing.Size(214, 67);
                textBox2.Size = new System.Drawing.Size(214, 67);
                textBox3.Size = new System.Drawing.Size(214, 67);
                textBox4.Size = new System.Drawing.Size(214, 67);
                textBox5.Size = new System.Drawing.Size(214, 67);
                textBox6.Size = new System.Drawing.Size(214, 67);
                textBox7.Size = new System.Drawing.Size(214, 67);
               
            }
        }

        private void prix_lot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // Simule un clic sur le bouton "Supprimer"
                button3.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                textBox1.Text = "Saisie RÉFÉRENCE";
                textBox1.ForeColor = System.Drawing.Color.Gray;
                textBox2.Text = "Saisie DÉSIGNATION";
                textBox2.ForeColor = System.Drawing.Color.Gray;
                textBox3.Text = "Saisie N°LOT";
                textBox3.ForeColor = System.Drawing.Color.Gray;
                textBox4.Text = "Saisie DATE";
                textBox4.ForeColor = System.Drawing.Color.Gray;
                textBox5.Text = "Saisie QUANTITÉ";
                textBox5.ForeColor = System.Drawing.Color.Gray;
                textBox6.Text = "Saisie UNITÉ";
                textBox6.ForeColor = System.Drawing.Color.Gray;
                textBox7.Text = "Saisie PRIX LOT";
                textBox7.ForeColor = System.Drawing.Color.Gray;

                textBox1.Multiline = false;
                textBox2.Multiline = false;
                textBox3.Multiline = false;
                textBox4.Multiline = false;
                textBox5.Multiline = false;
                textBox6.Multiline = false;
                textBox7.Multiline = false;
                

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox14.Text;

            // Try to parse the search term as a date
            if (DateTime.TryParse(searchTerm, out DateTime searchDate))
            {
                // Format the search date as a string in a specific format that can be used in the SQL query
                searchTerm = searchDate.ToString("dd/MM/yyyy");
                string sql = "SELECT * FROM prix_lot WHERE date  = '" + searchTerm + "'";
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
                string sql = "SELECT * FROM prix_lot WHERE ref = '" + searchTerm + "' OR Désignation  = '" + searchTerm + "' OR Nlot  = '" + searchTerm + "' ";

                using (var adapter = new SqlDataAdapter(sql, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                connection.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
                textBox14.Text = "Rechercher..."; // réinitialise le contenu du texte de recherche
                textBox14.ForeColor = System.Drawing.Color.Black;
                dataGridView1.DataSource = allData;
            
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
