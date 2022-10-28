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
using System.Collections.ObjectModel;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para DataGridBase.xaml
    /// </summary>
    public partial class DataGridBase : Window
    {
        public DataGridBase()
        {
            InitializeComponent();
        }

        //Evento para maximizar la pantalla

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton==MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximized = false;

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount==2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }

                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
            }
        }

        private void Seleccionar()
        {
            FrmClientes clientePrueba = new FrmClientes();

            

            if (clientePrueba.DataGridClientes.Items.Count == 0)
            {
                //Si el DataGrid está vacio que no devuelva nada pero si tiene al menos una fila es decir un dato que ejecute el bloque else
                System.Windows.Forms.MessageBox.Show("No hay registro para seleccionar", "Seleccionar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Registro seleccionado", "Seleccionar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                //Al momento de hacer clic en el boton si hay datos pues se cierra 
                DialogResult = true;
                Hide();
            }
        }

        

        private void BotonAdd_Click(object sender, RoutedEventArgs e)
        {
            Seleccionar();
        }
    }
}
