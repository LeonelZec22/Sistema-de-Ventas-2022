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
    public class CD_Detalle_Ingreso
    {
        CD_Conexion Con = new CD_Conexion();

        private SqlCommand Cmd;

        //Método para agregar el ingreso de un producto

        public void AgregarDetalleIngreso(CE_Detalle_Ingreso Detalles)
        {
            Cmd = new SqlCommand("Agregar_Detalle_Ingreso", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Id_IngresoProducto", Detalles.Id_IngresoProducto));
            Cmd.Parameters.Add(new SqlParameter("@Id_Producto", Detalles.Id_Producto));
            Cmd.Parameters.Add(new SqlParameter("@Nombre", Detalles.Nombre));
            Cmd.Parameters.Add(new SqlParameter("@Cantidad", Detalles.Cantidad));
            Cmd.Parameters.Add(new SqlParameter("@Costo_Unitario", Detalles.Costo_Unitario));
            Cmd.Parameters.Add(new SqlParameter("@Sub_Total", Detalles.Sub_Total));
            Cmd.ExecuteNonQuery();

            Con.Cerrar();
        }

        //Método para anular el ingreso de un producto por ejemplo se cancelo el envio de un producto o se devuelve un producto

        public void AnularDetalleIngreso(CE_Detalle_Ingreso Detalles)
        {
            try
            {

                string Estado = string.Empty;
                Cmd = new SqlCommand("Select Estado From Ingreso_Productos Where Id_IngresoProducto=" + Detalles.Id_IngresoProducto + "", Con.Abrir());
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

                    Cmd = new SqlCommand("Anular_Detalle_Ingreso", Con.Abrir());
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.Add(new SqlParameter("@ID_Detalle", Detalles.Id_Detalle));
                    Cmd.Parameters.Add(new SqlParameter("@Id_IngresoProducto", Detalles.Id_IngresoProducto));
                    Cmd.Parameters.Add(new SqlParameter("@Id_Producto", Detalles.Id_Producto));
                    Cmd.Parameters.Add(new SqlParameter("@Nombre", Detalles.Nombre));
                    Cmd.Parameters.Add(new SqlParameter("@Cantidad", Detalles.Cantidad));
                    Cmd.Parameters.Add(new SqlParameter("@Costo_Unitario", Detalles.Costo_Unitario));
                    Cmd.Parameters.Add(new SqlParameter("@Sub_Total", Detalles.Sub_Total));
                    Cmd.ExecuteNonQuery();
                    Con.Cerrar();
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se puedo anular la comprar por: " + ex, "Cancelar Ingreso Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }

        public DataTable MostrarIngresoProducto(int ID_Ingreso)
        {
            DataTable Dt = new DataTable("Detalles_Ingreso");
            Cmd = new SqlCommand("Mostrar_Detalle_Ingreso", Con.Abrir());
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("@Id_IngresoProducto", ID_Ingreso));
           
            Cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(Cmd);
            da.Fill(Dt);

            Con.Cerrar();

            return Dt;
        }
    }
}
