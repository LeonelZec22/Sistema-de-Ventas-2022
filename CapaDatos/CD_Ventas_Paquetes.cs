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
    public class CD_Ventas_Paquetes
    {
        CD_Conexion Con = new CD_Conexion();

        private SqlCommand Cmd;
        SqlDataAdapter Da;
        DataTable dataTable;
        //Método para agregar el ingreso de un venta

        public void AgregarVentaPaquetes(CE_Ventas_Paquetes Ventas)
        {
            Cmd = new SqlCommand("AgregarVentaPaquetes", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@id_Cliente", Ventas.Id_Cliente));
            Cmd.Parameters.Add(new SqlParameter("@Id_Paquete", Ventas.Id_Venta_Paquetes));
            Cmd.Parameters.Add(new SqlParameter("@Cliente", Ventas.Cliente));
            Cmd.Parameters.Add(new SqlParameter("@Paquete", Ventas.Paquete));
            Cmd.Parameters.Add(new SqlParameter("@Fecha_Venta", Ventas.Fecha_Venta));
            Cmd.Parameters.Add(new SqlParameter("@Cantidad_Vendida", Ventas.Cantidad_Vendida));
            Cmd.Parameters.Add(new SqlParameter("@Cantidad_Usada", Ventas.Cantidad_Usada));
            Cmd.Parameters.Add(new SqlParameter("@Estado", Ventas.Estado));
            Cmd.Parameters.Add(new SqlParameter("@Precio_Venta", Ventas.Precio_Venta));
            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método para anular el ingreso de un producto por ejemplo se cancelo el envio de un producto o se devuelve un producto

        public void AnularVentaPaquetes(CE_Ventas_Paquetes Ventas)
        {
            try
            {

                string Estado = string.Empty;

                Cmd = new SqlCommand("Select Estado From Ventas_Paquetes Where Id_Venta_Paquetes=" + Ventas.Id_Venta_Paquetes + "", Con.Abrir());
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
                    System.Windows.Forms.MessageBox.Show("Esta Venta de Paquete ya ha sido Anulada anteriormente", "Cancelar Venta Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    Cmd = new SqlCommand("AnularVentaPaquetes", Con.Abrir());
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.Add(new SqlParameter("@id_Cliente", Ventas.Id_Cliente));
                    Cmd.Parameters.Add(new SqlParameter("@Id_Paquete", Ventas.Id_Paquetes));
                    Cmd.Parameters.Add(new SqlParameter("@Cliente", Ventas.Cliente));
                    Cmd.Parameters.Add(new SqlParameter("@Paquete", Ventas.Paquete));
                    Cmd.Parameters.Add(new SqlParameter("@Fecha_Venta", Ventas.Fecha_Venta));
                    Cmd.Parameters.Add(new SqlParameter("@Cantidad_Vendida", Ventas.Cantidad_Vendida));
                    Cmd.Parameters.Add(new SqlParameter("@Cantidad_Usada", Ventas.Cantidad_Usada));
                    Cmd.Parameters.Add(new SqlParameter("@Estado", Ventas.Estado));
                    Cmd.Parameters.Add(new SqlParameter("@Precio_Venta", Ventas.Precio_Venta));
                    Cmd.Parameters.Add(new SqlParameter("@Id_Venta_Paquetes", Ventas.Id_Venta_Paquetes));
                    Cmd.ExecuteNonQuery();
                    System.Windows.Forms.MessageBox.Show("La Venta fue anulada correctamente", "Cancelar Venta de Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    Con.Cerrar();
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se puedo anular la venta por: " + ex, "Cancelar Venta de Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }

        public void EditarVentaPaquetes(CE_Ventas_Paquetes Ventas)
        {
            string fech = Convert.ToString(Ventas.Fecha_Venta);

            Cmd = new SqlCommand("EditarVentaPaquetes", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@id_Cliente", Ventas.Id_Cliente));
            Cmd.Parameters.Add(new SqlParameter("@Id_Paquete", Ventas.Id_Venta_Paquetes));
            Cmd.Parameters.Add(new SqlParameter("@Cliente", Ventas.Cliente));
            Cmd.Parameters.Add(new SqlParameter("@Paquete", Ventas.Paquete));
            Cmd.Parameters.Add(new SqlParameter("@Fecha_Venta", Ventas.Fecha_Venta));
            Cmd.Parameters.Add(new SqlParameter("@Cantidad_Vendida", Ventas.Cantidad_Vendida));
            Cmd.Parameters.Add(new SqlParameter("@Cantidad_Usada", Ventas.Cantidad_Usada));
            Cmd.Parameters.Add(new SqlParameter("@Estado", Ventas.Estado));
            Cmd.Parameters.Add(new SqlParameter("@Precio_Venta", Ventas.Precio_Venta));
            Cmd.Parameters.Add(new SqlParameter("@Id_Venta_Paquetes", Ventas.Id_Venta_Paquetes));
            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        public DataTable MostrarVentasPaquetes()
        {
            DataTable Dt = new DataTable("Venta_de_Paquetes");
            Cmd = new SqlCommand("MostrarVentasPaquetes", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader Dr = Cmd.ExecuteReader();
            Dt.Load(Dr);

            Con.Cerrar();

            Dr.Close();

            return Dt;
        }

        public DataTable Buscar_VentaPaquete_Cliente(CE_Ventas_Paquetes Ventas)
        {
            dataTable = new DataTable("Nombre");
            Cmd = new SqlCommand("Buscar_VentaPaquete_Nombre", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Ventas.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

        public DataTable Buscar_VentaPaquete_Estado(CE_Ventas_Paquetes Ventas)
        {
            dataTable = new DataTable("Estado");
            Cmd = new SqlCommand("Buscar_VentaPaquete_Estado", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Ventas.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

        public DataTable Buscar_VentaPaquete_Paquete(CE_Ventas_Paquetes Ventas)
        {
            dataTable = new DataTable("Paquete");
            Cmd = new SqlCommand("Buscar_VentaPaquete_Paquete", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Ventas.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }
    }
}
