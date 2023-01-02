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
    /// Lógica de interacción para FrmProductos.xaml
    /// </summary>
    public partial class FrmProductos : Window
    {
        public FrmProductos()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();

        }

        //Evento para que se pueda mover la ventana

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.ChangedButton == MouseButton.Left)
            //{
            //    this.DragMove();
            //}
        }

        //Creamos un objeto de nuestra clase conexión

        CD_Conexion Con = new CD_Conexion();

        //Inicialiamos las variables a utilizar
        SqlCommand Cmd;
        SqlDataReader Dr;
        DataTable Dt;
        CDo_Productos Productos = new CDo_Productos();
        CE_Productos Producto = new CE_Productos();
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();


        //Método para cargar o mostrar los datos en la tabla del formulario
        private void CargarDatos()
        {
            Dt = new DataTable("Cargar_Datos");
            Cmd = new SqlCommand("SELECT * FROM Productos", Con.Abrir());

            Cmd.CommandType = CommandType.Text;

            Dr = Cmd.ExecuteReader();

            Dt.Load(Dr);

            Dr.Close();

            Con.Cerrar();

            DataGridProductos.ItemsSource = Dt.DefaultView;

        }

        //Evento para ocultar una columna de un datagrid autogenerico
        private void DataGridProductos_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Producto = e.Column.Header.ToString();

            if (Id_Producto == "Id_Producto")
            {
                e.Cancel = true;
            }

            string Codigo = e.Column.Header.ToString();
            string Nombre = e.Column.Header.ToString();
            string Descripcion = e.Column.Header.ToString();
            string Precio_Venta = e.Column.Header.ToString();
            string Costo_Unitario = e.Column.Header.ToString();
            
            if(Codigo == "Codigo")
            {
                e.Column.Width = 150;
            }

            if (Nombre == "Nombre")
            {
                e.Column.Width = 200;
            }

            if (Descripcion == "Descripcion")
            {
                e.Column.Width = 225;
            }

            if (Precio_Venta == "Precio_Venta")
            {
                e.Column.Width = 100;
            }

            if (Costo_Unitario == "Costo_Unitario")
            {
                e.Column.Width = 110;
            }
        }

        private void AgPro_UpdateEventHandler(object sender, FrmAgregarProducto.UpdateEventArgs args )
        {
            CargarDatos();
        }

        private void EdPro_UpdateEventHandler(object sender, FrmEditarProducto.UpdateEventArgs args)
        {
            CargarDatos();
        }


       
        private void BtnAddProd_Click(object sender, RoutedEventArgs e)
        {
            FrmAgregarProducto AgregarProducto = new FrmAgregarProducto(this);
            AgregarProducto.UpdateEventHandler += AgPro_UpdateEventHandler;
            AgregarProducto.ShowDialog();
            //Close();

        }


        private void BtnEditProd_Click(object sender, RoutedEventArgs e)
        {
            if(dr != null)
            {
                
                EditarProductos.ShowDialog();
                
            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Por favor seleccione un dato!!! ", "Editar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        FrmEditarProducto EditarProductos = new FrmEditarProducto();
        DataGrid dg;
        DataRowView dr;

       private void DataGridProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg = sender as DataGrid;

            dr = dg.SelectedItem as DataRowView;

            if (dr != null )
            {
                EditarProductos.UpdateEventHandler += EdPro_UpdateEventHandler;
                EditarProductos.txtEditIDProduct.Text = dr[0].ToString();
                EditarProductos.txtEditCodeProduct.Text = dr[1].ToString();
                EditarProductos.txtEditNombreProduct.Text = dr[2].ToString();
                EditarProductos.txtEditDescripcion.Text = dr[3].ToString();
                EditarProductos.txtEditPrecioVenta.Text = dr[4].ToString();
                EditarProductos.txtEditCostoUnit.Text = dr[5].ToString();
                EditarProductos.EditguardarBtn.IsEnabled = true;
                btnEditProd.IsEnabled = true;
            }

            
        }

        private void BtnDeleteProd_Click(object sender, RoutedEventArgs e)
        {
            Eliminar();
        }

        //Método Eliminar

        public  void Eliminar()
        {
            if (DataGridProductos.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("No hay registros para eliminar!!! ", "Eliminar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if(DataGridProductos.SelectedItems == null)
                    {
                        return;
                    }
                    else
                    {
                        System.Windows.Forms.DialogResult Resultado = System.Windows.Forms.MessageBox.Show("¿Está seguro que desea Eliminar este registro?", "Eliminar Producto", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);

                        if(Resultado == System.Windows.Forms.DialogResult.Yes)
                        {
                            Producto.Id_Producto = Convert.ToInt32(dr[0].ToString());
                            Productos.EliminarProducto(Producto);
                            System.Windows.Forms.MessageBox.Show("Registro Eliminado correctamente!!! ", "Eliminar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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
                    System.Windows.Forms.MessageBox.Show("Debe seleccionar un registro para eliminar: "+ex, "Eliminar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
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
                if (cboProducto.Text == "Codigo")
                {
                    Producto.Buscar = txtBuscador.Text.Trim();
                    DataGridProductos.ItemsSource = Productos.Buscar_Producto_Codigo(Producto).AsDataView();
                }
                else if (cboProducto.Text == "Nombre")
                {
                    Producto.Buscar = txtBuscador.Text.Trim();
                    DataGridProductos.ItemsSource = Productos.Buscar_Producto_Nombre(Producto).AsDataView();
                }
                else if (cboProducto.Text == "Descripcion")
                {
                    Producto.Buscar = txtBuscador.Text.Trim();
                    DataGridProductos.ItemsSource = Productos.Buscar_Producto_Descripcion(Producto).AsDataView();
                }
            }

            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Producto no fue encontrado por: " + ex.Message, "Buscar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        //Botones del Menú
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
            FrmClientes Clientes = new FrmClientes();
            Hide();

            Clientes.ShowDialog();
            Close();
        }

        private void BtnServicios_Click(object sender, RoutedEventArgs e)
        {
            FrmServicios Servicios = new FrmServicios();
            Hide();

            Servicios.ShowDialog();

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

        private void BtnAyuda_Click(object sender, RoutedEventArgs e)
        {

        }

        

        
    }
}
