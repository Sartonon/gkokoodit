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
    /// Interaction logic for Saannot.xaml
    /// </summary>
    public partial class Saannot : Window
    {
        public Saannot()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SaannotBlock.Text = "Aloitustilanteessa kaksi alinta riviä on täytetty valkeilla nappuloilla ja kaksi ylintä riviä mustilla. Toinen pelaajista pelaa valkeilla, toinen mustilla nappuloilla. Toinen pelaajista aloittaa siirtämällä jotakin nappuloistaan. Sen jälkeen pelaajat siirtävät omia nappuloitaan vuorotellen. \n\nValkeita napuloita on mahdollistaa siirtää ainoastaan ylöspäin ja mustia vain alaspäin. Vuoron aikana voidaan siirtää vain yhtä nappulaa, ja sitä voi siirtää eteenpäin yhden ruudun joko suoraan tai vinottain, jos kohderuudussa ei ennestään ole nappulaa. Siirto kuitenkin on mahdollinen myös, jos se tapahtuu vinottain ja kohderuudussa on vastustajan nappula. Tällöin kyseinen vastustajan nappula poistetaan pelilaudalta ja siirtyvä nappula asetetaan ruutuun sen tilalle. \n\nEnsimmäinen pelaaja, jonka nappula saavuttaa pelaajasta katsoen kauimmaisen rivin tai jonka vastustajan kaikki nappulat joutuvat poistetuiksi laudalta, voittaa pelin";
            SaannotBlock2.Text = "Pelissä käytetään tavallista 8x8 ruudun šakkilautaa ja kahta eriväristä sarjaa nappuloita, virallisesti väreiltään punaiset ja valkoiset, 12 kummallakin pelaajalla.\n\nPelaajat valitsevat arpomalla kumpi on punainen ja kumpi valkoinen. Pelaajat asettavat nappulansa oman päätynsä kolmen ensimmäisen vaakarivin mustille ruuduille. Koko pelin aikana käytetään ainoastaan mustia ruutuja.\n\nPelin aloittaa pelaaja, jolla on punaiset nappulat. Nappuloita siirretään vuorotellen askelen verran mustia ruutuja pitkin etuviistoon. Jos oman nappulan edessä on vihollisnappula, jonka takana on vapaa ruutu, sen voi syödä hyppäämällä sen yli eli siirtämällä oman nappulansa syödyn nappulan taakse ja poistamalla syöty nappula laudalta. Jos pelaajalla on mahdollisuus syödä jokin vastustajan nappuloista, hänen on tehtävä niin.\n\nJos syömiseen on useita vaihtoehtoja, pelaaja saa valita, minkä syö. Samalla pelivuorolla voi syödä useampiakin vihollisnappuloita, jos samalla nappulalla pystyy tekemään useamman loikan vihollisnappuloiden yli. Tässäkin pätee pakkosyönti, eli jos pelaajalla on mahdollisuus jatkaa vuoroaan syömällä samalla nappulalla useampi vastustajan nappula peräjälkeen, hänen on tehtävä niin.\n\nMikäli nappula pääsee laudan viimeiselle riville, siitä tulee kuningas tai tammi, joka pystyy tavallisista nappuloista poiketen liikkumaan laudalla myös takaviistoon. Kuningasnappula merkataan laittamalla sen päälle toinen pelinappula. Jos pelaaja saavuttaa laudan päädyn syötyään vastustajan nappulan, hän ei saa jatkaa syömistä enää samalla vuorolla, vaikka siihen tarjoutuisikin mahdollisuus.\n\nPelin tarkoituksena on syödä kaikki vihollisnappulat tai saada vihollinen tilanteeseen, jossa hän ei voi liikuttaa ainoatakaan nappulaansa. Kumpi tahansa pelaaja voi omalla vuorollaan luovuttaa pelin tai ehdottaa tasapeliä, joka julistetaan kummankin pelaajan siihen suostuessa.";
        }

        private void SaannotIkkuna_Closed(object sender, EventArgs e)
        {
            
        }
    }
}
