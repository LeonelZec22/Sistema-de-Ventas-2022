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
    /// Lógica de interacción para FrmAnularIngresoProducto.xaml
    /// </summary>
    public partial class FrmAnularIngresoProducto : Window
    {
        public FrmAnularIngresoProducto()
        {
            InitializeComponent();
            
        }

        public FrmAnularIngresoProducto(FrmCompraDeProducto Compras)
        {
            InitializeComponent();
           
        }



        #region Instancia de objetos a usar

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Ingreso_Productos Ingresos = new CDo_Ingreso_Productos();
        CE_Ingreso_Productos Ingreso = new CE_Ingreso_Productos();
        CDo_Detalle_Ingreso DetalleIngresos = new CDo_Detalle_Ingreso();
        CE_Detalle_Ingreso DetalleIngreso = new CE_Detalle_Ingreso();

        

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


        protected void Actualizar()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MostrarDetalleIngreso();
            Procedimientos.FormatoMoneda(txtTotal_Pago);
        }

        private void MostrarDetalleIngreso()
        {
            int Index = Convert.ToInt32(txtId_IngresoProducto.Text);

            DataGridAnularProducto.ItemsSource = DetalleIngresos.MostrarIngresoProducto(Index).AsDataView();
        }

        

        private void BtnAnularIngreso_Click(object sender, RoutedEventArgs e)
        {
            Editar();
        }

        private void DataGridAnularProducto_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Detalle = e.Column.Header.ToString();

            string Id_IngresoProducto = e.Column.Header.ToString();

            string Id_Producto = e.Column.Header.ToString();

            if (Id_Detalle == "Id_Detalle")
            {
                e.Cancel = true;
            }


            if (Id_IngresoProducto == "Id_IngresoProducto")
            {
                e.Cancel = true;
            }

            if (Id_Producto == "Id_Producto")
            {
                e.Cancel = true;
            }

        }

        private void DataGridAnularProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        public virtual void Editar()
        {
            try
            {
                if (txtId_IngresoProducto.Text == string.Empty || txtNo_Ingreso.Text == string.Empty || txtId_Proveedor.Text == string.Empty || txtNombre_Proveedor.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos por favor!!", "Cancelar Ingreso Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    Ingreso.Id_IngresoProducto =Convert.ToInt32(txtId_IngresoProducto.Text);
                    Ingreso.No_Ingreso = txtNo_Ingreso.Text;
                    Ingreso.Id_Proveedor = Convert.ToInt32(txtId_Proveedor.Text);
                    Ingreso.Fecha_Ingreso = Convert.ToDateTime(dtp_FechaIngreso.Text);
                    Ingreso.Monto_total = Convert.ToDecimal(txtTotal_Pago.Text);
                    Ingreso.Estado = "Cancelado";

                    foreach (DataRowView drv in DataGridAnularProducto.ItemsSource)
                    {
                        DataRow row = drv.Row;

                        DetalleIngreso.Id_Detalle = Convert.ToInt32(row[0].ToString());
                        DetalleIngreso.Id_IngresoProducto = Convert.ToInt32(txtId_IngresoProducto.Text);
                        DetalleIngreso.Id_Producto = Convert.ToInt32(row[2].ToString());
                        DetalleIngreso.Nombre = Convert.ToString(row[3].ToString());
                        DetalleIngreso.Cantidad = Convert.ToInt32(row[4].ToString());
                        DetalleIngreso.Costo_Unitario = Convert.ToDecimal(row[5].ToString());
                        DetalleIngreso.Sub_Total = Convert.ToDecimal(row[6].ToString());

                        DetalleIngresos.AnularDetalleIngreso(DetalleIngreso);
                    }

                    Ingresos.AnularIngreso(Ingreso);

                    //System.Windows.Forms.MessageBox.Show("El Ingreso de Producto fue Cancelado correctamente!!", "Cancelar Ingreso Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    this.Hide();
                    Actualizar();
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Ingreso de Producto no fue agregado por: " + ex, "Agregar Ingreso Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            
        }

        private void TxtId_IngresoProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                e.Handled = true;
            }
        }

        private void BtnCancelarIngreso_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
