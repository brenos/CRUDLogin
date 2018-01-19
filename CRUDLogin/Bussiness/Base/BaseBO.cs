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

        public bool isConectado(string conexao)
        {
            BaseFactory bf = new BaseFactory();
            IBase bs = bf.Factory(BaseFactory.BASESQLSERVER, conexao);
            return bs.isConectado();
        }

        public List<DatabaseTO> getDatabases(string conexao)
        {
            BaseFactory bf = new BaseFactory();
            IBase bs = bf.Factory(BaseFactory.BASESQLSERVER, conexao);
            return bs.getDatabases();
        }
    }
}
