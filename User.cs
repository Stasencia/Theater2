using MetroFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_theater
{
    public class User
    {
        private static User instance;

        public int ID = 0;
        public int Right = 0;

        protected User()
        {
            
        }

        public static User GetInstance()
        {
            if (instance == null)
                instance = new User();
            return instance;
        }

        public async void Registration(Form sender, string login, string password)
        {
            if (!string.IsNullOrWhiteSpace(login) && !string.IsNullOrEmpty(login) &&
           !string.IsNullOrWhiteSpace(password) && !string.IsNullOrEmpty(password))
            {
                using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand selection_com = new SqlCommand("SELECT * FROM Users", connection);
                    SqlDataReader reader = await selection_com.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ID = int.Parse(reader.GetValue(0).ToString());
                            if (reader.GetValue(1).ToString() == login)
                            {
                                ID = 0;
                                break;
                            }
                        }
                    }
                    reader.Close();
                }
                using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
                {
                    await connection.OpenAsync();
                    if (ID != 0)
                    {
                        SqlCommand command = new SqlCommand("INSERT INTO [Users] (Login, Password) VALUES(@Login, @Password)", connection);
                        command.Parameters.AddWithValue("Login", login);
                        command.Parameters.AddWithValue("Password", password);
                        await command.ExecuteNonQueryAsync();
                        MetroMessageBox.Show(sender, "Вы были успешно зарегистрированы!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 120);
                    }
                    else
                        MetroMessageBox.Show(sender, "Данный логин уже занят", "Значение логина", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, 120);
                }

            }
            else
                MetroMessageBox.Show(sender, "Заполните все необходимые поля", "Ошибка заполнения", MessageBoxButtons.OK, MessageBoxIcon.Error, 120);

        }
    }
}
