using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocio
{
    public class CDo_Detalle_Reservas
    {
        CD_Detalle_Reservas objDetalleReservas = new CD_Detalle_Reservas();


        public void AgregarDetalleReserva(CE_Detalle_Reservas DetallesReserva)
        {
            objDetalleReservas.AgregarDetalleReserva(DetallesReserva);
        }

        //Método para anular el ingreso de un producto por ejemplo se cancelo el envio de un producto o se devuelve un producto

        public void AnularDetalleReserva(CE_Detalle_Reservas DetallesReserva)
        {
            objDetalleReservas.AnularDetalleReserva(DetallesReserva);
        }

        public DataTable MostrarReserva(int Id_Reserva)
        {
            return objDetalleReservas.MostrarReserva(Id_Reserva);
        }
    }
}
