using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Informes
{
    public partial class FrmMostrar_Detalle_Venta : Form
    {
        public FrmMostrar_Detalle_Venta()
        {
            InitializeComponent();
        }

        private int _Id_Venta;

        public int Id_Venta { get => _Id_Venta; set => _Id_Venta = value; }

        private void FrmMostrar_Detalle_Venta_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'Datos_Generales3.SP_Mostrar_Informe_Venta' Puede moverla o quitarla según sea necesario.
            this.SP_Mostrar_Informe_VentaTableAdapter.Fill(this.Datos_Generales3.SP_Mostrar_Informe_Venta, Id_Venta);

            this.reportViewer1.RefreshReport();
        }
    }
}
