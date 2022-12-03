using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_Detalle_Venta_Servicios
    {
        private int _Id_DetalleVenta_Servicios;

        private int _Id_Venta_Servicios;

        private int _Id_Reserva;

        private DateTime _Fecha_Reserva;

        private string _Estado;

        private decimal _Descuento;

        private decimal _Monto_Total;

        public int Id_DetalleVenta_Servicios { get => _Id_DetalleVenta_Servicios; set => _Id_DetalleVenta_Servicios = value; }
        public int Id_Venta_Servicios { get => _Id_Venta_Servicios; set => _Id_Venta_Servicios = value; }
        public int Id_Reserva { get => _Id_Reserva; set => _Id_Reserva = value; }
        public DateTime Fecha_Reserva { get => _Fecha_Reserva; set => _Fecha_Reserva = value; }
        public string Estado { get => _Estado; set => _Estado = value; }
        public decimal Descuento { get => _Descuento; set => _Descuento = value; }
        public decimal Monto_Total { get => _Monto_Total; set => _Monto_Total = value; }
    }
}
