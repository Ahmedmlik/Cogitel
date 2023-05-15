using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Cogitel_QT
{
    public partial class cogitel : Form
    {

        string connectionString = ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString;
        public static cogitel Instance;
        public cogitel()
        {
            InitializeComponent();
            timer1.Start();
            customizeDesing();
            customizeDesing1();
            Instance = this;
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type panelType = panelslidemenu.GetType();
                PropertyInfo pi = panelType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(panelslidemenu, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type panelType = panelutilisateurmenu.GetType();
                PropertyInfo pi = panelType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(panelutilisateurmenu, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type buttonType = button2.GetType();
                PropertyInfo pi = buttonType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(button2, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type buttonType = button4.GetType();
                PropertyInfo pi = buttonType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(button4, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type buttonType = button6.GetType();
                PropertyInfo pi = buttonType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(button6, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type buttonType = button7.GetType();
                PropertyInfo pi = buttonType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(button7, true, null);
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type buttonType = buttonutili.GetType();
                PropertyInfo pi = buttonType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(buttonutili, true, null);
            }
        }
        private void customizeDesing()
        {
            panelutilisateurmenu.Visible = false;
        }
        private void hidepanelutilisateurmenu()
        {
            if (panelutilisateurmenu.Visible == true)
            {
                panelutilisateurmenu.Visible = false;
                panelRF.Visible = false;

            }


        }
        private void hidepanelRF()
        {
            if (panelutilisateurmenu.Visible == true)
            {
                panelRF.Visible = false;
              
            }


        }
        private void showpanelutilisateurmenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hidepanelutilisateurmenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        private void customizeDesing1()
        {
            panelRF.Visible = false;
        }

        private void showpanelRF(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hidepanelRF();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;


        }

        private bool isClosing = false; // Ajouter une variable pour suivre l'état de fermeture
        private void cogitel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClosing) // Vérifier si ce n'est pas une fermeture programmée
            {
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir quitter ?", "Quitter", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Annuler la fermeture du formulaire
                }
            }
        }

        private void buttonutili_Click(object sender, EventArgs e)
        {
            showpanelutilisateurmenu(panelutilisateurmenu);
        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        private modifiermotdepasse formInstance1 = null;
        private void button2_Click(object sender, EventArgs e)
        {
            if (formInstance1 == null)
            {
                formInstance1 = new modifiermotdepasse();
                formInstance1.FormClosed += (s, args) => formInstance1 = null;
            }

            formInstance1.MdiParent = this;
            formInstance1.Dock = DockStyle.Fill;
            formInstance1.Show();
            formInstance1.BringToFront();
        }




        private Erc formInstance5 = null;
        private void Erc_Click(object sender, EventArgs e)
        {

            if (formInstance5 == null)
            {
                formInstance5 = new Erc();
                formInstance5.FormClosed += (s, args) => formInstance5 = null;
            }
            formInstance5.MdiParent = this;
            formInstance5.Dock = DockStyle.Fill;
            formInstance5.WindowState = FormWindowState.Normal;
            formInstance5.FormBorderStyle = FormBorderStyle.None;
            formInstance5.Show();
            formInstance5.BringToFront();




        }


        private void cogitel_Load(object sender, EventArgs e)
        {
            AfficherNotificationNonConformitesSansReponse();

        }
        private N__des_conds_et_des_aides_Conds formInstance = null;
        private void button4_Click(object sender, EventArgs e)
        {
           
            if (formInstance == null)
            {
                formInstance = new N__des_conds_et_des_aides_Conds();
                formInstance.FormClosed += (s, args) => formInstance = null;
            }
            if (ButtonsVisibleget == false)
            {
                formInstance.SetButtonVisible(false);
            }
            formInstance.MdiParent = this;
            formInstance.Dock = DockStyle.Fill;
            formInstance.WindowState = FormWindowState.Normal;
            formInstance.Show();
            formInstance.BringToFront();

        }

        public void ShowOrHidePanel(bool show)
        {
            panelslidemenu.Visible = show;
        }


        private void button5_Click(object sender, EventArgs e)
        {

            // Check if the slide menu is visible
            if (panelslidemenu.Visible)
            {
                // If it is visible, hide the slide menu
                panelslidemenu.Hide();
            }
            else
            {
                // If it is hidden, show the slide menu
                panelslidemenu.Show();
            }
        }
        private bool mouseDown;
        private Point lastLocation;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        private bool maximized = false; // Variable pour stocker l'état actuel du formulaire
        private void max_Click(object sender, EventArgs e)
        {
            if (!maximized) // Si le formulaire n'est pas déjà agrandi
            {
                this.WindowState = FormWindowState.Maximized;
                maximized = true; // Mettre à jour l'état actuel du formulaire
            }
            else // Si le formulaire est déjà agrandi
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = new Size(800, 600);
                maximized = false; // Mettre à jour l'état actuel du formulaire
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private Thèmes formInstance2 = null;
        private void button6_Click(object sender, EventArgs e)
        {
            if (formInstance2 == null)
            {
                formInstance2 = new Thèmes();
                formInstance2.FormClosed += (s, args) => formInstance2 = null;
            }
            formInstance2.MdiParent = this;
            formInstance2.Dock = DockStyle.Fill;
            formInstance2.WindowState = FormWindowState.Normal;
            formInstance2.Show();
            formInstance2.BringToFront();
        }
        private docpf formInstance3 = null;
        private void button7_Click(object sender, EventArgs e)
        {
            if (formInstance3 == null)
            {
                formInstance3 = new docpf();
                formInstance3.FormClosed += (s, args) => formInstance3 = null;
            }
            formInstance3.MdiParent = this;
            formInstance3.Dock = DockStyle.Fill;
            formInstance3.WindowState = FormWindowState.Normal;
            formInstance3.Show();
            formInstance3.BringToFront();
        }

        private void cogitel_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.Top = 0;
                this.Left = 0;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            }
        }
        private PrixPF formInstance4 = null;
        private void button8_Click(object sender, EventArgs e)
        {
            if (formInstance4 == null)
            {
                formInstance4 = new PrixPF();
                formInstance4.FormClosed += (s, args) => formInstance4 = null;
            }
            formInstance4.MdiParent = this;
            formInstance4.Dock = DockStyle.Fill;
            formInstance4.WindowState = FormWindowState.Normal;
            formInstance4.Show();
            formInstance4.BringToFront();
        }
        public void SetButtonVisible(bool isVisible)
        {
            // Définissez la propriété Visible du bouton en fonction de la valeur booléenne passée en paramètre
            button2.Visible = isVisible;
        }
        public bool ButtonsVisibleget { get; set; }
        private tableau_de_bord_RC formInstance6 = null;
        private void button9_Click(object sender, EventArgs e)
        {
            if (formInstance6 == null)
            {
                formInstance6 = new tableau_de_bord_RC();
                formInstance6.FormClosed += (s, args) => formInstance6 = null;
            }
            formInstance6.MdiParent = this;
            formInstance6.Dock = DockStyle.Fill;
            formInstance6.WindowState = FormWindowState.Normal;
            formInstance6.Show();
            formInstance6.BringToFront();
            
        }
        private prix_lot formInstance7 = null;
        private void button10_Click(object sender, EventArgs e)
        {
            if (formInstance7 == null)
            {
                formInstance7 = new prix_lot();
                formInstance7.FormClosed += (s, args) => formInstance7 = null;
            }
            formInstance7.MdiParent = this;
            formInstance7.Dock = DockStyle.Fill;
            formInstance7.WindowState = FormWindowState.Normal;
            formInstance7.Show();
            formInstance7.BringToFront();
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            showpanelRF(panelRF);
        }
        private RFMR formInstance8 = null;
        private void button12_Click(object sender, EventArgs e)
        {
            if (formInstance8 == null)
            {
                formInstance8 = new RFMR();
                formInstance8.FormClosed += (s, args) => formInstance8 = null;
            }
            formInstance8.MdiParent = this;
            formInstance8.Dock = DockStyle.Fill;
            formInstance8.WindowState = FormWindowState.Normal;
            formInstance8.Show();
            formInstance8.BringToFront();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void max_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void max_MouseLeave(object sender, EventArgs e)
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

        private void button5_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void buttonutili_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void buttonutili_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
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

        private void button6_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button7_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button10_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button10_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void Erc_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Erc_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button9_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button11_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button11_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button12_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button12_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void button13_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button13_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        private void AfficherNotificationNonConformitesSansReponse()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Client, N_de_la_NC FROM NCE WHERE Date_réponse_client IS NULL ";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                List<string> nonConformities = new List<string>();
                while (reader.Read())
                {
                    string client = reader.IsDBNull(0) ? "" : reader.GetString(0);
                    string nonConformity = reader.IsDBNull(1) ? "" : reader.GetString(1);
                    if (!string.IsNullOrEmpty(client) && !string.IsNullOrEmpty(nonConformity))
                    {
                        nonConformities.Add(client + " / " + nonConformity);
                    }

                }
                // Fermeture de la connexion à la base de données
                reader.Close();
                connection.Close();
                StringBuilder notificationTextBuilder = new StringBuilder();
                notificationTextBuilder.AppendLine("Les non-conformités suivantes nécessitent une réponse :");
                foreach (string nonConformity in nonConformities)
                {
                    string truncatedNonConformity = nonConformity.Substring(0, Math.Min(nonConformity.Length, 50));
                    notificationTextBuilder.AppendLine("- " + truncatedNonConformity);
                }
                string notificationText = notificationTextBuilder.ToString();
                // Affichez la notification avec les non-conformités avec une date de réponse vide
                notifyIcon1.Visible = true; // add this line
                notifyIcon1.ShowBalloonTip(30000, "Non-conformités sans réponse", notificationText, ToolTipIcon.Warning);
            }

        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            if (formInstance9 == null || formInstance9.IsDisposed)
            {
                formInstance9 = new notif();
                formInstance9.MdiParent = this; // "this" fait référence à votre formulaire MDI
                formInstance9.FormClosed += (s, args) => formInstance9 = null;
                formInstance9.Show();
            }
            else
            {
                formInstance9.BringToFront();
            }

        }

        private void button17_Click(object sender, EventArgs e)
        {
            isClosing = true; // Indiquer une fermeture programmée
            DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir vous déconnecter ?", "Déconnexion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                Close();

                // Redémarrer l'application
                Application.Restart();
            }
        }

        private void button17_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.button8, "Déconnecter");
            this.Cursor = Cursors.Hand;
        }

        private void button17_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;

        }
        private notif formInstance9 = null;
        private bool isFormOpen = false;
        private void button18_Click(object sender, EventArgs e)
        {
            if (!isFormOpen)
            {
                formInstance9 = new notif();
                formInstance9.MdiParent = this; // "this" fait référence à votre formulaire MDI
                formInstance9.FormClosed += (s, args) =>
                {
                    formInstance9 = null;
                    isFormOpen = false;
                };
                formInstance9.Dock = DockStyle.Fill;
                formInstance9.Show();

                isFormOpen = true;
            }
            else
            {
                formInstance9.Close();
                isFormOpen = false;
            }

        }
        private RFMS formInstance10 = null;

        private void button13_Click(object sender, EventArgs e)
        {
            
            if (formInstance10 == null)
            {
                formInstance10 = new RFMS();
                formInstance10.FormClosed += (s, args) => formInstance10 = null;
            }
            formInstance10.MdiParent = this;
            formInstance10.Dock = DockStyle.Fill;
            formInstance10.WindowState = FormWindowState.Normal;
            formInstance10.Show();
            formInstance10.BringToFront();
        }
        private void button18_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void button18_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            label3.Text = currentTime.ToString("HH:mm:ss");
            label1.Text = currentTime.ToString("dddd");
            label2.Text = currentTime.ToString("dd/MM/yyyy");
        }
    }
}
