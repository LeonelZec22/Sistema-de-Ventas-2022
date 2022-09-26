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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CapaPresentacion.Menu
{
    /// <summary>
    /// Lógica de interacción para Modulos.xaml
    /// </summary>
    public partial class Modulos : UserControl
    {
        public Modulos()
        {
            InitializeComponent();
        }

        //Métodos para poder agregar los estilos

        public string Titulo
        {
            get { return (string)GetValue(TituloPropiedad); }
            set { SetValue(TituloPropiedad, value); }
        }

        public static readonly DependencyProperty TituloPropiedad = DependencyProperty.Register("Titulo", typeof(string), typeof(Modulos));

        //Métodos para poder agregar los estilos

        public bool EsActivo
        {
            get { return (bool)GetValue(EsActivoPropiedad); }
            set { SetValue(EsActivoPropiedad, value); }
        }

        public static readonly DependencyProperty EsActivoPropiedad = DependencyProperty.Register("EsActivo", typeof(bool), typeof(Modulos));
    }
}
