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
    /// Lógica de interacción para FrmAnularReserva.xaml
    /// </summary>
    public partial class FrmAnularReserva : Window
    {
        public FrmAnularReserva()
        {
            InitializeComponent();
        }

        public FrmAnularReserva(FrmAnularReserva Reservas)
        {
            InitializeComponent();
        }


        #region Instancia de objetos a usar

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Servicios Servicios = new CDo_Servicios();
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
            int Index = Convert.ToInt32(txtId_Reserva.Text);
            DataGridAnularReserva.ItemsSource = DetalleReservas.MostrarReserva(Index).AsDataView();
            DataGridAnularReserva.UnselectAllCells();
        }


        private void BtnAnularServicio_Click(object sender, RoutedEventArgs e)
        {
            Editar();
        }


        public virtual void Editar()
        {
            try
            {
                if (txtId_Reserva.Text == string.Empty || txtId_Cliente.Text == string.Empty || txtClienteNombre.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos por favor!!", "Cancelar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    System.Windows.Forms.DialogResult Resultado = System.Windows.Forms.MessageBox.Show("¿Está seguro que desea Cancela esta Reserva?", "Cancelar Reserva", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);

                    if (Resultado == System.Windows.Forms.DialogResult.Yes)
                    {
                        Reserva.Id_Cliente = Convert.ToInt32(txtId_Cliente.Text);
                        Reserva.Nombre = Convert.ToString(txtClienteNombre.Text);
                        Reserva.Id_Reserva = Convert.ToInt32(txtId_Reserva.Text);
                        Reserva.Fecha_Reserva = Convert.ToDateTime(dtp_FechaReserva.Text);
                        Reserva.Descuento = Convert.ToDecimal(txtDescuentoVenta.Text);
                        Reserva.Monto_Total = Convert.ToDecimal(txtMontoTotal.Text);
                        Reserva.Estado = "Cancelado";


                        foreach (DataRowView drv in DataGridAnularReserva.ItemsSource)
                        {
                            DataRow row = drv.Row;

                            DetalleReserva.Id_DetalleReserva = Convert.ToInt32(row[0].ToString());
                            DetalleReserva.Id_Reserva = Convert.ToInt32(txtId_Reserva.Text);
                            DetalleReserva.Id_Servicios = Convert.ToInt32(row[2].ToString());
                            DetalleReserva.Estado = Convert.ToString(row[4].ToString());
                            DetalleReserva.Descuento = Convert.ToDecimal(row[5].ToString());
                            DetalleReserva.Monto_Total = Convert.ToDecimal(row[6].ToString());

                            DetalleReservas.AnularDetalleReserva(DetalleReserva);
                        }

                        Reservas.AnularReserva(Reserva);
                        System.Windows.Forms.MessageBox.Show("Reserva Cancelada Correctamente", "Cancelar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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

        private void DataGridAnularReserva_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_DetalleReserva = e.Column.Header.ToString();

            if (Id_DetalleReserva == "Id_DetalleReserva")
            {
                e.Cancel = true;
            }

            string Id_Reserva = e.Column.Header.ToString();

            if (Id_Reserva == "Id_Reserva")
            {
                e.Cancel = true;
            }

            string Id_Servicios = e.Column.Header.ToString();

            if (Id_Servicios == "Id_Servicios")
            {
                e.Cancel = true;
            }

        }
        private void DataGridAnularReserva_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void BtnCancelarServicio_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
