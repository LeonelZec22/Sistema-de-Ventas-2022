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
    public class CDo_Reservas
    {
        CD_Reservas objReservas = new CD_Reservas();

        //Método para agregar el ingreso de una reserva

        public void AgregarReserva(CE_Reservas Reservas)
        {
            objReservas.AgregarReserva(Reservas);
        }

        //Método para anular el ingreso de una reseva por ejemplo se cancelo la reserva porque se movio para otro día

        public void AnularReserva(CE_Reservas Reservas)
        {
            objReservas.AnularReserva(Reservas);
        }


        //Método para mostrar las reservas de los servicios
        public DataTable MostrarReservas()
        {
            return objReservas.MostrarReservas();
        }
    }
}
