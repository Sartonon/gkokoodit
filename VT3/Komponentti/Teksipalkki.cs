using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Komponentti
{
    public partial class Teksipalkki: UserControl
    {
        String teksti;
        String[] osat;
        int pituus;
        Font kirjasin;
        int x = 0;
        [Category("Vaihtuva tekstikontrolli"),
        Description("lol"),
        DefaultValue(0),
        Browsable(true)]
        public String VaihtuvaTeksi
        {
            get { return teksti; }
            set 
            {
                teksti = value;
                osat = teksti.Split();
                pituus = osat.Length;
            }
        }

        public Teksipalkki()
        {
            InitializeComponent(); 
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (x < osat.Length - 1)
            {
                x++;
            }
            else x = 0;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            kirjasin = new Font("Verdana", 40);
            Graphics canvas = e.Graphics;
            
            if (osat[x] != null)
            {
                canvas.DrawString(osat[x], kirjasin, System.Drawing.Brushes.AntiqueWhite, new Point(0, 0));
            }

            //base.OnPaint(e);
        }

    }
}
