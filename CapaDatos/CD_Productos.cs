using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System.Windows.Forms;
using System.Windows.Controls;

namespace CapaDatos
{
    public class CD_Productos
    {
        //Instanciamos nuestra conexión

        CD_Conexion Con = new CD_Conexion();

        SqlCommand Cmd;
        SqlDataAdapter Da;
        DataTable dataTable;

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
            int Existencia = 0;

            Cmd = new SqlCommand("SELECT Cantidad FROM Inventario WHERE Id_Inventario="+Productos.Id_Producto+"", Con.Abrir());
            Cmd.CommandType = CommandType.Text;

            SqlDataReader Dr = Cmd.ExecuteReader();

            if (Dr.Read())
            {
                Existencia = Convert.ToInt32(Dr["Cantidad"].ToString());
            }

            Dr.Close();

            if(Existencia != 0)
            {
                System.Windows.Forms.MessageBox.Show("El Producto contiene existencia no puede ser eliminado !!! ", "Eliminar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                return;
            }
            else
            {
                Cmd = new SqlCommand("EliminarProductos", Con.Abrir());
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.Add(new SqlParameter("@Id_Producto", Productos.Id_Producto));

                Cmd.ExecuteNonQuery();

                System.Windows.Forms.MessageBox.Show("El Producto fue eliminado Correctamente!!! ", "Eliminar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                Con.Cerrar();
            }
        }

        #region Métodos para buscar productos

        //Método que me permite buscar un producto  por el codigo
        public DataTable Buscar_Producto_Codigo(CE_Productos Productos)
        {
            dataTable = new DataTable("Codigo");
            Cmd = new SqlCommand("Buscar_Producto_Codigo", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Productos.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

        //Método que me permite buscar un producto  por el nombre
        public DataTable Buscar_Producto_Nombre(CE_Productos Productos)
        {
            dataTable = new DataTable("Nombre");
            Cmd = new SqlCommand("Buscar_Producto_Nombre", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Productos.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

        //Método que me permite buscar un producto  por la descripcion
        public DataTable Buscar_Producto_Descripcion(CE_Productos Productos)
        {
            dataTable = new DataTable("Descripcion");
            Cmd = new SqlCommand("Buscar_Producto_Descripcion", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Productos.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }


        #endregion

    }
}
