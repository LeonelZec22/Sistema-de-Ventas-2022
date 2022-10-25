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
using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para FrmCompraDeProducto.xaml
    /// </summary>
    public partial class FrmCompraDeProducto : Window
    {
        public FrmCompraDeProducto()
        {
            InitializeComponent();
            CargarDatos();
        }


        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Ingreso_Productos IngresoProductos = new CDo_Ingreso_Productos();
        CE_Ingreso_Productos IngresoProducto = new CE_Ingreso_Productos();


        //Método para cargar datos de la tabla Ingreso Productos 

        private void CargarDatos()
        {
            DataGridIngresoProducto.ItemsSource = IngresoProductos.MostrarIngresoProductos().AsDataView();

        }

        //Evento para ocultar una columna de un datagrid autogenerico
        private void DataGridIngresoProducto_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_IngresoProducto = e.Column.Header.ToString();

            if (Id_IngresoProducto == "Id_IngresoProducto")
            {
                e.Cancel = true;
            }
        }









        private void BtnNuevoIngreso_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAnularIngreso_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnImprimirInfo_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DataGridIngresoProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
