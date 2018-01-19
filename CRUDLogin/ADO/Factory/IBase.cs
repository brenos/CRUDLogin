using CRUDLogin.ADO.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLogin.ADO.Factory
{
    interface IBase
    {
        bool isConectado();
        List<DatabaseTO> getDatabases();
    }
}
