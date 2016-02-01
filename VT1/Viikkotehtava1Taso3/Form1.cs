using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Viikkotehtava1Taso3
{
    /// <summary>
    /// Santeri Rusila 15.9.2014
    /// </summary>
    public partial class Form1 : Form
    {
        int kokonaisPuolue1;
        int kokonaisPuolue2;

        List<GroupBox> lista = new List<GroupBox>();

        Label Puolue1;
        Label label1;
        Label bonusLabel1;
        Label labelKortti;
        Label label2;
        TextBox textBox5;
        TextBox textBox6;
        TextBox textBox7;
        TextBox textBox8;
        Label label3;
        Label label4;

        public Form1()
        {
            InitializeComponent();
        }

        private void aloitaButton_Click(object sender, EventArgs e)
        {
            LuoBoksi();
            aloitaButton.Enabled = false;
        }

        private void LuoBoksi()
        {
            Puolue1 = new Label();
            label1 = new Label();
            bonusLabel1 = new Label();
            labelKortti = new Label();
            label2 = new Label();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            textBox7 = new TextBox();
            textBox8 = new TextBox();
            label3 = new Label();
            label4 = new Label();


            GroupBox groupJako1 = new GroupBox();
            groupJako1.Location = new System.Drawing.Point(16, 157);
            groupJako1.Name = "groupJako1";
            groupJako1.Size = new System.Drawing.Size(214, 124);
            groupJako1.TabIndex = 11;
            groupJako1.TabStop = false;
            groupJako1.Text = "Jako 1";
            

            // 
            // Puolue1
            // 
            Puolue1.AutoSize = true;
            Puolue1.Location = new System.Drawing.Point(76, 16);
            Puolue1.Name = "Puolue1";
            Puolue1.Size = new System.Drawing.Size(49, 13);
            Puolue1.TabIndex = 0;
            Puolue1.Text = "Puolue 1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(145, 16);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(49, 13);
            label1.TabIndex = 1;
            label1.Text = "Puolue 2";
            // 
            // bonusLabel1
            // 
            bonusLabel1.AutoSize = true;
            bonusLabel1.Location = new System.Drawing.Point(9, 45);
            bonusLabel1.Name = "bonusLabel1";
            bonusLabel1.Size = new System.Drawing.Size(37, 13);
            bonusLabel1.TabIndex = 2;
            bonusLabel1.Text = "Bonus";
            // 
            // labelKortti
            // 
            labelKortti.AutoSize = true;
            labelKortti.Location = new System.Drawing.Point(9, 67);
            labelKortti.Name = "labelKortti";
            labelKortti.Size = new System.Drawing.Size(31, 13);
            labelKortti.TabIndex = 3;
            labelKortti.Text = "Kortti";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(7, 95);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(52, 13);
            label2.TabIndex = 4;
            label2.Text = "Yhteensä";
            // 
            // textBox5
            // 
            textBox5.Location = new System.Drawing.Point(79, 38);
            textBox5.Name = "textBox5";
            textBox5.Size = new System.Drawing.Size(46, 20);
            textBox5.TabIndex = 5;
            // 
            // textBox6
            // 
            textBox6.Location = new System.Drawing.Point(79, 60);
            textBox6.Name = "textBox6";
            textBox6.Size = new System.Drawing.Size(46, 20);
            textBox6.TabIndex = 6;
            // 
            // textBox7
            // 
            textBox7.Location = new System.Drawing.Point(148, 38);
            textBox7.Name = "textBox7";
            textBox7.Size = new System.Drawing.Size(46, 20);
            textBox7.TabIndex = 7;
            // 
            // textBox8
            // 
            textBox8.Location = new System.Drawing.Point(148, 60);
            textBox8.Name = "textBox8";
            textBox8.Size = new System.Drawing.Size(46, 20);
            textBox8.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(79, 95);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(35, 13);
            label3.TabIndex = 9;
            label3.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(145, 95);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(35, 13);
            label4.TabIndex = 10;
            label4.Text = "0";

            int a = lista.Count;

            groupJako1.Controls.Add(label4);
            groupJako1.Controls.Add(label3);
            groupJako1.Controls.Add(textBox8);
            groupJako1.Controls.Add(textBox7);
            groupJako1.Controls.Add(textBox6);
            groupJako1.Controls.Add(textBox5);
            groupJako1.Controls.Add(label2);
            groupJako1.Controls.Add(labelKortti);
            groupJako1.Controls.Add(bonusLabel1);
            groupJako1.Controls.Add(label1);
            groupJako1.Controls.Add(Puolue1);
            // groupJako1.Location = new System.Drawing.Point(16, 175 * (a+1));
            groupJako1.Name = "groupJako1";
            groupJako1.Size = new System.Drawing.Size(214, 150);
            groupJako1.TabIndex = 11;
            groupJako1.TabStop = false;
            groupJako1.Text = "Jako " + (lista.Count + 1);
            flowLayoutPanel1.Controls.Add(groupJako1);

            lista.Add(groupJako1);
        }

        private void buttonLaske_Click(object sender, EventArgs e)
        {
            try
            {
                int a = Int32.Parse(textBox5.Text);
                int b = Int32.Parse(textBox6.Text);
                int yhteensaPuolue1 = a + b;
                
                int c = Int32.Parse(textBox7.Text);
                int d = Int32.Parse(textBox8.Text);
                int yhteensaPuolue2 = c + d;
                
                kokonaisPuolue1 += yhteensaPuolue1;
                kokonaisPuolue2 += yhteensaPuolue2;
                
                label3.Text = kokonaisPuolue1.ToString();
                label4.Text = kokonaisPuolue2.ToString();
                
                if (kokonaisPuolue1 >= 5000 || kokonaisPuolue2 >= 5000)
                {
                    if (kokonaisPuolue1 == kokonaisPuolue2)
                    {
                        label3.ForeColor = Color.Red;
                        label4.ForeColor = Color.Red;
                    }

                    if (kokonaisPuolue2 > kokonaisPuolue1)
                    {
                        label4.ForeColor = Color.Red;
                    }
                    else
                    {
                        label3.ForeColor = Color.Red;
                    }

                    buttonLaske.Enabled = false;
                }
                else
                {
                    LuoBoksi();
                }
            }
            catch
            {

            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 about = new Form2();
            about.Show();
        }
    }
}
