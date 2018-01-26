using CRUDLogin.ADO.TO;
using CRUDLogin.Bussiness.Base;
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
    public partial class FrmConexao : Form
    {
        private string _Conexao { get; set; }

        public FrmConexao()
        {
            InitializeComponent();
            //Inicialização de componentes
            cmbBanco.SelectedIndex = 0;

            cmbBase.SelectedIndex = 0;
            txtConexao.Text = @"DESKTOP-626SLSH\SQLEXPRESS";
            txtUsuario.Text = "sa";
            txtSenha.Text = "12345678";
        }

        private void chbBaseManual_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbIsManual = sender as CheckBox;
            if (cbIsManual.Checked)
                cmbBase.DropDownStyle = ComboBoxStyle.DropDown;
            else
                cmbBase.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnTestarCon_Click(object sender, EventArgs e)
        {
            cmbBase.Items.Clear();

            _Conexao = getConnectionString();

            BaseBO baseBO = new BaseBO();
            if (baseBO.IsConectado(_Conexao))
            {
                preencheComboBase();
                MessageBox.Show("Banco de dados conectado!", "Teste de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Banco de dados não conectado!", "Teste de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAvancar_Click(object sender, EventArgs e)
        {
            if (!camposVazios())
            {
                ParametroTO parametroTO = new ParametroTO
                {
                    Base = cmbBase.SelectedText,
                    Conexao = txtConexao.Text,
                    Senha = txtSenha.Text,
                    Usuario = txtUsuario.Text
                };
                FrmProjeto frmProjeto = new FrmProjeto(this, parametroTO);
                frmProjeto.Show();
                this.Visible = false;
            }
            
        }

        private void txtSenha_Leave(object sender, EventArgs e)
        {
            _Conexao = getConnectionString();
            BaseBO baseBO = new BaseBO();
            if (baseBO.IsConectado(_Conexao))
            {
                preencheComboBase();
            }
            else
            {
                chbBaseManual.Checked = true;
            }
        }

        /** Metodos Auxiliares **/

        private bool camposVazios()
        {
            bool isVazio = false;
            StringBuilder erros = new StringBuilder("Favor preencher os campos abaixo:");
            erros.AppendLine();
            erros.AppendLine();

            if (txtConexao.Text.Trim().Length <= 0)
            {
                isVazio = true;
                erros.AppendLine("Conexão");
            }

            if (txtUsuario.Text.Trim().Length <= 0)
            {
                isVazio = true;
                erros.AppendLine("Usuário");
            }

            if (txtSenha.Text.Trim().Length <= 0)
            {
                isVazio = true;
                erros.AppendLine("Senha");
            }

            if (cmbBase.Text.Trim().Length <= 0)
            {
                isVazio = true;
                erros.AppendLine("Base de dados");
            }

            if (isVazio)
                MessageBox.Show(erros.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return isVazio;
        }

        private void preencheComboBase()
        {
            cmbBase.Items.Clear();

            BaseBO baseBO = new BaseBO();
            List<DatabaseTO> databases = baseBO.GetDatabases(_Conexao);
            foreach (var database in databases)
            {
                cmbBase.Items.Add(database.Nome);
            }
            cmbBase.SelectedIndex = 0;
        }

        private string getConnectionString()
        {
            string connectionString = "Data Source=" + txtConexao.Text.Trim() + ";";
            connectionString += "User ID=" + txtUsuario.Text.Trim() + ";";
            connectionString += "Password=" + txtSenha.Text.Trim() + ";";
            return connectionString;
        }
        
    }
}
