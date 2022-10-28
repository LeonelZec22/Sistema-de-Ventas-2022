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


namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para FormAgregarBase.xaml
    /// </summary>
    public partial class FormAgregarBase : Window
    {
        public FormAgregarBase()
        {
            InitializeComponent();

            //EditarPrueba();
        }

        //Método para mover la ventana
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton== MouseButton.Left)
            {
                this.DragMove();
            }
        }

       
        public void EditarPrueba()
        {
            //FormEditBase EditaDialog = new FormEditBase();

            //EditaDialog.ShowDialog();

            //EditaDialog.DialogResult = true;

            //FrmClientes ClientesPrueba = new FrmClientes();

            //int num = ClientesPrueba.DataGridClientes.Items.Count;

            //txtAddCodeProduct.Text =Convert.ToString(num);

        }
        

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            
            
        }


    }
}
