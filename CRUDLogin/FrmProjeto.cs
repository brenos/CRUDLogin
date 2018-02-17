using CRUDLogin.ADO.TO;
using CRUDLogin.Bussiness.Gerador;
using CRUDLogin.Bussiness.Gerador.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDLogin
{
    public partial class FrmProjeto : Form
    {
        private FrmConexao _FrmConexao { get; set; }
        private bool _FormClosed { get; set; }
        private ParametroTO _ParametroTO { get; set; }

        public FrmProjeto(FrmConexao frmConexao, ParametroTO parametroTO)
        {
            InitializeComponent();
            _ParametroTO = parametroTO;
            _FrmConexao = frmConexao;
            _FormClosed = true;
            cmbTipoAutenticacao.SelectedIndex = 0;

            #if DEBUG
                txtNomeProjeto.Text = "Teste";
                txtPacote.Text = "Teste";
                txtCaminho.Text = @"C:\Users\INFLUIR - BRENO\source\repos\TesteCRUDLogin3\TesteCRUDLogin3";
                txtUsuarioAcesso.Text = "administrador";
                txtEmailUsuario.Text = "admin@crud.com";
                txtSenhaAdm.Text = "123456";
            #endif
        }

        private void btnSelecionarPasta_Click(object sender, EventArgs e)
        {
            if (fbdPastaProjeto.ShowDialog() == DialogResult.OK)
                txtCaminho.Text = fbdPastaProjeto.SelectedPath;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            _FrmConexao.Visible = true;
            _FormClosed = false;
            this.Close();
        }

        private void btnbtnGerar_Click(object sender, EventArgs e)
        {
            if (Valido())
            {
                _ParametroTO.Pacote = txtPacote.Text;
                _ParametroTO.Pasta = txtCaminho.Text;
                _ParametroTO.NmProjeto = txtNomeProjeto.Text;
                _ParametroTO.SenhaAdmin = txtSenhaAdm.Text;
                _ParametroTO.UsuarioAdmin = txtUsuarioAcesso.Text;
                _ParametroTO.EmailAdmin = txtEmailUsuario.Text;

                GeradorArquivoBO geradorArquivo = new GeradorArquivoBO();
                RetornoTO retorno = geradorArquivo.GerarCRUDLogin(_ParametroTO);
                if (retorno.IsOK)
                {
                    MessageBox.Show(retorno.Mensagem, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (DialogResult.No.Equals(MessageBox.Show("Deseja executar novamente o CRULogin?", "Executar novmente?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)))
                    {
                        Application.Exit();
                    }
                    
                }
                else
                {
                    MessageBox.Show(retorno.Mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        private void FrmProjeto_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_FormClosed)
                Application.Exit();
        }

        /** Métodos Auxiliares **/

        private bool Valido()
        {
            bool isValido = true;
            StringBuilder erros = new StringBuilder("Erro(s):");
            erros.AppendLine();
            erros.AppendLine();

            if (txtPacote.Text.Trim().Length <= 0)
            {
                isValido = false;
                erros.AppendLine("- Favor preencher o campo Pacote do Projeto");
            }

            if (txtCaminho.Text.Trim().Length <= 0)
            {
                isValido = false;
                erros.AppendLine("- Favor preencher o campo Caminho do Projeto");
            }

            if (txtUsuarioAcesso.Text.Trim().Length <= 0)
            {
                isValido = false;
                erros.AppendLine("- Favor preencher o campo Usuário de Acesso");
            }

            if (txtEmailUsuario.Text.Trim().Length <= 0)
            {
                isValido = false;
                erros.AppendLine("- Favor preencher o campo Email do Usuário");
            }
            else
            {
                if ((!txtEmailUsuario.Text.Contains("@") && !txtEmailUsuario.Text.Contains(".")) ||
                    txtEmailUsuario.Text.Length < 8)
                {
                    isValido = false;
                    erros.AppendLine("- O email digitado não é valido");
                }
            }

            if (txtSenhaAdm.Text.Trim().Length <= 0)
            {
                isValido = false;
                erros.AppendLine("- Favor preencher o campo Senha de Acesso");
            }

            if (txtSenhaAdm.Text.Trim().Length < 6)
            {
                isValido = false;
                erros.AppendLine("- A senha de acesso deve conter mais de 6 caracteres.");
            }

            if (!isValido)
                MessageBox.Show(erros.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return isValido;
        }

    }
}
