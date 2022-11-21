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

            #region traer datos
            //var Cell = DataGridGestionProveedores.CurrentCell;
            //GetDataGridCell(Cell);
            //dg = sender as DataGrid;

            //dr = dg.SelectedItem as DataRowView;

            ////AgregarIngresoProveedor.UpdateEventHandler += 
            //AgregarIngresoProveedor.txtId_Proveedor.Text = dr[0].ToString();
            //AgregarIngresoProveedor.txtNombre_Proveedor.Text = dr[2].ToString();

            #endregion

        }


        //Vacio
        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        FrmAgregarIngreso AddProveedor = new FrmAgregarIngreso();


        private void BtnSeleccionarProve_Click(object sender, RoutedEventArgs e)
        {

            #region comentarios btnselecionnar
            //if (dr != null)
            //{
            //    //AddProveedor.txtId_Proveedor.Text = dr[0].ToString();
            //    //AddProveedor.txtNombre_Proveedor.Text = dr[2].ToString();
            //    //Hide();
            //    AgregarIngresoProveedor.Show();
            //}

            //else
            //{
            //    System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Proveedor en la lista proveedores!!", "Seleccionar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            //}

            #endregion

            //Necesario


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

      

        #region datagridcell
        //public DataGridCell GetDataGridCell (DataGridCellInfo cellInfo)
        //{
        //    var cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);
        //    if (cellContent != null)
        //        return (DataGridCell)cellContent.Parent;

        //    return null;
        //}

        #endregion

        private void BtnCancelarProv_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            DataGridGestionProveedores.UnselectAllCells();
        }

        private void Btnaddcli_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregarProveedores proveedores = new FrmAgregarProveedores();
            proveedores.ShowDialog();
        }
    }
}
