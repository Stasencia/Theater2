using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Project_theater
{
    public partial class test : UserControl
    {
        public test()
        {
            InitializeComponent();
        }

        private async void test_Load(object sender, EventArgs e)
        {
            Label l = new Label();
            l.Text = "Список купленных билетов";
            l.Font = new Font("Century Gothic", 14, FontStyle.Bold);
            l.Location = new Point(173, 25);
            l.AutoSize = true; 
            Controls.Add(l);

            DataSet ds = new DataSet("Theater");
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlDataAdapter adapter1 = new SqlDataAdapter("SELECT * FROM Tickets", connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT * FROM Afisha", connection);
                SqlDataAdapter adapter3 = new SqlDataAdapter("SELECT * FROM Afisha_dates", connection);

                adapter1.Fill(ds, "Tickets");
                adapter2.Fill(ds, "Afisha");
                adapter3.Fill(ds, "Afisha_dates");
            }
            var q3 = (from x in ds.Tables["Tickets"].AsEnumerable()
                      join y in ds.Tables["Afisha_dates"].AsEnumerable()
                      on new { Perf_Id = x["Performance_id"], Date = x["Date"] } equals new { Perf_Id = y["Id_performance"], Date = y["Date"] }
                      select new { Id = x["Performance_id"], Date = y["Date"], Time = y["Time"], Seat = x["Seat"], UserId = x["User_Id"] }
                         )
                         .Join(ds.Tables["Afisha"].AsEnumerable(),
                            tp => tp.Id,
                            ap => ap["Id"],
                            (tp, ap) => new { Name = ap["Name"], Date = tp.Date, Time = tp.Time, Seat = tp.Seat, Sm_img = ap["Small_image"], UserId = tp.UserId })
                        .Where(k => Convert.ToInt32(k.UserId) == Program.user.ID)
                        .GroupBy(x => new { x.Name, x.Date, x.Time, x.Sm_img })
                            .Select(x => new { Key = x.Key, Count = x.Select(d => d.Seat).Count(), Seats = x.Select(d => d.Seat) });
            int i = 0;
            //    foreach (var q in q3)
            //    {
            //        this.Height += 140;
            //        Panel p = new Panel();
            //        Label l1, l2, l3, l4;
            //        l1 = new Label();
            //        l2 = new Label();
            //        l3 = new Label();
            //        l4 = new Label();
            //        string s = DB_connection.current_directory + "images_afisha\\" + q.Key.Sm_img;

            //        p.BackgroundImage = new Bitmap(@s);
            //        p.Size = new Size(105, 105);
            //        p.Location = new Point(100, 74 + i * 140);
            //        p.BackgroundImageLayout = ImageLayout.Stretch;
            //        p.Name = "p" + (i + 1);

            //        l1.Location = new Point(215, 74 + i * 140);
            //        string st = "Название:  «" + q.Key.Name + "»";
            //        l1.Text = st;
            //        l1.AutoSize = true;
            //        l1.Font = new Font("Century Gothic", 11, FontStyle.Regular);

            //        l2.Location = new Point(215, 101 + i * 140);
            //        st = "Дата: " + Convert.ToDateTime(q.Key.Date).ToShortDateString() + "    Время: " + q.Key.Time;
            //        l2.Text = st;
            //        l2.Font = new Font("Century Gothic", 11, FontStyle.Regular);
            //        l2.AutoSize = true;

            //        l3.Location = new Point(215, 130 + i * 140);
            //        st = "Количество билетов: " + q.Count;
            //        l3.Text = st;
            //        l3.Font = new Font("Century Gothic", 11, FontStyle.Regular);
            //        l3.AutoSize = true;

            //        l4.Location = new Point(215, 159 + i * 140);
            //        st = "Места: ";
            //        l4.Font = new Font("Century Gothic", 11, FontStyle.Regular);              

            //        var seats = q.Seats.Where(d => Convert.ToInt32(d) < 44);
            //        if (seats.ToArray().Count() != 0)
            //            st += "Партер: ";
            //        var seats1 = seats.Where(d => Convert.ToInt32(d) < 12);
            //        if (seats1.ToArray().Length != 0)
            //            st += "(1 ряд: " + String.Join(", ", seats1) + ")";
            //        var seats2 = seats.Where(d => Convert.ToInt32(d) >= 12 && Convert.ToInt32(d) < 26);
            //        if (seats2.ToArray().Count() != 0)
            //            st += "(2 ряд: " + String.Join(", ", seats2) + ")";
            //        var seats3 = seats.Where(d => Convert.ToInt32(d) >= 26);
            //        if (seats3.ToArray().Count() != 0)
            //            st += "(3 ряд: " + String.Join(", ", seats3) + ")";

            //        seats = q.Seats.Where(d => Convert.ToInt32(d) >= 44);
            //        if (seats.ToArray().Length != 0)
            //            st += "Бельэтаж: ";
            //        seats1 = seats.Where(d => Convert.ToInt32(d) < 58);
            //        if (seats1.ToArray().Count() != 0)
            //            st += "(1 ряд: " + String.Join(", ", seats1) + ")";
            //        seats2 = seats.Where(d => Convert.ToInt32(d) >= 58 && Convert.ToInt32(d) < 68);
            //        if (seats2.ToArray().Count() != 0)
            //            st += "(2 ряд: " + String.Join(", ", seats2) + ")";
            //        seats3 = seats.Where(d => Convert.ToInt32(d) >= 68);
            //        if (seats3.ToArray().Count() != 0)
            //            st += "(3 ряд: " + String.Join(", ", seats3) + ")";
            //        l4.Text = st;
            //        l4.AutoSize = true;

            //        Controls.Add(p);
            //        Controls.Add(l1);
            //        Controls.Add(l2);
            //        Controls.Add(l3);
            //        Controls.Add(l4);
            //        i++;
            //    }
            //    this.Size = new Size(this.Width, 352);
        }
    }
}
