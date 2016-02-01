using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aikakomponentti
{
    public partial class Aikakomponentti : TextBox
    {
        private ErrorProvider error = new ErrorProvider();
        public delegate void Tarkistus();
        public event Tarkistus Virhe;
        public event Tarkistus Ok;

        public Aikakomponentti()
        {
            InitializeComponent();
            this.Text = "00:00:00";
            
        }

        /// <summary>
        /// Validating tarkistaa kentän oikeellisuuden kun siitä yritetään poistua.
        /// HH:MM:SS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Aikakomponentti_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                // Tarkistetaan onko annettu oikeassa muodossa
                String[] taulukkoString = (this.Text.Split(':'));
                if (taulukkoString.Length != 3)
                {
                    throw new Exception("Anna aika muodossa: HH:MM:SS");
                }

                // Tarkistetaan onko kentän arvot kokonaislukuja
                int[] taulukkoInt = new int[taulukkoString.Length];
                for (int i = 0; i < taulukkoString.Length; i++)
                {
                    taulukkoInt[i] = Int16.Parse(taulukkoString[i]);
                }

                // Tarkistaa on kenttäämn annettu negatiivisia lukuja, jos on niin virhe!
                if (taulukkoInt[0] < 0 || taulukkoInt[1] < 0 || taulukkoInt[2] < 0) throw new Exception("Et voi antaa negatiivisia lukuja!");

                // Virhe jos aika on 00:00:00, täytyy olla vähintään 1 sekunti
                if (taulukkoInt[0] == 0 && taulukkoInt[1] == 0 && taulukkoInt[2] == 0) throw new Exception("Aika saa olla vähintään 00:00:01 (1 Sekunti)");

                // Jos tunnit alle 24h, tarkistetaan että minuutit ja sekunnit ovat 0 ja 60 välillä
                if (taulukkoInt[0] < 24)
                {
                    if (taulukkoInt[1] > 59 || taulukkoInt[1] < 0 || taulukkoInt[2] > 59 || taulukkoInt[2] < 0)
                    {
                        throw new Exception("Minuutit ja sekunnit on oltava väliltä 0-59, 00:00:60 --> 00:01:00!!");
                    }
                }

                // Jos tunnit tasat 24, tarkistetaan onko minuutit ja sekunit 0
                if (taulukkoInt[0] == 24)
                {
                    if (taulukkoInt[1] != 0 || taulukkoInt[2] != 0)
                    {
                        throw new Exception("Maksimissaan 24H (24:00:00)");
                    }
                }

                if (taulukkoInt[0] > 24) throw new Exception("Maksimissaan 24H (24:00:00)");

                // Kaikki OK
                if (Ok != null) Ok();
                error.Clear();
                e.Cancel = false;
                this.BackColor = DefaultBackColor;
            }
            catch (Exception p)
            {
                // Ilmoittaa virheestä ErrorProviderin avulla ja värjää tekstikentän taustan punaiseksi
                if (Virhe != null) Virhe();
                e.Cancel = true;
                error.SetError(this, p.Message);
                this.BackColor = Color.Red;
            }
        }



    }
}
