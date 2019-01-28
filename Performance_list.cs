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
    public partial class Performance_list : MetroForm
    {
        public int Month_id { get; set; }
        public Performance_list()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Performance f = new Performance(Convert.ToInt32(((Control)sender).Tag), (Afisha)this.Parent.Parent, Month_id);
            f.Show();
            this.Parent.Parent.Hide();
        }

        public void Basic_text()
        {
            Controls.Clear();
            Label l = new Label();
            l.Font = new Font("Century Gothic", 10, FontStyle.Regular);
            l.Location = new Point(219, 140);
            l.ForeColor = Color.Gray;
            l.Text = "Выберите месяц, пъесы по которому вы хотели бы просмотреть";
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.AutoSize = false;
            l.Size = new Size(313, 56);
            Controls.Add(l);
        }

        public void Refresh(int month)
        {
            Month_id = month;
            Controls.Clear();
            int i = 0;
            DataContext db = new DataContext(DB_connection.connectionString);
            var query2 = db.GetTable<TAfisha_dates>()
                        .Where(k => k.Date.Month == Month_id && k.Date >= DateTime.Now)
                        .Select(k => k.Id_performance)
                        .Distinct().ToArray();
            var query1 = db.GetTable<TAfisha>()
                        .Where(k => query2.Contains(k.Id))
                        .Select(k => new { k.Image, k.Name, k.Duration, k.Age_restriction, k.Description, k.Id});

            foreach (var q in query1)
            {
                this.Height += 360;
                Panel p = new Panel();
                Label l1, l2, l3, l4;
                Button b = new Button();
                l1 = new Label();
                l2 = new Label();
                l3 = new Label();
                l4 = new Label();
                string s = DB_connection.current_directory + "images_afisha\\" + q.Image;
                p.BackgroundImage = new Bitmap(@s);
                p.Size = new Size(237, 327);
                p.Location = new Point(50, 18 + i * 360);
                p.BackgroundImageLayout = ImageLayout.Stretch;
                l1.Location = new Point(293, 18 + i * 360);
                l1.Text = q.Name;
                l1.AutoSize = false;
                l1.Size = new Size(319, 56);
                l2.Location = new Point(293, 86 + i * 360);
                l2.AutoSize = true;
                l2.Text = q.Duration;
                l3.Location = new Point(293, 106 + i * 360);
                l3.AutoSize = true;
                l3.Text = q.Age_restriction;
                l4.Location = new Point(293, 149 + i * 360);
                l4.AutoSize = false;
                l4.Size = new Size(402, 146);
                l4.Text = q.Description.Substring(0, 310) + "...";
                l1.Font = new Font("Century Gothic", 12, FontStyle.Bold);
                l2.Font = new Font("Century Gothic", 10, FontStyle.Italic);
                l3.Font = new Font("Century Gothic", 10, FontStyle.Italic);
                l4.Font = new Font("Century Gothic", 10, FontStyle.Regular);
                b.Text = "Узнать больше";
                b.BackColor = Color.Tomato;
                b.FlatStyle = FlatStyle.Flat;
                b.ForeColor = Color.White;
                b.AutoSize = false;
                b.Size = new Size(192, 37);
                b.Location = new Point(297, 308 + i * 360);
                b.Font = new Font("Century", 12, FontStyle.Bold);
                b.Tag = q.Id;
                b.Click += new System.EventHandler(this.button_Click);
                Controls.Add(p);
                Controls.Add(l1);
                Controls.Add(l2);
                Controls.Add(l3);
                Controls.Add(l4);
                Controls.Add(b);
                i++; 
            }
        }
    }
}
