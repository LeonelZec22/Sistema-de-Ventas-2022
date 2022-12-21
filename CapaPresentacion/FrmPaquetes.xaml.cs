using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
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
    /// Lógica de interacción para FrmPaquetes.xaml
    /// </summary>
    public partial class FrmPaquetes : Window
    {
        public FrmPaquetes()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

         //Creamos un objeto de nuestra clase conexión

        CD_Conexion Con = new CD_Conexion();
        CDo_Paquetes Paquetes = new CDo_Paquetes();
        CE_Paquetes Paquete = new CE_Paquetes();
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();

        private void AgPaq_UpdateEventHandler(object sender, FrmAgregarPaquete.UpdateEventArgs args)
        {
            CargarDatos();
        }

        private void EdPaq_UpdateEventHandler(object sender, FrmEditarPaquetes.UpdateEventArgs args)
        {
            CargarDatos();
        }

        //Método para cargar o mostrar los datos en la tabla del formulario
        private void CargarDatos()
        {
            DataGridPaquetes.ItemsSource = Procedimientos.CargarDatos("Paquetes").AsDataView();

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();

            
        }

        #region Menu Lateral
        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Principal = new MainWindow();
            Hide();
            Principal.ShowDialog();
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

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            FrmInventario inventario = new FrmInventario();
            Hide();
            inventario.ShowDialog();
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

        private void BtnAddPaquetes_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregarPaquete paquetes = new FrmAgregarPaquete(this);
            paquetes.UpdateEventHandler += AgPaq_UpdateEventHandler;
            paquetes.ShowDialog();
        }

        private void BtnEditPaquete_Click(object sender, RoutedEventArgs e)
        {
            if (dr != null)
            {
                EditarPaquete.ShowDialog();

            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Por favor seleccione un dato!!! ", "Editar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void BtnDeletePaquete_Click(object sender, RoutedEventArgs e)
        {
            Eliminar();
        }

        FrmEditarPaquetes EditarPaquete= new FrmEditarPaquetes();
        DataGrid dg;
        DataRowView dr;

        private void DataGridPaquetes_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Paquetes = e.Column.Header.ToString();

            if (Id_Paquetes == "Id_Paquetes")
            {
                e.Cancel = true;
            }
        }

        private void DataGridPaquetes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg = sender as DataGrid;

            dr = dg.SelectedItem as DataRowView;

            if (dr != null)
            {
                EditarPaquete.UpdateEventHandler += EdPaq_UpdateEventHandler;
                EditarPaquete.txtEditIDPaquete.Text = dr[0].ToString();
                EditarPaquete.txtAddCodePaquete.Text = dr[1].ToString();
                EditarPaquete.txtAddNombrePaquete.Text = dr[2].ToString();
                EditarPaquete.txtAddDescripcionPaquete.Text = dr[3].ToString();
                EditarPaquete.txtAddCantidad_Vendida.Text = dr[4].ToString();
                EditarPaquete.txtAddPrecioVenta.Text = dr[5].ToString();
                EditarPaquete.AddguardarBtn.IsEnabled = true;
                EditarPaquete.IsEnabled = true;
            }
        }

        public void Eliminar()
        {
            if (DataGridPaquetes.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("No hay registros para eliminar!!! ", "Eliminar Paquetes", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (DataGridPaquetes.SelectedItems == null)
                    {
                        return;
                    }
                    else
                    {
                        System.Windows.Forms.DialogResult Resultado = System.Windows.Forms.MessageBox.Show("¿Está seguro que desea Eliminar este registro?", "Eliminar Paquete", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);

                        if (Resultado == System.Windows.Forms.DialogResult.Yes)
                        {
                            Paquete.Id_Paquetes = Convert.ToInt32(dr[0].ToString());
                            Paquetes.EliminarPaquete(Paquete);
                            System.Windows.Forms.MessageBox.Show("Registro Eliminado correctamente!!! ", "Eliminar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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
                    System.Windows.Forms.MessageBox.Show("Debe seleccionar un registro para eliminar!!! ", "Eliminar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
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
                if (cboPaquete.Text == "Nombre")
                {
                    Paquete.Buscar = txtBuscador.Text.Trim();
                    DataGridPaquetes.ItemsSource = Paquetes.Buscar_Paquete_Nombre(Paquete).AsDataView();
                }
                else if (cboPaquete.Text == "Descripcion")
                {
                    Paquete.Buscar = txtBuscador.Text.Trim();
                    DataGridPaquetes.ItemsSource = Paquetes.Buscar_Paquete_Descripcion(Paquete).AsDataView();
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Servicio no fue encontrado por: " + ex.Message, "Buscar Servicio", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

    }
}
