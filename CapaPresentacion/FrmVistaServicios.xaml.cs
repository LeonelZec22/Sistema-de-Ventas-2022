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
    /// Lógica de interacción para FrmVistaServicios.xaml
    /// </summary>
    public partial class FrmVistaServicios : Window
    {
        public FrmVistaServicios()
        {
            InitializeComponent();
        }

        CD_Conexion Con = new CD_Conexion();


        CDo_Servicios Servicios = new CDo_Servicios();
        CE_Servicios Servicio = new CE_Servicios();
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            DataGridGestionServicios.ItemsSource = Procedimientos.CargarDatos("Servicios").AsDataView();
            DataGridGestionServicios.UnselectAllCells();
        }

        private void DataGridGestionServicios_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Servicios = e.Column.Header.ToString();

            if (Id_Servicios == "Id_Servicios")
            {
                e.Cancel = true;
            }
        }

        private void BtnSeleccionarServicio_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridGestionServicios.SelectedItems.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Servicio en la lista de Servicios!!", "Seleccionar Servicio", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }

            else
            {

                DialogResult = true;

                Hide();
            }
        }

        private void BtnCancelarSer_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            DataGridGestionServicios.UnselectAllCells();
        }

        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void DataGridGestionServicios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Hide();
            DataGridGestionServicios.UnselectAllCells();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Hide();
            DataGridGestionServicios.UnselectAllCells();
        }
    }
}
