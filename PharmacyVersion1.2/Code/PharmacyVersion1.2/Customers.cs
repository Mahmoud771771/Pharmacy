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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            ShowCust();
        }


        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mahmoud\Documents\PharmacyDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowCust()
        {
            con.Open();
            string Query = "Select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            con.Close();

        }
        private void Reset()
        {
            txtCustName.Text = "";
            txtCustPhone.Text = "";
            txtCustAdd.Text = "";
            cbGender.SelectedValue = 0;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCustName.Text == "" || txtCustAdd.Text == "" || txtCustPhone.Text == ""||cbGender.SelectedIndex==-1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl(CustName,CustPhone,CustAdd,CustDOB,CustGen)values(@CN,@CP,@CA,@CD,@CG)", con);
                    cmd.Parameters.AddWithValue("@CN", txtCustName.Text);
                    cmd.Parameters.AddWithValue("@CP", txtCustPhone.Text);
                    cmd.Parameters.AddWithValue("@CA", txtCustAdd.Text);
                    cmd.Parameters.AddWithValue("@CD", txtCustDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", cbGender.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added");
                    con.Close();
                    ShowCust();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);

                }
            }
        }
        int key = 0;
        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCustName.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtCustPhone.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtCustAdd.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
            txtCustDOB.Text = CustomerDGV.SelectedRows[0].Cells[4].Value.ToString();
            cbGender.SelectedItem = CustomerDGV.SelectedRows[0].Cells[5].Value.ToString();

            if (txtCustName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (key == 0)
            {
                MessageBox.Show("Select The Customer");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from CustomerTbl where CustNum=@MKey", con);
                    cmd.Parameters.AddWithValue("@MKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted");
                    con.Close();
                    ShowCust();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);

                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtCustName.Text == "" || txtCustAdd.Text == "" || txtCustPhone.Text == "" || cbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update  CustomerTbl set CustName=@CN,CustPhone=@CP,CustAdd=@CA,CustDOB=@CD,CustGen=@CG where CustNum=@CKey ", con);
                    cmd.Parameters.AddWithValue("@CN", txtCustName.Text);
                    cmd.Parameters.AddWithValue("@CP", txtCustPhone.Text);
                    cmd.Parameters.AddWithValue("@CA", txtCustAdd.Text);
                    cmd.Parameters.AddWithValue("@CD", txtCustDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", cbGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CKey", key);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Update");
                    con.Close();
                    ShowCust();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);

                }
            }
        }

        private void label24_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Manufacturer obj = new Manufacturer();
            obj.Show();
            this.Hide();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
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

        private void label18_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }
    }
}
