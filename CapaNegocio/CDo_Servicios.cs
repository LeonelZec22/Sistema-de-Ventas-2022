using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaNegocio
{
    public class CDo_Servicios
    {
        CD_Servicios ObjServicios = new CD_Servicios();

        //Método que me permite agregar un Servicios a la base de datos

        public void AgregarServicio(CE_Servicios Servicios)
        {
            ObjServicios.AgregarServicios(Servicios);
        }

        //Método que me permite editar un Servicios a la base de datos

        public void EditarServicio(CE_Servicios Servicios)
        {
            ObjServicios.EditarServicios(Servicios);
        }

        //Método que me permite eliminar un Servicios a la base de datos

        public void EliminarServicio(CE_Servicios Servicios)
        {
            ObjServicios.EliminarServicio(Servicios);
        }


        #region Buscar Servicios
        //Método que me permite buscar un Servicios por codigo

        public DataTable Buscar_Servicio_Codigo(CE_Servicios Servicios)
        {
            return ObjServicios.Buscar_Servicio_Codigo(Servicios);
        }

        //Método que me permite buscar un Servicios por nombre

        public DataTable Buscar_Servicios_Nombre(CE_Servicios Servicios)
        {
            return ObjServicios.Buscar_Servicio_Nombre(Servicios);
        }



        #endregion
    }
}
