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
    public class CDo_Venta_Paquetes
    {
        CD_Ventas_Paquetes objVentas = new CD_Ventas_Paquetes();

        public void AgregarVentaPaquetes(CE_Ventas_Paquetes Ventas)
        {
            objVentas.AgregarVentaPaquetes(Ventas);
        }

        //Método para anular el ingreso de un producto por ejemplo se cancelo el envio de un producto o se devuelve un producto

        public void AnularVentaPaquetes(CE_Ventas_Paquetes Ventas)
        {

            objVentas.AnularVentaPaquetes(Ventas);
        }

        public void EditarVentaPaquetes(CE_Ventas_Paquetes Ventas)
        {
            objVentas.EditarVentaPaquetes(Ventas);
        }
        public DataTable MostrarVentasPaquetes()
        {
            return objVentas.MostrarVentasPaquetes();
        }

        public DataTable Buscar_VentaPaquete_Cliente(CE_Ventas_Paquetes Ventas)
        {
            return objVentas.Buscar_VentaPaquete_Cliente(Ventas);
        }

        public DataTable Buscar_VentaPaquete_Estado(CE_Ventas_Paquetes Ventas)
        {
            return objVentas.Buscar_VentaPaquete_Estado(Ventas);
        }

        public DataTable Buscar_VentaPaquete_Paquete(CE_Ventas_Paquetes Ventas)
        {
            return objVentas.Buscar_VentaPaquete_Paquete(Ventas);
        }


    }
}
