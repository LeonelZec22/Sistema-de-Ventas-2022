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
    /// Lógica de interacción para FrmAnular_Venta_Servicio.xaml
    /// </summary>
    public partial class FrmAnular_Venta_Servicio : Window
    {
        public FrmAnular_Venta_Servicio()
        {
            InitializeComponent();
        }

        public FrmAnular_Venta_Servicio(FrmVentas_Servicios Ventas)
        {
            InitializeComponent();
        }

        #region Instancia de objetos a usar

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Reservas Reservas = new CDo_Reservas();
        CDo_Ventas_Servicios VentasServicios = new CDo_Ventas_Servicios();
        CE_Ventas_Servicios VentaServicio = new CE_Ventas_Servicios();
        CDo_Detalle_Ventas_Servicioscs DetalleVentasServicios = new CDo_Detalle_Ventas_Servicioscs();
        CE_Detalle_Venta_Servicios DetalleVentaServicios = new CE_Detalle_Venta_Servicios();


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
            Procedimientos.FormatoMoneda(txtDescuentoVenta);
            Procedimientos.FormatoMoneda(txtMontoTotal);
            MostrarDetalleVenta();
        }

        public void MostrarDetalleVenta()
        {
            int Index = Convert.ToInt32(txtId_Venta_Servicios.Text);
            DataGridVentaServicios.ItemsSource = DetalleVentasServicios.MostrarVentasServicios(Index).AsDataView();
            DataGridVentaServicios.UnselectAllCells();
        }

        private void BtnAnularVentaServicios_Click(object sender, RoutedEventArgs e)
        {
            Editar();
        }
        private void DataGridVentaServicios_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_DetalleVenta_Servicios = e.Column.Header.ToString();
            string Id_Venta_Servicios = e.Column.Header.ToString();
            string Id_Reserva = e.Column.Header.ToString();

            if (Id_DetalleVenta_Servicios == "Id_DetalleVenta_Servicios")
            {
                e.Cancel = true;
            }

            if (Id_Venta_Servicios == "Id_Venta_Servicios")
            {
                e.Cancel = true;
            }

            if (Id_Reserva == "Id_Reserva")
            {
                e.Cancel = true;
            }
        }

        public virtual void Editar()
        {
            try
            {
                if (txtId_Venta_Servicios.Text == string.Empty || txtClienteNombre.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos por favor!!", "Cancelar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    System.Windows.Forms.DialogResult Resultado = System.Windows.Forms.MessageBox.Show("¿Está seguro que desea Cancela esta Venta?", "Cancelar Venta", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);

                    if (Resultado == System.Windows.Forms.DialogResult.Yes)
                    {
                        VentaServicio.Id_Cliente = Convert.ToInt32(txtId_Cliente.Text);
                        VentaServicio.Id_Venta_Servicios = Convert.ToInt32(txtId_Venta_Servicios.Text);
                        VentaServicio.Fecha_Venta = Convert.ToDateTime(dtp_FechaVenta.Text);
                        VentaServicio.Descuento = Convert.ToDecimal(txtDescuentoVenta.Text);
                        VentaServicio.Monto_Total = Convert.ToDecimal(txtMontoTotal.Text);
                        VentaServicio.Estado = "Cancelado";
                        VentaServicio.Id_Usuario = 1;


                        foreach (DataRowView drv in DataGridVentaServicios.ItemsSource)
                        {
                            DataRow row = drv.Row;

                            DetalleVentaServicios.Id_DetalleVenta_Servicios = Convert.ToInt32(row[0].ToString());
                            DetalleVentaServicios.Id_Venta_Servicios = Convert.ToInt32(txtId_Venta_Servicios.Text);
                            DetalleVentaServicios.Id_Reserva = Convert.ToInt32(row[2].ToString());
                            DetalleVentaServicios.Fecha_Reserva = Convert.ToDateTime(row[4].ToString());
                            DetalleVentaServicios.Estado = Convert.ToString(row[5].ToString());
                            DetalleVentaServicios.Descuento = Convert.ToDecimal(row[6].ToString());
                            DetalleVentaServicios.Monto_Total = Convert.ToDecimal(row[7].ToString());

                            DetalleVentasServicios.AnularDetalleVentaServicios(DetalleVentaServicios);
                        }

                        VentasServicios.AnularVentaServicios(VentaServicio);
                        System.Windows.Forms.MessageBox.Show("Venta Cancelada Correctamente", "Cancelar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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

        private void BtnCancelarVentaServicios_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
        }
    }
}
