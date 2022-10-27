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

            CargarDatos();
        }

        //Creamos un objeto de nuestra clase conexión

        CD_Conexion Con = new CD_Conexion();

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Proveedores Proveedores = new CDo_Proveedores();
        CE_Proveedores Proveedor = new CE_Proveedores();


        private void CargarDatos()
        {
            
            DataGridGestionProveedores.ItemsSource = Procedimientos.CargarDatos("Proveedores").AsDataView();

        }



        private void DataGridGestionProveedores_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Proveedor = e.Column.Header.ToString();

            if (Id_Proveedor == "Id_Proveedor")
            {
                e.Cancel = true;
            }
        }

        //Estamos accediendo a los textbox del otro formulario
        FrmAgregarIngreso AgregarIngresoProveedor = new FrmAgregarIngreso();


        DataGrid dg; //Seleccionar un DataGrid
        DataRowView dr; //Seleccionar una fila de ese DataGrid


        //No funciona 

        public void DataGridGestionProveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            dg = sender as DataGrid;

            dr = dg.SelectedItem as DataRowView;

            //AgregarIngresoProveedor.UpdateEventHandler += 
            AgregarIngresoProveedor.txtId_Proveedor.Text = dr[0].ToString();
            AgregarIngresoProveedor.txtNombre_Proveedor.Text = dr[2].ToString();

        }
        

        //Vacio
        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        FrmAgregarIngreso AddProveedor = new FrmAgregarIngreso();


        private void BtnSeleccionarProve_Click(object sender, RoutedEventArgs e)
        {
            if (dr != null)
            {
                //AddProveedor.txtId_Proveedor.Text = dr[0].ToString();
                //AddProveedor.txtNombre_Proveedor.Text = dr[2].ToString();
                //Hide();
                AgregarIngresoProveedor.Show();
            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Proveedor en la lista proveedores!!", "Seleccionar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
    }
}
