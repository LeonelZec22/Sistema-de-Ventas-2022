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
    /// Lógica de interacción para FrmVistaProveedores.xaml
    /// </summary>
    public partial class FrmVistaProveedores : Window
    {
        public FrmVistaProveedores()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

        #region Objetos de las clases a usar 

        CD_Conexion Con = new CD_Conexion();

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Proveedores Proveedores = new CDo_Proveedores();
        CE_Proveedores Proveedor = new CE_Proveedores();

        #endregion

        //Necesario
        private void CargarDatos()
        {
            
            DataGridGestionProveedores.ItemsSource = Procedimientos.CargarDatos("Proveedores").AsDataView();

        }

        private void AgProve_UpdateEventHandler(object sender, FrmAgregarProveedores.UpdateEventArgs args)
        {
            CargarDatos();
        }


        #region Ocultar la columna ID Proveedor

        private void DataGridGestionProveedores_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Proveedor = e.Column.Header.ToString();

            if (Id_Proveedor == "Id_Proveedor")
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region Variables de datagrid 
        //Estamos accediendo a los textbox del otro formulario
        FrmAgregarIngreso AgregarIngresoProveedor = new FrmAgregarIngreso();


        DataGrid dg; //Seleccionar un DataGrid
        DataRowView dr; //Seleccionar una fila de ese DataGrid

        #endregion

        //No funciona 

        public void DataGridGestionProveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            FrmAgregarIngreso ingreso = new FrmAgregarIngreso();

            ingreso.txtId_Proveedor.Clear();


        }


        //Vacio
        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar();
        }

        public virtual void Buscar()
        {
            try
            {
                if (cboGestionProv.Text == "Codigo")
                {
                    Proveedor.Buscar = txtBuscador.Text.Trim();
                    DataGridGestionProveedores.ItemsSource = Proveedores.Buscar_Proveedor_Codigo(Proveedor).AsDataView();
                }
                else if (cboGestionProv.Text == "Nombre")
                {
                    Proveedor.Buscar = txtBuscador.Text.Trim();
                    DataGridGestionProveedores.ItemsSource = Proveedores.Buscar_Proveedor_Nombre(Proveedor).AsDataView();
                }

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Cliente no fue encontrado por: " + ex.Message, "Buscar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        FrmAgregarIngreso AddProveedor = new FrmAgregarIngreso();


        private void BtnSeleccionarProve_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridGestionProveedores.SelectedItems.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Proveedor en la lista proveedores!!", "Seleccionar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }

            else
            {

                DialogResult = true;

                Hide();
            }
        }



        private void BtnCancelarProv_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            DataGridGestionProveedores.UnselectAllCells();
        }

       

        private void CloseApp_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Hide();
            DataGridGestionProveedores.UnselectAllCells();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Hide();
           
        }

        private void BtnNuevoProveedor_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregarProveedores proveedores = new FrmAgregarProveedores(this);
            proveedores.UpdateEventHandler += AgProve_UpdateEventHandler;

            proveedores.ShowDialog();
        }

       
    }
}
