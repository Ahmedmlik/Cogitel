using DocumentFormat.OpenXml.InkML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cogitel_QT
{
    public partial class notif : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;
        SqlConnection connection;
        public notif()
        {
            InitializeComponent();
          
        }


        
        private void notif_Load(object sender, EventArgs e)
        {
            string query = "SELECT  N_de_la_NC, Client, F70  FROM NCE WHERE Date_réponse_client IS NULL ORDER BY F70 DESC ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Créer un SqlDataAdapter et un DataTable pour stocker les résultats de la requête SQL
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                // Remplir le DataTable avec les résultats de la requête SQL
                adapter.Fill(dataTable);

                // Assigner le DataTable à la propriété DataSource du DataGridView
                dataGridView1.DataSource = dataTable;

                // Définir les noms de colonnes dans le DataGridView
                dataGridView1.Columns["N_de_la_NC"].HeaderText = "N° de la NC";
                dataGridView1.Columns["Client"].HeaderText = "Client";
                dataGridView1.Columns["F70"].HeaderText = "Temps d'attente";
                // Fermer la connexion à la base de données
                dataGridView1.Columns["F70"].DefaultCellStyle.Format = "0 'jours'";
                connection.Close();
            }

        }
    }
}
