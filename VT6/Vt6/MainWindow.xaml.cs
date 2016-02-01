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

namespace Vt6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int korkeus;
        int leveys;
        int yhteensaNumero = 0;
        Random rand = new Random();
        int kerrat = 0;
        List<LabelYhteensa> labelit = new List<LabelYhteensa>();
         
        public MainWindow()
        {
            InitializeComponent();
            
        }


        /// <summary>
        /// Luodaan neljä Yhteensa labelii dynaamisesti ja lisätään tapahtuma.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_Loaded(object sender, RoutedEventArgs e)
        {
            
            int koko = 2;
            int fontsize = 50;

            for (int i = 0; i < 4; i++)
            {
                LabelYhteensa label1 = new LabelYhteensa();
                label1.SizeChanged_1((int)canvas.ActualWidth, (int)canvas.ActualHeight);
                labelit.Add(label1);
                label1.NumeroChanged += label1_NumeroChanged;
                Canvas.SetLeft(label1, rand.Next(0, (int)canvas.ActualWidth));
                Canvas.SetTop(label1, rand.Next(0, (int)canvas.ActualHeight - 60));
                canvas.Children.Add(label1);
                label1.Alkuarvo = koko;
                koko += 2;
                label1.FontSize = fontsize;
                fontsize -= 10;
            }
        }

        /// <summary>
        /// Kun numero muuttuu päivitetään summaa ohjelman ylänurkassa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_NumeroChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {    
            yhteensaNumero = yhteensaNumero + e.NewValue;
            yhteensa.Content = yhteensaNumero.ToString();
        }

        private void canvas_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                try
                {
                    
                    string txt = e.Data.GetData(DataFormats.Text) as string;
                    int luku = Convert.ToInt16(txt);
                    LabelYhteensa label = new LabelYhteensa();
                    label.SizeChanged_1(leveys, korkeus);
                    labelit.Add(label);
                    label.NumeroChanged += label1_NumeroChanged; // tämä ei suostunut toimimaan xamlin puolella
                    kerrat++;
                    int koko = rand.Next(20, 50);
                    label.Alkuarvo = luku * -kerrat;
                    label.FontSize = koko;
                    Canvas.SetLeft(label, rand.Next(0, (int)canvas.ActualWidth));
                    Canvas.SetTop(label, rand.Next(0, (int)canvas.ActualHeight - 60));
                    canvas.Children.Add(label);
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Päivitetään ikkunan koon tiedot LabelYhteensa olioille kun ikkunan kokoa muutetaan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            korkeus = (int)canvas.ActualHeight;
            leveys = (int)canvas.ActualWidth;

            foreach(LabelYhteensa label in labelit)
            {
                label.SizeChanged_1(leveys, korkeus);
            }
        }
    }
}
