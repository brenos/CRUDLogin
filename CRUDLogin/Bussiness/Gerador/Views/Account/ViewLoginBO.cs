using CRUDLogin.ADO.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CRUDLogin.Properties;

namespace CRUDLogin.Bussiness.Gerador.Views.Account
{
    class ViewLoginBO
    {
        private ParametroTO _ParametroTO { get; set; }

        public ViewLoginBO(ParametroTO parametroTO)
        {
            _ParametroTO = parametroTO;
        }

        public bool GerarArquivo()
        {
            bool gerou = true;
            string pagina = "";
            MemoryStream m = new MemoryStream(ResourcesViews.Login_Account);
            using (StreamReader sr = new StreamReader(m))
            {
                pagina = sr.ReadToEnd();
            }
            pagina = pagina.Replace("{0}", _ParametroTO.NmProjeto);
            pagina = pagina.Replace("{1}", _ParametroTO.NmProjeto);
            using (StreamWriter sw = new StreamWriter(_ParametroTO.Pasta + "\\Views\\Account\\Login.cshtml"))
            {
                sw.WriteLine(pagina);
            }
            return gerou;
        }

    }
}
