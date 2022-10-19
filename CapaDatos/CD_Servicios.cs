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
    public class CD_Servicios
    {

        //Instanciamos nuestra conexión

        CD_Conexion Con = new CD_Conexion();

        SqlCommand Cmd;
        SqlDataAdapter Da;
        DataTable dataTable;

        //Método que me permite agregar un Servicios a la base de datos 

        public void AgregarServicios(CE_Servicios Servicios)
        {
            Cmd = new SqlCommand("AgregarServicios", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Codigo", Servicios.Codigo));
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Servicios.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Descripcion", Servicios.Descripcion));
            Cmd.Parameters.Add(new SqlParameter("@Precio_Venta", Servicios.Precio_Venta));

            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método que me permite Editar un Servicios a la base de datos 

        public void EditarServicios(CE_Servicios Servicios)
        {
            Cmd = new SqlCommand("EditarServicios", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Codigo", Servicios.Codigo));
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Servicios.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Descripcion", Servicios.Descripcion));
            Cmd.Parameters.Add(new SqlParameter("@Precio_Venta", Servicios.Precio_Venta));
            Cmd.Parameters.Add(new SqlParameter("@Id_Servicio", Servicios.Id_Servicio));

            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método que me permite eliminar un Servicio a la base de datos 

        public void EliminarServicio(CE_Servicios Servicios)
        {
            Cmd = new SqlCommand("EliminarServicio", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Id_Servicio", Servicios.Id_Servicio));

            Cmd.ExecuteNonQuery();


            Con.Cerrar();
        }

        #region Métodos para buscar Clientes

        //Método que me permite buscar un Servicio  por el codigo
        public DataTable Buscar_Servicio_Codigo(CE_Servicios Servicios)
        {
            dataTable = new DataTable("Codigo");
            Cmd = new SqlCommand("Buscar_Servicio_Codigo", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Servicios.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

        //Método que me permite buscar un Servicio  por el nombre
        public DataTable Buscar_Servicio_Nombre(CE_Servicios Servicios)
        {
            dataTable = new DataTable("Nombre");
            Cmd = new SqlCommand("Buscar_Servicio_Nombre", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Servicios.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

        #endregion
    }
}
