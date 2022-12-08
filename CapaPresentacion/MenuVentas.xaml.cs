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

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para MenuVentas.xaml
    /// </summary>
    public partial class MenuVentas : Window
    {
        public MenuVentas()
        {
            InitializeComponent();
        }

        private void BtnReservas_Click(object sender, RoutedEventArgs e)
        {

            FrmReservas reservas = new FrmReservas();
            Hide();
            reservas.ShowDialog();
            Close();
        }

        private void BtnVentaReserva_Click(object sender, RoutedEventArgs e)
        {
            FrmVentas_Servicios ventas_Servicios = new FrmVentas_Servicios();
            Hide();
            ventas_Servicios.ShowDialog();
            Close();
        }

        private void BtnVentaProducto_Click(object sender, RoutedEventArgs e)
        {
            FrmVentas ventas = new FrmVentas();
            Hide();
            ventas.ShowDialog();
            Close();
        }

        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {
            MainWindow FormPrincipal = new MainWindow();
            Hide();
            FormPrincipal.ShowDialog();
            Close();
        }

        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            FrmProductos productos = new FrmProductos();
            Hide();
            productos.ShowDialog();
            Close();
        }

        private void BtnProveedores_Click(object sender, RoutedEventArgs e)
        {
            FrmProveedores Proveedor = new FrmProveedores();
            Hide();
            Proveedor.ShowDialog();
            Close();
        }

        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            FrmClientes Clientes = new FrmClientes();
            Hide();

            Clientes.ShowDialog();
            Close();
        }

        private void BtnServicios_Click(object sender, RoutedEventArgs e)
        {
            FrmServicios Servicios = new FrmServicios();
            Hide();

            Servicios.ShowDialog();

            Close();
        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            FrmInventario inventario = new FrmInventario();
            Hide();

            inventario.ShowDialog();
            Close();
        }
    }
}
