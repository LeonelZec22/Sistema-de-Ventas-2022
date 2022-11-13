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
using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para FrmVentas.xaml
    /// </summary>
    public partial class FrmVentas : Window
    {
        public FrmVentas()
        {
            InitializeComponent();
        }

        //Instancia de la clases
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Ventas Ventas = new CDo_Ventas();
        CE_Ventas Venta = new CE_Ventas();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

        //Método para cargar datos de la tabla Ingreso Productos 

        private void CargarDatos()
        {
            DataGridVenta.ItemsSource = Ventas.MostrarVentas().AsDataView();
            DataGridVenta.UnselectAllCells();

        }

        private void DataGridVenta_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Venta = e.Column.Header.ToString();

            if (Id_Venta == "Id_Venta")
            {
                e.Cancel = true;
            }
        }





        private void BtnNuevaVenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAnularVenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnImprimirInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGridVenta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            FrmProductos Productos = new FrmProductos();

            Hide();

            Productos.ShowDialog();

            Close();
        }


    }
}
