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
    /// Lógica de interacción para FrmVentas.xaml
    /// </summary>
    public partial class FrmVentas : Window
    {
        public FrmVentas()
        {
            InitializeComponent();
        }

        //Instancia de la clases
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Ventas Ventas = new CDo_Ventas();
        CE_Ventas Venta = new CE_Ventas();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

        //Método para cargar datos de la tabla Ingreso Productos 

        private void CargarDatos()
        {
            DataGridVenta.ItemsSource = Ventas.MostrarVentas().AsDataView();
            DataGridVenta.UnselectAllCells();

        }

        private void DataGridVenta_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Venta = e.Column.Header.ToString();

            if (Id_Venta == "Id_Venta")
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
            string Sub_Total = e.Column.Header.ToString();
            string Monto_Total = e.Column.Header.ToString();
            string Usuario = e.Column.Header.ToString();

            string Descuento = e.Column.Header.ToString();

            if (Nombre == "Nombre")
            {
                e.Column.Width = 225;
            }

            if (Fecha_Venta == "Fecha_Venta")
            {
                e.Column.Width = 117;
            }

            if (Estado == "Estado")
            {
                e.Column.Width = 130;
            }

            if (Estado == "Sub_Total")
            {
                e.Column.Width = 120;
            }

            if (Estado == "Monto_Total")
            {
                e.Column.Width = 120;
            }

            if (Estado == "Usuario")
            {
                e.Column.Width = 175;
            }

            if (Descuento == "Descuento")
            {
                e.Column.Width = 100;
            }

        }

        private void AgVen_UpdateEventHandler(object sender, FrmAgregarVenta.UpdateEventArgs args)
        {
            CargarDatos();
        }
        private void AnVen_UpdateEventHandler(object sender, FrmAnularVentas.UpdateEventArgs args)
        {
            CargarDatos();
        }



        private void BtnNuevaVenta_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregarVenta AgregarVentas = new FrmAgregarVenta(this);
            AgregarVentas.UpdateEventHandler += AgVen_UpdateEventHandler;
            AgregarVentas.ShowDialog();


        }


        FrmAnularVentas AnularVenta = new FrmAnularVentas();
        DataGrid dg;
        DataRowView dr;
        private void BtnAnularVenta_Click(object sender, RoutedEventArgs e)
        {
            if (dr != null)
            {
                AnularVenta.MostrarDetalleVenta();
                AnularVenta.ShowDialog();
                DataGridVenta.UnselectAllCells();
            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Por favor seleccione un dato!!! ", "Anular Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        Informes.FrmMostrar_Detalle_Venta Mostrar = new Informes.FrmMostrar_Detalle_Venta();

        private void BtnImprimirInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dr != null)
                {
                    Mostrar.ShowDialog();
                    DataGridVenta.UnselectAllCells();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Por favor seleccione un dato!!! ", "Imprimir Informe", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    DataGridVenta.UnselectAllCells();
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se ha Podido generar el Informe Por: " + ex, "Informe de Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                DataGridVenta.UnselectAllCells();
            }
        }

        private void DataGridVenta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg = sender as DataGrid;

            dr = dg.SelectedItem as DataRowView;

            if (DataGridVenta.Items.Count==0)
            {
                System.Windows.Forms.MessageBox.Show("No Hay Compras para Anular!!! ", "Anular Venta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

            else 
            {
                if (dr != null)
                {
                    AnularVenta.UpdateEventHandler += AnVen_UpdateEventHandler;
                    AnularVenta.txtId_Venta.Text = dr[0].ToString();
                    AnularVenta.txtId_Cliente.Text = dr[1].ToString();
                    AnularVenta.txtClienteNombre.Text = dr[2].ToString();
                    AnularVenta.dtp_FechaVenta.Text = dr[3].ToString();
                    AnularVenta.txtSubTotal.Text = dr[5].ToString();
                    AnularVenta.txtDescuentoVenta.Text = dr[6].ToString();
                    AnularVenta.txtMontoTotal.Text = dr[7].ToString();

                    Mostrar.Id_Venta = Convert.ToInt32(dr[0].ToString());


                }
                
            }
        }


        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            FrmProductos Productos = new FrmProductos();

            Hide();

            Productos.ShowDialog();

            Close();
        }


    }
}
