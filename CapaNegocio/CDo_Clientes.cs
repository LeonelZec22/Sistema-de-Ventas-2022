using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaNegocio
{
    public class CDo_Clientes
    {
        CD_Clientes ObjClientes = new CD_Clientes();

        //Método que me permite agregar un Cliente a la base de datos

        public void AgregarCliente(CE_Clientes Clientes)
        {
            ObjClientes.AgregarCliente(Clientes);
        }

        //Método que me permite editar un Cliente a la base de datos

        public void EditarCliente(CE_Clientes Clientes)
        {
            ObjClientes.EditarCliente(Clientes);
        }

        //Método que me permite eliminar un Cliente a la base de datos

        public void EliminarCliente(CE_Clientes Clientes)
        {
            ObjClientes.EliminarCliente(Clientes);
        }


        #region Buscar productos
        //Método que me permite buscar un Cliente por codigo

        public DataTable Buscar_Cliente_Codigo(CE_Clientes Clientes)
        {
            return ObjClientes.Buscar_Cliente_Codigo(Clientes);
        }

        //Método que me permite buscar un Cliente por nombre

        public DataTable Buscar_Cliente_Nombre(CE_Clientes Clientes)
        {
            return ObjClientes.Buscar_Cliente_Nombre(Clientes);
        }



        #endregion
    }
}
