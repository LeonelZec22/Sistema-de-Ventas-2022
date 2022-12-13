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
    /// Lógica de interacción para FrmVista_Venta_Producto.xaml
    /// </summary>
    public partial class FrmVista_Venta_Producto : Window
    {
        public FrmVista_Venta_Producto()
        {
            InitializeComponent();
        }

        CDo_Ventas Ventas = new CDo_Ventas();
        CE_Ventas Venta = new CE_Ventas();
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Mostrar_Productos_Ventas();
        }



        private void Mostrar_Productos_Ventas()
        {
            DataGridProductosVenta.ItemsSource = Ventas.Mostrar_Ingreso_Ventas().AsDataView();
            DataGridProductosVenta.UnselectAllCells();
        }


        private void DataGridProductosVenta_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Producto = e.Column.Header.ToString();

            if (Id_Producto == "Id_Producto")
            {
                e.Cancel = true;
            }
        }

        

        private void BtnSeleccionarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridProductosVenta.SelectedItems.Count == 0)
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
            DataGridProductosVenta.UnselectAllCells();
        }

        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void DataGridProductosVenta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Hide();
            DataGridProductosVenta.UnselectAllCells();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Hide();
            DataGridProductosVenta.UnselectAllCells();
        }
    }
}
