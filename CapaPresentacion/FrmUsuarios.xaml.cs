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
using System.Text.RegularExpressions;
using System.Windows.Shapes;
using CapaNegocio;
using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para FrmUsuarios.xaml
    /// </summary>
    public partial class FrmUsuarios : Window
    {
        public FrmUsuarios()
        {
            InitializeComponent();

        }

        //Instancia de la clases
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Usuarios Usuarios = new CDo_Usuarios();
        CE_Usuarios Usuario = new CE_Usuarios();

        private void MostrarUsuarios()
        {
            DataGridUsuarios.ItemsSource = Procedimientos.CargarDatos("Usuarios").AsDataView();
            DataGridUsuarios.UnselectAllCells();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MostrarUsuarios();
        }

        #region Botones del encabezado
        private void BtnNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregarUsuario agregarUsuario = new FrmAgregarUsuario(this);
            agregarUsuario.UpdateEventHandler += AgUs_UpdateEventHandler;
            agregarUsuario.ShowDialog();
        }
        private void BtnEditarUsuario_Click(object sender, RoutedEventArgs e)
        {
            
            if (dr != null)
            {
                EditarUsuario.ShowDialog();

            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Por favor seleccione un dato!!! ", "Editar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void BtnEliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            Eliminar();
        }

        public void Eliminar()
        {
            if (DataGridUsuarios.Items.Count == 1)
            {
                System.Windows.Forms.MessageBox.Show("No se puede eliminar el unico usuario registrado!!! ", "Eliminar Clientes", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                DataGridUsuarios.UnselectAllCells();
            }
            else
            {
                try
                {
                    if (DataGridUsuarios.SelectedItems == null)
                    {
                        return;
                    }
                    else
                    {
                        System.Windows.Forms.DialogResult Resultado = System.Windows.Forms.MessageBox.Show("¿Está seguro que desea Eliminar este registro?", "Eliminar Cliente", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);

                        if (Resultado == System.Windows.Forms.DialogResult.Yes)
                        {
                            Usuario.Id_Usuario = Convert.ToInt32(dr[0].ToString());
                            Usuarios.EliminarUsuario(Usuario);
                            MostrarUsuarios();
                        }
                        else if (Resultado == System.Windows.Forms.DialogResult.No)
                        {
                            MostrarUsuarios();
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Debe seleccionar un registro para eliminar!!! ", "Eliminar Usuario", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
            }
        }

        FrmEditarUsuario EditarUsuario = new FrmEditarUsuario();

        DataGrid dg;
        DataRowView dr;

        #endregion

        #region Evento para modificar las columnas autogeneradas del DataGrid
        private void DataGridUsuarios_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Usuario = e.Column.Header.ToString();

            if (Id_Usuario == "Id_Usuario")
            {
                e.Cancel = true;
            }
            

            string Nombre = e.Column.Header.ToString();

            if (Nombre == "Nombre")
            {
                e.Column.Width = 200;
            }
            

            string Apellido = e.Column.Header.ToString();

            if (Apellido == "Apellido")
            {
                e.Column.Width = 200;
            }

            string Usuario = e.Column.Header.ToString();

            if (Usuario == "Usuario")
            {
                e.Column.Width = 150;
            }

            string Correo = e.Column.Header.ToString();

            if (Correo == "Correo")
            {
                e.Column.Width = 275;
            }

            string Password = e.Column.Header.ToString();

            if (Password == "Password")
            {
                e.Column.Width = 150;
            }



        }

        #endregion

        //Invocacion de un evento creado para recargar este formulario o pantalla
        private void AgUs_UpdateEventHandler(object sender, FrmAgregarUsuario.UpdateEventArgs args)
        {
            MostrarUsuarios();
        }
        private void EdUs_UpdateEventHandler(object sender, FrmEditarUsuario.UpdateEventArgs args)
        {
            MostrarUsuarios();
        }

        //Evento para capturar una fila seleccionada del DataGrid
        private void DataGridUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg = sender as DataGrid;

            dr = dg.SelectedItem as DataRowView;

            if (dr != null)
            {
                EditarUsuario.UpdateEventHandler += EdUs_UpdateEventHandler;
                EditarUsuario.txtIdUsuario.Text = dr[0].ToString();
                EditarUsuario.txtNombre.Text = dr[1].ToString();
                EditarUsuario.txtApellido.Text = dr[2].ToString();
                EditarUsuario.txtUsuario.Text = dr[3].ToString();
                EditarUsuario.txtEmail.Text = dr[4].ToString();
                EditarUsuario.txtContrasena.Text = dr[5].ToString();
                EditarUsuario.EditguardarBtn.IsEnabled = true;
            }
        }

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
