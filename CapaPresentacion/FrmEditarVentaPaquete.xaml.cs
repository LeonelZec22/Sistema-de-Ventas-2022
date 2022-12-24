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
    /// Lógica de interacción para FrmEditarVentaPaquete.xaml
    /// </summary>
    public partial class FrmEditarVentaPaquete : Window
    {
        public FrmEditarVentaPaquete()
        {
            InitializeComponent();
        }

        public FrmEditarVentaPaquete(FrmVenta_Paquete Venta)
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtCantidadUsar.Focus();
        }

        #region Instancia de objetos a usar

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Venta_Paquetes Ventas = new CDo_Venta_Paquetes();
        CE_Ventas_Paquetes Venta = new CE_Ventas_Paquetes();

        #endregion

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

        public virtual void Editar()
        {
            try
            {
                if (txtId_Cliente.Text == string.Empty || txtId_Paquetes.Text == string.Empty || txtNombre_Paquete.Text == string.Empty || txtClienteNombre.Text == string.Empty || txtCantidadVendida.Text == string.Empty || txtPrecio_Venta.Text == string.Empty || txtCantidadUsar.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Debe de completar todos los campos del formulario!!", "Editar Detalle Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (Convert.ToInt32(txtCantidadVendida.Text) > Convert.ToInt32(txtCantidadUsar.Text))
                    {
                        Venta.Id_Cliente = Convert.ToInt32(txtId_Cliente.Text);
                        Venta.Id_Paquetes = Convert.ToInt32(txtId_Paquetes.Text);
                        Venta.Cliente = Convert.ToString(txtClienteNombre.Text);
                        Venta.Paquete = Convert.ToString(txtNombre_Paquete.Text);
                        Venta.Fecha_Venta = Convert.ToDateTime(dtp_FechaVenta.Text);
                        Venta.Cantidad_Vendida = Convert.ToInt32(txtCantidadVendida.Text);
                        Venta.Cantidad_Usada = Convert.ToInt32(txtCantidadUsar.Text);
                        Venta.Estado = "Emitido";
                        Venta.Precio_Venta = Convert.ToDecimal(txtPrecio_Venta.Text);
                        Venta.Id_Venta_Paquetes = Convert.ToInt32(txtId_Venta_Paquetes.Text);

                        Ventas.EditarVentaPaquetes(Venta);

                        System.Windows.Forms.MessageBox.Show("Venta de Paquete Editada correctamente!!", "Editar Venta Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        Actualizar();
                        LimpiarDetalle();
                        Hide();
                        
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("La Cantidad de citas a usar no puede ser mayor que la cantidad de citas compradas", "Agregar Detalle Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("La venta de paquetes no fue agregado por: " + ex, "Agregar Venta de Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void BtnGuardarVenta_Click(object sender, RoutedEventArgs e)
        {
            Editar();
        }

        private void LimpiarDetalle()
        {
            txtId_Venta_Paquetes.Text = string.Empty;
            txtId_Cliente.Text = string.Empty;
            txtClienteNombre.Text = string.Empty;
            txtId_Paquetes.Text = string.Empty;
            txtNombre_Paquete.Text = string.Empty;
            txtCantidadVendida.Text = string.Empty;
            txtCantidadUsar.Text = Convert.ToString(0);
            txtPrecio_Venta.Text = string.Empty;

            
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            LimpiarDetalle();
            this.Hide();
            Actualizar();
        }

    

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LimpiarDetalle();
            this.Hide();
            Actualizar();
        }


        private void BtnCancelarVenta_Click(object sender, RoutedEventArgs e)
        {
            LimpiarDetalle();
            this.Hide();
            Actualizar();
        }

        private void BtnSumar_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(txtCantidadUsar.Text) >= Convert.ToInt32(txtCantidadVendida.Text) )
            {
                System.Windows.Forms.MessageBox.Show("La Cantidad de citas a usar no puede ser mayor que la cantidad de citas compradas", "Agregar Detalle Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            else
            {
                int sumar = Convert.ToInt32(txtCantidadUsar.Text) + 1;
                txtCantidadUsar.Text = Convert.ToString(sumar);

            }
        }

        private void BtnRestar_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(txtCantidadUsar.Text) <= 0)
            {
                System.Windows.Forms.MessageBox.Show("No se puede restar si la cantidad de cita es igual a cero", "Editar Venta de Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                int restar = Convert.ToInt32(txtCantidadUsar.Text) - 1;
                txtCantidadUsar.Text = Convert.ToString(restar);
            }
        }
    }
}
