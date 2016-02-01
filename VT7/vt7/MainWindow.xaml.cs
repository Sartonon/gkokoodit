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
using System.Windows.Markup;

namespace Vt7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Element_MediaOpened(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// Print-buttonin toiminta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            int t = 0;
            string[] text = System.IO.File.ReadAllLines(@"esimerkkiteksti.txt");
            int pituus = text.Length;
           // double sivujenMaara = pituus / 55.0;
            double sivujenMaara = 9.8;

            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() != true) return;

            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize = new Size(dialog.PrintableAreaWidth, dialog.PrintableAreaHeight);

            TextBlock otsikko = new TextBlock();
            otsikko.Text = "GKO";
            otsikko.FontSize = 40;

            for (int i = 1; i < sivujenMaara + 1; i++)
            {
                StringBuilder tekstiB = new StringBuilder();
                FixedPage page1 = new FixedPage();
                //page1.RenderTransform = new TranslateTransform(20, 20);
                page1.Width = document.DocumentPaginator.PageSize.Width;
                page1.Height = document.DocumentPaginator.PageSize.Height;

                // Pistetään tekstit dockpanelin sisään
                DockPanel panel = new DockPanel();
                panel.Width = page1.Width;
                panel.Height = page1.Height;
                panel.LastChildFill = true;
                page1.Children.Add(panel);

                TextBlock header = new TextBlock();
                header.Text = "Sivu " + i.ToString();
                header.FontSize = 20;
                header.HorizontalAlignment = HorizontalAlignment.Right;
                panel.Children.Add(header);
                DockPanel.SetDock(header, Dock.Top);

                if (t == 0)
                {
                    panel.Children.Add(otsikko);
                    DockPanel.SetDock(otsikko, Dock.Top);
                    otsikko.HorizontalAlignment = HorizontalAlignment.Center;
                }
                
                // Määritetään footeri
                TextBlock footer = new TextBlock();
                footer.Text = "TIEA212";
                footer.FontSize = 20;
                footer.HorizontalAlignment = HorizontalAlignment.Right;
                panel.Children.Add(footer); 
                DockPanel.SetDock(footer, Dock.Bottom);

                TextBlock page1Text = new TextBlock();
                page1Text.HorizontalAlignment = HorizontalAlignment.Center;
                page1Text.TextWrapping = TextWrapping.Wrap;

                for (int p = 0; p < 55; p++)
                {
                    tekstiB.Append(text[t] + "\n");
                    t++;
                    if (t >= text.Length) break;
                }

                page1Text.Text = tekstiB.ToString();
                page1Text.FontSize = 12;
                page1Text.Margin = new Thickness(30);
                panel.Children.Add(page1Text);
                DockPanel.SetDock(page1Text, Dock.Top);
                


                PageContent page1Content = new PageContent();
                ((IAddChild)page1Content).AddChild(page1);
                document.Pages.Add(page1Content);

            }

            dialog.PrintDocument(document.DocumentPaginator, "Fixed document");
        }
    }
}
