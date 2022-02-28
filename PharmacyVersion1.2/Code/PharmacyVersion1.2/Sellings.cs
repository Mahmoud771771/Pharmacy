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
    public partial class Sellings : Form
    {
        public Sellings()
        {
            InitializeComponent();
            ShowMed();
            ShowBill();
            SNameLbl.Text = Logins.user;
            GetCustomer();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mahmoud\Documents\PharmacyDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void GetCustomer()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select CustNum from CustomerTbl ", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustNum", typeof(int));
            dt.Load(Rdr);
            CustIdcb.ValueMember = "CustNum";
            CustIdcb.DataSource = dt;
            con.Close();
        }
        private void GetCustomerName()
        {
            con.Open();
            string Query = "Select * from CustomerTbl where CustNum='" + CustIdcb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
               CustNameTb.Text = item["CustName"].ToString();
            }

            con.Close();


        }

        private void UpdateQty()
        {
            try
            {
                int newQty = stock - Convert.ToInt32(txtQty.Text);
                con.Open();
                SqlCommand cmd = new SqlCommand("update  MedicineTbl set MedQty=@MQ where MedNum=@Mkey", con);
               
                
                cmd.Parameters.AddWithValue("@MQ",newQty);
              
                cmd.Parameters.AddWithValue("@MKey", key);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Medicien Update");
                con.Close();
                ShowMed();
                //Reset();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
                con.Close();
            }
        }
        private void InsertBill()
        {
            if (CustNameTb.Text=="") {


                MessageBox.Show("enter Customer Name");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl(SName,CustNum,CustName,BDate,BAmount)values(@UA,@CN,@CNa,@BD,@BA)", con);
                    cmd.Parameters.AddWithValue("@UA", SNameLbl.Text);
                    cmd.Parameters.AddWithValue("@CN", CustIdcb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CNa", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@BD", DateTime.Today.Date);
                    cmd.Parameters.AddWithValue("@BA", grdTotal);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Saved");
                    grdTotal = 0;
                    con.Close();
                    ShowBill();

                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);

                }
            }
            
        }
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
        private void ShowBill()
        {
            con.Open();
            string Query = "Select * from BillTbl where SName ='"+SNameLbl.Text+"'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            transactionDGV.DataSource = ds.Tables[0];
            con.Close();

        }
        int n = 0,grdTotal=0;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtQty.Text == "" || Convert.ToInt32(txtQty.Text) > stock)
            {
                MessageBox.Show("Enter Correct Quantity");
            }
            else
            {
                int total = Convert.ToInt32(txtPrise.Text) * Convert.ToInt32(txtQty.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = txtMedName.Text;
                newRow.Cells[2].Value = txtQty.Text;
                newRow.Cells[3].Value = txtPrise.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                grdTotal += total;
                TotalLbl.Text = "Rs  " + grdTotal ;
                n++;
                UpdateQty();


            }
        }
        int key = 0, stock;
        int MedId, Medprice, MedQty, MedTot;

        private void CustIdcb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustomerName();
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

        int pos = 60;
        

        private void btnPrint_Click(object sender, EventArgs e)
        {

            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            InsertBill();


        }
        String MedName;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("CSpace Pharmacy", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID Medicines Price Quantity Total", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26,40));
            foreach (DataGridViewRow item in BillDGV.Rows)
            {
                MedId = Convert.ToInt32(item.Cells["Column1"].Value);
                MedName = "" + item.Cells["Column2"].Value;
                Medprice = Convert.ToInt32(item.Cells["Column3"].Value);
                MedQty = Convert.ToInt32(item.Cells["Column4"].Value);
                MedTot = Convert.ToInt32(item.Cells["Column5"].Value);
                e.Graphics.DrawString(""+MedId, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26,pos));
                e.Graphics.DrawString("" +MedName, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + Medprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + MedQty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" +MedTot, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos += 20;
                
            }
            e.Graphics.DrawString("Grand Total" + grdTotal, new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(50, pos+50));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            //grdTotal = 0;
            n = 0;
        }
        private void MedicineDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMedName.Text = MedicinesDGV.SelectedRows[0].Cells[1].Value.ToString();
            //cbMedType.SelectedItem = MedicinesDGV.SelectedRows[0].Cells[2].Value.ToString();
            stock =Convert.ToInt32(MedicinesDGV.SelectedRows[0].Cells[3].Value.ToString());
            txtPrise.Text = MedicinesDGV.SelectedRows[0].Cells[4].Value.ToString();
           // cbManufacurer.SelectedValue = MedicinesDGV.SelectedRows[0].Cells[5].Value.ToString();
            //txtMedManufact.Text = MedicinesDGV.SelectedRows[0].Cells[6].Value.ToString();

            if (txtMedName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(MedicinesDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
