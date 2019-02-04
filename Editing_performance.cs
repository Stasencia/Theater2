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
    public partial class Editing_performance : MetroForm
    {
        TAfisha changed_performance = new TAfisha();
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
            panel_Text.Controls.Clear();

            NumericUpDown numericUpDown = new NumericUpDown();
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
            tb[2].Text = q.Small_info1 + Environment.NewLine + q.Small_info2 + Environment.NewLine + q.Small_info3;
            tb[3].Text = q.Duration;
            tb[4].Text = q.Age_restriction;
            tb[5].Text = q.Description;
            tb[0].Location = new Point(184, 9);
            tb[1].Location = new Point(184, 95);
            tb[2].Location = new Point(184, 132);
            tb[3].Location = new Point(184, 206);
            tb[4].Location = new Point(184, 245);
            tb[5].Location = new Point(184, 283);
           /* int lines, textwidth, textheight;
            for (int i = 0; i<6;i++)
            {
                textheight = TextRenderer.MeasureText(tb[i].Text, tb[i].Font).Height;
                textwidth = TextRenderer.MeasureText(tb[i].Text, tb[i].Font).Width;
                lines = textwidth / tb[i].Width;
                if (lines == 0 || textwidth % tb[i].Width != 0)
                    lines++;
                tb[i].Height = TextRenderer.MeasureText(tb[i].Text, tb[i].Font).Height * lines + 10;
            }*/
            
            Label[] l = new Label[7];
            for(int i =0; i<7; i++)
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
           panel_Text.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
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
            //changed_performance.Image = 

          //  Performance_class.Update(performance_id, );
        }

    }
}
