using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static Cogitel_QT.CogitelDataSet;
using System.Configuration;


namespace Cogitel_QT
{
    public partial class docpf : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;
        SqlConnection connection;
       
        public docpf()
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

        private void docpf_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Saisie DOCUMENT";
            textBox1.ForeColor = Color.Gray;
            textBox2.Text = "Saisie OF";
            textBox2.ForeColor = Color.Gray;
            textBox3.Text = "Saisie CATEG";
            textBox3.ForeColor = Color.Gray;
            textBox4.Text = "Saisie CLIENT";
            textBox4.ForeColor = Color.Gray;
            textBox5.Text = "Saisie FAMILLE";
            textBox5.ForeColor = Color.Gray;
            textBox6.Text = "Saisie REF";
            textBox6.ForeColor = Color.Gray;
            textBox7.Text = "Saisie DESIGNTION";
            textBox7.ForeColor = Color.Gray;
            textBox8.Text = "Saisie GRAMMAGE";
            textBox8.ForeColor = Color.Gray;
            textBox9.Text = "Saisie LT";
            textBox9.ForeColor = Color.Gray;
            textBox10.Text = "Saisie LD";
            textBox10.ForeColor = Color.Gray;
            textBox11.Text = "Saisie Nb Poses";
            textBox11.ForeColor = Color.Gray;
            textBox12.Text = "Saisie Nb cylindres";
            textBox12.ForeColor = Color.Gray;
            textBox13.Text = "Saisie PRIX DE VENTE";
            textBox13.ForeColor = Color.Gray;
            textBox14.Text = "Rechercher...";
            textBox14.ForeColor = Color.Black;
            LoadData();
            // Charger les données de la source de données

       
            //Ouvrir une connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //Définir la commande SQL pour récupérer les données de la table PF
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[PF]", connection);

                //Exécuter la commande SQL et récupérer les résultats dans un objet SqlDataReader
                SqlDataReader dr = cmd.ExecuteReader();

                //Créer des collections d'autocomplétion pour chaque TextBox
                AutoCompleteStringCollection collection1 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection collection4 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection collection5 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection collection6 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection collection7 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection collection8 = new AutoCompleteStringCollection();

                //Parcourir chaque ligne de résultats et ajouter les valeurs appropriées aux collections d'autocomplétion
                while (dr.Read())
                {
                    collection1.Add(dr["DOCUMENT"].ToString());
                    collection4.Add(dr["CLIENT"].ToString());
                    collection5.Add(dr["FAMILLE"].ToString());
                    collection6.Add(dr["REF"].ToString());
                    collection7.Add(dr["DESIGNTION"].ToString());
                    
                }
                //Lier chaque collection d'autocomplétion à son TextBox correspondant
                textBox1.AutoCompleteCustomSource = collection1;
                textBox4.AutoCompleteCustomSource = collection4;
                textBox5.AutoCompleteCustomSource = collection5;
                textBox6.AutoCompleteCustomSource = collection6;
                textBox7.AutoCompleteCustomSource = collection7;
                string[] combinedArray = collection1.Cast<string>() .Concat(collection4.Cast<string>()).Concat(collection7.Cast<string>()).ToArray();
              
                textBox14.AutoCompleteCustomSource.AddRange(combinedArray);

                //Fermer le DataReader et la connexion
                dr.Close();
               
            }
            

        }


        private readonly DataTable allData = new DataTable();
        private readonly int limit = 20;
        private int offset = 0;
        private void LoadData()
        {
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Obtenir le nombre total de lignes dans la table PF
                string countQuery = "SELECT COUNT(*) FROM PF";
                SqlCommand countCommand = new SqlCommand(countQuery, connection);
                int rowCount = (int)countCommand.ExecuteScalar();

                // Vérifier si le nombre de lignes dans allData est inférieur ou égal au nombre total de lignes
                if (allData.Rows.Count <= rowCount)
                {
                    // Exécuter la requête de sélection
                    string query = "SELECT * FROM PF ORDER BY id_pf DESC  OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
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
                else
                {
                    MessageBox.Show("Tous les enregistrements ont été récupérés.");
                }
            } }


        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text) || string.IsNullOrEmpty(textBox8.Text) || string.IsNullOrEmpty(textBox9.Text) || string.IsNullOrEmpty(textBox10.Text) || string.IsNullOrEmpty(textBox11.Text) || string.IsNullOrEmpty(textBox12.Text) || string.IsNullOrEmpty(textBox13.Text))
            {
                MessageBox.Show("Veuillez remplir toutes les cases .", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
             
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Vérifier si la valeur de DOCUMENT existe déjà dans la table
                string checkQuery = "SELECT COUNT(*) FROM PF WHERE DOCUMENT = @DOCUMENT";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@DOCUMENT", textBox1.Text);
                int existingCount = (int)checkCommand.ExecuteScalar();

                if (existingCount > 0)
                {
                    MessageBox.Show("Le DOCUMENT existe déjà dans la table.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string query = "INSERT INTO PF (DOCUMENT, NOF, CATEG, CLIENT, FAMILLE, REF, DESIGNTION, GRAMMAGE, LT, LD, Nb_Poses, Nb_cylindres, PRIX_DE_VENTE) VALUES (@valeur1, @valeur2, @valeur3, @valeur4, @valeur5, @valeur6, @valeur7, @valeur8, @valeur9, @valeur10, @valeur11, @valeur12, @valeur13)";
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
                    command.Parameters.AddWithValue("@valeur11", int.Parse(textBox11.Text));
                    command.Parameters.AddWithValue("@valeur12", int.Parse(textBox12.Text));
                    command.Parameters.AddWithValue("@valeur13", float.Parse(textBox13.Text));
                    command.ExecuteNonQuery();
                    connection.Close();
                    allData.Clear();
                    LoadData();
                    textBox1.Text = "Saisie DOCUMENT";
                    textBox1.ForeColor = Color.Gray;
                    textBox2.Text = "Saisie OF";
                    textBox2.ForeColor = Color.Gray;
                    textBox3.Text = "Saisie CATEG";
                    textBox3.ForeColor = Color.Gray;
                    textBox4.Text = "Saisie CLIENT";
                    textBox4.ForeColor = Color.Gray;
                    textBox5.Text = "Saisie FAMILLE";
                    textBox5.ForeColor = Color.Gray;
                    textBox6.Text = "Saisie REF";
                    textBox6.ForeColor = Color.Gray;
                    textBox7.Text = "Saisie DESIGNTION";
                    textBox7.ForeColor = Color.Gray;
                    textBox8.Text = "Saisie GRAMMAGE";
                    textBox8.ForeColor = Color.Gray;
                    textBox9.Text = "Saisie LT";
                    textBox9.ForeColor = Color.Gray;
                    textBox10.Text = "Saisie LD";
                    textBox10.ForeColor = Color.Gray;
                    textBox11.Text = "Saisie Nb Poses";
                    textBox11.ForeColor = Color.Gray;
                    textBox12.Text = "Saisie Nb cylindres";
                    textBox12.ForeColor = Color.Gray;
                    textBox13.Text = "Saisie PRIX DE VENTE";
                    textBox13.ForeColor = Color.Gray;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedRows.Count > 0 && !textBox1.Text.Contains("Saisie DOCUMENT") && !textBox2.Text.Contains("Saisie OF") && !textBox3.Text.Contains("Saisie CATEG") && !textBox4.Text.Contains("Saisie CLIENT") && !textBox5.Text.Contains("Saisie FAMILLE") && !textBox6.Text.Contains("Saisie REF") && !textBox7.Text.Contains("Saisie DESIGNTION") && !textBox8.Text.Contains("Saisie GRAMMAGE") && !textBox9.Text.Contains("Saisie LT") && !textBox10.Text.Contains("Saisie LD") && !textBox11.Text.Contains("Saisie Nb Poses") && !textBox12.Text.Contains("Saisie Nb cylindres") && !textBox13.Text.Contains("Saisie PRIX DE VENTE"))
            {
                DialogResult dialogResult = MessageBox.Show("Voulez-vous vraiment modifier cette ligne ?", "Confirmation de modification", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    connection = new SqlConnection(connectionString);
                    // Mettre à jour les données dans la base de données pour la ligne modifiée
                    string document = textBox1.Text;
                    string of = textBox2.Text;
                    string categ = textBox3.Text;
                    string client = textBox4.Text;
                    string famille = textBox5.Text;
                    string reference = textBox6.Text;
                    string designation = textBox7.Text;
                    float grammage = float.Parse(textBox8.Text);
                    float lt = float.Parse(textBox9.Text);
                    float ld = float.Parse(textBox10.Text);
                    int nb_poses = int.Parse(textBox11.Text);
                    int nb_cylindres = int.Parse(textBox12.Text);
                    float prix_vente = float.Parse(textBox13.Text);

                    // Récupérer l'id de la ligne sélectionnée dans le DataGridView
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_pf"].Value);

                    connection.Open();
                    string updateQuery = "UPDATE [PF] SET [DOCUMENT] = @document, [NOF] = @of, [CATEG] = @categ, [CLIENT] = @client, [FAMILLE] = @famille, [REF] = @reference, [DESIGNTION] = @designation, [GRAMMAGE] = @grammage, [LT] = @lt, [LD] = @ld, [Nb_Poses] = @nb_poses, [Nb_cylindres] = @nb_cylindres, [PRIX_DE_VENTE] = @prix_vente WHERE [id_pf] = @id";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@document", document);
                    updateCommand.Parameters.AddWithValue("@of", of);
                    updateCommand.Parameters.AddWithValue("@categ", categ);
                    updateCommand.Parameters.AddWithValue("@client", client);
                    updateCommand.Parameters.AddWithValue("@famille", famille);
                    updateCommand.Parameters.AddWithValue("@reference", reference);
                    updateCommand.Parameters.AddWithValue("@designation", designation);
                    updateCommand.Parameters.AddWithValue("@grammage", grammage);
                    updateCommand.Parameters.AddWithValue("@lt", lt);
                    updateCommand.Parameters.AddWithValue("@ld", ld);
                    updateCommand.Parameters.AddWithValue("@nb_poses", nb_poses);
                    updateCommand.Parameters.AddWithValue("@nb_cylindres", nb_cylindres);
                    updateCommand.Parameters.AddWithValue("@prix_vente", prix_vente);
                    updateCommand.Parameters.AddWithValue("@id", id);
                    updateCommand.ExecuteNonQuery();
                    connection.Close();
                    allData.Clear();
                    LoadData();
                    MessageBox.Show("Les données ont été modifiées avec succès .");
                    textBox1.Text = "Saisie DOCUMENT";
                    textBox1.ForeColor = Color.Gray;
                    textBox2.Text = "Saisie OF";
                    textBox2.ForeColor = Color.Gray;
                    textBox3.Text = "Saisie CATEG";
                    textBox3.ForeColor = Color.Gray;
                    textBox4.Text = "Saisie CLIENT";
                    textBox4.ForeColor = Color.Gray;
                    textBox5.Text = "Saisie FAMILLE";
                    textBox5.ForeColor = Color.Gray;
                    textBox6.Text = "Saisie REF";
                    textBox6.ForeColor = Color.Gray;
                    textBox7.Text = "Saisie DESIGNTION";
                    textBox7.ForeColor = Color.Gray;
                    textBox8.Text = "Saisie GRAMMAGE";
                    textBox8.ForeColor = Color.Gray;
                    textBox9.Text = "Saisie LT";
                    textBox9.ForeColor = Color.Gray;
                    textBox10.Text = "Saisie LD";
                    textBox10.ForeColor = Color.Gray;
                    textBox11.Text = "Saisie Nb Poses";
                    textBox11.ForeColor = Color.Gray;
                    textBox12.Text = "Saisie Nb cylindres";
                    textBox12.ForeColor = Color.Gray;
                    textBox13.Text = "Saisie PRIX DE VENTE";
                    textBox13.ForeColor = Color.Gray;
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
                    textBox12.Multiline = false;
                    textBox13.Multiline = false;
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
                int idToDelete = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_pf"].Value); // Remplacer "Id" par le nom de votre colonne contenant l'identifiant unique

                // Demander une confirmation avant de supprimer
                DialogResult dialogResult = MessageBox.Show("Voulez-vous vraiment supprimer cette ligne ?", "Confirmation de suppression", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Supprimer la ligne correspondante de la base de données
                    string query = "DELETE FROM PF WHERE id_pf = @id"; // Remplacer "MaTable" par le nom de votre table et "Id" par le nom de votre colonne d'identifiant unique
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
            if (textBox1.Text == "Saisie DOCUMENT")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Saisie DOCUMENT";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Saisie OF")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Saisie OF";
                textBox2.ForeColor = Color.Gray;
            }
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Saisie CATEG")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Saisie CATEG";
                textBox3.ForeColor = Color.Gray;
            }
        }
        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Saisie CLIENT")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.Black;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Saisie CLIENT";
                textBox4.ForeColor = Color.Gray;
            }
        }
        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Saisie FAMILLE")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.Black;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "Saisie FAMILLE";
                textBox5.ForeColor = Color.Gray;
            }
        }
        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Saisie REF")
            {
                textBox6.Text = "";
                textBox6.ForeColor = Color.Black;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = "Saisie REF";
                textBox6.ForeColor = Color.Gray;
            }
        }
        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "Saisie DESIGNTION")
            {
                textBox7.Text = "";
                textBox7.ForeColor = Color.Black;
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                textBox7.Text = "Saisie DESIGNTION";
                textBox7.ForeColor = Color.Gray;
            }
        }
        private void textBox8_Enter(object sender, EventArgs e)
        {
            if (textBox8.Text == "Saisie GRAMMAGE")
            {
                textBox8.Text = "";
                textBox8.ForeColor = Color.Black;
            }
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox8.Text))
            {
                textBox8.Text = "Saisie GRAMMAGE";
                textBox8.ForeColor = Color.Gray;
            }
            else if (!float.TryParse(textBox8.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox8.Text = "Saisie GRAMMAGE";
                textBox8.ForeColor = Color.Gray;
            }
        }
        private void textBox9_Enter(object sender, EventArgs e)
        {
            if (textBox9.Text == "Saisie LT")
            {
                textBox9.Text = "";
                textBox9.ForeColor = Color.Black;
            }
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox9.Text))
            {
                textBox9.Text = "Saisie LT";
                textBox9.ForeColor = Color.Gray;
            }

            else if (!float.TryParse(textBox9.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox9.Text = "Saisie LT";
                textBox9.ForeColor = Color.Gray;
            }
        }
        private void textBox10_Enter(object sender, EventArgs e)
        {
            if (textBox10.Text == "Saisie LD")
            {
                textBox10.Text = "";
                textBox10.ForeColor = Color.Black;
            }
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox10.Text))
            {
                textBox10.Text = "Saisie LD";
                textBox10.ForeColor = Color.Gray;
            }


            else if (!float.TryParse(textBox10.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox10.Text = "Saisie LD";
                textBox10.ForeColor = Color.Gray;
            }

        }
        private void textBox11_Enter(object sender, EventArgs e)
        {
            if (textBox11.Text == "Saisie Nb Poses")
            {
                textBox11.Text = "";
                textBox11.ForeColor = Color.Black;
            }
        }

        private void textBox11_Leave(object sender, EventArgs e)
        {
            int result;
            if (string.IsNullOrEmpty(textBox11.Text))
            {
                textBox11.Text = "Saisie Nb Poses";
                textBox11.ForeColor = Color.Gray;
            }
            else if (!int.TryParse(textBox11.Text, out result))
            {
                MessageBox.Show("Vous devez entrer un nombre .");
                // Le code à exécuter si la condition est fausse
                textBox11.Text = "Saisie Nb Poses";
                textBox11.ForeColor = Color.Gray;
            }
        }
        private void textBox12_Enter(object sender, EventArgs e)
        {
            if (textBox12.Text == "Saisie Nb cylindres")
            {
                textBox12.Text = "";
                textBox12.ForeColor = Color.Black;
            }
        }

        private void textBox12_Leave(object sender, EventArgs e)
        {
            int result;
            if (string.IsNullOrEmpty(textBox12.Text))
            {
                textBox12.Text = "Saisie Nb cylindres";
                textBox12.ForeColor = Color.Gray;
            }
            else
            {
                if (!int.TryParse(textBox12.Text, out result))
                {
                    MessageBox.Show("Vous devez entrer un nombre .");
                    // Le code à exécuter si la condition est fausse
                    textBox12.Text = "Saisie Nb cylindres";
                    textBox12.ForeColor = Color.Gray;
                }
            }
        }
        private void textBox13_Enter(object sender, EventArgs e)
        {
            if (textBox13.Text == "Saisie PRIX DE VENTE")
            {
                textBox13.Text = "";
                textBox13.ForeColor = Color.Black;
            }
        }

        private void textBox13_Leave(object sender, EventArgs e)
        {
            float result;
            if (string.IsNullOrEmpty(textBox13.Text))
            {
                textBox13.Text = "Saisie PRIX DE VENTE";
                textBox13.ForeColor = Color.Gray;
            }
            else
            {
                if (!float.TryParse(textBox13.Text, out result))
                {
                    MessageBox.Show("Vous devez entrer un nombre .");
                    // Le code à exécuter si la condition est fausse
                    textBox13.Text = "Saisie PRIX DE VENTE";
                    textBox13.ForeColor = Color.Gray;
                }
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void docpf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // Simule un clic sur le bouton "Supprimer"
                button3.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                textBox1.Text = "Saisie DOCUMENT";
                textBox1.ForeColor = Color.Gray;
                textBox2.Text = "Saisie OF";
                textBox2.ForeColor = Color.Gray;
                textBox3.Text = "Saisie CATEG";
                textBox3.ForeColor = Color.Gray;
                textBox4.Text = "Saisie CLIENT";
                textBox4.ForeColor = Color.Gray;
                textBox5.Text = "Saisie FAMILLE";
                textBox5.ForeColor = Color.Gray;
                textBox6.Text = "Saisie REF";
                textBox6.ForeColor = Color.Gray;
                textBox7.Text = "Saisie DESIGNTION";
                textBox7.ForeColor = Color.Gray;
                textBox8.Text = "Saisie GRAMMAGE";
                textBox8.ForeColor = Color.Gray;
                textBox9.Text = "Saisie LT";
                textBox9.ForeColor = Color.Gray;
                textBox10.Text = "Saisie LD";
                textBox10.ForeColor = Color.Gray;
                textBox11.Text = "Saisie Nb Poses";
                textBox11.ForeColor = Color.Gray;
                textBox12.Text = "Saisie Nb cylindres";
                textBox12.ForeColor = Color.Gray;
                textBox13.Text = "Saisie PRIX DE VENTE";
                textBox13.ForeColor = Color.Gray;
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
                textBox12.Multiline = false;
                textBox13.Multiline = false;

            }
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {

        }




        private void button5_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection("Data Source=AHMED-MLIK;Initial Catalog=Cogitel;Integrated Security=True");
            string searchTerm = textBox14.Text;
            string sql = "SELECT * FROM PF WHERE DOCUMENT = '" + searchTerm + "' OR CLIENT  = '" + searchTerm + "'OR DESIGNTION  = '" + searchTerm + "'OR FAMILLE  = '" + searchTerm + "'";
            using (var adapter = new SqlDataAdapter(sql, connection))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }

            connection.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox14.Text = "Rechercher..."; // réinitialise le contenu du texte de recherche
            textBox14.ForeColor = Color.Black;
            dataGridView1.DataSource = allData;

        }

        private void textBox14_Enter(object sender, EventArgs e)
        {
            if (textBox14.Text == "Rechercher...")
            {
                textBox14.Text = "";
                textBox14.ForeColor = Color.Black;
            }
        }

        private void textBox14_Leave(object sender, EventArgs e)
        {
            if (textBox14.Text == "")
            {
                textBox14.Text = "Rechercher...";
                textBox14.ForeColor = Color.Black;
            }
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

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
