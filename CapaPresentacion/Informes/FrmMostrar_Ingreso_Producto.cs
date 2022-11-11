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
    public partial class FrmMostrar_Ingreso_Producto: Form
    {
        public FrmMostrar_Ingreso_Producto()
        {
            InitializeComponent();

        }

        private int _Id_IngresoProducto;

        public int Id_IngresoProducto { get => _Id_IngresoProducto; set => _Id_IngresoProducto = value; }

        private void FrmMostrar_Ingreso_Producto_Load(object sender, EventArgs e)
        {
            this.mostrar_Ingreso_ProductosTableAdapter.Fill(this.dB_Sistemas_v2DataSet.Mostrar_Ingreso_Productos, Id_IngresoProducto);
            this.reportViewer1.RefreshReport();
        }
    }
}
