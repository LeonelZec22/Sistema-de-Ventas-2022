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
    public class CDo_Ventas
    {
        CD_Ventas objVentas = new CD_Ventas();

        public void AgregarVenta(CE_Ventas Ventas)
        {
            objVentas.AgregarVenta(Ventas);
        }

        //Método para anular el ingreso de un producto por ejemplo se cancelo el envio de un producto o se devuelve un producto

        public void AnularVenta(CE_Ventas Ventas)
        {
            objVentas.AnularVenta(Ventas);

        }


        //Método para mostrar los productos ingresos 
        public DataTable MostrarVentas()
        {
            return objVentas.MostrarVentas();
        }

        public DataTable Mostrar_Ingreso_Ventas()
        {
            return objVentas.Mostrar_Ingreso_Ventas();
        }

        
    }
}
