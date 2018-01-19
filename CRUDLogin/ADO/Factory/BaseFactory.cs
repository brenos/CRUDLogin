using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLogin.ADO.Factory
{
    class BaseFactory
    {
        public static int BASESQLSERVER = 0;
        public IBase Factory(int tipoBase, string conexao)
        {
            switch (tipoBase)
            {
                case 0:
                    return new BaseSqlServer(conexao);
                    break;
                default:
                    return null;
            }
        }
    }
}
