using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using CapaNegocio;
using CapaEntidad;
using CapaDatos;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para FrmServicios.xaml
    /// </summary>
    public partial class FrmServicios : Window
    {
        public FrmServicios()
        {
            InitializeComponent();

            CargarDatos();
        }

        //Creamos un objeto de nuestra clase conexión

        CD_Conexion Con = new CD_Conexion();


        CDo_Servicios Servicios = new CDo_Servicios();
        CE_Servicios Servicio = new CE_Servicios();
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();


        //Método para cargar o mostrar los datos en la tabla del formulario
        private void CargarDatos()
        {
            DataGridServicios.ItemsSource = Procedimientos.CargarDatos("Servicios").AsDataView();

        }

        //Evento para ocultar una columna de un datagrid autogenerico
        private void DataGridServicios_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string Id_Servicios = e.Column.Header.ToString();

            if (Id_Servicios == "Id_Servicios")
            {
                e.Cancel = true;
            }
        }


        //Eventos del DataGrid 

        //private void AgServi_UpdateEventHandler(object sender, FrmAgregarCliente.UpdateEventArgs args)
        //{
        //    CargarDatos();
        //}

        //private void EdClient_UpdateEventHandler(object sender, FrmEditarCliente.UpdateEventArgs args)
        //{
        //    CargarDatos();
        //}



        #region Botones superiores


        private void BtnAddServicio_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditServicio_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteServicio_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void DataGridServicios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        #region Botones menu izquierda
        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
