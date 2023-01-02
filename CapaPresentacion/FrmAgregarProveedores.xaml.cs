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
    /// Lógica de interacción para FrmAgregarProveedores.xaml
    /// </summary>
    public partial class FrmAgregarProveedores : Window
    {
        public FrmAgregarProveedores()
        {
            InitializeComponent();
           
        }

        public FrmAgregarProveedores(FrmProveedores Proveedores)
        {
            InitializeComponent();
        }

        public FrmAgregarProveedores(FrmVistaProveedores Proveedores)
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GenerarCodigo();

        }

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Proveedores Proveedores = new CDo_Proveedores();
        CE_Proveedores Proveedor = new CE_Proveedores();

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

        //FrmVistaProveedores vistaProveedores = new FrmVistaProveedores();



        #region Generar Código
        private void GenerarCodigo()
        {
            try
            {

            txtAddCodeProveedor.Text = "PROVE" + GenerarCodigoProve();
            }

            catch(Exception ex)
            {
                MessageBox.Show("El codigo del proveedor no se pudo generar por:" + ex.Message, "Agregar Proveedor", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        CD_Conexion Con = new CD_Conexion();


        SqlCommand Cmd;
        SqlDataReader Dr;

        private string GenerarCodigoProve()
        {
            string Codigo = string.Empty;
            int Total = 0;

            Cmd = new SqlCommand("SELECT COUNT(*) as TotalRegistros FROM Proveedores", Con.Abrir());

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

        //FrmProveedores proveedores = new FrmProveedores();
        public virtual bool Guardar()
        {
            try
            {
                if (txtAddCodeProveedor.Text == string.Empty || txtAddNombreProveedor.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Por favor llene todos los campos requeridos!!! ", "Agregar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (txtAddTelefono.Text != string.Empty)
                    {
                        bool val = Regexcel(txtAddTelefono);
                        if (val == false)
                        {
                            System.Windows.Forms.MessageBox.Show("El teléfono del Proveedor tiene formato incorrecto, ejemplo: 0000-0000.", "Agregar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        }
                        else
                        {

                            Proveedor.Codigo = txtAddCodeProveedor.Text.Trim();
                            Proveedor.Nombre = txtAddNombreProveedor.Text.Trim();
                            Proveedor.Direccion = txtAddDireccion.Text.Trim();
                            Proveedor.Telefono = txtAddTelefono.Text.Trim();


                            Proveedores.AgregarProveedor(Proveedor);

                            System.Windows.Forms.MessageBox.Show("Proveedor Agregado exitosamente!!! ", "Agregar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                            LimpiarControles();

                            Agregar();

                            Hide();
                        }
                    }
                    else
                    {
                        Proveedor.Codigo = txtAddCodeProveedor.Text.Trim();
                        Proveedor.Nombre = txtAddNombreProveedor.Text.Trim();
                        Proveedor.Direccion = txtAddDireccion.Text.Trim();
                        Proveedor.Telefono = txtAddTelefono.Text.Trim();


                        Proveedores.AgregarProveedor(Proveedor);

                        System.Windows.Forms.MessageBox.Show("Proveedor Agregado exitosamente!!! ", "Agregar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                        LimpiarControles();

                        Agregar();

                        Hide();


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
            txtAddCodeProveedor.Clear();
            txtAddNombreProveedor.Clear();
            txtAddDireccion.Clear();
            txtAddTelefono.Clear();
            Hide();
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

        #region Validacion de los TextBox 
        private void TxtAddNombreProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddDireccion.Focus();
                e.Handled = true;
            }

            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de numeros", "Agregar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void TxtAddDireccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddTelefono.Focus();
                e.Handled = true;
            }
        }


        private void TxtAddTelefono_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                AddguardarBtn.Focus();
                e.Handled = true;
            }

            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key == Key.Space) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de letras y espacios", "Agregar Venta Reservas", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }


        #endregion


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
