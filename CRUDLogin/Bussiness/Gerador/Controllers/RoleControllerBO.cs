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
    class RoleControllerBO
    {
        private ParametroTO _ParametroTO { get; set; }

        public RoleControllerBO(ParametroTO parametroTO)
        {
            _ParametroTO = parametroTO;
        }

        public bool GerarArquivo()
        {
            bool gerou = true;
            string pagina = "";
            MemoryStream m = new MemoryStream(ResourcesControllerMembership.RoleController);
            using (StreamReader sr = new StreamReader(m))
            {
                pagina = sr.ReadToEnd();
            }
            pagina = pagina.Replace("{0}", _ParametroTO.Pacote);
            pagina = pagina.Replace("{1}", _ParametroTO.Pacote);
            using (StreamWriter sw = new StreamWriter(_ParametroTO.Pasta + "\\Controllers\\RoleController.cs"))
            {
                sw.WriteLine(pagina);
            }
            return gerou;
        }
    }
}
