using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_Servicios
    {
        private int _Id_Servicio;
        private string _Codigo;
        private string _Nombre;
        private string _Descripcion;
        private string _Precio_Venta;
        private string _Buscar;

        public int Id_Servicio { get => _Id_Servicio; set => _Id_Servicio = value; }
        public string Codigo { get => _Codigo; set => _Codigo = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public string Precio_Venta { get => _Precio_Venta; set => _Precio_Venta = value; }
        public string Buscar { get => _Buscar; set => _Buscar = value; }
    }
}
