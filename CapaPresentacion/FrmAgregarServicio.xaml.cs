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
    /// Lógica de interacción para FrmAgregarServicio.xaml
    /// </summary>
    public partial class FrmAgregarServicio : Window
    {
        public FrmAgregarServicio()
        {
            InitializeComponent();

        }

        public FrmAgregarServicio(FrmServicios Servicios)
        {
            InitializeComponent();

        }

        public FrmAgregarServicio(FrmVistaServicios Servicios)
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GenerarCodigo();

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

        protected void Agregar()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);
        }

        #region Generar Código
        private void GenerarCodigo()
        {
            try
            {
                txtAddCodeServicio.Text = "SERVI" + GenerarCodigoServicio();
            }

            catch (Exception ex)
            {
                MessageBox.Show("El codigo del servicio no se pudo generar por:" + ex.Message, "Agregar Servicio", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        CD_Conexion Con = new CD_Conexion();


        SqlCommand Cmd;
        SqlDataReader Dr;

        private string GenerarCodigoServicio()
        {
            string Codigo = string.Empty;
            int Total = 0;

            Cmd = new SqlCommand("SELECT COUNT(*) as TotalRegistros FROM Servicios", Con.Abrir());

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


        public virtual bool Guardar()
        {
            try
            {
                if (txtAddCodeServicio.Text == string.Empty || txtAddNombreServicio.Text == string.Empty || txtAddPrecioVenta.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Por favor llene los campos de textos requeridos!!! ", "Agregar Servicio", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    Servicio.Codigo = txtAddCodeServicio.Text.Trim();
                    Servicio.Nombre = txtAddNombreServicio.Text.Trim();
                    Servicio.Descripcion = txtAddDescripcionServicio.Text.Trim();
                    Servicio.Precio_Venta = txtAddPrecioVenta.Text.Trim();


                    Servicios.AgregarServicio(Servicio);

                    System.Windows.Forms.MessageBox.Show("Servicio Agregado exitosamente!!! ", "Agregar Servicio", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    LimpiarControles();

                    GenerarCodigo();

                    txtAddCodeServicio.Focus();

                    Agregar();

                    //FrmProductos productos = new FrmProductos();
                    //productos.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Servicio no fue agregado porque: " + ex.Message, "Agregar Servicio", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
        }

        private void LimpiarControles()
        {
            txtAddCodeServicio.Clear();
            txtAddNombreServicio.Clear();
            txtAddDescripcionServicio.Clear();
            txtAddPrecioVenta.Clear();
            Hide();
        }

        #region Evento de los TextBox 
        private void TxtAddNombreServicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddDescripcionServicio.Focus();
                e.Handled = true;
            }

            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de numeros", "Agregar Servicios", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private void TxtAddDescripcionServicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddPrecioVenta.Focus();
                e.Handled = true;
            }
        }

        private void TxtAddPrecioVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddguardarBtn.Focus();
                e.Handled = true;
            }
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key == Key.LeftCtrl))
            {
                e.Handled = true;
                System.Windows.Forms.MessageBox.Show("No se permite el ingreso de Letras", "Agregar Servicios", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }
        private void TxtAddPrecioVenta_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtAddPrecioVenta.Text.Length > 12)
            {
                System.Windows.Forms.MessageBox.Show("El Descuento  no puede ser mayor a 12 caracteres", "Agregar Servicios", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

            else
            {
                try
                {
                    if (Convert.ToDecimal(txtAddPrecioVenta.Text) > 0)
                    {
                        Procedimientos.FormatoMoneda(txtAddPrecioVenta);
                       
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("El Descuento no puede ser menor a cero", "Agregar Servicios", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                        txtAddPrecioVenta.Clear();
                    }
                }

                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("El Descuento no es un numero por favor ingrese solo numeros", "Agregar Servicios", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    txtAddPrecioVenta.Clear();
                    //txtAddCostoUnit.Focus();
                }


            }
           
        }

        #endregion

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

       
    }
}
