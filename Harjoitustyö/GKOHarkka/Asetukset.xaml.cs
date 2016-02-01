using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;

namespace GKOHarkka
{
    /// <summary>
    /// Interaction logic for Asetukset.xaml
    /// </summary>
    public partial class Asetukset : Window
    {

        SolidColorBrush pelaaja1Vari = new SolidColorBrush(Colors.LightGreen);
        SolidColorBrush pelaaja2Vari = new SolidColorBrush(Colors.Red);
        SolidColorBrush ruudukko1Vari;
        SolidColorBrush ruudukko2Vari;
        int ruudukonKokoOletus = 1;
        Boolean pelaaja1Changed = false;
        Boolean pelaaja2Changed = false;
        Boolean ruudukko1Changed = false;
        Boolean ruudukko2Changed = false;
        Boolean ruudukonKokoChanged = false;
        Boolean pelimuotoChanged = false;
        string pelimuoto;
        string valittuPelimuoto;


        /// <summary>
        /// Asetetaan pelaajan 1 nappulan väri.
        /// </summary>
        /// <param name="color"></param>
        public void SetPelaaja1Vari(SolidColorBrush color)
        {
            pelaaja1Vari = color;
        }


        /// <summary>
        /// Asetetaan pelaajan 2 nappulan väri.
        /// </summary>
        /// <param name="color"></param>
        public void SetPelaaja2Vari(SolidColorBrush color)
        {
            pelaaja2Vari = color;
        }


        /// <summary>
        /// Palauttaa onko pelaajan 1 väriä muutettu.
        /// </summary>
        /// <returns></returns>
        public Boolean GetPelaaja1Changed()
        {
            return pelaaja1Changed;
        }


        /// <summary>
        /// Asettaa muutoksen pelaaja1Changed-muuttujaan.
        /// </summary>
        /// <param name="muutettu"></param>
        public void SetPelaaja1Changed(Boolean muutettu)
        {
            pelaaja1Changed = muutettu;
        }


        /// <summary>
        /// Palauttaa onko pelaajan 2 väriä muutettu.
        /// </summary>
        /// <returns></returns>
        public Boolean GetPelaaja2Changed()
        {
            return pelaaja2Changed;
        }


        /// <summary>
        /// Asettaa muutoksen pelaaja1Changed-muuttujaan.
        /// </summary>
        /// <param name="muutettu"></param>
        public void SetPelaaja2Changed(Boolean muutettu)
        {
            pelaaja2Changed = muutettu;
        }


        /// <summary>
        /// Palauttaa onko ruudukon 1 väriä muutettu.
        /// </summary>
        /// <returns></returns>
        public Boolean GetRuudukko1Changed()
        {
            return ruudukko1Changed;
        }


        /// <summary>
        /// Asettaa muutoksen ruudukko1Changed-muuttujaan.
        /// </summary>
        /// <param name="muutettu"></param>
        public void SetRuudukko1Changed(Boolean muutettu)
        {
            ruudukko1Changed = muutettu;
        }


        /// <summary>
        /// Palauttaa onko ruudukon 2 väriä muutettu.
        /// </summary>
        /// <returns></returns>
        public Boolean GetRuudukko2Changed()
        {
            return ruudukko2Changed;
        }


        /// <summary>
        /// Asettaa muutoksen ruudukko2Changed-muuttujaan.
        /// </summary>
        /// <param name="muutettu"></param>
        public void SetRuudukko2Changed(Boolean muutettu)
        {
            ruudukko2Changed = muutettu;
        }


        /// <summary>
        /// Palauttaa onko ruudukon kokoa muutettu.
        /// </summary>
        /// <returns></returns>
        public Boolean GetRuudukonKokoChanged()
        {
            return ruudukonKokoChanged;
        }


        /// <summary>
        /// Asettaa muutoksen ruudukonKokoChanged-muuttujaan.
        /// </summary>
        /// <param name="muutettu"></param>
        public void SetRuudukonKokoChanged(Boolean muutettu)
        {
            ruudukonKokoChanged = muutettu;
        }


        /// <summary>
        /// Vaihtaa ruuduko oletuskokoa ja vaihtaa valittua ruudukon kokoa
        /// asetus-ikkunassa.
        /// </summary>
        /// <param name="oletus"></param>
        public void SetRuudukonKokoOletus(int oletus)
        {
            ruudukonKokoOletus = oletus;
            ruudukonKoko.SelectedIndex = ruudukonKokoOletus;
        }


        /// <summary>
        /// Alustaa asetus-ikkunan.
        /// </summary>
        public Asetukset()
        {
            InitializeComponent();
            this.MaxWidth = this.Width;
            this.MinWidth = this.Width;
        }


        /// <summary>
        /// Suljetaan asetus-ikkuna. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPeruuta_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Avataan väridialogi ja otetaan valittu väri talteen, muutetaan asetus-dialogin
        /// värejä.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pelaaja1Vari_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if(colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pelaaja1Vari = MuutaVari(colorDialog.Color);
                pelaaja1Changed = true;
                pelaaja1Vari2.Fill = pelaaja1Vari;
            }

            
        }

        /// <summary>
        /// Muutetaan väriä WPF-muotoon
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static System.Windows.Media.Color ToColor(System.Drawing.Color color)
        {
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }


        /// <summary>
        /// Avataan väridialogi ja otetaan valittu väri talteen, muutetaan asetus-dialogin
        /// värejä.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pelaaja2Vari_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pelaaja2Vari = MuutaVari(colorDialog.Color);
                pelaaja2Changed = true;
                pelaaja2Vari2.Fill = pelaaja2Vari;
            }
        }


        /// <summary>
        /// Avataan väridialogi ja otetaan valittu väri talteen, muutetaan asetus-dialogin
        /// värejä.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vari1_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ruudukko1Vari = MuutaVari(colorDialog.Color);
                ruudukko1Changed = true;
                ruudukko1Vari2.Fill = ruudukko1Vari;
            }
        }


        /// <summary>
        /// Avataan väridialogi ja otetaan valittu väri talteen, muutetaan asetus-dialogin
        /// värejä.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vari2_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ruudukko2Vari = MuutaVari(colorDialog.Color);
                ruudukko2Changed = true;
                ruudukko2Vari2.Fill = ruudukko2Vari;
            }
        }



        /// <summary>
        /// Muutetaan väri wpf-muotoon
        /// </summary>
        /// <param name="myColor"></param>
        /// <returns></returns>
        public SolidColorBrush MuutaVari(System.Drawing.Color myColor)
        {
            System.Drawing.Color myColor1 = myColor;
            System.Windows.Media.Color vari = ToColor(myColor1);
            return new SolidColorBrush(vari);
        }

        /// <summary>
        /// Palautetaan pelaajan 1 väri.
        /// </summary>
        /// <returns></returns>
        public SolidColorBrush GetVariPelaaja1()
        {
            return pelaaja1Vari;
        }

        /// <summary>
        /// Tallennetaan asetukset.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTallenna_Click(object sender, RoutedEventArgs e)
        {
            if (ruudukonKoko.SelectedIndex != ruudukonKokoOletus )
            {
                ruudukonKokoOletus = ruudukonKoko.SelectedIndex;
                ruudukonKokoChanged = true;
            }
            RaiseAsetuksetChangedEvent();
            this.Close();
        }

        /// <summary>
        /// AsetuksetChanged-eventin määrittely
        /// </summary>
        public static readonly RoutedEvent AsetuksetChangedEvent = EventManager.RegisterRoutedEvent(
            "AsetuksetChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Asetukset));

        public event RoutedEventHandler AsetuksetChanged
        {
            add { AddHandler(AsetuksetChangedEvent, value); }
            remove { RemoveHandler(AsetuksetChangedEvent, value); }
        }

        void RaiseAsetuksetChangedEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(Asetukset.AsetuksetChangedEvent);
            RaiseEvent(newEventArgs);
        }


        /// <summary>
        /// Palauttaa pelaajan 2 värin.
        /// </summary>
        /// <returns></returns>
        public SolidColorBrush GetVariPelaaja2()
        {
            return pelaaja2Vari;
        }


        /// <summary>
        /// Palauttaa ruudukon 1 värin.
        /// </summary>
        /// <returns></returns>
        public SolidColorBrush GetVariRuudukko1()
        {
            return ruudukko1Vari;
        }


        /// <summary>
        /// Palauttaa ruudukon 2 värin.
        /// </summary>
        /// <returns></returns>
        public SolidColorBrush GetVariRuudukko2()
        {
            return ruudukko2Vari;
        }


        /// <summary>
        /// Palauttaa oletusvärin pelaajalle 1.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oletusVariPelaaja1_Click(object sender, RoutedEventArgs e)
        {
            pelaaja1Vari = new SolidColorBrush(Colors.GreenYellow);
            pelaaja1Vari2.Fill = pelaaja1Vari;
            pelaaja1Changed = true;
        }


        /// <summary>
        /// Palauttaa oletusvärin pelaajalle 2.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oletusVariPelaaja2_Click(object sender, RoutedEventArgs e)
        {
            pelaaja2Vari = new SolidColorBrush(Colors.Red);
            pelaaja2Vari2.Fill = pelaaja2Vari;
            pelaaja2Changed = true;
        }


        /// <summary>
        /// Palauttaa oletusvärin ruudukolle 1.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oletusVariRuudukko1_Click(object sender, RoutedEventArgs e)
        {
            ruudukko1Vari = new SolidColorBrush(Colors.Blue);
            ruudukko1Vari2.Fill = ruudukko1Vari;
            ruudukko1Changed = true;
        }


        /// <summary>
        /// Palauttaa oletusvärin ruudukolle 2.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oletusVariRuudukko2_Click(object sender, RoutedEventArgs e)
        {
            ruudukko2Vari = new SolidColorBrush(Colors.Aqua);
            ruudukko2Vari2.Fill = ruudukko2Vari;
            ruudukko2Changed = true;
        }


        /// <summary>
        /// Asettaa ruudukonKoko indeksin int-muuttujaan a.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ruudukonKoko_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int a = ruudukonKoko.SelectedIndex;
        }

        /// <summary>
        /// Palauttaa ruudukon oletuskoon.
        /// </summary>
        /// <returns></returns>
        public int GetRuudukonKoko()
        {
            return ruudukonKokoOletus;
        }


        /// <summary>
        /// Käytä-nappula käyttää asetukset.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kayta_Click(object sender, RoutedEventArgs e)
        {
            if (ruudukonKoko.SelectedIndex != ruudukonKokoOletus)
            {
                ruudukonKokoOletus = ruudukonKoko.SelectedIndex;
                ruudukonKokoChanged = true;
            }
            RaiseAsetuksetChangedEvent();
        }


        /// <summary>
        /// Asetetaan oletuspelimuoto.
        /// </summary>
        /// <param name="pelimuoto1"></param>
        public void SetPelimuotoOletus(string pelimuoto1)
        {
            pelimuoto = pelimuoto1;
            valittuPelimuoto = pelimuoto1;
        }


        /// <summary>
        /// Asetetaan pelimuodon muutos pelimuotoChanged-muuttujaan.
        /// </summary>
        /// <param name="muutettu"></param>
        public void PelimuotoChanged(Boolean muutettu)
        {
            pelimuotoChanged = muutettu;
        }


        /// <summary>
        /// Palautetaan pelimuotoChanged-muuttujan arvo.
        /// </summary>
        /// <returns></returns>
        public Boolean GetPelimuotoChanged()
        {
            return pelimuotoChanged;
        }
        

        /// <summary>
        /// Palautetaan valittu pelimuoto.
        /// </summary>
        /// <returns></returns>
        public string GetValittuPelimuoto()
        {
            return valittuPelimuoto;
        }


        /// <summary>
        /// Asetetaan valittuun pelimuotoon merkkijono sen mukaan kumpi pelimuoto
        /// valitaan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pelimuoto_Click(object sender, RoutedEventArgs e)
        {
            if (tammi.IsChecked == true) valittuPelimuoto = "Tammi";
            else valittuPelimuoto = "Breakthrough";

            if (valittuPelimuoto.Equals(pelimuoto))
            {
                pelimuotoChanged = false;
            }
            else pelimuotoChanged = true;
        }
    }
}
