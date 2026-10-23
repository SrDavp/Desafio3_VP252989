using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;

namespace Desafio3.sql
{
    public class conexion
    {
        private string cadena = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString; 
        public SqlConnection Conexion()
        {
            return new SqlConnection(cadena);
        }
        
    }
}
