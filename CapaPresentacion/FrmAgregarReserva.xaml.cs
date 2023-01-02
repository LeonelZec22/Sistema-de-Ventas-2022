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
    /// Lógica de interacción para FrmAgregarReserva.xaml
    /// </summary>
    public partial class FrmAgregarReserva : Window
    {
        public FrmAgregarReserva()
        {
            InitializeComponent();
        }

        public FrmAgregarReserva(FrmReservas Reservas)
        {
            InitializeComponent();
        }

        public FrmAgregarReserva(VistaReservas Reservas)
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDescuentoVenta.Text = "0.00";
            txtMontoTotal.Text = "0.00";
            GenerarCorrelativos();
            dtp_FechaReserva.SelectedDate = DateTime.Today;
        }

        #region Instancia de objetos a usar

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Reservas Reservas = new CDo_Reservas();
        CE_Reservas Reserva = new CE_Reservas();
        CDo_Detalle_Reservas DetalleReservas = new CDo_Detalle_Reservas();
        CE_Detalle_Reservas DetalleReserva = new CE_Detalle_Reservas();


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



        #region Método para calcular el descuento

        public double importe = 0, ImporteNeto = 0, Descuento1 = 0, Monto_Total = 0, Porcentaje = 0;

        private void Descuento()
        {
            try
            {
                if (txtDescuento.Text != string.Empty)
                {
                    importe = Convert.ToDouble(txtPrecio_Venta.Text);
                    Descuento1 = importe * Convert.ToDouble(txtDescuento.Text) / 100;
                    ImporteNeto = importe - Convert.ToDouble(Descuento1.ToString("N2"));
                    Monto_Total = Convert.ToDouble(ImporteNeto);
                    Porcentaje = Descuento1;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("El campo Descuento no puede estar vacio", "Calcular Descuento", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error al calcular Descuento por: " + ex.Message, "Calcular Descuento", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region Seleccionar un Cliente 

        private void SeleccionarCliente()
        {

            //Abrimos el Formulario de VistaClientes

            FrmVista_Reserva_Clientes VistaCliente = new FrmVista_Reserva_Clientes();

            VistaCliente.ShowDialog();

            txtId_Cliente.Text = "";

            txtClienteNombre.Text = "";

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

        #region Seleccionar un Servicio

        private void SeleccionarServicio()
        {

            //Abrimos el Formulario de VistaProveedor


            FrmVistaServicios VistaServicios = new FrmVistaServicios();

            VistaServicios.ShowDialog();

            txtId_Servicios.Text = "";

            txtNombre_Servicio.Text = "";

            txtPrecio_Venta.Text = "";

            try
            {
                if (VistaServicios.DialogResult == true)
                {

                    //Lenamos los textbox 

                    foreach (DataRowView drv in VistaServicios.DataGridGestionServicios.SelectedItems)
                    {
                        DataRow row = drv.Row;

                        var IdServicio = Convert.ToString(drv.Row[0]);

                        txtId_Servicios.Text = IdServicio;

                        var NombreServicio = Convert.ToString(drv.Row[2]);

                        txtNombre_Servicio.Text = NombreServicio;

                        var PrecioVenta = Convert.ToString(drv.Row[4]);

                        txtPrecio_Venta.Text = PrecioVenta;

                        

                    }

                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Producto en la lista Producto!!", "Seleccionar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
        private void BtnBuscarServicio_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarServicio();
        }

        #endregion

        #region Agregar Datos al DataGrid 
        DataSet dataSet;
        DataTable TableServicio;

        public static int ContFila = 0;
        decimal TotalResta1 = 0, TotalResta2 = 0;
        int indexDelete = 0;

        private void AgregarDetalle()
        {
            try
            {
                if (txtId_Servicios.Text == string.Empty ||  txtNombre_Servicio.Text == string.Empty || txtEstado.Text == string.Empty || txtPrecio_Venta.Text == string.Empty || txtDescuento.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos del detalle de servicio!!", "Agregar Detalle", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                
                else
                {
                    if (txtId_Servicios.Text == string.Empty || txtNombre_Servicio.Text == string.Empty || txtEstado.Text == string.Empty || txtPrecio_Venta.Text == string.Empty || txtDescuento.Text == string.Empty)
                    {
                        System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos del detalle de servicio!!", "Agregar Detalle", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                        return;
                    }
                    else
                    {
                        Descuento();
                        //bool Existe = false;
                        //int no_fila = 0;

                        if (ContFila == 0)
                        {
                            if (TableServicio == null)
                            {
                                //Creamos el Data Table porque no existe todavia
                                TableServicio = new DataTable();

                                //Creamos las columnas del Data Table 
                                DataColumn column;
                                //DataRow row;

                                column = new DataColumn();
                                column.ColumnName = "Id_Servicio";
                                TableServicio.Columns.Add(column);

                                column = new DataColumn();
                                column.ColumnName = "Nombre";
                                TableServicio.Columns.Add(column);

                                column = new DataColumn();
                                column.ColumnName = "Estado";
                                TableServicio.Columns.Add(column);

                                column = new DataColumn();
                                column.ColumnName = "Descuento";
                                TableServicio.Columns.Add(column);

                                column = new DataColumn();
                                column.ColumnName = "Total";
                                TableServicio.Columns.Add(column);

                                dataSet = new DataSet();

                                dataSet.Tables.Add(TableServicio);

                                //Creamos una fila del Data Table utilizando la información ya capturada
                                TableServicio.Rows.Add(txtId_Servicios.Text, txtNombre_Servicio.Text, txtEstado.Text, Descuento1.ToString("N2"), Monto_Total.ToString("N2"));

                                DataGridReserva.ItemsSource = TableServicio.DefaultView;
                                LimpiarDetalle();
                                ContFila++;
                                btnBuscarCliente.IsEnabled = false;
                                dtp_FechaReserva.IsEnabled = false;
                            }

                            else
                            {
                                TableServicio.Rows.Add(txtId_Servicios.Text, txtNombre_Servicio.Text, txtEstado.Text, Descuento1.ToString("N2"), Monto_Total.ToString("N2"));

                                DataGridReserva.ItemsSource = TableServicio.DefaultView;
                                LimpiarDetalle();
                                ContFila++;
                                DataGridReserva.UnselectAllCells();
                                btnBuscarCliente.IsEnabled = false;
                                dtp_FechaReserva.IsEnabled = false;
                            }
                        }

                        else
                        {
                            if (DataGridReserva.Items.Count >= 1)
                            {
                                System.Windows.Forms.MessageBox.Show("Solo se puede agregar una reserva a la vez", "Agregar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            }
                            else
                            {

                                TableServicio.Rows.Add(txtId_Servicios.Text, txtNombre_Servicio.Text, txtEstado.Text, Descuento1.ToString("N2"), Monto_Total.ToString("N2"));

                                DataGridReserva.ItemsSource = TableServicio.DefaultView;
                                LimpiarDetalle();
                                ContFila++;
                                DataGridReserva.UnselectAllCells();
                                btnBuscarCliente.IsEnabled = false;
                                dtp_FechaReserva.IsEnabled = false;
                            }
                        }

                        decimal Total1 = 0, Total2 = 0;
                        foreach (DataRowView row3 in DataGridReserva.ItemsSource)
                        {
                            Total1 += Convert.ToDecimal(row3[3]);
                            Total2 += Convert.ToDecimal(row3[4]);
                        }
                        
                        txtDescuentoVenta.Text = Total1.ToString("N2");
                        txtMontoTotal.Text = Total2.ToString("N2");

                        TotalResta1 = Total1;
                        TotalResta2 = Total2;
                    }
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("La Reseva no fue agregada por: " + ex, "Agregar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void BtnAgregarServicio_Click(object sender, RoutedEventArgs e)
        {
            AgregarDetalle();
        }


        #endregion

        
        #region Eliminar Datos del DataGrid

        private void EliminarDetalle()
        {
            try
            {
                if (ContFila > 0 && DataGridReserva.Items.Count > 0)
                {
                    if (DataGridReserva.SelectedItems.Count == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Debe de seleccionar el producto a eliminar de la tabla!!", "Eliminar Servicios", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }

                    else
                    {
                        //Va recorrer la fila seleccionada en busca de obtener su index
                        foreach (DataRowView drv in DataGridReserva.SelectedItems)
                        {

                            DataRow row = drv.Row;

                            indexDelete = TableServicio.Rows.IndexOf(row);

                        }
                        

                        //Restar Descuento 
                        TotalResta1 = TotalResta1 - Convert.ToDecimal(TableServicio.Rows[indexDelete][3]);
                        txtDescuentoVenta.Text = TotalResta2.ToString("N2");

                        // Restar Monto Total
                        TotalResta2 = TotalResta2 - Convert.ToDecimal(TableServicio.Rows[indexDelete][4]);
                        txtMontoTotal.Text = TotalResta2.ToString("N2");

                        TableServicio.Rows.RemoveAt(indexDelete);
                        DataGridReserva.ItemsSource = TableServicio.DefaultView;
                        DataGridReserva.UnselectAllCells();
                        ContFila--;
                        btnBuscarCliente.IsEnabled = true;
                        dtp_FechaReserva.IsEnabled = true;
                    }
                }

                else
                {
                    System.Windows.Forms.MessageBox.Show("No hay servicios para eliminar!!", "Eliminar Servicio", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se ha podido eliminar el Servicio porque: " + ex, "Eliminar Servicio", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void BtnEliminarServicio_Click(object sender, RoutedEventArgs e)
        {
            EliminarDetalle();
        }

        #endregion


        #region Método para guardar la Venta en la Base de datos
        private void BtnGuardarServicio_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        private void GenerarCorrelativos()
        {
            try
            {
                txtId_Reserva.Text = Procedimientos.GenerarCodigoId("Reserva");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Id de Reserva no fue generado por: " + ex, "Agregar Reservas", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            LimpiarDetalle();
            LimpiarCampo();
            limpiarFila();
            TableServicio = null;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
            LimpiarCampo();
            limpiarFila();
            TableServicio = null;
        }

      

        public virtual bool Guardar()
        {
            try
            {
                if (txtClienteNombre.Text == string.Empty || txtId_Cliente.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos por favor!!", "Agregar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {

                    Reserva.Id_Cliente = Convert.ToInt32(txtId_Cliente.Text);
                    Reserva.Nombre = Convert.ToString(txtClienteNombre.Text);
                    Reserva.Fecha_Reserva = Convert.ToDateTime(dtp_FechaReserva.Text);
                    Reserva.Descuento = Convert.ToDecimal(txtDescuentoVenta.Text);
                    Reserva.Monto_Total = Convert.ToDecimal(txtMontoTotal.Text);
                    Reserva.Estado = "Pendiente";

                    GenerarCorrelativos();

                    if (DataGridReserva.Items.Count == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Por Favor Agregue un Servicio a la Tabla", "Agregar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }

                    else
                    {

                        foreach (DataRowView drv in DataGridReserva.ItemsSource)
                        {
                            DataRow row = drv.Row;

                            DetalleReserva.Id_Reserva = Convert.ToInt32(txtId_Reserva.Text);
                            DetalleReserva.Id_Servicios = Convert.ToInt32(row[0].ToString());
                            DetalleReserva.Estado = Convert.ToString(row[2].ToString());
                            DetalleReserva.Descuento = Convert.ToDecimal(row[3].ToString());
                            DetalleReserva.Monto_Total = Convert.ToDecimal(row[4].ToString());

                            DetalleReservas.AgregarDetalleReserva(DetalleReserva);
                        }

                        Reservas.AgregarReserva(Reserva);

                        System.Windows.Forms.MessageBox.Show("Reserva de Servicio agregada correctamente!!", "Agregar Reserva de Servicio", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        txtDescuentoVenta.Text = "0.00";
                        txtMontoTotal.Text = "0.00";
                        Agregar();
                        LimpiarDetalle();
                        LimpiarCampo();
                        limpiarFila();
                        Hide();
                        return true;
                    }
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("La Reserva de Servicio no fue agregado por: " + ex, "Agregar Ingreso Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
        }

        private void BtnCancelarServicio_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LimpiarDetalle();
            LimpiarCampo();
            limpiarFila();
            TableServicio = null;
        }

        #endregion

        private void TxtDescuento_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtDescuento.Text == string.Empty)
            {
                return;
            }
            else
            {
                if (txtDescuento.Text.Length > 4)
                {
                    System.Windows.Forms.MessageBox.Show("El Descuento  no puede ser mayor a 4 caracteres", "Agregar Ingreso de Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    try
                    {
                        if (Convert.ToInt32(txtDescuento.Text) >= 0)
                        {
                            Procedimientos.FormatoEntero(txtDescuento);
                            Descuento();
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("El Descuento no puede ser mayor a cero", "Agregar Ingreso de Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                            txtDescuento.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("El Descuento no es un numero por favor ingrese solo numeros", "Agregar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        txtDescuento.Clear();
                    }
                }
                
            }
        }

        private void TxtDescuento_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key == Key.Space) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de letras y espacios", "Agregar Reservas", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void TxtDescuento_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("El Descuento solo se puede ingresar por medio del teclado", "Agregar Reservas", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
        }

        private void LimpiarDetalle()
        {
            txtNombre_Servicio.Text = string.Empty;
            txtPrecio_Venta.Text = string.Empty;
            txtDescuento.Text = string.Empty;
            btnBuscarServicio.Focus();
        }

        private void LimpiarCampo()
        {
            txtId_Servicios.Text = string.Empty;
            txtId_Cliente.Text = string.Empty;
            txtNombre_Servicio.Text = string.Empty;
        }

        public void limpiarFila()
        {
            ContFila = 0;
        }
    }
}
