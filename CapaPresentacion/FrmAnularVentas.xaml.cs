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
    /// Lógica de interacción para FrmAnularVentas.xaml
    /// </summary>
    public partial class FrmAnularVentas : Window
    {
        public FrmAnularVentas()
        {
            InitializeComponent();
        }


        public FrmAnularVentas(FrmVentas Ventas)
        {
            InitializeComponent();
        }


        #region Instancia de objetos a usar

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Productos Productos = new CDo_Productos();
        CDo_Ventas Ventas = new CDo_Ventas();
        CE_Ventas Venta = new CE_Ventas();
        CDo_Detalle_Ventas DetalleVentas = new CDo_Detalle_Ventas();
        CE_Detalle_Ventas DetalleVenta = new CE_Detalle_Ventas();
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


        protected void Anular()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);
        }

        #endregion
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Procedimientos.FormatoMoneda(txtSubTotal);
            Procedimientos.FormatoMoneda(txtDescuentoVenta);
            Procedimientos.FormatoMoneda(txtMontoTotal);
            Usuarios.DatosUsuario(MainWindow.Usuario);
            tbUsuario.Text = Convert.ToString(InformacionUsuario.IdUsuario);
            MostrarDetalleVenta();
        }

        public void MostrarDetalleVenta()
        {
            int Index = Convert.ToInt32(txtId_Venta.Text);
            DataGridAnularVenta.ItemsSource = DetalleVentas.MostrarVentas(Index).AsDataView();
            DataGridAnularVenta.UnselectAllCells();
        }
        private void BtnAnularVenta_Click(object sender, RoutedEventArgs e)
        {
            Editar();
        }


        public virtual void Editar()
        {
            try
            {
                if (txtId_Venta.Text == string.Empty || txtClienteNombre.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos por favor!!", "Cancelar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    System.Windows.Forms.DialogResult Resultado = System.Windows.Forms.MessageBox.Show("¿Está seguro que desea Cancela esta Venta?", "Cancelar Venta", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);

                    if (Resultado == System.Windows.Forms.DialogResult.Yes)
                    {
                        Venta.Id_Cliente = Convert.ToInt32(txtId_Cliente.Text);
                        Venta.Id_Venta = Convert.ToInt32(txtId_Venta.Text);
                        Venta.Fecha_Venta = Convert.ToDateTime(dtp_FechaVenta.Text);
                        Venta.Sub_Total = Convert.ToDecimal(txtSubTotal.Text);
                        Venta.Descuento = Convert.ToDecimal(txtDescuentoVenta.Text);
                        Venta.Monto_Total = Convert.ToDecimal(txtMontoTotal.Text);
                        Venta.Estado = "Cancelado";
                        Venta.Id_Usuario = Convert.ToInt32(tbUsuario.Text);


                        foreach (DataRowView drv in DataGridAnularVenta.ItemsSource)
                        {
                            DataRow row = drv.Row;

                            DetalleVenta.Id_DetalleVenta = Convert.ToInt32(row[0].ToString());
                            DetalleVenta.Id_Venta = Convert.ToInt32(txtId_Venta.Text);
                            DetalleVenta.Id_Producto = Convert.ToInt32(row[2].ToString());
                            DetalleVenta.Cantidad = Convert.ToInt32(row[4].ToString());
                            DetalleVenta.Precio_Venta = Convert.ToDecimal(row[5].ToString());
                            DetalleVenta.Sub_Total = Convert.ToDecimal(row[6].ToString());
                            DetalleVenta.Descuento = Convert.ToDecimal(row[7].ToString());
                            DetalleVenta.Monto_Total = Convert.ToDecimal(row[8].ToString());

                            DetalleVentas.AnularDetalleVenta(DetalleVenta);
                        }

                        Ventas.AnularVenta(Venta);

                        Anular();
                        this.Hide();
                    }

                    else if (Resultado == System.Windows.Forms.DialogResult.No)
                    {
                        MostrarDetalleVenta();
                    }

                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("La Venta no fue cancelada por: " + ex, "Cancelar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }


        private void BtnCancelarVenta_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
            Anular();
        }

        private void DataGridAnularVenta_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_DetalleVenta = e.Column.Header.ToString();

            string Id_Venta = e.Column.Header.ToString();

            string Id_Producto = e.Column.Header.ToString();

            if (Id_DetalleVenta == "Id_DetalleVenta")
            {
                e.Cancel = true;
            }


            if (Id_Venta == "Id_Venta")
            {
                e.Cancel = true;
            }

            if (Id_Producto == "Id_Producto")
            {
                e.Cancel = true;
            }

        }

        private void DataGridAnularVenta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            Anular();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
            Anular();
        }
    }
}
