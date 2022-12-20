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
    public class CDo_Paquetes
    {
        CD_Paquetes ObjPaquetes = new CD_Paquetes();

        //Método que me permite agregar un Servicios a la base de datos

        public void AgregarPaquete(CE_Paquetes Paquetes)
        {
            ObjPaquetes.AgregarPaquetes(Paquetes);
        }

        //Método que me permite editar un Servicios a la base de datos

        public void EditarPaquete(CE_Paquetes Paquetes)
        {
            ObjPaquetes.EditarPaquete(Paquetes);
        }

        //Método que me permite eliminar un Servicios a la base de datos

        public void EliminarPaquete(CE_Paquetes Paquetes)
        {
            ObjPaquetes.EliminarPaquete(Paquetes);
        }


        #region Buscar Servicios
        //Método que me permite buscar un Servicios por codigo

        public DataTable Buscar_Paquete_Nombre(CE_Paquetes Paquetes)
        {
            return ObjPaquetes.Buscar_Paquete_Nombre(Paquetes);
        }

        //Método que me permite buscar un Servicios por nombre

        public DataTable Buscar_Paquete_Descripcion(CE_Paquetes Paquetes)
        {
            return ObjPaquetes.Buscar_Paquete_Descripcion(Paquetes);
        }



        #endregion
    }
}
