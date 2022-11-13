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
    public class CD_Ingreso_Productos
    {
        CD_Conexion Con = new CD_Conexion();

        private SqlCommand Cmd;

        //Método para agregar el ingreso de un producto

        public void AgregarIngreso(CE_Ingreso_Productos Ingresos)
        {
            Cmd = new SqlCommand("Agregar_Ingreso_Productos", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@No_Ingreso", Ingresos.No_Ingreso));
            Cmd.Parameters.Add(new SqlParameter("@ID_Proveedor", Ingresos.Id_Proveedor));
            Cmd.Parameters.Add(new SqlParameter("@Fecha_Ingreso", Ingresos.Fecha_Ingreso));
            Cmd.Parameters.Add(new SqlParameter("@Monto_Total", Ingresos.Monto_total));
            Cmd.Parameters.Add(new SqlParameter("@Estado", Ingresos.Estado));
            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método para anular el ingreso de un producto por ejemplo se cancelo el envio de un producto o se devuelve un producto

        public void AnularIngreso(CE_Ingreso_Productos Ingresos)
        {
            try
            {

                string Estado = string.Empty;
                Cmd = new SqlCommand("Select Estado From Ingreso_Productos Where Id_IngresoProducto=" + Ingresos.Id_IngresoProducto + "", Con.Abrir());
                Cmd.CommandType = CommandType.Text;
                SqlDataReader dr = Cmd.ExecuteReader();

                if (dr.Read())
                {
                    Estado = dr["Estado"].ToString();
                }

                dr.Close();
                Con.Cerrar();

                if (Estado== "Cancelado")
                {
                    System.Windows.Forms.MessageBox.Show("Esta Compra ya ha sido Anulada anteriormente", "Cancelar Ingreso Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {

                    Cmd = new SqlCommand("Anular_Ingreso_Productos", Con.Abrir());
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.Add(new SqlParameter("@Id_IngresoProducto", Ingresos.Id_IngresoProducto));
                    Cmd.Parameters.Add(new SqlParameter("@No_Ingreso", Ingresos.No_Ingreso));
                    Cmd.Parameters.Add(new SqlParameter("@ID_Proveedor", Ingresos.Id_Proveedor));
                    Cmd.Parameters.Add(new SqlParameter("@Fecha_Ingreso", Ingresos.Fecha_Ingreso));
                    Cmd.Parameters.Add(new SqlParameter("@Monto_Total", Ingresos.Monto_total));
                    Cmd.Parameters.Add(new SqlParameter("@Estado", Ingresos.Estado));
                    Cmd.ExecuteNonQuery();
                    System.Windows.Forms.MessageBox.Show("La Compra fue anulada correctamente", "Cancelar Ingreso Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    Con.Cerrar();
                }

            }

            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se puedo anular la comprar por: " + ex, "Cancelar Ingreso Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }


        //Método para mostrar los productos ingresos 
        public DataTable MostrarIngresoProductos()
        {
            DataTable Dt = new DataTable("Ingreso_Productos");
            Cmd = new SqlCommand("Mostrar_Ingreso", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader Dr = Cmd.ExecuteReader();
            Dt.Load(Dr);

            Con.Cerrar();

            Dr.Close();

            return Dt;
        }

    }
}
