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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GKOHarkka
{
    /// <summary>
    /// Interaction logic for Pelikentta.xaml
    /// </summary>
    public partial class Pelikentta : UserControl
    {
        Random vuoroRandom = new Random();
        int pelikentanKoko = 8;
        int row;
        int column;
        int vuoro = 1;
        List<Rectangle> ruutulista = new List<Rectangle>();
        SolidColorBrush alkuperainenVari;
        Rectangle varjattavaRuutu;
        DispatcherTimer timer = new DispatcherTimer();
        int pari = 0;
        
        
        /// <summary>
        /// Alustetaan pelikenttä
        /// </summary>
        public Pelikentta()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += timer_Tick;

            for (int i = 0; i < pelikentanKoko; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                myGrid.ColumnDefinitions.Add(colDef);
                RowDefinition rowDef = new RowDefinition();
                myGrid.RowDefinitions.Add(rowDef);
            }

            for (int o = 0; o < pelikentanKoko; o += 2)
            {
                for (int i = 0; i < pelikentanKoko; i += 2)
                {
                    Rectangle ruutu = new Rectangle();
                    ruutulista.Add(ruutu);
                    Grid.SetRow(ruutu, o);
                    Grid.SetColumn(ruutu, i);
                    ruutu.Fill = Brushes.Blue;
                    myGrid.Children.Add(ruutu);
                }

                if ((pelikentanKoko % 2 != 0) && (o < pelikentanKoko - 1))
                {
                    for (int a = 1; a < pelikentanKoko; a += 2)
                    {
                        Rectangle ruutu = new Rectangle();
                        ruutulista.Add(ruutu);
                        Grid.SetRow(ruutu, (o + 1));
                        Grid.SetColumn(ruutu, a);
                        ruutu.Fill = Brushes.Blue;
                        myGrid.Children.Add(ruutu);
                    }
                }

                if (pelikentanKoko % 2 == 0)
                {
                    for (int a = 1; a < pelikentanKoko; a += 2)
                    {
                        Rectangle ruutu = new Rectangle();
                        ruutulista.Add(ruutu);
                        Grid.SetRow(ruutu, (o + 1));
                        Grid.SetColumn(ruutu, a);
                        ruutu.Fill = Brushes.Blue;
                        myGrid.Children.Add(ruutu);
                    }
                }

            }
             // toinen
            for (int o = 0; o < pelikentanKoko; o += 2)
            {
                for (int i = 0; i < pelikentanKoko - 1; i += 2)
                {
                    Rectangle ruutu = new Rectangle();
                    ruutulista.Add(ruutu);
                    Grid.SetRow(ruutu, o);
                    Grid.SetColumn(ruutu, i + 1);
                    ruutu.Fill = Brushes.Cyan;
                    myGrid.Children.Add(ruutu);
                }

                if ((pelikentanKoko % 2 != 0) && (o < pelikentanKoko - 1))
                {
                    for (int a = 1; a < pelikentanKoko + 1; a += 2)
                    {
                        Rectangle ruutu = new Rectangle();
                        ruutulista.Add(ruutu);
                        Grid.SetRow(ruutu, (o + 1));
                        Grid.SetColumn(ruutu, a - 1);
                        ruutu.Fill = Brushes.Cyan;
                        myGrid.Children.Add(ruutu);
                    }
                }

                if (pelikentanKoko % 2 == 0)
                {
                    for (int a = 1; a < pelikentanKoko; a += 2)
                    {
                        Rectangle ruutu = new Rectangle();
                        
                        ruutulista.Add(ruutu);
                        Grid.SetRow(ruutu, (o + 1));
                        Grid.SetColumn(ruutu, a - 1);
                        ruutu.Fill = Brushes.Cyan;
                        myGrid.Children.Add(ruutu);
                    }
                }

            }
        }


        /// <summary>
        /// Ajastin ruudun vilkutusta varten.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (pari % 2 == 0)
            {
                varjattavaRuutu.Fill = new SolidColorBrush(Colors.Red);
            }
            else varjattavaRuutu.Fill = alkuperainenVari;
            pari++;
            if (pari == 6)
            {
                pari = 0;
                timer.Stop();
            }
        }


        /// <summary>
        /// Värjätään/vilkutetaan väärin valittua ruutua punaisena.
        /// </summary>
        public void VarjaaPunaiseksi(int column, int row, SolidColorBrush vari, SolidColorBrush vari2)
        {
            int i = 1;
            int raja;
            int a = ruutulista.Count;
            if (a % 2 != 0)
            {
                raja = a / 2 + 1;
            }
            else
            {
                raja = a / 2;
            }

            if (!timer.IsEnabled)
            {
                
                foreach (Rectangle ruutu in ruutulista)
                {
                    
                    int rowRuutu = Grid.GetRow(ruutu);
                    int columnRuutu = Grid.GetColumn(ruutu);
                    if (columnRuutu == column && rowRuutu == row)
                    {
                        if (i <= raja)
                        {
                            alkuperainenVari = vari;
                        }
                        else
                        {
                            alkuperainenVari = vari2;
                        }
                        varjattavaRuutu = ruutu;
                        timer.Start();
                    }
                    i++;
                }
            }
        }

        /// <summary>
        /// Vaihdetaan ruutujen 1 värejä.
        /// </summary>
        /// <param name="vari"></param>
        public void VaihdaRuutujenVari(SolidColorBrush vari)
        {
            int a;
            int i = 1;
            int koko = ruutulista.Count;
            if (koko % 2 != 0)
            {
                a = koko / 2 + 1;
            }
            else
            {
                a = koko / 2;
            }
            foreach (Rectangle ruutu in ruutulista)
            {
                if (i <= a)
                {
                    ruutu.Fill = vari;
                }
                i++;
            }
        }


        /// <summary>
        /// Vaihdetaan ruutujen 2 värejä.
        /// </summary>
        /// <param name="vari"></param>
        public void VaihdaRuutujenVari2(SolidColorBrush vari)
        {
            int a;
            int i = 1;
            int koko = ruutulista.Count;
            if (koko % 2 != 0)
            {
                a = koko / 2 + 1;
            }
            else
            {
                a = koko / 2;
            }
            foreach (Rectangle ruutu in ruutulista)
            {
                if (i > a)
                {
                    ruutu.Fill = vari;
                }
                i++;
            }
        }


        /// <summary>
        /// Palauttaa pelikentän koon.
        /// </summary>
        /// <returns></returns>
        public int GetPelikentanKoko()
        {
            return pelikentanKoko;
        }


        /// <summary>
        /// Asetetaan vuoro, tarvitaan kun aloitetaan uusi peli.
        /// </summary>
        public void SetVuoro(int vuoro1)
        {
            vuoro = vuoro1;
        }


        /// <summary>
        /// Palautetaan onko vuoro kummalla puolella.
        /// </summary>
        /// <returns></returns>
        public int GetVuoro()
        {
            return vuoro;
        }


        /// <summary>
        /// Vaihdetaan vuoroa.
        /// </summary>
        public void VaihdaVuoro()
        {
            if (vuoro == 1)
            {
                vuoro = 2;
            }
            else vuoro = 1;
            RaiseVuoroChangedEvent();
        }


        /// <summary>
        /// Palauttaa rowin.
        /// </summary>
        /// <returns></returns>
        public int GetRow()
        {
            return row;
        }


        /// <summary>
        /// Palauttaa columnin.
        /// </summary>
        /// <returns></returns>
        public int GetColumn()
        {
            return column;
        }


        /// <summary>
        /// Kun painetaan vasenta hiiren painiketta selvitetään missä rowissa/columnissa 
        /// hiiri sijaitsee klikkaushetkellä.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(this);

            column = -1;
            double total = 0;
            foreach (ColumnDefinition clm in myGrid.ColumnDefinitions)
            {
                if (position.X < total)
                {
                    break;
                }
                column++;
                total += clm.ActualWidth;
            }

            row = -1;
            total = 0;
            foreach (RowDefinition rowDef in myGrid.RowDefinitions)
            {
                if (position.Y < total)
                {
                    break;
                }
                row++;
                total += rowDef.ActualHeight;
            }

            RaiseClickEvent();
        }


        /// <summary>
        /// Click-eventin alustus.
        /// </summary>
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
            "Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pelikentta));

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        void RaiseClickEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(Pelikentta.ClickEvent);
            RaiseEvent(newEventArgs);
        }


        /// <summary>
        /// VuoroChanged-eventin alustus.
        /// </summary>
        public static readonly RoutedEvent VuoroChangedEvent = EventManager.RegisterRoutedEvent(
            "VuoroChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pelikentta));

        public event RoutedEventHandler VuoroChanged
        {
            add { AddHandler(VuoroChangedEvent, value); }
            remove { RemoveHandler(VuoroChangedEvent, value); }
        }
 
        void RaiseVuoroChangedEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(Pelikentta.VuoroChangedEvent);
            RaiseEvent(newEventArgs);
        }


        /// <summary>
        /// Tehdään uusi kenttä, uusitaan ruudut.
        /// </summary>
        /// <param name="koko">Pelikentän koko</param>
        /// <param name="uusiVari">Ruutujen 1 uusi väri</param>
        /// <param name="uusiVari2">Ruutujen 2 uusi väri</param>
        public void TeeUusiKentta(int koko, SolidColorBrush uusiVari, SolidColorBrush uusiVari2)
        {
            pelikentanKoko = koko + 6;
            foreach (Rectangle ruutu in ruutulista)
            {
                myGrid.Children.Remove(ruutu);
            }
            ruutulista.Clear();
            myGrid.ColumnDefinitions.Clear();
            myGrid.RowDefinitions.Clear();

            for (int i = 0; i < pelikentanKoko; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                myGrid.ColumnDefinitions.Add(colDef);
                RowDefinition rowDef = new RowDefinition();
                myGrid.RowDefinitions.Add(rowDef);
            }

            for (int o = 0; o < pelikentanKoko; o += 2)
            {
                for (int i = 0; i < pelikentanKoko; i += 2)
                {
                    Rectangle ruutu = new Rectangle();
                    ruutulista.Add(ruutu);
                    Grid.SetRow(ruutu, o);
                    Grid.SetColumn(ruutu, i);
                    ruutu.Fill = uusiVari;
                    myGrid.Children.Add(ruutu);
                }

                if ((pelikentanKoko % 2 != 0) && (o < pelikentanKoko - 1))
                {
                    for (int a = 1; a < pelikentanKoko; a += 2)
                    {
                        Rectangle ruutu = new Rectangle();
                        ruutulista.Add(ruutu);
                        Grid.SetRow(ruutu, (o + 1));
                        Grid.SetColumn(ruutu, a);
                        ruutu.Fill = uusiVari;
                        myGrid.Children.Add(ruutu);
                    }
                }

                if (pelikentanKoko % 2 == 0)
                {
                    for (int a = 1; a < pelikentanKoko; a += 2)
                    {
                        Rectangle ruutu = new Rectangle();
                        ruutulista.Add(ruutu);
                        Grid.SetRow(ruutu, (o + 1));
                        Grid.SetColumn(ruutu, a);
                        ruutu.Fill = uusiVari;
                        myGrid.Children.Add(ruutu);
                    }
                }
            }

            for (int o = 0; o < pelikentanKoko; o += 2)
            {
                for (int i = 0; i < pelikentanKoko - 1; i += 2)
                {
                    Rectangle ruutu = new Rectangle();
                    ruutulista.Add(ruutu);
                    Grid.SetRow(ruutu, o);
                    Grid.SetColumn(ruutu, i + 1);
                    ruutu.Fill = uusiVari2;
                    myGrid.Children.Add(ruutu);
                }

                if ((pelikentanKoko % 2 != 0) && (o < pelikentanKoko - 1))
                {
                    for (int a = 1; a < pelikentanKoko + 1; a += 2)
                    {
                        Rectangle ruutu = new Rectangle();
                        ruutulista.Add(ruutu);
                        Grid.SetRow(ruutu, (o + 1));
                        Grid.SetColumn(ruutu, a - 1);
                        ruutu.Fill = uusiVari2;
                        myGrid.Children.Add(ruutu);
                    }
                }

                if (pelikentanKoko % 2 == 0)
                {
                    for (int a = 1; a < pelikentanKoko; a += 2)
                    {
                        Rectangle ruutu = new Rectangle();

                        ruutulista.Add(ruutu);
                        Grid.SetRow(ruutu, (o + 1));
                        Grid.SetColumn(ruutu, a - 1);
                        ruutu.Fill = uusiVari2;
                        myGrid.Children.Add(ruutu);
                    }
                }
            }
        }
    }
}
