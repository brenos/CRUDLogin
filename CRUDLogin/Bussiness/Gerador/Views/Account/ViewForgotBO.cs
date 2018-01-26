using CRUDLogin.ADO.TO;
using CRUDLogin.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLogin.Bussiness.Gerador.Views.Account
{
    class ViewForgotBO
    {
        private ParametroTO _ParametroTO { get; set; }

        public ViewForgotBO(ParametroTO parametroTO)
        {
            _ParametroTO = parametroTO;
        }

        public bool GerarArquivo()
        {
            bool gerou = true;
            string pagina = "";
            MemoryStream m = new MemoryStream(ResourcesViews.Forgot_Account);
            using (StreamReader sr = new StreamReader(m))
            {
                pagina = sr.ReadToEnd();
            }
            pagina = pagina.Replace("{0}", _ParametroTO.NmProjeto);
            pagina = pagina.Replace("{1}", _ParametroTO.NmProjeto);
            using (StreamWriter sw = new StreamWriter(_ParametroTO.Pasta + "\\Views\\Account\\Forgot.cshtml"))
            {
                sw.WriteLine(pagina);
            }
            return gerou;
        }
    }
}
