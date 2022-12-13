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
    /// Lógica de interacción para FrmVistaProducto.xaml
    /// </summary>
    public partial class FrmVistaProducto : Window
    {
        public FrmVistaProducto()
        {
            InitializeComponent();
            CargarDatos();
        }

        //Creamos un objeto de nuestra clase conexión

        CD_Conexion Con = new CD_Conexion();

        //Inicialiamos las variables a utilizar
        
        CDo_Productos Productos = new CDo_Productos();
        CE_Productos Producto = new CE_Productos();
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
           
            DataGridGestionProductos.ItemsSource = Procedimientos.CargarDatos("Productos").AsDataView();
            DataGridGestionProductos.UnselectAllCells();
        }

        private void DataGridGestionProductos_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Producto = e.Column.Header.ToString();

            if (Id_Producto == "Id_Producto")
            {
                e.Cancel = true;
            }
        }

        private void DataGridGestionProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //FrmAgregarIngreso ingreso = new FrmAgregarIngreso();


        }

        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnSeleccionarProd_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridGestionProductos.SelectedItems.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Producto en la lista productos!!", "Seleccionar Productos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }
            else
            {

                DialogResult = true;

                Hide();
            }
        }

        

        private void BtnCancelarPro_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            DataGridGestionProductos.UnselectAllCells();
        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Hide();
            DataGridGestionProductos.UnselectAllCells();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Hide();
            DataGridGestionProductos.UnselectAllCells();
        }
    }

}
