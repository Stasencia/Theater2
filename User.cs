using MetroFramework;
using System;
using System.Collections.Generic;
using System.Data.Linq;
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

        public void Registration(Form sender, string login, string password)
        {
            if (!string.IsNullOrWhiteSpace(login) && !string.IsNullOrEmpty(login) &&
           !string.IsNullOrWhiteSpace(password) && !string.IsNullOrEmpty(password))
            {
                DataContext db = new DataContext(DB_connection.connectionString);
                var query = db.GetTable<TUsers>()
                    .Any(k => k.Login == login);
                if (!query)
                {
                    TUsers user = new TUsers() { Login = login, Password = password };
                    db.GetTable<TUsers>().InsertOnSubmit(user);
                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (Exception e)
                    {
                        MetroMessageBox.Show(sender, e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
                    }
                    MetroMessageBox.Show(sender, "Вы были успешно зарегистрированы!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 120);
                    ID = user.Id;
                    sender.Close();
                }
                else
                    MetroMessageBox.Show(sender, "Данный логин уже занят", "Значение логина", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, 120);
            }
            else
                MetroMessageBox.Show(sender, "Заполните все необходимые поля", "Ошибка заполнения", MessageBoxButtons.OK, MessageBoxIcon.Error, 120);
        }

        public void Authorization(Form sender, string login, string password)
        {
            if (!string.IsNullOrWhiteSpace(login) && !string.IsNullOrEmpty(login) &&
            !string.IsNullOrWhiteSpace(password) && !string.IsNullOrEmpty(password))
            {
                DataContext db = new DataContext(DB_connection.connectionString);
                var query = db.GetTable<TUsers>()
                            .FirstOrDefault(k => k.Login == login && k.Password == password);
                if (query != null)
                {
                    Program.user.ID = query.Id;
                    Program.user.Right = query.Rights;
                    MetroMessageBox.Show(sender, "Добро пожаловать!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 120); ;
                    sender.Close();
                }
                else
                    MetroMessageBox.Show(sender, "Пользователя с таким логином и паролем нет в базе данных", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
            else
                MetroMessageBox.Show(sender, "Заполните все необходимые поля", "Ошибка заполнения", MessageBoxButtons.OK, MessageBoxIcon.Error, 120);
        }
    }
}
