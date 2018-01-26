using CRUDLogin.ADO.TO;
using CRUDLogin.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLogin.Bussiness.Gerador.Models
{
    class RoleModelBO
    {
        private ParametroTO _ParametroTO { get; set; }

        public RoleModelBO(ParametroTO parametroTO)
        {
            _ParametroTO = parametroTO;
        }

        public bool GerarArquivo()
        {
            bool gerou = true;
            string pagina = "";
            MemoryStream m = new MemoryStream(ResourcesModelMembership.RoleModel);
            using (StreamReader sr = new StreamReader(m))
            {
                pagina = sr.ReadToEnd();
            }
            pagina = pagina.Replace("{0}", _ParametroTO.Pacote);
            using (StreamWriter sw = new StreamWriter(_ParametroTO.Pasta + "\\Models\\RoleModel.cs"))
            {
                sw.WriteLine(pagina);
            }
            return gerou;
        }
    }
}
