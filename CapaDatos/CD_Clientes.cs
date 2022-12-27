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
    public class CD_Clientes
    {
        //Instanciamos nuestra conexión

        CD_Conexion Con = new CD_Conexion();

        SqlCommand Cmd;
        SqlDataAdapter Da;
        DataTable dataTable;

        //Método que me permite agregar un Cliente a la base de datos 

        public void AgregarCliente(CE_Clientes Clientes)
        {
            Cmd = new SqlCommand("AgregarCliente", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Codigo", Clientes.Codigo));
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Clientes.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Telefono", Clientes.Telefono));
            Cmd.Parameters.Add(new SqlParameter("@Email", Clientes.Email));
            Cmd.Parameters.Add(new SqlParameter("@Estado", Clientes.Estado));

            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método que me permite Editar un Cliente a la base de datos 

        public void EditarCliente(CE_Clientes Clientes)
        {
            Cmd = new SqlCommand("EditarCliente", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Codigo", Clientes.Codigo));
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Clientes.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Email", Clientes.Email));
            Cmd.Parameters.Add(new SqlParameter("@Telefono", Clientes.Telefono));
            Cmd.Parameters.Add(new SqlParameter("@Estado", Clientes.Estado));
            Cmd.Parameters.Add(new SqlParameter("@Id_Cliente", Clientes.Id_Cliente));

            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método que me permite eliminar un Cliente a la base de datos 

        public void EliminarCliente(CE_Clientes Clientes)
        {
            Cmd = new SqlCommand("EliminarCliente", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Id_Cliente", Clientes.Id_Cliente));

            Cmd.ExecuteNonQuery();


            Con.Cerrar();
        }

        #region Métodos para buscar Clientes

        //Método que me permite buscar un Cliente  por el codigo
        public DataTable Buscar_Cliente_Codigo(CE_Clientes Clientes)
        {
            dataTable = new DataTable("Codigo");
            Cmd = new SqlCommand("Buscar_Cliente_Codigo", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Clientes.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

        //Método que me permite buscar un Cliente  por el nombre
        public DataTable Buscar_Cliente_Nombre(CE_Clientes Clientes)
        {
            dataTable = new DataTable("Nombre");
            Cmd = new SqlCommand("Buscar_Cliente_Nombre", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Clientes.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

        #endregion

    }
}
