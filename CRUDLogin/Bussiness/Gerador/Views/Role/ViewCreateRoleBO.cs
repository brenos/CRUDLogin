using CRUDLogin.ADO.TO;
using CRUDLogin.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLogin.Bussiness.Gerador.Views.Role
{
    class ViewCreateRoleBO
    {
        private ParametroTO _ParametroTO { get; set; }

        public ViewCreateRoleBO(ParametroTO parametroTO)
        {
            _ParametroTO = parametroTO;
        }

        public bool GerarArquivo()
        {
            bool gerou = true;
            string pagina = "";
            MemoryStream m = new MemoryStream(ResourceViewsRole.Create_Role);
            using (StreamReader sr = new StreamReader(m))
            {
                pagina = sr.ReadToEnd();
            }
            using (StreamWriter sw = new StreamWriter(_ParametroTO.Pasta + "\\Views\\Role\\Create.cshtml"))
            {
                sw.WriteLine(pagina);
            }
            return gerou;
        }
    }
}
