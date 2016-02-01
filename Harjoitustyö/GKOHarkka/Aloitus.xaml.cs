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

namespace GKOHarkka
{
    /// <summary>
    /// Aloitus-ikkunassa kysytään pelaajien nimet, sekä pelimuoto jota
    /// halutaan pelata. Jos nimiä ei valita, pelaajilla on geneeriset nimet.
    /// </summary>
    public partial class Aloitus : Window
    {
        string pelaaja1;
        string pelaaja2;

        public Aloitus()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Jatka-nappulasta jatketaan MainWindowiin, jossa otetaan nimet talteen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jatkaNappula_Click(object sender, RoutedEventArgs e)
        {
            pelaaja1 = pelaaja1Nimi.Text.ToString();
            pelaaja2 = pelaaja2Nimi.Text.ToString();
            this.Close();
        }
    }
}
