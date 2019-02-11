using MetroFramework;
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
        int performance_id, month_number = 0;
        public int Month_number
        {
            get { return month_number; }
            set
            {
                month_number = value;
                On_month_number_Changed();   
            }
        }

        DataContext db = new DataContext(DB_connection.connectionString);

        public Editing_performance(Editing_performance_list form, int id)
        {
            InitializeComponent();
            editing_Performance_List = form;
            performance_id = id;
        }

        private void Editing_performance_Load(object sender, EventArgs e)
        {
            changed_performance = db.GetTable<TAfisha>().Where(k => k.Id == performance_id).First();
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
            s = DB_connection.current_directory + "images_afisha\\" + changed_performance.Image;
            p1.BackgroundImage = new Bitmap(@s);
            p1.BackgroundImage.Tag = s;
            p1.BackgroundImageLayout = ImageLayout.Stretch;
            p1.Location = new Point(123, 42);
            p1.Size = new Size(210, 281);
            s = DB_connection.current_directory + "images_afisha\\" + changed_performance.Small_image;
            p2.BackgroundImage = new Bitmap(@s);
            p2.BackgroundImage.Tag = s;
            p2.BackgroundImageLayout = ImageLayout.Stretch;
            p2.Size = new Size(140, 136);
            p2.Location = new Point(484, 105);
            s = DB_connection.current_directory + "images_afisha\\" + changed_performance.Big_image;
            p3.BackgroundImage = new Bitmap(@s);
            p3.BackgroundImage.Tag = s;
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

            tb[0].Text = changed_performance.Name;
            numericUpDown.Value = Convert.ToDecimal(changed_performance.Price);
            tb[1].Text = changed_performance.Small_name;
            tb[2].Text = changed_performance.Small_info;
            tb[3].Text = changed_performance.Duration;
            tb[4].Text = changed_performance.Age_restriction;
            tb[5].Text = changed_performance.Description;
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
            //Сама панель
            Panel panel_Dates = new Panel();
            panel_Dates.Name = "panel_Dates";
            panel_Dates.Location = new Point(-1, 124);
            panel_Dates.Size = new Size(800, 349);
                // panel_Dates.AutoScroll = true;
            this.Controls.Add(panel_Dates);
            //
            //Элементы интерфейса для добавления месяцев
            Button add_month = new Button();
            add_month.Name = "Add_month";
            add_month.FlatStyle = FlatStyle.Flat;
            add_month.BackColor = Color.Teal;
            add_month.ForeColor = Color.White;
            add_month.Text = "Добавить месяц";
            add_month.AutoSize = false;
            add_month.Size = new Size(135, 49);
            string s = DB_connection.current_directory + "icons\\plus.png";
            add_month.Image = new Bitmap(@s);
            add_month.ImageAlign = ContentAlignment.MiddleLeft;
            add_month.TextImageRelation = TextImageRelation.ImageBeforeText;
            add_month.Font = new Font("Century Gothic", 10, FontStyle.Regular);
            add_month.Location = new Point((panel_Dates.Width - add_month.Width) / 2, 6);
            add_month.Click += new System.EventHandler(Add_month_button_pushed);
            panel_Dates.Controls.Add(add_month);
            DateTimePicker dateTimePicker = new DateTimePicker();
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Width = add_month.Width;
            dateTimePicker.Location = new Point((panel_Dates.Width - dateTimePicker.Width) / 2, add_month.Bottom + 2);
            dateTimePicker.Font = new Font("Century Gothic", 10, FontStyle.Regular);
            dateTimePicker.CustomFormat = "MMMM yyyy";
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.MinDate = DateTime.Now;
            panel_Dates.Controls.Add(dateTimePicker);
            //
            //Запрос на нахождение всех доступных месяцев для текущего представления
            var query = db.GetTable<TAfisha_dates>()
                      .Where(l => l.Id_performance == performance_id && l.Date >= DateTime.Now && !l.Cancelled)
                      .Select(l => new { l.Date.Month, l.Date.Year })
                      .Distinct();
            //
            //Создание кнопок месяцев или же размещение заголовка, что доступных дат нет
            if (query.Any())
            {
                foreach (var q in query)
                {
                    AddMonth(q.Month, q.Year);
                }
                add_month.Location = new Point(panel_Dates.Controls["top" + Month_number].Right + 6, 6);
                ((Button)panel_Dates.Controls["top1"]).PerformClick();
                ((Button)panel_Dates.Controls["top1"]).Focus();
            }
            else
            {
                Label l = new Label();
                l.Name = "Label_no_dates";
                l.Text = "Для этого представления нет доступных дат";
                l.Font = new Font("Century Gothic", 12, FontStyle.Regular);
                l.AutoSize = true;
                l.Location = new Point(222, 164);
                l.ForeColor = Color.DarkGray;
                panel_Dates.Controls.Add(l);
            }
            //
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Controls["panel_Dates"].BringToFront();
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

        private void OnMonthButtonPushed(object sender, EventArgs args)
        {
            Panel p = Controls["panel_Dates"].Controls.OfType<Panel>().Where(k => k.Tag == sender).First();
               // ((Button)sender).Controls.OfType<Panel>().First();
            p.BringToFront();
        }

        private void Customize_days(string panel_name)
        {
            Panel p = (Panel)Controls["panel_Dates"].Controls[panel_name];
            DateTime d = Convert.ToDateTime(((Button)p.Tag).Tag);
            DateTime d1 = Convert.ToDateTime(((Button)p.Tag).Tag);
            while (d.DayOfWeek != DayOfWeek.Monday)
            {
                d = d.AddDays(-1);
            }
            for (int i = 0; i < 42; i++)
            {
                p.Controls["b" + (i + 1)].Text = d.Day.ToString();
                p.Controls["b" + (i + 1)].Enabled = true;
                if (((DateTime)((Button)p.Tag).Tag).Month != d.Month || d.AddDays(1) < DateTime.Now)
                    p.Controls["b" + (i + 1)].Enabled = false;
                p.Controls["b" + (i + 1)].BackgroundImage = null;
                p.Controls["b" + (i + 1)].Visible = true;
                p.Controls["b" + (i + 1)].Tag = d.Year + "-" + d.Month + "-" + d.Day;
                ((DateTimePicker)p.Controls["b" + (i + 1)].Controls["TimePicker"]).Value = d;
                p.Controls["b" + (i + 1)].Controls.OfType<DateTimePicker>().First().Visible = false;
                d = d.AddDays(1);
            }
            var query = db.GetTable<TAfisha>()
                        .Where(k => k.Id == performance_id)
                        .Join(db.GetTable<TAfisha_dates>(),
                              tp => tp.Id,
                              ap => ap.Id_performance,
                              (tp, ap) => new { tp.Small_image, ap.Date, ap.Cancelled })
                              .Where(k => k.Date >= d1 && k.Date >= DateTime.Now && !k.Cancelled);
            var buttons = p.Controls.OfType<Button>().Where(k => k.Name.StartsWith("b"))
                            .Join(query,
                                button => Convert.ToDateTime(button.Tag).ToShortDateString(),
                                afisha_info => afisha_info.Date.ToShortDateString(),
                                (button, afisha_info) => new { button, afisha_info });
            foreach (var b in buttons)
            {
                string s = DB_connection.current_directory + "images_afisha\\" + b.afisha_info.Small_image;
                b.button.BackgroundImage = new Bitmap(@s);
                b.button.Controls.OfType<DateTimePicker>().First().Visible = true;
                b.button.Controls.OfType<DateTimePicker>().First().Value = b.afisha_info.Date;
            }

        }

        private void Add_month_button_pushed(object sender, EventArgs e)
        {
            if (Controls["panel_Dates"].Controls.ContainsKey("Label_no_dates"))
                Controls["panel_Dates"].Controls.RemoveByKey("Label_no_dates");
            int month = ((DateTimePicker)Controls["panel_Dates"].Controls["dateTimePicker"]).Value.Month;
            int year = ((DateTimePicker)Controls["panel_Dates"].Controls["dateTimePicker"]).Value.Year;
            AddMonth(month, year);
        }

        private void Day_pushed(object sender, EventArgs e)
        {
            if (((Button)sender).BackgroundImage == null)
            {
                string s = DB_connection.current_directory + "images_afisha\\" + changed_performance.Small_image;
                ((Button)sender).BackgroundImage = new Bitmap(@s);
                ((Button)sender).Controls["TimePicker"].Visible = true;
            }
            else
            {
                ((Button)sender).BackgroundImage = null;
                ((Button)sender).Controls["TimePicker"].Visible = false;
            }
        }

        private void On_month_number_Changed()
        {
            if(Month_number >= 4)
            {
                Controls["panel_Dates"].Controls["Add_month"].Enabled = false;
                Controls["panel_Dates"].Controls["dateTimePicker"].Visible = false;
            }
            else
            {
                Controls["panel_Dates"].Controls["Add_month"].Enabled = true;
                Controls["panel_Dates"].Controls["dateTimePicker"].Visible = true;
            }
        }

        private void DeleteMonth(object sender, EventArgs e)
        {
            Control button = ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            Control panel = Controls["panel_Dates"].Controls.OfType<Panel>().Where(k => k.Tag == button).First();
            Controls["panel_Dates"].Controls.RemoveByKey(button.Name);
            Controls["panel_Dates"].Controls.RemoveByKey(panel.Name);
            Month_number--;
            IEnumerable<Button> tops = Controls["panel_Dates"].Controls.OfType<Button>().Where(k => k.Name.StartsWith("top"));
            IEnumerable<Panel> panels = Controls["panel_Dates"].Controls.OfType<Panel>().Where(k => k.Name.StartsWith("panel_on_panel_Dates"));
            if(tops.Any())
            {
                int i = 0;
                int k = (800 - (82 * Month_number + (Month_number - 1) * 22)) / 2;
                foreach (Button t in tops)
                {
                    t.Name = "top" + (i + 1);
                    t.Location = new Point(k + i * 82 + i * 6, 6);
                    i++;
                }
                i = 0;
                foreach(Panel p in panels)
                {
                    p.Name = "panel_on_panel_Dates" + (i + 1);
                    i++;
                }
                Controls["panel_Dates"].Controls["Add_month"].Location = new Point(Controls["panel_Dates"].Controls["top" + Month_number].Right + 6, 6);
                ((Button)Controls["panel_Dates"].Controls["top1"]).PerformClick();
                ((Button)Controls["panel_Dates"].Controls["top1"]).Focus();
            }
            else
            {
                Controls["panel_Dates"].Controls["Add_month"].Location = new Point((Controls["panel_Dates"].Width - Controls["panel_Dates"].Controls["Add_month"].Width) / 2, 6);
            }
        }

        private void AddMonth(int month, int year)
        {
            DateTime d = new DateTime(year, month, 1);
            bool query = Controls["panel_Dates"].Controls.OfType<Button>().Where(l => (Convert.ToDateTime(l.Tag) == d)).Any();
            if (!query)
            {
                Month_number++;
                //Контекстное меню
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem("Удалить");
                contextMenuStrip.Items.Add(deleteMenuItem);
                deleteMenuItem.Click += DeleteMonth;
                //
                //Кнопка месяца
                Months m = (Months)month;
                Button top = new Button();
                top.FlatStyle = FlatStyle.Flat;
                top.Text = m + "\n" + year;
                top.AutoSize = false;
                top.Size = new Size(85, 49);
                top.Font = new Font("Century Gothic", 10, FontStyle.Regular);
                top.Tag = d;
                top.ContextMenuStrip = contextMenuStrip;
                top.BringToFront();
                top.Click += new System.EventHandler(this.OnMonthButtonPushed);
                top.Name = "top" + Month_number;
                Controls["panel_Dates"].Controls.Add(top);
                //
                //Панель кнопки месяца
                Panel p = new Panel();
                p.Size = new Size(800, 264);
                p.Location = new Point(-1, 85);
                p.AutoScroll = true;
                p.Tag = top;
                p.Name = "panel_on_panel_Dates" + Month_number;
                //Кнопки дней и время для них
                for (int j = 0; j < 42; j++)
                {
                    Button b = new Button();
                    b.Size = new Size(90, 90);
                    b.Location = new Point(80 + (89 * (j % 7)), (89 * (int)Math.Floor(j / 7.0)));
                    b.FlatStyle = FlatStyle.Flat;
                    b.TextAlign = ContentAlignment.TopLeft;
                    b.BackgroundImageLayout = ImageLayout.Stretch;
                    b.Font = new Font("Century Gothic", 9, FontStyle.Regular);
                    b.Name = "b" + (j + 1);
                    b.Visible = true;
                    b.Click += new System.EventHandler(this.Day_pushed);

                    DateTimePicker TimePicker = new DateTimePicker();
                    TimePicker.Name = "TimePicker";
                    TimePicker.Format = DateTimePickerFormat.Custom;
                    TimePicker.CustomFormat = "HH:mm";
                    TimePicker.ShowUpDown = true;
                    TimePicker.Font = new Font("Century Gothic", 10, FontStyle.Regular);
                    TimePicker.Size = new Size(62, 23);
                    TimePicker.Visible = false;
                    TimePicker.Dock = DockStyle.Bottom;
                    b.Controls.Add(TimePicker);
                    p.Controls.Add(b);
                }
                //
                Controls["panel_Dates"].Controls.Add(p);
                //
                int k = (800 - (85 * Month_number + (Month_number - 1) * 22)) / 2;
                for (int j = 0; j < Month_number; j++)
                {
                    Controls["panel_Dates"].Controls["top" + (j + 1)].Location = new Point(k + j * 85 + j * 6, 6);
                }
                Controls["panel_Dates"].Controls["Add_month"].Location = new Point(Controls["panel_Dates"].Controls["top" + Month_number].Right + 6, 6);
                //Make days for the month
                Customize_days(p.Name);
                //
                top.PerformClick();
                top.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            changed_performance.Name = Controls["panel_Text"].Controls["Name"].Text;
            changed_performance.Price = Convert.ToDouble(((NumericUpDown)Controls["panel_Text"].Controls["Price"]).Value);
            changed_performance.Small_name = Controls["panel_Text"].Controls["Small_name"].Text;
            changed_performance.Small_info = Controls["panel_Text"].Controls["Small_info"].Text;
            changed_performance.Duration = Controls["panel_Text"].Controls["Duration"].Text;
            changed_performance.Age_restriction = Controls["panel_Text"].Controls["Age_restriction"].Text;
            changed_performance.Description = Controls["panel_Text"].Controls["Description"].Text;
            string[] words = Controls["panel_Images"].Controls["Small"].BackgroundImage.Tag.ToString().Split('\\');
            string s = DB_connection.current_directory + "images_afisha\\" + words[words.ToList().Count - 1];
            if (!File.Exists(s))
                File.Copy(Controls["panel_Images"].Controls["Small"].BackgroundImage.Tag.ToString(), s);
            changed_performance.Small_image = words[words.ToList().Count - 1];
            words = Controls["panel_Images"].Controls["Medium"].BackgroundImage.Tag.ToString().Split('\\');
            s = DB_connection.current_directory + "images_afisha\\" + words[words.ToList().Count - 1];
            if (!File.Exists(s))
                File.Copy(Controls["panel_Images"].Controls["Medium"].BackgroundImage.Tag.ToString(), s);
            changed_performance.Image = words[words.ToList().Count - 1];
            words = Controls["panel_Images"].Controls["Large"].BackgroundImage.Tag.ToString().Split('\\');
            s = DB_connection.current_directory + "images_afisha\\" + words[words.ToList().Count - 1];
            if (!File.Exists(s))
                File.Copy(Controls["panel_Images"].Controls["Large"].BackgroundImage.Tag.ToString(), s);
            changed_performance.Big_image = words[words.ToList().Count - 1];

            List<TAfisha_dates> new_afisha_dates = new List<TAfisha_dates>();
            List<TAfisha_dates> cancelled_afisha_dates = new List<TAfisha_dates>();
            var query1 = Controls["panel_Dates"].Controls.OfType<Panel>().Where(k => k.Name.StartsWith("panel_on_panel_Dates"));
            foreach(Panel p in query1)
            {
                var query2 = db.GetTable<TAfisha_dates>()
                            .Where(k => k.Id_performance == performance_id && k.Date.Month == Convert.ToDateTime(((Button)p.Tag).Tag).Month && !k.Cancelled);
                var query3 = db.GetTable<TAfisha_dates>()
                            .Where(k => k.Id_performance == performance_id && k.Date.Month == Convert.ToDateTime(((Button)p.Tag).Tag).Month && k.Cancelled);
                var dates = p.Controls.OfType<Button>()
                            .Where(k => k.Name.StartsWith("b") && k.BackgroundImage != null)
                            .Select(k => k.Controls.OfType<DateTimePicker>().Where(l => l.Name.StartsWith("TimePicker")).First().Value);
                var new_dates = dates.Where(k => !query2.Select(l => l.Date).Contains(k) && !query3.Select(l => l.Date).Contains(k));
                var uncancelled_dates = query3.Where(k => dates.Contains(k.Date));
                foreach(DateTime d in new_dates)
                {
                    new_afisha_dates.Add(new TAfisha_dates() { Id_performance = performance_id, Date = d});
                }
                foreach(TAfisha_dates d in uncancelled_dates)
                {
                    d.Cancelled = false;
                }
                var cancelled_dates = query2.Where(k => !dates.Contains(k.Date));
                foreach(TAfisha_dates d in cancelled_dates)
                {
                    d.Cancelled = true;
                }
            }
            db.GetTable<TAfisha_dates>().InsertAllOnSubmit(new_afisha_dates);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception exc)
            {
                MetroMessageBox.Show(this, exc.Message);
            }
            MetroMessageBox.Show(this, "Изменения были успешно применены", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 120);

        }

    }
}
