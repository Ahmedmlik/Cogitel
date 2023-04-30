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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Configuration;

namespace Cogitel_QT
{
    public partial class Thèmes : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;
        SqlConnection connection;
        public Thèmes()
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

        private void Thèmes_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'cogitelDataSet.Thèmes'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.thèmesTableAdapter.Fill(this.cogitelDataSet.Thèmes);
            textBox1.Text = "Saisie Défaut";
            textBox1.ForeColor = Color.Gray;
            textBox2.Text = "Saisie Théme";
            textBox2.ForeColor = Color.Gray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Veuillez remplir toutes les cases .", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string query = "INSERT INTO Thèmes (Défaut,Thème) VALUES (@valeur1, @valeur2)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@valeur1", textBox1.Text);
                command.Parameters.AddWithValue("@valeur2", textBox2.Text);

                command.ExecuteNonQuery();
                textBox1.Text = "Saisie Défaut";
                textBox1.ForeColor = Color.Gray;
                textBox2.Text = "Saisie Théme";
                textBox2.ForeColor = Color.Gray;

            }

            // TODO: cette ligne de code charge les données dans la table 'cogitelDataSet.Thèmes'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.thèmesTableAdapter.Fill(this.cogitelDataSet.Thèmes);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && !textBox1.Text.Contains("Saisie Défaut") && !textBox2.Text.Contains("Saisie Théme"))
            {
                DialogResult dialogResult = MessageBox.Show("Voulez-vous vraiment modifier cette ligne ?", "Confirmation de suppression", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    connection = new SqlConnection(connectionString);
                    // Mettre à jour les données dans la base de données pour la ligne modifiée
                    string defaut = textBox1.Text;
                    string théme = textBox2.Text;

                    // Récupérer l'id de la ligne sélectionnée dans le DataGridView
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["idthèmesDataGridViewTextBoxColumn"].Value);

                    connection.Open();
                    string updateQuery = "UPDATE Thèmes SET Défaut = @defaut, Thème = @théme WHERE id_thèmes = @id";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@defaut", defaut);
                    updateCommand.Parameters.AddWithValue("@théme", théme);
                    updateCommand.Parameters.AddWithValue("@id", id);
                    updateCommand.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Les données ont été modifiées avec succès .");
                    textBox1.Text = "Saisie Défaut";
                    textBox1.ForeColor = Color.Gray;
                    textBox2.Text = "Saisie Théme";
                    textBox2.ForeColor = Color.Gray;
                    this.thèmesTableAdapter.Fill(this.cogitelDataSet.Thèmes);
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
                int idToDelete = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["idthèmesDataGridViewTextBoxColumn"].Value); // Remplacer "Id" par le nom de votre colonne contenant l'identifiant unique

                // Demander une confirmation avant de supprimer
                DialogResult dialogResult = MessageBox.Show("Voulez-vous vraiment supprimer cette ligne ?", "Confirmation de suppression", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Supprimer la ligne correspondante de la base de données
                    string query = "DELETE FROM Thèmes WHERE id_thèmes = @id"; // Remplacer "MaTable" par le nom de votre table et "Id" par le nom de votre colonne d'identifiant unique
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
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Saisie Défaut")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Saisie Défaut";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Saisie Théme")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Saisie Théme";
                textBox2.ForeColor = Color.Gray;
            }
        }
      
        private void advancedDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                textBox1.Text = selectedRow.Cells["défautDataGridViewTextBoxColumn"].Value.ToString();
                textBox1.ForeColor = Color.Black;
                textBox2.Text = selectedRow.Cells["thèmeDataGridViewTextBoxColumn"].Value.ToString();
                textBox2.ForeColor = Color.Black;

            }
        }

        private void Thèmes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // Simule un clic sur le bouton "Supprimer"
                button3.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                textBox1.Text = "Saisie Défaut";
                textBox1.ForeColor = Color.Gray;
                textBox2.Text = "Saisie Théme";
                textBox2.ForeColor = Color.Gray;
            }
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
