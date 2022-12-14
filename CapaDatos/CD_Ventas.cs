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
    public class CD_Ventas
    {
        CD_Conexion Con = new CD_Conexion();

        private SqlCommand Cmd;
        SqlDataAdapter Da;
        DataTable dataTable;
        //Método para agregar el ingreso de un venta

        public void AgregarVenta(CE_Ventas Ventas)
        {
            Cmd = new SqlCommand("AgregarVenta", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@id_Cliente", Ventas.Id_Cliente));
            Cmd.Parameters.Add(new SqlParameter("@Fecha_Venta", Ventas.Fecha_Venta));
            Cmd.Parameters.Add(new SqlParameter("@Sub_Total", Ventas.Sub_Total));
            Cmd.Parameters.Add(new SqlParameter("@Descuento", Ventas.Descuento));
            Cmd.Parameters.Add(new SqlParameter("@Monto_Total", Ventas.Monto_Total));
            Cmd.Parameters.Add(new SqlParameter("@Estado", Ventas.Estado));
            Cmd.Parameters.Add(new SqlParameter("@id_Usuario", Ventas.Id_Usuario));
            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método para anular el ingreso de un producto por ejemplo se cancelo el envio de un producto o se devuelve un producto

        public void AnularVenta(CE_Ventas Ventas)
        {
            try
            {

                string Estado = string.Empty;

                Cmd = new SqlCommand("Select Estado From Ventas Where Id_Venta=" + Ventas.Id_Venta + "", Con.Abrir());
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
                    System.Windows.Forms.MessageBox.Show("Esta Venta ya ha sido Anulada anteriormente", "Cancelar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    Cmd = new SqlCommand("AnularVenta", Con.Abrir());
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.Add(new SqlParameter("@id_Cliente", Ventas.Id_Cliente));
                    Cmd.Parameters.Add(new SqlParameter("@Fecha_Venta", Ventas.Fecha_Venta));
                    Cmd.Parameters.Add(new SqlParameter("@Sub_Total", Ventas.Sub_Total));
                    Cmd.Parameters.Add(new SqlParameter("@Descuento", Ventas.Descuento));
                    Cmd.Parameters.Add(new SqlParameter("@Monto_Total", Ventas.Monto_Total));
                    Cmd.Parameters.Add(new SqlParameter("@Estado", Ventas.Estado));
                    Cmd.Parameters.Add(new SqlParameter("@id_Usuario", Ventas.Id_Usuario));
                    Cmd.Parameters.Add(new SqlParameter("@Id_Venta", Ventas.Id_Venta));
                    Cmd.ExecuteNonQuery();
                    System.Windows.Forms.MessageBox.Show("La Venta fue anulada correctamente", "Cancelar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    Con.Cerrar();
                }

            }

            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se puedo anular la venta por: " + ex, "Cancelar Venta de Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }


        //Método para mostrar los productos ingresos 
        public DataTable MostrarVentas()
        {
            DataTable Dt = new DataTable("Venta_de_Producto");
            Cmd = new SqlCommand("MostrarVentas", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader Dr = Cmd.ExecuteReader();
            Dt.Load(Dr);

            Con.Cerrar();

            Dr.Close();

            return Dt;
        }

        public DataTable Mostrar_Ingreso_Ventas()
        {
            DataTable Dt = new DataTable("Mostrar_Producto_Venta");
            Cmd = new SqlCommand("Mostrar_Ingreso_Ventas", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader Dr = Cmd.ExecuteReader();
            Dt.Load(Dr);

            Con.Cerrar();

            Dr.Close();

            return Dt;
        }

        //Método que me permite buscar una venta  por el nombre
        public DataTable Buscar_Venta_Nombre(CE_Ventas Ventas)
        {
            dataTable = new DataTable("Nombre");
            Cmd = new SqlCommand("Buscar_Venta_Nombre", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Ventas.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

        //Método que me permite buscar una venta  por el estado
        public DataTable Buscar_Venta_Estado(CE_Ventas Ventas)
        {
            dataTable = new DataTable("Estado");
            Cmd = new SqlCommand("Buscar_Venta_Estado", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Buscar", Ventas.Buscar));

            Da = new SqlDataAdapter(Cmd);
            Da.Fill(dataTable);

            Con.Cerrar();

            return dataTable;
        }

    }
}
