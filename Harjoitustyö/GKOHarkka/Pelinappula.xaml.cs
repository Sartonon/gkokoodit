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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GKOHarkka
{
    /// <summary>
    /// Interaction logic for Pelinappula.xaml
    /// </summary>
    public partial class Pelinappula : UserControl
    {
        Boolean syoty = false;
        Boolean valittu = false;
        int pelaaja;
        Boolean vuoro;
        SolidColorBrush nappula1Vari = new SolidColorBrush(Colors.LightGreen);
        SolidColorBrush nappula2Vari = new SolidColorBrush(Colors.Red);
        Boolean tammi = false;
        DispatcherTimer timer = new DispatcherTimer();
        Storyboard myStoryboard2;
        int tick = 0;
        double alkuperainenKorkeus;
        double alkuperainenLeveys;
        int thickness = 1;
        int valittuThickness = 5;

        /// <summary>
        /// Palautetaan onko nappula tammi.
        /// </summary>
        /// <returns></returns>
        public Boolean GetTammi()
        {
            return tammi;
        }

        /// <summary>
        /// Asetetaan nappula tammeksi.
        /// </summary>
        /// <param name="muutettu"></param>
        public void SetTammi(Boolean muutettu)
        {
            tammi = muutettu;
        }


        /// <summary>
        /// Nappulan alustus.
        /// </summary>
        public Pelinappula()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(250);
            timer.Tick += timer_Tick;
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Center;
        }


        /// <summary>
        /// Ajastin jota käytetään pelinappulan suurennoksessa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            tick++;
            if (tick == 4)
            {
                pelinappula.Margin = new Thickness(0);
                thickness = 5;
                valittuThickness = 25;
                pelinappula.StrokeThickness = thickness;
                timer.Stop();
            }
        }


        /// <summary>
        /// Kutsutaan kun pelinappula syödään.
        /// </summary>
        public void Syoty(Boolean boo)
        {
            syoty = boo;
        }


        /// <summary>
        /// Kysytään onko syöty
        /// </summary>
        /// <returns></returns>
        public Boolean OnkoSyoty()
        {
            return syoty;
        }

        /// <summary>
        /// Asetetaan nappulalle pelaaja-muuttuja (kumpi pelaaja on).
        /// </summary>
        /// <param name="i"></param>
        public void SetPelaaja(int i)
        {
            pelaaja = i;
            if (i == 1)
            {
                pelinappula.Fill = nappula1Vari;
            }
            else
            {
                pelinappula.Fill = nappula2Vari;
            }
        }


        /// <summary>
        /// Palautetaan pelaajan numero.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int GetPelaaja()
        {
            return pelaaja;
        }


        /// <summary>
        /// Asetetaan vuoro.
        /// </summary>
        /// <param name="i"></param>
        public void SetVuoro(Boolean i)
        {
            vuoro = i;
        }


        /// <summary>
        /// Asetetaan nappulan 1 väri.
        /// </summary>
        /// <param name="vari"></param>
        public void SetVari1(SolidColorBrush vari)
        {
            nappula1Vari = vari;
            pelinappula.Fill = nappula1Vari;
            pelinappula.StrokeThickness = thickness;
        }

        /// <summary>
        /// Sijoitetaan nappulan tietoihin true/false sen perusteella
        /// onko se valittu vai ei.
        /// </summary>
        /// <param name="i"></param>
        public void Valittu(Boolean i)
        {
            valittu = i;
            if (valittu == false)
            {
                if (GetPelaaja() == 1)
                {
                    pelinappula.Fill = nappula1Vari;
                    pelinappula.StrokeThickness = thickness;
                }
                else
                {
                    pelinappula.Fill = nappula2Vari;
                    pelinappula.StrokeThickness = thickness;
                }
            }
        }

        /// <summary>
        /// Palauttaa true/false sen mukaan onko nappula valittuna.
        /// </summary>
        /// <returns></returns>
        public Boolean OnkoValittu()
        {
            return valittu;
        }

        public void Voitti()
        {
            pelinappula.Fill = Brushes.Blue;
        }

        /// <summary>
        /// Vaihetaan valitun nappulan väriä ja merkataan se
        /// valituksi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ellipse_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (vuoro == true)
            {
                RaiseClickEvent();
                if (valittu == true)
                {
                    valittu = false;
                    if (GetPelaaja() == 1)
                    {
                        pelinappula.Fill = nappula1Vari;
                        pelinappula.StrokeThickness = thickness;
                    }
                    else
                    {
                        pelinappula.Fill = nappula2Vari;
                        pelinappula.StrokeThickness = thickness;
                    }
                }
                else
                {
                    pelinappula.StrokeThickness = valittuThickness;
                    // pelinappula.Fill = Brushes.Black;
                    valittu = true;
                }
            }

            // e.Handled = true;  
        }


        /// <summary>
        /// Click-eventin alustus.
        /// </summary>
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
            "Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pelinappula));

        public event RoutedEventHandler ClickLol
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        void RaiseClickEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(Pelinappula.ClickEvent);
            RaiseEvent(newEventArgs);
        }

        /// <summary>
        /// Asetetaan nappulan 2 väri.
        /// </summary>
        /// <param name="vari"></param>
        public void SetVari2(SolidColorBrush vari)
        {
            nappula2Vari = vari;
            pelinappula.Fill = vari;
            pelinappula.StrokeThickness = thickness;
        }


        /// <summary>
        /// Nappulan animointi kun se korotetaan tammmeksi.
        /// </summary>
        public void SuurennaNappula()
        {
            alkuperainenKorkeus = pelinappula.Height;
            alkuperainenLeveys = pelinappula.Width;
            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = pelinappula.ActualWidth;
            myDoubleAnimation.To = pelinappula.ActualWidth + 200;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            Storyboard.SetTargetName(myDoubleAnimation, pelinappula.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation,
                new PropertyPath(Ellipse.WidthProperty));

            DoubleAnimation myDoubleAnimation2 = new DoubleAnimation();
            myDoubleAnimation2.From = pelinappula.ActualHeight;
            myDoubleAnimation2.To = pelinappula.ActualHeight + 200;
            myDoubleAnimation2.Duration = new Duration(TimeSpan.FromSeconds(1));
            Storyboard.SetTargetName(myDoubleAnimation2, pelinappula.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation2,
                new PropertyPath(Ellipse.HeightProperty));

            Storyboard myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            myStoryboard2 = new Storyboard();
            myStoryboard2.Children.Add(myDoubleAnimation2);

            myStoryboard.Begin(this);
            myStoryboard2.Begin(this);
            timer.Start();
            
        }
    }
}
