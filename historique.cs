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
    public partial class historique : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;
        SqlConnection connection;
        public historique()
        {
            InitializeComponent();
        }

        private void historique_Load(object sender, EventArgs e)
        {
            LoadData3();
            LoadData();
            LoadData1();
            LoadData2();
        }
        private readonly DataTable allData = new DataTable();
        private readonly int limit = 20;
        private int offset = 0;
        private void LoadData()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

              
                    // Exécuter la requête de sélection
                    string query = "SELECT * FROM ChangePF ORDER BY Id DESC  OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
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
        private readonly DataTable allData1 = new DataTable();
        private readonly int limit1 = 20;
        private int offset1 = 0;
        private void LoadData1()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                // Exécuter la requête de sélection
                string query = "SELECT * FROM ChangePRIXPF ORDER BY Id DESC  OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Offset", offset1);
                    command.Parameters.AddWithValue("@Limit", limit1);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable newDataTable = new DataTable();
                    dataAdapter.Fill(newDataTable);

                    allData1.Merge(newDataTable); // Merge the new data with existing data
                    dataGridView2.DataSource = allData1;
                    connection.Close();
                }

            }
        }

        private void dataGridView2_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.SmallIncrement && e.ScrollOrientation == ScrollOrientation.VerticalScroll && e.NewValue >= dataGridView2.Rows.Count - dataGridView2.DisplayedRowCount(true))
            {
                int firstDisplayedRowIndex = dataGridView2.FirstDisplayedScrollingRowIndex;
                offset1 += limit1;
                LoadData1();
                dataGridView2.FirstDisplayedScrollingRowIndex = firstDisplayedRowIndex;
            }
        }
        private readonly DataTable allData2 = new DataTable();
        private readonly int limit2 = 20;
        private int offset2 = 0;
        private void LoadData2()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                // Exécuter la requête de sélection
                string query = "SELECT * FROM ChangePrix_lot ORDER BY Id DESC  OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Offset", offset2);
                    command.Parameters.AddWithValue("@Limit", limit2);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable newDataTable = new DataTable();
                    dataAdapter.Fill(newDataTable);

                    allData2.Merge(newDataTable); // Merge the new data with existing data
                    dataGridView3.DataSource = allData2;
                    connection.Close();
                }

            }
        }

        private void dataGridView3_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.SmallIncrement && e.ScrollOrientation == ScrollOrientation.VerticalScroll && e.NewValue >= dataGridView3.Rows.Count - dataGridView3.DisplayedRowCount(true))
            {
                int firstDisplayedRowIndex = dataGridView3.FirstDisplayedScrollingRowIndex;
                offset2 += limit2;
                LoadData2();
                dataGridView3.FirstDisplayedScrollingRowIndex = firstDisplayedRowIndex;
            }
        }
        private readonly DataTable allData3 = new DataTable();
        private readonly int limit3 = 20;
        private int offset3 = 0;
        private void LoadData3()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                // Exécuter la requête de sélection
                string query = "SELECT * FROM ChangeNCE ORDER BY Id DESC  OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Offset", offset3);
                    command.Parameters.AddWithValue("@Limit", limit3);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable newDataTable = new DataTable();
                    dataAdapter.Fill(newDataTable);

                    allData3.Merge(newDataTable); // Merge the new data with existing data
                    dataGridView4.DataSource = allData3;
                    connection.Close();
                }

            }
        }

        private void dataGridView4_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.SmallIncrement && e.ScrollOrientation == ScrollOrientation.VerticalScroll && e.NewValue >= dataGridView4.Rows.Count - dataGridView4.DisplayedRowCount(true))
            {
                int firstDisplayedRowIndex = dataGridView4.FirstDisplayedScrollingRowIndex;
                offset3 += limit3;
                LoadData3();
                dataGridView4.FirstDisplayedScrollingRowIndex = firstDisplayedRowIndex;
            }

        }
    }
}
