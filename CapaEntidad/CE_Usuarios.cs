using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_Usuarios
    {
        int _Id_Usuario;
        string _Nombre;
        string _Apellido;
        string _Usuario;
        string _Correo;
        string _Password;

        public int Id_Usuario { get => _Id_Usuario; set => _Id_Usuario = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Apellido { get => _Apellido; set => _Apellido = value; }
        public string Usuario { get => _Usuario; set => _Usuario = value; }
        public string Correo { get => _Correo; set => _Correo = value; }
        public string Password { get => _Password; set => _Password = value; }
    }
}
