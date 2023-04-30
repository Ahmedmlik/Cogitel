using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Cogitel_QT
{

    internal class Connexion
    {
        public SqlConnection con;
        public SqlCommand cmd;
        public SqlDataAdapter sda;
        public string pkk;
        public void Connexion1()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["MaConnexion"].ConnectionString);
            con.Open();
        }
        public void dataSend(string SQL)
        {
            try
            {
                Connexion1();
                cmd = new SqlCommand(SQL, con);
                cmd.ExecuteNonQuery();
                pkk = "";
            }
            catch (Exception)
            {
                pkk = "Please chek your Data";
            }
            con.Close();
        }
        public void dataGet(string SQL)
        {
            try
            {
                Connexion1();
                sda = new SqlDataAdapter(SQL, con);
            }
            catch (Exception)
            {

            }
        }
    }

}
