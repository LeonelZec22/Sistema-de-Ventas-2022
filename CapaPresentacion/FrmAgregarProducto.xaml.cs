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

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para FrmAgregarProducto.xaml
    /// </summary>
    public partial class FrmAgregarProducto : Window
    {
        public FrmAgregarProducto()
        {
            InitializeComponent();
        }

        //Creamos una instancia de la pantalla productos
        public FrmAgregarProducto(FrmProductos Productos)
        {
            InitializeComponent();
        }

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Productos Productos = new CDo_Productos();
        CE_Productos Producto = new CE_Productos();

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

        private void AddCancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Evento para cambiar al siguiente textbox presionando la tecla enter
        private void TxtAddNombreProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key== Key.Enter)
            {
                txtAddDescripcion.Focus();
                e.Handled = true;
            }
        }

        private void TxtAddDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtAddCostoUnit.Focus();
                e.Handled = true;
            }
        }

        private void TxtAddCostoUnit_KeyDown(object sender, KeyEventArgs e)
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

        private void TxtAddCostoUnit_LostFocus(object sender, RoutedEventArgs e)
        {
            Procedimientos.FormatoMoneda(txtAddCostoUnit);
        }

        private void TxtAddPrecioVenta_LostFocus(object sender, RoutedEventArgs e)
        {
            Procedimientos.FormatoMoneda(txtAddPrecioVenta);
        }

        
    }
}
