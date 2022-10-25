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
using System.Windows.Forms;


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

        private void DataGridGestionProveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridGestionProveedores.Items.Count == 0)
            {
                return;
            }

            else
            {
                DialogResult= Convert.ToBoolean("Ok");
                Close();
            }
        }








        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


    }
}
