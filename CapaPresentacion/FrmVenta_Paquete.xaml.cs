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
    /// Lógica de interacción para FrmVenta_Paquete.xaml
    /// </summary>
    public partial class FrmVenta_Paquete : Window
    {
        public FrmVenta_Paquete()
        {
            InitializeComponent();
        }

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Venta_Paquetes Ventas = new CDo_Venta_Paquetes();
        CE_Ventas_Paquetes Venta = new CE_Ventas_Paquetes();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            //DataGridVentaPaquete.ItemsSource = Ventas.MostrarVentasPaquetes().AsDataView();
            DataGridVentaPaquete.ItemsSource = Procedimientos.CargarDatos("Ventas_Paquetes").AsDataView();
            DataGridVentaPaquete.UnselectAllCells();

        }

        private void DataGridVentaPaquete_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Venta_Paquetes = e.Column.Header.ToString();

            if (Id_Venta_Paquetes == "Id_Venta_Paquetes")
            {
                e.Cancel = true;

            }

            string Id_Cliente = e.Column.Header.ToString();

            if (Id_Cliente == "Id_Cliente")
            {
                e.Cancel = true;

            }

            string Id_Paquetes = e.Column.Header.ToString();

            if (Id_Paquetes == "Id_Paquetes")
            {
                e.Cancel = true;

            }


            if (e.PropertyType == typeof(System.DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
            }

            #region Modificar columnas

            string Nombre = e.Column.Header.ToString();
            string Paquete = e.Column.Header.ToString();
            string Fecha_Venta = e.Column.Header.ToString();
            string Estado = e.Column.Header.ToString();
            string Cantidad_Vendida = e.Column.Header.ToString();
            string Cantidad_Usada = e.Column.Header.ToString();
            string Precio_Venta = e.Column.Header.ToString();

            if (Nombre == "Cliente")
            {
                e.Column.Width = 175;

            }

            if (Paquete == "Paquete")
            {
                e.Column.Width = 170;


            }

            if (Fecha_Venta == "Fecha Venta")
            {
                e.Column.Width = 117;
            }

            if (Estado == "Estado")
            {
                //e.Column.Width = 130;
            }

            if (Cantidad_Vendida == "Cantidad_Vendida")
            {
                e.Column.Header = "Vendida";
                e.Column.Width = 100;
            }

            if (Cantidad_Usada == "Cantidad_Usada")
            {
                e.Column.Header = "Usada";
                e.Column.Width = 100;
            }

            if (Precio_Venta == "Precio")
            {
                e.Column.Width = 100;
            }
            #endregion


        }


        private void AgVen_UpdateEventHandler(object sender, FrmAgregarVentaPaquete.UpdateEventArgs args)
        {
            CargarDatos();
        }
        //private void AnVen_UpdateEventHandler(object sender, FrmAnularVentas.UpdateEventArgs args)
        //{
        //    CargarDatos();
        //}

        private void BtnNuevaVenta_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregarVentaPaquete AgregarVentas = new FrmAgregarVentaPaquete(this);
            AgregarVentas.UpdateEventHandler += AgVen_UpdateEventHandler;
            AgregarVentas.ShowDialog();
        }


        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnProveedores_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnServicios_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnVentas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MinimizeApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        

        private void BtnAnularVenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        

        private void DataGridVentaPaquete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
    }
}
