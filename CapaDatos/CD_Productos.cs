using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Productos
    {
        //Instanciamos nuestra conexión

        CD_Conexion Con = new CD_Conexion();

        SqlCommand Cmd;

        //Método que me permite agregar un producto a la base de datos 

        public void AgregarProducto(CE_Productos Productos)
        {
            Cmd = new SqlCommand("AgregarProductos", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Codigo", Productos.Codigo));
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Productos.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Descripcion", Productos.Descripcion));
            Cmd.Parameters.Add(new SqlParameter("@Precio_Venta", Productos.Precio_Venta));
            Cmd.Parameters.Add(new SqlParameter("@Costo_Unitario", Productos.Costo_Unitario));

            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método que me permite Editar un producto a la base de datos 

        public void EditarProducto(CE_Productos Productos)
        {
            Cmd = new SqlCommand("EditarProductos", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Codigo", Productos.Codigo));
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Productos.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Descripcion", Productos.Descripcion));
            Cmd.Parameters.Add(new SqlParameter("@Precio_Venta", Productos.Precio_Venta));
            Cmd.Parameters.Add(new SqlParameter("@Costo_Unitario", Productos.Costo_Unitario));
            Cmd.Parameters.Add(new SqlParameter("@Id_Producto", Productos.Id_Producto));

            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método que me permite eliminar un producto a la base de datos 

        public void EliminarProducto(CE_Productos Productos)
        {
            Cmd = new SqlCommand("EliminarProductos", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Id_Producto", Productos.Id_Producto));

            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }


    }
}
