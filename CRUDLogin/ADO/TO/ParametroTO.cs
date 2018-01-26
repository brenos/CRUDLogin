using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLogin.ADO.TO
{
    public class ParametroTO
    {
        public string Conexao { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Base { get; set; }
        public string NmProjeto { get; set; }
        public string Pacote { get; set; }
        public string Pasta { get; set; }
        public string UsuarioAdmin { get; set; }
        public string SenhaAdmin { get; set; }
        public string EmailAdmin { get; set; }

    }
}
