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
    public partial class Medicines : Form
    {
        public Medicines()
        {
            InitializeComponent();
            ShowMed();
            GetManufacturer();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mahmoud\Documents\PharmacyDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowMed()
        {
            con.Open();
            string Query = "Select * from MedicineTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MedicinesDGV.DataSource = ds.Tables[0];
            con.Close();

        }
        private void Reset()
        {
            txtMedName.Text = "";
            txtMedManufact.Text = "";
            txtQty.Text = "";
            txtPrise.Text = ""; 
            cbMedType.SelectedIndex = 0;
            key = 0;



        }
        private void GetManufacturer(){
            con.Open();
            SqlCommand cmd = new SqlCommand("select ManId from ManufacturerTbl ",con);
            SqlDataReader Rdr; 
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("ManId", typeof(int));
            dt.Load(Rdr);
            cbManufacurer.ValueMember = "ManId";
            cbManufacurer.DataSource = dt;
            con.Close();
            }
        private void GetManufacturerName()
        {
            con.Open();
            string Query="Select * from ManufacturerTbl where ManId='"+cbManufacurer.SelectedValue.ToString()+"'";
            SqlCommand cmd = new SqlCommand(Query,con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                txtMedManufact.Text = item["ManName"].ToString();
            }

            con.Close();


        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMedName.Text==""|| txtPrise.Text == "" ||txtMedManufact.Text==""|| txtQty.Text == ""||cbMedType.SelectedIndex==-1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into MedicineTbl(MedName,MedType,MedQty,MedPrice,MedManId,MedManufact)values(@MN,@MT,@MQ,@MP,@MMI,@MM)", con);
                    cmd.Parameters.AddWithValue("@MN", txtMedName.Text);
                    cmd.Parameters.AddWithValue("@MT", cbMedType.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MQ",Convert.ToInt32( txtQty.Text));
                    cmd.Parameters.AddWithValue("@MP",Convert.ToInt32( txtPrise.Text));
                    cmd.Parameters.AddWithValue("@MMI", Convert.ToInt32( cbManufacurer.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("@MM", txtMedManufact.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicien Added");
                    con.Close();
                    ShowMed();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                    con.Close();
                }
            }
        }

        private void cbManufacurer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetManufacturerName();
        }

        int key = 0;
        private void MedicinesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMedName.Text = MedicinesDGV.SelectedRows[0].Cells[1].Value.ToString();
            cbMedType.SelectedItem = MedicinesDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtQty.Text = MedicinesDGV.SelectedRows[0].Cells[3].Value.ToString();
            txtPrise.Text = MedicinesDGV.SelectedRows[0].Cells[4].Value.ToString();
            cbManufacurer.SelectedValue = MedicinesDGV.SelectedRows[0].Cells[5].Value.ToString();
            txtMedManufact.Text = MedicinesDGV.SelectedRows[0].Cells[6].Value.ToString();

            if (txtMedName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(MedicinesDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (key == 0)
            {
                MessageBox.Show("Select The Medicine");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from MedicineTbl where MedNum=@MKey", con);
                    cmd.Parameters.AddWithValue("@MKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine Deleted");
                    con.Close();
                    ShowMed();
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

            if (txtMedName.Text == "" || txtPrise.Text == "" || txtMedManufact.Text == "" || txtQty.Text == "" || cbMedType.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update  MedicineTbl set MedName=@MN,MedType=@MT,MedQty=@MQ,MedPrice=@MP,MedManId=@MMI,MedManufact=@MM where MedNum=@Mkey", con);
                    cmd.Parameters.AddWithValue("@MN", txtMedName.Text);
                    cmd.Parameters.AddWithValue("@MT", cbMedType.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MQ", Convert.ToInt32(txtQty.Text));
                    cmd.Parameters.AddWithValue("@MP", Convert.ToInt32(txtPrise.Text));
                    cmd.Parameters.AddWithValue("@MMI", Convert.ToInt32(cbManufacurer.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("@MM", txtMedManufact.Text);
                    cmd.Parameters.AddWithValue("@MKey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicien Update");
                    con.Close();
                    ShowMed();
                    Reset();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                    con.Close();
                }
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbManufacurer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbMedType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtMedManufact_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrise_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMedName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Sellings obj = new Sellings();
            obj.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Sellings obj = new Sellings();
            obj.Show();
            this.Hide();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Sellers obj = new Sellers();
            obj.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Sellers obj = new Sellers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void pictureBox9_Click_1(object sender, EventArgs e)
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
