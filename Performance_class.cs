using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_theater
{
    class Performance_class
    {
        public static int Delete(int ID)
        {
            DataContext db = new DataContext(DB_connection.connectionString);
            TAfisha perf = db.GetTable<TAfisha>().Where(k => k.Id == ID).First();
            perf.Is_relevant = false;
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 1;
            }
            return 0;
        }
    }
}
