using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PharmacyVersion1._2
{
    public partial class Manufacturer : Form
    {
        public Manufacturer()
        {
            InitializeComponent();
            ShowMan();
        }


        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mahmoud\Documents\PharmacyDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowMan()
        {
            con.Open();
            string Query = "Select * from ManufacturerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ManufacturerDGV.DataSource = ds.Tables[0];
            con.Close();
            
        }
        private void btnManSave_Click(object sender, EventArgs e)
        {
            if (txtManName.Text == "" || txtManPhone.Text == "" || txtManAddress.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ManufacturerTbl(ManName,ManAdd,ManPhone,ManDate)values(@MN,@MA,@MP,@MD)", con);
                    cmd.Parameters.AddWithValue("@MN", txtManName.Text);
                    cmd.Parameters.AddWithValue("@MA", txtManAddress.Text);
                    cmd.Parameters.AddWithValue("@MP", txtManPhone.Text);
                    cmd.Parameters.AddWithValue("@MD", txtManDate.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("ManuFacturer Added");
                    con.Close();
                    ShowMan();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);

                }
            }
        }
        int key = 0;
        private void ManufacturerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtManName.Text = ManufacturerDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtManAddress.Text = ManufacturerDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtManPhone.Text = ManufacturerDGV.SelectedRows[0].Cells[3].Value.ToString();
            txtManDate.Text = ManufacturerDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (txtManName.Text=="")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ManufacturerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void btnManDelete_Click(object sender, EventArgs e)
        {
            if (key==0)
            {
                MessageBox.Show("Select The Manufacurer");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from ManufacturerTbl where ManId=@MKey",con);
                    cmd.Parameters.AddWithValue("@MKey",key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("ManuFacturer Deleted");
                    con.Close();
                    ShowMan();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);

                }
            }
        }
        private void Reset()
        {
            txtManName.Text = "";
            txtManAddress.Text = "";
            txtManPhone.Text = "";
            key = 0;



        }

        private void btnManEdit_Click(object sender, EventArgs e)
        {
            if (txtManName.Text == "" || txtManPhone.Text == "" || txtManAddress.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update  ManufacturerTbl set ManName=@MN,ManAdd=@MA,ManPhone=@MP,ManDate=@MD where ManId=@Mkey", con);
                    cmd.Parameters.AddWithValue("@MN", txtManName.Text);
                    cmd.Parameters.AddWithValue("@MA", txtManAddress.Text);
                    cmd.Parameters.AddWithValue("@MP", txtManPhone.Text);
                    cmd.Parameters.AddWithValue("@MD", txtManDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Mkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("ManuFacturer Updated");
                    con.Close();
                    ShowMan();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);

                }
            }
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

        private void label18_Click(object sender, EventArgs e)
        {
            AdminLogin obj = new AdminLogin();
            obj.Show();
            this.Hide();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            AdminLogin obj = new AdminLogin();
            obj.Show();
            this.Hide();
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

        private void label13_Click_1(object sender, EventArgs e)
        {
            Medicines obj = new Medicines();
            obj.Show();
            this.Hide();
        }

        private void pictureBox10_Click_1(object sender, EventArgs e)
        {
            Medicines obj = new Medicines();
            obj.Show();
            this.Hide();
        }

        private void label15_Click_1(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox11_Click_1(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void label16_Click_1(object sender, EventArgs e)
        {
            Sellers obj = new Sellers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox12_Click_1(object sender, EventArgs e)
        {
            Sellers obj = new Sellers();
            obj.Show();
            this.Hide();
        }

        private void label17_Click_1(object sender, EventArgs e)
        {
            Sellings obj = new Sellings();
            obj.Show();
            this.Hide();
        }

        private void pictureBox13_Click_1(object sender, EventArgs e)
        {
            Sellings obj = new Sellings();
            obj.Show();
            this.Hide();
        }

        private void label18_Click_1(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void pictureBox14_Click_1(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }
    }
}