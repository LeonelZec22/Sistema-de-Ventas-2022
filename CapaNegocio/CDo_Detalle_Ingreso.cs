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
    class CDo_Detalle_Ingreso
    {
        CD_Detalle_Ingreso objDetalleIngreso = new CD_Detalle_Ingreso();

        public void AgregarDetalleIngreso(CE_Detalle_Ingreso Detalles)
        {
            objDetalleIngreso.AgregarDetalleIngreso(Detalles);
        }

        public void AnularDetalleIngreso(CE_Detalle_Ingreso Detalles)
        {
            objDetalleIngreso.AnularDetalleIngreso(Detalles);
        }
    }
}
