using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ajastinkomponentti
{
    public partial class Ajastinkomponentti: Timer
    {
        TimeSpan AikaAlussa;
        public delegate void Sekunti();
        public delegate void Halytys();
        public event Sekunti Sekunti1;      
        public event Halytys AikaLoppu;
        
        
        
        String pAika;
        [Category("Keittiöajastin"),
        Description("Ajastin"),
        DefaultValue(0),
        Browsable(true)]
        public String Aika
        {
            get { return pAika; }
            set { pAika = value; }
        }

        String jAika;
        public String Jaika
        {
            get { return jAika; }
            set { jAika = value; }
        }
        
        public Ajastinkomponentti()
        {
            InitializeComponent();
            this.Interval = 1000;
        }

        // Aloittaa ajastuksen
        public void StartAjastin()
        {
            this.Start();
            if (jAika != "" && jAika != Aika && !jAika.Equals("00:00:00"))
            {
                AikaAlussa = TimeSpan.Parse(jAika);
            }
            else
            {
                jAika = Aika;
                AikaAlussa = TimeSpan.Parse(Aika);
            }
        }

        public void StopAjastin()
        {
            this.Stop();
        }

        // Tick-event
        private void Ajastinkomponentti_Tick(object sender, EventArgs e)
        {
            AikaAlussa = AikaAlussa.Subtract(new TimeSpan(0, 0, 1));
            jAika = AikaAlussa.ToString();
            Sekunti1();

            if (jAika.ToString().Equals("00:00:00"))
            {
                this.Stop();
                AikaLoppu();
            }
        }
    }
}
