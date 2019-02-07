﻿using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_theater
{
    public partial class Editing_performance : MetroForm
    {
        TAfisha changed_performance;
        Editing_performance_list editing_Performance_List;
        int performance_id;
        DataContext db = new DataContext(DB_connection.connectionString);

        public Editing_performance(Editing_performance_list form, int id)
        {
            InitializeComponent();
            editing_Performance_List = form;
            performance_id = id;
        }

        private void Editing_performance_Load(object sender, EventArgs e)
        {
            panel_Images_Load();
            panel_Text_Load();
            panel_Dates_Load();
            button1.PerformClick();
        }

        private void Editing_performance_FormClosing(object sender, FormClosingEventArgs e)
        {
            editing_Performance_List.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controls["panel_Images"].BringToFront();
        }

        private void panel_Images_Load()
        {
            Panel panel_Images = new Panel();
            panel_Images.Name = "panel_Images";
            panel_Images.Location = new Point(-1, 124);
            panel_Images.Size = new Size(800, 349);
            panel_Images.AutoScroll = true;
            this.Controls.Add(panel_Images);
            string s;
            Panel p1, p2, p3;
            p1 = new Panel();
            p2 = new Panel();
            p3 = new Panel();
            p1.Name = "Medium";
            p2.Name = "Small";
            p3.Name = "Large";
            Label l1, l2, l3;
            l1 = new Label();
            l2 = new Label();
            l3 = new Label();
            l1.Text = "Средняя афиша";
            l2.Text = "Маленькая афиша";
            l3.Text = "Большая афиша";
            l1.Font = new Font("Century Gothic", 10, FontStyle.Regular);
            l2.Font = new Font("Century Gothic", 10, FontStyle.Regular);
            l3.Font = new Font("Century Gothic", 10, FontStyle.Regular);
            l1.Location = new Point(171, 22);
            l2.Location = new Point(480, 22);
            l3.Location = new Point(343, 362);
            l1.AutoSize = true;
            l2.AutoSize = true;
            l3.AutoSize = true;
            p1.Click += panel_Click;
            p2.Click += panel_Click;
            p3.Click += panel_Click;
            var query = db.GetTable<TAfisha>().Where(k => k.Id == performance_id)
                        .Select(k => new { k.Image, k.Big_image, k.Small_image }).First();
            s = DB_connection.current_directory + "images_afisha\\" + query.Image;
            p1.BackgroundImage = new Bitmap(@s);
            p1.BackgroundImageLayout = ImageLayout.Stretch;
            p1.Location = new Point(123, 42);
            p1.Size = new Size(210, 281);
            s = DB_connection.current_directory + "images_afisha\\" + query.Small_image;
            p2.BackgroundImage = new Bitmap(@s);
            p2.BackgroundImageLayout = ImageLayout.Stretch;
            p2.Size = new Size(140, 136);
            p2.Location = new Point(484, 105);
            s = DB_connection.current_directory + "images_afisha\\" + query.Big_image;
            p3.BackgroundImage = new Bitmap(@s);
            p3.BackgroundImageLayout = ImageLayout.Stretch;
            p3.Location = new Point(87, 382);
            p3.Size = new Size(636, 281);
            panel_Images.Controls.Add(p1);
            panel_Images.Controls.Add(p2);
            panel_Images.Controls.Add(p3);
            panel_Images.Controls.Add(l1);
            panel_Images.Controls.Add(l2);
            panel_Images.Controls.Add(l3);
        }

        public void panel_Text_Load()
        {
            Panel panel_Text = new Panel();
            panel_Text.Name = "panel_Text";
            panel_Text.Location = new Point(-1, 124);
            panel_Text.Size = new Size(800, 349);
            panel_Text.AutoScroll = true;
            this.Controls.Add(panel_Text);

            NumericUpDown numericUpDown = new NumericUpDown();
            numericUpDown.Name = "Price";
            numericUpDown.Font = new Font("Century Gothic", 12, FontStyle.Regular);
            numericUpDown.Size = new Size(90, 27);
            numericUpDown.Maximum = 10000;
            numericUpDown.DecimalPlaces = 2;
            numericUpDown.Location = new Point(184, 51);
            panel_Text.Controls.Add(numericUpDown);
            TextBox[] tb = new TextBox[6];
            for (int i = 0; i < 6; i++)
            {
                tb[i] = new TextBox();
                tb[i].Multiline = true;
                tb[i].ScrollBars = ScrollBars.Vertical;
                tb[i].Font = new Font("Century Gothic", 10, FontStyle.Regular);
                panel_Text.Controls.Add(tb[i]);
            }
            tb[0].Name = "Name";
            tb[1].Name = "Small_name";
            tb[2].Name = "Small_info";
            tb[3].Name = "Duration";
            tb[4].Name = "Age_restriction";
            tb[5].Name = "Description";
            tb[0].Font = new Font("Century Gothic", 12, FontStyle.Bold);
            tb[0].Size = new Size(357, 29);
            tb[1].Size = new Size(357, 23);
            tb[2].Size = new Size(357, 58);
            tb[3].Size = new Size(357, 25);
            tb[4].Size = new Size(357, 25);
            tb[5].Size = new Size(576, 208);

            var q = db.GetTable<TAfisha>().Where(k => k.Id == performance_id).First();
            tb[0].Text = q.Name;
            numericUpDown.Value = Convert.ToDecimal(q.Price);
            tb[1].Text = q.Small_name;
            tb[2].Text = q.Small_info;
            tb[3].Text = q.Duration;
            tb[4].Text = q.Age_restriction;
            tb[5].Text = q.Description;
            tb[0].Location = new Point(184, 9);
            tb[1].Location = new Point(184, 95);
            tb[2].Location = new Point(184, 132);
            tb[3].Location = new Point(184, 206);
            tb[4].Location = new Point(184, 245);
            tb[5].Location = new Point(184, 283);

            Label[] l = new Label[7];
            for (int i = 0; i < 7; i++)
            {
                l[i] = new Label();
                l[i].AutoSize = true;
                l[i].Font = new Font("Century Gothic", 12, FontStyle.Regular);
                l[i].ForeColor = Color.DarkGray;
                panel_Text.Controls.Add(l[i]);
            }
            l[0].Location = new Point(10, 11);
            l[1].Location = new Point(10, 52);
            l[2].Location = new Point(10, 94);
            l[3].Location = new Point(10, 132);
            l[4].Location = new Point(10, 206);
            l[5].Location = new Point(10, 244);
            l[6].Location = new Point(10, 283);
            l[0].Text = "Название:";
            l[1].Text = "Цена:";
            l[2].Text = "Краткое описание:";
            l[3].Text = "Дополнительная\nинформация:";
            l[4].Text = "Продолжительность:";
            l[5].Text = "Ограничение:";
            l[6].Text = "Полное описание:";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Controls["panel_Text"].BringToFront();
        }

        private void panel_Dates_Load()
        {
            /* Panel panel_Dates = new Panel();
             panel_Dates.Name = "panel_Text";
             panel_Dates.Location = new Point(-1, 124);
             panel_Dates.Size = new Size(800, 349);
             panel_Dates.AutoScroll = true;
             this.Controls.Add(panel_Dates);*/
            panel_Dates.Controls.Clear();
            /*dateTimePicker1.CustomFormat = "MMMM yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;*/
            var query = db.GetTable<TAfisha_dates>()
                      .Where(l => l.Id_performance == performance_id && l.Date >= DateTime.Now)
                      .Select(l => new { l.Date.Month, l.Date.Year })
                      .Distinct();
            if (query.Any())
            {
                int i = 0;
                Months m;
                foreach (var q in query)
                {
                    Button top = new Button();
                    top.FlatStyle = FlatStyle.Flat;
                    m = (Months)q.Month;
                    top.Text = m + "\n" + q.Year;
                    top.AutoSize = false;
                    top.Size = new Size(82, 49);
                    top.Font = new Font("Century Gothic", 10, FontStyle.Regular);
                    top.Tag = q.Month + ";" + q.Year;
                    top.BringToFront();
                    top.Click += new System.EventHandler(this.Days_show);
                    top.Name = "top" + (i + 1);
                    top.TabStop = false;
                    panel_Dates.Controls.Add(top);
                    i++;
                }

                int k = (800 - (82 * i + (i - 1) * 22)) / 2;
                for (int j = 0; j < i; j++)
                {
                    panel_Dates.Controls["top" + (j + 1)].Location = new Point(k + j * 82 + j * 22, 6);
                }

                for (int j = 0; j < 42; j++)
                {
                    Button b = new Button();
                    b.Size = new Size(90, 90);
                    b.Location = new Point(75 + (89 * (j % 7)), panel_Dates.Controls["top1"].Bottom + 15 + (89 * (int)Math.Floor(j / 7.0)));
                    b.FlatStyle = FlatStyle.Flat;
                    b.TextAlign = ContentAlignment.TopLeft;
                    b.BackgroundImageLayout = ImageLayout.Stretch;
                    b.Font = new Font("Century Gothic", 9, FontStyle.Regular);
                    b.Name = "b" + (j + 1);
                    b.Click += new System.EventHandler(this.Day_pushed);
                    panel_Dates.Controls.Add(b);
                }
            }
            else
            {
                Label l = new Label();
                l.Text = "Для этого представления нет доступных дат";
                l.Font = new Font("Century Gothic", 12, FontStyle.Regular);
                l.AutoSize = true;
                l.Location = new Point(222, 164);
                l.ForeColor = Color.DarkGray;
               
                panel_Dates.Controls.Add(l);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Controls["panel_Dates"].BringToFront();
            panel_Dates.BringToFront();
        }
  
        private void panel_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Bitmap image = new Bitmap(open_dialog.FileName);
                    ((Panel)sender).BackgroundImage = image;
                    ((Panel)sender).BackgroundImage.Tag = open_dialog.FileName;
                }
                catch
                {
                    DialogResult result = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            changed_performance = db.GetTable<TAfisha>().Where(k => k.Id == performance_id).First();
            changed_performance.Name = Controls["panel_Text"].Controls["Name"].Text;
            changed_performance.Price = Convert.ToDouble(((NumericUpDown)Controls["panel_Text"].Controls["Price"]).Value);
            changed_performance.Small_name = Controls["panel_Text"].Controls["Small_name"].Text;
            changed_performance.Small_info = Controls["panel_Text"].Controls["Small_info"].Text;
            changed_performance.Duration = Controls["panel_Text"].Controls["Duration"].Text;
            changed_performance.Age_restriction = Controls["panel_Text"].Controls["Age_restriction"].Text;
            changed_performance.Description = Controls["panel_Text"].Controls["Description"].Text;
        /*    try
            {
                db.SubmitChanges();
            }
            catch (Exception exc)
            {
                MetroMessageBox.Show(this, exc.Message);
            }*/
            /* string[] words = panel1.BackgroundImage.Tag.ToString().Split('\\');
             string s = DB_connection.current_directory + "images_afisha\\" + words[words.ToList().Count - 1];
             File.Delete();
             File.Copy(panel1.BackgroundImage.Tag.ToString(), s);
             */
        }

        private void Days_show(object sender, EventArgs args)
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
                        .Where(k => k.Id == performance_id)
                        .Join(db.GetTable<TAfisha_dates>(),
                              tp => tp.Id,
                              ap => ap.Id_performance,
                              (tp, ap) => new { tp.Small_image, ap.Date })
                              .Where(k => k.Date >= d1 && k.Date >= DateTime.Now);
            var buttons = Controls.OfType<Button>().Where(k => k.Name.StartsWith("b"))
                            .Join(query,
                                button => Convert.ToDateTime(button.Tag),
                                afisha_info => afisha_info.Date,
                                (button, afisha_info) => new { button, afisha_info });
            foreach (var b in buttons)
            {
                string s = DB_connection.current_directory + "images_afisha\\" + b.afisha_info.Small_image;
                b.button.Enabled = true;
                b.button.BackgroundImage = new Bitmap(@s);
            }
        }

        private void Day_pushed(object sender, EventArgs e)
        {

        }
    }
}
