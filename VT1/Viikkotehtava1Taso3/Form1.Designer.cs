namespace Viikkotehtava1Taso3
{
    partial class Form1
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
            this.labelPuolue1 = new System.Windows.Forms.Label();
            this.labelPuolue2 = new System.Windows.Forms.Label();
            this.Pelaaja1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Pelaaja2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Pelaaja3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.Pelaaja4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.aloitaButton = new System.Windows.Forms.Button();
            this.buttonLaske = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelPuolue1
            // 
            this.labelPuolue1.AutoSize = true;
            this.labelPuolue1.Location = new System.Drawing.Point(13, 24);
            this.labelPuolue1.Name = "labelPuolue1";
            this.labelPuolue1.Size = new System.Drawing.Size(49, 13);
            this.labelPuolue1.TabIndex = 0;
            this.labelPuolue1.Text = "Puolue 1";
            this.labelPuolue1.Click += new System.EventHandler(this.aloitaButton_Click);
            // 
            // labelPuolue2
            // 
            this.labelPuolue2.AutoSize = true;
            this.labelPuolue2.Location = new System.Drawing.Point(127, 24);
            this.labelPuolue2.Name = "labelPuolue2";
            this.labelPuolue2.Size = new System.Drawing.Size(49, 13);
            this.labelPuolue2.TabIndex = 1;
            this.labelPuolue2.Text = "Puolue 2";
            // 
            // Pelaaja1
            // 
            this.Pelaaja1.AutoSize = true;
            this.Pelaaja1.Location = new System.Drawing.Point(13, 55);
            this.Pelaaja1.Name = "Pelaaja1";
            this.Pelaaja1.Size = new System.Drawing.Size(51, 13);
            this.Pelaaja1.TabIndex = 2;
            this.Pelaaja1.Text = "Pelaaja 1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 77);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            // 
            // Pelaaja2
            // 
            this.Pelaaja2.AutoSize = true;
            this.Pelaaja2.Location = new System.Drawing.Point(13, 100);
            this.Pelaaja2.Name = "Pelaaja2";
            this.Pelaaja2.Size = new System.Drawing.Size(51, 13);
            this.Pelaaja2.TabIndex = 4;
            this.Pelaaja2.Text = "Pelaaja 2";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(13, 123);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 5;
            // 
            // Pelaaja3
            // 
            this.Pelaaja3.AutoSize = true;
            this.Pelaaja3.Location = new System.Drawing.Point(127, 55);
            this.Pelaaja3.Name = "Pelaaja3";
            this.Pelaaja3.Size = new System.Drawing.Size(51, 13);
            this.Pelaaja3.TabIndex = 6;
            this.Pelaaja3.Text = "Pelaaja 3";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(130, 77);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 7;
            // 
            // Pelaaja4
            // 
            this.Pelaaja4.AutoSize = true;
            this.Pelaaja4.Location = new System.Drawing.Point(127, 100);
            this.Pelaaja4.Name = "Pelaaja4";
            this.Pelaaja4.Size = new System.Drawing.Size(51, 13);
            this.Pelaaja4.TabIndex = 8;
            this.Pelaaja4.Text = "Pelaaja 4";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(130, 123);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 9;
            // 
            // aloitaButton
            // 
            this.aloitaButton.Location = new System.Drawing.Point(13, 149);
            this.aloitaButton.Name = "aloitaButton";
            this.aloitaButton.Size = new System.Drawing.Size(75, 23);
            this.aloitaButton.TabIndex = 10;
            this.aloitaButton.Text = "Aloita peli";
            this.aloitaButton.UseVisualStyleBackColor = true;
            this.aloitaButton.Click += new System.EventHandler(this.aloitaButton_Click);
            // 
            // buttonLaske
            // 
            this.buttonLaske.Location = new System.Drawing.Point(130, 149);
            this.buttonLaske.Name = "buttonLaske";
            this.buttonLaske.Size = new System.Drawing.Size(75, 23);
            this.buttonLaske.TabIndex = 11;
            this.buttonLaske.Text = "&Laske";
            this.buttonLaske.UseVisualStyleBackColor = true;
            this.buttonLaske.Click += new System.EventHandler(this.buttonLaske_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(266, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 189);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(242, 142);
            this.flowLayoutPanel1.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(266, 399);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.buttonLaske);
            this.Controls.Add(this.aloitaButton);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.Pelaaja4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.Pelaaja3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Pelaaja2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Pelaaja1);
            this.Controls.Add(this.labelPuolue2);
            this.Controls.Add(this.labelPuolue1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Canasta";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form1_Load(object sender, System.EventArgs e)
        {

        }

        #endregion

        private System.Windows.Forms.Label labelPuolue1;
        private System.Windows.Forms.Label labelPuolue2;
        private System.Windows.Forms.Label Pelaaja1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Pelaaja2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label Pelaaja3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label Pelaaja4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button aloitaButton;
        private System.Windows.Forms.Button buttonLaske;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

