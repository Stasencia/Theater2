using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Data.SqlClient;
using MetroFramework;

namespace Project_theater
{
    public partial class Personal_info_panel : UserControl
    {
        string New_Login;
        string Initial_Login;
        string New_Password;
        string Initial_Password;
        public My_Account account { get; set; }

        public Personal_info_panel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Initial_Login = label3.Text;
            New_Login = textBox1.Text;
            if (button1.Text == "Изменить")
            {
                textBox1.Visible = true;
                button1.Text = "Подтвердить изменения";
            }
            else
            {
                Program.user.Login_change(New_Login, Initial_Login, account);
                textBox1.Visible = false;
                button1.Text = "Изменить";
                button1.Width = 86;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Initial_Password = label4.Text;
            New_Password = textBox2.Text;
            if (button2.Text == "Изменить")
            {
                textBox2.Visible = true;
                button2.Text = "Подтвердить изменения";
            }
            else
            {
                Program.user.Password_change(New_Password, Initial_Password, account);
                textBox2.Visible = false;
                button2.Text = "Изменить";
                button2.Width = 86;
            }
        }
    }
}
