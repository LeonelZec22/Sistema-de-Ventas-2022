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
    public class CD_Reservas
    {
        CD_Conexion Con = new CD_Conexion();

        private SqlCommand Cmd;
        SqlDataAdapter Da;
        DataTable dataTable;

        //Método para agregar el ingreso de una reserva

        public void AgregarReserva(CE_Reservas Reservas)
        {
            Cmd = new SqlCommand("AgregarReserva", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@id_Cliente", Reservas.Id_Cliente));
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Reservas.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Fecha_Reserva", Reservas.Fecha_Reserva));
            Cmd.Parameters.Add(new SqlParameter("@Estado", Reservas.Estado));
            Cmd.Parameters.Add(new SqlParameter("@Descuento", Reservas.Descuento));
            Cmd.Parameters.Add(new SqlParameter("@Monto_Total", Reservas.Monto_Total));
            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método para anular el ingreso de una reseva por ejemplo se cancelo la reserva porque se movio para otro día

        public void AnularReserva(CE_Reservas Reservas)
        {
            try
            {

                string Estado = string.Empty;

                Cmd = new SqlCommand("Select Estado From Reserva Where Id_Reserva=" + Reservas.Id_Reserva + "", Con.Abrir());
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
                    System.Windows.Forms.MessageBox.Show("Esta Reserva ya ha sido Anulada anteriormente", "Cancelar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    Cmd = new SqlCommand("AnularReserva", Con.Abrir());
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.Add(new SqlParameter("@id_Cliente", Reservas.Id_Cliente));
                    Cmd.Parameters.Add(new SqlParameter("@Nombre", Reservas.Nombre));
                    Cmd.Parameters.Add(new SqlParameter("@Fecha_Reserva", Reservas.Fecha_Reserva));
                    Cmd.Parameters.Add(new SqlParameter("@Estado", Reservas.Estado));
                    Cmd.Parameters.Add(new SqlParameter("@Descuento", Reservas.Descuento));
                    Cmd.Parameters.Add(new SqlParameter("@Monto_Total", Reservas.Monto_Total));
                    Cmd.Parameters.Add(new SqlParameter("@Id_Reserva", Reservas.Id_Reserva));
                    Cmd.ExecuteNonQuery();
                    System.Windows.Forms.MessageBox.Show("La Reserva fue anulada correctamente", "Cancelar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    Con.Cerrar();
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se puedo anular la Reserva por: " + ex, "Cancelar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }


        }


        //Método para mostrar las reservas de los servicios
        public DataTable MostrarReservas()
        {
            DataTable Dt = new DataTable("Reserva_de_Servicios");
            Cmd = new SqlCommand("MostrarReserva", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader Dr = Cmd.ExecuteReader();
            Dt.Load(Dr);

            Con.Cerrar();

            Dr.Close();

            return Dt;
        }

        //Método que me permite buscar una reserva  por el nombre
        public DataTable Buscar_Reserva_Nombre(CE_Reservas Reservas)
        {
            dataTable = new DataTable("Nombre");
            Cmd = new SqlCommand("Buscar_Reserva_Nombre", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Reservas.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

        //Método que me permite buscar una reserva  por la fecha de reserva
        public DataTable Buscar_Reserva_FechaReserva(CE_Reservas Reservas)
        {
            dataTable = new DataTable("FechaReserva");
            Cmd = new SqlCommand("Buscar_Reserva_FechaReserva", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Reservas.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

        //Método que me permite buscar una reserva  por el estado de la reserva
        public DataTable Buscar_Reserva_Estado(CE_Reservas Reservas)
        {
            dataTable = new DataTable("Estado");
            Cmd = new SqlCommand("Buscar_Reserva_Estado", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Reservas.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }
    }
}
