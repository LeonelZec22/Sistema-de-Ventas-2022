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
    /// Lógica de interacción para FrmAgregarUsuario.xaml
    /// </summary>
    public partial class FrmAgregarUsuario : Window
    {
        public FrmAgregarUsuario(FrmUsuarios Usuarios)
        {
            InitializeComponent();
        }

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Usuarios Usuarios = new CDo_Usuarios();
        CE_Usuarios Usuario = new CE_Usuarios();

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
        private void AddguardarBtn_Click(object sender, RoutedEventArgs e)
        {
             Guardar();
        }

        private void AddCancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Agregar();
        }

        #endregion

        #region Evento de los textBox
        private void TxtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtApellido.Focus();
                e.Handled = true;
            }

            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de numeros", "Agregar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void TxtApellido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtUsuario.Focus();
                e.Handled = true;
            }

            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de numeros", "Agregar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void TxtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtEmail.Focus();
                e.Handled = true;
            }
        }
       

        private void TxtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtContrasena.Focus();
                e.Handled = true;
            }
        }

        private void TxtContrasena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddguardarBtn.Focus();
                e.Handled = true;
            }
        }

        private void TxtEmail_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        #endregion



        public virtual bool Guardar()
        {
            try
            {
                if (txtNombre.Text == string.Empty || txtApellido.Text == string.Empty || txtUsuario.Text == string.Empty || txtEmail.Text == string.Empty || txtContrasena.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Por favor llene todos los campos de textos!!! ", "Agregar Usuario", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (txtEmail.Text != string.Empty)
                    {
                        bool val2 = regeEmail(txtEmail);
                        if (val2 == false)
                        {
                            System.Windows.Forms.MessageBox.Show("El Email del Cliente tiene formato incorrecto, ejemplo: Ejemplo@gmail.com", "Agregar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        }

                        else
                        {
                            Usuario.Nombre = txtNombre.Text.Trim();
                            Usuario.Apellido = txtApellido.Text.Trim();
                            Usuario.Usuario = txtUsuario.Text.Trim();
                            Usuario.Correo = txtEmail.Text.Trim();
                            Usuario.Password = txtContrasena.Text.Trim();

                            Usuarios.AgregarUsuario(Usuario);

                            System.Windows.Forms.MessageBox.Show("Usuario Agregado exitosamente!!! ", "Agregar Usuario", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                            LimpiarControles();

                            Agregar();
                        }
                    }

                    else
                    {
                        System.Windows.Forms.MessageBox.Show("El Email no puede estar vacio!!! ", "Agregar Usuario", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    }

                  

                    //FrmProductos productos = new FrmProductos();
                    //productos.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Usuario no fue agregado porque: " + ex.Message, "Agregar Usuario", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
        }

        private void LimpiarControles()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtUsuario.Clear();
            txtContrasena.Clear();
            Hide();
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

    }
}
