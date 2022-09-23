using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CapaNegocio;
using CapaEntidad;
using CapaDatos;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para FrmInventario.xaml
    /// </summary>
    public partial class FrmInventario : Window
    {
        public FrmInventario()
        {
            InitializeComponent();
            CargarDatos();
            SumarInventario();

        }

        //Método para que se pueda mover la ventana

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        //Creamos un objeto de nuestra clase conexión

        CD_Conexion Con = new CD_Conexion();

        //Inicialiamos las variables a utilizar
        SqlCommand Cmd;
        SqlDataReader Dr;
        DataTable Dt;

        //Método para cargar o mostrar los datos en la tabla
        private void CargarDatos()
        {
            Dt = new DataTable("Cargar_Datos");
            Cmd = new SqlCommand("SELECT Codigo, Nombre, Cantidad, Precio_Venta, Costo_Unitario, Monto_Total FROM Inventario", Con.Abrir());

            Cmd.CommandType = CommandType.Text;

            Dr = Cmd.ExecuteReader();

            Dt.Load(Dr);

            Dr.Close();

            Con.Cerrar();

            DataGridInventario.ItemsSource = Dt.DefaultView;

        }

        //Método para sumar toda la columna de Monto Inventario y imprimir ese valor en el textbox

        public static decimal Total;

        private void SumarInventario()
        {
            Total = 0;
            foreach (DataRowView row in DataGridInventario.ItemsSource)
            {
                Total += Convert.ToDecimal(row[5]);
            }

            txtMontoInv.Text = Total.ToString("N2");
        }


        //Evento para ocultar una columna de un datagrid autogenerico
        private void DataGridInventario_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string IdInventario = e.Column.Header.ToString();

            if (IdInventario == "ldInventario")
            {
                e.Cancel = false;
            }

        }
    }
}
