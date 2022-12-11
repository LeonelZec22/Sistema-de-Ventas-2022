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
            txtSubTotal.Text = "0.00";
            txtDescuentoVenta.Text = "0.00";
            txtMontoTotal.Text = "0.00";
            //dtp_FechaVenta.Text = Convert.ToString(UltimoDia);
           
        }


        #region Método para calcular el descuento

        public double importe = 0, ImporteNeto= 0, Descuento1= 0, Monto_Total=0, Porcentaje=0;

        private void Descuento()
        {
            try
            {
                if (txtCantidad.Text != string.Empty)
                {
                    importe = Convert.ToDouble(txtPrecio_Venta.Text) * Convert.ToDouble(txtCantidad.Text);
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
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error al calcular Descuento por: " + ex.Message, "Calcular Descuento", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
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

        #region Seleccionar un Producto

        //Método para cargar en los textBox el producto
        private void SeleccionarProducto()
        {

            //Abrimos el Formulario de VistaProveedor


            FrmVista_Venta_Producto VistaProductoVenta = new FrmVista_Venta_Producto();

            VistaProductoVenta.ShowDialog();

            txtId_Producto.Text = "";

            txtCod_Producto.Text = "";

            txtNombre_Producto.Text = "";

            txtStockActual.Text = "";

            txtPrecio_Venta.Text = "";

            try
            {
                if (VistaProductoVenta.DialogResult == true)
                {

                    //Lenamos los textbox 

                    foreach (DataRowView drv in VistaProductoVenta.DataGridProductosVenta.SelectedItems)
                    {
                        DataRow row = drv.Row;

                        var IdProducto = Convert.ToString(drv.Row[0]);

                        txtId_Producto.Text = IdProducto;

                        var CodProducto = Convert.ToString(drv.Row[1]);

                        txtCod_Producto.Text = CodProducto;

                        var NombreProducto = Convert.ToString(drv.Row[2]);

                        txtNombre_Producto.Text = NombreProducto;
                        
                        var PrecioVenta = Convert.ToString(drv.Row[3]);

                        txtPrecio_Venta.Text = PrecioVenta;

                        var StockActual = Convert.ToString(drv.Row[4]);

                        txtStockActual.Text = StockActual;

                    }

                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Producto en la lista Producto!!", "Seleccionar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
        private void BtnBuscarProducto_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarProducto();
        }

        #endregion

       

        #region Agregar Datos al DataGrid 
        DataSet dataSet;
        DataTable TableProductos;

        public static int ContFila = 0;
        decimal TotalResta1 = 0, TotalResta2 = 0, TotalResta3 = 0;
        int indexDelete = 0;

        private void AgregarDetalle()
        {
            try
            {
                if (txtId_Producto.Text == string.Empty || txtCod_Producto.Text == string.Empty || txtNombre_Producto.Text == string.Empty || txtCantidad.Text == string.Empty || txtPrecio_Venta.Text == string.Empty || txtStockActual.Text == string.Empty || txtDescuento.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos del detalle de producto!!", "Agregar Detalle", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else if (Convert.ToInt32(txtCantidad.Text) == 0)
                {
                    System.Windows.Forms.MessageBox.Show("No hay producto en existencia", "Agregar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                    txtCantidad.Text = string.Empty;
                }
                else if (Convert.ToInt32(txtStockActual.Text) < Convert.ToInt32(txtCantidad.Text))
                {
                    System.Windows.Forms.MessageBox.Show("No hay suficientes productos en existencia", "Agregar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    txtCantidad.Text = string.Empty;
                    txtDescuento.Text = string.Empty;
                }
                else
                {
                    if (txtId_Producto.Text == string.Empty || txtCod_Producto.Text == string.Empty || txtNombre_Producto.Text == string.Empty || txtCantidad.Text == string.Empty || txtPrecio_Venta.Text == string.Empty || txtStockActual.Text == string.Empty || txtDescuento.Text == string.Empty)
                    {
                        System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos del detalle de producto!!", "Agregar Detalle", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                        return;
                    }
                    else
                    {
                        Descuento();
                        bool Existe = false;
                        int no_fila = 0;

                        if (ContFila == 0)
                        {
                            if (TableProductos == null)
                            {
                                //Creamos el Data Table porque no existe todavia
                                TableProductos = new DataTable();

                                //Creamos las columnas del Data Table 
                                DataColumn column;
                                //DataRow row;

                                column = new DataColumn();
                                column.ColumnName = "Id_Producto";

                                TableProductos.Columns.Add(column);

                                column = new DataColumn();
                                column.ColumnName = "Nombre";
                                TableProductos.Columns.Add(column);

                                column = new DataColumn();
                                column.ColumnName = "Cantidad";
                                TableProductos.Columns.Add(column);

                                column = new DataColumn();
                                column.ColumnName = "Precio_Venta";
                                TableProductos.Columns.Add(column);

                                column = new DataColumn();
                                column.ColumnName = "Sub_Total";
                                TableProductos.Columns.Add(column);
                                column = new DataColumn();

                                column.ColumnName = "Descuento";
                                TableProductos.Columns.Add(column);
                                column = new DataColumn();

                                column.ColumnName = "Total";
                                TableProductos.Columns.Add(column);
                                column = new DataColumn();

                                dataSet = new DataSet();

                                dataSet.Tables.Add(TableProductos);

                                //Creamos una fila del Data Table utilizando la información ya capturada
                                TableProductos.Rows.Add(txtId_Producto.Text, txtNombre_Producto.Text, txtCantidad.Text, txtPrecio_Venta.Text, importe.ToString("N2"), Descuento1.ToString("N2"), Monto_Total.ToString("N2"));

                                DataGridVenta.ItemsSource = TableProductos.DefaultView;
                                LimpiarDetalle();
                                ContFila++;
                            }

                            else
                            {
                                TableProductos.Rows.Add(txtId_Producto.Text, txtNombre_Producto.Text, txtCantidad.Text, txtPrecio_Venta.Text, importe.ToString("N2"), Descuento1.ToString("N2"), Monto_Total.ToString("N2"));

                                DataGridVenta.ItemsSource = TableProductos.DefaultView;

                                DataGridVenta.UnselectAllCells();
                                LimpiarDetalle();
                                ContFila++;
                            }
                        }

                        else
                        {
                            foreach (DataRowView drv in DataGridVenta.ItemsSource)
                            {
                                DataRow row = drv.Row;

                                if (row[0].ToString() == txtId_Producto.Text)
                                {
                                    Existe = true;

                                    no_fila = TableProductos.Rows.IndexOf(row);
                                }

                            }

                            if (Existe == true)
                            {
                                int ValidarStock = Convert.ToInt32(TableProductos.Rows[no_fila][2]) + Convert.ToInt32(txtCantidad.Text);

                                if (ValidarStock > Convert.ToInt32(txtStockActual.Text))
                                {
                                    System.Windows.Forms.MessageBox.Show("No hay suficientes productos en existencia", "Agregar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                                    LimpiarDetalle();
                                    DataGridVenta.UnselectAllCells();
                                }

                                else
                                {
                                    TableProductos.Rows[no_fila]["Cantidad"] = Convert.ToDouble(txtCantidad.Text) + Convert.ToDouble(TableProductos.Rows[no_fila][2].ToString());

                                    TableProductos.Rows[no_fila]["Sub_Total"] = ImporteNeto + Convert.ToDouble(TableProductos.Rows[no_fila][4].ToString());

                                    TableProductos.Rows[no_fila]["Descuento"] = Descuento1 + Convert.ToDouble(TableProductos.Rows[no_fila][5].ToString());

                                    TableProductos.Rows[no_fila]["Total"] = Monto_Total + Convert.ToDouble(TableProductos.Rows[no_fila][6].ToString());

                                    DataGridVenta.ItemsSource = TableProductos.DefaultView;
                                    LimpiarDetalle();
                                    DataGridVenta.UnselectAllCells();
                                }
                                
                            }

                            else
                            {
                                TableProductos.Rows.Add(txtId_Producto.Text, txtNombre_Producto.Text, txtCantidad.Text, txtPrecio_Venta.Text, importe.ToString("N2"), Descuento1.ToString("N2"), Monto_Total.ToString("N2"));

                                DataGridVenta.ItemsSource = TableProductos.DefaultView;

                                DataGridVenta.UnselectAllCells();
                                LimpiarDetalle();
                                ContFila++;
                            }
                        }

                        decimal Total1 = 0, Total2 = 0, Total3 = 0;
                        foreach (DataRowView row3 in DataGridVenta.ItemsSource)
                        {
                            Total1 += Convert.ToDecimal(row3[4]);
                            Total2 += Convert.ToDecimal(row3[5]);
                            Total3 += Convert.ToDecimal(row3[6]);
                        }

                        txtSubTotal.Text = Total1.ToString("N2");
                        txtDescuentoVenta.Text = Total2.ToString("N2");
                        txtMontoTotal.Text = Total3.ToString("N2");

                        TotalResta1 = Total1;
                        TotalResta2 = Total2;
                        TotalResta3 = Total3;
                    }
                }
            }

            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("La Venta no fue agregada por: " + ex, "Agregar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void BtnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            AgregarDetalle();
        }

        private void TxtDescuento_LostFocus(object sender, RoutedEventArgs e)
        {
            if(txtDescuento.Text == string.Empty)
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
                if (ContFila > 0 && DataGridVenta.Items.Count > 0)
                {
                    if (DataGridVenta.SelectedItems.Count == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Debe de seleccionar el producto a eliminar de la tabla!!", "Eliminar Productor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }

                    else
                    {
                        //Va recorrer la fila seleccionada en busca de obtener su index
                        foreach (DataRowView drv in DataGridVenta.SelectedItems)
                        {
                            
                            DataRow row = drv.Row;

                            indexDelete = TableProductos.Rows.IndexOf(row);

                        }

                        //Restar SubTotal 

                        TotalResta1 = TotalResta1 - Convert.ToDecimal(TableProductos.Rows[indexDelete][4]);
                        txtSubTotal.Text = TotalResta1.ToString("N2");

                        //Restar Descuento 
                        TotalResta2 = TotalResta2 - Convert.ToDecimal(TableProductos.Rows[indexDelete][5]);
                        txtDescuentoVenta.Text = TotalResta2.ToString("N2");

                        // Restar Monto Total
                        TotalResta3 = TotalResta3 - Convert.ToDecimal(TableProductos.Rows[indexDelete][6]);
                        txtMontoTotal.Text = TotalResta3.ToString("N2");

                        TableProductos.Rows.RemoveAt(indexDelete);
                        DataGridVenta.ItemsSource = TableProductos.DefaultView;
                        DataGridVenta.UnselectAllCells();
                        ContFila--;
                    }
                }

                else
                {
                    System.Windows.Forms.MessageBox.Show("No hay productos para eliminar!!", "Eliminar Productor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
            }

            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se ha podido eliminar el producto porque: " + ex, "Eliminar Productor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void BtnEliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            EliminarDetalle();
        }

        #endregion


        #region Método para guardar la Venta en la Base de datos
        private void BtnGuardarVenta_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            limpiarFila();
            TableProductos = null;
        }

        private void GenerarCorrelativos()
        {
            txtId_Venta.Text = Procedimientos.GenerarCodigoId("Ventas");
        }

        public virtual bool Guardar()
        {
            try
            {
                if (txtClienteNombre.Text == string.Empty || txtId_Cliente.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos por favor!!", "Agregar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {

                    Venta.Id_Cliente = Convert.ToInt32(txtId_Cliente.Text);
                    Venta.Fecha_Venta = Convert.ToDateTime(dtp_FechaVenta.Text);
                    Venta.Sub_Total = Convert.ToDecimal(txtSubTotal.Text);
                    Venta.Descuento = Convert.ToDecimal(txtDescuentoVenta.Text);
                    Venta.Monto_Total = Convert.ToDecimal(txtMontoTotal.Text);
                    Venta.Estado = "Emitido";
                    Venta.Id_Usuario = 1;

                    GenerarCorrelativos();

                    foreach (DataRowView drv in DataGridVenta.ItemsSource)
                    {
                        DataRow row = drv.Row;

                        DetalleVenta.Id_Venta = Convert.ToInt32(txtId_Venta.Text);
                        DetalleVenta.Id_Producto = Convert.ToInt32(row[0].ToString());
                        DetalleVenta.Cantidad = Convert.ToInt32(row[2].ToString());
                        DetalleVenta.Precio_Venta = Convert.ToDecimal(row[3].ToString());
                        DetalleVenta.Sub_Total = Convert.ToDecimal(row[4].ToString());
                        DetalleVenta.Descuento = Convert.ToDecimal(row[5].ToString());
                        DetalleVenta.Monto_Total = Convert.ToDecimal(row[6].ToString());

                        DetalleVentas.AgregarDetalleVenta(DetalleVenta);
                    }

                    Ventas.AgregarVenta(Venta);

                    System.Windows.Forms.MessageBox.Show("Venta de Productos agregada correctamente!!", "Agregar Venta Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    txtSubTotal.Text= "0.00";
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

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Ingreso de Producto no fue agregado por: " + ex, "Agregar Ingreso Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
        }

        private void BtnCancelarVenta_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            limpiarFila();
            TableProductos = null;
        }

        #endregion

        private void LimpiarDetalle()
        {
            txtId_Producto.Text = string.Empty;
            txtCod_Producto.Text = string.Empty;
            txtNombre_Producto.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtStockActual.Text = string.Empty;
            txtPrecio_Venta.Text = string.Empty;
            txtDescuento.Text = string.Empty;
            btnBuscarProducto.Focus();
        }

        private void LimpiarCampo()
        {
            txtId_Cliente.Text = string.Empty;
            txtNombre_Producto.Text = string.Empty;
        }

        public void limpiarFila()
        {
            ContFila = 0;
        }
    }
}
