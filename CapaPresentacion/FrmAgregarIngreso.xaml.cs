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
        
        
        //Método para generar el numero de ingreso 

        private void Correlativo()
        {
            txtId_IngresoProducto.Text = Procedimientos.GenerarCodigoId("Ingreso_Productos");

            txtNo_Ingreso.Text = "INGR" + Procedimientos.GenerarCodigo("Ingreso_Productos");

            txtId_Detalle.Text = Procedimientos.GenerarCodigoId("Detalles_Ingreso");
        }


        DataGrid dg;
        DataRowView dr;

       

        private void BtnBuscarProveedor_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarProveedor();

            //this.Hide();

            #region comentarioos
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

        }

        // Método para agregar los productos al datagrid

        public static int ContFila = 0; //Contar fila 
        public static decimal Total; // Sumar el subtotal de los productos agregados 


        //Metódo para agregar detalle del producto
        private void AgregarDetalle()
        {
            decimal SubTotal = 0;

            //No se activa el método con los campos vacios 
            if (txtId_Producto.Text == string.Empty || txtNombre_Producto.Text == string.Empty || txtCantidad.Text == string.Empty || txtCosto_Unitario.Text == string.Empty)
            {
                System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos del detalle de producto!!", "Agregar Detalle", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                return;
            }

            else
            {
                bool Existe = false;

                int No_Fila = 0; //Numero de la fila seleccionada

                //Si no hay productos agregados en la tabla
                if (ContFila == 0)
                {
                    SubTotal = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtCosto_Unitario.Text);

                    DataGridIngresoProducto.Items.Add(txtId_Producto.Text);
                    DataGridIngresoProducto.Items.Add(txtNombre_Producto.Text);
                    DataGridIngresoProducto.Items.Add(txtCantidad.Text);
                    DataGridIngresoProducto.Items.Add(txtCosto_Unitario.Text);
                    DataGridIngresoProducto.Items.Add(SubTotal.ToString("N2"));

                    //LimpiarDetalle();

                    ContFila++;
                }
                 
                else
                {

                    

                    foreach (DataRowView row in DataGridIngresoProducto.ItemsSource)
                    {
                        
                        if(row[0].ToString() == txtId_Producto.Text)
                        {
                            Existe = true;
                            var IndexRow = DataGridIngresoProducto.Items.IndexOf(DataGridIngresoProducto.CurrentItem);
                            No_Fila = IndexRow;
                        }
                        
                    }

                    if(Existe == true)
                    {
                        SubTotal = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtCosto_Unitario.Text);
                        //DataGridIngresoProducto.ItemsSource() = 
                    }

                    else
                    {
                        SubTotal = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtCosto_Unitario.Text);

                        DataGridIngresoProducto.Items.Add(txtId_Producto.Text);
                        DataGridIngresoProducto.Items.Add(txtNombre_Producto.Text);
                        DataGridIngresoProducto.Items.Add(txtCantidad.Text);
                        DataGridIngresoProducto.Items.Add(txtCosto_Unitario.Text);
                        DataGridIngresoProducto.Items.Add(SubTotal.ToString("N2"));

                        //LimpiarDetalle();

                        ContFila++;
                    }

                    Total = 0;

                    //foreach(DataRowView row in DataGridIngresoProducto.Items)
                    //{
                    //    Total += Convert.ToDecimal(row);
                    //}
                }
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

        private void SeleccionarProveedor()
        {
           
            //Abrimos el Formulario de VistaProveedor

            FrmVistaProveedores VistaProveedor = new FrmVistaProveedores();

            VistaProveedor.ShowDialog();

            txtId_Proveedor.Text = "";

            txtNombre_Proveedor.Text = "";

            int counter = 0;

            var Datos = VistaProveedor.DataGridGestionProveedores;

            int FirstValue = 0;

            Boolean Primero = true;

            int SecondValue = 0;

            try
            {
                if(VistaProveedor.DialogResult == true)
                {


                    //SeleccionItem();

                    //foreach (DataRowView row in VistaProveedor.DataGridGestionProveedores.ItemsSource)
                    //{
                    //    txtId_Proveedor.Text = row[0].ToString();
                        
                       
                    //}

                    //foreach (DataGridCell row in VistaProveedor.DataGridGestionProveedores.ItemsSource)
                    //{
                    //    txtId_Proveedor.Text = row[0];


                    //}


                    foreach (DataRowView drv in VistaProveedor.DataGridGestionProveedores.SelectedItems)
                    {
                        DataRow row = drv.Row;

                        var IdProveedor = Convert.ToString(drv.Row[0]);

                        txtId_Proveedor.Text = IdProveedor;

                        var NombreProveedor = Convert.ToString(drv.Row[2]);

                        txtNombre_Proveedor.Text = NombreProveedor;
                    }



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

                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Proveedor en la lista proveedores!!", "Seleccionar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void SeleccionItem ()
        {

            FrmVistaProveedores VistaProveedor = new FrmVistaProveedores();

            txtId_Proveedor.Text = "";

            txtNombre_Proveedor.Text = "";

            int counter = 0;

            var Datos = VistaProveedor.DataGridGestionProveedores;

            int FirstValue = 0;

            int SecondValue = 0;

            foreach (DataRowView row in VistaProveedor.DataGridGestionProveedores.ItemsSource)
            {
                if (FirstValue != 0)
                {
                    return;
                }

                else
                {
                    txtId_Proveedor.Text = row[0].ToString();
                    FirstValue++;
                }

            }
        }
    }
}
