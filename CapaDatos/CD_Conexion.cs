using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CapaDatos
{
    public class CD_Conexion
    {
        //public SqlConnection Conexion = new SqlConnection(@"Data Source=LEONEL\SISTEMADEVENTAS;Initial Catalog=DB_Sistemas_V2;Integrated Security=True");
        public SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString);

        //Método que permite abrir la conexión a la base de datos 

        public SqlConnection Abrir()
        {
            //Validación para ver si está abierta o cerrada
            if (Conexion.State == ConnectionState.Closed)
            {
                Conexion.Open();
            }

            return Conexion;
        }

        //Método que permite cerrar la conexión a la base de datos 

        public SqlConnection Cerrar()
        {
            //Validación para ver si está abierta o cerrada

            if (Conexion.State == ConnectionState.Open)
            {
                Conexion.Close();
            }

            return Conexion;
        }
    }
}
