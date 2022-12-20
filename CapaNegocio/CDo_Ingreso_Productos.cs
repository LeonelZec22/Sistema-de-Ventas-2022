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
    public class CDo_Ingreso_Productos
    {
        CD_Ingreso_Productos objIngresoProducto = new CD_Ingreso_Productos();

        //Método para agregar el ingreso de un producto

        public void AgregarIngreso(CE_Ingreso_Productos Ingresos)
        {
            objIngresoProducto.AgregarIngreso(Ingresos);
        }

        //Método para anular el ingreso de un producto por ejemplo se cancelo el envio de un producto o se devuelve un producto

        public void AnularIngreso(CE_Ingreso_Productos Ingresos)
        {
            objIngresoProducto.AnularIngreso(Ingresos);
        }


        //Método para mostrar los productos ingresos 
        public DataTable MostrarIngresoProductos()
        {
            return objIngresoProducto.MostrarIngresoProductos();
        }

        public DataTable Buscar_Ingreso_Proveedor(CE_Ingreso_Productos Ingresos)
        {

            return objIngresoProducto.Buscar_Ingreso_Proveedor(Ingresos);
        }

        public DataTable Buscar_Ingreso_Fecha(CE_Ingreso_Productos Ingresos)
        {
            return objIngresoProducto.Buscar_Ingreso_Fecha(Ingresos);

        }
    }
}
