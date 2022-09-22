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
    /// Lógica de interacción para FrmProductos.xaml
    /// </summary>
    public partial class FrmProductos : Window
    {
        public FrmProductos()
        {
            InitializeComponent();

            CargarDatos();

            //DataGridProductos.Columns[0].Visibility = Visibility.Collapsed;
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
            Cmd = new SqlCommand("SELECT * FROM Productos", Con.Abrir());

            Cmd.CommandType = CommandType.Text;

            Dr = Cmd.ExecuteReader();

            Dt.Load(Dr);

            Dr.Close();

            Con.Cerrar();

            DataGridProductos.ItemsSource = Dt.DefaultView;

        }

        //Evento para ocultar una columna de un datagrid autogenerico
        private void DataGridProductos_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Producto = e.Column.Header.ToString();

            if (Id_Producto == "Id_Producto")
            {
                e.Cancel = true;
            }
        }
    }
}
