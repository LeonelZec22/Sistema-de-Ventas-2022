using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatos;
using System.Windows.Forms;
using System.Windows.Controls;
namespace CapaNegocio
{
    public class CDo_Procedimientos
    {
        //Capa negocio o dominio

        //Capa negocio o dominio

        CD_Procedimientos ObjProcedimientos = new CD_Procedimientos();

        //Método que me permite cargar los datos de una tabla a un datagridview

        public DataTable CargarDatos(string Tabla)
        {
            return ObjProcedimientos.CargarDatos(Tabla);
        }

        //Método que me permite alternar los  colores de las filas de un datagridview

        //public void AlternarColorFilaDataGridView(DataGridView Dgv)
        //{
        //    ObjProcedimientos.AlternarColorFilaDataGridView(Dgv);
        //}

        //Método que me permite generar los códigos de los productos y demás 

        public string GenerarCodigo(string Tabla)
        {
            return ObjProcedimientos.GenerarCodigo(Tabla);
        }



        //Método que me permite generar los id de los productos y demás 

        public string GenerarCodigoId(string Tabla)
        {
            return ObjProcedimientos.GenerarCodigoId(Tabla);
        }

        //Método que permite dar formato moneda a un TextBox o caja de texto

        public void FormatoMoneda(System.Windows.Controls.TextBox xTBox)
        {
            ObjProcedimientos.FormatoMoneda(xTBox);

        }

        //Método que permite dar limpiar una TextBox o caja de texto o combobox

        public void LimpiarControles(Form xForm)
        {
            ObjProcedimientos.LimpiarControles(xForm);

        }

        //Método que permite dar llenar un ComboBox de manera generica

        public void LlenarComboBox(string Tabla, string Nombre, System.Windows.Controls.ComboBox xCBox)
        {
            ObjProcedimientos.LlenarComboBox(Tabla, Nombre, xCBox);

        }

        public DataTable CargarProcedimiento(string Tabla)
        {
            return ObjProcedimientos.CargarProcedimiento(Tabla);
        }
    }
}
