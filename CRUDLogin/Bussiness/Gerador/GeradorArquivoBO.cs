using CRUDLogin.ADO.TO;
using CRUDLogin.Bussiness.Gerador.Config;
using CRUDLogin.Bussiness.Gerador.Controllers;
using CRUDLogin.Bussiness.Gerador.Models;
using CRUDLogin.Bussiness.Gerador.Views.Account;
using CRUDLogin.Bussiness.Gerador.Views.Role;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLogin.Bussiness.Gerador
{
    class GeradorArquivoBO
    {

        public GeradorArquivoBO()
        {
        }

        public RetornoTO GerarCRUDLogin(ParametroTO parametroTO)
        {
            RetornoTO retorno = new RetornoTO();
            
            if (Directory.GetDirectories(parametroTO.Pasta).Where(s => s == parametroTO.Pasta + "\\Views").Count() > 0)
            {
                //Escrever no Web.Config
                //Criar as pastas necessárias das views
                Directory.CreateDirectory(parametroTO.Pasta + "\\Views\\Role");
                Directory.CreateDirectory(parametroTO.Pasta + "\\Views\\Account");
                //Gerar as Views
                GerarViews(parametroTO, false);
                //Criar a pasta necessárias do controller
                Directory.CreateDirectory(parametroTO.Pasta + "\\Controllers");
                //Gerar os Controllers
                GerarControllers(parametroTO);
                //Criar a pasta necessárias do controller
                Directory.CreateDirectory(parametroTO.Pasta + "\\Models");
                //Gerar os Models
                GerarModels(parametroTO);
                //Alterar o web.config
                AlterarWebConfig(parametroTO);

                retorno.IsOK = true;
                retorno.Mensagem = "CRUD gerado com sucesso!";
            }
            else
            {
                retorno.IsOK = false;
                retorno.Mensagem = "Favor selecionar a pasta correta do projeto.";
            }

            return retorno;
        }

        private void GerarViews(ParametroTO parametroTO, bool isBotstrap)
        {
            if (!isBotstrap)
            {
                //Views ACCOUNT
                ViewLoginBO viewLogin = new ViewLoginBO(parametroTO);
                viewLogin.GerarArquivo();

                ViewResetPasswordBO viewResetPassword = new ViewResetPasswordBO(parametroTO);
                viewResetPassword.GerarArquivo();

                ViewForgotBO viewForgot = new ViewForgotBO(parametroTO);
                viewForgot.GerarArquivo();

                ViewCreateAccountBO viewCreate = new ViewCreateAccountBO(parametroTO);
                viewCreate.GerarArquivo();

                ViewEditAccountBO viewEditAccount = new ViewEditAccountBO(parametroTO);
                viewEditAccount.GerarArquivo();

                ViewDeleteAccountBO viewDeleteAccount = new ViewDeleteAccountBO(parametroTO);
                viewDeleteAccount.GerarArquivo();

                ViewDetailsAccountBO viewDetailsAccount = new ViewDetailsAccountBO(parametroTO);
                viewDetailsAccount.GerarArquivo();

                ViewIndexAccountBO viewIndexAccount = new ViewIndexAccountBO(parametroTO);
                viewIndexAccount.GerarArquivo();

                //Views ROLE
                ViewCreateRoleBO viewCreateRole = new ViewCreateRoleBO(parametroTO);
                viewCreateRole.GerarArquivo();

                ViewDetailsRoleBO viewDetailsRole = new ViewDetailsRoleBO(parametroTO);
                viewDetailsRole.GerarArquivo();

                ViewDeleteRoleBO viewDeleteRole = new ViewDeleteRoleBO(parametroTO);
                viewDeleteRole.GerarArquivo();

                ViewIndexRoleBO viewIndexRole = new ViewIndexRoleBO(parametroTO);
                viewIndexRole.GerarArquivo();
            }
        }

        private void GerarControllers(ParametroTO parametroTO)
        {
            AccountControllerBO accountController = new AccountControllerBO(parametroTO);
            accountController.GerarArquivo();

            RoleControllerBO roleController = new RoleControllerBO(parametroTO);
            roleController.GerarArquivo();
        }

        private void GerarModels(ParametroTO parametroTO)
        {
            LoginModelBO loginModel = new LoginModelBO(parametroTO);
            loginModel.GerarArquivo();

            RoleModelBO roleModel = new RoleModelBO(parametroTO);
            roleModel.GerarArquivo();
        }

        private void AlterarWebConfig(ParametroTO parametroTO)
        {
            WebConfigBO webConfig = new WebConfigBO(parametroTO);
            webConfig.AddTagsNoWebConfig();
        }
    }
}
