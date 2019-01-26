using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using MetroFramework;

namespace Project_theater
{
    public partial class Registration : MetroForm
    {
        private string Login { get; set; }
        private string Password { get; set; }

        public Registration()
        {
            InitializeComponent();
        }

        private void Registration_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm f = new MainForm(Program.user.ID + 1);
            f.Show();
            this.Hide();
        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Login = metroTextBox1.Text;
            Password = metroTextBox2.Text;
            Program.user.Registration(this, Login, Password);
        } 
    }
}
