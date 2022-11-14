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
    /// Lógica de interacción para FrmAgregarVenta.xaml
    /// </summary>
    public partial class FrmAgregarVenta : Window
    {
        public FrmAgregarVenta()
        {
            InitializeComponent();
        }

        public FrmAgregarVenta(FrmVentas Ventas)
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


        protected void Agregar()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void GenerarCorrelativo()
        {
            
        }


        #region Seleccionar un Clientee 

        private void SeleccionarCliente()
        {

            //Abrimos el Formulario de VistaClientes

            FrmVistaClientes VistaCliente = new FrmVistaClientes();

            VistaCliente.ShowDialog();

            txtId_Cliente.Text = "";

            txtClienteNombre.Text = "";

            var Datos = VistaCliente.DataGridGestionClientes;
            
            try
            {
                if (VistaCliente.DialogResult == true)
                {


                    foreach (DataRowView drv in VistaCliente.DataGridGestionClientes.SelectedItems)
                    {
                        DataRow row = drv.Row;

                        var IdCliente = Convert.ToString(drv.Row[0]);

                        txtId_Cliente.Text = IdCliente;

                        var NombreCliente = Convert.ToString(drv.Row[2]);

                        txtClienteNombre.Text = NombreCliente;
                    }
                    

                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Cliente en la lista Clientes!!", "Seleccionar Clientes", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void BtnBuscarCliente_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarCliente(); 
        }

        #endregion
        DataSet dataSet;
        DataTable TableProductos;

        private void BtnBuscarProducto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEliminarProducto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnGuardarVenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancelarVenta_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
