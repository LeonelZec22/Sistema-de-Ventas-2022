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
    public class CD_Detalle_Ventas
    {
        CD_Conexion Con = new CD_Conexion();

        private SqlCommand Cmd;

        public void AgregarDetalleVenta(CE_Detalle_Ventas Detalles)
        {
            Cmd = new SqlCommand("AgregarDetalleVenta", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Id_Venta", Detalles.Id_Venta));
            Cmd.Parameters.Add(new SqlParameter("@Id_Producto", Detalles.Id_Producto));
            Cmd.Parameters.Add(new SqlParameter("@Cantidad", Detalles.Cantidad));
            Cmd.Parameters.Add(new SqlParameter("@Precio_Venta", Detalles.Precio_Venta));
            Cmd.Parameters.Add(new SqlParameter("@Sub_Total", Detalles.Sub_Total));
            Cmd.Parameters.Add(new SqlParameter("@Descuento", Detalles.Descuento));
            Cmd.Parameters.Add(new SqlParameter("@Monto_Total", Detalles.Monto_Total));
            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método para anular el ingreso de un producto por ejemplo se cancelo el envio de un producto o se devuelve un producto

        public void AnularDetalleVenta(CE_Detalle_Ventas Detalles)
        {
            try
            {

                string Estado = string.Empty;
                Cmd = new SqlCommand("Select Estado From Ventas Where Id_Venta=" + Detalles.Id_Venta + "", Con.Abrir());
                Cmd.CommandType = CommandType.Text;
                SqlDataReader dr = Cmd.ExecuteReader();

                if (dr.Read())
                {
                    Estado = dr["Estado"].ToString();
                }

                dr.Close();
                Con.Cerrar();

                if (Estado == "Cancelado")
                {
                    return;
                }
                else
                {

                    Cmd = new SqlCommand("AnularDetalleVenta", Con.Abrir());
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.Add(new SqlParameter("@Id_Venta", Detalles.Id_Venta));
                    Cmd.Parameters.Add(new SqlParameter("@Id_Producto", Detalles.Id_Producto));
                    Cmd.Parameters.Add(new SqlParameter("@Cantidad", Detalles.Cantidad));
                    Cmd.Parameters.Add(new SqlParameter("@Precio_Venta", Detalles.Precio_Venta));
                    Cmd.Parameters.Add(new SqlParameter("@Sub_Total", Detalles.Sub_Total));
                    Cmd.Parameters.Add(new SqlParameter("@Descuento", Detalles.Descuento));
                    Cmd.Parameters.Add(new SqlParameter("@Monto_Total", Detalles.Monto_Total));
                    Cmd.Parameters.Add(new SqlParameter("@Id_DetalleVenta", Detalles.Id_DetalleVenta));
                    Cmd.CommandTimeout = 30;
                    Cmd.ExecuteNonQuery();
                    Con.Cerrar();
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se puedo anular la venta por: " + ex, "Cancelar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }

        public DataTable MostrarVentas(int Id_Venta)
        {
            DataTable Dt = new DataTable("Detalle_Venta");
            Cmd = new SqlCommand("Mostrar_Detalle_Venta", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Id_Venta", Id_Venta));

            Cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(Cmd);
            da.Fill(Dt);

            Con.Cerrar();

            return Dt;
        }

    }
}
