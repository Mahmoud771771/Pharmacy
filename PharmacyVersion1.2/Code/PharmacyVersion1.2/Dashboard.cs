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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountMed();
            CountCust();
            CountSeller();
            SumAmt();
            GetSeller();
            GetBestSeller();
            GetBestCustomer();


        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mahmoud\Documents\PharmacyDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void CountMed()
        {
            con.Open();
            String Query = "select Count(*) from MedicineTbl";

            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MedNum.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountCust()
        {
            con.Open();
            String Query = "select Count(*) from CustomerTbl";

            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CustomerLb.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountSeller()
        {
            con.Open();
            String Query = "select Count(*) from SellerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SellerLb.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void SumAmt()
        {
            con.Open();
            String Query = "select sum(BAmount) from BillTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SellAmtLpl.Text ="B "+ dt.Rows[0][0].ToString();
            con.Close();
        }
        private void GetBestSeller ()
        {
            try
            {
                con.Open();
                String innerQuery = "select max(BAmount) from BillTbl";
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(innerQuery, con);
                sda1.Fill(dt1);
                String Query = "select CustName from BillTbl where BAmount ='" + dt1.Rows[0][0].ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BestCustLbl.Text = dt.Rows[0][0].ToString();
                con.Close();
            }
            catch(Exception Ex)
            {
                con.Close();
                MessageBox.Show(Ex.Message);


            }
        }
        private void GetBestCustomer()
        {
            try
            {
                con.Open();
                String innerQuery = "select max(BAmount) from BillTbl";
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(innerQuery, con);
                sda1.Fill(dt1);
                String Query = "select SName from BillTbl where BAmount ='" + dt1.Rows[0][0].ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BestSellerLbl.Text = dt.Rows[0][0].ToString();
                con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                con.Close();

            }
        }
        private void SumAmtBySeller()
        {
            try
            {
                con.Open();
                String Query = "select sum(BAmount) from BillTbl where SName ='" + SellerCb.SelectedValue.ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SellsBySeller.Text = "B " + dt.Rows[0][0].ToString();
                con.Close();

            }catch(Exception Ex)
            {
                con.Close();
                MessageBox.Show(Ex.Message);
            }
        }

        private void GetSeller()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select SName from SellerTbl ", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SName", typeof(String));
            dt.Load(Rdr);
            SellerCb.ValueMember = "SName";
            SellerCb.DataSource = dt;
            con.Close();
        }
        private void pictureBox14_Click(object sender, EventArgs e)
        {
            AdminLogin obj = new AdminLogin();
            obj.Show();
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {

            
            AdminLogin obj = new AdminLogin();
            obj.Show();
            this.Hide();
        }

        private void SellerCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SumAmtBySeller();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Manufacturer obj = new Manufacturer();
            obj.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Manufacturer obj = new Manufacturer();
            obj.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Medicines obj = new Medicines();
            obj.Show();
            this.Hide();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Medicines obj = new Medicines();
            obj.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Sellers obj = new Sellers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Sellers obj = new Sellers();
            obj.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Sellings obj = new Sellings();
            obj.Show();
            this.Hide();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Sellings obj = new Sellings();
            obj.Show();
            this.Hide();
        }


    }
}
