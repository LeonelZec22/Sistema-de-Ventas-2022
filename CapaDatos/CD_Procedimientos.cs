using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Controls;

namespace CapaDatos
{
    public class CD_Procedimientos
    {
        //Creamos un objeto de nuestra clase conexión
      CD_Conexion Con = new CD_Conexion();


        SqlCommand Cmd;
        SqlDataReader Dr;
        DataTable Dt;

       // Método que me permite cargar los datos de una tabla a un datagridview


        public DataTable CargarDatos(string Tabla)
        {
            Dt = new DataTable("Cargar_Datos");
            Cmd = new SqlCommand("SELECT * FROM " + Tabla, Con.Abrir());

            Cmd.CommandType = CommandType.Text;

            Dr = Cmd.ExecuteReader();

            Dt.Load(Dr);

            Dr.Close();

            Con.Cerrar();

            return Dt;
        }

        //Método que me permite alternar los  colores de las filas de un datagridview

        public void AlternarColorFilaDataGridView(DataGridView Dgv)
        {
            Dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            Dgv.DefaultCellStyle.BackColor = Color.White;
            
        }

        //Método que me permite generar los códigos de los productos y demás 

        public string GenerarCodigo(string Tabla)
        {
            string Codigo = string.Empty;
            int Total = 0;

            Cmd = new SqlCommand("SELECT (*) as TotalRegistros FROM " + Tabla, Con.Abrir());

            Cmd.CommandType = CommandType.Text;

            Dr = Cmd.ExecuteReader();

            if (Dr.Read())
            {
                Total = Convert.ToInt32(Dr["TotalRegistros"]) + 1;
            }

            Dr.Close();

            if (Total < 10)
            {
                Codigo = "0000000" + Total;
            }

            else if (Total < 100)
            {
                Codigo = "000000" + Total;
            }

            else if (Total < 1000)
            {
                Codigo = "00000" + Total;
            }

            else if (Total < 10000)
            {
                Codigo = "0000" + Total;
            }

            else if (Total < 100000)
            {
                Codigo = "000" + Total;
            }

            else if (Total < 1000000)
            {
                Codigo = "00" + Total;
            }

            else if (Total < 10000000)
            {
                Codigo = "0" + Total;
            }

            Con.Cerrar();

            return Codigo;
        }

        //Método que me permite generar los id de los productos y demás 

        public string GenerarCodigoId(string Tabla)
        {
            string Codigo = string.Empty;
            int Total = 0;

            Cmd = new SqlCommand("SELECT (*) as TotalRegistros FROM " + Tabla, Con.Abrir());

            Cmd.CommandType = CommandType.Text;

            Dr = Cmd.ExecuteReader();

            if (Dr.Read())
            {
                Total = Convert.ToInt32(Dr["TotalRegistros"]) + 1;
            }

            Dr.Close();



            Con.Cerrar();

            return Codigo;
        }

        //Método que permite dar formato moneda a un TextBox o caja de texto

         

        public void FormatoMoneda(System.Windows.Controls.TextBox xTBox)
        {
            if (xTBox.Text == string.Empty)
            {
                return;
            }

            else
            {
                decimal Monto;

                Monto = Convert.ToDecimal(xTBox.Text);

                xTBox.Text = Monto.ToString("N2");
            }

        }

        //Método que permite dar limpiar una TextBox o caja de texto o combobox

        public void LimpiarControles(Form xForm)
        {
            foreach (var xCtrl in xForm.Controls)
            {
                if (xCtrl is System.Windows.Controls.TextBox)
                {
                    ((System.Windows.Controls.TextBox)xCtrl).Text = string.Empty;
                }
                else if (xCtrl is System.Windows.Controls.ComboBox)
                {
                    ((System.Windows.Controls.ComboBox)xCtrl).Text = string.Empty;
                }
            }

        }

        //Método que permite dar llenar un ComboBox de manera generica

        public void LlenarComboBox(string Tabla, string Nombre, System.Windows.Controls.ComboBox xCBox)
        {
            Cmd = new SqlCommand("SELECT * FROM " + Tabla, Con.Abrir());

            Cmd.CommandType = CommandType.Text;

            Dr = Cmd.ExecuteReader();

            while (Dr.Read())
            {
               // xCBox.Items.Add(Dr[Nombre].ToString());
            }
        }
    }
}
