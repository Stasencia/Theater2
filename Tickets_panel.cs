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
    public partial class Tickets_panel : UserControl
    {
        public Tickets_panel()
        {
            InitializeComponent();
        }

        private async void Tickets_panel_Load(object sender, EventArgs e)
        {
            this.AutoSize = false;
            this.Size = new Size(586, 226);
            Controls.Clear();
            Label l = new Label();
            l.Location = new Point(173, 35);
            l.Text = "Список купленных билетов";
            l.AutoSize = false;
            l.Size = new Size(270, 23);
            l.Font = new Font("Century Gothic", 14, FontStyle.Bold);
            Controls.Add(l);
            this.Dock = DockStyle.None;
            this.AutoScroll = true;
            this.VerticalScroll.Enabled = true;
            
            //  this.VerticalScroll.Maximum = this.Height;
            // this.VerticalScroll.Value = this.Height - 100;
            DataSet ds = new DataSet("Theater");
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlDataAdapter adapter1 = new SqlDataAdapter("SELECT * FROM Tickets", connection);
                SqlDataAdapter adapter2= new SqlDataAdapter("SELECT * FROM Afisha", connection);
                SqlDataAdapter adapter3 = new SqlDataAdapter("SELECT * FROM Afisha_dates", connection);

               
                adapter1.Fill(ds, "Tickets");
                adapter2.Fill(ds, "Afisha");
                adapter3.Fill(ds, "Afisha_dates");

               /* var q1 = ds.Tables["Tickets"].AsEnumerable()
                            .Join(ds.Tables["Afisha_dates"].AsEnumerable(),
                            tp => tp["Performance_id"],
                            ap => ap["Id_performance"],
                            (tp, ap) => new { Id = tp["Performance_id"], Date = ap["Date"], Time = ap["Time"], Seat = tp["Seat"], UserId = tp["User_Id"] })
                            .Join(ds.Tables["Afisha"].AsEnumerable(),
                            tp => tp.Id,
                            ap => ap["Id"],
                            (tp, ap) => new { Name = ap["Name"], Date = tp.Date, Time = tp.Time, Seat = tp.Seat, Sm_img = ap["Small_image"], UserId = tp.UserId })
                            .Where(k => Convert.ToInt32(k.UserId) == User.ID);

                var q2 = (from x in ds.Tables["Tickets"].AsEnumerable()
                         join y in ds.Tables["Afisha_dates"].AsEnumerable()
                         on new { Perf_Id = x["Performance_id"], Date = x["Date"] } equals new { Perf_Id = y["Id_performance"], Date = y["Date"] }
                         select new { Id = x["Performance_id"], Date = y["Date"], Time = y["Time"], Seat = x["Seat"], UserId = x["User_Id"] }
                         ).Join(ds.Tables["Afisha"].AsEnumerable(),
                            tp => tp.Id,
                            ap => ap["Id"],
                            (tp, ap) => new { Name = ap["Name"], Date = tp.Date, Time = tp.Time, Seat = tp.Seat, Sm_img = ap["Small_image"], UserId = tp.UserId })
                            .Where(k => Convert.ToInt32(k.UserId) == User.ID);*/              
            }
            var q3 = (from x in ds.Tables["Tickets"].AsEnumerable()
                      join y in ds.Tables["Afisha_dates"].AsEnumerable()
                      on new { Perf_Id = x["Performance_id"], Date = x["Date"] } equals new { Perf_Id = y["Id_performance"], Date = y["Date"] }
                      select new { Id = x["Performance_id"], Date = y["Date"], Time = y["Time"], Seat = x["Seat"], UserId = x["User_Id"] }
                         ).Join(ds.Tables["Afisha"].AsEnumerable(),
                            tp => tp.Id,
                            ap => ap["Id"],
                            (tp, ap) => new { Name = ap["Name"], Date = tp.Date, Time = tp.Time, Seat = tp.Seat, Sm_img = ap["Small_image"], UserId = tp.UserId })
                            .Where(k => Convert.ToInt32(k.UserId) == Program.user.ID)
                            .GroupBy(x => new { x.Name, x.Date, x.Time, x.Sm_img })
                            .Select(x => new { Key = x.Key, Count = x.Select(d => d.Seat).Count(), Seats = x.Select(d => d.Seat) });
            int i = 0;
            foreach (var q in q3)
            {
                this.Height += 240;
                Panel p = new Panel();
                Label l1, l2, l3, l4, l5;
                l1 = new Label();
                l2 = new Label();
                l3 = new Label();
                l4 = new Label();
                l5 = new Label();
                string s = DB_connection.current_directory + "images_afisha\\" + q.Key.Sm_img;
                p.BackgroundImage = new Bitmap(@s);
                p.Size = new Size(105, 105);
                p.Location = new Point(104, 84 + i * 140);
                p.BackgroundImageLayout = ImageLayout.Stretch;
                p.Name = "p" + (i + 1);

                l1.Location = new Point(220, 84 + i * 140);
                l1.Text = "Название:  «" + q.Key.Name + "»";
                l1.AutoSize = false;
                l1.Font = new Font("Century Gothic", 11, FontStyle.Regular);
                l1.Width = (int)(l1.Text.Length * l1.Font.SizeInPoints);

                l2.Location = new Point(220, 111 + i * 140);
                l2.Text = "Дата: " + Convert.ToDateTime(q.Key.Date).ToShortDateString();
                l2.Font = new Font("Century Gothic", 11, FontStyle.Regular);
                l2.AutoSize = false;
                l2.Width = (int)(l2.Text.Length * l2.Font.SizeInPoints) - 50;

                l3.Location = new Point(363, 111 + i * 140);
                l3.Text = "Время: " + q.Key.Time;
                l3.Font = new Font("Century Gothic", 11, FontStyle.Regular);
                l3.AutoSize = false;
                l3.Width = (int)(l3.Text.Length * l3.Font.SizeInPoints);

                l4.Location = new Point(220, 140 + i * 140);
                l4.Text = "Количество билетов: " + q.Count;
                l4.Font = new Font("Century Gothic", 11, FontStyle.Regular);
                l4.AutoSize = false;
                l4.Width = (int)(l4.Text.Length * l4.Font.SizeInPoints);

                l5.Location = new Point(220, 169 + i * 140);
                l5.Text = "Места: ";
                l5.Font = new Font("Century Gothic", 11, FontStyle.Regular);
                l5.AutoSize = false;

                var seats = q.Seats.Where(d => Convert.ToInt32(d) < 44);
                if (seats.ToArray().Count() != 0)
                    l5.Text += "Партер: ";
                var seats1 = seats.Where(d => Convert.ToInt32(d) < 12);
                if (seats1.ToArray().Length != 0)
                    l5.Text += "(1 ряд: " + String.Join(", ", seats1) + ")";
                var seats2 = seats.Where(d => Convert.ToInt32(d) >= 12 && Convert.ToInt32(d) < 26);
                if (seats2.ToArray().Count() != 0)
                    l5.Text += "(2 ряд: " + String.Join(", ", seats2) + ")";
                var seats3 = seats.Where(d => Convert.ToInt32(d) >= 26);
                if (seats3.ToArray().Count() != 0)
                    l5.Text += "(3 ряд: " + String.Join(", ", seats3) + ")";

                seats = q.Seats.Where(d => Convert.ToInt32(d) >= 44);
                if (seats.ToArray().Length != 0)
                    l5.Text += "Бельэтаж: ";
                seats1 = seats.Where(d => Convert.ToInt32(d) < 58);
                if (seats1.ToArray().Count() != 0)
                    l5.Text += "(1 ряд: " + String.Join(", ", seats1) + ")";
                seats2 = seats.Where(d => Convert.ToInt32(d) >= 58 && Convert.ToInt32(d) < 68);
                if (seats2.ToArray().Count() != 0)
                    l5.Text += "(2 ряд: " + String.Join(", ", seats2) + ")";
                seats3 = seats.Where(d => Convert.ToInt32(d) >= 68);
                if (seats3.ToArray().Count() != 0)
                    l5.Text += "(3 ряд: " + String.Join(", ", seats3) + ")";
                l5.Width = (int)(l5.Text.Length * l5.Font.SizeInPoints);

                Controls.Add(p);
                Controls.Add(l1);
                Controls.Add(l2);
                Controls.Add(l3);
                Controls.Add(l4);
                Controls.Add(l5);
                i++;
            }

            this.VerticalScroll.Visible = true;
        }

        private void Tickets_panel_Enter(object sender, EventArgs e)
        {
            //VerticalScroll.Visible = true;
        }
    }
}
