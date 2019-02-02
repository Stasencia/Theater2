using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_theater
{
    public partial class Editing_performance_list : MetroForm
    {
        DataContext db = new DataContext(DB_connection.connectionString);
        MainForm mainform;
        public Editing_performance_list(MainForm form)
        {
            InitializeComponent();
            mainform = form;
        }

        private void Editing_performance_list_Load(object sender, EventArgs e)
        {
            Refresh_page();
        }

        private void Refresh_page()
        {
            panel1.Controls.Clear();
            int i = 0;
            var query = db.GetTable<TAfisha>().Where(k => k.Is_relevant == true);
            foreach (var q in query)
            {
                panel1.Height += 360;
                Panel p = new Panel();
                Label l1, l2, l3, l4;
                Button b1, b2;
                b1 = new Button();
                b2 = new Button();
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
                b1.Text = "Редактировать";
                b1.BackColor = Color.Teal;
                b1.FlatStyle = FlatStyle.Flat;
                b1.ForeColor = Color.White;
                b1.AutoSize = false;
                b1.Size = new Size(192, 37);
                b1.Location = new Point(299, 312 + i * 360);
                b1.Font = new Font("Century", 12, FontStyle.Bold);
                b1.Tag = q.Id;
                b1.Click += new System.EventHandler(this.Editing_Click);
                b2.Text = "Удалить";
                b2.BackColor = Color.Brown;
                b2.FlatStyle = FlatStyle.Flat;
                b2.ForeColor = Color.White;
                b2.AutoSize = false;
                b2.Size = new Size(192, 37);
                b2.Location = new Point(497, 312 + i * 360);
                b2.Font = new Font("Century", 12, FontStyle.Bold);
                b2.Tag = q.Id;
                b2.Click += new System.EventHandler(this.Deletion_Click);
                panel1.Controls.Add(p);
                panel1.Controls.Add(l1);
                panel1.Controls.Add(l2);
                panel1.Controls.Add(l3);
                panel1.Controls.Add(l4);
                panel1.Controls.Add(b1);
                panel1.Controls.Add(b2);
                i++;
            }
            panel1.Size = new Size(763, 371);
        }

        public void Editing_Click(object sender, System.EventArgs e)
        {

        }

        public void Deletion_Click(object sender, System.EventArgs e)
        {
            if (Performance_class.Delete(Convert.ToInt32(((Button)sender).Tag)) == 0)
            {
                MetroMessageBox.Show(this, "Удаление было выполнено успешно");
                Refresh_page();
            }
        }

        private void Editing_performance_list_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainform.Show();
        }
    }
}
