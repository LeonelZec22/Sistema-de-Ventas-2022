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


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDescuentoVenta.Text = "0.00";
            txtMontoTotal.Text = "0.00";
            Usuarios.DatosUsuario(MainWindow.Usuario);
            tbUsuario.Text = Convert.ToString(InformacionUsuario.IdUsuario);
        }

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
                    System.Windows.Forms.MessageBox.Show("El campo Cantidad no puede estar vacio", "Calcular Descuento", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
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

                            if (Estado == "Finalizada")
                            {
                                System.Windows.Forms.MessageBox.Show("Solo se pueden seleccionar Reservas que están pendientes!!", "Seleccionar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                                break;
                                //VistaReserva.DataGridGestionReserva.UnselectAllCells();
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
                    System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos del detalle de producto!!", "Agregar Detalle", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (txtId_Reserva.Text == string.Empty || txtFechaReserva.Text == string.Empty || txtClienteNombre.Text == string.Empty || txtEstado.Text == string.Empty || txtMontoTotal1.Text == string.Empty || txtDescuento.Text == string.Empty)
                    {
                        System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos del detalle de producto!!", "Agregar Detalle", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                        return;
                    }
                    else
                    {
                        Descuento();
                        //bool Existe = false;
                        //int no_fila = 0;

                        if (ContFila == 0)
                        {
                            if (TableReservas == null)
                            {
                                //Creamos el Data Table porque no existe todavia
                                TableReservas = new DataTable();

                                //Creamos las columnas del Data Table 
                                DataColumn column;
                                //DataRow row;

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
                            }

                            else
                            {
                                TableReservas.Rows.Add(txtId_Reserva.Text, txtClienteNombre.Text, txtFechaReserva.Text, txtEstado.Text, Descuento1.ToString("N2"), Monto_Total.ToString("N2"));

                                DataGridVentaServicios.ItemsSource = TableReservas.DefaultView;

                                DataGridVentaServicios.UnselectAllCells();
                                LimpiarDetalle();
                                ContFila++;
                                txtEstado.Text = "Finalizado";
                            }
                        }

                        else
                        {
                            TableReservas.Rows.Add(txtId_Reserva.Text, txtClienteNombre.Text, txtFechaReserva.Text, txtEstado.Text, Descuento1.ToString("N2"), Monto_Total.ToString("N2"));

                            DataGridVentaServicios.ItemsSource = TableReservas.DefaultView;

                            DataGridVentaServicios.UnselectAllCells();
                            LimpiarDetalle();
                            ContFila++;
                            txtEstado.Text = "Finalizado";
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
                System.Windows.Forms.MessageBox.Show("La Venta no fue agregada por: " + ex, "Agregar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void BtnAgregarReserva_Click(object sender, RoutedEventArgs e)
        {
            AgregarDetalle();
        }

        private void TxtDescuento_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtDescuento.Text == string.Empty)
            {
                return;
            }
            else
            {
                Descuento();
            }
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
                        System.Windows.Forms.MessageBox.Show("Debe de seleccionar el producto a eliminar de la tabla!!", "Eliminar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }

                    else
                    {
                        //Va recorrer la fila seleccionada en busca de obtener su index
                        foreach (DataRowView drv in DataGridVentaServicios.SelectedItems)
                        {

                            DataRow row = drv.Row;

                            indexDelete = TableReservas.Rows.IndexOf(row);

                        }

                        //Restar SubTotal 

                      

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
                        LimpiarCampo();
                        ContFila--;
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
            txtId_Venta_Servicios.Text = Procedimientos.GenerarCodigoId("Ventas_Servicios");
        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            limpiarFila();
            TableReservas = null;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
            limpiarFila();
            TableReservas = null;
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

                    GenerarCorrelativos();

                    if (DataGridVentaServicios.Items.Count == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Por Favor Agregue una Reserva a la Tabla", "Agregar Venta Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
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

        private void BtnCancelarVentaServicios_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            limpiarFila();
            TableReservas = null;
        }

        #endregion

        private void LimpiarDetalle()
        {
            txtId_Reserva.Text = string.Empty;
            txtFechaReserva.Text = string.Empty;
            txtEstado.Text = string.Empty;
            txtMontoTotal1.Text = string.Empty;
            txtDescuento.Text = string.Empty;
        }

        private void LimpiarCampo()
        {
            txtId_Cliente.Text = string.Empty;
            txtClienteNombre.Text = string.Empty;
        }

        public void limpiarFila()
        {
            ContFila = 0;
        }
    }
}
