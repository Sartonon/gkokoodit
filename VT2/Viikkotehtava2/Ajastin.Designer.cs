namespace Viikkotehtava2
{
    partial class Ajastin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelAika = new System.Windows.Forms.Label();
            this.aikakomponentti1 = new Aikakomponentti.Aikakomponentti();
            this.ajastinkomponentti1 = new Ajastinkomponentti.Ajastinkomponentti();
            this.SuspendLayout();
            // 
            // buttonReset
            // 
            this.buttonReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReset.Location = new System.Drawing.Point(79, 189);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(114, 60);
            this.buttonReset.TabIndex = 1;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.Location = new System.Drawing.Point(79, 88);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(114, 51);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // labelAika
            // 
            this.labelAika.AutoSize = true;
            this.labelAika.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAika.Location = new System.Drawing.Point(73, 39);
            this.labelAika.Name = "labelAika";
            this.labelAika.Size = new System.Drawing.Size(0, 31);
            this.labelAika.TabIndex = 3;
            this.labelAika.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // aikakomponentti1
            // 
            this.aikakomponentti1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aikakomponentti1.Location = new System.Drawing.Point(79, 145);
            this.aikakomponentti1.MaxLength = 8;
            this.aikakomponentti1.Name = "aikakomponentti1";
            this.aikakomponentti1.Size = new System.Drawing.Size(114, 38);
            this.aikakomponentti1.TabIndex = 0;
            this.aikakomponentti1.Text = "00:00:00";
            this.aikakomponentti1.Virhe += new Aikakomponentti.Aikakomponentti.Tarkistus(this.aikakomponentti1_Virhe);
            this.aikakomponentti1.Ok += new Aikakomponentti.Aikakomponentti.Tarkistus(this.aikakomponentti1_Ok);
            // 
            // ajastinkomponentti1
            // 
            this.ajastinkomponentti1.Aika = "";
            this.ajastinkomponentti1.Interval = 1000;
            this.ajastinkomponentti1.Jaika = "";
            this.ajastinkomponentti1.Sekunti1 += new Ajastinkomponentti.Ajastinkomponentti.Sekunti(this.ajastinkomponentti1_Sekunti1);
            this.ajastinkomponentti1.AikaLoppu += new Ajastinkomponentti.Ajastinkomponentti.Halytys(this.ajastinkomponentti1_AikaLoppu);
            // 
            // Ajastin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.labelAika);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.aikakomponentti1);
            this.Name = "Ajastin";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ajastin_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Aikakomponentti.Aikakomponentti aikakomponentti1;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelAika;
        private Ajastinkomponentti.Ajastinkomponentti ajastinkomponentti1;



    }
}

