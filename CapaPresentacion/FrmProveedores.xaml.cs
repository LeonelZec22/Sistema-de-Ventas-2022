using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CapaNegocio;
using CapaEntidad;
using CapaDatos;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para FrmProveedores.xaml
    /// </summary>
    public partial class FrmProveedores : Window
    {
        public FrmProveedores()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();

        }
        //Creamos un objeto de nuestra clase conexión

        CD_Conexion Con = new CD_Conexion();

        //Inicialiamos las variables a utilizar
        //SqlCommand Cmd;
        //SqlDataReader Dr;
        //DataTable Dt;

        CDo_Proveedores Proveedores = new CDo_Proveedores();
        CE_Proveedores Proveedor = new CE_Proveedores();
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();

        //Método para cargar o mostrar los datos en la tabla del formulario
        private void CargarDatos()
        {
            

            DataGridProveedores.ItemsSource = Procedimientos.CargarDatos("Proveedores").AsDataView();

        }

        #region Evento para modificar las columnas autogeneradas del DataGrid
        //Evento para ocultar una columna de un datagrid autogenerico
        private void DataGridProveedores_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Proveedor = e.Column.Header.ToString();

            if (Id_Proveedor == "Id_Proveedor")
            {
                e.Cancel = true;
            }
        }

        #endregion


        #region Botones del encabezado
        private void BtnAddProve_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregarProveedores proveedores = new FrmAgregarProveedores(this);
            proveedores.UpdateEventHandler += AgProve_UpdateEventHandler;
          
            proveedores.ShowDialog();
        }

        private void BtnAddIngre_Click(object sender, RoutedEventArgs e)
        {

            FrmCompraDeProducto compraDeProducto = new FrmCompraDeProducto();
            Hide();
            compraDeProducto.ShowDialog();
            Close();
        }
        private void BtnEditProve_Click(object sender, RoutedEventArgs e)
        {
            if (dr != null)
            {
                EditarProveedor.ShowDialog();

            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Por favor seleccione un dato!!! ", "Editar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void BtnDeleteProve_Click(object sender, RoutedEventArgs e)
        {
            Eliminar();
        }

        //Método para eliminar 

        public void Eliminar()
        {
            if (DataGridProveedores.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("No hay registros para eliminar!!! ", "Eliminar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (DataGridProveedores.SelectedItems == null)
                    {
                        return;
                    }
                    else
                    {
                        System.Windows.Forms.DialogResult Resultado = System.Windows.Forms.MessageBox.Show("¿Está seguro que desea Eliminar este registro?", "Eliminar Proveedor", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);

                        if (Resultado == System.Windows.Forms.DialogResult.Yes)
                        {
                            Proveedor.Id_Proveedor = Convert.ToInt32(dr[0].ToString());
                            Proveedores.EliminarProveedor(Proveedor);
                            System.Windows.Forms.MessageBox.Show("Registro Eliminado correctamente!!! ", "Eliminar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            CargarDatos();
                        }
                        else if (Resultado == System.Windows.Forms.DialogResult.No)
                        {
                            CargarDatos();
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Debe seleccionar un registro para eliminar!!! ", "Eliminar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
            }
        }
        #endregion


        FrmEditarProveedor EditarProveedor = new FrmEditarProveedor();
        DataGrid dg;
        DataRowView dr;

        //Invocacion de un evento creado para recargar este formulario o pantalla
        private void AgProve_UpdateEventHandler(object sender, FrmAgregarProveedores.UpdateEventArgs args)
        {
            CargarDatos();
        }

        private void EdProve_UpdateEventHandler(object sender, FrmEditarProveedor.UpdateEventArgs args)
        {
            CargarDatos();
        }

        //Evento para capturar una fila seleccionada del DataGrid
        private void DataGridProveedores_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            dg = sender as DataGrid;

            dr = dg.SelectedItem as DataRowView;

            if (dr != null)
            {
                EditarProveedor.UpdateEventHandler += EdProve_UpdateEventHandler;
                EditarProveedor.txtEditIDProveedor.Text = dr[0].ToString();
                EditarProveedor.txtEditCodeProveedor.Text = dr[1].ToString();
                EditarProveedor.txtEditNombreProveedor.Text = dr[2].ToString();
                EditarProveedor.txtEditDireccion.Text = dr[3].ToString();
                EditarProveedor.txtEditTelefono.Text = dr[4].ToString();
                EditarProveedor.EditguardarBtn.IsEnabled = true;
                btnEditProve.IsEnabled = true;
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
                if (cboProveedores.Text == "Codigo")
                {
                    Proveedor.Buscar = txtBuscador.Text.Trim();
                    DataGridProveedores.ItemsSource = Proveedores.Buscar_Proveedor_Codigo(Proveedor).AsDataView();
                }
                else if (cboProveedores.Text == "Nombre")
                {
                    Proveedor.Buscar = txtBuscador.Text.Trim();
                    DataGridProveedores.ItemsSource = Proveedores.Buscar_Proveedor_Nombre(Proveedor).AsDataView();
                }
                
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Proveedor no fue encontrado por: " + ex.Message, "Buscar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region  Formas de Cerrar la App
        private void CloseApp_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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

        private void MinimizeApp_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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

        #region Botones del menu lateral

        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Inicio = new MainWindow();
            Hide();
            Inicio.ShowDialog();
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
