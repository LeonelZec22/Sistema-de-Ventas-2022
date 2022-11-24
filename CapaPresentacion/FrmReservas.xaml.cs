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
    /// Lógica de interacción para FrmReservas.xaml
    /// </summary>
    public partial class FrmReservas : Window
    {
        public FrmReservas()
        {
            InitializeComponent();
        }


        //Instancia de la clases
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Reservas Reservas = new CDo_Reservas();
        CE_Reservas Reserva = new CE_Reservas();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }
        
        //Método para mostrar todas las reservas
        private void CargarDatos()
        {
            DataGridReserva.ItemsSource = Reservas.MostrarReservas().AsDataView();
            DataGridReserva.UnselectAllCells();

        }
        private void DataGridReserva_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Reserva = e.Column.Header.ToString();

            if (Id_Reserva == "Id_Reserva")
            {
                e.Cancel = true;

            }

            string Id_Cliente = e.Column.Header.ToString();

            if (Id_Cliente == "Id_Cliente")
            {
                e.Cancel = true;

            }

        }

        private void AgRes_UpdateEventHandler(object sender, FrmAgregarReserva.UpdateEventArgs args)
        {
            CargarDatos();
        }
        //private void AnRes_UpdateEventHandler(object sender, FrmAnularVentas.UpdateEventArgs args)
        //{
        //    CargarDatos();
        //}


        private void BtnNuevaReserva_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregarReserva AgregarReservas = new FrmAgregarReserva(this);
            AgregarReservas.UpdateEventHandler += AgRes_UpdateEventHandler;
            AgregarReservas.ShowDialog();
        }

        private void BtnAnularReserva_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnImprimirInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGridReserva_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
