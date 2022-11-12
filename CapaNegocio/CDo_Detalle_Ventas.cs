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
    public class CDo_Detalle_Ventas
    {

        CD_Detalle_Ventas objDetalleVentas = new CD_Detalle_Ventas();

        public void AgregarDetalleVenta(CE_Detalle_Ventas Detalles)
        {
            objDetalleVentas.AgregarDetalleVenta(Detalles);
        }

        //Método para anular el ingreso de un producto por ejemplo se cancelo el envio de un producto o se devuelve un producto

        public void AnularDetalleVenta(CE_Detalle_Ventas Detalles)
        {
            objDetalleVentas.AnularDetalleVenta(Detalles);
        }

        public DataTable MostrarVentas(int Id_Venta)
        {
            return objDetalleVentas.MostrarVentas(Id_Venta);
        }

    }
}
