﻿using System;
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
using CapaEntidad;
using CapaNegocio;
using System.Data;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para FrmAgregarIngreso.xaml
    /// </summary>
    public partial class FrmAgregarIngreso : Window
    {
        public FrmAgregarIngreso()
        {
            InitializeComponent();
        }

        public FrmAgregarIngreso(FrmCompraDeProducto Compras)
        {
            InitializeComponent();
        }

        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
        CDo_Ingreso_Productos Ingresos = new CDo_Ingreso_Productos();
        CE_Ingreso_Productos Ingreso = new CE_Ingreso_Productos();
        CDo_Detalle_Ingreso DetalleIngresos = new CDo_Detalle_Ingreso();
        CE_Detalle_Ingreso DetalleIngreso = new CE_Detalle_Ingreso();

        CDo_Proveedores Proveedores = new CDo_Proveedores();
        CE_Proveedores Proveedor = new CE_Proveedores();

        //Agregamos un delegado

        public delegate void UpdateDelegate(object sender, UpdateEventArgs args);

        //Creamos un evento para actualizar el datagrid
        public event UpdateDelegate UpdateEventHandler;

        public class UpdateEventArgs : EventArgs
        {
            public string Data { get; set; }
        }

        protected void Agregar()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtTotal_Pago.Text = "0.00";
            Correlativo();
        }

        //Método para generar el numero de ingreso 

        private void Correlativo()
        {
            txtId_IngresoProducto.Text = Procedimientos.GenerarCodigoId("Ingreso_Productos");

            txtNo_Ingreso.Text = "INGR" + Procedimientos.GenerarCodigo("Ingreso_Productos");

            txtId_Detalle.Text = Procedimientos.GenerarCodigoId("Detalles_Ingreso");
        }


        DataGrid dg;
        DataRowView dr;

        private void BtnBuscarProveedor_Click(object sender, RoutedEventArgs e)
        {
            FrmVistaProveedores VistaProveedor = new FrmVistaProveedores();

            VistaProveedor.ShowDialog();

            this.Hide();
            //try
            //{
            //    if(VistaProveedor.DialogResult == true)
            //    {

            //        //txtId_Proveedor.Text = VistaProveedor.DataGridGestionProveedores.Items[VistaProveedor.DataGridGestionProveedores.CurrentItem.]

            //        //txtId_Proveedor.Text = VistaProveedor

            //        Proveedor.Id_Proveedor = Convert.ToInt32(txtId_Proveedor.Text.Trim());
            //        Proveedor.Nombre = txtNombre_Proveedor.Text.Trim();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show("Debe de Seleccionar un Proveedor en la lista proveedores!!", "Seleccionar Proveedor", System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Exclamation);
            //}
        }
    }
}
