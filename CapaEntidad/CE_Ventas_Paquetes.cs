using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_Ventas_Paquetes
    {
        private int _Id_Venta_Paquetes;
        private int _Id_Cliente;
        private int _Id_Paquetes;
        private string _Cliente;
        private string _Paquete;
        private DateTime _Fecha_Venta;
        private int _Cantidad_Vendida;
        private int _Cantidad_Usada;
        private string _Estado;
        private decimal _Precio_Venta;
        private string _Buscar;

        public int Id_Venta_Paquetes { get => _Id_Venta_Paquetes; set => _Id_Venta_Paquetes = value; }
        public int Id_Cliente { get => _Id_Cliente; set => _Id_Cliente = value; }
        public int Id_Paquetes { get => _Id_Paquetes; set => _Id_Paquetes = value; }
        public DateTime Fecha_Venta { get => _Fecha_Venta; set => _Fecha_Venta = value; }
        public int Cantidad_Vendida { get => _Cantidad_Vendida; set => _Cantidad_Vendida = value; }
        public int Cantidad_Usada { get => _Cantidad_Usada; set => _Cantidad_Usada = value; }
        public string Estado { get => _Estado; set => _Estado = value; }
        public decimal Precio_Venta { get => _Precio_Venta; set => _Precio_Venta = value; }
        public string Buscar { get => _Buscar; set => _Buscar = value; }
        public string Cliente { get => _Cliente; set => _Cliente = value; }
        public string Paquete { get => _Paquete; set => _Paquete = value; }
    }
}
