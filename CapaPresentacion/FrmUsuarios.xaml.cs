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
    /// Lógica de interacción para FrmUsuarios.xaml
    /// </summary>
    public partial class FrmUsuarios : Window
    {
        public FrmUsuarios()
        {
            InitializeComponent();

            
        }

        //Instancia de la clases
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Usuarios Usuarios = new CDo_Usuarios();
        CE_Usuarios Usuario = new CE_Usuarios();

        private void MostrarUsuarios()
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MostrarUsuarios();
        }

        private void BtnNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditarUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TxtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGridUsuarios_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void DataGridUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnProductos_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
