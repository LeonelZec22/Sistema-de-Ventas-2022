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
using CapaNegocio;
using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para FrmVentas_Servicios.xaml
    /// </summary>
    public partial class FrmVentas_Servicios : Window
    {
        public FrmVentas_Servicios()
        {
            InitializeComponent();
        }

        //Instancia de la clases
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Ventas_Servicios VentasServicios = new CDo_Ventas_Servicios();
        CE_Ventas_Servicios VentaServicio = new CE_Ventas_Servicios();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            DataGridVentaReservaa.ItemsSource = VentasServicios.MostrarVentasServicios().AsDataView();
            DataGridVentaReservaa.UnselectAllCells();

        }

        #region Evento para modificar las columnas autogeneradas del DataGrid
        private void DataGridVentaReservaa_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Venta_Servicios = e.Column.Header.ToString();

            if (Id_Venta_Servicios == "Id_Venta_Servicios")
            {
                e.Cancel = true;

            }

            string Id_Cliente = e.Column.Header.ToString();

            if (Id_Cliente == "Id_Cliente")
            {
                e.Cancel = true;

            }

            string Nombre = e.Column.Header.ToString();
            string Fecha_Venta = e.Column.Header.ToString();
            string Estado = e.Column.Header.ToString();
            string Descuento = e.Column.Header.ToString();
            string Monto_Total = e.Column.Header.ToString();
            string Usuario = e.Column.Header.ToString();

            if (Nombre == "Nombre")
            {
                e.Column.Width = 200;
            }

            //if (Fecha_Venta == "Fecha_Venta")
            //{
            //    e.Column.Width = 117;
            //}

            //if (Estado == "Estado")
            //{
            //    e.Column.Width = 130;
            //}

            //if (Descuento == "Descuento")
            //{
            //    e.Column.Width = 100;
            //}

            //if (Monto_Total == "Monto_Total")
            //{
            //    e.Column.Width = 120;
            //}

            //if (Usuario == "Usuario")
            //{
            //    e.Column.Width = 150;
            //}



            if (e.PropertyType == typeof(System.DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
            }
        }

        #endregion

        //Invocacion de un evento creado para recargar este formulario o pantalla
        private void AgVenSer_UpdateEventHandler(object sender, FrmAgregar_Venta_Servicios.UpdateEventArgs args)
        {
            CargarDatos();
        }

        private void AnVenSer_UpdateEventHandler(object sender, FrmAnular_Venta_Servicio.UpdateEventArgs args)
        {
            CargarDatos();
        }

        #region Botones del encabezado
        private void BtnNuevaVenta_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregar_Venta_Servicios AgregarVentas = new FrmAgregar_Venta_Servicios(this);
            AgregarVentas.UpdateEventHandler += AgVenSer_UpdateEventHandler;
            AgregarVentas.ShowDialog();
        }


        FrmAnular_Venta_Servicio AnularVenta = new FrmAnular_Venta_Servicio();
        DataGrid dg;
        DataRowView dr;


        private void BtnAnularVenta_Click(object sender, RoutedEventArgs e)
        {
            if (dr != null)
            {
                AnularVenta.MostrarDetalleVenta();
                AnularVenta.ShowDialog();
                DataGridVentaReservaa.UnselectAllCells();
            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Por favor seleccione un dato!!! ", "Anular Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        #endregion  

        //Evento para capturar una fila seleccionada del DataGrid

        private void DataGridVentaReservaa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg = sender as DataGrid;

            dr = dg.SelectedItem as DataRowView;

            if (DataGridVentaReservaa.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("No Hay Compras para Anular!!! ", "Anular Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

            else
            {
                if (dr != null)
                {
                    AnularVenta.UpdateEventHandler += AnVenSer_UpdateEventHandler;
                    AnularVenta.txtId_Venta_Servicios.Text = dr[0].ToString();
                    AnularVenta.txtId_Cliente.Text = dr[1].ToString();
                    AnularVenta.txtClienteNombre.Text = dr[2].ToString();
                    AnularVenta.dtp_FechaVenta.Text = dr[3].ToString();
                    AnularVenta.txtDescuentoVenta.Text = dr[5].ToString();
                    AnularVenta.txtMontoTotal.Text = dr[6].ToString();

                    //Mostrar.Id_Venta = Convert.ToInt32(dr[0].ToString());


                }

            }
        }

        #region Buscador

        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar();
        }

        public virtual void Buscar()
        {
            try
            {
                if (cboVenta.Text == "Cliente")
                {
                    VentaServicio.Buscar = txtBuscador.Text.Trim();
                    DataGridVentaReservaa.ItemsSource = VentasServicios.Buscar_VentaReserva_Nombre(VentaServicio).AsDataView();
                }
                else if (cboVenta.Text == "Estado")
                {
                    VentaServicio.Buscar = txtBuscador.Text.Trim();
                    DataGridVentaReservaa.ItemsSource = VentasServicios.Buscar_VentaReserva_Estado(VentaServicio).AsDataView();
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Cliente no fue encontrado por: " + ex.Message, "Buscar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region  Formas de Cerrar la App
        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Windows.Forms.DialogResult Resultado = System.Windows.Forms.MessageBox.Show("¿Está seguro que desea Cerrar la Aplicacion?", "Cerrar Aplicacion", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);

                if (Resultado == System.Windows.Forms.DialogResult.Yes)
                {

                    Application.Current.Shutdown();

                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Hubo un error al cerrar la aplicacion: " + ex.Message, "Cerrar Aplicación", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void MinimizeApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Hubo un error al cerrar la aplicacion: " + ex.Message, "Cerrar Aplicación", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Hubo un error al cerrar la aplicacion: " + ex.Message, "Cerrar Aplicación", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Menu Lateral
        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {
            MainWindow FormPrincipal = new MainWindow();
            Hide();
            FormPrincipal.ShowDialog();
            Close();
        }

        private void BtnPaquete_Click(object sender, RoutedEventArgs e)
        {

            MenuPaquete menuPaquete = new MenuPaquete();
            Hide();
            menuPaquete.ShowDialog();
            Close();
        }

        private void BtnProveedores_Click(object sender, RoutedEventArgs e)
        {
            MenuProveedores Proveedor = new MenuProveedores();
            Hide();
            Proveedor.ShowDialog();
            Close();
        }

        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            FrmClientes clientes = new FrmClientes();
            Hide();
            clientes.ShowDialog();
            Close();
        }

        private void BtnServicios_Click(object sender, RoutedEventArgs e)
        {
            FrmServicios servicios = new FrmServicios();
            Hide();
            servicios.ShowDialog();
            Close();
        }

        private void BtnReservas_Click(object sender, RoutedEventArgs e)
        {
            MenuReserva Reserva = new MenuReserva();

            Hide();

            Reserva.ShowDialog();

            Close();
        }

        private void BtnVentas_Click(object sender, RoutedEventArgs e)
        {
            MenuVentas ventas = new MenuVentas();
            Hide();
            ventas.ShowDialog();
            Close();
        }

        #endregion


    }
}
