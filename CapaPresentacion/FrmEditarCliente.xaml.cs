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
    /// Lógica de interacción para FrmEditarCliente.xaml
    /// </summary>
    public partial class FrmEditarCliente : Window
    {
        public FrmEditarCliente()
        {
            InitializeComponent();
        }

        public FrmEditarCliente(FrmClientes Clientes)
        {
            InitializeComponent();
        }

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Clientes Clientes = new CDo_Clientes();
        CE_Clientes Cliente = new CE_Clientes();

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

        #region Evento de los TextBox
        private void TxtEditNombreCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtEditEmailCliente.Focus();
                e.Handled = true;
            }
        }

        private void TxtEditTelefonoCliente_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                cboEstadoClientes.Focus();
                e.Handled = true;
            }
        }

        private void TxtEditEmailCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtEditTelefonoCliente.Focus();
                e.Handled = true;
            }
        }

        #endregion

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
        public virtual void Editar()
        {
            try
            {
                if (txtEditCodeCliente.Text == string.Empty || txtEditNombreCliente.Text == string.Empty || cboEstadoClientes.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Por favor llene todos los campos de textos!!! ", "Editar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    Cliente.Id_Cliente = Convert.ToInt32(txtEditIDCliente.Text.Trim());
                    Cliente.Codigo = txtEditCodeCliente.Text.Trim();
                    Cliente.Nombre = txtEditNombreCliente.Text.Trim();
                    Cliente.Telefono = txtEditTelefonoCliente.Text.Trim();
                    Cliente.Email = txtEditEmailCliente.Text.Trim();
                    Cliente.Estado = cboEstadoClientes.Text.Trim();


                    Clientes.EditarCliente(Cliente);

                    System.Windows.Forms.MessageBox.Show("Cliente Editado exitosamente!!! ", "Editar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    this.Hide();

                    Actualizar();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Cliente no fue editado porque: " + ex.Message, "Editar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

        }
    }
}
