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

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para VistaReservas.xaml
    /// </summary>
    public partial class VistaReservas : Window
    {
        public VistaReservas()
        {
            InitializeComponent();
        }

        CD_Conexion Con = new CD_Conexion();


        CDo_Reservas Reservas = new CDo_Reservas();
        CE_Reservas Reserva = new CE_Reservas();
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            DataGridGestionReserva.ItemsSource = Procedimientos.CargarDatos("Reserva").AsDataView();
            DataGridGestionReserva.UnselectAllCells();
        }

        private void DataGridGestionReserva_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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

            if (e.PropertyType == typeof(System.DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
            }
        }

        private void BtnSeleccionarReserva_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridGestionReserva.SelectedItems.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Debe de Seleccionar una Reserva en la lista reserva!!", "Seleccionar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }

            else
            {

                DialogResult = true;

                Hide();
            }
        }

        private void BtnCancelarReser_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            DataGridGestionReserva.UnselectAllCells();
        }

        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

      
        private void DataGridGestionReserva_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Hide();
            DataGridGestionReserva.UnselectAllCells();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Hide();
            DataGridGestionReserva.UnselectAllCells();
        }
    }
}
