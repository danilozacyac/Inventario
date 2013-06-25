using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Inventario.DbAccess
{
    public class Conexion
    {

        public static SqlConnection GetConexion()
        {
            String bdStringSql = ConfigurationManager.ConnectionStrings["serverConnection"].ConnectionString;


            SqlConnection realConnection = new SqlConnection(bdStringSql);
            return realConnection;
        }
    }
}
