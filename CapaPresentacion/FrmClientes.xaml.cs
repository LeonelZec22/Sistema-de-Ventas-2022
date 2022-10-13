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
    /// Lógica de interacción para FrmClientes.xaml
    /// </summary>
    public partial class FrmClientes : Window
    {
        public FrmClientes()
        {
            InitializeComponent();
            CargarDatos();
        }

        //Creamos un objeto de nuestra clase conexión

        CD_Conexion Con = new CD_Conexion();


        CDo_Clientes Clientes = new CDo_Clientes();
        CE_Clientes Cliente = new CE_Clientes();
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();


        //Método para cargar o mostrar los datos en la tabla del formulario
        private void CargarDatos()
        {
            DataGridClientes.ItemsSource = Procedimientos.CargarDatos("Clientes").AsDataView();

        }

        //Evento para ocultar una columna de un datagrid autogenerico
        private void DataGridClientes_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Cliente = e.Column.Header.ToString();

            if (Id_Cliente == "Id_Cliente")
            {
                e.Cancel = true;
            }
        }

        
        //Botones de menu superior
        private void BtnAddCliente_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregarCliente clientes = new FrmAgregarCliente(this);
            clientes.UpdateEventHandler += AgClient_UpdateEventHandler;
            clientes.ShowDialog();
        }


        //Método para editar 
        private void BtnEditCliente_Click(object sender, RoutedEventArgs e)
        {
            if (dr != null)
            {
                EditarCliente.ShowDialog();

            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Por favor seleccione un dato!!! ", "Editar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void BtnDeleteCliente_Click(object sender, RoutedEventArgs e)
        {

        }


        FrmEditarCliente EditarCliente = new FrmEditarCliente();
        DataGrid dg;
        DataRowView dr;



        //Eventos del DataGrid 

        private void AgClient_UpdateEventHandler(object sender, FrmAgregarCliente.UpdateEventArgs args)
        {
            CargarDatos();
        }

        private void EdClient_UpdateEventHandler(object sender, FrmEditarCliente.UpdateEventArgs args)
        {
            CargarDatos();
        }


        private void DataGridClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg = sender as DataGrid;

            dr = dg.SelectedItem as DataRowView;

            if (dr != null)
            {
                EditarCliente.UpdateEventHandler += EdClient_UpdateEventHandler;
                EditarCliente.txtEditIDCliente.Text = dr[0].ToString();
                EditarCliente.txtEditCodeCliente.Text = dr[1].ToString();
                EditarCliente.txtEditNombreCliente.Text = dr[2].ToString();
                EditarCliente.txtEditEmailCliente.Text = dr[3].ToString();
                EditarCliente.txtEditTelefonoCliente.Text = dr[4].ToString();
                EditarCliente.cboEstadoClientes.Text = dr[5].ToString();
                EditarCliente.EditguardarBtn.IsEnabled = true;
                btnEditCliente.IsEnabled = true;
            }
        }


        //Método para eliminar

        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        #region menu lateral
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
    }
}
