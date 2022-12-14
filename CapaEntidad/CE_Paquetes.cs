using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_Paquetes
    {
        private int _Id_Paquetes;
        private string _Codigo;
        private string _Nombre;
        private string _Descripcion;
        private int _Cantidad_Vendida;
        private decimal _Precio_Venta;
        private string _Buscar;

        public int Id_Paquetes { get => _Id_Paquetes; set => _Id_Paquetes = value; }
        public string Codigo { get => _Codigo; set => _Codigo = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public decimal Precio_Venta { get => _Precio_Venta; set => _Precio_Venta = value; }
        public string Buscar { get => _Buscar; set => _Buscar = value; }
        public int Cantidad_Vendida { get => _Cantidad_Vendida; set => _Cantidad_Vendida = value; }
    }
}
