using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.Configuration;
using System.Threading;




namespace Cogitel_QT
{
    public partial class PrixPF : Form
    {
        SqlConnection connection;
        string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;

        public PrixPF()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);

            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type dgvType = dataGridView1.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dataGridView1, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type tlpType = tableLayoutPanel2.GetType();
                PropertyInfo pi = tlpType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(tableLayoutPanel2, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type tlpType = tableLayoutPanel3.GetType();
                PropertyInfo pi = tlpType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(tableLayoutPanel3, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type tlpType = tableLayoutPanel4.GetType();
                PropertyInfo pi = tlpType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(tableLayoutPanel4, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type tlpType = tableLayoutPanel5.GetType();
                PropertyInfo pi = tlpType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(tableLayoutPanel5, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type tlpType = tableLayoutPanel6.GetType();
                PropertyInfo pi = tlpType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(tableLayoutPanel6, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type tlpType = tableLayoutPanel7.GetType();
                PropertyInfo pi = tlpType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(tableLayoutPanel7, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type panelType = panel1.GetType();
                PropertyInfo pi = panelType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(panel1, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type panelType = panel2.GetType();
                PropertyInfo pi = panelType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(panel2, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type panelType = panel3.GetType();
                PropertyInfo pi = panelType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(panel3, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type buttonType = button1.GetType();
                PropertyInfo pi = buttonType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(button1, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type buttonType = button2.GetType();
                PropertyInfo pi = buttonType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(button2, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type buttonType = button3.GetType();
                PropertyInfo pi = buttonType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(button3, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type buttonType = button5.GetType();
                PropertyInfo pi = buttonType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(button5, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type buttonType = button7.GetType();
                PropertyInfo pi = buttonType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(button7, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type buttonType = button6.GetType();
                PropertyInfo pi = buttonType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(button6, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type buttonType = button4.GetType();
                PropertyInfo pi = buttonType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(button4, true, null);
            }
        }
      
        private void PrixPF_Load(object sender, EventArgs e)
        {

            timer1.Interval = 2000;
            // Démarrer le timer
            timer1.Start();
            // Charger les données initiales
            LoadData();
            textBox1.Text = "Saisie N° de doc";
            textBox1.ForeColor = System.Drawing.Color.Gray;
            textBox2.Text = "Saisie Article";
            textBox2.ForeColor = System.Drawing.Color.Gray;
            textBox3.Text = "Saisie Désignation";
            textBox3.ForeColor = System.Drawing.Color.Gray;
            textBox5.Text = "Saisie Qté Stock";
            textBox5.ForeColor = System.Drawing.Color.Gray;
            textBox6.Text = "Saisie N° de lot";
            textBox6.ForeColor = System.Drawing.Color.Gray;
            textBox7.Text = "Saisie Q";
            textBox7.ForeColor = System.Drawing.Color.Gray;
            textBox8.Text = "Saisie F";
            textBox8.ForeColor = System.Drawing.Color.Gray; ;
            textBox9.Text = "Saisie Prix unitaire";
            textBox9.ForeColor = System.Drawing.Color.Gray;
            textBox10.Text = "Saisie P";
            textBox10.ForeColor = System.Drawing.Color.Gray;
            textBox11.Text = "Saisie date";
            textBox11.ForeColor = System.Drawing.Color.Gray;
            textBox4.Text = "Saisie S.Client";
            textBox4.ForeColor = System.Drawing.Color.Gray;
            textBox14.Text = "Rechercher...";
            textBox14.ForeColor = System.Drawing.Color.Black;
            
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
                AutoCompleteStringCollection collection2 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection collection3 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection collection4 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection collection5 = new AutoCompleteStringCollection();

                //Parcourir chaque ligne de résultats et ajouter les valeurs appropriées aux collections d'autocomplétion
                while (dr.Read())
                {
                    collection1.Add(dr["Article"].ToString());
                    collection2.Add(dr["Désignation"].ToString());
                    collection3.Add(dr["N_de_lot"].ToString());
                    collection4.Add(dr["N_de_doc"].ToString());
                    DateTime dateValue;
                    if (DateTime.TryParse(dr["date"].ToString(), out dateValue))
                    {
                        collection5.Add(dateValue.ToString("dd/MM/yyyy"));
                    }

                }
                //Lier chaque collection d'autocomplétion à son TextBox correspondant
                textBox2.AutoCompleteCustomSource = collection1;
                textBox3.AutoCompleteCustomSource = collection2;
                textBox5.AutoCompleteCustomSource = collection3;
                textBox10.AutoCompleteCustomSource = collection5;
                string[] combinedArray = collection1.Cast<string>().Concat(collection2.Cast<string>()).Concat(collection3.Cast<string>()).Concat(collection4.Cast<string>()).Concat(collection5.Cast<string>()).ToArray();

                textBox14.AutoCompleteCustomSource.AddRange(combinedArray);

                //Fermer le DataReader et la connexion
                dr.Close();

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


                
                    // Exécuter la requête de sélection
                    string query = "SELECT * FROM PrixPF ORDER BY id_pricpf DESC  OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Offset", offset);
                        command.Parameters.AddWithValue("@Limit", limit);
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable newDataTable = new DataTable();
                        dataAdapter.Fill(newDataTable);

                        allData.Merge(newDataTable); // Merge the new data with existing data
                        dataGridView1.DataSource = allData;
                        connection.Close();
                    }
               
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
                string query = "SELECT * FROM [Cogitel].[dbo].[ChangePRIXPF] WHERE [Timestamp] > @LastCheckDate AND [Id] = (SELECT MAX([Id]) FROM [Cogitel].[dbo].[ChangePRIXPF]);";

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
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Saisie N° de doc" || textBox2.Text == "Saisie Article" || textBox3.Text == "Saisie Désignation" || textBox4.Text == "Saisie S.Client" || textBox5.Text == "Saisie Qté Stock" || textBox6.Text == "Saisie N° de lot" || textBox7.Text == "Saisie Q" || textBox8.Text == "Saisie F" || textBox9.Text == "Saisie Prix unitaire" || textBox10.Text == "Saisie P" || textBox11.Text == "Saisie date" ||
                string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text) || string.IsNullOrEmpty(textBox8.Text) || string.IsNullOrEmpty(textBox9.Text) || string.IsNullOrEmpty(textBox10.Text) || string.IsNullOrEmpty(textBox11.Text))
            {
                MessageBox.Show("Veuillez remplir toutes les cases .", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

          
                    string query = "INSERT INTO PrixPF (N_de_doc, Article, Désignation, s_clinet, Qté_Stock, N_de_lot, Q, F, Prix_unitaire, P, date) VALUES (@valeur1, @valeur2, @valeur3, @valeur4, @valeur5, @valeur6, @valeur7, @valeur8, @valeur9, @valeur10, @valeurDate)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@valeur1", textBox1.Text);
                    command.Parameters.AddWithValue("@valeur2", textBox2.Text);
                    command.Parameters.AddWithValue("@valeur3", textBox3.Text);
                    command.Parameters.AddWithValue("@valeur4", textBox4.Text);
                    command.Parameters.AddWithValue("@valeur5", textBox5.Text);
                    command.Parameters.AddWithValue("@valeur6", textBox6.Text);
                    command.Parameters.AddWithValue("@valeur7", textBox7.Text);
                    command.Parameters.AddWithValue("@valeur8", float.Parse(textBox8.Text));
                    command.Parameters.AddWithValue("@valeur9", float.Parse(textBox9.Text));
                    command.Parameters.AddWithValue("@valeur10", float.Parse(textBox10.Text));
                    DateTime? nullableDate = null;
                    if (!string.IsNullOrWhiteSpace(textBox11.Text) && DateTime.TryParse(textBox11.Text, out DateTime date))
                    {
                        nullableDate = date;
                    }
                    SqlParameter valeurDateParam = new SqlParameter("@valeurDate", nullableDate ?? (object)DBNull.Value);
                    command.Parameters.Add(valeurDateParam);
                    command.ExecuteNonQuery();
                    connection.Close();
                    
                    textBox1.Text = "Saisie N° de doc";
                    textBox1.ForeColor = System.Drawing.Color.Gray;
                    textBox2.Text = "Saisie Article";
                    textBox2.ForeColor = System.Drawing.Color.Gray;
                    textBox3.Text = "Saisie Désignation";
                    textBox3.ForeColor = System.Drawing.Color.Gray;
                    textBox5.Text = "Saisie Qté Stock";
                    textBox5.ForeColor = System.Drawing.Color.Gray;
                    textBox6.Text = "Saisie N° de lot";
                    textBox6.ForeColor = System.Drawing.Color.Gray;
                    textBox7.Text = "Saisie Q";
                    textBox7.ForeColor = System.Drawing.Color.Gray;
                    textBox8.Text = "Saisie F";
                    textBox8.ForeColor = System.Drawing.Color.Gray; ;
                    textBox9.Text = "Saisie Prix unitaire";
                    textBox9.ForeColor = System.Drawing.Color.Gray;
                    textBox10.Text = "Saisie P";
                    textBox10.ForeColor = System.Drawing.Color.Gray;
                    textBox11.Text = "Saisie date";
                    textBox11.ForeColor = System.Drawing.Color.Gray;
                    textBox4.Text = "Saisie S.Client";
                    textBox4.ForeColor = System.Drawing.Color.Gray;
                
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && !textBox1.Text.Contains("Saisie N° de doc") && !textBox2.Text.Contains("Saisie Article") && !textBox3.Text.Contains("Saisie Désignation") && !textBox4.Text.Contains("Saisie Qté Stock") && !textBox5.Text.Contains("Saisie N° de lot") && !textBox6.Text.Contains("Saisie Q") && !textBox7.Text.Contains("Saisie F") && !textBox8.Text.Contains("Saisie Prix unitaire") && !textBox9.Text.Contains("Saisie P") && !textBox10.Text.Contains("Saisie date") && !textBox11.Text.Contains("Prix Facturation"))
            {
                if (textBox1.Text == "Saisie N° de doc" || textBox2.Text == "Saisie Article" || textBox3.Text == "Saisie Désignation" || textBox4.Text == "Saisie S.Client" || textBox5.Text == "Saisie Qté Stock" || textBox6.Text == "Saisie N° de lot" || textBox7.Text == "Saisie Q" || textBox8.Text == "Saisie F" || textBox9.Text == "Saisie Prix unitaire" || textBox10.Text == "Saisie P" || textBox11.Text == "Saisie date" ||
                string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text) || string.IsNullOrEmpty(textBox8.Text) || string.IsNullOrEmpty(textBox9.Text) || string.IsNullOrEmpty(textBox10.Text) || string.IsNullOrEmpty(textBox11.Text))
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
                        string N_de_doc = textBox1.Text;
                        string Article = textBox2.Text;
                        string Désignation = textBox3.Text;
                        string SClient = textBox4.Text;
                        string Qté_Stock = textBox5.Text;
                        string N_de_lot = textBox6.Text;
                        string Q = textBox7.Text;
                        float F = float.Parse(textBox8.Text);
                        float Prix_unitaire = float.Parse(textBox9.Text);
                        float P = float.Parse(textBox10.Text);
                        DateTime? nullableDate = null;
                        DateTime date;

                        if (string.IsNullOrWhiteSpace(textBox11.Text))
                        {
                            // If the TextBox is empty or contains only white spaces, set nullableDate to null
                            nullableDate = null;
                        }
                        else if (DateTime.TryParse(textBox11.Text, out date))
                        {
                            // If the TextBox contains a valid date, assign the value to nullableDate
                            nullableDate = date;
                        }

                        // Récupérer l'id de la ligne sélectionnée dans le DataGridView
                        int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_pricpf"].Value);

                        connection.Open();
                        string updateQuery = "UPDATE [PrixPF] SET [N_de_doc] = @N_de_doc, [Article] = @Article, [Désignation] = @Désignation, [s_clinet] = @SClient, [Qté_Stock] = @Qté_Stock, [N_de_lot] = N_de_lot, [Q] = @Q, [F] = @F, [Prix_unitaire] = @Prix_unitaire, [P] = @P, [date] = @nullableDate  WHERE [id_pricpf] = @id";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@N_de_doc", N_de_doc);
                        updateCommand.Parameters.AddWithValue("@Article", Article);
                        updateCommand.Parameters.AddWithValue("@Désignation", Désignation);
                        updateCommand.Parameters.AddWithValue("@Qté_Stock", Qté_Stock);
                        updateCommand.Parameters.AddWithValue("@N_de_lot", N_de_lot);
                        updateCommand.Parameters.AddWithValue("@Q", Q);
                        updateCommand.Parameters.AddWithValue("@F", F);
                        updateCommand.Parameters.AddWithValue("@Prix_unitaire ", Prix_unitaire);
                        updateCommand.Parameters.AddWithValue("@P", P);
                        updateCommand.Parameters.AddWithValue("@nullableDate", nullableDate.HasValue ? (object)nullableDate : DBNull.Value);
                        updateCommand.Parameters.AddWithValue("@SClient", SClient);
                        updateCommand.Parameters.AddWithValue("@id", id);
                        updateCommand.ExecuteNonQuery();
                        connection.Close();
                        
                        MessageBox.Show("Les données ont été modifiées avec succès .");
                        textBox1.Text = "Saisie N° de doc";
                        textBox1.ForeColor = System.Drawing.Color.Gray;
                        textBox2.Text = "Saisie Article";
                        textBox2.ForeColor = System.Drawing.Color.Gray;
                        textBox3.Text = "Saisie Désignation";
                        textBox3.ForeColor = System.Drawing.Color.Gray;
                        textBox5.Text = "Saisie Qté Stock";
                        textBox5.ForeColor = System.Drawing.Color.Gray;
                        textBox6.Text = "Saisie N° de lot";
                        textBox6.ForeColor = System.Drawing.Color.Gray;
                        textBox7.Text = "Saisie Q";
                        textBox7.ForeColor = System.Drawing.Color.Gray;
                        textBox8.Text = "Saisie F";
                        textBox8.ForeColor = System.Drawing.Color.Gray; ;
                        textBox9.Text = "Saisie Prix unitaire";
                        textBox9.ForeColor = System.Drawing.Color.Gray;
                        textBox10.Text = "Saisie P";
                        textBox10.ForeColor = System.Drawing.Color.Gray;
                        textBox11.Text = "Saisie date";
                        textBox11.ForeColor = System.Drawing.Color.Gray;
                        textBox4.Text = "Saisie S.Client";
                        textBox4.ForeColor = System.Drawing.Color.Gray;
                        textBox1.Multiline = false;
                        textBox2.Multiline = false;
                        textBox3.Multiline = false;
                        textBox4.Multiline = false;
                        textBox5.Multiline = false;
                        textBox6.Multiline = false;
                        textBox7.Multiline = false;
                        textBox8.Multiline = false;
                        textBox9.Multiline = false;
                        textBox10.Multiline = false;
                        textBox11.Multiline = false;
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
                int idToDelete = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_pricpf"].Value); // Remplacer "Id" par le nom de votre colonne contenant l'identifiant unique

                // Demander une confirmation avant de supprimer
                DialogResult dialogResult = MessageBox.Show("Voulez-vous vraiment supprimer cette ligne ?", "Confirmation de suppression", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Supprimer la ligne correspondante de la base de données
                    string query = "DELETE FROM PrixPF WHERE id_pricpf = @id"; // Remplacer "MaTable" par le nom de votre table et "Id" par le nom de votre colonne d'identifiant unique
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
            if (textBox1.Text == "Saisie N° de doc")
            {
                textBox1.Text = "";
                textBox1.ForeColor = System.Drawing.Color.Black;
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Saisie N° de doc";
                textBox1.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Saisie Article")
            {
                textBox2.Text = "";
                textBox2.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Saisie Article";
                textBox2.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Saisie Désignation")
            {
                textBox3.Text = "";
                textBox3.ForeColor = System.Drawing.Color.Black; ;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Saisie Désignation";
                textBox3.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Saisie Qté Stock")
            {
                textBox5.Text = "";
                textBox5.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "Saisie Qté Stock";
                textBox5.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Saisie N° de lot")
            {
                textBox6.Text = "";
                textBox6.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = "Saisie N° de lot";
                textBox6.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "Saisie Q")
            {
                textBox7.Text = "";
                textBox7.ForeColor = System.Drawing.Color.Black;
            }
        }
        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                textBox7.Text = "Saisie Q";
                textBox7.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void textBox8_Enter(object sender, EventArgs e)
        {
            if (textBox8.Text == "Saisie F")
            {
                textBox8.Text = "";
                textBox8.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {

            float result;
            if (string.IsNullOrEmpty(textBox8.Text))
            {
                textBox8.Text = "Saisie F";
                textBox8.ForeColor = System.Drawing.Color.Gray;
            }
            else if (!float.TryParse(textBox8.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox8.Text = "Saisie F";
                textBox8.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void textBox9_Enter(object sender, EventArgs e)
        {
            if (textBox9.Text == "Saisie Prix unitaire")
            {
                textBox9.Text = "";
                textBox9.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox9.Text))
            {
                textBox9.Text = "Saisie Prix unitaire";
                textBox9.ForeColor = System.Drawing.Color.Gray;
            }
            else if (!float.TryParse(textBox9.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox9.Text = "Saisie Prix unitaire";
                textBox9.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void textBox10_Enter(object sender, EventArgs e)
        {
            if (textBox10.Text == "Saisie P")
            {
                textBox10.Text = "";
                textBox10.ForeColor = System.Drawing.Color.Black;
            }
        }
        private void textBox10_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox10.Text))
            {
                textBox10.Text = "Saisie P";
                textBox10.ForeColor = System.Drawing.Color.Gray;
            }

            else if (!float.TryParse(textBox10.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox10.Text = "Saisie P";
                textBox10.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void textBox11_Enter(object sender, EventArgs e)
        {
            if (textBox11.Text == "Saisie date")
            {
                textBox11.Text = "";
                textBox11.ForeColor = System.Drawing.Color.Black;
            }
        }
        private void textBox11_Leave(object sender, EventArgs e)
        {
            DateTime result;
            if (string.IsNullOrEmpty(textBox11.Text))
            {
                textBox11.Text = "Saisie date";
                textBox11.ForeColor = System.Drawing.Color.Gray;
            }
            else if (!DateTime.TryParseExact(textBox11.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                MessageBox.Show("Vous devez entrer une date valide au format jj/mm/aaaa.");
                textBox11.Text = "Saisie date";
                textBox11.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Saisie S.Client";
                textBox4.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Saisie S.Client")
            {
                textBox4.Text = "";
                textBox4.ForeColor = System.Drawing.Color.Black;
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                textBox1.Text = selectedRow.Cells["ndedocDataGridViewTextBoxColumn"].Value.ToString().Replace("\n", Environment.NewLine);
                textBox1.ForeColor = System.Drawing.Color.Black; ;
                textBox1.Multiline = true;

                textBox2.Text = selectedRow.Cells["articleDataGridViewTextBoxColumn"].Value.ToString().Replace("\n", Environment.NewLine);
                textBox2.ForeColor = System.Drawing.Color.Black;
                textBox2.Multiline = true;

                textBox3.Text = selectedRow.Cells["désignationDataGridViewTextBoxColumn"].Value.ToString().Replace("\n", Environment.NewLine);
                textBox3.ForeColor = System.Drawing.Color.Black;
                textBox3.Multiline = true;

                textBox5.Text = selectedRow.Cells["qtéStockDataGridViewTextBoxColumn"].Value.ToString().Replace("\n", Environment.NewLine);
                textBox5.ForeColor = System.Drawing.Color.Black;
                textBox5.Multiline = true;

                textBox6.Text = selectedRow.Cells["ndelotDataGridViewTextBoxColumn"].Value.ToString().Replace("\n", Environment.NewLine);
                textBox6.ForeColor = System.Drawing.Color.Black;
                textBox6.Multiline = true;

                textBox7.Text = selectedRow.Cells["qDataGridViewTextBoxColumn"].Value.ToString().Replace("\n", Environment.NewLine);
                textBox7.ForeColor = System.Drawing.Color.Black;
                textBox7.Multiline = true;

                textBox8.Text = selectedRow.Cells["fDataGridViewTextBoxColumn"].Value.ToString().Replace("\n", Environment.NewLine);
                textBox8.ForeColor = System.Drawing.Color.Black;
                textBox8.Multiline = true;

                textBox9.Text = selectedRow.Cells["prixunitaireDataGridViewTextBoxColumn"].Value.ToString().Replace("\n", Environment.NewLine);
                textBox9.ForeColor = System.Drawing.Color.Black;
                textBox9.Multiline = true;

                textBox10.Text = selectedRow.Cells["pDataGridViewTextBoxColumn"].Value.ToString().Replace("\n", Environment.NewLine);
                textBox10.ForeColor = System.Drawing.Color.Black;
                textBox10.Multiline = true;
                if (selectedRow.Cells["dateDataGridViewTextBoxColumn"].Value != DBNull.Value)
                {
                    DateTime dateValue = (DateTime)selectedRow.Cells["dateDataGridViewTextBoxColumn"].Value;
                    textBox11.Text = dateValue.ToString("dd/MM/yyyy");
                    textBox11.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    textBox11.Text = "";
                }
                textBox11.Multiline = true;
                textBox4.Text = selectedRow.Cells["sclinetDataGridViewTextBoxColumn"].Value.ToString();
                textBox4.ForeColor = System.Drawing.Color.Black;
                textBox4.Multiline = true;
                textBox1.Size = new System.Drawing.Size(214, 67);
                textBox2.Size = new System.Drawing.Size(214, 67);
                textBox3.Size = new System.Drawing.Size(214, 67);
                textBox4.Size = new System.Drawing.Size(214, 67);
                textBox5.Size = new System.Drawing.Size(214, 67);
                textBox6.Size = new System.Drawing.Size(214, 67);
                textBox7.Size = new System.Drawing.Size(214, 67);
                textBox8.Size = new System.Drawing.Size(214, 67);
                textBox9.Size = new System.Drawing.Size(214, 67);
                textBox10.Size = new System.Drawing.Size(214, 67);
                textBox11.Size = new System.Drawing.Size(214, 67);
            }
        }

        private void PrixPF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // Simule un clic sur le bouton "Supprimer"
                button3.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                textBox1.Text = "Saisie N° de doc";
                textBox1.ForeColor = System.Drawing.Color.Gray;
                textBox2.Text = "Saisie Article";
                textBox2.ForeColor = System.Drawing.Color.Gray;
                textBox3.Text = "Saisie Désignation";
                textBox3.ForeColor = System.Drawing.Color.Gray;
                textBox5.Text = "Saisie Qté Stock";
                textBox5.ForeColor = System.Drawing.Color.Gray;
                textBox6.Text = "Saisie N° de lot";
                textBox6.ForeColor = System.Drawing.Color.Gray;
                textBox7.Text = "Saisie Q";
                textBox7.ForeColor = System.Drawing.Color.Gray;
                textBox8.Text = "Saisie F";
                textBox8.ForeColor = System.Drawing.Color.Gray; ;
                textBox9.Text = "Saisie Prix unitaire";
                textBox9.ForeColor = System.Drawing.Color.Gray;
                textBox10.Text = "Saisie P";
                textBox10.ForeColor = System.Drawing.Color.Gray;
                textBox11.Text = "Saisie date";
                textBox11.ForeColor = System.Drawing.Color.Gray;
                textBox4.Text = "Saisie S.Client";
                textBox4.ForeColor = System.Drawing.Color.Gray;
                textBox1.Multiline = false;
                textBox2.Multiline = false;
                textBox3.Multiline = false;
                textBox4.Multiline = false;
                textBox5.Multiline = false;
                textBox6.Multiline = false;
                textBox7.Multiline = false;
                textBox8.Multiline = false;
                textBox9.Multiline = false;
                textBox10.Multiline = false;
                textBox11.Multiline = false;

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

        private void button5_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox14.Text;

            // Try to parse the search term as a date
            if (DateTime.TryParse(searchTerm, out DateTime searchDate))
            {
                // Format the search date as a string in a specific format that can be used in the SQL query
                searchTerm = searchDate.ToString("dd/MM/yyyy");
                string sql = "SELECT * FROM PrixPF WHERE date  = '" + searchTerm + "'";
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
                string sql = "SELECT * FROM PrixPF WHERE N_de_doc = '" + searchTerm + "' OR Article  = '" + searchTerm + "' OR Désignation  = '" + searchTerm + "' OR N_de_lot  = '" + searchTerm + "' ";

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

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers Excel (*.xlsx)|*.xlsx|Tous les fichiers (*.*)|*.*";
            openFileDialog.Title = "Sélectionner un fichier Excel";
            DialogResult result = openFileDialog.ShowDialog();

            // Vérifier si l'utilisateur a sélectionné un fichier
            if (result == DialogResult.OK)
            {
                // Ouvrir la connexion à la base de données
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    try
                    {
                        // Charger le fichier Excel avec ClosedXML
                        XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName);
                        IXLWorksheet worksheet = workbook.Worksheet(1);

                    // Parcourir les lignes et insérer les données dans la base de données
                   
                         int lastRow = worksheet.LastRowUsed().RowNumber();
                         int colCount = worksheet.ColumnsUsed().Count();
                        // Ignorer les lignes vides
                        for (int i = 8; i <= lastRow; i++) // Ignorer la première ligne si elle contient des en-têtes de colonnes
                        {
                            // Ignorer les lignes vides
                            if (worksheet.Cell(i, 1).Value.ToString().Trim() == "")
                            {
                                continue;
                            }

                                SqlCommand command = new SqlCommand();
                                command.Connection = connection;
                                command.CommandType = System.Data.CommandType.Text;
                                command.CommandText = "INSERT INTO PrixPF (N_de_doc,Article, Désignation, s_clinet, Qté_Stock, N_de_lot, Q, F, Prix_unitaire, P, date) VALUES(@N_de_doc, @Article, @Désignation, @s_clinet, @Qté_Stock, @N_de_lot, @Q, @F, @Prix_unitaire, @P, @date)";
                            command.Parameters.AddWithValue("@N_de_doc", worksheet.Cell(i, 5).Value.ToString().Substring(0, 7));
                            command.Parameters.AddWithValue("@Article", worksheet.Cell(i, 1).Value.ToString());
                                command.Parameters.AddWithValue("@Désignation", worksheet.Cell(i, 2).Value.ToString());
                                command.Parameters.AddWithValue("@s_clinet", worksheet.Cell(i, 3).Value.ToString());
                                command.Parameters.AddWithValue("@Qté_Stock", worksheet.Cell(i, 4).Value.ToString());
                                command.Parameters.AddWithValue("@N_de_lot", worksheet.Cell(i, 5).Value.ToString());
                                command.Parameters.AddWithValue("@Q", worksheet.Cell(i, 6).Value.ToString());

                            float fValue;
                            if (float.TryParse(worksheet.Cell(i, 7).Value.ToString(), out fValue))
                            {
                                float roundedValue = (float)Math.Round(fValue, 3);
                                command.Parameters.AddWithValue("@F", roundedValue);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@F", DBNull.Value);
                            }
                            float prixUnitaireValue;
                            string prixUnitaireString = worksheet.Cell(i, 8).Value.ToString().Replace(",", ".");
                            if (float.TryParse(prixUnitaireString, NumberStyles.Float, CultureInfo.InvariantCulture, out prixUnitaireValue))
                            {
                                float roundedValue = (float)Math.Round(prixUnitaireValue, 3);
                                command.Parameters.AddWithValue("@Prix_unitaire", roundedValue);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Prix_unitaire", DBNull.Value);
                            }
                            float pValue;
                            if (float.TryParse(worksheet.Cell(i, 9).Value.ToString(), out pValue))
                            {
                                float roundedValue = (float)Math.Round(pValue, 3);
                                command.Parameters.AddWithValue("@P", roundedValue);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@P", DBNull.Value);
                            }
                            DateTime? nullableDate = null;
                                if (!string.IsNullOrWhiteSpace(worksheet.Cell(i, 10).Value.ToString()) && DateTime.TryParse(worksheet.Cell(i, 10).Value.ToString(), out DateTime date))
                                {
                                    nullableDate = date;
                                }
                                SqlParameter dateParam = new SqlParameter("@date", nullableDate ?? (object)DBNull.Value);
                                command.Parameters.Add(dateParam);
                                command.ExecuteNonQuery(); // Exécuter la commande SQL INSERT
                          

                        }    
                            
                    }
                     catch (IOException)
                    {
                        // Afficher un message d'erreur si le fichier est déjà ouvert dans une autre application
                        MessageBox.Show("Le fichier est déjà ouvert dans une autre application.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }


            string tableName = "PrixPF"; // Remplacez par le nom de votre table
            int decimalPlaces = 3; // Nombre de décimales à conserver

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = $"UPDATE {tableName} SET F = ROUND(F, {decimalPlaces}), Prix_unitaire = ROUND(Prix_unitaire, {decimalPlaces}), P = ROUND(P, {decimalPlaces})";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.ExecuteNonQuery();

                }
             
            }

            connection.Close();


        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button8, "Importer Excel");
            this.Cursor = Cursors.Hand;
        }

        private void button8_MouseLeave(object sender, EventArgs e)
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

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }
    }
              
     
}
