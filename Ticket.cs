using MetroFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_theater
{
    class Ticket
    {
        public static IQueryable<Ticket_sell_info> Find_tickets()
        {
            DataContext db = new DataContext(DB_connection.connectionString);

            IQueryable<Ticket_sell_info> ticket_Sell_Infos = db.GetTable<TTickets>()
                    .Join(db.GetTable<TAfisha_dates>(),
                        tp => tp.Performance_info_id,
                        ap => ap.Id,
                        (tp, ap) => new { Id = ap.Id_performance, ap.Date, ap.Time, tp.Seat, tp.User_Id })
                    .Join(db.GetTable<TAfisha>(),
                      tp => tp.Id,
                      ap => ap.Id,
                      (tp, ap) => new { ap.Name, tp.Date, tp.Time, tp.Seat, ap.Small_image, tp.User_Id })
                  .Where(k => k.User_Id == Program.user.ID)
                  .GroupBy(x => new { x.Name, x.Date, x.Time, x.Small_image })
                  .OrderByDescending(x => x.Key.Date)
                  .Select(x => new Ticket_sell_info { Name = x.Key.Name, Date = x.Key.Date, Time = x.Key.Time, Small_image = x.Key.Small_image, Count = x.Select(d => d.Seat).Count(), Seats = x.Select(d => d.Seat) });

            return ticket_Sell_Infos;
        }

        public static void Show_tickets(Panel panel)
        {
            Label l = new Label();
            l.Text = "Список купленных билетов";
            l.Font = new Font("Century Gothic", 14, FontStyle.Bold);
            l.Location = new Point(173, 25);
            l.AutoSize = true;
            panel.Controls.Add(l);

            IQueryable<Ticket_sell_info> ticket_Sell_Info = Find_tickets();
            int i = 0;
            foreach (var q in ticket_Sell_Info)
            {
                panel.Height += 140;
                Panel p = new Panel();
                Label l1, l2, l3, l4;
                l1 = new Label();
                l2 = new Label();
                l3 = new Label();
                l4 = new Label();
                string s = DB_connection.current_directory + "images_afisha\\" + q.Small_image;

                p.BackgroundImage = new Bitmap(@s);
                p.Size = new Size(105, 105);
                p.Location = new Point(100, 74 + i * 140);
                p.BackgroundImageLayout = ImageLayout.Stretch;
                p.Name = "p" + (i + 1);

                l1.Location = new Point(215, 74 + i * 140);
                string st = "Название:  «" + q.Name + "»";
                l1.Text = st;
                l1.AutoSize = true;
                l1.Font = new Font("Century Gothic", 11, FontStyle.Regular);

                l2.Location = new Point(215, 101 + i * 140);
                st = "Дата: " + Convert.ToDateTime(q.Date).ToShortDateString() + "    Время: " + q.Time;
                l2.Text = st;
                l2.Font = new Font("Century Gothic", 11, FontStyle.Regular);
                l2.AutoSize = true;

                l3.Location = new Point(215, 130 + i * 140);
                st = "Количество билетов: " + q.Count;
                l3.Text = st;
                l3.Font = new Font("Century Gothic", 11, FontStyle.Regular);
                l3.AutoSize = true;

                l4.Location = new Point(215, 159 + i * 140);
                st = "Места: ";
                l4.Font = new Font("Century Gothic", 11, FontStyle.Regular);

                var seats = q.Seats.Where(d => Convert.ToInt32(d) < 44);
                if (seats.ToArray().Count() != 0)
                    st += "Партер: ";
                var seats1 = seats.Where(d => Convert.ToInt32(d) < 12);
                if (seats1.ToArray().Length != 0)
                    st += "(1 ряд: " + String.Join(", ", seats1) + ")";
                var seats2 = seats.Where(d => Convert.ToInt32(d) >= 12 && Convert.ToInt32(d) < 26);
                if (seats2.ToArray().Count() != 0)
                    st += "(2 ряд: " + String.Join(", ", seats2) + ")";
                var seats3 = seats.Where(d => Convert.ToInt32(d) >= 26);
                if (seats3.ToArray().Count() != 0)
                    st += "(3 ряд: " + String.Join(", ", seats3) + ")";

                seats = q.Seats.Where(d => Convert.ToInt32(d) >= 44);
                if (seats.ToArray().Length != 0)
                    st += "Бельэтаж: ";
                seats1 = seats.Where(d => Convert.ToInt32(d) < 58);
                if (seats1.ToArray().Count() != 0)
                    st += "(1 ряд: " + String.Join(", ", seats1) + ")";
                seats2 = seats.Where(d => Convert.ToInt32(d) >= 58 && Convert.ToInt32(d) < 68);
                if (seats2.ToArray().Count() != 0)
                    st += "(2 ряд: " + String.Join(", ", seats2) + ")";
                seats3 = seats.Where(d => Convert.ToInt32(d) >= 68);
                if (seats3.ToArray().Count() != 0)
                    st += "(3 ряд: " + String.Join(", ", seats3) + ")";
                l4.Text = st;
                l4.AutoSize = true;

                panel.Controls.Add(p);
                panel.Controls.Add(l1);
                panel.Controls.Add(l2);
                panel.Controls.Add(l3);
                panel.Controls.Add(l4);
                i++;
            }
            panel.Size = new Size(panel.Width, 352);
        }

        public static int Ticket_purchase(Panel panel, int perf_info_id, float price, Ticket_purchase form)
        {
            int k = 0;
            TTickets ticket;
            DataContext db = new DataContext(DB_connection.connectionString);
            for (int i = 0; i < panel.Controls.Count; i++)
            {
                if (panel.Controls["button" + (i + 1)].BackColor == Color.MediumTurquoise)
                {
                    ticket = new TTickets() { User_Id = Program.user.ID, Performance_info_id = perf_info_id, Seat = i, Price = price };
                    db.GetTable<TTickets>().InsertOnSubmit(ticket);
                    k++;
                    panel.Controls["button" + (i + 1)].BackColor = Color.DarkGray;
                    panel.Controls["button" + (i + 1)].Enabled = false;
                }
            }
            try
            {
                db.SubmitChanges();
            }
            catch(Exception e)
            {
                MetroMessageBox.Show(form, e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
                return 1;
            }
            MetroMessageBox.Show(form, "Билеты были успешно заказаны!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 100);
            return 0;   
        }
    }
}
