using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_theater
{
    class DB_connection
    {
        public static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Stasia\Desktop\Project_theater\DB_Theater.mdf;Integrated Security=True";
        public static string current_directory = Environment.CurrentDirectory + "\\";
    }
}
