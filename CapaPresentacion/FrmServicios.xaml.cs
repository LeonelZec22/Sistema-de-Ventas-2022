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
    /// Lógica de interacción para FrmServicios.xaml
    /// </summary>
    public partial class FrmServicios : Window
    {
        public FrmServicios()
        {
            InitializeComponent();

            CargarDatos();
        }

        //Creamos un objeto de nuestra clase conexión

        CD_Conexion Con = new CD_Conexion();


        CDo_Servicios Servicios = new CDo_Servicios();
        CE_Servicios Servicio = new CE_Servicios();
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();


        //Método para cargar o mostrar los datos en la tabla del formulario
        private void CargarDatos()
        {
            DataGridServicios.ItemsSource = Procedimientos.CargarDatos("Servicios").AsDataView();

        }

        //Evento para ocultar una columna de un datagrid autogenerico
        private void DataGridServicios_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Servicios = e.Column.Header.ToString();

            if (Id_Servicios == "Id_Servicios")
            {
                e.Cancel = true;
            }
        }

        #region Botones superiores


        private void BtnAddServicio_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregarServicio servicios = new FrmAgregarServicio(this);
            servicios.UpdateEventHandler += AgServi_UpdateEventHandler;
            servicios.ShowDialog();
        }

        private void BtnEditServicio_Click(object sender, RoutedEventArgs e)
        {

            if (dr != null)
            {
                EditarServicio.ShowDialog();

            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Por favor seleccione un dato!!! ", "Editar Servicio", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void BtnDeleteServicio_Click(object sender, RoutedEventArgs e)
        {
            Eliminar();
        }

        #endregion


        FrmEditarServicios EditarServicio = new FrmEditarServicios();
        DataGrid dg;
        DataRowView dr;


        //Eventos del DataGrid 

        private void AgServi_UpdateEventHandler(object sender, FrmAgregarServicio.UpdateEventArgs args)
        {
            CargarDatos();
        }

        private void EdServi_UpdateEventHandler(object sender, FrmEditarServicios.UpdateEventArgs args)
        {
            CargarDatos();
        }

        private void DataGridServicios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg = sender as DataGrid;

            dr = dg.SelectedItem as DataRowView;

            if (dr != null)
            {
                EditarServicio.UpdateEventHandler += EdServi_UpdateEventHandler;
                EditarServicio.txtEditIDServicio.Text = dr[0].ToString();
                EditarServicio.txtEditCodeServicio.Text = dr[1].ToString();
                EditarServicio.txtEditNombreServicio.Text = dr[2].ToString();
                EditarServicio.txtEditDescripcionServicio.Text = dr[3].ToString();
                EditarServicio.txtEditPrecioVenta.Text = dr[4].ToString();
                EditarServicio.EditguardarBtn.IsEnabled = true;
                EditarServicio.IsEnabled = true;
            }
        }


        //Método para eliminar

        public void Eliminar()
        {
            if (DataGridServicios.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("No hay registros para eliminar!!! ", "Eliminar Servicios", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (DataGridServicios.SelectedItems == null)
                    {
                        return;
                    }
                    else
                    {
                        System.Windows.Forms.DialogResult Resultado = System.Windows.Forms.MessageBox.Show("¿Está seguro que desea Eliminar este registro?", "Eliminar Servicio", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);

                        if (Resultado == System.Windows.Forms.DialogResult.Yes)
                        {
                            Servicio.Id_Servicio = Convert.ToInt32(dr[0].ToString());
                            Servicios.EliminarServicio(Servicio);
                            System.Windows.Forms.MessageBox.Show("Registro Eliminado correctamente!!! ", "Eliminar Servicio", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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
                    System.Windows.Forms.MessageBox.Show("Debe seleccionar un registro para eliminar!!! ", "Eliminar Servicio", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
            }
        }

        
        //Método Buscar Clientes
        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar();
        }

        public virtual void Buscar()
        {
            try
            {
                if (cboServicios.Text == "Codigo")
                {
                    Servicio.Buscar = txtBuscador.Text.Trim();
                    DataGridServicios.ItemsSource = Servicios.Buscar_Servicio_Codigo(Servicio).AsDataView();
                }
                else if (cboServicios.Text == "Nombre")
                {
                    Servicio.Buscar = txtBuscador.Text.Trim();
                    DataGridServicios.ItemsSource = Servicios.Buscar_Servicios_Nombre(Servicio).AsDataView();
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Servicio no fue encontrado por: " + ex.Message, "Buscar Servicio", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        #region Botones menu izquierda
        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
