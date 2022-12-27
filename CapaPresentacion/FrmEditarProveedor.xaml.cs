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
    /// Lógica de interacción para FrmEditarProveedor.xaml
    /// </summary>
    public partial class FrmEditarProveedor : Window
    {
        public FrmEditarProveedor()
        {
            InitializeComponent();
        }


        //Creamos una instancia de la pantalla proveedores 

        public FrmEditarProveedor( FrmProveedores Proveedores)
        {
            InitializeComponent();
        }

        //Creacion de variables a usar 
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Proveedores Proveedores = new CDo_Proveedores();
        CE_Proveedores Proveedor = new CE_Proveedores();


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


        #region Evento de los textbox
        private void TxtEditNombreProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtEditDireccion.Focus();
                e.Handled = true;
            }


            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de numeros", "Editar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void TxtEditDireccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtEditTelefono.Focus();
                e.Handled = true;
            }
        }

        private void TxtEditTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EditguardarBtn.Focus();
                e.Handled = true;
            }

            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key == Key.Space) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de letras y espacios", "Agregar Venta Reservas", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
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
                if (txtEditCodeProveedor.Text == string.Empty || txtEditNombreProveedor.Text == string.Empty )
                {
                    System.Windows.Forms.MessageBox.Show("Por favor llene todos los campos de textos!!! ", "Editar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (txtEditTelefono.Text != string.Empty)
                    {
                        bool val = Regexcel(txtEditTelefono);
                        if (val == false)
                        {
                            System.Windows.Forms.MessageBox.Show("El teléfono del Proveedor tiene formato incorrecto, ejemplo: 0000-0000.", "Agregar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            Proveedor.Id_Proveedor = Convert.ToInt32(txtEditIDProveedor.Text.Trim());
                            Proveedor.Codigo = txtEditCodeProveedor.Text.Trim();
                            Proveedor.Nombre = txtEditNombreProveedor.Text.Trim();
                            Proveedor.Direccion = txtEditDireccion.Text.Trim();
                            Proveedor.Telefono = txtEditTelefono.Text.Trim();


                            Proveedores.EditarProveedor(Proveedor);

                            System.Windows.Forms.MessageBox.Show("Proveedor Editado exitosamente!!! ", "Editar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                            this.Hide();

                            Actualizar();
                        }
                    }
                    else
                    {
                        Proveedor.Id_Proveedor = Convert.ToInt32(txtEditIDProveedor.Text.Trim());
                        Proveedor.Codigo = txtEditCodeProveedor.Text.Trim();
                        Proveedor.Nombre = txtEditNombreProveedor.Text.Trim();
                        Proveedor.Direccion = txtEditDireccion.Text.Trim();
                        Proveedor.Telefono = txtEditTelefono.Text.Trim();


                        Proveedores.EditarProveedor(Proveedor);

                        System.Windows.Forms.MessageBox.Show("Proveedor Editado exitosamente!!! ", "Editar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                        this.Hide();

                        Actualizar();


                    }


                    
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Proveedor no fue editado porque: " + ex.Message, "Editar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region Expresion Regular
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

        #endregion

    }
}
