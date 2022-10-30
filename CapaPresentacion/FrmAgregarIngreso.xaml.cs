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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtTotal_Pago.Text = "0.00";
            Correlativo();
        }

        #endregion


        #region Método para generar el numero de ingreso 

        private void Correlativo()
        {
            txtId_IngresoProducto.Text = Procedimientos.GenerarCodigoId("Ingreso_Productos");

            txtNo_Ingreso.Text = "INGR" + Procedimientos.GenerarCodigo("Ingreso_Productos");

            txtId_Detalle.Text = Procedimientos.GenerarCodigoId("Detalles_Ingreso");
        }

        #endregion

        DataGrid dg;
        DataRowView dr;

        DataSet dataSet;
        DataTable TableProductos;

        decimal SubTotal = 0;
        private void LlenarDataTable()
        {

            if(TableProductos == null)
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
            }

            else
            {
                SubTotal = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtCosto_Unitario.Text);
                TableProductos.Rows.Add(txtId_Producto.Text, txtNombre_Producto.Text, txtCantidad.Text, txtCosto_Unitario.Text, SubTotal.ToString("N2"));

                DataGridIngresoProducto.ItemsSource = TableProductos.DefaultView;
            }

            decimal sum5 = 0;

            foreach (DataRowView row3 in DataGridIngresoProducto.ItemsSource)
            {
                sum5 += Convert.ToDecimal(row3[4]);
            }

            txtTotal_Pago.Text = sum5.ToString("N2");

            
            //row = TableProductos.NewRow();

            //row[0] = "1";

            //row[1] = "Leonel";

            //row[2] = "2";

            //row[3] = "3";

            //row[4] = "20";

            //TableProductos.Rows.Add(row);

            //TableProductos.Rows.Add(new Object[]
            //{
            //    "1", "Leonel", "2", "3","20"
            //});



        }

        private void BtnBuscarProveedor_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarProveedor();

            #region comentarioos

            //this.Hide();

            //var Index = GetRow(DataGridIngresoProducto, 0);

            //labelrow.Text = Convert.ToString(Index);
            //try
            //{
            //    if(VistaProveedor.DialogResult == true)
            //    {

            // IndexOf
            //       txtId_Proveedor.Text = VistaProveedor.DataGridGestionProveedores.Items.IndexOf(VistaProveedor.DataGridGestionProveedores.CurrentItem).ToString();

            //       txtId_Proveedor.Text = VistaProveedor.DataGridGestionProveedores.Items.IndexOf(VistaProveedor.DataGridGestionProveedores.CurrentItem)

            //        //txtId_Proveedor.Text = VistaProveedor.DataGridGestionProveedores.Items[VistaProveedor.DataGridGestionProveedores.CurrentItem.]

            //        //txtId_Proveedor.Text = VistaProveedor

            //        Proveedor.Id_Proveedor = Convert.ToInt32(txtId_Proveedor.Text.Trim());
            //        Proveedor.Nombre = txtNombre_Proveedor.Text.Trim();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Proveedor en la lista proveedores!!", "Seleccionar Proveedor", System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Exclamation);
            //}

            #endregion
        }


        private void BtnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            #region comentarios4

            //Ingreso2 = new List<DataGridItemsProducto>M
            //DataTable dt = new DataTable();

            //DataRow dr = null;

            //dr = dt.NewRow();

            //dt.Rows.Add(txtId_Proveedor.Text, txtNombre_Proveedor);
            //DataGridIngresoProducto.ItemsSource = dt.DefaultView;


            // DataGridIngresoProducto.Items.Add(new { Id_Producto = "1", Nombre = "Leonel", Cantidad = "5", Costo_Unitario = "2.50", Sub_Total = "50" });

            // Id_Producto



            // Imprime el número de filas en la colección.

            //labelrow.Text = Convert.ToString(DataGridIngresoProducto.Items.Count);

            //// Imprime el valor de la columna 2  en cada fila
            ///
            // FrmVistaProveedores VistaProveedor = new FrmVistaProveedores();

            //DataTable Dta = new DataTable();

            //DataRowCollection rowCollection = Dta.Rows;

            //DataRow newRow = Dta.NewRow();

            //DataTable dataTable = new DataTable();

            //dataTable.Rows.Add(txtId_Proveedor.Text, txtNombre_Proveedor.Text);

            //string idProducto = Convert.ToString(txtId_Producto);

            //string NombreProve = Convert.ToString(txtNombre_Proveedor);

            //string[] Array = new string[] { idProducto, NombreProve, "Hola", "Bye"};

            //dataTable.Rows.Add(Array);

            //DataGridIngresoProducto.ItemsSource = dataTable.DefaultView;

            //foreach (DataRowView row in VistaProveedor.DataGridGestionProveedores.Items)
            //{
            //    labelrow.Text = Convert.ToString(row[2]);

            //    labelrow2.Text = Convert.ToString(row[2]);
            //}


            //RecorrerData();

            //DataGridIngresoProducto.Items.Add(new { Id_Producto = "1", Nombre = "Leonel", Cantidad = "5", Costo_Unitario = "2.50", Sub_Total = "50" });
            //DataGridIngresoProducto.UnselectAll();
            #endregion

            //AgregarDetalle();

            //fillaDT();
            //Tabla222();
            //var list = new List<DataGridItemsProducto>(DataGridIngresoProducto.ItemsSource as IEnumerable<DataGridItemsProducto>);

            //myDataTable = ToDataTable(list);
            //if (myDataTable != null)
            //{
            //    return;
            //}

            //DataGridIngresoProducto.ItemsSource = myDataTable.DefaultView;

            LlenarDataTable();
            
        }

        DataTable myDataTable;
        private void Tabla222 ()
        {
            //List<DataGridItemsProducto> productos = new List<DataGridItemsProducto>();

            //productos.Add(new DataGridItemsProducto() { Id_Producto = "1", Nombre = "Leonel", Cantidad = "3", Costo_Unitario = "4", Sub_Total = "10" });
            //productos.Add(new DataGridItemsProducto() { Id_Producto = "2", Nombre = "Ronaldo", Cantidad = "3", Costo_Unitario = "4", Sub_Total = "10" });
            //productos.Add(new DataGridItemsProducto() { Id_Producto = "4", Nombre = "James", Cantidad = "3", Costo_Unitario = "4", Sub_Total = "10" });

            //DataGridIngresoProducto.ItemsSource = productos;

            //DataGrid DataGr= new DataGrid();

            //DataGr.Items.Add(new { Id_Producto = "1", Nombre = "Leonel", Cantidad = "3", Costo_Unitario = "4", Sub_Total = "10" });

            //DataGridIngresoProducto.ItemsSource = DataGr;

            decimal SubTotal = 0;

            SubTotal = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtCosto_Unitario.Text);

            DataGridIngresoProducto.Items.Add(new { Id_Producto = txtId_Producto.Text, Nombre = txtNombre_Producto.Text, Cantidad = txtCantidad.Text, Costo_Unitario = txtCosto_Unitario.Text, Sub_Total = SubTotal.ToString("N2") });

            string Typo =Convert.ToString(DataGridIngresoProducto.GetType());

            System.Windows.Forms.MessageBox.Show(Typo, "Agregar Detalle", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
        }



        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }




        private void fillaDT()
        {
            decimal SubTotal = 0;
            SubTotal = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtCosto_Unitario.Text);
            DataGridItemsProducto Person1 = new DataGridItemsProducto();
            Person1.Id_Producto = txtId_Producto.Text;
            Person1.Nombre = txtNombre_Producto.Text;
            Person1.Cantidad = txtCantidad.Text;
            Person1.Costo_Unitario = txtCosto_Unitario.Text;
            Person1.Sub_Total = SubTotal.ToString("N2");

            DataGridIngresoProducto.Items.Add(Person1);

            //decimal Totaal = 0;
            //foreach (DataRowView row in DataGridIngresoProducto.ItemsSource)
            //{
            //    Totaal += Convert.ToDecimal(row[5]);
            //}

            //txtTotal_Pago.Text = Total.ToString("N2");
        }

        #region comentario
        public void RowPrueba()
        {
            //List<DataGridItemsProducto> productos = new List<DataGridItemsProducto>();

            //productos.Add(new DataGridItemsProducto() { Id_Producto = 1, Nombre = "Leonel", Cantidad = 5, Costo_Unitario = 2.50, Sub_Total = 50 });

            //DataGridIngresoProducto.ItemsSource = productos;
        }

        #endregion

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


                    #region Pruebas fallidas
                    //SeleccionItem();

                    //foreach (DataRowView row in VistaProveedor.DataGridGestionProveedores.ItemsSource)
                    //{
                    //    txtId_Proveedor.Text = row[0].ToString();


                    //}

                    //foreach (DataGridCell row in VistaProveedor.DataGridGestionProveedores.ItemsSource)
                    //{
                    //    txtId_Proveedor.Text = row[0];


                    //}



                    //foreach (DataRowView row2 in VistaProveedor.DataGridGestionProveedores.ItemsSource)
                    //{
                    //    if (SecondValue == 0)
                    //    {
                    //        txtNombre_Proveedor.Text = row2[2].ToString();
                    //    }

                    //    SecondValue++;
                    //    //VistaProveedor.DataGridGestionProveedores.UnselectAllCells();

                    //}


                    //if (i.Items[counter] != null)
                    //{

                    //}

                    //foreach (DataRowView row in VistaProveedor.DataGridGestionProveedores.ItemsSource)
                    //{
                    //    if (FirstValue == 0)
                    //    {
                    //        txtId_Proveedor.Text = VistaProveedor.DataGridGestionProveedores.SelectedCells[0].ToString();
                    //        FirstValue++;
                    //    }
                    //}

                    //txtId_Proveedor.Text = VistaProveedor.DataGridGestionProveedores.Items();
                    //txtId_Proveedor.Text = VistaProveedor.DataGridGestionProveedores.Items[0].ToString(); 
                    //txtId_Proveedor.Text = VistaProveedor.DataGridGestionProveedores.CurrentCell.ToString();
                    //txtNombre_Proveedor.Text = VistaProveedor.DataGridGestionProveedores.SelectedCells[2].ToString();
                    //txtNombre_Proveedor.Text = VistaProveedor.DataGridGestionProveedores.CurrentCell.ToString();

                    #endregion

                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Proveedor en la lista proveedores!!", "Seleccionar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
        private void LimpiarDetalle()
        {
            txtId_Producto.Text = string.Empty;
            txtCod_Producto.Text = string.Empty;
            txtNombre_Producto.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtCosto_Unitario.Text = string.Empty;
        }
        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        ///

        #region Agregar datos al datagrid 

        public static int ContFila = 0; 
        public static decimal Total;  

        // Método para agregar los productos al datagrid

        //Metódo para agregar detalle del producto
        private void AgregarDetalle()
        {
            decimal SubTotal = 0;

            try
            {

                if (txtId_Producto.Text == string.Empty || txtNombre_Producto.Text == string.Empty || txtCantidad.Text == string.Empty || txtCosto_Unitario.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos del detalle de producto!!", "Agregar Detalle", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                    return;
                }

                else
                {
                    bool Existe = false;

                    int No_Fila = 0; 

                    //Si no hay productos agregados en la tabla

                    if (ContFila == 0)
                    {
                        #region comentario
                        //dataGridProducto = DataGridIngresoProducto;
                        //List<DataGridItemsProducto> productos = new List<DataGridItemsProducto>();
                        //productos.Add(new DataGridItemsProducto() { Id_Producto =txtId_Producto.Text, Nombre = txtNombre_Producto.Text, Cantidad = txtCantidad.Text, Costo_Unitario = txtCosto_Unitario.Text, Sub_Total = SubTotal.ToString("N2")});

                        //DataGridIngresoProducto.ItemsSource = productos;
                        #endregion

                        //Sumamos los datos que hay en los textbox
                        SubTotal = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtCosto_Unitario.Text);

                        //Agregar a la tabla los campos de textos
                        DataGridIngresoProducto.Items.Add(new { Id_Producto = txtId_Producto.Text, Nombre = txtNombre_Producto.Text, Cantidad = txtCantidad.Text, Costo_Unitario = txtCosto_Unitario.Text, Sub_Total = SubTotal.ToString("N2") });

                        DataGridIngresoProducto.UnselectAll();

                        btnAgregarProducto.Focus();
                        ContFila++;

                        LimpiarDetalle();

                        #region comentariooos
                        //DataGridIngresoProducto.Items.Add(txtId_Producto.Text);
                        //DataGridIngresoProducto.Items.Add(txtNombre_Producto.Text);
                        //DataGridIngresoProducto.Items.Add(txtCantidad.Text);
                        //DataGridIngresoProducto.Items.Add(txtCosto_Unitario.Text);
                        //DataGridIngresoProducto.Items.Add(SubTotal.ToString("N2"));

                        #endregion

                        //Hasta aquí todo bien
                    }

                    else
                    {

                        //Trabado en esto
                        #region comentario 5
                        //foreach (DataRowView drv in VistaProveedor.DataGridGestionProveedores.SelectedItems)
                        //{
                        //    DataRow row = drv.Row;

                        //    var IdProveedor = Convert.ToString(drv.Row[0]);

                        //    txtId_Proveedor.Text = IdProveedor;

                        //    var NombreProveedor = Convert.ToString(drv.Row[2]);

                        //    txtNombre_Proveedor.Text = NombreProveedor;
                        //}

                        //DataRow Darow = fila.Row;

                        //var FilasSel = Convert.ToString(fila.Row[0]);

                        //if(FilasSel == txtId_Producto.Text)
                        //{
                        //    Existe = true;
                        //    //var IndexRow = DataGridIngresoProducto.Items.IndexOf(DataGridIngresoProducto.CurrentItem);
                        //    //No_Fila = IndexRow;

                        //    No_Fila = DataGridIngresoProducto.SelectedIndex;
                        //}

                        #endregion
                        //Recorrer un DataGrid 

                        #region si un producto ya ha sido agregado actualizar su cantidad y subtotalS
                        //foreach (DataRowView fila in DataGridIngresoProducto.ItemsSource)
                        //{
                        //    if (fila[0].ToString() == txtId_Producto.Text)
                        //    {
                        //        //DataRow data = fila.Row;

                        //        Existe = true;

                        //        //DataGridRow row = (DataGridRow)DataGridIngresoProducto.ItemContainerGenerator.ContainerFromIndex;

                        //        //Obtener la posicion de la fila en el DataGrid
                        //        //No_Fila = fila.g;

                        //    }

                        //}

                        #endregion

                        #region si existe 
                        //if (Existe == true)
                        //{
                        //    SubTotal = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtCosto_Unitario.Text);
                        //    foreach (DataRowView fila2 in DataGridIngresoProducto.ItemsSource)
                        //    {
                        //        //Obtiene o establece el valor de una columna especificada.
                        //        fila2[3] = Convert.ToDouble(txtCantidad.Text) + Convert.ToDouble(fila2[3].ToString());

                        //        fila2[4] = (SubTotal + Convert.ToDecimal(fila2[4])).ToString("N2");

                        //        LimpiarDetalle();
                        //    }
                        //}

                        ////Esperamos que funcione

                        //else
                        //{
                        //    SubTotal = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtCosto_Unitario.Text);

                        //    //string CantidadPro = Convert.ToString(txtCantidad.Text);
                        //    //string CostoUnitPro = Convert.ToString(txtCosto_Unitario.Text);
                        //    //DataGridIngresoProducto.Items.Add(new { Id_Producto = txtId_Producto.Text, Nombre = txtNombre_Producto.Text, Cantidad = CantidadPro, Costo_Unitario = CostoUnitPro, Sub_Total = SubTotal.ToString("N2") });

                        //    //DataGridIngresoProducto.UnselectAll();

                        //    //btnAgregarProducto.Focus();
                        //    //ContFila++;

                        //    //LimpiarDetalle();

                        //    List<DataGridItemsProducto> productos = new List<DataGridItemsProducto>();

                        //    productos.Add(new DataGridItemsProducto() { Id_Producto = txtId_Producto.Text, Nombre = txtNombre_Producto.Text, Cantidad = txtCantidad.Text, Costo_Unitario = txtCosto_Unitario.Text, Sub_Total = SubTotal.ToString("N2") });

                        //    DataGridIngresoProducto.ItemsSource = productos;

                        //    DataGridIngresoProducto.UnselectAll();

                        //    btnAgregarProducto.Focus();
                        //    ContFila++;

                        //    LimpiarDetalle();
                        //}

                        #endregion

                    }

                    #region sumar el total

                    Total = 0;

                    //DataTable table;

                    //for (int j = 0;  j < DataGridIngresoProducto.Items.Count; j++)
                    //{
                    //    table.Rows.Add(DataGridIngresoProducto.Items[j]);
                    //}


                    foreach (DataRowView row in DataGridIngresoProducto.ItemsSource)
                    {
                        Total += Convert.ToDecimal(row[5]);
                    }

                    txtTotal_Pago.Text = Total.ToString("N2");

                    //foreach (DataRowView fila3 in DataGridIngresoProducto.ItemsSource)
                    //{
                    //    Total += Convert.ToDecimal(fila3[4]);
                    //}

                    //txtTotal_Pago.Text = Total.ToString("N2");

                    //Total = 0;

                    //string Prueba = "";

                    //for (int i = 0; i < DataGridIngresoProducto.Items.Count; i++)
                    //{
                    //    //TextBlock tb = DataGridIngresoProducto.ColumnFromDisplayIndex(4).GetCellContent(DataGridIngresoProducto.SelectedItem[i]) as TextBlock;


                    //    //Total += (decimal.Parse((DataGridIngresoProducto.Columns[3].GetCellContent(DataGridIngresoProducto.Items[i] as TextBlock).ToString())));

                    //    //Total = (decimal.Parse((DataGridIngresoProducto.Columns[3].GetCellContent(DataGridIngresoProducto.Items[i]).ToString())));

                    //    //txtTotal_Pago.Text = Total.ToString("N2");

                    //    //Prueba = ((DataGridIngresoProducto.Columns[3].GetCellContent(DataGridIngresoProducto.Items[i]) as TextBlock).Text);
                    //}

                    //System.Windows.Forms.MessageBox.Show(Prueba, "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    #endregion
                }
            }

            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Producto no fue agregado por: " + ex, "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }

        }

        #endregion


        private void BtnBuscarProducto_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarProducto();
        }

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

            //var Datos = VistaProducto.DataGridGestionProductos;



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
                System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Proveedor en la lista proveedores!!", "Seleccionar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        
       
        public static DataGridRow GetRow(DataGrid dataGrid, int index)
        {
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {

                dataGrid.ScrollIntoView(dataGrid.Items[index]);
                dataGrid.UpdateLayout();

                row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(index);
            }

            return row;
            
            
        }

        public void row22()
        {
            decimal SubTotal = 0;
            //dataGridProducto = DataGridIngresoProducto;

            ////dataGridProducto.Items.Add(new { Id_Producto = txtId_Producto.Text, Nombre = txtNombre_Producto.Text, Cantidad = txtCantidad.Text, Costo_Unitario = txtCosto_Unitario.Text, Sub_Total = SubTotal.ToString("N2") });

            //List<DataGridItemsProducto> productos = new List<DataGridItemsProducto>();

            //productos.Add(new DataGridItemsProducto() { Id_Producto = txtId_Producto.Text, Nombre = txtNombre_Producto.Text, Cantidad = txtCantidad.Text, Costo_Unitario = txtCosto_Unitario.Text, Sub_Total = SubTotal.ToString("N2") });

            //DataGridIngresoProducto.ItemsSource = productos;

            SubTotal = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtCosto_Unitario.Text);

            //DataGridIngresoProducto.Items.Add(new { Id_Producto = txtId_Producto.Text, Nombre = txtNombre_Producto.Text, Cantidad = txtCantidad.Text, Costo_Unitario = txtCosto_Unitario.Text, Sub_Total = SubTotal.ToString("N2") });

            //DataGridIngresoProducto.UnselectAll();

            //btnAgregarProducto.Focus();

            //CE_Ingreso_Productos CE = new CE_Ingreso_Productos();

            //CE.AddProducto.Add(new DataGridItemsProducto());

            //this.DataGridIngresoProducto.Items.Add(new { Id_Producto = txtId_Producto.Text, Nombre = txtNombre_Producto.Text, Cantidad = txtCantidad.Text, Costo_Unitario = txtCosto_Unitario.Text, Sub_Total = SubTotal.ToString("N2") });

           

            Total = 0;

            foreach (DataRowView row in this.DataGridIngresoProducto.ItemsSource)
            {
                Total += Convert.ToDecimal(row[5]);
            }

            txtTotal_Pago.Text = Total.ToString("N2");

            LimpiarDetalle();

        }

        //List<DataGridItemsProducto> productos = new List<DataGridItemsProducto>();

        //public List<DataGridItemsProducto> GetProducto()
        //{
        //    productos.Add(new DataGridItemsProducto() { Id_Producto = "1", Nombre = "Leonel", Cantidad = "2", Costo_Unitario = "4", Sub_Total = "5" });
        //    productos.Add(new DataGridItemsProducto() { Id_Producto = "2", Nombre = "Leonel", Cantidad = "2", Costo_Unitario = "4", Sub_Total = "5" });

        //    return productos;
        //}

        #region prueba fallida
        //private void SeleccionItem ()
        //{

        //    FrmVistaProveedores VistaProveedor = new FrmVistaProveedores();

        //    txtId_Proveedor.Text = "";

        //    txtNombre_Proveedor.Text = "";

        //    int counter = 0;

        //    var Datos = VistaProveedor.DataGridGestionProveedores;

        //    int FirstValue = 0;

        //    int SecondValue = 0;

        //    foreach (DataRowView row in VistaProveedor.DataGridGestionProveedores.ItemsSource)
        //    {
        //        if (FirstValue != 0)
        //        {
        //            return;
        //        }

        //        else
        //        {
        //            txtId_Proveedor.Text = row[0].ToString();
        //            FirstValue++;
        //        }

        //    }
        //}

        //Método Mostrar las filas de la tabla 
        //Espera recibir un datagrid 
        //private void ShowRows(DataTable table)
        //{
        //    // Imprime el número de filas en la colección.

        //    labelrow.Text = Convert.ToString(table.Rows.Count);
        //}
        #endregion

        //private void RecorrerData()
        //{

        //    bool Existe = false;

        //    int No_Fila = 0;

        //    if (ContFila == 0)
        //    {

        //        DataGridIngresoProducto.Items.Add(new { Id_Producto = "1", Nombre = "Leonel", Cantidad = "5", Costo_Unitario = "2.50", Sub_Total = "50" });
        //        DataGridIngresoProducto.UnselectAll();
        //        ContFila++;
        //    }


        //    else
        //    {
        //        foreach (DataRowView fila in DataGridIngresoProducto.ItemsSource)
        //        {
        //            if (fila[0].ToString() == txtId_Producto.Text)
        //            {
        //                Existe = true;

        //                No_Fila = DataGridIngresoProducto.SelectedIndex;

        //                labelrow.Text = Convert.ToString(No_Fila);
        //            }
        //        }
        //    }
        //}

    }
}
