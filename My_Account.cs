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
    public partial class My_Account : MetroForm
    {
        public My_Account()
        {
            InitializeComponent();
        }

        private void My_Account_Load(object sender, EventArgs e)
        {
            this.Size = new Size(802, 463);
            Fields_fill();
            panel5.Visible = false;
            personal_info_panel1.account = this;
            my_account_firsttext_panel1.BringToFront();
        }

        public async void Fields_fill()
        {
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM [Users] WHERE Id = @ID", connection);
                command.Parameters.AddWithValue("@ID", Program.user.ID);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    metroLabel1.Text = "Личный кабинет пользователя\n" + reader.GetValue(1).ToString();
                    personal_info_panel1.label3.Text = reader.GetValue(1).ToString();
                    personal_info_panel1.label4.Text = reader.GetValue(2).ToString(); 
                    personal_info_panel1.textBox1.Text = reader.GetValue(1).ToString();
                    personal_info_panel1.textBox2.Text = reader.GetValue(2).ToString();
                }
            }
        }

        private void My_Account_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            panel_slide((Button)sender);
            Ticket.Show_tickets(panel3);
            panel3.BringToFront();
        }

        private void panel_slide(Button b)
        {
            panel5.Height = b.Height;
            panel5.Top = b.Top;
            panel5.Visible = true;
            panel5.BringToFront();
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            panel_slide((Button)sender);
            MainForm f = new MainForm(Program.user.ID);
            f.Show();
            this.Hide();
        }

        private void button3_MouseClick(object sender, MouseEventArgs e)
        {
            panel_slide((Button)sender);
            personal_info_panel1.BringToFront();
        }
    }
}
