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
    public class CDo_Productos
    {
        CD_Productos ObjProductos = new CD_Productos();

        //Método que me permite agregar un producto a la base de datos

        public void AgregarProducto(CE_Productos Productos)
        {
            ObjProductos.AgregarProducto(Productos);
        }

        //Método que me permite editar un producto a la base de datos

        public void EditarProducto(CE_Productos Productos)
        {
            ObjProductos.EditarProducto(Productos);
        }

        //Método que me permite eliminar un producto a la base de datos

        public void EliminarProducto(CE_Productos Productos)
        {
            ObjProductos.EliminarProducto(Productos);
        }


        #region Buscar productos
        //Método que me permite buscar un producto por codigo

        public DataTable Buscar_Producto_Codigo(CE_Productos Productos)
        {
           return ObjProductos.Buscar_Producto_Codigo(Productos);
        }

        //Método que me permite buscar un producto por nombre

        public DataTable Buscar_Producto_Nombre(CE_Productos Productos)
        {
            return ObjProductos.Buscar_Producto_Nombre(Productos);
        }

        //Método que me permite buscar un producto por descripcion

        public DataTable Buscar_Producto_Descripcion(CE_Productos Productos)
        {
            return ObjProductos.Buscar_Producto_Descripcion(Productos);
        }

        #endregion
    }
}
