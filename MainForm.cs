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
    public partial class MainForm : MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(int k)
        {
            InitializeComponent();
            if (k>0)
            {
                Program.user.ID = k;
                metroLabel1.Visible = false;
                metroLabel2.Visible = false;
                metroLabel7.Text = "Личный кабинет";
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            metroLabel6.Text = "Театр имени \nОльги Кобылянской";
            if(Program.user.Right == 1)
            {
                label1.Text = "Редактирование афиши";
                label1.AutoSize = true;
                label1.Location = new Point(label1.Location.X-80,label1.Location.Y);
            }
        }

        private void metroLabel1_MouseClick(object sender, MouseEventArgs e)
        {
            Registration form = new Registration();
            form.Show();
            this.Hide();
        }

        private void metroLabel2_MouseClick(object sender, MouseEventArgs e)
        {
            Authorization form = new Authorization();
            form.Show();
            this.Hide();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void metroLabel7_MouseClick(object sender, MouseEventArgs e)
        {
            My_Account form = new My_Account();
            form.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (Program.user.Right == 1)
            {
                Editing form = new Editing(this);
                form.Show();
            }
            else
            {
                Afisha form = new Afisha(this);
                form.Show();
            }
            this.Hide();
        }
    }
}
