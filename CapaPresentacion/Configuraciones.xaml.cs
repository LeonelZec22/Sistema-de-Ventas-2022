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
    /// Lógica de interacción para Configuraciones.xaml
    /// </summary>
    public partial class Configuraciones : Window
    {
        public Configuraciones()
        {
            InitializeComponent();
        }

        private void CancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        InformacionEmpresa empresa = new InformacionEmpresa();

        private void BtnInforEmp_Click(object sender, RoutedEventArgs e)
        {
            empresa.ShowDialog();
        }


        private void BtnBackup_Click(object sender, RoutedEventArgs e)
        {
            BackupBaseDatos backupBase = new BackupBaseDatos();
            backupBase.ShowDialog();
        }

        private void CloseApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
        }
    }
}
