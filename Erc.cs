using System.Reflection;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Drawing;








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
            timer1.Interval = 3000;
            // Démarrer le timer
            timer1.Start();
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
                string query = "SELECT * FROM [Cogitel].[dbo].[ChangeNCE] WHERE [Timestamp] > @LastCheckDate AND [Id] = (SELECT MAX([Id]) FROM [Cogitel].[dbo].[ChangeNCE]);";

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
        public int offset = 0;
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
                        dataGridView1.ClearSelection();
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
            if (Application.OpenForms.OfType<expoexcel>().Any())
            {
                // Fermer la Form ERCAMS
                expoexcel expoexcel = Application.OpenForms.OfType<expoexcel>().FirstOrDefault();
                expoexcel.Close();
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
            expoexcel expoexcel = new expoexcel();
            expoexcel.Show();
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

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Vérifier si la colonne de la date de réception client est vide
                if (string.IsNullOrEmpty(row.Cells["dateréponseclientDataGridViewTextBoxColumn"].Value?.ToString()))
                {
                    // Colorer toute la ligne en rouge
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }
    }
}
