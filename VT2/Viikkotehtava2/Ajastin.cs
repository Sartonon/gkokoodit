using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Viikkotehtava2
{
    public partial class Ajastin : Form
    {
        Button buttonStop = new Button();
        public Ajastin()
        {
            InitializeComponent();
        }


        private void aikakomponentti1_Virhe()
        {

        }

        private void aikakomponentti1_Ok()
        {
           ajastinkomponentti1.Aika = aikakomponentti1.Text;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (ajastinkomponentti1.Jaika == "" || ajastinkomponentti1.Jaika == "00:00:00")
            {
                labelAika.Text = aikakomponentti1.Text;
            }
            ajastinkomponentti1.StartAjastin();
            
            // Piilotetaan start-nappula ja laitetaan stop-nappula esiin
            buttonStart.Hide();       
            buttonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonStop.Location = new System.Drawing.Point(79, 88);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new System.Drawing.Size(114, 51);
            buttonStop.TabIndex = 2;
            buttonStop.Text = "Stop";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            Controls.Add(buttonStop);
            buttonStop.Show();
        }

        //Stop-nappula pysäyttää ajan ja laittaa start-nappulan näkyville
        private void buttonStop_Click(object sender, EventArgs e)
        {
            ajastinkomponentti1.StopAjastin();
            buttonStart.Show();
        }

        // Sekuntitapahtuma päivittää aijaa labeliin
        private void ajastinkomponentti1_Sekunti1()
        {
            labelAika.Text = ajastinkomponentti1.Jaika;
        }

        // Ajan loppuessa ilmestyy messagebox, stop-button häviää ja start-button tulee esiin
        private void ajastinkomponentti1_AikaLoppu()
        {
            MessageBox.Show("Aika loppui!");
            buttonStop.Hide();
            buttonStart.Show();
        }

        // Button-reset asettaa uuden ajan labeliin
        private void buttonReset_Click(object sender, EventArgs e)
        {
            labelAika.Text = aikakomponentti1.Text;
            ajastinkomponentti1.StopAjastin();
            buttonStop.Hide();
            buttonStart.Show();
            ajastinkomponentti1.Aika = aikakomponentti1.Text;
            ajastinkomponentti1.Jaika = aikakomponentti1.Text;
        }

        // Form ei sulkeudu jos ajastin on käynnissä
        private void Ajastin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ajastinkomponentti1.Enabled)
            {
                e.Cancel = true;
            }
        }


    }
}
