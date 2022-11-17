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
using CapaEntidad;
using CapaNegocio;
using System.Data;
using System.Reflection;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para FrmAnularVentas.xaml
    /// </summary>
    public partial class FrmAnularVentas : Window
    {
        public FrmAnularVentas()
        {
            InitializeComponent();
        }


        public FrmAnularVentas(FrmVentas Ventas)
        {
            InitializeComponent();
        }


        #region Instancia de objetos a usar

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Ventas Ventas = new CDo_Ventas();
        CE_Ventas Venta = new CE_Ventas();
        CDo_Detalle_Ventas DetalleVentas = new CDo_Detalle_Ventas();
        CE_Detalle_Ventas DetalleVenta = new CE_Detalle_Ventas();


        #endregion

        #region Eventos del formulario 

        //Agregamos un delegado

        public delegate void UpdateDelegate(object sender, UpdateEventArgs args);

        //Creamos un evento para actualizar el datagrid
        public event UpdateDelegate UpdateEventHandler;

        public class UpdateEventArgs : EventArgs
        {
            public string Data { get; set; }
        }


        protected void Anular()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);
        }

        #endregion
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Procedimientos.FormatoMoneda(txtSubTotal);
            Procedimientos.FormatoMoneda(txtDescuentoVenta);
            Procedimientos.FormatoMoneda(txtMontoTotal);
            MostrarDetalleVenta();
        }

        public void MostrarDetalleVenta()
        {
            int Index = Convert.ToInt32(txtId_Venta.Text);
            DataGridAnularVenta.ItemsSource = DetalleVentas.MostrarVentas(Index).AsDataView();
            DataGridAnularVenta.UnselectAllCells();
        }
        private void BtnAnularVenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancelarVenta_Click(object sender, RoutedEventArgs e)
        {
           
            this.Hide();
        }

        private void DataGridAnularVenta_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_DetalleVenta = e.Column.Header.ToString();

            string Id_Venta = e.Column.Header.ToString();

            string Id_Producto = e.Column.Header.ToString();

            if (Id_DetalleVenta == "Id_DetalleVenta")
            {
                e.Cancel = true;
            }


            if (Id_Venta == "Id_Venta")
            {
                e.Cancel = true;
            }

            if (Id_Producto == "Id_Producto")
            {
                e.Cancel = true;
            }

        }

        private void DataGridAnularVenta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
