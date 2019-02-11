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
            IQueryable<TAfisha_dates> dates = db.GetTable<TAfisha_dates>().Where(k => k.Id_performance == ID && k.Date > DateTime.Now);
            foreach(TAfisha_dates d in dates)
            {
                d.Cancelled = true;
            }
            TAfisha performance = db.GetTable<TAfisha>().Where(k => k.Id == ID).First();
            performance.Is_relevant = false;
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

        public static int Update(int ID, TAfisha performance)
        {
            DataContext db = new DataContext(DB_connection.connectionString);
            TAfisha perf = db.GetTable<TAfisha>().Where(k => k.Id == ID).First();
            perf = performance;
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
