using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sessao_1___OCE
{
    public partial class Form1 : Form
    {
        sessao_1_OCEEntities bd = new sessao_1_OCEEntities();
        public static usuario logado = new usuario();
        public static usuario logado2 = new usuario();
        public static string caminhoFoto;
        public Form1()
        {
            InitializeComponent();
            btnEntrar.Click += entrar;
            btnSair.Click += sair;
            btnEntrar.Hide();
        }
        private void sair(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void entrar(object sender, EventArgs e)
        {
            logado = null;
            logado2 = null;
            string senha = encryption(txtSenha.Text);
            if (string.IsNullOrEmpty(txtSenha.Text))
            {
                MessageBox.Show("Atenção: Informe sua Senha");
            }
            logado = bd.usuario.Where(u => u.e_mail.Equals(txtEmail.Text) && u.senha.Equals(senha)).FirstOrDefault();
            if (logado != null)
            {
                usuario usuarioLogado = logado;
                new Form2(usuarioLogado).Show();
                this.Hide();
            }

            logado2 = bd.usuario.Where(u2 => u2.e_mail.Equals(txtEmail.Text)).FirstOrDefault();
            if(logado2 == null)
            {
                MessageBox.Show("Atenção: Representante não cadastrado");
            }
            else if (logado2.senha != senha)
            {
                MessageBox.Show("Atenção: Senha Incorreta");
            }
        }

        private string encryption(string senha)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(senha);
            byte[] hash = md5.ComputeHash(inputBytes);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
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
            if (ValidarEmail(txtEmail.Text) == false && txtEmail.Text != "")
            {
                MessageBox.Show("Email com formato incorreto!");
            }
            if (txtEmail.Text == "")
            {
                label4.Text = "Atenção: Informe seu E-mail";
            }
            if (ValidarEmail(txtEmail.Text) == true)
            {
                bd.usuario.ToList().ForEach(i =>
                {
                    if (i.e_mail == txtEmail.Text)
                    {
                        caminhoFoto = i.imagem;
                        imagemCarregar();
                    }
                });
                btnEntrar.Show();
            }
        }
        private void imagemCarregar()
        {
            byte[] content = File.ReadAllBytes(caminhoFoto);
            MemoryStream ms = new MemoryStream(content);
            circularPicturebox1.Image = Image.FromStream(ms);
        }
    }
}
