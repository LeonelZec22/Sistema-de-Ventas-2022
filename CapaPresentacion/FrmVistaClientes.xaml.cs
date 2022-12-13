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
    /// Lógica de interacción para FrmVistaClientes.xaml
    /// </summary>
    public partial class FrmVistaClientes : Window
    {
        public FrmVistaClientes()
        {
            InitializeComponent();
        }


        CD_Conexion Con = new CD_Conexion();


        CDo_Clientes Clientes = new CDo_Clientes();
        CE_Clientes Cliente = new CE_Clientes();
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            DataGridGestionClientes.ItemsSource = Procedimientos.CargarDatos("Clientes").AsDataView();
            //DataGridGestionClientes.ItemsSource = Clientes.Mo("Clientes").AsDataView();
            DataGridGestionClientes.UnselectAllCells();
        }

        private void DataGridGestionClientes_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Cliente = e.Column.Header.ToString();

            if (Id_Cliente == "Id_Cliente")
            {
                e.Cancel = true;
            }
        }

        private void DataGridGestionClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void BtnSeleccionarCliente_Click(object sender, RoutedEventArgs e)
        {

            if (DataGridGestionClientes.SelectedItems.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Cliente en la lista clientes!!", "Seleccionar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }

            else
            {

                DialogResult = true;

                Hide();
            }
        }

        private void BtnCancelarCli_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            DataGridGestionClientes.UnselectAllCells();
        }


        private void TxtBuscadorr_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar();
        }

        public virtual void Buscar()
        {
            try
            {
                if (cboVistaClientes.Text == "Codigo")
                {
                    Cliente.Buscar = txtBuscadorr.Text.Trim();
                    DataGridGestionClientes.ItemsSource = Clientes.Buscar_Cliente_Codigo(Cliente).AsDataView();
                }
                else if (cboVistaClientes.Text == "Nombre")
                {
                    Cliente.Buscar = txtBuscadorr.Text.Trim();
                    DataGridGestionClientes.ItemsSource = Clientes.Buscar_Cliente_Nombre(Cliente).AsDataView();
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Cliente no fue encontrado por: " + ex.Message, "Buscar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Hide();
            DataGridGestionClientes.UnselectAllCells();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Hide();
            DataGridGestionClientes.UnselectAllCells();
        }
    }
}
