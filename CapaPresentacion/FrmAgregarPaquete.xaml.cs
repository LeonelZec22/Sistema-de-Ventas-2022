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
    /// Lógica de interacción para FrmAgregarPaquete.xaml
    /// </summary>
    public partial class FrmAgregarPaquete : Window
    {
        public FrmAgregarPaquete()
        {
            InitializeComponent();
        }

        public FrmAgregarPaquete(FrmPaquetes Paquetes)
        {
            InitializeComponent();
        }

        public FrmAgregarPaquete(VistaPaquetes Paquetes)
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GenerarCodigo();

        }

        CDo_Paquetes Paquetes = new CDo_Paquetes();
        CE_Paquetes Paquete = new CE_Paquetes();
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();

        #region Evento de Cargar 
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
                txtAddCodePaquete.Text = "PAQ" + GenerarCodigoPaquete();
            }

            catch (Exception ex)
            {
                MessageBox.Show("El codigo del paquete no se pudo generar por:" + ex.Message, "Agregar Paquete", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        CD_Conexion Con = new CD_Conexion();
        SqlCommand Cmd;
        SqlDataReader Dr;

        private string GenerarCodigoPaquete()
        {
            string Codigo = string.Empty;
            int Total = 0;

            Cmd = new SqlCommand("SELECT COUNT(*) as TotalRegistros FROM Paquetes", Con.Abrir());

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

        #region Método para guardar información en la base de datos
        public virtual bool Guardar()
        {
            try
            {
                if (txtAddCodePaquete.Text == string.Empty || txtAddNombrePaquete.Text == string.Empty || txtAddDescripcionPaquete.Text == string.Empty || txtAddCantidad_Vendida.Text == string.Empty||
                    txtAddPrecioVenta.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Por favor llene los campos de textos requeridos!!! ", "Agregar Paquetes", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    Paquete.Codigo = txtAddCodePaquete.Text.Trim();
                    Paquete.Nombre = txtAddNombrePaquete.Text.Trim();
                    Paquete.Descripcion = txtAddDescripcionPaquete.Text.Trim();
                    Paquete.Cantidad_Vendida = Convert.ToInt32(txtAddCantidad_Vendida.Text.Trim());
                    Paquete.Precio_Venta = Convert.ToDecimal(txtAddPrecioVenta.Text.Trim());

                    Paquetes.AgregarPaquete(Paquete);

                    System.Windows.Forms.MessageBox.Show("Paquete Agregado exitosamente!!! ", "Agregar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    LimpiarControles();

                    Agregar();

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Paquete no fue agregado porque: " + ex.Message, "Agregar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
        }
        private void AddguardarBtn_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        #endregion

        private void LimpiarControles()
        {
            txtAddCodePaquete.Clear();
            txtAddNombrePaquete.Clear();
            txtAddDescripcionPaquete.Clear();
            txtAddCantidad_Vendida.Clear();
            txtAddPrecioVenta.Clear();
            Hide();
        }


        #region Validacion de los TextBox
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
           
        }

        private void TxtAddCantidad_Vendida_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtAddCantidad_Vendida.Text.Length > 5)
            {
                System.Windows.Forms.MessageBox.Show("La Cantidad Vendida  no puede ser mayor a 5 caracteres", "Agregar Paquete", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
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
        #endregion


        #region Métodos para salir de la pantalla
        private void AddCancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Agregar();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
            Agregar();
        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            Agregar();
        }

        #endregion

      
    }
}
