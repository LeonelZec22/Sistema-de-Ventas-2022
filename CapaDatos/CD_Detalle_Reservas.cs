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
    public class CD_Detalle_Reservas
    {
        CD_Conexion Con = new CD_Conexion();

        private SqlCommand Cmd;

        public void AgregarDetalleReserva(CE_Detalle_Reservas DetallesReserva)
        {
            Cmd = new SqlCommand("AgregarDetalleReserva", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Id_Reserva", DetallesReserva.Id_Reserva));
            Cmd.Parameters.Add(new SqlParameter("@Id_Servicios", DetallesReserva.Id_Servicios));
            Cmd.Parameters.Add(new SqlParameter("@Estado", DetallesReserva.Estado));
            Cmd.Parameters.Add(new SqlParameter("@Descuento", DetallesReserva.Descuento));
            Cmd.Parameters.Add(new SqlParameter("@Monto_Total", DetallesReserva.Monto_Total));
            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        public void AnularDetalleReserva(CE_Detalle_Reservas DetallesReserva)
        {
            try
            {

                string Estado = string.Empty;
                Cmd = new SqlCommand("Select Estado From Reserva Where Id_Reserva=" + DetallesReserva.Id_Reserva + "", Con.Abrir());
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

                    Cmd = new SqlCommand("AnularDetalleReserva", Con.Abrir());
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.Add(new SqlParameter("@Id_Reserva", DetallesReserva.Id_Reserva));
                    Cmd.Parameters.Add(new SqlParameter("@Id_Servicios", DetallesReserva.Id_Servicios));
                    Cmd.Parameters.Add(new SqlParameter("@Estado", DetallesReserva.Estado));
                    Cmd.Parameters.Add(new SqlParameter("@Descuento", DetallesReserva.Descuento));
                    Cmd.Parameters.Add(new SqlParameter("@Monto_Total", DetallesReserva.Monto_Total));
                    Cmd.Parameters.Add(new SqlParameter("@Id_DetalleReserva", DetallesReserva.Id_DetalleReserva));
                    Cmd.ExecuteNonQuery();
                    Con.Cerrar();
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se puedo anular la Reserva por: " + ex, "Cancelar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }

        public DataTable MostrarReserva(int Id_Reserva)
        {
            DataTable Dt = new DataTable("Detalle_Reserva");
            Cmd = new SqlCommand("Mostrar_Detalle_Reserva", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Id_Reserva", Id_Reserva));

            Cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(Cmd);
            da.Fill(Dt);

            Con.Cerrar();

            return Dt;
        }
    }
}
