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
    /// Lógica de interacción para VistaPaquetes.xaml
    /// </summary>
    public partial class VistaPaquetes : Window
    {
        public VistaPaquetes()
        {
            InitializeComponent();
        }

        CD_Conexion Con = new CD_Conexion();


        CDo_Paquetes Paquetes = new CDo_Paquetes();
        CE_Paquetes Paquete = new CE_Paquetes();
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

       
        private void CargarDatos()
        {
            DataGridGestionPaquete.ItemsSource = Procedimientos.CargarDatos("Paquetes").AsDataView();
            DataGridGestionPaquete.UnselectAllCells();
        }

        private void DataGridGestionPaquete_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Paquetes = e.Column.Header.ToString();

            if (Id_Paquetes == "Id_Paquetes")
            {
                e.Cancel = true;
            }

        }


        private void BtnSeleccionarPaquete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridGestionPaquete.SelectedItems.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Paquete en la lista!!", "Seleccionar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }

            else
            {

                DialogResult = true;
                
                Hide();
            }
        }

        private void BtnCancelarPaq_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            DataGridGestionPaquete.UnselectAllCells();
        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Hide();
            DataGridGestionPaquete.UnselectAllCells();
        }

      

        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void DataGridGestionPaquete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void Window_Closed(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
