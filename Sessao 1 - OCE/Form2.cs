using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sessao_1___OCE
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();


        }
        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            timer1.Interval = 12000;
            progressBar1.Maximum = 10;
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value != 10)
            {
                progressBar1.Value++;
                label1.Text = "Carregando componentes...";
                if(timer1.Interval == 25)
                {
                    label1.Text = "Carregando dados...";
                    progressBar1.Value++;
                }
                if (timer1.Interval == 50)
                {
                    label1.Text = "Carregando Imagens...";
                    progressBar1.Value++;
                }
                if (timer1.Interval == 75)
                {
                    label1.Text = "Carregando dashboard...";
                    progressBar1.Value++;
                }
            }
            else
            {
                timer1.Stop();
                new Form3().Show();
                this.Hide();
                  
            }
        }
    }
}
