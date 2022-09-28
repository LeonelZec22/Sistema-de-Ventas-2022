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
    public class CDo_Proveedores
    {
        CD_Proveedores ObjProveedores = new CD_Proveedores();

        //Método que me permite agregar un Proveedor a la base de datos

        public void AgregarProveedor(CE_Proveedores Proveedores)
        {
            ObjProveedores.AgregarProveedor(Proveedores);
        }

        //Método que me permite editar un Proveedor a la base de datos

        public void EditarProveedor(CE_Proveedores Proveedores)
        {
            ObjProveedores.EditarProveedor(Proveedores);
        }

        //Método que me permite eliminar un Proveedor a la base de datos

        public void EliminarProveedor(CE_Proveedores Proveedores)
        {
            ObjProveedores.EliminarProveedor(Proveedores);
        }


        #region Buscar productos
        //Método que me permite buscar un Proveedor por codigo

        public DataTable Buscar_Proveedor_Codigo(CE_Proveedores Proveedores)
        {
            return ObjProveedores.Buscar_Proveedor_Codigo(Proveedores);
        }

        //Método que me permite buscar un Proveedor por nombre

        public DataTable Buscar_Proveedor_Nombre(CE_Proveedores Proveedores)
        {
            return ObjProveedores.Buscar_Proveedor_Nombre(Proveedores);
        }

       

        #endregion
    }
}
