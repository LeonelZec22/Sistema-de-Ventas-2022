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
using CapaNegocio;
using CapaEntidad;
using CapaEntidad.Caches;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string Usuario;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string pUser)
        {
            InitializeComponent();
            Usuario = pUser;
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MostrarUsuario();
        }

        //Creamos un nuevo constructor del objeto 

        public MainWindow(bool doNotMakeInvisible)
        {
            InitializeComponent();
        }

        CDo_Usuarios Usuarios = new CDo_Usuarios();
        private void MostrarUsuario()
        {
            Usuarios.DatosUsuario(Usuario);
            tbUsuario.Text = InformacionUsuario.Nombre + " " + InformacionUsuario.Apellido;
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
            MenuVentas ventas = new MenuVentas();
            Hide();
            ventas.ShowDialog();
            Close();
        }

        private void BtnPaquetes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            FrmUsuarios usuarios = new FrmUsuarios();
            Hide();
            usuarios.ShowDialog();
            Hide();
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
            Application.Current.Shutdown();
        }
    }
}
