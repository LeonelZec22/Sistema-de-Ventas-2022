using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_Ingreso_Productos
    {
        private int _Id_IngresoProducto;
        private string _No_Ingreso;
        private int _Id_Proveedor;
        private DateTime _Fecha_Ingreso;
        private decimal _Monto_total;
        private string _Estado;
        private string _Buscar;
        //private ObservableCollection _ProductData;

        public int Id_IngresoProducto { get => _Id_IngresoProducto; set => _Id_IngresoProducto = value; }
        public string No_Ingreso { get => _No_Ingreso; set => _No_Ingreso = value; }
        public int Id_Proveedor { get => _Id_Proveedor; set => _Id_Proveedor = value; }
        public DateTime Fecha_Ingreso { get => _Fecha_Ingreso; set => _Fecha_Ingreso = value; }
        public decimal Monto_total { get => _Monto_total; set => _Monto_total = value; }
        public string Estado { get => _Estado; set => _Estado = value; }
        public string Buscar { get => _Buscar; set => _Buscar = value; }
        
        public IEnumerable<DataGridItemsProducto> AddProducto { get; set; }
    }
}