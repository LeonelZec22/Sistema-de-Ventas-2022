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
    /// Lógica de interacción para FrmEditarServicios.xaml
    /// </summary>
    public partial class FrmEditarServicios : Window
    {
        public FrmEditarServicios()
        {
            InitializeComponent();
        }


        public FrmEditarServicios(FrmServicios Servicios)
        {
            InitializeComponent();
        }

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Servicios Servicios = new CDo_Servicios();
        CE_Servicios Servicio = new CE_Servicios();


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

        public virtual void Editar()
        {
            try
            {
                if (txtEditCodeServicio.Text == string.Empty || txtEditNombreServicio.Text == string.Empty || txtEditDescripcionServicio.Text == string.Empty || txtEditPrecioVenta.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Por favor llene los campos de textos requeridos!!! ", "Editar Servicio", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    Servicio.Id_Servicio = Convert.ToInt32(txtEditIDServicio.Text.Trim());
                    Servicio.Codigo = txtEditCodeServicio.Text.Trim();
                    Servicio.Nombre = txtEditNombreServicio.Text.Trim();
                    Servicio.Descripcion = txtEditDescripcionServicio.Text.Trim();
                    Servicio.Precio_Venta = txtEditPrecioVenta.Text.Trim();


                    Servicios.EditarServicio(Servicio);

                    System.Windows.Forms.MessageBox.Show("Servicio Editador exitosamente!!! ", "Editar Servicio", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    this.Hide();

                    Actualizar();

                    //FrmProductos productos = new FrmProductos();
                    //productos.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Servicio no fue agregado porque: " + ex.Message, "Agregar Servicio", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
          
        }

        #region Botones
        private void EditguardarBtn_Click(object sender, RoutedEventArgs e)
        {
            Editar();
        }

        private void EditCancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Actualizar();
        }
        #endregion


        #region TextBox
        private void TxtEditNombreServicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtEditDescripcionServicio.Focus();
                e.Handled = true;
            }
        }

        private void TxtEditDescripcionServicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtEditPrecioVenta.Focus();
                e.Handled = true;
            }
        }

        private void TxtEditPrecioVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EditguardarBtn.Focus();
                e.Handled = true;
            }
        }

        private void TxtEditPrecioVenta_LostFocus(object sender, RoutedEventArgs e)
        {
            Procedimientos.FormatoMoneda(txtEditPrecioVenta);
        }

        #endregion

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
