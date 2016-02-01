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

namespace Jakolasku
{
    /// <summary>
    /// Interaction logic for Jako.xaml
    /// </summary>
    public partial class Jako : UserControl
    {
        
        public Jako()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Jaettavaproperty
        /// </summary>
        public static readonly DependencyProperty JaettavaProperty =
         DependencyProperty.Register(
           "Jaettava",
           typeof(int), // propertyn tietotyyppi
           typeof(Jako), // luokka jossa property sijaitsee
           new FrameworkPropertyMetadata(0,  // propertyn oletusarvo
              FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
              new PropertyChangedCallback(OnValueChanged),  // kutsutaan propertyn arvon muuttumisen jälkeen
              new CoerceValueCallback(MuutaJaettava)));

        public int Jaettava
        {
            get { return (int)GetValue(JaettavaProperty); }
            set { SetValue(JaettavaProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private static object MuutaJaettava(DependencyObject d, object baseValue)
        {
            int luku = (int)baseValue;
            return luku;

        }

        public static readonly DependencyProperty JakajaProperty =
         DependencyProperty.Register(
           "Jakaja",
           typeof(int), // propertyn tietotyyppi
           typeof(Jako), // luokka jossa property sijaitsee
           new FrameworkPropertyMetadata(0,  // propertyn oletusarvo
              FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
              new PropertyChangedCallback(OnValueChanged),  // kutsutaan propertyn arvon muuttumisen jälkeen
              new CoerceValueCallback(MuutaJakaja)));

        public int Jakaja
        {
            get { return (int)GetValue(JakajaProperty); }
            set { SetValue(JakajaProperty, value); }
        }

        private static object MuutaJakaja(DependencyObject d, object baseValue)
        {
            int luku = (int)baseValue;
            return luku;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double jaettava = Jaettava;
                double jakaja = Jakaja;
                double jako = jaettava / jakaja;
                JakoTulos.Content = Math.Round(jako, 2);
            }
            catch
            {

            }
        } 
    }
}
