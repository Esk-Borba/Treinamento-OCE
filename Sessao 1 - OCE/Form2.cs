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
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(progressBar1.Value < 100)
            {        
                if(timer1.Interval > 0)
                {
                    label1.Text = "Carregando Componentes...";
                    progressBar1.Value = progressBar1.Value + 25;
                    if (timer1.Interval <= 3000)
                    {
                        label1.Text = "Carregando Dados...";
                        progressBar1.Value =progressBar1.Value + 25;
                        if(timer1.Interval <= 6000)
                        {
                            label1.Text = "Carregando Imagens...";
                            progressBar1.Value += 0;
                            if(timer1.Interval <= 9000)
                            {
                                label1.Text = "Iniciando Dashboard";
                                progressBar1.Value = progressBar1.Value + 25;
                                if (timer1.Interval <= 12000)
                                { 
                                    progressBar1.Value += 25;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                timer1.Stop();
                timer1.Enabled = false;
            }
        }
    }
}
