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
    public class CDo_Ventas_Servicios
    {
        CD_Ventas_Servicios objVentas = new CD_Ventas_Servicios();

        public void AgregarVentaServicios(CE_Ventas_Servicios Ventas)
        {
            objVentas.AgregarVentaServicios(Ventas);
        }

        public void AnularVentaServicios(CE_Ventas_Servicios Ventas)
        {

            objVentas.AnularVentaServicios(Ventas);
        }

        public DataTable MostrarVentasServicios()
        {
            return objVentas.MostrarVentasServicios();
        }

        public DataTable Buscar_VentaReserva_Nombre(CE_Ventas_Servicios Ventas)
        {
            return objVentas.Buscar_VentaReserva_Nombre(Ventas);
        }

        public DataTable Buscar_VentaReserva_Estado(CE_Ventas_Servicios Ventas)
        {
            return objVentas.Buscar_VentaReserva_Estado(Ventas);
        }
    }
}
