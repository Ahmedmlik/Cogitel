using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using System.Configuration;
using System.Net.Sockets;



namespace Cogitel_QT
{
    public partial class Formlogin : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;
        SqlConnection connection;
        public Formlogin()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Formlogin_KeyPress);
        }

        private void buttonloginquitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonlogin_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Nom d'utilisateur" || textBox2.Text == "Mot de passe")
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                SHA256 sha256 = SHA256.Create();
                
                // Get the plaintext password from the textbox
                string password = textBox2.Text;

                // Convert the plaintext password to a byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash value of the password byte array
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hash value byte array to a string
                string hashedPassword = Convert.ToBase64String(hashBytes);

                Connexion con = new Connexion();
                con.dataGet("Select * from [Login] Where Utilisateur = '" + textBox1.Text + "' and MotdePasse  = '" + hashedPassword + "'");
                DataTable dt = new DataTable();
                con.sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    
                    if (textBox1.Text == "admin")
                    {
                        // Get the instance of the form where the buttons are located
                        cogitel cogitel = new cogitel();

                        // Hide the buttons
                        cogitel.SetButtonVisible(false);
                        cogitel.ButtonsVisibleget = false;
                        cogitel.Show();
                        this.Hide();
                    }
                    else
                    {
                        cogitel cogitel = new cogitel();
                        cogitel.ButtonsVisibleget = true;
                        // Affiche le formulaire
                        cogitel.Show();

                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Utilisateur ou Mot de passe incorrect ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Text = "";
                    textBox1.Text = "";
                    textBox1.Text = "Nom d'utilisateur";
                    textBox1.ForeColor = Color.Gray;
                    textBox2.Text = "Mot de passe";
                    textBox2.ForeColor = Color.Gray;
                    textBox2.UseSystemPasswordChar = false;

                    textBox1.BackColor = Color.White;
                    panel2.BackColor = Color.White;
                    panel3.BackColor = SystemColors.Control;
                    textBox2.BackColor = SystemColors.Control;
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            textBox1.Text = "Nom d'utilisateur";
            textBox1.ForeColor = Color.Gray;
            textBox2.Text = "Mot de passe";
            textBox2.ForeColor = Color.Gray;
            textBox2.UseSystemPasswordChar = false;
        
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            // Requête SQL pour mettre à jour la colonne
            string sqlQuery = "UPDATE NCE SET F70 = DATEDIFF(day, Date_de_réclamtion, GETDATE())";

            // Exécution de la requête SQL
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            int rowsAffected = command.ExecuteNonQuery();
            string sqlQuery3 = "SELECT LastEmailSent FROM EmailConfig WHERE id_email = 1";
            SqlCommand command3 = new SqlCommand(sqlQuery3, connection);
            object result = command3.ExecuteScalar();
            DateTime lastEmailSent = result != null && result != DBNull.Value ? Convert.ToDateTime(result) : DateTime.MinValue;

            // Vérifier si la différence de dates est supérieure à 1 jour
            if ((DateTime.Now - lastEmailSent).TotalDays >= 1)
            {
                if (rowsAffected > 0)
                {
                    // Vérifier si la différence de dates est égale à 5 jours
                    string sqlQuery2 = "SELECT n.F70, n.N_de_la_NC, n.Responsables, n.Client, e.email " +
                                       "FROM NCE n " +
                                       "JOIN Email e ON n.Responsables = e.Nom " +
                                       "WHERE n.Date_réponse_client IS NULL";
                    SqlCommand command2 = new SqlCommand(sqlQuery2, connection);
                    SqlDataReader reader = command2.ExecuteReader();

                    DateTime lastEmailDate = new DateTime(); // Initialiser la variable pour la première exécution
                    bool emailSent = false;
                    while (reader.Read())
                    {
                        int differenceDates = Convert.ToInt32(reader["F70"]);
                        string numNonConformite = reader["N_de_la_NC"].ToString();
                        string email = reader["email"].ToString();
                        string Client = reader["Client"].ToString();
                        if (differenceDates == 5)
                        {
                            

                            // Vérifier si la dernière date d'envoi est différente de la date actuelle
                            if (lastEmailDate.Date != DateTime.Now.Date)
                            {
                                // Envoyer un e-mail
                                string from = "Cogitel_Conformite@outlook.fr";
                                string to = email;
                                string subject = "Rappel de réclamation";
                                string body = "Bonjour,\n\nNous souhaitons vous rappeler que vous avez une réclamation N° de non conformité : " + numNonConformite + " Client : " + Client + " en attente de réponse depuis 5 jours.\n\nCordialement,\n Société Cogitel";
                                string smtpHost = "smtp.office365.com";
                                int smtpPort = 587;
                                string smtpUsername = "Cogitel_Conformite@outlook.fr";
                                string smtpPassword = "AZERTY123+";

                                SmtpClient client = new SmtpClient(smtpHost, smtpPort);
                                client.EnableSsl = true;
                                
                                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                                Console.WriteLine("messdae envoyer");
                                MailMessage message = new MailMessage(from, to, subject, body);
                                string localIP = "";
                                foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
                                {
                                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                                    {
                                        localIP = ip.ToString();
                                        break;
                                    }
                                }

                                // Ajout de l'en-tête X-Originating-IP avec l'adresse IP locale
                                message.Headers.Add("X-Originating-IP", localIP);
                                message.Headers.Add("X-Mailer", "Microsoft Outlook");
                                message.Headers.Add("X-Sender", "Cogitel_Conformite@outlook.fr");
                                message.Headers.Add("X-Antispam", "none");
                                message.Headers.Add("X-Antivirus", "none");
                                client.Send(message);
                                emailSent = true;


                                Thread.Sleep(500);

                            }
                           
                            // Fermeture de la connexion
                        }
                    }

                    reader.Close();
                    if (emailSent)
                    {
                        string sqlQuery4 = "UPDATE EmailConfig SET LastEmailSent = @lastEmailSent WHERE id_email = 1";
                        SqlCommand command4 = new SqlCommand(sqlQuery4, connection);
                        command4.Parameters.AddWithValue("@lastEmailSent", DateTime.Now);
                        command4.ExecuteNonQuery();
                    }
                }
            }
            
           
            connection.Close();
        }




            private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
                textBox2.BackColor = Color.White;
                panel3.BackColor = Color.White;
                textBox1.BackColor = SystemColors.Control;
                panel2.BackColor = SystemColors.Control;

            }
            else
            {
                textBox1.Focus();
                textBox1.BackColor = Color.White;
                panel2.BackColor = Color.White;
                panel3.BackColor = SystemColors.Control;
                textBox2.BackColor = SystemColors.Control;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonlogin.Focus();
            }
            else
            {
                textBox2.Focus();
                textBox2.BackColor = Color.White;
                panel3.BackColor = Color.White;
                textBox1.BackColor = SystemColors.Control;
                panel2.BackColor = SystemColors.Control;
            }
        }

        private void Formlogin_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {

                // Empêcher la génération de bip sonore lors de la frappe sur la touche Entrée
                e.Handled = true;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = SystemColors.Control;
            textBox2.BackColor = SystemColors.Control;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            textBox1.BackColor = SystemColors.Control;
            panel2.BackColor = SystemColors.Control;
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Nom d'utilisateur")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black ;
            }


        }



        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Nom d'utilisateur";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Mot de passe")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Mot de passe";
                textBox2.ForeColor = Color.Gray;
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.pictureBox3, "Afficher mot de passe");
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void buttonlogin_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void buttonlogin_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void buttonloginquitter_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void buttonloginquitter_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void Formlogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox1.Focus();


            }

        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
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

        private void button3_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
    }

}
