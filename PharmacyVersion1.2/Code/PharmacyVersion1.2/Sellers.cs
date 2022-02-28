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
    public partial class Sellers : Form
    {
        public Sellers()
        {
            InitializeComponent();
            ShowSeller();

        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mahmoud\Documents\PharmacyDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowSeller()
        {
            con.Open();
            string Query = "Select * from SellerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SellersDGV.DataSource = ds.Tables[0];
            con.Close();

        }
        private void Reset()
        {
            SellerTBName.Text = "";
            SellerTBPhone.Text = "";
            SellerTBAddress.Text = "";
            SellerTBPassword.Text = "";
            SellerCBGender.SelectedIndex = 0;

        }
        private void SellerBtnSava_Click(object sender, EventArgs e)
        {
            if (SellerTBName.Text == "" || SellerTBAddress.Text == "" || SellerTBPhone.Text == "" || SellerTBPassword.Text == ""||SellerCBGender.SelectedIndex==-1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into SellerTbl(SName,SDOB,SPhone,SAdd,SGen,SPass)values(@SN,@SDOB,@SP,@SA,@SG,@SPA)", con);
                    cmd.Parameters.AddWithValue("@SN", SellerTBName.Text);
                    cmd.Parameters.AddWithValue("@SDOB", SellerDPDOP.Value.Date);
                    cmd.Parameters.AddWithValue("@SP", SellerTBPhone.Text);
                    cmd.Parameters.AddWithValue("@SA", SellerTBAddress.Text);
                    cmd.Parameters.AddWithValue("@SG", SellerCBGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SPA", SellerTBPassword.Text);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added");
                    con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);

                }
            }
        }
        int key = 0;
        private void SellersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SellerTBName.Text = SellersDGV.SelectedRows[0].Cells[1].Value.ToString();
            SellerDPDOP.Text = SellersDGV.SelectedRows[0].Cells[2].Value.ToString();
            SellerTBPhone.Text= SellersDGV.SelectedRows[0].Cells[3].Value.ToString();
            SellerTBAddress.Text = SellersDGV.SelectedRows[0].Cells[4].Value.ToString();
            SellerCBGender.SelectedItem= SellersDGV.SelectedRows[0].Cells[5].Value.ToString();
            SellerTBPassword.Text = SellersDGV.SelectedRows[0].Cells[6].Value.ToString();

            if (SellerTBName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(SellersDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void SellerBtnDelete_Click(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("Delete from SellerTbl where SNum=@MKey", con);
                    cmd.Parameters.AddWithValue("@MKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Deleted");
                    con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);

                }
            }
        }

        private void SellerBtnEdit_Click(object sender, EventArgs e)
        {
            if (SellerTBName.Text == "" || SellerTBAddress.Text == "" || SellerTBPhone.Text == "" || SellerTBPassword.Text == "" || SellerCBGender.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update  SellerTbl set SName=@SN,SDOB=@SDOB,SPhone=@SP,SAdd=@SA,SGen=@SG,SPass=@SPA where SNum=@SKey", con);
                    cmd.Parameters.AddWithValue("@SN", SellerTBName.Text);
                    cmd.Parameters.AddWithValue("@SDOB", SellerDPDOP.Value.Date);
                    cmd.Parameters.AddWithValue("@SP", SellerTBPhone.Text);
                    cmd.Parameters.AddWithValue("@SA", SellerTBAddress.Text);
                    cmd.Parameters.AddWithValue("@SG", SellerCBGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SPA", SellerTBPassword.Text);
                    
                    cmd.Parameters.AddWithValue("@SKey", key);




                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Update");
                    con.Close();
                    ShowSeller();
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
