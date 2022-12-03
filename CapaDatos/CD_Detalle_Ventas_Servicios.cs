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
    public class CD_Detalle_Ventas_Servicios
    {
        CD_Conexion Con = new CD_Conexion();

        private SqlCommand Cmd;

        public void AgregarDetalleVentaServicios(CE_Detalle_Venta_Servicios Detalles)
        {
            Cmd = new SqlCommand("AgregarDetalleVentaServicios", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Id_Venta_Servicios", Detalles.Id_Venta_Servicios));
            Cmd.Parameters.Add(new SqlParameter("@Id_Reserva", Detalles.Id_Reserva));
            Cmd.Parameters.Add(new SqlParameter("@Fecha_Reserva", Detalles.Fecha_Reserva));
            Cmd.Parameters.Add(new SqlParameter("@Estado", Detalles.Estado));
            Cmd.Parameters.Add(new SqlParameter("@Descuento", Detalles.Descuento));
            Cmd.Parameters.Add(new SqlParameter("@Monto_Total", Detalles.Monto_Total));
            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        public void AnularDetalleVentaServicios(CE_Detalle_Venta_Servicios Detalles)
        {
            try
            {

                string Estado = string.Empty;
                Cmd = new SqlCommand("Select Estado From Ventas_Servicios Where Id_Venta_Servicios=" + Detalles.Id_Venta_Servicios + "", Con.Abrir());
                Cmd.CommandType = CommandType.Text;
                SqlDataReader dr = Cmd.ExecuteReader();

                if (dr.Read())
                {
                    Estado = dr["Estado"].ToString();
                }

                dr.Close();
                Con.Cerrar();

                if (Estado == "Anulado")
                {
                    return;
                }
                else
                {

                    Cmd = new SqlCommand("AnularDetalleVentaServicios", Con.Abrir());
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.Add(new SqlParameter("@Id_Venta_Servicios", Detalles.Id_Venta_Servicios));
                    Cmd.Parameters.Add(new SqlParameter("@Id_Reserva", Detalles.Id_Reserva));
                    Cmd.Parameters.Add(new SqlParameter("@Fecha_Reserva", Detalles.Fecha_Reserva));
                    Cmd.Parameters.Add(new SqlParameter("@Estado", Detalles.Estado));
                    Cmd.Parameters.Add(new SqlParameter("@Descuento", Detalles.Descuento));
                    Cmd.Parameters.Add(new SqlParameter("@Monto_Total", Detalles.Monto_Total));
                    Cmd.Parameters.Add(new SqlParameter("@Id_DetalleVenta_Servicios", Detalles.Id_DetalleVenta_Servicios));
                    Cmd.ExecuteNonQuery();
                    Con.Cerrar();
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se puedo anular la venta por: " + ex, "Cancelar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }

        public DataTable MostrarVentasServicios(int Id_Venta_Servicios)
        {
            DataTable Dt = new DataTable("Detalle_Venta_Servicio");
            Cmd = new SqlCommand("Mostrar_Detalle_Venta_Servicios", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Id_Venta_Servicios", Id_Venta_Servicios));

            Cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(Cmd);
            da.Fill(Dt);

            Con.Cerrar();

            return Dt;
        }
    }
}
