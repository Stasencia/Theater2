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
    public partial class Change_info : MetroForm
    {
        int userid, flag;
        public Change_info(int id,int k)
        {
            InitializeComponent();
            userid = id;
            flag = k;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command;
                if (flag == 0)
                {
                    command= new SqlCommand("UPDATE Users SET Login=@Login WHERE Id = @ID", connection);
                    command.Parameters.AddWithValue("@Login", textBox1.Text);
                    command.Parameters.AddWithValue("@ID", userid);
                }
                else
                {
                    command = new SqlCommand("UPDATE Users SET Password=@Password WHERE Id = @ID", connection);
                    command.Parameters.AddWithValue("@Password", textBox1.Text);
                    command.Parameters.AddWithValue("@ID", userid);
                    }
                command.ExecuteNonQuery();
                MetroMessageBox.Show(this, "Изменения были успешно внесены", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 120);
                Changed?.Invoke();
                this.Close();
            }
        }

        private async void Change_info_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM [Users] WHERE Id = @ID", connection);
                command.Parameters.AddWithValue("@ID", userid);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (flag == 0)
                    {
                        label1.Text = "Введите новое значение логина:";
                        textBox1.Text = reader.GetValue(1).ToString();
                    }
                    else
                    {
                        label1.Text = "Введите новое значение пароля:";
                        textBox1.Text = reader.GetValue(2).ToString();
                    }
                }
            }
        }
        public delegate void ChangedDelegate();
        public event ChangedDelegate Changed;
    }
}
