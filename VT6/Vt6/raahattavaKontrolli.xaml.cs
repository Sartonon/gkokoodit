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
    /// Interaction logic for raahattavaKontrolli.xaml
    /// </summary>
    public partial class raahattavaKontrolli : UserControl
    {

        /// <summary>
        /// Luodaan Laskuri-property
        /// </summary>
        public string Laskuri
        {
            get { return label1.Content.ToString(); }
            set { label1.Content = value; }
        }


        Point startPoint = new Point(0, 0);

        public raahattavaKontrolli()
        {
            InitializeComponent();
        }
            

            private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                Point mousePos = e.GetPosition(null);
                Vector diff = startPoint - mousePos;
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DataObject dragData = new DataObject(DataFormats.Text, ((Label)sender).Content);
                    DragDrop.DoDragDrop((Label)sender, dragData, DragDropEffects.Move);
                }
            }

            private void label1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                startPoint = e.GetPosition(null);
            }
        }
    }
