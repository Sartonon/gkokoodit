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

namespace Vt6
{
    /// <summary>
    /// Interaction logic for labelYhteensa.xaml
    /// </summary>
    public partial class LabelYhteensa : UserControl
    {
        Random rand = new Random();
        private int o = 0;
        private int paikkaX = 0;
        private int paikkaY = 0;
        private int nopeusX = 1;
        private int nopeusY = 1;
        private int laskuri = 0;
        private int leveys;
        private int korkeus;
        DispatcherTimer timer = new DispatcherTimer();
        

        public int Alkuarvo
        {
            get { return (int)label1.Content; }
            set { label1.Content = value; laskuri = value; }
        }

        public LabelYhteensa()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Start();
            

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (o == 0)
            {
                paikkaX = (int)Canvas.GetLeft(this);
                paikkaY = (int)Canvas.GetTop(this);
                o++;
            }
            
            paikkaX += nopeusX;
            paikkaY += nopeusY;
            if (paikkaX > (leveys - this.ActualWidth) || paikkaX < 0) nopeusX = -nopeusX;          
            if (paikkaY > (korkeus - this.ActualHeight) || paikkaY < 0) nopeusY = -nopeusY;
            Canvas.SetLeft(this, paikkaX);
            Canvas.SetTop(this, paikkaY);
        }


        public static readonly RoutedEvent NumeroChangedEvent = EventManager.RegisterRoutedEvent(
          "NumeroChanged", // tapahtuman nimi
          RoutingStrategy.Bubble, // toimintatapa
          typeof(RoutedPropertyChangedEventHandler<int>), //eventissä liikuteltavan tiedon tietotyyppi int
          typeof(LabelYhteensa)); // luokka johon event lisätään


        public event RoutedPropertyChangedEventHandler<int> NumeroChanged
        {
            add { AddHandler(NumeroChangedEvent, value); }
            remove { RemoveHandler(NumeroChangedEvent, value); }
        }

        public void SizeChanged_1(int a, int b)
        {
            leveys = a;
            korkeus = b;
        }

        private void Label_Drop_1(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                try
                {
                    string txt = e.Data.GetData(DataFormats.Text) as string;
                    int luku = Convert.ToInt16(txt);
                    
                    laskuri *= luku;
                    if (nopeusX > 0) nopeusX += luku;
                    else nopeusX -= luku;
                    if (nopeusY > 0) nopeusY += luku;
                    else nopeusY -= luku;
                    label1.Content = laskuri.ToString();
                    RoutedPropertyChangedEventArgs<int> args = new RoutedPropertyChangedEventArgs<int>(
                    laskuri - luku, laskuri, NumeroChangedEvent);
                    RaiseEvent(args);
                    e.Handled = true;
                }
                catch (Exception)
                {
                    return;
                }
            }
        }


    }
}
