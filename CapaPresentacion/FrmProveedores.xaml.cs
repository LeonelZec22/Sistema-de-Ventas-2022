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
            //Dt = new DataTable("Cargar_Datos");
            //Cmd = new SqlCommand("SELECT * FROM Proveedores", Con.Abrir());

            //Cmd.CommandType = CommandType.Text;

            //Dr = Cmd.ExecuteReader();

            //Dt.Load(Dr);

            //Dr.Close();

            //Con.Cerrar();

            //DataGridProveedores.ItemsSource = Dt.DefaultView;

            DataGridProveedores.ItemsSource = Procedimientos.CargarDatos("Proveedores").AsDataView();

        }

        #region Eventos del DataGrid
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
        #endregion


        FrmEditarProveedor EditarProveedor = new FrmEditarProveedor();
        DataGrid dg;
        DataRowView dr;

        #region Botones del menu lateral

        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Inicio = new MainWindow();
            Hide();
            Inicio.ShowDialog();
            Close();
        }

        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {
            FrmProductos Producto = new FrmProductos();
            Hide();
            Producto.ShowDialog();
            Close();
        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            FrmInventario Inventario = new FrmInventario();
            Hide();
            Inventario.ShowDialog();
            Close();
        }

        #endregion



        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar();
        }

        private void AgProve_UpdateEventHandler(object sender, FrmAgregarProveedores.UpdateEventArgs args)
        {
            CargarDatos();
        }

        private void EdProve_UpdateEventHandler(object sender, FrmEditarProveedor.UpdateEventArgs args)
        {
            CargarDatos();
        }


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

        private void BtnAddIngre_Click(object sender, RoutedEventArgs e)
        {

            FrmCompraDeProducto compraDeProducto = new FrmCompraDeProducto();
            Hide();
            compraDeProducto.ShowDialog();
            Close();
        }
    }
}
