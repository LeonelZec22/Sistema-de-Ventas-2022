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
    /// Lógica de interacción para FrmEditarPaquetes.xaml
    /// </summary>
    public partial class FrmEditarPaquetes : Window
    {
        public FrmEditarPaquetes()
        {
            InitializeComponent();
        }



        CDo_Paquetes Paquetes = new CDo_Paquetes();
        CE_Paquetes Paquete = new CE_Paquetes();
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();

        //Agregamos un delegado

        public delegate void UpdateDelegate(object sender, UpdateEventArgs args);

        //Creamos un evento para actualizar el datagrid
        public event UpdateDelegate UpdateEventHandler;

        public class UpdateEventArgs : EventArgs
        {
            public string Data { get; set; }
        }

        protected void Actualizar()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);
        }

        public virtual bool Editar()
        {
            try
            {
                if (txtAddCodePaquete.Text == string.Empty || txtAddNombrePaquete.Text == string.Empty || txtAddDescripcionPaquete.Text == string.Empty || txtAddPrecioVenta.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Por favor llene los campos de textos requeridos!!! ", "Agregar Paquetes", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    Paquete.Id_Paquetes = Convert.ToInt32(txtEditIDPaquete.Text.Trim());
                    Paquete.Codigo = txtAddCodePaquete.Text.Trim();
                    Paquete.Nombre = txtAddNombrePaquete.Text.Trim();
                    Paquete.Descripcion = txtAddDescripcionPaquete.Text.Trim();
                    Paquete.Cantidad_Vendida = Convert.ToInt32(txtAddCantidad_Vendida.Text.Trim());
                    Paquete.Precio_Venta = Convert.ToDecimal(txtAddPrecioVenta.Text.Trim());


                    Paquetes.EditarPaquete(Paquete);

                    System.Windows.Forms.MessageBox.Show("Paquete Editado exitosamente!!! ", "Agregar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    this.Hide();

                    Actualizar();

                    //FrmProductos productos = new FrmProductos();
                    //productos.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Paquete no fue Editado porque: " + ex.Message, "Editar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
            Actualizar();
        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            Actualizar();
        }

        private void TxtAddNombrePaquete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddDescripcionPaquete.Focus();
                e.Handled = true;
            }

            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de numeros", "Agregar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void TxtAddDescripcionPaquete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddCantidad_Vendida.Focus();
                e.Handled = true;
            }
        }
        private void TxtAddCantidad_Vendida_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddPrecioVenta.Focus();
                e.Handled = true;
            }

            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key == Key.Space) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de letras y espacios", "Agregar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
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
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de letras y espacios", "Agregar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void TxtAddPrecioVenta_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtAddPrecioVenta.Text.Length > 12)
            {
                System.Windows.Forms.MessageBox.Show("El Precio de Venta no puede ser mayor a 12 caracteres", "Agregar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(txtAddPrecioVenta.Text) >= 0)
                    {
                        Procedimientos.FormatoMoneda(txtAddPrecioVenta);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("El Precio de Venta no puede ser mayor o igual a cero", "Agregar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        txtAddPrecioVenta.Clear();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("La Cantidad Vendida no es un numero por favor ingrese solo numeros", "Agregar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    txtAddPrecioVenta.Clear();
                }
            }
            // Procedimientos.FormatoMoneda(txtAddPrecioVenta);
        }

        private void AddguardarBtn_Click(object sender, RoutedEventArgs e)
        {
            Editar();
        }

        private void AddCancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Actualizar();
        }

        private void TxtAddCantidad_Vendida_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtAddCantidad_Vendida.Text.Length > 5)
            {
                System.Windows.Forms.MessageBox.Show("La Cantidad Vendida  no puede ser mayor a 4 caracteres", "Agregar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    if (Convert.ToInt32(txtAddCantidad_Vendida.Text) > 0)
                    {
                        Procedimientos.FormatoEntero(txtAddCantidad_Vendida);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("La Cantidad no puede ser mayor a cero", "Agregar Ingreso de Producto", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        txtAddCantidad_Vendida.Clear();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("La Cantidad Vendida no es un numero por favor ingrese solo numeros", "Agregar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    txtAddCantidad_Vendida.Clear();
                }
            }
        }
    }
}
