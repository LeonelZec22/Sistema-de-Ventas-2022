using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;
using System.Data;


namespace CapaNegocio
{
    public class CDo_Usuarios
    {
        CD_Usuarios ObjUsuarios = new CD_Usuarios();


        public void AgregarUsuario(CE_Usuarios Usuarios)
        {
            ObjUsuarios.AgregarUsuario(Usuarios);
        }

        //Método que me permite Editar un usuario a la base de datos 

        public void EditarUsuario(CE_Usuarios Usuarios)
        {
            ObjUsuarios.EditarUsuario(Usuarios);
        }

        //Método que me permite eliminar un usuario a la base de datos 

        public void EliminarUsuario(CE_Usuarios Usuarios)
        {
            ObjUsuarios.EliminarUsuario(Usuarios);
        }
    }
}
