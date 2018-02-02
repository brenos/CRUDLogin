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
            if (!camposVazios())
            {
                _ParametroTO.Pacote = txtPacote.Text;
                _ParametroTO.Pasta = txtCaminho.Text;
                _ParametroTO.NmProjeto = txtNomeProjeto.Text;
                _ParametroTO.SenhaAdmin = txtSenhaAdm.Text;

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

        private bool camposVazios()
        {
            bool isVazio = false;
            StringBuilder erros = new StringBuilder("Favor preencher os campos abaixo:");
            erros.AppendLine();
            erros.AppendLine();

            if (txtPacote.Text.Trim().Length <= 0)
            {
                isVazio = true;
                erros.AppendLine("Pacote do Projeto");
            }

            if (txtCaminho.Text.Trim().Length <= 0)
            {
                isVazio = true;
                erros.AppendLine("Caminho do Projeto");
            }

            if (txtSenhaAdm.Text.Trim().Length <= 0)
            {
                isVazio = true;
                erros.AppendLine("Senha (Administrador)");
            }

            if (isVazio)
                MessageBox.Show(erros.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return isVazio;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
