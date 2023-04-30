using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;


namespace Cogitel_QT
{
    public partial class modifiermotdepasse : Form
    {
      
        public modifiermotdepasse()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(modifiermotdepasse_KeyPress);

        }


        private void Annuler_Click(object sender, EventArgs e)
        {
            cogitel.Instance.ShowOrHidePanel(true);
            this.Close();
        }

        private void Valider_Click(object sender, EventArgs e)
        {
            if (textnu.Text == "Nom d'utilisateur" || textap.Text == "Ancien mot de passe" || textnnu.Text == "Nouveau nom d'utilisateur" || textnp.Text == "Nouveau mot de passe")
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                SHA256 sha257 = SHA256.Create();
                string apassword = textap.Text;

                // Convert the plaintext password to a byte array
                byte[] apasswordBytes = Encoding.UTF8.GetBytes(apassword);

                // Compute the hash value of the password byte array
                byte[] ahashBytes = sha257.ComputeHash(apasswordBytes);

                // Convert the hash value byte array to a string
                string ahashedPassword = Convert.ToBase64String(ahashBytes);

                SHA256 sha256 = SHA256.Create();

                // Get the plaintext password from the textbox
                string password = textnp.Text;

                // Convert the plaintext password to a byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash value of the password byte array
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hash value byte array to a string
                string hashedPassword = Convert.ToBase64String(hashBytes);


                Connexion con = new Connexion();
                con.dataGet("Select 1 from [Login] Where Utilisateur = '" + textnu.Text + "' and MotdePasse  = '" + ahashedPassword + "'");
                DataTable dt = new DataTable();
                con.sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    con.dataSend("Update [Login] set Utilisateur = '" + textnnu.Text + "' ,  MotdePasse = '" + hashedPassword + "'  Where Utilisateur = '" + textnu.Text + "' and MotdePasse = '" + ahashedPassword + "'");
                    MessageBox.Show("Nom de l'Utilisateur et le mot de passe a été changé avec succès", "message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

                else
                {
                    MessageBox.Show("Utilisateur ou Mot de passe incorrect ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    textnu.Text = "Nom d'utilisateur";
                    textnu.ForeColor = Color.Gray;
                    textap.Text = "Ancien mot de passe";
                    textap.ForeColor = Color.Gray;
                    textap.UseSystemPasswordChar = false;
                    textnnu.Text = "Nouveau nom d'utilisateur";
                    textnnu.ForeColor = Color.Gray;
                    textnp.Text = "Nouveau mot de passe";
                    textnp.ForeColor = Color.Gray;
                    textnp.UseSystemPasswordChar = false;
                }



            }
        }

        private void textnu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textnnu.Focus();
            }
            else
            {
                textnu.Focus();
            }
        }

        private void textap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textnp.Focus();
            }
            else
            {
                textap.Focus();
            }
        }

        private void textnnu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textap.Focus();
            }
            else
            {
                textnnu.Focus();
            }
        }

        private void textnp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Valider.Focus();
            }
            else
            {
                textnp.Focus();
            }
        }

        private void modifiermotdepasse_Load(object sender, EventArgs e)
        {

            textnu.Text = "Nom d'utilisateur";
            textnu.ForeColor = Color.Gray;
            textap.Text = "Ancien mot de passe";
            textap.ForeColor = Color.Gray;
            textap.UseSystemPasswordChar = false;
            textnnu.Text = "Nouveau nom d'utilisateur";
            textnnu.ForeColor = Color.Gray;
            textnp.Text = "Nouveau mot de passe";
            textnp.ForeColor = Color.Gray;
            textnp.UseSystemPasswordChar = false;
            this.ActiveControl = null;

        }

        private void modifiermotdepasse_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Empêcher la génération de bip sonore lors de la frappe sur la touche Entrée
                e.Handled = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textnu_Enter(object sender, EventArgs e)
        {
            if (textnu.Text == "Nom d'utilisateur")
            {
                textnu.Text = "";
                textnu.ForeColor = Color.Black;
            }
        }

        private void textnu_Leave(object sender, EventArgs e)
        {
            if (textnu.Text == "")
            {
                textnu.Text = "Nom d'utilisateur";
                textnu.ForeColor = Color.Gray;
            }
        }

        private void textap_Enter(object sender, EventArgs e)
        {
            if (textap.Text == "Ancien mot de passe")
            {
                textap.Text = "";
                textap.ForeColor = Color.Black;
                textap.UseSystemPasswordChar = true;
            }
        }

        private void textap_Leave(object sender, EventArgs e)
        {
            if (textap.Text == "")
            {
                textap.Text = "Ancien mot de passe";
                textap.ForeColor = Color.Gray;
                textap.UseSystemPasswordChar = false;
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            textap.UseSystemPasswordChar = false;
            textnp.UseSystemPasswordChar = false;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            textap.UseSystemPasswordChar = true;
            textnp.UseSystemPasswordChar = true;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(this.pictureBox2, "Afficher mot de passe");
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void textnnu_Enter(object sender, EventArgs e)
        {
            if (textnnu.Text == "Nouveau nom d'utilisateur")
            {
                textnnu.Text = "";
                textnnu.ForeColor = Color.Black;
            }
        }

        private void textnnu_Leave(object sender, EventArgs e)
        {
            if (textnnu.Text == "")
            {
                textnnu.Text = "Nouveau nom d'utilisateur";
                textnnu.ForeColor = Color.Gray;
            }

        }

        private void textnp_Enter(object sender, EventArgs e)
        {
            if (textnp.Text == "Nouveau mot de passe")
            {
                textnp.Text = "";
                textnp.ForeColor = Color.Black;
                textnp.UseSystemPasswordChar = true;
            }
        }

        private void textnp_Leave(object sender, EventArgs e)
        {
            if (textnp.Text == "")
            {
                textnp.Text = "Nouveau mot de passe";
                textnp.ForeColor = Color.Gray;
                textnp.UseSystemPasswordChar = false;
            }
        }

        private void Valider_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Valider_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void Annuler_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void Annuler_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
    }
}
