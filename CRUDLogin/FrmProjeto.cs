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

        public FrmProjeto(FrmConexao frmConexao)
        {
            InitializeComponent();
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
    }
}
