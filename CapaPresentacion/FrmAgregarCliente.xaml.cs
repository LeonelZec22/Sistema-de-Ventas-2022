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
    /// Lógica de interacción para FrmAgregarCliente.xaml
    /// </summary>
    public partial class FrmAgregarCliente : Window
    {
        public FrmAgregarCliente()
        {
            InitializeComponent();
        }

        public FrmAgregarCliente(FrmClientes Clientes)
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GenerarCodigo();
        }

        //Instancias de objetos a utilizar

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Clientes Clientes = new CDo_Clientes();
        CE_Clientes Cliente = new CE_Clientes();

        #region Evento para recargar el formulario de Clientes
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

        #endregion

        #region Generar Código
        private void GenerarCodigo()
        {
            try
            {

                txtAddCodeCliente.Text = "CLIE" + GenerarCodigoCliente();
            }

            catch (Exception ex)
            {
                MessageBox.Show("El codigo del cliente no se pudo generar por:" + ex.Message, "Agregar Cliente", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        CD_Conexion Con = new CD_Conexion();


        SqlCommand Cmd;
        SqlDataReader Dr;

        private string GenerarCodigoCliente()
        {
            string Codigo = string.Empty;
            int Total = 0;

            Cmd = new SqlCommand("SELECT COUNT(*) as TotalRegistros FROM Clientes", Con.Abrir());

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

        #endregion

        //Método para guardar la información del cliente en la Base de Datos
        public virtual bool Guardar()
        {
            try
            {
                if (txtAddCodeCliente.Text == string.Empty || txtAddNombreCliente.Text == string.Empty || cboEstadoClientes.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Por favor llene los campos de textos requeridos!!! ", "Agregar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    //Si el textbox de email está vacio
                    if (txtAddEmailCliente.Text != string.Empty)
                    {
                        //El campo de email puede estar vacio o ingresado con el formato correcto
                        bool val2 = regeEmail(txtAddEmailCliente);
                        if(val2 == false)
                        {
                            System.Windows.Forms.MessageBox.Show("El Email del Cliente tiene formato incorrecto, ejemplo: Ejemplo@gmail.com", "Agregar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        }
                        else
                        {

                            if (txtAddTelefonoCliente.Text != string.Empty)
                            {
                                bool val = Regexcel(txtAddTelefonoCliente);
                                if (val == false)
                                {
                                    System.Windows.Forms.MessageBox.Show("El teléfono del Cliente tiene formato incorrecto,         ejemplo: 0000-0000.", "Agregar Proveedor", System.Windows.Forms.MessageBoxButtons.OK,            System.Windows.Forms.MessageBoxIcon.Exclamation);
                                }

                                else
                                {
                                    Cliente.Codigo = txtAddCodeCliente.Text.Trim();
                                    Cliente.Nombre = txtAddNombreCliente.Text.Trim();
                                    Cliente.Telefono = txtAddTelefonoCliente.Text.Trim();
                                    Cliente.Email = txtAddEmailCliente.Text.Trim();
                                    Cliente.Estado = cboEstadoClientes.Text.Trim();


                                    Clientes.AgregarCliente(Cliente);

                                    System.Windows.Forms.MessageBox.Show("Cliente Agregado exitosamente!!! ", "Agregar Cliente",    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                                    LimpiarControles();

                                    GenerarCodigo();

                                    txtAddNombreCliente.Focus();

                                    Agregar();

                                   


                                }
                            }

                            else
                            {
                              
                                Cliente.Codigo = txtAddCodeCliente.Text.Trim();
                                Cliente.Nombre = txtAddNombreCliente.Text.Trim();
                                Cliente.Telefono = txtAddTelefonoCliente.Text.Trim();
                                Cliente.Email = txtAddEmailCliente.Text.Trim();
                                Cliente.Estado = cboEstadoClientes.Text.Trim();


                                Clientes.AgregarCliente(Cliente);

                                System.Windows.Forms.MessageBox.Show("Cliente Agregado exitosamente!!! ", "Agregar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                                LimpiarControles();

                                GenerarCodigo();

                                txtAddNombreCliente.Focus();

                                Agregar();
                            }
                        }
                    }

                    else
                    {
                        //Si el textbox de telefono está vacio
                        if (txtAddTelefonoCliente.Text != string.Empty)
                        {
                            //El campo de telefono puede estar vacio o ingresado con el formato correcto
                            bool val = Regexcel(txtAddTelefonoCliente);
                            if (val == false)
                            {
                                System.Windows.Forms.MessageBox.Show("El teléfono del Cliente tiene formato incorrecto,         ejemplo: 0000-0000.", "Agregar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                            }

                            else
                            {
                                Cliente.Codigo = txtAddCodeCliente.Text.Trim();
                                Cliente.Nombre = txtAddNombreCliente.Text.Trim();
                                Cliente.Telefono = txtAddTelefonoCliente.Text.Trim();
                                Cliente.Email = txtAddEmailCliente.Text.Trim();
                                Cliente.Estado = cboEstadoClientes.Text.Trim();


                                Clientes.AgregarCliente(Cliente);

                                System.Windows.Forms.MessageBox.Show("Cliente Agregado exitosamente!!! ", "Agregar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                                LimpiarControles();

                                GenerarCodigo();

                                txtAddNombreCliente.Focus();

                                Agregar();




                            }
                        }

                        //Ingresa la información del cliente ya que el email y el telefono son opcionales pueden estar vacios
                        else
                        {

                            Cliente.Codigo = txtAddCodeCliente.Text.Trim();
                            Cliente.Nombre = txtAddNombreCliente.Text.Trim();
                            Cliente.Telefono = txtAddTelefonoCliente.Text.Trim();
                            Cliente.Email = txtAddEmailCliente.Text.Trim();
                            Cliente.Estado = cboEstadoClientes.Text.Trim();


                            Clientes.AgregarCliente(Cliente);

                            System.Windows.Forms.MessageBox.Show("Cliente Agregado exitosamente!!! ", "Agregar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                            LimpiarControles();

                            GenerarCodigo();

                            txtAddNombreCliente.Focus();

                            Agregar();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Proveedor no fue agregado porque: " + ex.Message, "Agregar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
        }

        //Método para limpiar los textbox del formulario
        private void LimpiarControles()
        {
            txtAddCodeCliente.Clear();
            txtAddNombreCliente.Clear();
            txtAddTelefonoCliente.Clear();
            txtAddEmailCliente.Clear();
            Hide();
        }


        #region Validacion de los TextBox 
        private void TxtAddNombreCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddEmailCliente.Focus();
                e.Handled = true;
            }


            if ((e.Key >= Key.D0 && e.Key <= Key.D9) ||  (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de numeros", "Agregar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
        private void TxtAddTelefonoCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cboEstadoClientes.Focus();
                e.Handled = true;
            }

            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key == Key.Space) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de letras y espacios", "Agregar Cliente", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
        private void TxtAddEmailCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddTelefonoCliente.Focus();
                e.Handled = true;
            }
        }


        #endregion

        #region Botones de la pantalla
        private void AddguardarBtn_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        private void AddCancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Agregar();
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

        #endregion

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

     
    }
}
