using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDLogin.ADO.TO;

namespace CRUDLogin.ADO.Factory
{
    class BaseSqlServer : IBase
    {
        private string _Conn { get; set; }

        private SqlConnection _Conexao
        {
            get
            {
                return new SqlConnection(_Conn);
            }
        }

        public BaseSqlServer(string conexao)
        {
            _Conn = conexao;
        }

        public List<DatabaseTO> GetDatabases()
        {
            List<DatabaseTO> databases = new List<DatabaseTO>();
            using (SqlConnection con = _Conexao)
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT name FROM SYS.DATABASES", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DatabaseTO databaseTO = new DatabaseTO
                            {
                                Nome = dr[0].ToString()
                            };
                            databases.Add(databaseTO);
                        }
                    }
                }
            }
            return databases;
        }

        public bool IsConectado()
        {
            bool isConectado = true;
            try
            {
                _Conexao.Open();
                isConectado = true;
                _Conexao.Close();
            }
            catch (Exception)
            {
                isConectado = false;
            }
            return isConectado;
        }
    }
}
