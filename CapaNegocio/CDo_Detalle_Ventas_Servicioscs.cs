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
    public class CDo_Detalle_Ventas_Servicioscs
    {
        CD_Detalle_Ventas_Servicios objDetalleVentas = new CD_Detalle_Ventas_Servicios();

        public void AgregarDetalleVentaServicios(CE_Detalle_Venta_Servicios Detalles)
        {
            objDetalleVentas.AgregarDetalleVentaServicios(Detalles);
        }

        public void AnularDetalleVentaServicios(CE_Detalle_Venta_Servicios Detalles)
        {

            objDetalleVentas.AnularDetalleVentaServicios(Detalles);
        }

        public DataTable MostrarVentasServicios(int Id_Venta_Servicios)
        {
            return objDetalleVentas.MostrarVentasServicios(Id_Venta_Servicios);
        }
    }
}
