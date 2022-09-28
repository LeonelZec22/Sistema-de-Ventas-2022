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
    public class CD_Proveedores
    {
        //Instanciamos nuestra conexión

        CD_Conexion Con = new CD_Conexion();

        SqlCommand Cmd;
        SqlDataAdapter Da;
        DataTable dataTable;

        //Método que me permite agregar un Proveedor a la base de datos 

        public void AgregarProveedor(CE_Proveedores Proveedores)
        {
            Cmd = new SqlCommand("AgregarProveedor", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Codigo", Proveedores.Codigo));
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Proveedores.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Direccion", Proveedores.Direccion));
            Cmd.Parameters.Add(new SqlParameter("@Telefono", Proveedores.Telefono));

            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método que me permite Editar un Proveedor a la base de datos 

        public void EditarProveedor(CE_Proveedores Proveedores)
        {
            Cmd = new SqlCommand("EditarProveedor", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Codigo", Proveedores.Codigo));
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Proveedores.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Direccion", Proveedores.Direccion));
            Cmd.Parameters.Add(new SqlParameter("@Telefono", Proveedores.Telefono));
            Cmd.Parameters.Add(new SqlParameter("@Id_Proveedor", Proveedores.Id_Proveedor));

            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método que me permite eliminar un Proveedor a la base de datos 

        public void EliminarProveedor(CE_Proveedores Proveedores)
        {
            Cmd = new SqlCommand("EliminarProveedor", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Id_Proveedor", Proveedores.Id_Proveedor));

            Cmd.ExecuteNonQuery();
            

            Con.Cerrar();
        }

        #region Métodos para buscar productos

        //Método que me permite buscar un Proveedor  por el codigo
        public DataTable Buscar_Proveedor_Codigo(CE_Proveedores Proveedores)
        {
            dataTable = new DataTable("Codigo");
            Cmd = new SqlCommand("Buscar_Proveedor_Codigo", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Proveedores.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

        //Método que me permite buscar un Proveedor  por el nombre
        public DataTable Buscar_Proveedor_Nombre(CE_Proveedores Proveedores)
        {
            dataTable = new DataTable("Nombre");
            Cmd = new SqlCommand("Buscar_Proveedor_Nombre", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Proveedores.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

        #endregion

    }
}
