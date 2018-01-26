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
    class LoginModelBO
    {
        private ParametroTO _ParametroTO { get; set; }

        public LoginModelBO(ParametroTO parametroTO)
        {
            _ParametroTO = parametroTO;
        }

        public bool GerarArquivo()
        {
            bool gerou = true;
            string pagina = "";
            MemoryStream m = new MemoryStream(ResourcesModelMembership.LoginModel);
            using (StreamReader sr = new StreamReader(m))
            {
                pagina = sr.ReadToEnd();
            }
            pagina = pagina.Replace("{0}", _ParametroTO.Pacote);
            using (StreamWriter sw = new StreamWriter(_ParametroTO.Pasta + "\\Models\\LoginModel.cs"))
            {
                sw.WriteLine(pagina);
            }
            return gerou;
        }
    }
}
