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
    /// Lógica de interacción para FrmAgregarIngreso.xaml
    /// </summary>
    public partial class FrmAgregarIngreso : Window
    {
        public FrmAgregarIngreso()
        {
            InitializeComponent();

        }

        public FrmAgregarIngreso(FrmCompraDeProducto Compras)
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtTotal_Pago.Text = "0.00";
            Correlativo();
            dtp_FechaIngreso.SelectedDate = DateTime.Today;
        }

        #region Instancia de objetos a usar

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Ingreso_Productos Ingresos = new CDo_Ingreso_Productos();
        CE_Ingreso_Productos Ingreso = new CE_Ingreso_Productos();
        CDo_Detalle_Ingreso DetalleIngresos = new CDo_Detalle_Ingreso();
        CE_Detalle_Ingreso DetalleIngreso = new CE_Detalle_Ingreso();

        //
        CDo_Proveedores Proveedores = new CDo_Proveedores();
        CE_Proveedores Proveedor = new CE_Proveedores();

        //
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


        //Método para generar el numero de ingreso 

        private void Correlativo()
        {
            txtId_IngresoProducto.Text = Procedimientos.GenerarCodigoId("Ingreso_Productos");

            txtNo_Ingreso.Text = "INGR" + Procedimientos.GenerarCodigo("Ingreso_Productos");

            txtId_Detalle.Text = Procedimientos.GenerarCodigoId("Detalles_Ingreso");
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        

        DataSet dataSet;
        DataTable TableProductos;


        //Método para limpiar los textBox de productos

        private void LimpiarDetalle()
        {
            txtId_Producto.Text = string.Empty;
            txtCod_Producto.Text = string.Empty;
            txtNombre_Producto.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtCosto_Unitario.Text = string.Empty;
        }
       
        #region Agregar datos al datagrid 

        public static int ContFila = 0;

        decimal TotalResta = 0;

        int indexDelete = 0;
        
        // Método para agregar los productos al datagrid
        private void AgregarDetalle()
        {
            decimal SubTotal = 0;

            try
            {
                //Valida que los campos de texto de productos no esten vacios 

                if (txtId_Producto.Text == string.Empty || txtNombre_Producto.Text == string.Empty || txtCantidad.Text == string.Empty || txtCosto_Unitario.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos del detalle de producto!!", "Agregar Detalle", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                    return;
                }

                else
                {
                    //Variable que verifica si un producto ya ha fue agregado a la DataTable para solo modificar la columna de Cantidad y Subtotal de ese producto
                    bool Existe = false;

                    //Variable para guardar el index del producto en caso que este fuera agregado al DataTable
                    int no_fila = 0;

                    //Verificamos si el DataGrid contiene datos por defecto no estara vacio porque después de agregar un producto el valor de está aumenta
                    if (ContFila == 0)
                    {
                        //Verficamos si la DataTable ya esta creada con una fila agregada
                        if (TableProductos == null)
                        {
                            TableProductos = new DataTable();

                            DataColumn column;
                            DataRow row;

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
                            column.ColumnName = "Costo_Unitario";
                            TableProductos.Columns.Add(column);

                            column = new DataColumn();
                            column.ColumnName = "Sub_Total";
                            TableProductos.Columns.Add(column);

                            dataSet = new DataSet();

                            dataSet.Tables.Add(TableProductos);

                            SubTotal = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtCosto_Unitario.Text);
                            TableProductos.Rows.Add(txtId_Producto.Text, txtNombre_Producto.Text, txtCantidad.Text, txtCosto_Unitario.Text, SubTotal.ToString("N2"));

                            DataGridIngresoProducto.ItemsSource = TableProductos.DefaultView;
                            LimpiarDetalle();
                            ContFila++;
                        }

                        //En caso que ya hay datos en la DataTable solo agregamos una fila con los datos de los TextBox
                        else
                        {
                            SubTotal = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtCosto_Unitario.Text);
                            TableProductos.Rows.Add(txtId_Producto.Text, txtNombre_Producto.Text, txtCantidad.Text, txtCosto_Unitario.Text, SubTotal.ToString("N2"));

                            DataGridIngresoProducto.ItemsSource = TableProductos.DefaultView;
                            DataGridIngresoProducto.UnselectAllCells();
                            LimpiarDetalle();
                        }

                    }

                    else
                    {
                        //Recorro el DataGrid en busca de encontrar un producto que ya ha sido agregado si tiene mismo id que el está en el textbox
                        foreach (DataRowView drv in DataGridIngresoProducto.ItemsSource)
                        {
                            DataRow row = drv.Row; //Tomo la primera fila y accedo a la columna 0 ver su valor y ver si es igual que el está en el textbox id si no es igual entonces tomo las siguientes filas
                            if (row[0].ToString() ==txtId_Producto.Text)
                            {
                                Existe = true;

                                no_fila = TableProductos.Rows.IndexOf(row);
                            }
                        }

                        //Si el producto nuevo ya fue agregado a la Tabla entonces solo modifico la columna Cantidad y SubTotal de ese producto ya existente
                        if(Existe == true)
                        {
                            SubTotal = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtCosto_Unitario.Text);

                            TableProductos.Rows[no_fila]["Cantidad"] = Convert.ToDouble(txtCantidad.Text) + Convert.ToDouble(TableProductos.Rows[no_fila][2].ToString());
                            TableProductos.Rows[no_fila]["Sub_Total"] = (SubTotal + Convert.ToDecimal(TableProductos.Rows[no_fila][4].ToString()));

                            DataGridIngresoProducto.ItemsSource = TableProductos.DefaultView;
                            LimpiarDetalle();
                            DataGridIngresoProducto.UnselectAllCells();

                        }

                        //Si no existe entonces solo agrego una  nueva fila con los datos que hay en los textbox
                        else
                        {
                            SubTotal = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtCosto_Unitario.Text);
                            TableProductos.Rows.Add(txtId_Producto.Text, txtNombre_Producto.Text, txtCantidad.Text, txtCosto_Unitario.Text, SubTotal.ToString("N2"));

                            DataGridIngresoProducto.ItemsSource = TableProductos.DefaultView;
                            LimpiarDetalle();
                            DataGridIngresoProducto.UnselectAllCells();
                            ContFila++;
                        }
                    }

                    decimal TotalSuma = 0;
                    
                    //Recorro el DataGrid para seleccionar la columna SubtoTotal de todas la filas y sumar su valor
                    foreach (DataRowView row3 in DataGridIngresoProducto.ItemsSource)
                    {
                        //Cuando entre a la primera fila agarre el valor de la columna 5 y que a las filas siguientes el valor de esas filas se sume al valor actual
                        TotalSuma += Convert.ToDecimal(row3[4]);
                    }
                    TotalResta = TotalSuma;

                    txtTotal_Pago.Text = TotalSuma.ToString("N2");
                   
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Producto no fue agregado por: " + ex, "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void BtnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            AgregarDetalle(); 
        }

        #endregion

        #region Método para cargar en los textBox el proveedor
        private void SeleccionarProveedor()
        {
            //Abrimos el Formulario de VistaProveedor

            FrmVistaProveedores VistaProveedor = new FrmVistaProveedores();

            VistaProveedor.ShowDialog();

            txtId_Proveedor.Text = "";

            txtNombre_Proveedor.Text = "";
            
            var Datos = VistaProveedor.DataGridGestionProveedores;

            try
            {
                if(VistaProveedor.DialogResult == true)
                {


                    foreach (DataRowView drv in VistaProveedor.DataGridGestionProveedores.SelectedItems)
                    {
                        DataRow row = drv.Row;

                        var IdProveedor = Convert.ToString(drv.Row[0]);

                        txtId_Proveedor.Text = IdProveedor;

                        var NombreProveedor = Convert.ToString(drv.Row[2]);

                        txtNombre_Proveedor.Text = NombreProveedor;
                    }

                    
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Proveedor en la lista proveedores!!", "Seleccionar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void BtnBuscarProveedor_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarProveedor();
        }
        #endregion

        #region Método para cargar en los textBox el producto
        private void SeleccionarProducto()
        {

            //Abrimos el Formulario de VistaProveedor


            FrmVistaProducto VistaProducto = new FrmVistaProducto();

            VistaProducto.ShowDialog();

            txtId_Producto.Text = "";

            txtCod_Producto.Text = "";

            txtNombre_Producto.Text = "";

            txtCantidad.Text = "";

            txtCosto_Unitario.Text = "";

            try
            {
                if (VistaProducto.DialogResult == true)
                {

                    //Lenamos los textbox 

                    foreach (DataRowView drv in VistaProducto.DataGridGestionProductos.SelectedItems)
                    {
                        DataRow row = drv.Row;

                        var IdProducto = Convert.ToString(drv.Row[0]);

                        txtId_Producto.Text = IdProducto;

                        var CodProducto = Convert.ToString(drv.Row[1]);

                        txtCod_Producto.Text = CodProducto;

                        var NombreProducto = Convert.ToString(drv.Row[2]);

                        txtNombre_Producto.Text = NombreProducto;

                        var CostoUnitario = Convert.ToString(drv.Row[5]);

                        txtCosto_Unitario.Text = CostoUnitario;
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

        #region Eliminar datos del DataGrid
        private void BtnEliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Verificamos si hay productos agregados a la tabla es decir si hay filas agregadas
                if (ContFila > 0 && DataGridIngresoProducto.Items.Count > 0)
                {
                    //Verificamos si hay un producto o una fila seleccionada de la tabla
                    if(DataGridIngresoProducto.SelectedItems.Count == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Debe de seleccionar el producto a eliminar de la tabla!!", "Eliminar Productor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        //Va recorrer la fila seleccionada en busca de obtener su index
                        foreach (DataRowView drv in DataGridIngresoProducto.SelectedItems)
                        {
                            //if(DataGridIngresoProducto.SelectedItems.Count == 1)
                            //{
                            DataRow row = drv.Row;

                            indexDelete = TableProductos.Rows.IndexOf(row);
                            //}

                        }

                        
                        TotalResta = TotalResta - Convert.ToDecimal(TableProductos.Rows[indexDelete][4]);
                        txtTotal_Pago.Text = TotalResta.ToString("N2");

                        TableProductos.Rows.RemoveAt(indexDelete);
                        DataGridIngresoProducto.ItemsSource = TableProductos.DefaultView;
                        DataGridIngresoProducto.UnselectAllCells();
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
                System.Windows.Forms.MessageBox.Show("No se ha podido eliminar el producto porque: "+ ex, "Eliminar Productor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        FrmCompraDeProducto compraDeProducto = new FrmCompraDeProducto();

        #endregion

        #region Método para guardar la información en la Base de Datos

        public virtual bool Guardar()
        {
            try
            {
                if (txtId_IngresoProducto.Text == string.Empty || txtNo_Ingreso.Text == string.Empty || txtId_Proveedor.Text == string.Empty || txtNombre_Proveedor.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos por favor!!", "Agregar Ingreso Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    //Ingreso.No_Ingreso
                    Ingreso.No_Ingreso = txtNo_Ingreso.Text;
                    Ingreso.Id_Proveedor = Convert.ToInt32(txtId_Proveedor.Text);
                    Ingreso.Fecha_Ingreso = Convert.ToDateTime(dtp_FechaIngreso.Text);
                    Ingreso.Monto_total = Convert.ToDecimal(txtTotal_Pago.Text);
                    Ingreso.Estado = "Recibido";

                    if (DataGridIngresoProducto.Items.Count == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Por favor agregue un producto a la tabla", "Agregar Ingreso Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }

                    else
                    {
                            foreach (DataRowView drv in DataGridIngresoProducto.ItemsSource)
                            {
                                DataRow row = drv.Row;

                                DetalleIngreso.Id_IngresoProducto = Convert.ToInt32(txtId_IngresoProducto.Text);
                                DetalleIngreso.Id_Producto = Convert.ToInt32(row[0].ToString());
                                DetalleIngreso.Nombre = Convert.ToString(row[1].ToString());
                                DetalleIngreso.Cantidad = Convert.ToInt32(row[2].ToString());
                                DetalleIngreso.Costo_Unitario = Convert.ToDecimal(row[3].ToString());
                                DetalleIngreso.Sub_Total = Convert.ToDecimal(row[4].ToString());

                                DetalleIngresos.AgregarDetalleIngreso(DetalleIngreso);
                            }

                            Ingresos.AgregarIngreso(Ingreso);

                            System.Windows.Forms.MessageBox.Show("Ingreso de Producto agregado correctamente!!", "Agregar Ingreso Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            txtId_Detalle.Text = string.Empty;
                            txtTotal_Pago.Text = "0.00";
                            Agregar();
                            LimpiarDetalle();
                            limpiarproveedor();
                            limpiarFila();
                            Correlativo();
                            Hide();
                            //compraDeProducto.ShowDialog();
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
        private void BtnGuardarIngreso_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        #endregion

        #region Métodos para limpiar los textbox
        public void limpiarproveedor()
        {
            txtId_IngresoProducto.Clear();
            txtNo_Ingreso.Clear();
            txtId_Proveedor.Clear();
            txtNombre_Proveedor.Clear();
            
        }

        public void limpiarFila()
        {
             ContFila = 0;
        }

        #endregion


        #region Botones para salir de la pantalla
        private void BtnCancelarIngreso_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            limpiarFila();
            LimpiarDetalle();
            TableProductos = null;
        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            limpiarFila();
            LimpiarDetalle();
            TableProductos = null;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
            limpiarFila();
            LimpiarDetalle();
            TableProductos = null;
        }

        #endregion

        #region Validacion de los textbox
        private void TxtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key == Key.Space) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de letras y espacios", "Agregar Ingreso  de Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void TxtCantidad_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtCantidad.Text.Length > 4)
            {
                System.Windows.Forms.MessageBox.Show("El Descuento  no puede ser mayor a 4 caracteres", "Agregar Ingreso de Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (Convert.ToInt32(txtCantidad.Text) > 0)
                    {
                        Procedimientos.FormatoEntero(txtCantidad);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("La Cantidad no puede ser mayor a cero", "Agregar Ingreso de Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        txtCantidad.Clear();
                    }
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("La cantidad no es un numero por favor ingrese solo numeros", "Agregar Ingreso de Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    txtCantidad.Clear();
                }
            }
        }

        private void TxtCantidad_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

            System.Windows.Forms.MessageBox.Show("La Cantidad solo se puede ingresar por medio del teclado", "Agregar Ingreso de Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
        }

        #endregion

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 




    }
}
