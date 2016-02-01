using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vt3
{
    public partial class Form1 : Form
    {

        LinearGradientBrush lgb;
        LinearGradientBrush lgb2;
        Image pollo;
        Graphics dc;
        int xKoordinaatti;
        Boolean suunta;
        Komponentti.Teksipalkki palkki;

        /// <summary>
        /// Piirretään pöllö ja tekstipalkki
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            pollo = Image.FromFile("owl.png");
            timer1.Start();
            xKoordinaatti = 0;

            palkki = new Komponentti.Teksipalkki();
            palkki.Left = 0;
            //palkki.AutoSize = true;
            palkki.Height = 70;
            palkki.Width = ClientSize.Width;
            palkki.Top = ClientSize.Height - 70;
            Controls.Add(palkki);
            palkki.VaihtuvaTeksi = "Tämä on Graafisten käyttöliittymien ohjelmointi";

        }

        /// <summary>
        /// Piirtää taustan ja päivittää pöllön sijaintia
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height / 2);
            lgb = new LinearGradientBrush(rect, Color.Purple, Color.White, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(lgb, rect);

            Rectangle rect2 = new Rectangle(0, ClientSize.Height / 2 + 1, ClientSize.Width, ClientSize.Height / 2);
            lgb2 = new LinearGradientBrush(rect, Color.White, Color.Purple, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(lgb2, rect2);

            dc = e.Graphics;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            dc.DrawImage(pollo, ClientSize.Width / 2 - pollo.Width / 2 + xKoordinaatti, 0, pollo.Width, pollo.Height);
            //base.OnPaint(e);
        }

        /// <summary>
        /// Liikutetaan tekstipalkkia formin koon mukaan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            palkki.Width = ClientSize.Width;
            palkki.Top = this.ClientSize.Height - 70;
            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (suunta == false)
            {
                xKoordinaatti--;
                if (xKoordinaatti < -150) suunta = true;
            }
            if (suunta == true)
            {
                xKoordinaatti++;
                if (xKoordinaatti > 150) suunta = false;
            }
            Invalidate();
        }
    }
}
