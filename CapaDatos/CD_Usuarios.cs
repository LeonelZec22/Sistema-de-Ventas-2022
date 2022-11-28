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
    public class CD_Usuarios
    {
        CD_Conexion Con = new CD_Conexion();

        SqlCommand Cmd;
        //SqlDataAdapter Da;
        //DataTable dataTable;

        //Método que me permite agregar un usuario a la base de datos 

        public void AgregarUsuario(CE_Usuarios Usuarios)
        {
            Cmd = new SqlCommand("SP_Agregar_Usuario", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Usuarios.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Apellido", Usuarios.Apellido));
            Cmd.Parameters.Add(new SqlParameter("@Usuario", Usuarios.Usuario));
            Cmd.Parameters.Add(new SqlParameter("@Correo", Usuarios.Correo));
            Cmd.Parameters.Add(new SqlParameter("@Password", Usuarios.Password));

            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método que me permite Editar un usuario a la base de datos 

        public void EditarUsuario(CE_Usuarios Usuarios)
        {
            Cmd = new SqlCommand("SP_Editar_Usuario", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Usuarios.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Apellido", Usuarios.Apellido));
            Cmd.Parameters.Add(new SqlParameter("@Usuario", Usuarios.Usuario));
            Cmd.Parameters.Add(new SqlParameter("@Password", Usuarios.Password));
            Cmd.Parameters.Add(new SqlParameter("@Correo", Usuarios.Correo));
            Cmd.Parameters.Add(new SqlParameter("@IdUsuario", Usuarios.Id_Usuario));

            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método que me permite eliminar un usuario a la base de datos 

        public void EliminarUsuario(CE_Usuarios Usuarios)
        {

            Cmd = new SqlCommand("SP_Eliminar_Usuario", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Id_Usuario", Usuarios.Id_Usuario));

            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

       
    }
}
