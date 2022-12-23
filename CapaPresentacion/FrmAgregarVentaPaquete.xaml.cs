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
using CapaEntidad.Caches;
using CapaNegocio;
using System.Data;
using System.Reflection;


namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para FrmAgregarVentaPaquete.xaml
    /// </summary>
    public partial class FrmAgregarVentaPaquete : Window
    {
        public FrmAgregarVentaPaquete()
        {
            InitializeComponent();
        }

        public FrmAgregarVentaPaquete(FrmVenta_Paquete Ventas)
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GenerarCorrelativos();
            dtp_FechaVenta.SelectedDate = DateTime.Today;
            txtCantidadUsar.Focus();
        }

        #region Instancia de objetos a usar

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Venta_Paquetes Ventas = new CDo_Venta_Paquetes();
        CE_Ventas_Paquetes Venta = new CE_Ventas_Paquetes();

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

        #region Seleccionar un Cliente 

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


        #region Seleccionar un Paquete

        //Método para cargar en los textBox el producto
        private void SeleccionarPaquete()
        {

            //Abrimos el Formulario de VistaProveedor


            VistaPaquetes VistaPaquete = new VistaPaquetes();

            VistaPaquete.ShowDialog();

            txtId_Paquetes.Text = "";

            txtNombre_Paquete.Text = "";

            txtCantidadVendida.Text = "";

            txtPrecio_Venta.Text = "";

            try
            {
                if (VistaPaquete.DialogResult == true)
                {

                    //Lenamos los textbox 

                    foreach (DataRowView drv in VistaPaquete.DataGridGestionPaquete.SelectedItems)
                    {
                        DataRow row = drv.Row;

                        var IdPaquete = Convert.ToString(drv.Row[0]);

                        txtId_Paquetes.Text = IdPaquete;

                        var NombrePaquete = Convert.ToString(drv.Row[2]);

                        txtNombre_Paquete.Text = NombrePaquete;

                        var CantidadVendida = Convert.ToString(drv.Row[4]);

                        txtCantidadVendida.Text = CantidadVendida;

                        var PrecioVenta = Convert.ToString(drv.Row[5]);

                        txtPrecio_Venta.Text = PrecioVenta;

                    }

                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Paquete en la lista!!", "Seleccionar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }


        private void BtnBuscarPaquete_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarPaquete();
        }

        #endregion

        #region Método para guardar la Venta en la Base de datos

        private void GenerarCorrelativos()
        {
            txtId_Venta_Paquetes.Text = Procedimientos.GenerarCodigoId("Ventas_Paquetes");
        }

        public virtual bool Guardar()
        {
            try
            {
                if (txtId_Cliente.Text == string.Empty || txtId_Paquetes.Text == string.Empty || txtNombre_Paquete.Text == string.Empty || txtClienteNombre.Text == string.Empty || txtCantidadVendida.Text == string.Empty || txtPrecio_Venta.Text == string.Empty || txtCantidadUsar.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos del formulario!!", "Agregar Detalle Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    //GenerarCorrelativos();

                    if(Convert.ToInt32(txtCantidadVendida.Text) > Convert.ToInt32(txtCantidadUsar.Text))
                    {
                        Venta.Id_Cliente = Convert.ToInt32(txtId_Cliente.Text);
                        Venta.Id_Paquetes = Convert.ToInt32(txtId_Paquetes.Text);
                        Venta.Cliente = Convert.ToString(txtClienteNombre.Text);
                        Venta.Paquete = Convert.ToString(txtNombre_Paquete.Text);
                        Venta.Fecha_Venta = Convert.ToDateTime(dtp_FechaVenta.Text);
                        Venta.Cantidad_Vendida = Convert.ToInt32(txtCantidadVendida.Text);
                        Venta.Cantidad_Usada = Convert.ToInt32(txtCantidadUsar.Text);
                        Venta.Estado = "Emitido";
                        Venta.Precio_Venta = Convert.ToDecimal(txtPrecio_Venta.Text);
                        Venta.Id_Venta_Paquetes = Convert.ToInt32(txtId_Venta_Paquetes.Text);
                        Ventas.AgregarVentaPaquetes(Venta);
                        System.Windows.Forms.MessageBox.Show("Venta de Paquete agregada correctamente!!", "Agregar Venta Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        Agregar();
                        LimpiarDetalle();
                        Hide();
                        return true;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("La Cantidad de citas a usar no puede ser mayor que la cantidad de citas compradas", "Agregar Detalle Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("La venta de paquetes no fue agregado por: " + ex, "Agregar Venta de Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
        }
        #endregion

        private void LimpiarDetalle()
        {
            txtId_Venta_Paquetes.Text = string.Empty;
            txtId_Cliente.Text = string.Empty;
            txtClienteNombre.Text = string.Empty;
            txtId_Paquetes.Text = string.Empty;
            txtNombre_Paquete.Text = string.Empty;
            txtCantidadVendida.Text = string.Empty;
            txtCantidadUsar.Text = Convert.ToString(0);
            txtPrecio_Venta.Text = string.Empty;

            btnBuscarPaquete.Focus();
        }

        private void BtnGuardarVenta_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        private void BtnCancelarVenta_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            LimpiarDetalle();
            this.Hide();
        }


        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LimpiarDetalle();
            this.Hide();
        }
    }
}
