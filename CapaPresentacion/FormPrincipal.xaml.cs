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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        //Creamos un nuevo constructor del objeto 

        public MainWindow(bool doNotMakeInvisible)
        {
            InitializeComponent();
        }

        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            FrmProductos frmProductos = new FrmProductos();
            frmProductos.ShowDialog();
            Close();
        }


        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            FrmInventario frmInventario = new FrmInventario();
            Hide();
            frmInventario.ShowDialog();
            Close();
        }

        private void BtnProveedores_Click(object sender, RoutedEventArgs e)
        {
            FrmProveedores Proveedor = new FrmProveedores();
            Hide();
            Proveedor.ShowDialog();
            Close();
        }

        private void BtnConfiguracion_Click(object sender, RoutedEventArgs e)
        {
            Configuraciones configuraciones = new Configuraciones();
            configuraciones.ShowDialog();
            Close();
        }

        private void BtnReservas_Click(object sender, RoutedEventArgs e)
        {
            FrmReservas reservas = new FrmReservas();
            Hide();
            reservas.ShowDialog();
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


        private void BtnVentas_Click(object sender, RoutedEventArgs e)
        {
            FrmVentas ventas = new FrmVentas();
            Hide();
            ventas.ShowDialog();
            Close();
        }

        private void BtnPaquetes_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
