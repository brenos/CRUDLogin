using CRUDLogin.ADO.TO;
using CRUDLogin.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLogin.Bussiness.Gerador.Controllers
{
    class AccountControllerBO
    {
        private ParametroTO _ParametroTO { get; set; }

        public AccountControllerBO(ParametroTO parametroTO)
        {
            _ParametroTO = parametroTO;
        }

        public bool GerarArquivo()
        {
            bool gerou = true;
            string pagina = "";
            MemoryStream m = new MemoryStream(ResourcesControllerMembership.AccountController);
            using (StreamReader sr = new StreamReader(m))
            {
                pagina = sr.ReadToEnd();
            }
            pagina = pagina.Replace("{0}", _ParametroTO.Pacote);
            pagina = pagina.Replace("{1}", _ParametroTO.Pacote);
            pagina = pagina.Replace("{2}", _ParametroTO.UsuarioAdmin);
            pagina = pagina.Replace("{3}", _ParametroTO.SenhaAdmin);
            pagina = pagina.Replace("{4}", _ParametroTO.EmailAdmin);
            using (StreamWriter sw = new StreamWriter(_ParametroTO.Pasta + "\\Controllers\\AccountController.cs"))
            {
                sw.WriteLine(pagina);
            }
            return gerou;
        }
    }
}
