using CRUDLogin.ADO.Factory;
using CRUDLogin.ADO.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLogin.Bussiness.Base
{
    class BaseBO
    {
        public BaseBO()
        {
        }

        public bool IsConectado(string conexao)
        {
            BaseFactory bf = new BaseFactory();
            IBase bs = bf.Factory(BaseFactory.BASESQLSERVER, conexao);
            return bs.IsConectado();
        }

        public List<DatabaseTO> GetDatabases(string conexao)
        {
            BaseFactory bf = new BaseFactory();
            IBase bs = bf.Factory(BaseFactory.BASESQLSERVER, conexao);
            return bs.GetDatabases();
        }
    }
}
