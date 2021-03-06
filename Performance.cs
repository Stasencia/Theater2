﻿using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Performance : MetroForm
    {
        public int Month_id { get; set; }
        int perf_id, perf_info_id;
        Afisha a;
        DataContext db = new DataContext(DB_connection.connectionString);
        public Performance(int perf_id, Afisha a, int Month_id)
        {
            InitializeComponent();
            this.perf_id = perf_id;
            this.a = a;
            this.Month_id = Month_id;
        }

        private void Button_customization(object sender, EventArgs args)
        {
            string[] words = ((Control)sender).Tag.ToString().Split(';');
            DateTime d = new DateTime(Convert.ToInt32(words[1]), Convert.ToInt32(words[0]), 1);
            DateTime d1 = new DateTime(Convert.ToInt32(words[1]), Convert.ToInt32(words[0]), 1);
            while (d.DayOfWeek != DayOfWeek.Monday)
            {
                d = d.AddDays(-1);
            }
            for (int i = 0; i < 42; i++)
            {
                Controls["b" + (i + 1)].Text = d.Day.ToString();
                Controls["b" + (i + 1)].Enabled = false;
                Controls["b" + (i + 1)].BackgroundImage = null;
                Controls["b" + (i + 1)].Tag = d.Year + "-" + d.Month + "-" + d.Day;
                d = d.AddDays(1);
            }
            
            var query = db.GetTable<TAfisha>()
                        .Where(k => k.Id == perf_id)
                        .Join(db.GetTable<TAfisha_dates>(),
                              tp => tp.Id,
                              ap => ap.Id_performance,
                              (tp, ap) => new { tp.Small_image, ap.Date, ap.Cancelled })
                              .Where(k => k.Date >=d1 && k.Date >= DateTime.Now && !k.Cancelled);
            var buttons = Controls.OfType<Button>().Where(k => k.Name.StartsWith("b"))
                            .Join(query,
                                button => Convert.ToDateTime(button.Tag).ToShortDateString(),
                                afisha_info => afisha_info.Date.ToShortDateString(),
                                (button, afisha_info) => new { button, afisha_info });
            foreach(var b in buttons)
            {
                string s = DB_connection.current_directory + "images_afisha\\" + b.afisha_info.Small_image;
                b.button.Enabled = true;
                b.button.BackgroundImage = new Bitmap(@s);
                b.button.Tag = b.afisha_info.Date;
            }
        }

        private void Performance_Load(object sender, EventArgs e)
        {
            string s;
            Button push = new Button();
            AutoSize = false;
            Size = new Size(797, 530);

            var query1 = db.GetTable<TAfisha>()
                            .Where(l => l.Id == perf_id)
                            .Select(l => new { l.Big_image, l.Small_name, l.Small_info, l.Duration, l.Age_restriction, l.Description});
            foreach(var q in query1)
            {
                s = DB_connection.current_directory + "images_afisha\\" + q.Big_image;
                panel1.BackgroundImage = new Bitmap(@s);
                label1.Text = q.Small_name;
                label2.Text = q.Small_info;
                label7.Text = q.Duration;
                label8.Text = q.Age_restriction;
                label9.Text = q.Description;
            }
        
            label9.Height = (int)(((label9.Text.Length * label9.Font.SizeInPoints) / label9.Width + label9.Text.Where(x => x == '\n').Count()) * label9.Font.Height);

            var query = db.GetTable<TAfisha_dates>()
                        .Where(l => l.Id_performance == perf_id && l.Date >= DateTime.Now)
                        .Select(l => new { l.Date.Month, l.Date.Year })
                        .Distinct();
            int i = 0;
            Months m;
            foreach(var q in query)
            {
                Button top = new Button();
                top.FlatStyle = FlatStyle.Flat;
                m = (Months)q.Month;
                top.Text = m + "\n" + q.Year;
                top.AutoSize = false;
                top.Size = new Size(82, 49);
                top.Font = new Font("Century Gothic", 10, FontStyle.Regular);
                top.Tag = q.Month + ";" + q.Year;
                if (q.Month == Month_id)
                    push = top;
                top.BringToFront();
                top.Click += new System.EventHandler(this.Button_customization);
                top.Name = "top" + (i + 1);
                top.TabStop = false;
                Controls.Add(top);
                i++;
            }      
                
            int k = (797 - (82 * i + (i - 1) * 22)) / 2;
            for (int j = 0; j < i; j++)
            {
                Controls["top" + (j + 1)].Location = new Point(k + j * 82 + j * 22, label9.Bottom);
            }
        
            for (int j = 0; j< 42; j++)
            {
                Button b = new Button();
                b.Size = new Size(90, 90);
                b.Location = new Point(75 + (89 * (j % 7)), Controls["top1"].Bottom + 15 + (89 * (int) Math.Floor(j / 7.0)));
                b.FlatStyle = FlatStyle.Flat;
                b.TextAlign = ContentAlignment.TopLeft;
                b.BackgroundImageLayout = ImageLayout.Stretch;
                b.MouseEnter += new EventHandler(button_MouseEnter);
                b.MouseLeave += new EventHandler(button_MouseLeave);
                b.Font = new Font("Century Gothic", 9, FontStyle.Regular);
                b.Name = "b" + (j + 1);
                b.Click += new System.EventHandler(this.Day_pushed);
                Controls.Add(b);
            }
            push.TabStop = true;
            push.PerformClick();
            panel2.Size = new Size(15, 15);
            panel2.Location = new Point(0, Controls["b41"].Bottom);
            panel2.BringToFront();
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            Button p = (Button)sender;
            p.Height = 90;
            p.Width = 90;
            p.Location = new Point(p.Location.X + 10, p.Location.Y + 10);
            p.Text = ((DateTime)((Button)sender).Tag).Day.ToString();
            p.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            p.ForeColor = Color.Black;
            p.TextAlign = ContentAlignment.TopLeft;
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            Button p = (Button)sender;
            p.Height = 110;
            p.Width = 110;
            p.Location = new Point(p.Location.X - 10, p.Location.Y - 10);
            DateTime date = Convert.ToDateTime(((Button)sender).Tag);
            var query = db.GetTable<TAfisha_dates>()
                        .Where(l => l.Id_performance == perf_id && l.Date == date)
                        .Select(l => new { l.Date, l.Id}).First();
            p.Text = query.Date.ToShortTimeString();
            p.Font = new Font("Century Gothic", 11, FontStyle.Bold);
            p.ForeColor = Color.White;
            p.TextAlign = ContentAlignment.BottomLeft;
            perf_info_id = Convert.ToInt32(query.Id);
            p.BringToFront();
        }

        private void Performance_Activated(object sender, EventArgs e)
        {
           // panel1.Size = new Size(780, 380);
        }

        private void Performance_Shown(object sender, EventArgs e)
        {
            VerticalScroll.Value = 0;
        }

        private void Performance_FormClosing(object sender, FormClosingEventArgs e)
        {
            a.Show();
        }

        private void Day_pushed(object sender, EventArgs e)
        {
            if (Program.user.ID == 0) 
            {
                MetroMessageBox.Show(this, "Для того, чтобы заказать билеты, Вам необходимо быть авторизированным в системе", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, 120);  
            }
            else
            {
                Ticket_purchase t = new Ticket_purchase(this, perf_info_id);
                t.Show();
                this.Hide();
            }
        }
    }
}
