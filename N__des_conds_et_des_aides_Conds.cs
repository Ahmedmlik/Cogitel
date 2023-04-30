using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Configuration;


namespace Cogitel_QT
{
    public partial class N__des_conds_et_des_aides_Conds : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;
        SqlConnection connection;
        public N__des_conds_et_des_aides_Conds()
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
                Type tlpType = tableLayoutPanel1.GetType();
                PropertyInfo pi = tlpType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(tableLayoutPanel1, true, null);
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
        }

        private void N__des_conds_et_des_aides_Conds_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'cogitelDataSet.N__des_conds_et_des_aides_Conds'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.n__des_conds_et_des_aides_CondsTableAdapter1.Fill(this.cogitelDataSet.N__des_conds_et_des_aides_Conds);
            
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from N__des_conds_et_des_aides_Conds ", connection);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            AutoCompleteStringCollection collection1 = new AutoCompleteStringCollection();
            AutoCompleteStringCollection collection2 = new AutoCompleteStringCollection();
            AutoCompleteStringCollection collection3 = new AutoCompleteStringCollection();
            while (dr.Read())
            {
                collection.Add(dr["Ndéquipe"].ToString());
                collection1.Add(dr["Matricule"].ToString());
                collection2.Add(dr["NOMETPRENOM"].ToString());
                collection3.Add(dr["SECTION"].ToString());
            }

            textBox1.AutoCompleteCustomSource = collection;
            textBox2.AutoCompleteCustomSource = collection1;
            textBox3.AutoCompleteCustomSource = collection2;
            textBox4.AutoCompleteCustomSource = collection3;
            dr.Close();
            connection.Close();
            textBox1.Text = "Saisie N° d'équipe";
            textBox1.ForeColor = Color.Gray;
            textBox2.Text = "Saisie Matricule";
            textBox2.ForeColor = Color.Gray;
            textBox3.Text = "Saisie Nom et Prenom";
            textBox3.ForeColor = Color.Gray;
            textBox4.Text = "Saisie Section";
            textBox4.ForeColor = Color.Gray;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Veuillez remplir toutes les cases.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Vérifier si la valeur de Matricule existe déjà dans la table
                string checkQuery = "SELECT COUNT(*) FROM N__des_conds_et_des_aides_Conds WHERE Matricule = @matricule";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@matricule", textBox2.Text);
                int existingCount = (int)checkCommand.ExecuteScalar();

                if (existingCount > 0)
                {
                    MessageBox.Show("Le matricule existe déjà dans la table.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Insérer les données si la valeur de Matricule n'existe pas encore dans la table
                    string insertQuery = "INSERT INTO N__des_conds_et_des_aides_Conds (Ndéquipe, Matricule, NOMETPRENOM, SECTION) VALUES (@valeur1, @valeur2, @valeur3, @valeur4)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@valeur1", textBox1.Text);
                    insertCommand.Parameters.AddWithValue("@valeur2", textBox2.Text);
                    insertCommand.Parameters.AddWithValue("@valeur3", textBox3.Text);
                    insertCommand.Parameters.AddWithValue("@valeur4", textBox4.Text);
                    insertCommand.ExecuteNonQuery();

                    textBox1.Text = "Saisie N° d'équipe";
                    textBox1.ForeColor = Color.Gray;
                    textBox2.Text = "Saisie Matricule";
                    textBox2.ForeColor = Color.Gray;
                    textBox3.Text = "Saisie Nom et Prenom";
                    textBox3.ForeColor = Color.Gray;
                    textBox4.Text = "Saisie Section";
                    textBox4.ForeColor = Color.Gray;

                    // Recharger les données dans la table
                    this.n__des_conds_et_des_aides_CondsTableAdapter1.Fill(this.cogitelDataSet.N__des_conds_et_des_aides_Conds);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && !textBox1.Text.Contains("Saisie N° d'équipe") && !textBox2.Text.Contains("Saisie Matricule") && !textBox3.Text.Contains("Saisie Nom et Prenom") && !textBox4.Text.Contains("Saisie Section"))
            {
                DialogResult dialogResult = MessageBox.Show("Voulez-vous vraiment modifier cette ligne ?", "Confirmation de suppression", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                        connection = new SqlConnection(connectionString);
                        // Mettre à jour les données dans la base de données pour la ligne modifiée
                        string ndEquipe = textBox1.Text;
                        string matricule = textBox2.Text;
                        string nomEtPrenom = textBox3.Text;
                        string section = textBox4.Text;

                        // Récupérer l'id de la ligne sélectionnée dans le DataGridView
                     
                        int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                          

                        connection.Open();
                        string updateQuery = "UPDATE N__des_conds_et_des_aides_Conds SET Ndéquipe = @ndEquipe, Matricule = @matricule, NOMETPRENOM = @nomEtPrenom, SECTION = @section WHERE id = @id";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@ndEquipe", ndEquipe);
                        updateCommand.Parameters.AddWithValue("@matricule", matricule);
                        updateCommand.Parameters.AddWithValue("@nomEtPrenom", nomEtPrenom);
                        updateCommand.Parameters.AddWithValue("@section", section);
                        updateCommand.Parameters.AddWithValue("@id", id);
                        updateCommand.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Les données ont été modifiées avec succès .");
                        // Crée une nouvelle instance du formulaire
                        // TODO: cette ligne de code charge les données dans la table 'cogitelDataSet.N__des_conds_et_des_aides_Conds'. Vous pouvez la déplacer ou la supprimer selon les besoins.
                        this.n__des_conds_et_des_aides_CondsTableAdapter1.Fill(this.cogitelDataSet.N__des_conds_et_des_aides_Conds);
                    
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
                int idToDelete = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["idDataGridViewTextBoxColumn"].Value); // Remplacer "Id" par le nom de votre colonne contenant l'identifiant unique

                // Demander une confirmation avant de supprimer
                DialogResult dialogResult = MessageBox.Show("Voulez-vous vraiment supprimer cette ligne ?", "Confirmation de suppression", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Supprimer la ligne correspondante de la base de données
                    string query = "DELETE FROM N__des_conds_et_des_aides_Conds WHERE id = @id"; // Remplacer "MaTable" par le nom de votre table et "Id" par le nom de votre colonne d'identifiant unique
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

        private void textBox1_Leave(object sender, EventArgs e)
        {
            int result;

             if (string.IsNullOrEmpty(textBox1.Text))
            {
                    textBox1.Text = "Saisie N° d'équipe";
                    textBox1.ForeColor = Color.Gray;
                }
            else
            {
                if (!int.TryParse(textBox1.Text, out result))
                {
                    MessageBox.Show("Vous devez entrer un nombre entier.");
                    // Le code à exécuter si la condition est fausse
                    textBox1.Text = "Saisie N° d'équipe";
                    textBox1.ForeColor = Color.Gray;
                }
            }
            
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Saisie N° d'équipe")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Saisie Matricule")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            int result;

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Text = "Saisie Matricule";
                textBox2.ForeColor = Color.Gray;
            }
            else
            {
                if (!int.TryParse(textBox2.Text, out result))
                {
                    MessageBox.Show("Vous devez entrer un nombre entier.");
                    // Le code à exécuter si la condition est fausse
                    textBox2.Text = "Saisie Matricule";
                    textBox2.ForeColor = Color.Gray;
                }
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Saisie Nom et Prenom")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Saisie Nom et Prenom";
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Saisie Section")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.Black;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Saisie Section";
                textBox4.ForeColor = Color.Gray;
            }
        }

        

        private void advancedDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                textBox1.Text = selectedRow.Cells["ndéquipeDataGridViewTextBoxColumn"].Value.ToString();
                textBox1.ForeColor = Color.Black;
                textBox2.Text = selectedRow.Cells["matriculeDataGridViewTextBoxColumn"].Value.ToString();
                textBox2.ForeColor = Color.Black;
                textBox3.Text = selectedRow.Cells["nOMETPRENOMDataGridViewTextBoxColumn"].Value.ToString();
                textBox3.ForeColor = Color.Black;
                textBox4.Text = selectedRow.Cells["sECTIONDataGridViewTextBoxColumn"].Value.ToString();
                textBox4.ForeColor = Color.Black;
            }
        }


        private void N__des_conds_et_des_aides_Conds_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // Simule un clic sur le bouton "Supprimer"
                button3.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                textBox1.Text = "Saisie N° d'équipe";
                textBox1.ForeColor = Color.Gray;
                textBox2.Text = "Saisie Matricule";
                textBox2.ForeColor = Color.Gray;
                textBox3.Text = "Saisie Nom et Prenom";
                textBox3.ForeColor = Color.Gray;
                textBox4.Text = "Saisie Section";
                textBox4.ForeColor = Color.Gray;
              
            }
        }
        public  void SetButtonVisible(bool isVisible)
        {
            // Définissez la propriété Visible du bouton en fonction de la valeur booléenne passée en paramètre
            button3.Visible = isVisible;
            button2.Visible = isVisible;
            button1.Visible = isVisible;
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button4_MouseHover_1(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button4_MouseLeave_1(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
    }
}
