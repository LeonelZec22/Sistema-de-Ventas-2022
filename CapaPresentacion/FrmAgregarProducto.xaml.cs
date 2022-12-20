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

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para FrmAgregarProducto.xaml
    /// </summary>
    public partial class FrmAgregarProducto : Window
    {
        public FrmAgregarProducto()
        {
            InitializeComponent();
            GenerarCodigoPro();
        }

        //Creamos una instancia de la pantalla productos
        public FrmAgregarProducto(FrmProductos Productos)
        {
            InitializeComponent();
            GenerarCodigo();
        }

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Productos Productos = new CDo_Productos();
        CE_Productos Producto = new CE_Productos();

        //Agregamos un delegado

        public delegate void UpdateDelegate(object sender, UpdateEventArgs args);

        //Creamos un evento para actualizar el datagrid
        public event UpdateDelegate UpdateEventHandler;

        public class UpdateEventArgs : EventArgs
        {
            public string Data { get; set; }
        }

        protected void Agregar()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);
        }


        #region Evento Clic de los botones
        private void AddCancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Agregar();
        }
        private void AddguardarBtn_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }
        #endregion

        //Evento para cambiar al siguiente textbox presionando la tecla enter
        #region Evento de los textBox
        private void TxtAddNombreProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key== Key.Enter)
            {
                txtAddDescripcion.Focus();
                e.Handled = true;
            }
        }

        private void TxtAddDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddCostoUnit.Focus();
                e.Handled = true;
            }
        }

        private void TxtAddCostoUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddPrecioVenta.Focus();
                e.Handled = true;
            }

            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key == Key.Space) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de letras y espacios", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void TxtAddPrecioVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddguardarBtn.Focus();
                e.Handled = true;
            }

            
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key == Key.Space) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de letras y espacios", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

            
        }

        private void TxtAddCostoUnit_LostFocus(object sender, RoutedEventArgs e)
        {

            //int numero = 0;
            if(txtAddCostoUnit.Text.Length > 12)
            {
                System.Windows.Forms.MessageBox.Show("El Costo Unitario no puede ser mayor a 12 caracteres", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(txtAddCostoUnit.Text) >0)
                    {
                        Procedimientos.FormatoMoneda(txtAddCostoUnit);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("El Costo Unitario no puede ser cero", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }
                }

                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("El Costo Unitario no es un numero por favor ingrese solo numeros", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    txtAddCostoUnit.Clear();
                    //txtAddCostoUnit.Focus();
                }
               
            }
        }

        private void TxtAddPrecioVenta_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtAddPrecioVenta.Text.Length > 12)
            {
                System.Windows.Forms.MessageBox.Show("El Precio de venta no puede ser mayor a 12 caracteres", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(txtAddPrecioVenta.Text) > 0)
                    {
                        Procedimientos.FormatoMoneda(txtAddPrecioVenta);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("El Precio de venta no puede ser cero", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }
                }

                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("El Precio de Venta no es un numero por favor ingrese solo numeros", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    txtAddPrecioVenta.Clear();
                    //txtAddPrecioVenta.Focus();
                }

            }

        }

        #endregion


        //Método para guardar productos en la base de datos mediante el formulario
        public virtual bool Guardar()
        {
            try
            {
                if (txtAddCodeProduct.Text == string.Empty || txtAddNombreProduct.Text == string.Empty || txtAddDescripcion.Text == string.Empty || txtAddPrecioVenta.Text == string.Empty || txtAddCostoUnit.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Por favor llene todos los campos de textos!!! ", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    Producto.Codigo = txtAddCodeProduct.Text.Trim();
                    Producto.Nombre = txtAddNombreProduct.Text.Trim();
                    Producto.Descripcion = txtAddDescripcion.Text.Trim();
                    Producto.Precio_Venta = Convert.ToDecimal(txtAddPrecioVenta.Text.Trim());
                    Producto.Costo_Unitario = Convert.ToDecimal(txtAddCostoUnit.Text.Trim());

                    Productos.AgregarProducto(Producto);

                    System.Windows.Forms.MessageBox.Show("Producto Agregado exitosamente!!! ", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    LimpiarControles();

                    GenerarCodigoPro();

                    txtAddNombreProduct.Focus();

                    Agregar();

                    //FrmProductos productos = new FrmProductos();
                    //productos.ShowDialog();
                    
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Producto no fue agregado porque: "+ex.Message, "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
        }

        //Método para limpiar los textbox del formulario
        private void LimpiarControles()
        {
            txtAddCodeProduct.Clear();
            txtAddNombreProduct.Clear();
            txtAddDescripcion.Clear();
            txtAddPrecioVenta.Clear();
            txtAddCostoUnit.Clear();
            Close();
        }

        private void GenerarCodigo()
        {
            txtAddCodeProduct.Text = "PROD" + GenerarCodigoPro();
        }

        CD_Conexion Con = new CD_Conexion();


        SqlCommand Cmd;
        SqlDataReader Dr;
        //DataTable Dt;

        //Método para generar el código del producto
        private string GenerarCodigoPro()
        {
            string Codigo = string.Empty;
            int Total = 0;

            Cmd = new SqlCommand("SELECT COUNT(*) as TotalRegistros FROM Productos" , Con.Abrir());

            Cmd.CommandType = CommandType.Text;

            Dr = Cmd.ExecuteReader();

            if (Dr.Read())
            {
                Total = Convert.ToInt32(Dr["TotalRegistros"]) + 1;
            }

            Dr.Close();

            if (Total < 10)
            {
                Codigo = "0000000" + Total;
            }

            else if (Total < 100)
            {
                Codigo = "000000" + Total;
            }

            else if (Total < 1000)
            {
                Codigo = "00000" + Total;
            }

            else if (Total < 10000)
            {
                Codigo = "0000" + Total;
            }

            else if (Total < 100000)
            {
                Codigo = "000" + Total;
            }

            else if (Total < 1000000)
            {
                Codigo = "00" + Total;
            }

            else if (Total < 10000000)
            {
                Codigo = "0" + Total;
            }

            Con.Cerrar();

            return Codigo;
        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            Agregar();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
            Agregar();
        }

        private void TxtAddCostoUnit_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("El precio solo se puede ingresar por medio del teclado", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
        }
    }
}
