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
    public class CD_Ventas_Servicios
    {
        CD_Conexion Con = new CD_Conexion();

        private SqlCommand Cmd;
        SqlDataAdapter Da;
        DataTable dataTable;
        //Método para agregar el ingreso de un venta

        public void AgregarVentaServicios(CE_Ventas_Servicios Ventas)
        {

            try {
                
                Cmd = new SqlCommand("AgregarVentaServicios", Con.Abrir());
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.Add(new SqlParameter("@id_Cliente", Ventas.Id_Cliente));
                Cmd.Parameters.Add(new SqlParameter("@Fecha_Venta", Ventas.Fecha_Venta));
                Cmd.Parameters.Add(new SqlParameter("@Descuento", Ventas.Descuento));
                Cmd.Parameters.Add(new SqlParameter("@Monto_Total", Ventas.Monto_Total));
                Cmd.Parameters.Add(new SqlParameter("@Estado", Ventas.Estado));
                Cmd.Parameters.Add(new SqlParameter("@id_Usuario", Ventas.Id_Usuario));
                Cmd.ExecuteNonQuery();

                Con.Cerrar();
            }

            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se puedo Agregar la Venta por: " + ex, "Agregar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        public void AnularVentaServicios(CE_Ventas_Servicios Ventas)
        {
            try
            {

                string Estado = string.Empty;

                Cmd = new SqlCommand("Select Estado From Ventas_Servicios Where Id_Venta_Servicios=" + Ventas.Id_Venta_Servicios + "", Con.Abrir());
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
                    System.Windows.Forms.MessageBox.Show("Esta Venta ya ha sido Anulada anteriormente", "Cancelar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    Cmd = new SqlCommand("AnularVentaServicios", Con.Abrir());
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.Add(new SqlParameter("@id_Cliente", Ventas.Id_Cliente));
                    Cmd.Parameters.Add(new SqlParameter("@Fecha_Venta", Ventas.Fecha_Venta));
                    Cmd.Parameters.Add(new SqlParameter("@Descuento", Ventas.Descuento));
                    Cmd.Parameters.Add(new SqlParameter("@Monto_Total", Ventas.Monto_Total));
                    Cmd.Parameters.Add(new SqlParameter("@Estado", Ventas.Estado));
                    Cmd.Parameters.Add(new SqlParameter("@id_Usuario", Ventas.Id_Usuario));
                    Cmd.Parameters.Add(new SqlParameter("@Id_Venta_Servicios", Ventas.Id_Venta_Servicios));
                    Cmd.ExecuteNonQuery();
                    System.Windows.Forms.MessageBox.Show("La Venta fue anulada correctamente", "Cancelar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    Con.Cerrar();
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se puedo anular la venta por: " + ex, "Cancelar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }

        public DataTable MostrarVentasServicios()
        {
            DataTable Dt = new DataTable("Venta_de_Reserva");
            Cmd = new SqlCommand("MostrarVentasServicios", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader Dr = Cmd.ExecuteReader();
            Dt.Load(Dr);

            Con.Cerrar();

            Dr.Close();

            return Dt;
        }

        
    }
}
