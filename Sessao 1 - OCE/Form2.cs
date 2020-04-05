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
        usuario usuario;
        public Form2(usuario usuarioLogado)
        {
            InitializeComponent();
            this.usuario = usuarioLogado;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            timer1.Interval = 1300;
            progressBar1.Maximum = 12;
            timer1.Tick += new EventHandler(timer1_Tick);
        }
        void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            if (progressBar1.Value == 1)
            {
                label1.Text = "Carregando Componentes...";
            }
            if (progressBar1.Value == 4)
            {
                label1.Text = "Carregando Dados...";
            }
            if (progressBar1.Value == 7)
            {
                label1.Text = "Carregando Imagens...";
            }
            if (progressBar1.Value == 10)
            {
                label1.Text = "Carregando Dashboard...";
            }
            if (progressBar1.Value == progressBar1.Maximum)
            {
                timer1.Stop();
                new Form3(usuario).Show();
                this.Hide();
            }
        }
    }
}
