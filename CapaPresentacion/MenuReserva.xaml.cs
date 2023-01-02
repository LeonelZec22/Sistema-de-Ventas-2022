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
    /// Lógica de interacción para MenuReserva.xaml
    /// </summary>
    public partial class MenuReserva : Window
    {
        public MenuReserva()
        {
            InitializeComponent();
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

        #region Menu Lateral
        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {
            MainWindow FormPrincipal = new MainWindow();
            Hide();
            FormPrincipal.ShowDialog();
            Close();
        }

 

        private void BtnPaquete2_Click(object sender, RoutedEventArgs e)
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

        private void BtnReservas_Click(object sender, RoutedEventArgs e)
        {
            MenuReserva menuReserva = new MenuReserva();
            Hide();
            menuReserva.ShowDialog();
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

        private void BtnAddReserva_Click(object sender, RoutedEventArgs e)
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

       
    }
}
