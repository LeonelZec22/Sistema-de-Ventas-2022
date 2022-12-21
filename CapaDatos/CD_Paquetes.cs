using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System.Windows.Forms;
using System.Windows.Controls;

namespace CapaDatos
{
    public class CD_Paquetes
    {
        //Instanciamos nuestra conexión

        CD_Conexion Con = new CD_Conexion();

        SqlCommand Cmd;
        SqlDataAdapter Da;
        DataTable dataTable;

        //Método que me permite agregar un Servicios a la base de datos 

        //Método que me permite agregar un Servicios a la base de datos 

        public void AgregarPaquetes(CE_Paquetes Paquetes)
        {
            Cmd = new SqlCommand("AgregarPaquetes", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Codigo", Paquetes.Codigo));
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Paquetes.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Descripcion", Paquetes.Descripcion));
            Cmd.Parameters.Add(new SqlParameter("@Cantidad_Vendida", Paquetes.Cantidad_Vendida));
            Cmd.Parameters.Add(new SqlParameter("@Precio_Venta", Paquetes.Precio_Venta));

            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método que me permite Editar un Servicios a la base de datos 

        public void EditarPaquete(CE_Paquetes Paquetes)
        {
            Cmd = new SqlCommand("EditarPaquete", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Codigo", Paquetes.Codigo));
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Paquetes.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Descripcion", Paquetes.Descripcion));
            Cmd.Parameters.Add(new SqlParameter("@Cantidad_Vendida", Paquetes.Cantidad_Vendida));
            Cmd.Parameters.Add(new SqlParameter("@Precio_Venta", Paquetes.Precio_Venta));
            Cmd.Parameters.Add(new SqlParameter("@Id_Paquete", Paquetes.Id_Paquetes));

            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método que me permite eliminar un Servicio a la base de datos 

        public void EliminarPaquete(CE_Paquetes Paquetes)
        {
            Cmd = new SqlCommand("EliminarPaquete", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Id_Paquete", Paquetes.Id_Paquetes));

            Cmd.ExecuteNonQuery();


            Con.Cerrar();
        }

        #region Métodos para buscar Paquetes

        //Método que me permite buscar un Servicio  por el codigo
        public DataTable Buscar_Paquete_Nombre(CE_Paquetes Paquetes)
        {
            dataTable = new DataTable("Nombre");
            Cmd = new SqlCommand("Buscar_Paquete_Nombre", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Paquetes.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

        //Método que me permite buscar un Servicio  por el nombre
        public DataTable Buscar_Paquete_Descripcion(CE_Paquetes Paquetes)
        {
            dataTable = new DataTable("Descripcion");
            Cmd = new SqlCommand("Buscar_Paquete_Descripcion", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Paquetes.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

        #endregion
    }
}
