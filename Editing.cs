using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_theater
{
    public partial class Editing : MetroForm
    {
        int perf_id;
        MainForm mainForm;
        public Editing(MainForm main)
        {
            InitializeComponent();
            mainForm = main;
        }

        private void Editing_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.Show();
        }

        private async void Editing_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT Name FROM Afisha", connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetValue(0));
                    }
                }
            }
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            perf_id = comboBox1.SelectedIndex + 1;
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM [Afisha] WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", perf_id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        string s = DB_connection.current_directory + "images_afisha\\" + reader.GetValue(2).ToString();
                        panel1.BackgroundImage = new Bitmap(@s);
                        s = DB_connection.current_directory + "images_afisha\\" + reader.GetValue(11).ToString();
                        panel2.BackgroundImage = new Bitmap(@s);
                        s = DB_connection.current_directory + "images_afisha\\" + reader.GetValue(6).ToString();
                        panel3.BackgroundImage = new Bitmap(@s);
                        textBox1.Text = reader.GetValue(10).ToString();
                        textBox2.Text = reader.GetValue(7).ToString() + "\n" + reader.GetValue(8).ToString() + "\n" + reader.GetValue(9).ToString();
                        textBox3.Text = reader.GetValue(3).ToString();
                        textBox4.Text = reader.GetValue(4).ToString();
                        textBox5.Text = reader.GetValue(5).ToString();
                    }
                }
            }
            textBox5.Height = (int)(((textBox5.Text.Length * textBox5.Font.SizeInPoints) / textBox5.Width + textBox5.Text.Where(x => x == '\n').Count()) * textBox5.Font.Height);
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                int i = 0;
                Months m;
                SqlCommand command2 = new SqlCommand("SELECT DISTINCT MONTH(Date), YEAR(Date) FROM Afisha_dates WHERE Id_performance = @Id AND ((MONTH(Date) >= MONTH(GETDATE()) AND YEAR(Date) >= YEAR(GETDATE())) OR YEAR(Date) > YEAR(GETDATE())) ORDER BY YEAR(Date)", connection);
                command2.Parameters.AddWithValue("@Id", perf_id);
                SqlDataReader reader2 = command2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        Button top = new Button();
                        top.FlatStyle = FlatStyle.Flat;
                        m = (Months)reader2.GetInt32(0);
                        top.Text = m + "\n" + reader2.GetValue(1).ToString();
                        top.AutoSize = false;
                        top.Size = new Size(82, 49);
                        top.Font = new Font("Century Gothic", 10, FontStyle.Regular);
                        top.Tag = reader2.GetValue(0) + ";" + reader2.GetValue(1);
                        top.BringToFront();
                        top.Click += new System.EventHandler(this.Button_customization);
                        top.Name = "top" + (i + 1);
                        Controls.Add(top);
                        i++;
                    }
                }
                int k = (797 - (82 * i + (i - 1) * 22)) / 2;
                for (int j = 0; j < i; j++)
                {
                    Controls["top" + (j + 1)].Location = new Point(k + j * 82 + j * 22, textBox5.Bottom + 15);
                }
            }
            Button[] b = new Button[42];
            for (int i = 0; i < 42; i++)
            {
                b[i] = new Button();
                b[i].Size = new Size(90, 90);
                b[i].Location = new Point(45 + (89 * (i % 7)), Controls["top1"].Bottom + 15 + (89 * (int)Math.Floor(i / 7.0)));
                b[i].FlatStyle = FlatStyle.Flat;
                b[i].TextAlign = ContentAlignment.TopLeft;
                b[i].BackgroundImageLayout = ImageLayout.Stretch;
                b[i].Font = new Font("Century Gothic", 9, FontStyle.Regular);
                b[i].Name = "b" + (i + 1);
                b[i].Click += new System.EventHandler(this.button_Click);
                DateTimePicker dateTimePicker = new DateTimePicker();
                dateTimePicker.Format = DateTimePickerFormat.Custom;
                dateTimePicker.CustomFormat = "HH:mm";
                dateTimePicker.ShowUpDown = true;
                dateTimePicker.Font = new Font("Century Gothic", 10, FontStyle.Regular);
                dateTimePicker.Size = new Size(62,23);
                dateTimePicker.Name = "dateTimePicker";
                dateTimePicker.MinDate = new DateTime();
                b[i].Controls.Add(dateTimePicker);
                dateTimePicker.Dock = DockStyle.Bottom;
                Controls.Add(b[i]);
            }
            ((Button)Controls["top1"]).PerformClick();
            Button button = new Button();
            button.Size = new Size(195, 40);
            button.Location = new Point(330, Controls["b41"].Bottom + 15);
            button.FlatStyle = FlatStyle.Flat;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.BackColor = Color.Tomato;
            button.ForeColor = Color.White;
            button.Text = "Применить изменения";
            button.BackgroundImageLayout = ImageLayout.Stretch;
            button.Font = new Font("Century Gothic", 10, FontStyle.Bold);
            button.Click += new System.EventHandler(this.Write_Changes);
            button.Name = "btn";
            Controls.Add(button);
            foreach (Control c in Controls)
            {
                c.Visible = true;
            }
            
        }

        private async void button_Click(object sender, EventArgs args)
        {
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM [Afisha] WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", perf_id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if(((Button)sender).BackgroundImage == null)
                    {
                        string s = DB_connection.current_directory + "images_afisha\\" + reader.GetValue(11).ToString();
                        ((Button)sender).BackgroundImage = new Bitmap(@s);
                        ((Button)sender).Controls["dateTimePicker"].Visible = true;
                    }
                    else
                    {
                        ((Button)sender).BackgroundImage = null;
                        ((Button)sender).Controls["dateTimePicker"].Visible = false;
                    }
                }
            }
        }

        private void Write_Changes(object sender, EventArgs args)
        {
            string[] words = panel1.BackgroundImage.Tag.ToString().Split('\\');
            // panel1.BackgroundImage.Tag = words[words.ToList().Count-1];
            string s = DB_connection.current_directory + "images_afisha\\" + words[words.ToList().Count - 1];
            File.Copy(panel1.BackgroundImage.Tag.ToString(), s);
        }

        private async void Button_customization(object sender, EventArgs args)
        {
            string[] words = ((Control)sender).Tag.ToString().Split(';');
            DateTime d = new DateTime(Convert.ToInt32(words[1]), Convert.ToInt32(words[0]), 1);
            while (d.DayOfWeek != DayOfWeek.Monday)
            {
                d = d.AddDays(-1);
            }
            for (int i = 0; i < 42; i++)
            {
                Controls["b" + (i + 1)].Text = d.Day.ToString();
                if(!((Button)sender).Tag.ToString().StartsWith(d.Month.ToString()))
                    Controls["b" + (i + 1)].Enabled = false;
                Controls["b" + (i + 1)].BackgroundImage = null;
                Controls["b" + (i + 1)].Controls["dateTimePicker"].Visible = false;
                Controls["b" + (i + 1)].Tag = d.Year + "-" + d.Month + "-" + d.Day;
                d = d.AddDays(1);
            }
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT Id_performance, Date, DAY(Date) AS day, Afisha.Small_image, Afisha_dates.Time FROM [Afisha_dates] LEFT JOIN[Afisha] ON Afisha_dates.Id_performance = Afisha.Id WHERE Id_performance = @Id AND MONTH(Date) = @month AND YEAR(Date) = @year", connection);
                command.Parameters.AddWithValue("@Id", perf_id);
                command.Parameters.AddWithValue("@month", Convert.ToInt32(words[0]));
                command.Parameters.AddWithValue("@year", Convert.ToInt32(words[1]));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < 42; i++)
                        {
                            string[] dateparts = Controls["b" + (i + 1)].Tag.ToString().Split('-');
                            if (dateparts[2] == reader.GetValue(2).ToString() && (Convert.ToInt32(dateparts[2]) >= DateTime.Now.Day || Convert.ToInt32(dateparts[0]) > DateTime.Now.Year) && dateparts[1] == (words[0]))
                            {
                                string s = DB_connection.current_directory + "images_afisha\\" + reader.GetValue(3).ToString();
                               // Controls["b" + (i + 1)].Enabled = true;
                                Controls["b" + (i + 1)].BackgroundImage = new Bitmap(@s);
                                Controls["b" + (i + 1)].Controls["dateTimePicker"].Visible = true;
                                string st = reader.GetValue(1).ToString().Split(' ')[0] + " " + reader.GetValue(4).ToString();
                                ((DateTimePicker)Controls["b" + (i + 1)].Controls["dateTimePicker"]).Value = DateTime.Parse(st);
                            }
                            else
                            {
                                ((DateTimePicker)Controls["b" + (i + 1)].Controls["dateTimePicker"]).Value = ((DateTimePicker)Controls["b" + (i + 1)].Controls["dateTimePicker"]).MinDate;
                            
                            }
                        }
                    }
                }
            }

        }

        private void panel1_Click(object sender, EventArgs e)
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
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Editing_Click(object sender, EventArgs e)
        {
            
        }

        private void Editing_DoubleClick(object sender, EventArgs e)
        {
           
        }
    }
}
