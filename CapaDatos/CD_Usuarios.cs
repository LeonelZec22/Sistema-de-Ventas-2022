using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using CapaEntidad.Caches;
using System.Windows.Forms;
using System.Windows.Controls;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        CD_Conexion Con = new CD_Conexion();

        SqlCommand Cmd;
        SqlDataAdapter Da;
        SqlDataReader Dr;
        DataTable Dt;

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
            int Id = 0;

            Cmd = new SqlCommand("Select Id_Usuario From Usuarios where Id_Usuario=" + Usuarios.Id_Usuario + "", Con.Abrir());
            Cmd.CommandType = CommandType.Text;
            SqlDataReader dr = Cmd.ExecuteReader();

            if (dr.Read())
            {
                Id = Convert.ToInt32(dr["Id_Usuario"].ToString());
            }

            dr.Close();
            Con.Cerrar();

            if (Convert.ToString(Id) == "1")
            {
                System.Windows.Forms.MessageBox.Show("Este Usuario no se puede editar del sistema", "Editar Usuario", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

            else
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

                System.Windows.Forms.MessageBox.Show("Usuario Editado exitosamente!!! ", "Editar Usuario", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }

           
        }

        //Método que me permite eliminar un usuario a la base de datos 

        public void EliminarUsuario(CE_Usuarios Usuarios)
        {
            int Id = 0;

            Cmd = new SqlCommand("Select Id_Usuario From Usuarios where Id_Usuario=" + Usuarios.Id_Usuario + "", Con.Abrir());
            Cmd.CommandType = CommandType.Text;
            SqlDataReader dr = Cmd.ExecuteReader();

            if (dr.Read())
            {
                Id = Convert.ToInt32(dr["Id_Usuario"].ToString());
            }

            dr.Close();
            Con.Cerrar();

            if (Convert.ToString(Id) == "1")
            {
                System.Windows.Forms.MessageBox.Show("Este Usuario no se puede eliminar del sistema", "Eliminar Usuario", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

            else
            {

                Cmd = new SqlCommand("SP_Eliminar_Usuario", Con.Abrir());
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.Add(new SqlParameter("@Id_Usuario", Usuarios.Id_Usuario));

                Cmd.ExecuteNonQuery();

                Con.Cerrar();

                System.Windows.Forms.MessageBox.Show("Registro Eliminado correctamente!!! ", "Eliminar Usuario", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            
        }

       //Método que permite Accesar al Sistema 

        public DataTable LoginUsuario(CE_Usuarios Usuarios)
        {
            Dt = new DataTable("Login Usuario");

            Cmd = new SqlCommand("SP_LoginUsuarios", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Usuario", Usuarios.Usuario));
            Cmd.Parameters.Add(new SqlParameter("@Password", Usuarios.Password));

            Da = new SqlDataAdapter(Cmd);

            Da.Fill(Dt);

            Con.Cerrar();

            return Dt;
        }

        public void DatosUsuario(string Usuario)
        {
            DataTable Dt = new DataTable("Datos Usuarios");

            Cmd = new SqlCommand("SP_DatosUsuario", Con.Abrir());

            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));

            Dr = Cmd.ExecuteReader();
            if (Dr.Read())
            {
                InformacionUsuario.IdUsuario = Dr.GetInt32(0);
                InformacionUsuario.Nombre = Dr.GetString(1);
                InformacionUsuario.Apellido = Dr.GetString(2);
                InformacionUsuario.Usuario = Dr.GetString(3);
                InformacionUsuario.Correo = Dr.GetString(4);
                InformacionUsuario.Password = Dr.GetString(5);
            }

            Dr.Close();

            Con.Cerrar();
        }



        public List<CE_Usuarios> comprobarCorreo(string regcorreo)
        {
           
            SqlDataReader reader;
            List<CE_Usuarios> usuario = new List<CE_Usuarios>();

            try
            {
                string consulta = "SELECT Id_Usuario, Correo FROM Usuarios WHERE Correo = " + regcorreo + "";
                SqlCommand comando = new SqlCommand(consulta);
                Con.Abrir();
                reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    CE_Usuarios reg = new CE_Usuarios
                    {
                        Id_Usuario = Convert.ToInt32(reader["Id_Usuario"]),
                        Correo = Convert.ToString(reader["Correo"])
                    };
                    usuario.Add(reg);
                }
                return usuario;
            }

            catch (Exception ex)
            {
                return null;
            }

            finally
            {
                Con.Cerrar();
            }
        }



        public List<CE_Usuarios> comprobarUsuario(string regnom)
        {
            
            SqlDataReader reader;
            List<CE_Usuarios> usuario = new List<CE_Usuarios>();

            try
            {

                //string consulta = "SELECT * FROM Usuarios WHERE Usuario = " + regnom + "";
                //SqlCommand comando = new SqlCommand(consulta);
                //Con.Abrir();

                Cmd = new SqlCommand("Select * FROM Usuarios WHERE Usuario = '" + regnom + "'", Con.Abrir());
                Cmd.CommandType = CommandType.Text;
                reader = Cmd.ExecuteReader();

                if (reader.Read())
                {
                    CE_Usuarios reg = new CE_Usuarios
                    {
                        Id_Usuario = Convert.ToInt32(reader["Id_Usuario"]),
                        Nombre = Convert.ToString(reader["Nombre"]),
                        Apellido = Convert.ToString(reader["Apellido"]),
                        Usuario = Convert.ToString(reader["Usuario"]),
                        Correo = Convert.ToString(reader["Correo"]),
                        Password = Convert.ToString(reader["Password"])
                    };
                    usuario.Add(reg);
                }
                return usuario;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

            finally
            {
                Con.Cerrar();
            }
        }
    }
}
