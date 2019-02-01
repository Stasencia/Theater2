using MetroFramework;
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
    public partial class Ticket_purchase : MetroForm
    {
        Performance perf_form;
        int perf_info_id;
        double price;
        double initial_price;
        DataContext db = new DataContext(DB_connection.connectionString);
        public Ticket_purchase(Performance form, int info)
        {
            InitializeComponent();
            perf_form = form;
            perf_info_id = info;
        }

        private void Ticket_purchase_FormClosing(object sender, FormClosingEventArgs e)
        {
            perf_form.Show();
        }

        private void Price_count()
        {
            price = 0;
            for(int i = 0; i < panel2.Controls.Count; i++)
            { 
                if(panel2.Controls["button" + (i + 1)].BackColor == Color.MediumTurquoise)
                {
                    if (i < 44)
                        price += initial_price;
                    else
                        price += initial_price - 10;
                    if (checkBox1.Checked)
                        price = price * (float)0.75;
                }
            }
            label4.Text = "Цена: " + price + " грн.";
            if (price == 0)
            {
                button77.Enabled = false;
                button77.BackColor = Color.DarkGray;
            }
        }

        private void Ticket_purchase_Load(object sender, EventArgs e)
        {
            var query1 = db.GetTable<TAfisha_dates>()
                        .Where(l => l.Id == perf_info_id)
                        .Join(db.GetTable<TAfisha>(),
                            a => a.Id_performance,
                            b => b.Id,
                            (a, b) => new { a.Date, a.Time, b.Duration, b.Image, b.Id, b.Price }).First();
            label5.Text = "Дата: " + query1.Date.ToShortDateString();
            label1.Text = "Начало: " + query1.Time;
            label2.Text = query1.Duration;
            string s = DB_connection.current_directory + "images_afisha\\" + query1.Image;
            panel1.BackgroundImage = new Bitmap(@s);
            initial_price = query1.Price;
           
            foreach (Button b in panel2.Controls)
            {
                b.TabStop = false;
                b.Click += new System.EventHandler(this.button_Click);
                b.BackColor = Color.White;
            }
            
            var query = db.GetTable<TTickets>()
                    .Where(k => k.Performance_info_id == perf_info_id)
                    .Select(k => k.Seat);
            foreach(var q in query)
            {
                panel2.Controls["button" + (q + 1)].BackColor = Color.DarkGray;
                panel2.Controls["button" + (q + 1)].Enabled = false;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor == Color.White)
                ((Button)sender).BackColor = Color.MediumTurquoise;
            else
                ((Button)sender).BackColor = Color.White;
            label1.Select();
            button77.Enabled = true;
            button77.BackColor = Color.MediumTurquoise;
            Price_count();
        }

        private void button77_Click(object sender, EventArgs e)
        {
            if(Ticket.Ticket_purchase(panel2, perf_info_id, price, this) == 0)
            {
                label4.Text = "Цена: 0 грн.";
                button77.Enabled = false;
                button77.BackColor = Color.DarkGray; 
            }       
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Price_count();
        }
    }
}
