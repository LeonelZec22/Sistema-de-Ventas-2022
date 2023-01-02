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
    /// Lógica de interacción para FrmCompraDeProducto.xaml
    /// </summary>
    public partial class FrmCompraDeProducto : Window
    {
        public FrmCompraDeProducto()
        {
            InitializeComponent();
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Ingreso_Productos IngresoProductos = new CDo_Ingreso_Productos();
        CE_Ingreso_Productos IngresoProducto = new CE_Ingreso_Productos();


        //Método para cargar datos de la tabla Ingreso Productos 

        private void CargarDatos()
        {
            //Procedimiento Mostrar_Ingreso 

            DataGridIngresoProducto.ItemsSource = IngresoProductos.MostrarIngresoProductos().AsDataView();
            
        }

        //Evento para ocultar una columna de un datagrid autogenerico
        private void DataGridIngresoProducto_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_IngresoProducto = e.Column.Header.ToString();

            if (Id_IngresoProducto == "Id_IngresoProducto")
            {
                e.Cancel = true;
            }

            string Id_Proveedor = e.Column.Header.ToString();

            if (Id_Proveedor == "Id_Proveedor")
            {
                e.Cancel = true;

            }

        }

        private void AgIn_UpdateEventHandler(object sender, FrmAgregarIngreso.UpdateEventArgs args)
        {
            CargarDatos();
        }

        private void AnIn_UpdateEventHandler(object sender, FrmAnularIngresoProducto.UpdateEventArgs args)
        {
            CargarDatos();
        }

        private void BtnNuevoIngreso_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregarIngreso AgregarProducto = new FrmAgregarIngreso(this);
            AgregarProducto.UpdateEventHandler += AgIn_UpdateEventHandler;
            //this.Hide();
            AgregarProducto.ShowDialog();
        }

        FrmAnularIngresoProducto AnularProducto = new FrmAnularIngresoProducto();
        DataGrid dg;
        DataRowView dr;

        private void BtnAnularIngreso_Click(object sender, RoutedEventArgs e)
        {
            //FrmAnularIngresoProducto AnularProducto = new FrmAnularIngresoProducto(this);
            //AnularProducto.UpdateEventHandler += AnIn_UpdateEventHandler;
            if (dr != null)
            {
                //AnularProducto.MostrarDetalleIngreso();
                AnularProducto.ShowDialog();
                DataGridIngresoProducto.UnselectAllCells();
                
            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Por favor seleccione un dato!!! ", "Anular Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        Informes.FrmMostrar_Ingreso_Producto Mostrar = new Informes.FrmMostrar_Ingreso_Producto();

        private void BtnImprimirInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dr != null)
                {
                    Mostrar.ShowDialog();
                    DataGridIngresoProducto.UnselectAllCells();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Por favor seleccione un dato!!! ", "Imprimir Informe", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    DataGridIngresoProducto.UnselectAllCells();
                }
            }

            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se ha Podido generar el Informe Por: " + ex, "Anular Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                DataGridIngresoProducto.UnselectAllCells();
            }
        }
        private void DataGridIngresoProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg = sender as DataGrid;

            dr = dg.SelectedItem as DataRowView;

            if (dr != null)
            {
                //Procedimiento Mostrar_Ingreso 
                AnularProducto.UpdateEventHandler += AnIn_UpdateEventHandler;
                AnularProducto.txtId_IngresoProducto.Text = dr[0].ToString();
                AnularProducto.txtId_Proveedor.Text = dr[1].ToString();
                AnularProducto.txtNo_Ingreso.Text = dr[2].ToString();
                AnularProducto.txtNombre_Proveedor.Text = dr[3].ToString();
                AnularProducto.dtp_FechaIngreso.Text = dr[4].ToString();
                AnularProducto.txtTotal_Pago.Text = dr[5].ToString();

                Mostrar.Id_IngresoProducto = Convert.ToInt32(dr[0].ToString());

            }
        }

        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar();
        }

        public virtual void Buscar()
        {
            try
            {
                if (cboIngresoProducto.Text == "Proveedor")
                {
                    IngresoProducto.Buscar = txtBuscador.Text.Trim();
                    DataGridIngresoProducto.ItemsSource = IngresoProductos.Buscar_Ingreso_Proveedor(IngresoProducto).AsDataView();
                }
               

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Proveedor no fue encontrado por: " + ex.Message, "Buscar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
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
                Application.Current.Shutdown();
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

       
       
    }
}
