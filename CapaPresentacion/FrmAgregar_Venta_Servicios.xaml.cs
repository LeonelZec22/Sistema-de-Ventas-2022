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
    /// Lógica de interacción para FrmAgregar_Venta_Servicios.xaml
    /// </summary>
    public partial class FrmAgregar_Venta_Servicios : Window
    {
        public FrmAgregar_Venta_Servicios()
        {
            InitializeComponent();
        }

        public FrmAgregar_Venta_Servicios(FrmVentas_Servicios Ventas)
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDescuentoVenta.Text = "0.00";
            txtMontoTotal.Text = "0.00";
            Usuarios.DatosUsuario(MainWindow.Usuario);
            tbUsuario.Text = Convert.ToString(InformacionUsuario.IdUsuario);
            dtp_FechaVenta.SelectedDate = DateTime.Today;
            GenerarCorrelativos();
        }

        #region Instancia de objetos a usar

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Ventas_Servicios VentasServicios = new CDo_Ventas_Servicios();
        CE_Ventas_Servicios VentaServicio = new CE_Ventas_Servicios();
        CDo_Detalle_Ventas_Servicioscs DetalleVentasServicios = new CDo_Detalle_Ventas_Servicioscs();
        CE_Detalle_Venta_Servicios DetalleVentaServicios = new CE_Detalle_Venta_Servicios();

        CDo_Usuarios Usuarios = new CDo_Usuarios();
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
                    importe = Convert.ToDouble(txtMontoTotal1.Text);
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

        #region Seleccionar una Reserva

        private void SeleccionarReserva()
        {

            //Abrimos el Formulario de VistaProveedor


            VistaReservas VistaReserva = new VistaReservas();

            VistaReserva.ShowDialog();

            txtId_Reserva.Text = "";

            txtId_Cliente.Text = "";

            txtClienteNombre.Text = "";

            txtFechaReserva.Text = "";

            txtMontoTotal1.Text = "";

            try
            {
                if (VistaReserva.DialogResult == true)
                {

                    //Lenamos los textbox 

                    if (VistaReserva.DataGridGestionReserva.SelectedItems.Count != 0)
                    {
                       
                        foreach (DataRowView drv in VistaReserva.DataGridGestionReserva.SelectedItems)
                        {
                            DataRow row = drv.Row;

                            var Estado = Convert.ToString(drv.Row[4]);

                            if (Estado == "Finalizada" || Estado =="Cancelada")
                            {
                                System.Windows.Forms.MessageBox.Show("Solo se pueden seleccionar Reservas que estén pendientes!!", "Seleccionar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                                break;
                                
                            }
                            else
                            {
                                var IdReserva = Convert.ToString(drv.Row[0]);

                                txtId_Reserva.Text = IdReserva;

                                var IdCliente = Convert.ToString(drv.Row[1]);

                                txtId_Cliente.Text = IdCliente;

                                var NombreReserva = Convert.ToString(drv.Row[2]);

                                txtClienteNombre.Text = NombreReserva;

                                var FechaReserva = Convert.ToString(drv.Row[3]);

                                txtFechaReserva.Text = FechaReserva;

                                var MontoTotal = Convert.ToString(drv.Row[6]);

                                txtMontoTotal1.Text = MontoTotal;

                            }

                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Debe de Seleccionar una Reserva en la lista Reserva!!", "Seleccionar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }

                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Ha ocurrido un error al seleccionar una Reserva!" + ex.Message, "Seleccionar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void BtnBuscarReserva_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarReserva();
        }

        #endregion

        #region Agregar Datos al DataGrid 
        DataSet dataSet;
        DataTable TableReservas;

        public static int ContFila = 0;
        decimal  TotalResta2 = 0, TotalResta3 = 0;
        int indexDelete = 0;

        private void AgregarDetalle()
        {
            try
            {
                if (txtId_Reserva.Text == string.Empty || txtFechaReserva.Text == string.Empty || txtClienteNombre.Text == string.Empty || txtEstado.Text == string.Empty || txtMontoTotal1.Text == string.Empty || txtDescuento.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos del detalle de la reserva!!", "Agregar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (txtId_Reserva.Text == string.Empty || txtFechaReserva.Text == string.Empty || txtClienteNombre.Text == string.Empty || txtEstado.Text == string.Empty || txtMontoTotal1.Text == string.Empty || txtDescuento.Text == string.Empty)
                    {
                        System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos del detalle de reserva!!", "Agregar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                        return;
                    }
                    else
                    {
                        //Descuento();
                       
                        if (ContFila == 0)
                        {
                            if (TableReservas == null)
                            {
                                //Creamos el Data Table porque no existe todavia
                                TableReservas = new DataTable();

                                //Creamos las columnas del Data Table 
                                DataColumn column;

                                column = new DataColumn();
                                column.ColumnName = "Id_Reserva";

                                TableReservas.Columns.Add(column);

                                column = new DataColumn();
                                column.ColumnName = "Nombre";
                                TableReservas.Columns.Add(column);

                                column = new DataColumn();
                                column.ColumnName = "Fecha_Reserva";
                                TableReservas.Columns.Add(column);

                                column = new DataColumn();
                                column.ColumnName = "Estado";
                                TableReservas.Columns.Add(column);
                                column = new DataColumn();

                                column.ColumnName = "Descuento";
                                TableReservas.Columns.Add(column);
                                column = new DataColumn();

                                column.ColumnName = "Monto_Total";
                                TableReservas.Columns.Add(column);
                                column = new DataColumn();

                                dataSet = new DataSet();

                                dataSet.Tables.Add(TableReservas);

                                //Creamos una fila del Data Table utilizando la información ya capturada
                                TableReservas.Rows.Add(txtId_Reserva.Text, txtClienteNombre.Text, txtFechaReserva.Text, txtEstado.Text, Descuento1.ToString("N2"), Monto_Total.ToString("N2"));

                                DataGridVentaServicios.ItemsSource = TableReservas.DefaultView;
                                LimpiarDetalle();
                                ContFila++;
                                txtEstado.Text = "Finalizado";
                                btnBuscarReserva.IsEnabled = false;
                                dtp_FechaVenta.IsEnabled = false;
                            }

                            else
                            {
                                TableReservas.Rows.Add(txtId_Reserva.Text, txtClienteNombre.Text, txtFechaReserva.Text, txtEstado.Text, Descuento1.ToString("N2"), Monto_Total.ToString("N2"));

                                DataGridVentaServicios.ItemsSource = TableReservas.DefaultView;

                                DataGridVentaServicios.UnselectAllCells();
                                LimpiarDetalle();
                                ContFila++;
                                txtEstado.Text = "Finalizado";
                                btnBuscarReserva.IsEnabled = false;
                                dtp_FechaVenta.IsEnabled = false;
                            }
                        }

                        else
                        {
                            if (DataGridVentaServicios.Items.Count >= 1)
                            {
                                System.Windows.Forms.MessageBox.Show("Solo se puede agregar una reserva a la vez", "Agregar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            }

                            else
                            {

                                TableReservas.Rows.Add(txtId_Reserva.Text, txtClienteNombre.Text, txtFechaReserva.Text, txtEstado.Text, Descuento1.ToString("N2"), Monto_Total.ToString("N2"));

                                DataGridVentaServicios.ItemsSource = TableReservas.DefaultView;

                                DataGridVentaServicios.UnselectAllCells();
                                LimpiarDetalle();
                                ContFila++;
                                txtEstado.Text = "Finalizado";
                                btnBuscarReserva.IsEnabled = false;
                                dtp_FechaVenta.IsEnabled = false;
                            }
                        }

                        decimal  Total2 = 0, Total3 = 0;
                        foreach (DataRowView row3 in DataGridVentaServicios.ItemsSource)
                        {
                           
                            Total2 += Convert.ToDecimal(row3[4]);
                            Total3 += Convert.ToDecimal(row3[5]);
                        }
                        
                        txtDescuentoVenta.Text = Total2.ToString("N2");
                        txtMontoTotal.Text = Total3.ToString("N2");
                        
                        TotalResta2 = Total2;
                        TotalResta3 = Total3;
                    }
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("La Venta  de Reserva no fue agregada por: " + ex, "Agregar Venta Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void BtnAgregarReserva_Click(object sender, RoutedEventArgs e)
        {
            AgregarDetalle();
        }


        #endregion

        #region Eliminar Datos del DataGrid

        private void EliminarDetalle()
        {
            try
            {
                if (ContFila > 0 && DataGridVentaServicios.Items.Count > 0)
                {
                    if (DataGridVentaServicios.SelectedItems.Count == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Debe de seleccionar la reserva a eliminar de la tabla!!", "Eliminar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }

                    else
                    {
                        //Va recorrer la fila seleccionada en busca de obtener su index
                        foreach (DataRowView drv in DataGridVentaServicios.SelectedItems)
                        {

                            DataRow row = drv.Row;

                            indexDelete = TableReservas.Rows.IndexOf(row);

                        }

                        //Restar Descuento 
                        TotalResta2 = TotalResta2 - Convert.ToDecimal(TableReservas.Rows[indexDelete][4]);
                        txtDescuentoVenta.Text = TotalResta2.ToString("N2");

                        // Restar Monto Total
                        TotalResta3 = TotalResta3 - Convert.ToDecimal(TableReservas.Rows[indexDelete][5]);
                        txtMontoTotal.Text = TotalResta3.ToString("N2");

                        TableReservas.Rows.RemoveAt(indexDelete);
                        DataGridVentaServicios.ItemsSource = TableReservas.DefaultView;
                        DataGridVentaServicios.UnselectAllCells();
                        txtEstado.Text = "Finalizado";
                        ContFila--;
                        btnBuscarReserva.IsEnabled = true;
                        txtClienteNombre.Clear();
                        dtp_FechaVenta.IsEnabled = true;
                    }
                }

                else
                {
                    System.Windows.Forms.MessageBox.Show("No hay Reserva para eliminar!!", "Eliminar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se ha podido eliminar la Reserva porque: " + ex, "Eliminar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void BtnEliminarReserva_Click(object sender, RoutedEventArgs e)
        {
            EliminarDetalle();
        }

        #endregion

        #region Método para guardar la Venta en la Base de datos
        private void BtnGuardarVentaServicios_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        private void GenerarCorrelativos()
        {
            try
            {
                txtId_Venta_Servicios.Text = Procedimientos.GenerarCodigoId("Ventas_Servicios");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se ha podido Generar el codigo de la Reserva porque: " + ex, "Codigo de Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
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

                    VentaServicio.Id_Cliente = Convert.ToInt32(txtId_Cliente.Text);
                    VentaServicio.Fecha_Venta = Convert.ToDateTime(dtp_FechaVenta.Text);
                    VentaServicio.Descuento = Convert.ToDecimal(txtDescuentoVenta.Text);
                    VentaServicio.Monto_Total = Convert.ToDecimal(txtMontoTotal.Text);
                    VentaServicio.Estado = "Emitida";
                    VentaServicio.Id_Usuario = Convert.ToInt32(tbUsuario.Text);

                   

                    if (DataGridVentaServicios.Items.Count == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Por Favor Agregue una Reserva a la Tabla", "Agregar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }
                    else
                    {

                   
                        foreach (DataRowView drv in DataGridVentaServicios.ItemsSource)
                        {
                            DataRow row = drv.Row;

                            DetalleVentaServicios.Id_Venta_Servicios = Convert.ToInt32(txtId_Venta_Servicios.Text);
                            DetalleVentaServicios.Id_Reserva = Convert.ToInt32(row[0].ToString());
                            DetalleVentaServicios.Fecha_Reserva = Convert.ToDateTime(row[2].ToString());
                            DetalleVentaServicios.Estado = Convert.ToString(row[3].ToString());
                            DetalleVentaServicios.Descuento = Convert.ToDecimal(row[4].ToString());
                            DetalleVentaServicios.Monto_Total = Convert.ToDecimal(row[5].ToString());

                            DetalleVentasServicios.AgregarDetalleVentaServicios(DetalleVentaServicios);
                        }
                        VentasServicios.AgregarVentaServicios(VentaServicio);

                        System.Windows.Forms.MessageBox.Show("Venta de Reserva agregada correctamente!!", "Agregar Venta Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                   
                        txtDescuentoVenta.Text = "0.00";
                        txtMontoTotal.Text = "0.00";
                        Agregar();
                        LimpiarDetalle();
                        LimpiarCampo();
                        limpiarFila();
                        Hide();
                        TableReservas = null;
                        return true;
                    }
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Ingreso de Producto no fue agregado por: " + ex, "Agregar Ingreso Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
        }

        #endregion


        #region Botones para salir de la pantalla
        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            limpiarFila();
            LimpiarDetalle();
            LimpiarCampo();
            TableReservas = null;
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            limpiarFila();
            LimpiarDetalle();
            LimpiarCampo();
            TableReservas = null;
            this.Hide();
        }

        private void BtnCancelarVentaServicios_Click(object sender, RoutedEventArgs e)
        {
            limpiarFila();
            LimpiarDetalle();
            LimpiarCampo();
            TableReservas = null;
            this.Hide();
        }
        #endregion


        #region Validacion de los textbox
        private void TxtDescuento_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key == Key.Space) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de letras y espacios", "Agregar Venta Reservas", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
        private void TxtDescuento_LostFocus(object sender, RoutedEventArgs e)
        {

            if (txtDescuento.Text == string.Empty)
            {
                return;
            }
            else
            {
                if (txtDescuento.Text.Length > 5)
                {
                    System.Windows.Forms.MessageBox.Show("El Descuento  no puede ser mayor a 5 caracteres", "Agregar Venta de Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
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
                            System.Windows.Forms.MessageBox.Show("El Descuento no puede ser menor a cero", "Agregar Venta de Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                            txtDescuento.Clear();
                        }
                    }

                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("El Descuento no es un numero por favor ingrese solo numeros", "Agregar Venta de Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        txtDescuento.Clear();
                        //txtAddCostoUnit.Focus();
                    }
                   

                }
            }
        }

        private void TxtDescuento_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("El Descuento solo se puede ingresar por medio del teclado", "Agregar Venta Reservas", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
        }

        #endregion
        
        #region Métodos para limpiar los textbox
        private void LimpiarDetalle()
        {
            txtFechaReserva.Text = string.Empty;
            txtEstado.Text = string.Empty;
            txtMontoTotal1.Text = string.Empty;
            txtDescuento.Text = string.Empty;
        }

        private void LimpiarCampo()
        {
            txtId_Reserva.Text = string.Empty;
            txtId_Cliente.Text = string.Empty;
            txtClienteNombre.Text = string.Empty;
        }

        public void limpiarFila()
        {
            ContFila = 0;
        }

        #endregion
    }
}
