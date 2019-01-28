using MetroFramework;
using MetroFramework.Forms;
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

namespace Project_theater
{
    public partial class Authorization : MetroForm
    {
        private string Login { get; set; }
        private string Password { get; set; }

        public Authorization()
        {
            InitializeComponent();
        }

        private void Authorization_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm f = new MainForm(Program.user.ID);
            f.Show();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Login = metroTextBox1.Text;
            Password = metroTextBox2.Text;
            Program.user.Authorization(this, Login, Password);
        }
    }
}
