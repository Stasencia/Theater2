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
        int ID = 0;
        public Authorization()
        {
            InitializeComponent();
        }

        private void Authorization_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm f = new MainForm(ID);
            f.Show();
        }

        private async void metroButton1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(metroTextBox1.Text) && !string.IsNullOrEmpty(metroTextBox1.Text) &&
            !string.IsNullOrWhiteSpace(metroTextBox2.Text) && !string.IsNullOrEmpty(metroTextBox2.Text))
            {
                using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand selection_com = new SqlCommand("SELECT * FROM Users", connection);
                    SqlDataReader reader = await selection_com.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            if (reader.GetValue(1).ToString() == metroTextBox1.Text && reader.GetValue(2).ToString() == metroTextBox2.Text)
                            {
                                ID = int.Parse(reader.GetValue(0).ToString());
                                break;
                            }
                        }
                    }

                    if (ID != 0)
                    {
                        MetroMessageBox.Show(this, "Добро пожаловать!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 120);;
                        Program.user.Right = Convert.ToInt32(reader.GetValue(0));
                        Close();
                    }
                    else
                        MetroMessageBox.Show(this, "Пользователя с таким логином и паролем нет в базе данных", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                    reader.Close();
                }
            }
            else
                MetroMessageBox.Show(this, "Заполните все необходимые поля", "Ошибка заполнения", MessageBoxButtons.OK, MessageBoxIcon.Error, 120);
        }
    }
}
