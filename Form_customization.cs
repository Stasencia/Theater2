﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_theater
{
    class Form_customization
    {
        public static void Top_buttons(Form form)
        {
            /*Months m;
            DataContext db = new DataContext(DB_connection.connectionString);
            var query2 = db.GetTable<TAfisha_dates>()
                         .Where(k => k.Date >= DateTime.Now)
                         .GroupBy(row =>
                            new
                            {
                                Year = row.Date.Year,
                                Month = row.Date.Month
                            },
                            (key, group) => new
                            {
                                key1 = key.Year,
                                key2 = key.Month
                            })
                            .OrderBy(k => k.key1).ThenBy(k => k.key2)
                            .Take(4);

            foreach (var q in query2)
            {
                Button b = new Button();
                b.FlatStyle = FlatStyle.Flat;
                m = (Months)q.key2;
                b.Text = m + "\n" + q.key1;
                b.AutoSize = false;
                b.Size = new Size(82, 49);
                b.Font = new Font("Century Gothic", 10, FontStyle.Regular);
                b.Tag = q.key2;
                b.BringToFront();
                b.Click += new System.EventHandler(button1_Click);
                form.panel1.Controls.Add(b);
                i++;
                form.
            }
            form.GetType();
            int j = 0;
            int o = (797 - (82 * i + (i - 1) * 22)) / 2;
            foreach (Button b in panel1.Controls)
            {
                panel1.Controls[j].Location = new Point(o + j * 82 + j * 22, 7);
                j++;
            }*/
        }
    }
}
