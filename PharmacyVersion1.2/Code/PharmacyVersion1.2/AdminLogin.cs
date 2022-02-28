using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyVersion1._2
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Logins obj = new Logins();
            obj.Show();
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (AdminPasswordTb.Text == "")
            {

            }
            else if (AdminPasswordTb.Text == "Admin")
            {
                
                Dashboard obj = new Dashboard();
                obj.Show();
                this.Hide();
            }
        }

        
    }
}
