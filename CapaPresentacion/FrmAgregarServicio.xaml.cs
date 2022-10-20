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

            GenerarCodigo();
        }

        public FrmAgregarServicio(FrmServicios Servicios)
        {
            InitializeComponent();

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
                if (txtAddCodeServicio.Text == string.Empty || txtAddNombreServicio.Text == string.Empty || txtAddDescripcionServicio.Text == string.Empty || txtAddPrecioVenta.Text == string.Empty)
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
            Close();
        }

        #region Evento de los TextBox 
        private void TxtAddNombreServicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddDescripcionServicio.Focus();
                e.Handled = true;
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
        }
        private void TxtAddPrecioVenta_LostFocus(object sender, RoutedEventArgs e)
        {
            Procedimientos.FormatoMoneda(txtAddPrecioVenta);
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

    }
}
