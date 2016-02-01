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
using System.IO;
using System.Windows.Markup;

namespace GKOHarkka
{
    /// <summary>
    /// Sijoitetaan omat nappula ja pelilauta -kontrollit MainWindowiin
    /// </summary>
    public partial class MainWindow : Window
    {

        Boolean varjataanko = true;
   //     Boolean vaihdaVuoro = true;
        // Luodaan lista nappuloille
        List<Pelinappula> lista = new List<Pelinappula>();
        Saannot saannot; // Säännöt-ikkuna-olion alustus, helpompi tarkistaa onko valmiiksi avattu jo ikkuna
        About about;
        TextBlock voittaja;
        Random vuoroRandom = new Random();
        StringBuilder muisti = new StringBuilder();
        Boolean soiko = false;
        Asetukset asetukset;
        SolidColorBrush pelaaja1VariMuisti;
        SolidColorBrush pelaaja2VariMuisti;
        SolidColorBrush ruudukko1VariMuisti = new SolidColorBrush(Colors.Blue);
        SolidColorBrush ruudukko2VariMuisti = new SolidColorBrush(Colors.Cyan);
        string pelaaja1 = "Pelaaja 1";
        string pelaaja2 = "Pelaaja 2";
        string pelimuoto;
        int rowKlikkaus;
        int columnKlikkaus;
        int columnNappula;
        int rowNappula;
        Boolean joLiikutettu;
        Boolean liikuttuEriSuuntaan = false;
        int pelaaja1NappuloidenMaara = 0;
        int pelaaja2NappuloidenMaara = 0;
        private MediaPlayer mediaPlayer = new MediaPlayer();
        int ruudukonKoko = 2;

        /// <summary>
        /// Asetetaan ruudukon koko.
        /// </summary>
        /// <param name="i"></param>
        public void SetRuudukonKoko(int i)
        {
            ruudukonKoko = i;
        }

        /// <summary>
        /// Soitetaan voittoääni
        /// </summary>
        private void SoitaAani()
        {
            mediaPlayer.Open(new Uri("win.mp3", UriKind.Relative));
            mediaPlayer.Play();
        }


        /// <summary>
        /// Alustetaan MainWindow.
        /// </summary>
        public MainWindow()
        {
            Aloitus aloitus = new Aloitus();
            aloitus.ShowDialog();
            if (aloitus.pelaaja1Nimi.Text != "") pelaaja1 = aloitus.pelaaja1Nimi.Text.ToString();
            if (aloitus.pelaaja2Nimi.Text != "") pelaaja2 = aloitus.pelaaja2Nimi.Text.ToString();
            if (aloitus.tammi.IsChecked == true) pelimuoto = "Tammi";
            else pelimuoto = "Breakthrough";

            InitializeComponent();
            luovutus1.Header = "Luovuta " + "(" + pelaaja1 + ")" + " (Ctrl + L + F1)";
            luovutus2.Header = "Luovuta " + "(" + pelaaja2 + ")" + " (Ctrl + L + F2)";
            this.MinHeight = 200;
            this.MinWidth = 200;

        }


        /// <summary>
        /// Lisätään pelinappulat kun pelikenttä on ladattu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pelikentta_Loaded(object sender, RoutedEventArgs e)
        {
            if (pelimuoto.Equals("Tammi"))
            {
                AlustaTammi();
            }
            else
            {
                AlustaBreakthrough();
            }
        }


        /// <summary>
        /// Tammen monimutkainen alustus, otetti huomioon kaikki
        /// eri pelilaudan koot.
        /// </summary>
        private void AlustaTammi()
        {
            pelaaja1NappuloidenMaara = 0;
            pelaaja2NappuloidenMaara = 0;
            int pariton = 0;
            int pelikentanKoko = pelikentta.GetPelikentanKoko();
            // Ensimmäisen pelaajan nappulat sijoittuvat samalla tavalla riippumatta laudan koosta
            for (int i = 0; i < 3; i++)
            {
                for (int o = 0; o < pelikentanKoko; o += 2)
                {
                    if (pariton % 2 == 0)
                    {
                        Pelinappula nappula = new Pelinappula();
                        nappula.SetPelaaja(1);
                        if (pelaaja1VariMuisti != null) nappula.SetVari1(pelaaja1VariMuisti);
                        pelaaja1NappuloidenMaara++;
                        nappula.ClickLol += nappula_Click;
                        Grid.SetColumn(nappula, o);
                        Grid.SetRow(nappula, i);
                        pelikentta.myGrid.Children.Add(nappula);
                        lista.Add(nappula);

                    }
                    else
                    {
                        if (o < pelikentanKoko - 1)
                        {
                            Pelinappula nappula = new Pelinappula();
                            nappula.SetPelaaja(1);
                            if (pelaaja1VariMuisti != null) nappula.SetVari1(pelaaja1VariMuisti);
                            pelaaja1NappuloidenMaara++;
                            nappula.ClickLol += nappula_Click;
                            Grid.SetColumn(nappula, o + 1);
                            Grid.SetRow(nappula, i);
                            pelikentta.myGrid.Children.Add(nappula);
                            lista.Add(nappula);
                        }
                    }

                }
                pariton++;
            }
            pariton = 0;
            // Toisen pelaajan nappuloiden sijoittelu on vähän monimutkaisempi
            // Menevät eri tavalla riippuen onko pelilaudan koko pariton vai parillinen
            if (pelikentanKoko % 2 != 0)
            {
                for (int i = pelikentanKoko - 1; i > pelikentanKoko - 4; i--)
                {
                    for (int o = 0; o < pelikentanKoko; o += 2)
                    {
                        if (pariton % 2 == 0)
                        {
                            Pelinappula nappula = new Pelinappula();
                            nappula.SetPelaaja(2);
                            if (pelaaja2VariMuisti != null) nappula.SetVari2(pelaaja2VariMuisti);
                            pelaaja2NappuloidenMaara++;
                            nappula.ClickLol += nappula_Click;
                            Grid.SetColumn(nappula, o);
                            Grid.SetRow(nappula, i);
                            pelikentta.myGrid.Children.Add(nappula);
                            lista.Add(nappula);
                        }
                        else
                        {
                            if (o < pelikentanKoko - 1)
                            {
                                Pelinappula nappula = new Pelinappula();
                                nappula.SetPelaaja(2);
                                if (pelaaja2VariMuisti != null) nappula.SetVari2(pelaaja2VariMuisti);
                                pelaaja2NappuloidenMaara++;
                                nappula.ClickLol += nappula_Click;
                                Grid.SetColumn(nappula, o + 1);
                                Grid.SetRow(nappula, i);
                                pelikentta.myGrid.Children.Add(nappula);
                                lista.Add(nappula);
                            }
                        }

                    }

                    pariton++;
                }
            }
            else
            {
                for (int i = pelikentanKoko - 1; i > pelikentanKoko - 4; i--)
                {
                    for (int o = 0; o < pelikentanKoko; o += 2)
                    {
                        if (pariton % 2 == 0)
                        {
                            Pelinappula nappula = new Pelinappula();
                            nappula.SetPelaaja(2);
                            if (pelaaja2VariMuisti != null) nappula.SetVari2(pelaaja2VariMuisti);
                            pelaaja2NappuloidenMaara++;
                            nappula.ClickLol += nappula_Click;
                            Grid.SetColumn(nappula, o + 1);
                            Grid.SetRow(nappula, i);
                            pelikentta.myGrid.Children.Add(nappula);
                            lista.Add(nappula);
                        }
                        else
                        {
                            if (o < pelikentanKoko - 1)
                            {
                                Pelinappula nappula = new Pelinappula();
                                nappula.SetPelaaja(2);
                                if (pelaaja2VariMuisti != null) nappula.SetVari2(pelaaja2VariMuisti);
                                pelaaja2NappuloidenMaara++;
                                nappula.ClickLol += nappula_Click;
                                Grid.SetColumn(nappula, o);
                                Grid.SetRow(nappula, i);
                                pelikentta.myGrid.Children.Add(nappula);
                                lista.Add(nappula);
                            }
                        }

                    }
                    pariton++;
                }
            }
            pariton = 0;
            ArvoVuoro(lista);
        }



        /// <summary>
        /// Alustetaan Breakthrough-pelikenttä.
        /// </summary>
        private void AlustaBreakthrough()
        {
            int pelikentanKoko = pelikentta.GetPelikentanKoko();
            // Toisen pelaajan nappulat
            for (int i = 0; i < 2; i++)
            {
                for (int o = 0; o < pelikentanKoko; o++)
                {
                    Pelinappula nappula = new Pelinappula();
                    nappula.SetPelaaja(1);
                    if (pelaaja1VariMuisti != null) nappula.SetVari1(pelaaja1VariMuisti);
                    nappula.ClickLol += nappula_Click;
                    Grid.SetColumn(nappula, o);
                    Grid.SetRow(nappula, i);
                    pelikentta.myGrid.Children.Add(nappula);
                    lista.Add(nappula);

                }
            }


            // Ja toisen pelaajan nappulat
            for (int i = pelikentanKoko - 1; i > pelikentanKoko - 3; i--)
            {
                for (int o = 0; o < pelikentanKoko; o++)
                {
                    Pelinappula nappula = new Pelinappula();
                    nappula.SetPelaaja(2);
                    if (pelaaja2VariMuisti != null) nappula.SetVari2(pelaaja2VariMuisti);
                    nappula.ClickLol += nappula_Click;
                    Grid.SetColumn(nappula, o);
                    Grid.SetRow(nappula, i);
                    pelikentta.myGrid.Children.Add(nappula);
                    lista.Add(nappula);
                }
            }

            ArvoVuoro(lista);
        }


        /// <summary>
        /// Arvotaan peli aloittaja
        /// </summary>
        private void ArvoVuoro(List<Pelinappula> lista)
        {
            int vuoro = vuoroRandom.Next(1, 3);
            pelikentta.SetVuoro(vuoro);

            foreach (Pelinappula nappula in lista)
            {
                if (nappula.GetPelaaja() == vuoro)
                {
                    nappula.SetVuoro(true);
                }
                else
                {
                    nappula.SetVuoro(false);
                }

            }
        }


        /// <summary>
        /// Käydään poistamassa valinta nappuloilta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nappula_Click(object sender, RoutedEventArgs e)
        {
            foreach (Pelinappula nappula in lista)
            {
                nappula.Valittu(false);
            }
        }


        /// <summary>
        /// Siirretään valittua nappulaan ruutuun jota klikataan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pelikentta_Click(object sender, RoutedEventArgs e)
        {
            if (pelimuoto.Equals("Breakthrough"))
            {
                foreach (Pelinappula nappula in lista)
                {
                    TarkistaLiikutusBreakthrough(nappula);
                }
                varjataanko = true;
            }

            if (pelimuoto.Equals("Tammi"))
            {
                foreach (Pelinappula nappula in lista)
                {
                    TarkistaLiikutusTammi(nappula);
                }

                liikuttuEriSuuntaan = false;
                joLiikutettu = false;
                varjataanko = true;

            }
        }


        /// <summary>
        /// Tarkistetaan nappulan liikutus tammi-pelissä.
        /// </summary>
        /// <param name="nappula"></param>
        private void TarkistaLiikutusTammi(Pelinappula nappula)
        {
            rowKlikkaus = pelikentta.GetRow();

            if (nappula.OnkoValittu())
            {
                /// AUTOMAATTILIIKUTUS
                bool liikutettu = TarkistaMahdollisetLiikutukset(nappula);
                if (liikutettu == true) return;
                if (nappula.GetPelaaja() == 1)
                {
                    if (nappula.GetTammi())
                    {
                        if (Grid.GetRow(nappula) - rowKlikkaus == 1)
                        {
                            liikuttuEriSuuntaan = true;
                            OnkoRuudussaNappula(-1, 1, -1, 2, 1, nappula);
                        }
                        else
                        {
                            OnkoRuudussaNappula(1, 1, -1, 2, 1, nappula);
                        }
                    }
                    else
                    {
                        OnkoRuudussaNappula(1, 1, -1, 2, 1, nappula);
                    }

                }

                if (nappula.GetPelaaja() == 2)
                {
                    if (nappula.GetTammi())
                    {
                        if (Grid.GetRow(nappula) - rowKlikkaus == -1)
                        {
                            liikuttuEriSuuntaan = true;
                            OnkoRuudussaNappula(1, 1, -1, 1, 2, nappula);
                        }
                        else
                        {
                            OnkoRuudussaNappula(-1, 1, -1, 1, 2, nappula);
                        }

                    }
                    else
                    {
                        // Siirryttävät ruudut ja pelaajat
                        OnkoRuudussaNappula(-1, 1, -1, 1, 2, nappula);
                    }

                }
            }
        }


        /// <summary>
        /// Tarkistetaan mahdolliset automaattiset liikutukset.
        /// </summary>
        /// <param name="nappula">Liikutettava nappula</param>
        private bool TarkistaMahdollisetLiikutukset(Pelinappula nappula)
        {

            if (nappula.GetTammi())
            {

            }
            else
            {
                bool siirretty = TarkistaVapaatSiirrot(nappula);
                return siirretty;
            }
            return false;

        }


        /// <summary>
        /// Tarkistaa onko nappulalla vapaita siirjtoja.
        /// </summary>
        /// <param name="nappula"></param>
        /// <param name="pelaaja"></param>
        /// <returns></returns>
        private bool TarkistaVapaatSiirrot(Pelinappula nappula)
        {
            int nappulaColumn = Grid.GetColumn(nappula);
            int nappulaRow = Grid.GetRow(nappula);
            if (nappula.GetPelaaja() == 1 && !nappula.GetTammi())
            {
                if (nappulaColumn == 0 || nappulaColumn == pelikentta.GetPelikentanKoko() - 1) // tarkistetaan onko nappula ruudukon vasemmassa vai oikeassa reunassa
                {
                    if (nappulaColumn == 0) // jos column vasemmassa reunassa
                    {
                        bool a = Nappula1ColumnReuna(nappula, nappulaColumn, nappulaRow, 1, 1, 2, 2);
                        return a;
                    }
                    else // jos column oikea reuna
                    {
                        bool a = Nappula1ColumnReuna(nappula, nappulaColumn, nappulaRow, -1, 1, -2, 2);
                        return a;
                    }
                }
                else // Tänne kaikki muut automaattiset siirrot
                {

                }
            }
            else if (nappula.GetPelaaja() == 1 && nappula.GetTammi()) // tänne tamminappulan 1 automaattisiirrot
            {

            }

            if (nappula.GetPelaaja() == 2 && !nappula.GetTammi())
            {
                if (nappulaColumn == 0 || nappulaColumn == pelikentta.GetPelikentanKoko() - 1)
                {
                    if (nappulaColumn == 0) // jos column vasemmassa reunassa
                    {
                        bool a = Nappula1ColumnReuna(nappula, nappulaColumn, nappulaRow, 1, -1, 2, -2);
                        return a;
                    }
                    else // jos column oikeassa reunassa
                    {
                        bool a = Nappula1ColumnReuna(nappula, nappulaColumn, nappulaRow, -1, -1, -2, -2);
                        return a;
                    }
                }
                else // tänne kaikki muut automaattisiirrot
                {

                }
            }
            else if (nappula.GetPelaaja() == 2 && nappula.GetTammi()) // tamminappulan 2 automaattisiirrot
            {

            }
            return false;
        }


        /// <summary>
        /// Tarkistetaan nappulan automaattiliikutus jos se on pelaajan 1 nappula
        /// ja sijaitsee oikeassa laidassa.
        /// </summary>
        /// <param name="nappula">Automaattisesti liikutettava nappula</param>
        /// <param name="nappulaColumn">Automaattisesti liikutettavan nappulan column</param>
        /// <param name="nappulaRow">Automaattisesti liikutettavan nappulan row</param>
        /// <param name="p1">Liikutusehto 1 kun ei syödä</param>
        /// <param name="p2">Liikutusehto 2 kun ei syödä</param>
        /// <param name="p11">Liikutusehto 1 kun syödään automaattisesti</param>
        /// <param name="p22">Liikutusehto 2 kun syödään automaattisesti</param>
        /// <returns>Palauttaa false/true sen mukaan onko liikutettu automaattisesti</returns>
        private bool Nappula1ColumnReuna(Pelinappula nappula, int nappulaColumn, int nappulaRow, int p1, int p2, int p11, int p22)
        {
            Boolean liikutetaanko = true;

            if (nappulaRow < pelikentta.GetPelikentanKoko() - 2 && nappula.GetPelaaja() == 1 || nappula.GetPelaaja() == 2 && nappulaRow > 1)
            {
                foreach (Pelinappula nappula1 in lista)
                {
                    if (Grid.GetColumn(nappula1) == nappulaColumn + p1 && Grid.GetRow(nappula1) == nappulaRow + p2)
                    {
                        liikutetaanko = false;
                        if (nappula1.GetPelaaja() != nappula.GetPelaaja())
                        {
                            if (TarkistaOnkoRuutuTyhjaAutomaatti(nappulaColumn + p11, nappulaRow + p22, true))
                            {
                                soiko = true;
                                LiikutaNappulaa(nappula, nappulaColumn + p11, nappulaRow + p22);
                                SyoNappula(nappula1);
                                return true;

                            }
                            else
                            {
                                PoistaValinta();
                            }
                        }
                    }
                }
                if (liikutetaanko == true)
                {
                    LiikutaNappulaa(nappula, nappulaColumn + p1, nappulaRow + p2);
                    return true;
                }
                else
                {
                    TarkistaOnkoRuutuTyhjaAutomaatti(nappulaColumn + p1, nappulaRow + p2, true);
                    PoistaValinta();
                }
            }
            else
            {
                liikutetaanko = true;
                foreach (Pelinappula nappula1 in lista)
                {
                    if (Grid.GetColumn(nappula1) == nappulaColumn + p1 && Grid.GetRow(nappula1) == nappulaRow + p2)
                    {
                        liikutetaanko = false;
                        if (nappula1.GetPelaaja() == 2)
                        {
                            PoistaValinta();
                            liikutetaanko = false;
                            TarkistaOnkoRuutuTyhjaAutomaatti(nappulaColumn + p1, nappulaRow + p2, true);
                        }
                        if (nappula1.GetPelaaja() == 1)
                        {
                            PoistaValinta();
                            liikutetaanko = false;
                            TarkistaOnkoRuutuTyhjaAutomaatti(nappulaColumn + p1, nappulaRow + p2, true);
                        }
                    }
                }
                if (liikutetaanko == true)
                {
                    LiikutaNappulaa(nappula, nappulaColumn + p1, nappulaRow + p2);
                    return true;
                }
            }

            return false;
        }



        /// <summary>
        /// Poistetaan syöty nappula pelilaudalta ja tarkistetaan onko
        /// peli voitettu
        /// </summary>
        /// <param name="nappula1">Nappula joka syödään</param>
        private void SyoNappula(Pelinappula nappula1)
        {
            pelikentta.myGrid.Children.Remove(nappula1);
            Grid.SetColumn(nappula1, 200);
            Grid.SetRow(nappula1, 200);
            TarkistaVoitto(nappula1);
            nappula1.Syoty(true);
            joLiikutettu = true;
            varjataanko = false;
        }


        /// <summary>
        /// Tarkistetaan voidaanko ruutuun liikkua automaattisessa siirrossa, jos ei
        /// niin värjätään ruutu punaiseksi.
        /// </summary>
        /// <param name="column">Column johon yritetään siirtää</param>
        /// <param name="row">Row johon yritetään siirtää</param>
        /// <returns></returns>
        private bool TarkistaOnkoRuutuTyhjaAutomaatti(int column, int row, bool vilkutetaanko)
        {
            foreach (Pelinappula nappula in lista)
            {
                if (Grid.GetColumn(nappula) == column && Grid.GetRow(nappula) == row)
                {
                    if (vilkutetaanko == true) pelikentta.VarjaaPunaiseksi(column, row, ruudukko1VariMuisti, ruudukko2VariMuisti);
                    return false;
                }
            }
            return true;
        }



        /// <summary>
        /// Tarkistaa onko siirryttävässä ruudussa nappula, jos ei niin siirrytään ja jos on 
        /// niin tarkisteaan voidaanko syödä.
        /// </summary>
        /// <param name="r1">Ehto 1</param>
        /// <param name="r2">Ehto 2</param>
        /// <param name="r3">Ehto 3</param>
        /// <param name="p1">Oma nappula</param>
        /// <param name="p2">Vastustajan nappula</param>
        private void OnkoRuudussaNappula(int r1, int r2, int r3, int p2, int p1, Pelinappula nappula)
        {
            rowKlikkaus = pelikentta.GetRow();
            columnKlikkaus = pelikentta.GetColumn();
            columnNappula = Grid.GetColumn(nappula);
            rowNappula = Grid.GetRow(nappula);

            if ((rowKlikkaus - rowNappula) == r1 && ((columnKlikkaus - columnNappula) == r2 || (columnKlikkaus - columnNappula) == r3))
            {
                // Käydään läpi nappulat, että tiedetään syökö nappula toisen nappulan tai onko ruudussa jo oma nappula.
                foreach (Pelinappula nappula1 in lista)
                {
                    // Tarkistetaan onko ruudussa johon liikutaan vastustajan nappula
                    if (Grid.GetRow(nappula1) == rowKlikkaus && Grid.GetColumn(nappula1) == columnKlikkaus && nappula1.GetPelaaja() == p2)
                    {
                        TarkistaSyontiLiikutusTammi(nappula, nappula1);
                    }

                    // Lopetetaan jos siirryttävässä ruudussa on jo oma nappula.
                    if ((Grid.GetRow(nappula1) == rowKlikkaus) && (Grid.GetColumn(nappula1) == columnKlikkaus) && (nappula1.GetPelaaja() == p1))
                    {
                        pelikentta.VarjaaPunaiseksi(columnKlikkaus, rowKlikkaus, ruudukko1VariMuisti, ruudukko2VariMuisti);
                        return;
                    }
                }
                // Jos päästään tänne, voidaan siirtää valittu nappula klikattavaan kohtaan.
                if (joLiikutettu == false)
                {
                    liikuttuEriSuuntaan = false;
                    LiikutaNappulaa(nappula, columnKlikkaus, rowKlikkaus);
                    varjataanko = false;
                }
                joLiikutettu = false;
            }
            if (rowKlikkaus != rowNappula || columnKlikkaus != columnNappula)
            {
                if (varjataanko == true)
                {
                    pelikentta.VarjaaPunaiseksi(columnKlikkaus, rowKlikkaus, ruudukko1VariMuisti, ruudukko2VariMuisti);
                    varjataanko = true;
                }
            }
        }


        /// <summary>
        /// Tarkistaa muuttuuko nappula tammeksi, eli onko se pelilaudan toisessa päässä verrattuna
        /// aloituspäähän.
        /// </summary>
        /// <param name="nappula">Liikutettava nappula</param>
        private void TarkistaMuuttuukoTammeksi(Pelinappula nappula)
        {
            if (nappula.GetPelaaja() == 1)
            {
                if (Grid.GetRow(nappula) == pelikentta.GetPelikentanKoko() - 1)
                {
                    if (!nappula.GetTammi())
                    {
                        nappula.SuurennaNappula();
                        nappula.SetTammi(true);
                    }
                }
            }

            if (nappula.GetPelaaja() == 2)
            {
                if (Grid.GetRow(nappula) == 0)
                {
                    if (!nappula.GetTammi())
                    {
                        nappula.SuurennaNappula();
                        nappula.SetTammi(true);
                    }
                }
            }
        }


        /// <summary>
        /// Tarkistetaan voidaanko nappulalla syödä toinen nappula.
        /// </summary>
        /// <param name="nappula">Liikutettava nappula</param>
        /// <param name="syotavaNappula">Syötävä nappula</param>
        private void TarkistaSyontiLiikutusTammi(Pelinappula nappula, Pelinappula syotavaNappula)
        {
            int liikutettavanRow = Grid.GetRow(nappula);
            int liikutettavanColumn = Grid.GetColumn(nappula);
            int syotavaRow = Grid.GetRow(syotavaNappula);
            int syotavaColumn = Grid.GetColumn(syotavaNappula);
            int i = liikutettavanColumn - syotavaColumn;
            // Ei anneta hypätä pelilaudan ulkopuolelle, palataan.
            if (syotavaColumn == 0 || (syotavaColumn == pelikentta.GetPelikentanKoko() - 1) || (syotavaRow == 0) || (syotavaRow == pelikentta.GetPelikentanKoko() - 1))
            {
                pelikentta.VarjaaPunaiseksi(syotavaColumn, syotavaRow, ruudukko1VariMuisti, ruudukko2VariMuisti);
                joLiikutettu = true;
                return;
            }

            if (nappula.GetTammi() && liikuttuEriSuuntaan == true)
            {
                if (nappula.GetPelaaja() == 1)
                {
                    // oikea (-1) vasen (1)
                    if (i == -1) // oikea
                    {
                        VoikoLiikkuaTammi(liikutettavanColumn + 2, liikutettavanRow - 2, nappula, syotavaNappula);
                    }
                    if (i == 1) // vasen
                    {
                        VoikoLiikkuaTammi(liikutettavanColumn - 2, liikutettavanRow - 2, nappula, syotavaNappula);
                    }
                    return;
                }

                if (nappula.GetPelaaja() == 2)
                {
                    if (i == -1) // oikea
                    {
                        VoikoLiikkuaTammi(liikutettavanColumn + 2, liikutettavanRow + 2, nappula, syotavaNappula);

                    }
                    if (i == 1) // vasen
                    {
                        VoikoLiikkuaTammi(liikutettavanColumn - 2, liikutettavanRow + 2, nappula, syotavaNappula);
                    }
                    return;
                }

            }

            if (nappula.GetPelaaja() == 1)
            {
                // oikea (-1) vasen (1)
                if (i == -1) // oikea
                {
                    VoikoLiikkuaTammi(liikutettavanColumn + 2, liikutettavanRow + 2, nappula, syotavaNappula);
                }
                if (i == 1) // vasen
                {
                    VoikoLiikkuaTammi(liikutettavanColumn - 2, liikutettavanRow + 2, nappula, syotavaNappula);
                }
            }

            if (nappula.GetPelaaja() == 2)
            {
                if (i == -1) // oikea
                {
                    VoikoLiikkuaTammi(liikutettavanColumn + 2, liikutettavanRow - 2, nappula, syotavaNappula);

                }
                if (i == 1) // vasen
                {
                    VoikoLiikkuaTammi(liikutettavanColumn - 2, liikutettavanRow - 2, nappula, syotavaNappula);
                }
            }
        }


        /// <summary>
        /// Tarkistetaan voiko nappula liikkua tammessa.
        /// </summary>
        /// <param name="p1">Liikutusehto 1</param>
        /// <param name="p2">Liikutusehto 2</param>
        private void VoikoLiikkuaTammi(int p1, int p2, Pelinappula nappula, Pelinappula syotavaNappula)
        {
            Boolean a = TarkistaOnkoRuutuTyhja(p1, p2, syotavaNappula);
            if (a == true)
            {
                soiko = true;
                SyoNappula(syotavaNappula);
                LiikutaNappulaa(nappula, p1, p2);
                // SyoNappula(syotavaNappula);
            }
            else
            {
                joLiikutettu = true;
                return;
            }

        }


        /// <summary>
        /// Tarkistetaan voittiko jompi kumpi pelaaja pelin
        /// </summary>
        private void TarkistaVoitto(Pelinappula syotyNappula)
        {
            if (syotyNappula.GetPelaaja() == 1)
            {
                pelaaja1NappuloidenMaara--;
            }
            else pelaaja2NappuloidenMaara--;

            if (pelaaja1NappuloidenMaara == 0)
            {
                IlmoitaVoittaja(pelaaja2 + " voitti!");
            }

            if (pelaaja2NappuloidenMaara == 0)
            {
                IlmoitaVoittaja(pelaaja1 + " voitti!");
            }
        }


        /// <summary>
        /// Tarkistetaan onko nappulan toisella puolella tyhjä ruutu, eli voidaanko
        /// hypätä yli.
        /// </summary>
        /// <param name="liikutettavanColumn">Liikutettavan nappulan column</param>
        /// <param name="liikutettavanRow">Liikutettavan nappulan row</param>
        /// <returns></returns>
        private bool TarkistaOnkoRuutuTyhja(int liikutettavanColumn, int liikutettavanRow, Pelinappula syotavaNappula)
        {
            foreach (Pelinappula nappula in lista)
            {
                if (Grid.GetColumn(nappula) == liikutettavanColumn && Grid.GetRow(nappula) == liikutettavanRow)
                {
                    pelikentta.VarjaaPunaiseksi(columnKlikkaus, rowKlikkaus, ruudukko1VariMuisti, ruudukko2VariMuisti);
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// Kaikkien nappuloiden liikuttamiseen käytetty alihojelma.
        /// </summary>
        /// <param name="nappula">Liikutettava nappula</param>
        /// <param name="columnKlikkaus">Column johon ollaan siirtämässä</param>
        /// <param name="rowKlikkaus">Row johon ollaan siirtämässä</param>
        private void LiikutaNappulaa(Pelinappula nappula, int columnKlikkaus, int rowKlikkaus)
        {
            luovutus.IsEnabled = true;

            KirjoitaMuistiin(columnKlikkaus, rowKlikkaus, nappula);
            Grid.SetColumn(nappula, columnKlikkaus);
            Grid.SetRow(nappula, rowKlikkaus);
            if (!nappula.GetTammi()) TarkistaMuuttuukoTammeksi(nappula);
            if (soiko == true)
            {
                if (OnkoSyotavia(nappula))
                {

                }
                else
                {
                    pelikentta.VaihdaVuoro();
                    PoistaValinta();
                }
            }
            else
            {
                pelikentta.VaihdaVuoro();
                PoistaValinta();
            }
            soiko = false;
        }


        /// <summary>
        /// Tarkisetaan syönnin jälkeen voiko nappula syödä lisää, jos voi niin
        /// jätetään vuoro syöjälle jotta voi syödä lisää.
        /// </summary>
        /// <param name="nappula"></param>
        /// <returns></returns>
        private bool OnkoSyotavia(Pelinappula nappula)
        {
            int mahdollisetSiirrot = 0;
            // Pelaajan 1 syöminen
            if (nappula.GetPelaaja() == 1 && !nappula.GetTammi() && Grid.GetRow(nappula) < pelikentta.GetPelikentanKoko() - 2)
            {
                foreach (Pelinappula nappula1 in lista)
                {
                    if (Grid.GetRow(nappula1) == Grid.GetRow(nappula) + 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) + 1 && nappula1.GetPelaaja() == 2 && Grid.GetColumn(nappula) < pelikentta.GetPelikentanKoko() - 2)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) + 2, Grid.GetRow(nappula) + 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }

                    if (Grid.GetRow(nappula1) == Grid.GetRow(nappula) + 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) - 1 && nappula1.GetPelaaja() == 2 && Grid.GetColumn(nappula) > 1)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) - 2, Grid.GetRow(nappula) + 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }
                }

            }
            // Pelaajan 2 syöminen
            if (nappula.GetPelaaja() == 2 && !nappula.GetTammi() && Grid.GetRow(nappula) > 1)
            {
                foreach (Pelinappula nappula1 in lista)
                {
                    if (Grid.GetRow(nappula1) == Grid.GetRow(nappula) - 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) + 1 && nappula1.GetPelaaja() == 1 && Grid.GetColumn(nappula) < pelikentta.GetPelikentanKoko() - 2)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) + 2, Grid.GetRow(nappula) - 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }

                    if (Grid.GetRow(nappula1) == Grid.GetRow(nappula) - 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) - 1 && nappula1.GetPelaaja() == 1 && Grid.GetColumn(nappula) > 1)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) - 2, Grid.GetRow(nappula) - 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }
                }
            }
            if (nappula.GetTammi())
            {
                if (nappula.GetPelaaja() == 1) return OnkoSyotaviaTammi(nappula, 2);
                else return OnkoSyotaviaTammi(nappula, 1);
            }
            if (mahdollisetSiirrot != 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Tarkistetaan onko syötäviä nappuloita tammi-nappulalla kun on jo kerran syöty
        /// </summary>
        /// <param name="nappula">Liikutettava nappula</param>
        /// <param name="vastustaja">Vastustaja</param>
        /// <returns></returns>
        private bool OnkoSyotaviaTammi(Pelinappula nappula, int vastustaja)
        {
            int mahdollisetSiirrot = 0;
            // keskellä kaikki
            if (Grid.GetRow(nappula) > 1 && Grid.GetRow(nappula) < pelikentta.GetPelikentanKoko() - 2 && Grid.GetColumn(nappula) > 1 && Grid.GetColumn(nappula) < pelikentta.GetPelikentanKoko() - 2)
            {
                foreach (Pelinappula nappula1 in lista)
                {
                    if (nappula1.GetPelaaja() == vastustaja && Grid.GetRow(nappula1) == Grid.GetRow(nappula) + 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) + 1)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) + 2, Grid.GetRow(nappula) + 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }

                    if (nappula1.GetPelaaja() == vastustaja && Grid.GetRow(nappula1) == Grid.GetRow(nappula) + 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) - 1)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) - 2, Grid.GetRow(nappula) + 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }

                    if (nappula1.GetPelaaja() == vastustaja && Grid.GetRow(nappula1) == Grid.GetRow(nappula) - 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) + 1)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) + 2, Grid.GetRow(nappula) - 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }

                    if (nappula1.GetPelaaja() == vastustaja && Grid.GetRow(nappula1) == Grid.GetRow(nappula) - 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) - 1)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) - 2, Grid.GetRow(nappula) - 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }
                }
               
                
            }
            // ylhäälä vasen ja oikea
            if (Grid.GetRow(nappula) <= 1 && Grid.GetColumn(nappula) > 1 && Grid.GetColumn(nappula) < pelikentta.GetPelikentanKoko() - 2)
            {
                foreach (Pelinappula nappula1 in lista)
                {
                    if (nappula1.GetPelaaja() == vastustaja && Grid.GetRow(nappula1) == Grid.GetRow(nappula) + 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) + 1)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) + 2, Grid.GetRow(nappula) + 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }

                    if (nappula1.GetPelaaja() == vastustaja && Grid.GetRow(nappula1) == Grid.GetRow(nappula) + 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) - 1)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) - 2, Grid.GetRow(nappula) + 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }
                }
            }
            // alhaalla vasen ja oikea
            if (Grid.GetRow(nappula) >= pelikentta.GetPelikentanKoko() - 2 && Grid.GetColumn(nappula) > 1 && Grid.GetColumn(nappula) < pelikentta.GetPelikentanKoko() - 2)
            {
                foreach (Pelinappula nappula1 in lista)
                {
                    if (nappula1.GetPelaaja() == vastustaja && Grid.GetRow(nappula1) == Grid.GetRow(nappula) - 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) + 1)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) + 2, Grid.GetRow(nappula) - 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }

                    if (nappula1.GetPelaaja() == vastustaja && Grid.GetRow(nappula1) == Grid.GetRow(nappula) - 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) - 1)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) - 2, Grid.GetRow(nappula) - 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }
                }
            }
            // vasemmalla ylös oikea ja alas oikea
            if (Grid.GetColumn(nappula) <= 1 && Grid.GetRow(nappula) > 1 && Grid.GetRow(nappula) < pelikentta.GetPelikentanKoko() - 2)
            {
                foreach (Pelinappula nappula1 in lista)
                {
                    if (nappula1.GetPelaaja() == vastustaja && Grid.GetRow(nappula1) == Grid.GetRow(nappula) - 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) + 1)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) + 2, Grid.GetRow(nappula) - 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }

                    if (nappula1.GetPelaaja() == vastustaja && Grid.GetRow(nappula1) == Grid.GetRow(nappula) + 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) + 1)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) + 2, Grid.GetRow(nappula) + 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }
                }
            }
            // oikealla ylös vase ja alas vasen
            if (Grid.GetColumn(nappula) >= pelikentta.GetPelikentanKoko() - 2 && Grid.GetRow(nappula) > 1 && Grid.GetRow(nappula) < pelikentta.GetPelikentanKoko() - 2)
            {
                foreach (Pelinappula nappula1 in lista)
                {
                    if (nappula1.GetPelaaja() == vastustaja && Grid.GetRow(nappula1) == Grid.GetRow(nappula) - 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) - 1)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) - 2, Grid.GetRow(nappula) - 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }

                    if (nappula1.GetPelaaja() == vastustaja && Grid.GetRow(nappula1) == Grid.GetRow(nappula) + 1 && Grid.GetColumn(nappula1) == Grid.GetColumn(nappula) - 1)
                    {
                        if (TarkistaOnkoRuutuTyhjaAutomaatti(Grid.GetColumn(nappula) - 2, Grid.GetRow(nappula) + 2, false))
                        {
                            mahdollisetSiirrot++;
                        }
                    }
                }
            }

            if (mahdollisetSiirrot > 0) return true;

            return false;
        }

        /// <summary>
        /// Siirretään valittua nappulaan ruutuun jota klikataan ja syödään nappula
        /// tarvittaessa. (Pelilogiikka)
        /// </summary>
        /// <param name="nappula"></param>
        private void TarkistaLiikutusBreakthrough(Pelinappula nappula)
        {

            if (nappula.OnkoValittu())
            {
                // otetaan int-muuttujiin tiedot nappulan sijainnista ja klikkauksen sijainnista
                int rowKlikkaus = pelikentta.GetRow();
                int columnKlikkaus = pelikentta.GetColumn();
                int columnNappula = Grid.GetColumn(nappula);
                int rowNappula = Grid.GetRow(nappula);

                // Tarkistetaan kumman puolen nappulaa ollaan siirtämässä. TODO: REFAKTOROI samat koodit!!!!
                if (nappula.GetPelaaja() == 1)
                {
                    // Katsotaan täyttyvätkö siirtoehdot
                    if ((rowKlikkaus - rowNappula) == 1 && ((columnKlikkaus - columnNappula) == 1 || (columnKlikkaus - columnNappula) == 0 || (columnKlikkaus - columnNappula) == -1))
                    {
                        // Käydään läpi nappulat, että tiedetään syökö nappula toisen nappulan tai onko ruudussa jo oma nappula.
                        foreach (Pelinappula nappula1 in lista)
                        {
                            // Tarkistetaan onko ruudussa johon liikutaan vastustajan nappula
                            if (Grid.GetRow(nappula1) == rowKlikkaus && Grid.GetColumn(nappula1) == columnKlikkaus && nappula1.GetPelaaja() == 2)
                            {
                                if (columnKlikkaus - columnNappula == 0)
                                {
                                    pelikentta.VarjaaPunaiseksi(columnKlikkaus, rowKlikkaus, ruudukko1VariMuisti, ruudukko2VariMuisti);
                                    return;

                                }// Ei voida syödä jos nappula kohtisuorassa.
                                pelikentta.myGrid.Children.Remove(nappula1);
                                // Ainakin väliaikainen ratkaisu nappulan poistoon gridiltä.
                                Grid.SetColumn(nappula1, 200);
                                Grid.SetRow(nappula1, 200);
                                nappula1.Syoty(true);
                                soiko = true;
                            }

                            // Lopetetaan jos siirryttävässä ruudussa on jo oma nappula.
                            if ((Grid.GetRow(nappula1) == rowKlikkaus) && (Grid.GetColumn(nappula1) == columnKlikkaus) && (nappula1.GetPelaaja() == 1))
                            {
                                pelikentta.VarjaaPunaiseksi(columnKlikkaus, rowKlikkaus, ruudukko1VariMuisti, ruudukko2VariMuisti);
                                return;

                            }
                        }
                        KirjoitaMuistiin(columnKlikkaus, rowKlikkaus, nappula);
                        // Jos päästään tänne, voidaan siirtää valittu nappula klikattavaan kohtaan.
                        Grid.SetColumn(nappula, columnKlikkaus);
                        Grid.SetRow(nappula, rowKlikkaus);
                        varjataanko = false;
                        // Jos klikkaus oli vastustajan päädyssä voittaa pelin
                        if (rowKlikkaus == pelikentta.GetPelikentanKoko() - 1)
                        {
                            nappula.Voitti();
                            LopetaPeli();
                            IlmoitaVoittaja(pelaaja1 + " voitti!");
                            return;
                        }
                        pelikentta.VaihdaVuoro();
                        PoistaValinta();
                    }
                    if (rowKlikkaus != rowNappula || columnKlikkaus != columnNappula)
                    {
                        if (varjataanko == true)
                        {
                            pelikentta.VarjaaPunaiseksi(columnKlikkaus, rowKlikkaus, ruudukko1VariMuisti, ruudukko2VariMuisti);
                            varjataanko = true;
                        }
                    }
                }

                // Tarkistetaan kumman puolen nappulaa ollaan siirtämässä. TODO: REFAKTOROI samat koodit!!!!
                if (nappula.GetPelaaja() == 2)
                {

                    // Tarkistetaan täyttyvätkö siirtoehdot, ei voi liikkua ihan minne tahansa
                    if ((rowKlikkaus - rowNappula) == -1 && ((columnKlikkaus - columnNappula) == 0 || (columnKlikkaus - columnNappula) == -1 || (columnKlikkaus - columnNappula) == 1))
                    {
                        // Käydään läpi nappulat, että tiedetään syökö nappula toisen nappulan tai onko ruudussa jo oma nappula.
                        foreach (Pelinappula nappula1 in lista)
                        {
                            // Tarkistetaan onko ruudussa johon liikutaan vastustajan nappula
                            if (Grid.GetRow(nappula1) == rowKlikkaus && Grid.GetColumn(nappula1) == columnKlikkaus && nappula1.GetPelaaja() == 1)
                            {
                                if (columnKlikkaus - columnNappula == 0)
                                {
                                    pelikentta.VarjaaPunaiseksi(columnKlikkaus, rowKlikkaus, ruudukko1VariMuisti, ruudukko2VariMuisti);
                                    return;
                                }
                                pelikentta.myGrid.Children.Remove(nappula1);
                                // Ainakin väliaikainen ratkaisu nappulan poistoon gridiltä.
                                Grid.SetColumn(nappula1, 200);
                                Grid.SetRow(nappula1, 200);
                                nappula1.Syoty(true);
                                soiko = true;
                            }

                            // Lopetetaan jos siirryttävässä ruudussa on jo oma nappula.
                            if ((Grid.GetRow(nappula1) == rowKlikkaus) && (Grid.GetColumn(nappula1) == columnKlikkaus) && (nappula1.GetPelaaja() == 2) && (nappula1.OnkoSyoty() == false))
                            {
                                pelikentta.VarjaaPunaiseksi(columnKlikkaus, rowKlikkaus, ruudukko1VariMuisti, ruudukko2VariMuisti);
                                return;
                            }
                        }

                        // Jos päästään tänne, voidaan siirtää valittu nappula klikattavaan kohtaan.
                        KirjoitaMuistiin(columnKlikkaus, rowKlikkaus, nappula);
                        Grid.SetColumn(nappula, columnKlikkaus);
                        Grid.SetRow(nappula, rowKlikkaus);
                        varjataanko = false;
                        if (rowKlikkaus == 0)
                        {
                            nappula.Voitti();
                            LopetaPeli();
                            IlmoitaVoittaja(pelaaja2 + " voitti!");
                            return;
                        }
                        pelikentta.VaihdaVuoro();
                        PoistaValinta();
                    }
                    if (rowKlikkaus != rowNappula || columnKlikkaus != columnNappula)
                    {
                        if (varjataanko == true)
                        {
                            pelikentta.VarjaaPunaiseksi(columnKlikkaus, rowKlikkaus, ruudukko1VariMuisti, ruudukko2VariMuisti);
                            varjataanko = true;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Laitetaan siirrot muistiin StringBuilderiin.
        /// </summary>
        /// <param name="columnKlikkaus">Klikattu column</param>
        /// <param name="rowKlikkaus">Klikattu row</param>
        /// <param name="nappula">nappula jota liikutettiin</param>
        private void KirjoitaMuistiin(int columnKlikkaus, int rowKlikkaus, Pelinappula nappula)
        {
            if (nappula.GetPelaaja() == 1)
            {
                muisti.Append(pelaaja1 + "(1)" + ": " + "Ruudusta " + "(" + (Grid.GetColumn(nappula) + 1) + ", " + (Grid.GetRow(nappula) + 1) + ") " + " ruutuun " + "(" + (columnKlikkaus + 1) + ", " + (rowKlikkaus + 1) + ") ");
                if (soiko == true) muisti.Append("Söi vastustajan nappulan");
                muisti.Append("\n");
                //    soiko = false;
            }
            else
            {
                muisti.Append(pelaaja2 + "(2)" + ": " + "Ruudusta " + "(" + (Grid.GetColumn(nappula) + 1) + ", " + (Grid.GetRow(nappula) + 1) + ") " + " ruutuun " + "(" + (columnKlikkaus + 1) + ", " + (rowKlikkaus + 1) + ") ");
                if (soiko == true) muisti.Append("Söi vastustajan nappulan");
                muisti.Append("\n");
                //      soiko = false;
            }

            tallennus.IsEnabled = true;
            tulostus.IsEnabled = true;
            luovutus.IsEnabled = true;
        }



        /// <summary>
        /// Ilmoitetaan voittaja pistämällä Textblock ruudulle.
        /// </summary>
        /// <param name="voittaja1"></param>
        private void IlmoitaVoittaja(string voittaja1)
        {
            voittaja = new TextBlock();
            voittaja.Text = voittaja1;
            voittaja.FontSize = 50;
            voittaja.HorizontalAlignment = HorizontalAlignment.Center;
            voittaja.VerticalAlignment = VerticalAlignment.Center;
            voittaja.TextWrapping = TextWrapping.Wrap;
            Grid.SetRow(voittaja, 1);
            peliGrid.Children.Add(voittaja);
            SoitaAani();
            muisti.Append(voittaja1);
            tallennus.IsEnabled = true;
            tulostus.IsEnabled = true;
            luovutus.IsEnabled = false;
        }


        /// <summary>
        /// Pelin lopetus.
        /// </summary>
        private void LopetaPeli()
        {
            foreach (Pelinappula nappula in lista)
            {
                nappula.SetVuoro(false);
            }

        }

        /// <summary>
        /// Poistetaan valinta nappuloilta.
        /// </summary>
        private void PoistaValinta()
        {
            foreach (Pelinappula nappula in lista)
            {
                nappula.Valittu(false);
            }
        }

        /// <summary>
        /// Vaihdetaan vuoroa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VuoroChanged(object sender, RoutedEventArgs e)
        {
            foreach (Pelinappula nappula in lista)
            {
                if (pelikentta.GetVuoro() == nappula.GetPelaaja())
                {
                    nappula.SetVuoro(true);
                }
                else
                {
                    nappula.SetVuoro(false);
                }
            }
        }

        /// <summary>
        /// Avaa säännöt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_ClickSaannot(object sender, RoutedEventArgs e)
        {
            if (saannot == null)
            {
                saannot = new Saannot();
                saannot.Show();
                saannot.Closed += saannot_Closed;
            }
            saannot.Activate();
        }


        /// <summary>
        /// Laitetaan säännöt-ikkuna nulliksi kun suljetaan
        /// jotta ei voi avata montaa ikkunaa saman aikaan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saannot_Closed(object sender, EventArgs e)
        {
            saannot = null;
        }


        /// <summary>
        /// Suljetaan ikkuna ohjelma.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_Sulje(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// About-dialogi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_ClickAbout(object sender, RoutedEventArgs e)
        {
            if (about == null)
            {
                about = new About();
                about.Show();
                about.Closed += about_Closed;
            }
            about.Activate();
        }


        /// <summary>
        /// Pistetään about-ikkuna nulliksi kun suljetaan jotta
        /// ei voi käynnistää montaa ikkunaa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void about_Closed(object sender, EventArgs e)
        {
            about = null;
        }


        /// <summary>
        /// Aloittaa peli alusta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (pelimuoto.Equals("Breakthrough"))
            {

                UusiNappulatBreakthrough();
                tallennus.IsEnabled = false;
                tulostus.IsEnabled = false;

            }
            else
            {
                //  pelikentta.TeeUusiKentta(ruudukonKoko, ruudukko1VariMuisti, ruudukko2VariMuisti);
                UusiNappulatTammi();

                tallennus.IsEnabled = false;
                tulostus.IsEnabled = false;
            }
            muisti.Clear();
        }


        /// <summary>
        /// Alustetaan tammen nappulat
        /// </summary>
        private void UusiNappulatTammi()
        {
            foreach (Pelinappula nappula in lista)
            {
                pelikentta.myGrid.Children.Remove(nappula);
            }
            lista.Clear();
            muisti.Clear();
            soiko = false;
            peliGrid.Children.Remove(voittaja);
            AlustaTammi();
        }


        /// <summary>
        /// Uusitaan breakthrough:n nappulat.
        /// </summary>
        private void UusiNappulatBreakthrough()
        {
            foreach (Pelinappula nappula in lista)
            {
                pelikentta.myGrid.Children.Remove(nappula);
            }
            lista.Clear();
            muisti.Clear();
            soiko = false;
            peliGrid.Children.Remove(voittaja);
            AlustaBreakthrough();
        }


        /// <summary>
        /// Tallennetaan liikutukset tiedostoon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TallennaTiedostoon(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Tallennus";
            dlg.DefaultExt = ".text"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
            string pelaajat = "Pelaaja 1: " + pelaaja1 + "\n" + "Pelaaja 2: " + pelaaja2 + "\n\n";
            // Show save file dialog box
            bool result = (bool)dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                File.WriteAllText(dlg.FileName, pelaajat);
                File.AppendAllText(dlg.FileName, muisti.ToString());

            }

        }


        /// <summary>
        /// Avataan asetusdialogin ja asettaa tietoja sinne.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AvaaAsetukset(object sender, RoutedEventArgs e)
        {
            if (asetukset == null)
            {
                asetukset = new Asetukset();

                asetukset.SetRuudukonKokoOletus(pelikentta.GetPelikentanKoko() - 6);
                asetukset.SetPelimuotoOletus(pelimuoto);
                asetukset.Closed += asetukset_Closed;
            }
            asetukset.Activate();
            asetukset.AsetuksetChanged += asetukset_AsetuksetChanged;

            if (pelimuoto.Equals("Tammi"))
            {
                asetukset.tammi.IsChecked = true;
            }
            else asetukset.breakthrough.IsChecked = true;

            if (pelaaja1VariMuisti != null)
            {
                asetukset.pelaaja1Vari2.Fill = pelaaja1VariMuisti;
                asetukset.SetPelaaja1Vari(pelaaja1VariMuisti);
            }
            else asetukset.pelaaja1Vari2.Fill = Brushes.LightGreen;

            if (pelaaja2VariMuisti != null)
            {
                asetukset.pelaaja2Vari2.Fill = pelaaja2VariMuisti;
                asetukset.SetPelaaja2Vari(pelaaja2VariMuisti);
            }
            else asetukset.pelaaja2Vari2.Fill = Brushes.Red;

            if (ruudukko1VariMuisti != null)
            {
                asetukset.ruudukko1Vari2.Fill = ruudukko1VariMuisti;
            }
            else asetukset.ruudukko1Vari2.Fill = Brushes.Blue;

            if (ruudukko2VariMuisti != null)
            {
                asetukset.ruudukko2Vari2.Fill = ruudukko2VariMuisti;
            }
            else asetukset.ruudukko2Vari2.Fill = Brushes.Cyan;

            asetukset.ShowDialog();
        }


        /// <summary>
        /// Asetetaan asetukset nulliksi, jotta ei voida avata monaa asetus-ikkunaa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void asetukset_Closed(object sender, EventArgs e)
        {
            asetukset = null;
        }


        /// <summary>
        /// Muokataan pelikenttää sen mukaan mitä asetuksia muutettu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void asetukset_AsetuksetChanged(object sender, RoutedEventArgs e)
        {
            SetRuudukonKoko(asetukset.GetRuudukonKoko());
            if (asetukset.GetRuudukko1Changed())
            {
                pelikentta.VaihdaRuutujenVari(asetukset.GetVariRuudukko1());
                ruudukko1VariMuisti = asetukset.GetVariRuudukko1();
            }

            if (asetukset.GetRuudukko2Changed())
            {
                pelikentta.VaihdaRuutujenVari2(asetukset.GetVariRuudukko2());
                ruudukko2VariMuisti = asetukset.GetVariRuudukko2();
            }

            if (asetukset.GetRuudukonKokoChanged())
            {
                pelikentta.TeeUusiKentta(asetukset.GetRuudukonKoko(), ruudukko1VariMuisti, ruudukko2VariMuisti);
                if (pelimuoto.Equals("Breakthrough"))
                {
                    UusiNappulatBreakthrough();

                }
                else UusiNappulatTammi();
                tallennus.IsEnabled = false;
                tulostus.IsEnabled = false;
                asetukset.SetRuudukonKokoChanged(false);
                muisti.Clear();
            }

            if (asetukset.GetPelimuotoChanged())
            {
                pelimuoto = asetukset.GetValittuPelimuoto();
                if (pelimuoto.Equals("Tammi")) UusiNappulatTammi();
                else UusiNappulatBreakthrough();
                asetukset.SetPelimuotoOletus(pelimuoto);
                asetukset.PelimuotoChanged(false);
                tallennus.IsEnabled = false;
                tulostus.IsEnabled = false;
                muisti.Clear();
            }

            foreach (Pelinappula nappula in lista)
            {
                if (nappula.GetPelaaja() == 1)
                {
                    nappula.SetVari1(asetukset.GetVariPelaaja1());
                    pelaaja1VariMuisti = asetukset.GetVariPelaaja1();
                }
            }

            foreach (Pelinappula nappula in lista)
            {
                if (nappula.GetPelaaja() == 2)
                {
                    nappula.SetVari2(asetukset.GetVariPelaaja2());
                    pelaaja2VariMuisti = asetukset.GetVariPelaaja2();
                }
            }

        }


        /// <summary>
        /// Avataan avustus-ikkuna.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_Avustus(object sender, RoutedEventArgs e)
        {
            Avustus avustus = new Avustus();
            avustus.ShowDialog();

        }


        /// <summary>
        /// Access-keyt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.A)) MenuItem_Click_Avustus(null, null);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.U)) MenuItem_Click(null, null);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.S)) TallennaTiedostoon(null, null);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.O)) AvaaAsetukset(null, null);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.Q)) MenuItem_Click_Sulje(null, null);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.R)) MenuItem_ClickSaannot(null, null);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.T)) MenuItem_ClickAbout(null, null);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.L) && Keyboard.IsKeyDown(Key.F1)) luovutus1_Click(null, null);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.L) && Keyboard.IsKeyDown(Key.F2)) luovutus2_Click(null, null);

        }


        /// <summary>
        /// Tulostetaan pelin lopputulos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TulostaLopputulos(object sender, RoutedEventArgs e)
        {
            string lopputulos = GetRuudukko();
            string siirrot1 = muisti.ToString();
            string[] lines = siirrot1.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            int rivit = lines.Length;
            double sivujenMaara = rivit / 55.0;
            int t = 0;

            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() != true) return;

            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize = new Size(dialog.PrintableAreaWidth, dialog.PrintableAreaHeight);


            FixedPage page1 = new FixedPage();
            page1.Width = document.DocumentPaginator.PageSize.Width;
            page1.Height = document.DocumentPaginator.PageSize.Height;

            // Pistetään tekstit dockpanelin sisään
            DockPanel panel = new DockPanel();
            panel.Width = page1.Width;
            panel.Height = page1.Height;
            panel.LastChildFill = true;
            page1.Children.Add(panel);


            TextBlock pelaajat = new TextBlock();
            pelaajat.Margin = new Thickness(0, 20, 0, 0);
            pelaajat.Text = "Pelaajat:\n" + "Pelaaja 1" + ": " + pelaaja1 + "\n" + "Pelaaja 2" + ": " + pelaaja2;
            pelaajat.FontSize = 30;
            pelaajat.HorizontalAlignment = HorizontalAlignment.Center;
            pelaajat.TextWrapping = TextWrapping.Wrap;
            panel.Children.Add(pelaajat);
            DockPanel.SetDock(pelaajat, Dock.Top);


            TextBlock otsikko = new TextBlock();
            otsikko.Text = "Kuva pelilaudasta\n";
            otsikko.FontSize = 30;
            otsikko.HorizontalAlignment = HorizontalAlignment.Center;
            otsikko.TextWrapping = TextWrapping.Wrap;
            panel.Children.Add(otsikko);
            DockPanel.SetDock(otsikko, Dock.Top);

            TextBlock info = new TextBlock();
            info.Text = "Siirrot seuraavalla sivulla";
            info.Margin = new Thickness(0, 0, 0, 20);
            info.FontSize = 30;
            info.HorizontalAlignment = HorizontalAlignment.Center;
            info.TextWrapping = TextWrapping.Wrap;
            panel.Children.Add(info);
            DockPanel.SetDock(info, Dock.Bottom);

            TextBlock ruudukkoText = new TextBlock();
            ruudukkoText.Text = lopputulos;

            ruudukkoText.FontFamily = new FontFamily("Courier New");
            ruudukkoText.FontSize = 40;
            ruudukkoText.HorizontalAlignment = HorizontalAlignment.Center;
            ruudukkoText.VerticalAlignment = VerticalAlignment.Center;
            ruudukkoText.TextWrapping = TextWrapping.Wrap;
            panel.Children.Add(ruudukkoText);
            DockPanel.SetDock(ruudukkoText, Dock.Top);

            PageContent page1Content = new PageContent();
            ((IAddChild)page1Content).AddChild(page1);
            document.Pages.Add(page1Content);


            for (int i = 0; i < sivujenMaara; i++)
            {
                StringBuilder tekstiB = new StringBuilder();
                FixedPage page2 = new FixedPage();
                page2.Width = document.DocumentPaginator.PageSize.Width;
                page2.Height = document.DocumentPaginator.PageSize.Height;
                DockPanel panel2 = new DockPanel();
                panel2.Width = page2.Width;
                panel2.Height = page2.Height;
                page2.Children.Add(panel2);

                TextBlock siirrotOtsikko = new TextBlock();
                siirrotOtsikko.Margin = new Thickness(10);
                siirrotOtsikko.FontSize = 30;
                siirrotOtsikko.HorizontalAlignment = HorizontalAlignment.Center;
                siirrotOtsikko.TextWrapping = TextWrapping.Wrap;
                panel2.Children.Add(siirrotOtsikko);
                DockPanel.SetDock(siirrotOtsikko, Dock.Top);
                siirrotOtsikko.Text = "Siirrot";


                TextBlock siirrot = new TextBlock();
                siirrot.HorizontalAlignment = HorizontalAlignment.Center;
                siirrot.TextWrapping = TextWrapping.Wrap;
                panel2.Children.Add(siirrot);
                DockPanel.SetDock(siirrot, Dock.Top);
                for (int p = 0; p < 55; p++)
                {
                    tekstiB.Append(lines[t] + "\n");
                    t++;
                    if (t >= lines.Length) break;
                }
                siirrot.Text = tekstiB.ToString();


                PageContent page2Content = new PageContent();
                ((IAddChild)page2Content).AddChild(page2);
                document.Pages.Add(page2Content);
            }

            dialog.PrintDocument(document.DocumentPaginator, "Fixed document");
            t = 0;
        }


        /// <summary>
        /// Muodostetaan pelin lopputulos kirjaimin ja numeroin esitettynä.
        /// </summary>
        /// <returns>Ruudukon</returns>
        private string GetRuudukko()
        {
            Boolean onko = false;
            StringBuilder moi = new StringBuilder();
            for (int r = 0; r < pelikentta.GetPelikentanKoko(); r++)
            {
                for (int c = 0; c < pelikentta.GetPelikentanKoko(); c++)
                {
                    foreach (Pelinappula nappula in lista)
                    {
                        if (Grid.GetColumn(nappula) == c && Grid.GetRow(nappula) == r)
                        {
                            if (nappula.GetPelaaja() == 1)
                            {
                                moi.Append("1 ");
                                onko = true;
                            }
                            else
                            {
                                moi.Append("2 ");
                                onko = true;
                            }
                        }

                    }
                    if (onko == false)
                    {
                        moi.Append("X ");
                    }
                    else
                    {
                        onko = false;
                    }

                }
                moi.Append("\n");
            }
            string tulos = moi.ToString();
            return tulos;
        }


        /// <summary>
        /// Pelaajan 2 luovutus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void luovutus2_Click(object sender, RoutedEventArgs e)
        {
            IlmoitaVoittaja(pelaaja1 + " voitti!");
        }


        /// <summary>
        /// Pelaajan 1 luovutus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void luovutus1_Click(object sender, RoutedEventArgs e)
        {
            IlmoitaVoittaja(pelaaja2 + " voitti!");
        }


    }
}