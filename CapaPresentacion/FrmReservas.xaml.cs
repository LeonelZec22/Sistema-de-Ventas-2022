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
    /// Lógica de interacción para FrmReservas.xaml
    /// </summary>
    public partial class FrmReservas : Window
    {
        public FrmReservas()
        {
            InitializeComponent();
        }


        //Instancia de la clases
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Reservas Reservas = new CDo_Reservas();
        CE_Reservas Reserva = new CE_Reservas();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }
        
        //Método para mostrar todas las reservas
        private void CargarDatos()
        {
            DataGridReserva.ItemsSource = Reservas.MostrarReservas().AsDataView();
            DataGridReserva.UnselectAllCells();

        }
        private void DataGridReserva_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Reserva = e.Column.Header.ToString();

            if (Id_Reserva == "Id_Reserva")
            {
                e.Cancel = true;

            }

            string Id_Cliente = e.Column.Header.ToString();

            if (Id_Cliente == "Id_Cliente")
            {
                e.Cancel = true;

            }

            if (e.PropertyType == typeof(System.DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
            }
            //DataGridReserva.Columns[3].HeaderStringFormat.
        }

        private void AgRes_UpdateEventHandler(object sender, FrmAgregarReserva.UpdateEventArgs args)
        {
            CargarDatos();
        }
        private void AnRes_UpdateEventHandler(object sender, FrmAnularReserva.UpdateEventArgs args)
        {
            CargarDatos();
        }


        private void BtnNuevaReserva_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregarReserva AgregarReservas = new FrmAgregarReserva(this);
            AgregarReservas.UpdateEventHandler += AgRes_UpdateEventHandler;
            AgregarReservas.ShowDialog();
        }



        FrmAnularReserva AnularReserva = new FrmAnularReserva();
        DataGrid dg;
        DataRowView dr;



        private void DataGridReserva_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg = sender as DataGrid;

            dr = dg.SelectedItem as DataRowView;

            if (DataGridReserva.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("No Hay Reservas para Anular!!! ", "Anular Resevas", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

            else
            {
                if (dr != null)
                {
                    AnularReserva.UpdateEventHandler += AnRes_UpdateEventHandler;
                    AnularReserva.txtId_Reserva.Text = dr[0].ToString();
                    AnularReserva.txtId_Cliente.Text = dr[1].ToString();
                    AnularReserva.txtClienteNombre.Text = dr[2].ToString();
                    AnularReserva.dtp_FechaReserva.Text = dr[3].ToString();
                    AnularReserva.txtEstado.Text = dr[4].ToString();
                    AnularReserva.txtDescuentoVenta.Text = dr[5].ToString();
                    AnularReserva.txtMontoTotal.Text = dr[6].ToString();

                    //Mostrar.Id_Venta = Convert.ToInt32(dr[0].ToString());


                }

            }
        }
        private void BtnAnularReserva_Click(object sender, RoutedEventArgs e)
        {
            if (dr != null)
            {
                AnularReserva.MostrarDetalleVenta();
                AnularReserva.ShowDialog();
                DataGridReserva.UnselectAllCells();
            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Por favor seleccione un dato!!! ", "Anular Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void BtnImprimirInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar();
        }

        public virtual void Buscar()
        {
            try
            {
                if (cboReserva.Text == "Nombre")
                {
                    Reserva.Buscar = txtBuscador.Text.Trim();
                    DataGridReserva.ItemsSource = Reservas.Buscar_Reserva_Nombre(Reserva).AsDataView();
                }
                else if (cboReserva.Text == "Fecha")
                {
                    Reserva.Buscar = txtBuscador.Text.Trim();
                    DataGridReserva.ItemsSource = Reservas.Buscar_Reserva_FechaReserva(Reserva).AsDataView();
                }
                else if (cboReserva.Text == "Estado")
                {
                    Reserva.Buscar = txtBuscador.Text.Trim();
                    DataGridReserva.ItemsSource = Reservas.Buscar_Reserva_Estado(Reserva).AsDataView();
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("La Reserva no fue encontrada por: " + ex.Message, "Buscar Reserva", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }


        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            FrmProductos Productos = new FrmProductos();

            Hide();

            Productos.ShowDialog();

            Close();
        }

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

        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {
            MainWindow FormPrincipal = new MainWindow();
            Hide();
            FormPrincipal.ShowDialog();
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

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            FrmInventario frmInventario = new FrmInventario();
            Hide();
            frmInventario.ShowDialog();
            Close();
        }

        private void BtnVentas_Click(object sender, RoutedEventArgs e)
        {
            MenuVentas ventas = new MenuVentas();
            Hide();
            ventas.ShowDialog();
            Close();
        }
    }
}
