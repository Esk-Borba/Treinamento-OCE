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
    public partial class Form1 : Form
    {
        sessao_1_OCEEntities bd = new sessao_1_OCEEntities();
        public static usuario logado = new usuario();
        public Form1()
        {
            InitializeComponent();

            btnEntrar.Click += entrar;
            btnSair.Click += sair;
            btnEntrar.Hide();
            label4.Text = "Atenção: Informe seu E-mail";
        }

        private void sair(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void entrar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSenha.Text))
            {
                MessageBox.Show("Atenção: Informe sua Senha");
            }
            logado = bd.usuario.Where(u => u.e_mail.Equals(txtEmail.Text) && u.senha.Equals(txtSenha.Text)).FirstOrDefault();
            if (logado != null)
            {
                new Form2().Show();
                this.Hide();
            }
            bd.usuario.ToList().ForEach(u =>
            {
                if (u.e_mail == txtEmail.Text && u.senha != txtSenha.Text)
                {
                    MessageBox.Show("Atenção: Senha Incorreta");
                }
                if (u.e_mail != txtEmail.Text)
                {
                    MessageBox.Show("Atenção: Representanb");
                }
            });
        }
        public static bool ValidarEmail(string strEmail)
        {
            string strModelo = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (System.Text.RegularExpressions.Regex.IsMatch(strEmail, strModelo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (ValidarEmail(txtEmail.Text) == false)
            {
                MessageBox.Show("Email com formato incorreto!");
            }
            if (ValidarEmail(txtEmail.Text) == true)
            {
                btnEntrar.Show();
            }
        }
    }
}
