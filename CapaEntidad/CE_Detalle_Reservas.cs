using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_Detalle_Reservas
    {
        private int _Id_DetalleReserva;
        private int _Id_Reserva;
        private int _Id_Servicios;
        private string _Estado;
        private decimal _Descuento;
        private decimal _Monto_Total;

        public int Id_DetalleReserva { get => _Id_DetalleReserva; set => _Id_DetalleReserva = value; }
        public int Id_Reserva { get => _Id_Reserva; set => _Id_Reserva = value; }
        public int Id_Servicios { get => _Id_Servicios; set => _Id_Servicios = value; }
        public string Estado { get => _Estado; set => _Estado = value; }
        public decimal Descuento { get => _Descuento; set => _Descuento = value; }
        public decimal Monto_Total { get => _Monto_Total; set => _Monto_Total = value; }
    }
}
