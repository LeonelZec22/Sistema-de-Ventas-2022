using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CE_Productos
    {
        //Variables para guardar y enviar información de la tabla productos

        private int _Id_Producto;
        private string _Codigo;
        private string _Nombre;
        private string _Descripcion;
        private decimal _Precio_Venta;
        private decimal _Costo_Unitario;
        private string _Buscar;

        //Creo los métodos get y set de cada variable que corresponde a los campos de la tabla productos

        public int Id_Producto { get => _Id_Producto; set => _Id_Producto = value; }
        public string Codigo { get => _Codigo; set => _Codigo = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public decimal Precio_Venta { get => _Precio_Venta; set => _Precio_Venta = value; }
        public decimal Costo_Unitario { get => _Costo_Unitario; set => _Costo_Unitario = value; }
        public string Buscar { get => _Buscar; set => _Buscar = value; }
    }
}
