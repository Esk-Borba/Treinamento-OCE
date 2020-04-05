using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sessao_1___OCE
{
    public partial class Form3 : Form
    {
        sessao_1_OCEEntities bd = new sessao_1_OCEEntities();
        public static string caminhoFoto;
        public string caminhoFoto2;
        usuario usuarioLogado;
        public bool validacao;
        public static int contador = 0;
        public Form3(usuario usuario)
        {
            InitializeComponent();
            this.usuarioLogado = usuario;
            btnSalvar.Click += salvar;
            verificarUsuario();
            carregarImagem();
            carregarImagem2();
            verificarCampos();
            toolTip1.SetToolTip(circularPicturebox1, usuarioLogado.nome);
            circularPicturebox1.MouseClick += aparecerListBox;
            listBox.Items.Add("Perfil");
            listBox.Items.Add("Configurações");
            listBox.Items.Add("Sair");
            txtBairro.Visible = false;
            txtEmail.Visible = false;
            txtNome.Visible = false;
            txtSenha.Visible = false;
            txtEndereco.Visible = false;
            circularPicturebox2.Visible = false;
            lbBairro.Visible = false;
            lbCelular.Visible = false;
            lbCep.Visible = false;
            lbCidade.Visible = false;
            lbDataNascimento.Visible = false;
            lbEmail.Visible = false;
            lbEndereco.Visible = false;
            lbEstado.Visible = false;
            lbNome.Visible = false;
            lbPais.Visible = false;
            lbPerfil.Visible = false;
            lbSenha.Visible = false;
            lbTelefone.Visible = false;
            mkbCelular.Visible = false;
            mkbCep.Visible = false;
            mkbTelefone.Visible = false;
            cbCidade.Visible = false;
            cbEstado.Visible = false;
            cbPais.Visible = false;
            btnAlterarFoto.Visible = false;
            btnSalvar.Visible = false;
            dateTimePicker1.Visible = false;
            ckAdministrador.Visible = false;
            pictureBox3.Visible = false;
        }

        private bool verificarCampos()
        {
            if (string.IsNullOrEmpty(txtBairro.Text) || string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(dateTimePicker1.Text) || string.IsNullOrEmpty(mkbTelefone.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtSenha.Text) || string.IsNullOrEmpty(mkbCep.Text) || string.IsNullOrEmpty(cbPais.Text) || string.IsNullOrEmpty(cbEstado.Text) || string.IsNullOrEmpty(cbCidade.Text) || string.IsNullOrEmpty(txtEndereco.Text))
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public string cryptographyPass(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        private void salvar(object sender, EventArgs e)
        {
            verificarCampos();
            if (verificarCampos() == true)
            {
                MessageBox.Show("Prencha todos os campos obrigatórios");
            }
            if (verificarCampos() == false)
            {
                bd.usuario.ToList().ForEach(u =>
                {
                    if (u.id == usuarioLogado.id)
                    {
                        u.nome = txtNome.Text;
                        u.data_nascimento = dateTimePicker1.Value;
                        u.telefone = mkbTelefone.Text;
                        u.celular = mkbCelular.Text;
                        u.e_mail = txtEmail.Text;
                        u.senha = cryptographyPass(txtSenha.Text);
                        u.cep = mkbCep.Text;
                        u.bairro = txtBairro.Text;
                        u.enderaco = txtEndereco.Text;
                        bd.SaveChanges();
                    }
                });
                bd.pais.ToList().ForEach(p =>
                {
                    if (p.id_usuario == usuarioLogado.id)
                    {
                        p.id_usuario = usuarioLogado.id;
                        p.pais1 = cbPais.Text;
                        bd.SaveChanges();
                        bd.estados.ToList().ForEach(es =>
                        {
                            if (es.id_usuario == usuarioLogado.id)
                            {
                                es.id_pais = p.id;
                                es.id_usuario = usuarioLogado.id;
                                es.estado = cbEstado.Text;
                                bd.SaveChanges();
                                bd.cidades.ToList().ForEach(c =>
                                {
                                    if (c.id_usuario == usuarioLogado.id)
                                    {
                                        c.id_cidade = es.id;
                                        c.id_usuario = usuarioLogado.id;
                                        c.cidade = cbCidade.Text;
                                        bd.SaveChanges();
                                    }
                                });
                            }
                        });
                    }
                });
                MessageBox.Show("Informações Salvas com Sucesso!!!");
            }
        }

        private void aparecerListBox(object sender, EventArgs e)
        {
            listBox.Visible = true;
        }
        private void verificarUsuario()
        {
            bd.usuario.ToList().ForEach(v =>
            {
                if (v.id == usuarioLogado.id)
                {
                    caminhoFoto = v.imagem;
                }
            });
        }
        private void carregarImagem()
        {
            byte[] content = File.ReadAllBytes(caminhoFoto);
            MemoryStream ms = new MemoryStream(content);
            circularPicturebox1.Image = Image.FromStream(ms);
        }
        private void carregarImagem2()
        {
            byte[] content = File.ReadAllBytes(caminhoFoto);
            MemoryStream ms = new MemoryStream(content);
            circularPicturebox2.Image = Image.FromStream(ms);
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
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == 0)
            {

            }
            if (listBox.SelectedIndex == 1)
            {
                string senha = encryption(txtSenha.Text);
                contador = 0;
                listBox.Visible = false;
                txtBairro.Visible = true;
                txtEmail.Visible = true;
                txtNome.Visible = true;
                txtSenha.Visible = true;
                txtEndereco.Visible = true;
                circularPicturebox2.Visible = true;
                lbBairro.Visible = true;
                lbCelular.Visible = true;
                lbCep.Visible = true;
                lbCidade.Visible = true;
                lbDataNascimento.Visible = true;
                lbEmail.Visible = true;
                lbEndereco.Visible = true;
                lbEstado.Visible = true;
                lbNome.Visible = true;
                lbPais.Visible = true;
                lbPerfil.Visible = true;
                lbSenha.Visible = true;
                lbTelefone.Visible = true;
                mkbCelular.Visible = true;
                mkbCep.Visible = true;
                mkbTelefone.Visible = true;
                cbPais.Visible = true;
                cbEstado.Visible = true;
                cbCidade.Visible = true;
                btnAlterarFoto.Visible = true;
                btnSalvar.Visible = true;
                dateTimePicker1.Visible = true;
                ckAdministrador.Visible = true;
                bd.usuario.ToList().ForEach(u =>
                {
                    if (u.id == usuarioLogado.id)
                    {
                        txtNome.Text = u.nome;
                        txtEmail.Text = u.e_mail;
                        txtSenha.Text = senha;
                        txtBairro.Text = u.bairro;
                        txtEndereco.Text = u.enderaco;
                        dateTimePicker1.Value = u.data_nascimento;
                        mkbCelular.Text = u.celular;
                        mkbCep.Text = u.cep;
                        mkbTelefone.Text = u.telefone;
                        caminhoFoto = u.imagem;
                    }
                });
                cbPais.Items.Clear();
                bd.pais.ToList().ForEach(p =>
                {
                    cbPais.Items.Add(p.pais1);
                    if (p.id_usuario == usuarioLogado.id)
                    {
                        cbPais.Text = p.pais1;
                        cbEstado.Items.Clear();
                    }
                    bd.estados.ToList().ForEach(es =>
                    {
                        if (es.id_pais == p.id && es.id_usuario == usuarioLogado.id)
                        {
                            cbEstado.Items.Add(es.estado);
                            cbEstado.Text = es.estado;
                            cbCidade.Items.Clear();
                        }
                        bd.cidades.ToList().ForEach(c =>
                        {
                            if (c.id_cidade == es.id && c.id_usuario == usuarioLogado.id && contador < 1)
                            {
                                contador++;
                                cbCidade.Items.Add(c.cidade);
                                cbCidade.Text = c.cidade;
                            }
                        });
                    });
                });

            }
            if (listBox.SelectedIndex == 2)
            {
                new Form1().Show();
                this.Hide();
            }
        }

        private void cbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            bd.pais.ToList().ForEach(p =>
            {
                if (p.pais1 == cbPais.Text)
                {
                    cbEstado.Items.Clear();
                    bd.estados.ToList().ForEach(es =>
                    {

                        if (es.id_pais == p.id)
                        {
                            cbEstado.Items.Add(es.estado);
                            cbEstado.Text = es.estado;
                            cbCidade.Items.Clear();
                            bd.cidades.ToList().ForEach(c =>
                            {
                                if (es.estado == cbEstado.Text)
                                {
                                    if (c.id_cidade == es.id)
                                    {
                                        cbCidade.Items.Add(c.cidade);
                                        cbCidade.Text = c.cidade;
                                    }
                                }
                            });
                        }
                    });
                }
            });
        }
        private void txtNome_Leave(object sender, EventArgs e)
        {
            if (txtNome.TextLength >= 2 && !string.IsNullOrEmpty(txtNome.Text))
            {
                validacao = true;
                carregarPic1();
            }
            if (txtNome.TextLength < 2)
            {
                validacao = false;
                carregarPic1();
            }
        }
        private void carregarPic1()
        {
            pictureBox3.Visible = true;
            bd.imagensPrograma.ToList().ForEach(i =>
            {
                if (i.id == 1 && validacao == false)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
                if (i.id == 2 && validacao == true)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
            });
            byte[] content = File.ReadAllBytes(caminhoFoto2);
            MemoryStream ms = new MemoryStream(content);
            pictureBox3.Image = Image.FromStream(ms);
        }
        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            if (dateTimePicker1.Checked == true)
            {
                validacao = true;
                carregarPic2();
            }
            if (dateTimePicker1.Checked == false)
            {
                validacao = false;
                carregarPic2();
            }
        }
        private void carregarPic2()
        {
            pictureBox4.Visible = true;
            bd.imagensPrograma.ToList().ForEach(i =>
            {
                if (i.id == 1 && validacao == false)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
                if (i.id == 2 && validacao == true)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
            });
            byte[] content = File.ReadAllBytes(caminhoFoto2);
            MemoryStream ms = new MemoryStream(content);
            pictureBox4.Image = Image.FromStream(ms);
        }
        private void mkbTelefone_Leave(object sender, EventArgs e)
        {
            if (mkbTelefone.MaskCompleted == true && !string.IsNullOrEmpty(mkbTelefone.Text))
            {
                validacao = true;
                carregarPic3();
            }
            if (mkbTelefone.MaskCompleted == false)
            {
                validacao = false;
                carregarPic3();
            }
        }
        private void carregarPic3()
        {
            pictureBox5.Visible = true;
            bd.imagensPrograma.ToList().ForEach(i =>
            {
                if (i.id == 1 && validacao == false)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
                if (i.id == 2 && validacao == true)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
            });
            byte[] content = File.ReadAllBytes(caminhoFoto2);
            MemoryStream ms = new MemoryStream(content);
            pictureBox5.Image = Image.FromStream(ms);
        }
        private void mkbCelular_Leave(object sender, EventArgs e)
        {
            if (mkbCelular.MaskCompleted == true && !string.IsNullOrEmpty(mkbCelular.Text))
            {
                validacao = true;
                carregarPic4();
            }
            if (mkbCelular.MaskCompleted == false)
            {
                validacao = false;
                carregarPic4();
            }
        }
        private void carregarPic4()
        {
            pictureBox6.Visible = true;
            bd.imagensPrograma.ToList().ForEach(i =>
            {
                if (i.id == 1 && validacao == false)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
                if (i.id == 2 && validacao == true)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
            });
            byte[] content = File.ReadAllBytes(caminhoFoto2);
            MemoryStream ms = new MemoryStream(content);
            pictureBox6.Image = Image.FromStream(ms);
        }
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (ValidarEmail(txtEmail.Text) == false && txtEmail.Text != "")
            {
                validacao = false;
                carregarPic5();
            }
            else
            {
                validacao = true;
                carregarPic5();
            }
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
        private void carregarPic5()
        {
            pictureBox7.Visible = true;
            bd.imagensPrograma.ToList().ForEach(i =>
            {
                if (i.id == 1 && validacao == false)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
                if (i.id == 2 && validacao == true)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
            });
            byte[] content = File.ReadAllBytes(caminhoFoto2);
            MemoryStream ms = new MemoryStream(content);
            pictureBox7.Image = Image.FromStream(ms);
        }
        private void txtSenha_Leave(object sender, EventArgs e)
        {
            if (txtSenha.TextLength >= 2 && !string.IsNullOrEmpty(txtSenha.Text))
            {
                validacao = true;
                carregarPic6();

            }
            if (txtSenha.TextLength < 2)
            {
                validacao = false;
                carregarPic6();
            }
        }
        private void carregarPic6()
        {
            pictureBox8.Visible = true;
            bd.imagensPrograma.ToList().ForEach(i =>
            {
                if (i.id == 1 && validacao == false)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
                if (i.id == 2 && validacao == true)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
            });
            byte[] content = File.ReadAllBytes(caminhoFoto2);
            MemoryStream ms = new MemoryStream(content);
            pictureBox8.Image = Image.FromStream(ms);
        }
        private void mkbCep_Leave(object sender, EventArgs e)
        {
            if (mkbCelular.MaskCompleted == true && !string.IsNullOrEmpty(mkbCep.Text))
            {
                validacao = true;
                carregarPic16();
            }
            if (mkbCelular.MaskCompleted == false)
            {
                validacao = false;
                carregarPic16();
            }
        }
        private void carregarPic16()
        {
            pictureBox16.Visible = true;
            bd.imagensPrograma.ToList().ForEach(i =>
            {
                if (i.id == 1 && validacao == false)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
                if (i.id == 2 && validacao == true)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
            });
            byte[] content = File.ReadAllBytes(caminhoFoto2);
            MemoryStream ms = new MemoryStream(content);
            pictureBox16.Image = Image.FromStream(ms);
        }

        private void cbPais_Leave(object sender, EventArgs e)
        {
            if (cbPais.SelectedIndex >= 0 && !string.IsNullOrEmpty(cbPais.Text))
            {
                validacao = true;
                carregarPic15();
            }
            else
            {
                validacao = false;
                carregarPic15();
            }
        }
        private void carregarPic15()
        {
            pictureBox15.Visible = true;
            bd.imagensPrograma.ToList().ForEach(i =>
            {
                if (i.id == 1 && validacao == false)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
                if (i.id == 2 && validacao == true)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
            });
            byte[] content = File.ReadAllBytes(caminhoFoto2);
            MemoryStream ms = new MemoryStream(content);
            pictureBox15.Image = Image.FromStream(ms);
        }
        private void cbEstado_Leave(object sender, EventArgs e)
        {
            if (cbEstado.SelectedIndex >= 0 && !string.IsNullOrEmpty(cbEstado.Text))
            {
                validacao = true;
                carregarPic13();
            }
            else
            {
                validacao = false;
                carregarPic13();
            }
        }
        private void carregarPic13()
        {
            pictureBox13.Visible = true;
            bd.imagensPrograma.ToList().ForEach(i =>
            {
                if (i.id == 1 && validacao == false)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
                if (i.id == 2 && validacao == true)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
            });
            byte[] content = File.ReadAllBytes(caminhoFoto2);
            MemoryStream ms = new MemoryStream(content);
            pictureBox13.Image = Image.FromStream(ms);
        }
        private void cbCidade_Leave(object sender, EventArgs e)
        {
            if (cbCidade.SelectedIndex >= 0 && !string.IsNullOrEmpty(cbCidade.Text))
            {
                validacao = true;
                carregarPic12();
            }
            else
            {
                validacao = false;
                carregarPic12();
            }
        }
        private void carregarPic12()
        {
            pictureBox12.Visible = true;
            bd.imagensPrograma.ToList().ForEach(i =>
            {
                if (i.id == 1 && validacao == false)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
                if (i.id == 2 && validacao == true)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
            });
            byte[] content = File.ReadAllBytes(caminhoFoto2);
            MemoryStream ms = new MemoryStream(content);
            pictureBox12.Image = Image.FromStream(ms);
        }
        private void txtBairro_Leave(object sender, EventArgs e)
        {
            if (txtBairro.TextLength >= 2 && !string.IsNullOrEmpty(txtBairro.Text))
            {
                validacao = true;
                carregarPic11();
            }
            if (txtBairro.TextLength < 2)
            {
                validacao = false;
                carregarPic11();
            }
        }
        private void carregarPic11()
        {
            pictureBox11.Visible = true;
            bd.imagensPrograma.ToList().ForEach(i =>
            {
                if (i.id == 1 && validacao == false)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
                if (i.id == 2 && validacao == true)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
            });
            byte[] content = File.ReadAllBytes(caminhoFoto2);
            MemoryStream ms = new MemoryStream(content);
            pictureBox11.Image = Image.FromStream(ms);
        }
        private void txtEndereco_Leave(object sender, EventArgs e)
        {
            if (txtEndereco.TextLength >= 2 && !string.IsNullOrEmpty(txtEndereco.Text))
            {
                validacao = true;
                carregarPic10();
            }
            if (txtEndereco.TextLength < 2)
            {
                validacao = false;
                carregarPic10();
            }
        }
        private void carregarPic10()
        {
            pictureBox10.Visible = true;
            bd.imagensPrograma.ToList().ForEach(i =>
            {
                if (i.id == 1 && validacao == false)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
                if (i.id == 2 && validacao == true)
                {
                    caminhoFoto2 = i.enderecoImagem;
                }
            });
            byte[] content = File.ReadAllBytes(caminhoFoto2);
            MemoryStream ms = new MemoryStream(content);
            pictureBox10.Image = Image.FromStream(ms);
        }
    }
}