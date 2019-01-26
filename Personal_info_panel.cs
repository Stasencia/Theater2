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
        public int userid;
        public My_Account account { get; set; }
        public Personal_info_panel()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Изменить")
            {
                textBox1.Visible = true;
                button1.Text = "Подтвердить изменения";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrEmpty(textBox1.Text))
                {
                    if(textBox1.Text != label3.Text)
                    using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
                    {
                        await connection.OpenAsync();
                        SqlCommand command = new SqlCommand("UPDATE Users SET Login=@Login WHERE Id = @ID", connection);
                        command.Parameters.AddWithValue("@Login", textBox1.Text);
                        command.Parameters.AddWithValue("@ID", userid);
                        command.ExecuteNonQuery();
                        MetroMessageBox.Show(this, "Изменения были успешно внесены", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 120);
                        account.Fields_fill();
                    }   
                }
                else
                    MetroMessageBox.Show(this, "Неправильно введено значение логина", "", MessageBoxButtons.OK, MessageBoxIcon.Error, 120);
                textBox1.Visible = false;
                button1.Text = "Изменить";
                button1.Width = 86;
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Изменить")
            {
                textBox2.Visible = true;
                button2.Text = "Подтвердить изменения";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrEmpty(textBox1.Text))
                {
                    if (textBox2.Text != label4.Text)
                    using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
                    {
                        await connection.OpenAsync();
                        SqlCommand command = new SqlCommand("UPDATE Users SET Password=@Password WHERE Id = @ID", connection);
                        command.Parameters.AddWithValue("@Password", textBox2.Text);
                        command.Parameters.AddWithValue("@ID", userid);
                        command.ExecuteNonQuery();
                        MetroMessageBox.Show(this, "Изменения были успешно внесены", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 120);
                        account.Fields_fill();
                    }
                    
                }
                else
                    MetroMessageBox.Show(this, "Неправильно введено значение пароля", "", MessageBoxButtons.OK, MessageBoxIcon.Error, 120);
                textBox2.Visible = false;
                button2.Text = "Изменить";
                button2.Width = 86;
            }
        }
    }
}
