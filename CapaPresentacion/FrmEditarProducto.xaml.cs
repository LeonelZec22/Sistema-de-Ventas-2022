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
    /// Lógica de interacción para FrmEditarProducto.xaml
    /// </summary>
    public partial class FrmEditarProducto : Window
    {
        public FrmEditarProducto()
        {
            
            InitializeComponent();
        }

        //Creamos una instancia de la pantalla productos
        public FrmEditarProducto(FrmProductos Productos)
        {
            InitializeComponent();
        }

        //Creacion de variables a usar 
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


        //Metodo para Actualizar
        protected void Actualizar()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);
        }

        #region Evento Clic de los botones
        private void EditCancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Actualizar();
        }
        private void EditguardarBtn_Click(object sender, RoutedEventArgs e)
        {
            Editar();
        }

        #endregion

        //Evento para cambiar al siguiente textbox presionando la tecla enter
        #region Evento de los textBox
        private void txtEditNombreProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtEditDescripcion.Focus();
                e.Handled = true;
            }

            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de numeros", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void TxtEditDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtEditCostoUnit.Focus();
                e.Handled = true;
            }
        }

        private void TxtEditCostoUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtEditPrecioVenta.Focus();
                e.Handled = true;
            }

            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key == Key.Space) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de letras y espacios", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
        private void TxtEditPrecioVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EditguardarBtn.Focus();
                e.Handled = true;
            }

            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key == Key.Space) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de letras y espacios", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
        
        private void TxtEditCostoUnit_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtEditCostoUnit.Text.Length > 12)
            {
                System.Windows.Forms.MessageBox.Show("El Costo Unitario no puede ser mayor a 12 caracteres", "Editar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(txtEditCostoUnit.Text) > 0)
                    {
                        Procedimientos.FormatoMoneda(txtEditCostoUnit);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("El Costo Unitario no puede ser cero", "Editar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }
                }

                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("El Costo Unitario no es un numero por favor ingrese solo numeros", "Editar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    txtEditCostoUnit.Clear();
                    //txtAddCostoUnit.Focus();
                }
            }

            //Procedimientos.FormatoMoneda(txtEditCostoUnit);
        }

        private void TxtEditPrecioVenta_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtEditPrecioVenta.Text.Length > 12)
            {
                System.Windows.Forms.MessageBox.Show("El Precio de venta no puede ser mayor a 12 caracteres", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(txtEditPrecioVenta.Text) > 0)
                    {
                        Procedimientos.FormatoMoneda(txtEditPrecioVenta);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("El Precio de venta no puede ser cero", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }
                }

                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("El Precio de Venta no es un numero por favor ingrese solo numeros", "Agregar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    txtEditPrecioVenta.Clear();
                    //txtAddPrecioVenta.Focus();
                }

            }
            //Procedimientos.FormatoMoneda(txtEditPrecioVenta);
        }

        #endregion

        public virtual void Editar()
        {
            try
            {
                if (txtEditCodeProduct.Text == string.Empty || txtEditNombreProduct.Text == string.Empty || txtEditDescripcion.Text == string.Empty || txtEditPrecioVenta.Text == string.Empty || txtEditCostoUnit.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Por favor llene todos los campos de textos!!! ", "Editar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    Producto.Id_Producto = Convert.ToInt32(txtEditIDProduct.Text.Trim());
                    Producto.Codigo = txtEditCodeProduct.Text.Trim();
                    Producto.Nombre = txtEditNombreProduct.Text.Trim();
                    Producto.Descripcion = txtEditDescripcion.Text.Trim();
                    Producto.Precio_Venta = Convert.ToDecimal(txtEditPrecioVenta.Text.Trim());
                    Producto.Costo_Unitario = Convert.ToDecimal(txtEditCostoUnit.Text.Trim());

                    Productos.EditarProducto(Producto);

                    System.Windows.Forms.MessageBox.Show("Producto Editado exitosamente!!! ", "Editar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    this.Hide();

                    Actualizar();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Producto no fue editado porque: " + ex.Message, "Editar Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            this.Hide();
            Actualizar();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
            Actualizar();
        }
    }
}
