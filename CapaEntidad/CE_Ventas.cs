using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_Ventas
    {
        private int _Id_Venta;  
        private int _Id_Cliente;  
        private DateTime _Fecha_Venta;
        private decimal _Sub_Total;
        private decimal _Descuento;
        private decimal _Monto_Total; 
        private string _Estado;
        private int _Id_Usuario;

        public int Id_Venta { get => _Id_Venta; set => _Id_Venta = value; }
        public int Id_Cliente { get => _Id_Cliente; set => _Id_Cliente = value; }
        public DateTime Fecha_Venta { get => _Fecha_Venta; set => _Fecha_Venta = value; }
        public decimal Sub_Total { get => _Sub_Total; set => _Sub_Total = value; }
        public decimal Descuento { get => _Descuento; set => _Descuento = value; }
        public decimal Monto_Total { get => _Monto_Total; set => _Monto_Total = value; }
        public string Estado { get => _Estado; set => _Estado = value; }
        public int Id_Usuario { get => _Id_Usuario; set => _Id_Usuario = value; }
    }
}
