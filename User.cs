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

        public void Login_change(string new_login, string initial_login, My_Account account)
        {
            if (!string.IsNullOrWhiteSpace(new_login) && !string.IsNullOrEmpty(new_login))
            {
                if (new_login != initial_login)
                {
                    DataContext db = new DataContext(DB_connection.connectionString);
                    var query = db.GetTable<TUsers>()
                        .Any(k => k.Login == new_login);
                    if (!query)
                    {
                        TUsers user = db.GetTable<TUsers>().Where(k => k.Id == Program.user.ID).First();
                        user.Login = new_login;
                        try
                        {
                            db.SubmitChanges();
                        }
                        catch (Exception e)
                        {
                            MetroMessageBox.Show(account, e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
                        }
                        MetroMessageBox.Show(account, "Изменения были успешно внесены", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 120);
                    }
                    else
                        MetroMessageBox.Show(account, "Данный логин уже занят", "Значение логина", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, 120);
                }
            }
            else
                MetroMessageBox.Show(account, "Неправильно введено значение логина", "", MessageBoxButtons.OK, MessageBoxIcon.Error, 120);
            account.Fields_fill();
        }  
        
        public void Password_change(string new_password, string initial_password, My_Account account)
        {
            if (!string.IsNullOrWhiteSpace(new_password) && !string.IsNullOrEmpty(new_password))
            {
                if (new_password != initial_password)
                {
                    DataContext db = new DataContext(DB_connection.connectionString);
                    TUsers user = db.GetTable<TUsers>().Where(k => k.Id == Program.user.ID).First();
                    user.Password = new_password;
                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (Exception e)
                    {
                        MetroMessageBox.Show(account, e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
                    }
                    MetroMessageBox.Show(account, "Изменения были успешно внесены", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 120);
                }
            }
            else
                MetroMessageBox.Show(account, "Неправильно введено значение пароля", "", MessageBoxButtons.OK, MessageBoxIcon.Error, 120);
            account.Fields_fill();
        }

    }
}
