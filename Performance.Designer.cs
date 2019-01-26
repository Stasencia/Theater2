namespace Project_theater
{
    partial class Performance
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Performance));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 380);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(10, 421);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "Спектакль театра-цирка «7 пальцев» \r\nThe 7 Fingers, Канада";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(10, 474);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(282, 48);
            this.label2.TabIndex = 3;
            this.label2.Text = "идея, постановка, хореография: Шена Кэрролл\r\ncценография: Ана Каппеллуто\r\ncвет: Э" +
    "рик Шампу";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Century Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(11, 557);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(249, 20);
            this.label8.TabIndex = 25;
            this.label8.Text = "Возрастное ограничение: 12+";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Century Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(10, 537);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(249, 20);
            this.label7.TabIndex = 24;
            this.label7.Text = "Продолжительность: 1 час 30 минут";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(11, 589);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(769, 216);
            this.label9.TabIndex = 26;
            this.label9.Text = resources.GetString("label9.Text");
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(641, 474);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(35, 31);
            this.panel2.TabIndex = 27;
            // 
            // Performance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(831, 788);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "Performance";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.White;
            this.Activated += new System.EventHandler(this.Performance_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Performance_FormClosing);
            this.Load += new System.EventHandler(this.Performance_Load);
            this.Shown += new System.EventHandler(this.Performance_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel2;
    }
}