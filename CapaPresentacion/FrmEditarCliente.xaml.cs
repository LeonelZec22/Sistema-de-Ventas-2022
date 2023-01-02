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
using System.Text.RegularExpressions;

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
                    if (txtEditEmailCliente.Text != string.Empty)
                    {
                        bool val2 = regeEmail(txtEditEmailCliente);
                        if (val2 == false)
                        {
                            System.Windows.Forms.MessageBox.Show("El Email del Cliente tiene formato incorrecto, ejemplo: Ejemplo@gmail.com", "Agregar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        }

                        else
                        {
                            if (txtEditTelefonoCliente.Text != string.Empty)
                            {
                                bool val = Regexcel(txtEditTelefonoCliente);
                                if (val == false)
                                {
                                    System.Windows.Forms.MessageBox.Show("El teléfono del Cliente tiene formato incorrecto,         ejemplo: 0000-0000.", "Agregar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
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
                    }

                    else
                    {
                        if (txtEditTelefonoCliente.Text != string.Empty)
                        {
                            //El campo de telefono puede estar vacio o ingresado con el formato correcto
                            bool val = Regexcel(txtEditTelefonoCliente);
                            if (val == false)
                            {
                                System.Windows.Forms.MessageBox.Show("El teléfono del Cliente tiene formato incorrecto,         ejemplo: 0000-0000.", "Agregar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
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
                  
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Cliente no fue editado porque: " + ex.Message, "Editar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region Evento de los TextBox
        private void TxtEditNombreCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtEditEmailCliente.Focus();
                e.Handled = true;
            }
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de numeros", "Agregar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
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
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key == Key.Space) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de letras y espacios", "Agregar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        private void TxtEditEmailCliente_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void TxtEditTelefonoCliente_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        #region Expresiones Regulares
        public bool Regexcel(TextBox txt)
        {
            string re = "^([0-9]{4})-([0-9]{4})$";
            bool valido;
            Regex regex = new Regex(re);
            if (regex.IsMatch(txt.Text))
            {
                valido = true;
                return valido;
            }
            else
            {
                valido = false;
                return valido;
            }
        }
        public bool regeEmail(TextBox Variablee)
        {
            string ema = @"^\S{1,}@\S{2,}\.\S{2,}$";
            bool validoo;
            Regex regex = new Regex(ema);

            if (regex.IsMatch(Variablee.Text))
            {
                validoo = true;
                return validoo;
            }

            else
            {
                validoo = false;
                return validoo;
            }
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
