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
using System.Collections;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para FrmProveedores.xaml
    /// </summary>
    public partial class FrmProveedores : Window
    {
        public FrmProveedores()
        {
            InitializeComponent();
            CargarDatos();
        }


        //Creamos un objeto de nuestra clase conexión

        CD_Conexion Con = new CD_Conexion();

        //Inicialiamos las variables a utilizar
        SqlCommand Cmd;
        SqlDataReader Dr;
        DataTable Dt;

        CDo_Proveedores Proveedores = new CDo_Proveedores();
        CE_Proveedores Proveedor = new CE_Proveedores();
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();

        //Método para cargar o mostrar los datos en la tabla del formulario
        private void CargarDatos()
        {
            //Dt = new DataTable("Cargar_Datos");
            //Cmd = new SqlCommand("SELECT * FROM Proveedores", Con.Abrir());

            //Cmd.CommandType = CommandType.Text;

            //Dr = Cmd.ExecuteReader();

            //Dt.Load(Dr);

            //Dr.Close();

            //Con.Cerrar();

            //DataGridProveedores.ItemsSource = Dt.DefaultView;

            DataGridProveedores.ItemsSource = Procedimientos.CargarDatos("Proveedores").AsDataView();

        }

        #region Eventos del DataGrid
        //Evento para ocultar una columna de un datagrid autogenerico
        private void DataGridProveedores_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Proveedor = e.Column.Header.ToString();

            if (Id_Proveedor == "Id_Proveedor")
            {
                e.Cancel = true;
            }
        }

        private void DataGridProveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion


        #region Botones del encabezado
        private void BtnAddProve_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregarProveedores proveedores = new FrmAgregarProveedores(this);
            proveedores.UpdateEventHandler += AgProve_UpdateEventHandler;
            proveedores.ShowDialog();
        }

        private void BtnEditProve_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteProve_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion


        #region Botones del menu lateral

        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Inicio = new MainWindow();
            Hide();
            Inicio.ShowDialog();
            Close();
        }

        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            FrmProductos Producto = new FrmProductos();
            Hide();
            Producto.ShowDialog();
            Close();
        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            FrmInventario Inventario = new FrmInventario();
            Hide();
            Inventario.ShowDialog();
            Close();
        }

        #endregion



        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AgProve_UpdateEventHandler(object sender, FrmAgregarProveedores.UpdateEventArgs args)
        {
            CargarDatos();
        }


    }
}
