using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyVersion1._2
{
    public partial class Logins : Form
    {
        public Logins()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mahmoud\Documents\PharmacyDb.mdf;Integrated Security=True;Connect Timeout=30");
        public static String user;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Please Enter Both User Name And Password");
            }
            else
            {
                con.Open();
                String query = "select count(*) from SellerTbl Where SName='" + UnameTb.Text + "' and SPass='" + PasswordTb.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    user = UnameTb.Text;
                    Sellings obj = new Sellings();
                    obj.Show();
                    this.Hide();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Wrong User Name Or Password");
                }
                con.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
            AdminLogin obj = new AdminLogin();
            obj.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Close();
            Application.Exit();
        }
    }
}
