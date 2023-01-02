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
    /// Lógica de interacción para FrmVenta_Paquete.xaml
    /// </summary>
    public partial class FrmVenta_Paquete : Window
    {
        public FrmVenta_Paquete()
        {
            InitializeComponent();
        }

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Venta_Paquetes Ventas = new CDo_Venta_Paquetes();
        CE_Ventas_Paquetes Venta = new CE_Ventas_Paquetes();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            //DataGridVentaPaquete.ItemsSource = Ventas.MostrarVentasPaquetes().AsDataView();
            DataGridVentaPaquete.ItemsSource = Procedimientos.CargarDatos("Ventas_Paquetes").AsDataView();
            DataGridVentaPaquete.UnselectAllCells();

        }

        #region Evento para modificar las columnas autogeneradas del DataGrid
        private void DataGridVentaPaquete_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Venta_Paquetes = e.Column.Header.ToString();

            if (Id_Venta_Paquetes == "Id_Venta_Paquetes")
            {
                e.Cancel = true;

            }

            string Id_Cliente = e.Column.Header.ToString();

            if (Id_Cliente == "Id_Cliente")
            {
                e.Cancel = true;

            }

            string Id_Paquetes = e.Column.Header.ToString();

            if (Id_Paquetes == "Id_Paquetes")
            {
                e.Cancel = true;

            }


            if (e.PropertyType == typeof(System.DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
            }

            #region Modificar columnas

            string Nombre = e.Column.Header.ToString();
            string Paquete = e.Column.Header.ToString();
            string Fecha_Venta = e.Column.Header.ToString();
            string Estado = e.Column.Header.ToString();
            string Cantidad_Vendida = e.Column.Header.ToString();
            string Cantidad_Usada = e.Column.Header.ToString();
            string Precio_Venta = e.Column.Header.ToString();

            if (Nombre == "Cliente")
            {
                e.Column.Width = 175;

            }

            if (Paquete == "Paquete")
            {
                e.Column.Width = 170;


            }

            if (Fecha_Venta == "Fecha Venta")
            {
                e.Column.Width = 117;
            }

            if (Estado == "Estado")
            {
                //e.Column.Width = 130;
            }

            if (Cantidad_Vendida == "Cantidad_Vendida")
            {
                e.Column.Header = "Vendida";
                e.Column.Width = 100;
            }

            if (Cantidad_Usada == "Cantidad_Usada")
            {
                e.Column.Header = "Usada";
                e.Column.Width = 100;
            }

            if (Precio_Venta == "Precio")
            {
                e.Column.Width = 100;
            }

            #endregion 

        }

        #endregion

        //Invocacion de un evento creado para recargar este formulario o pantalla
        private void AgVen_UpdateEventHandler(object sender, FrmAgregarVentaPaquete.UpdateEventArgs args)
        {
            CargarDatos();
        }
        private void EdVen_UpdateEventHandler(object sender, FrmEditarVentaPaquete.UpdateEventArgs args)
        {
            CargarDatos();
        }

        #region Botones del encabezado
        private void BtnNuevaVenta_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregarVentaPaquete AgregarVentas = new FrmAgregarVentaPaquete(this);
            AgregarVentas.UpdateEventHandler += AgVen_UpdateEventHandler;
            AgregarVentas.ShowDialog();
        }

        FrmEditarVentaPaquete EditarVenta = new FrmEditarVentaPaquete();
        DataGrid dg;
        DataRowView dr;

        private void BtnEditarVenta_Click(object sender, RoutedEventArgs e)
        {
            if (dr != null)
            {
               
                EditarVenta.ShowDialog();
                DataGridVentaPaquete.UnselectAllCells();
            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Por favor seleccione un dato!!! ", "Editar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void BtnAnularVenta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dr != null)
                {

                    Venta.Id_Cliente = Convert.ToInt32(EditarVenta.txtId_Cliente.Text);
                    Venta.Id_Paquetes = Convert.ToInt32(EditarVenta.txtId_Paquetes.Text);
                    Venta.Cliente = Convert.ToString(EditarVenta.txtClienteNombre.Text);
                    Venta.Paquete = Convert.ToString(EditarVenta.txtNombre_Paquete.Text);
                    Venta.Fecha_Venta = Convert.ToDateTime(EditarVenta.dtp_FechaVenta.Text);
                    Venta.Cantidad_Vendida = Convert.ToInt32(EditarVenta.txtCantidadVendida.Text);
                    Venta.Cantidad_Usada = Convert.ToInt32(EditarVenta.txtCantidadUsar.Text);
                    Venta.Estado = "Anulado";
                    Venta.Precio_Venta = Convert.ToDecimal(EditarVenta.txtPrecio_Venta.Text);
                    Venta.Id_Venta_Paquetes = Convert.ToInt32(EditarVenta.txtId_Venta_Paquetes.Text);

                    Ventas.AnularVentaPaquetes(Venta);

                    CargarDatos();
                }

                else
                {
                    System.Windows.Forms.MessageBox.Show("Por favor seleccione un dato!!! ", "Editar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
            }

            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se pudo anular; " + ex.Message, "Editar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        //Evento para capturar una fila seleccionada del DataGrid
        private void DataGridVentaPaquete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg = sender as DataGrid;

            dr = dg.SelectedItem as DataRowView;

            if (DataGridVentaPaquete.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("No Hay Compras para Anular!!! ", " Editar Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            else
            {
                if (dr != null)
                {
                    EditarVenta.UpdateEventHandler += EdVen_UpdateEventHandler;
                    EditarVenta.txtId_Venta_Paquetes.Text = dr[0].ToString();
                    EditarVenta.txtId_Cliente.Text = dr[1].ToString();
                    EditarVenta.txtId_Paquetes.Text = dr[2].ToString();
                    EditarVenta.txtClienteNombre.Text = dr[3].ToString();
                    EditarVenta.txtNombre_Paquete.Text = dr[4].ToString();
                    EditarVenta.dtp_FechaVenta.Text = dr[5].ToString();
                    EditarVenta.txtCantidadVendida.Text = dr[7].ToString();
                    EditarVenta.txtCantidadUsar.Text = dr[8].ToString();
                    EditarVenta.txtPrecio_Venta.Text = dr[9].ToString();

                   


                }
            }
        }


        #region Menu Lateral
        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Principal = new MainWindow();
            Hide();
            Principal.ShowDialog();
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

        #region buscador
        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar();
        }

        public virtual void Buscar()
        {
            try
            {
                if (cboVentaPaquete.Text == "Cliente")
                {
                    Venta.Buscar = txtBuscador.Text.Trim();
                    DataGridVentaPaquete.ItemsSource = Ventas.Buscar_VentaPaquete_Cliente(Venta).AsDataView();
                }
                else if (cboVentaPaquete.Text == "Estado")
                {
                    Venta.Buscar = txtBuscador.Text.Trim();
                    DataGridVentaPaquete.ItemsSource = Ventas.Buscar_VentaPaquete_Estado(Venta).AsDataView();
                }
                else if (cboVentaPaquete.Text == "Paquete")
                {
                    Venta.Buscar = txtBuscador.Text.Trim();
                    DataGridVentaPaquete.ItemsSource = Ventas.Buscar_VentaPaquete_Paquete(Venta).AsDataView();
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Cliente no fue encontrado por: " + ex.Message, "Buscar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        #endregion

    }
}
