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
using CapaNegocio;
using CapaEntidad;
using CapaDatos;
using System.Data;
using System.Data.SqlClient;


namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para FrmEditarProveedor.xaml
    /// </summary>
    public partial class FrmEditarProveedor : Window
    {
        public FrmEditarProveedor()
        {
            InitializeComponent();
        }


        //Creamos una instancia de la pantalla proveedores 

        public FrmEditarProveedor( FrmProveedores Proveedores)
        {
            InitializeComponent();
        }

        //Creacion de variables a usar 
        CDo_Procedimientos Procedimientos = new CDo_Procedimientos();
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


        //Metodo para Actualizar
        protected void Actualizar()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);
        }


        #region Evento de los textbox
        private void TxtEditNombreProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtEditDireccion.Focus();
                e.Handled = true;
            }
        }

        private void TxtEditDireccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtEditTelefono.Focus();
                e.Handled = true;
            }
        }

        private void TxtEditTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EditguardarBtn.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region Botones 

        private void EditguardarBtn_Click(object sender, RoutedEventArgs e)
        {
            Editar();
        }

        private void EditCancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Actualizar();
        }

        #endregion

        public virtual void Editar()
        {
            try
            {
                if (txtEditCodeProveedor.Text == string.Empty || txtEditNombreProveedor.Text == string.Empty || txtEditDireccion.Text == string.Empty || txtEditTelefono.Text == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("Por favor llene todos los campos de textos!!! ", "Editar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
                else
                {
                    Proveedor.Id_Proveedor = Convert.ToInt32(txtEditIDProveedor.Text.Trim());
                    Proveedor.Codigo = txtEditCodeProveedor.Text.Trim();
                    Proveedor.Nombre = txtEditNombreProveedor.Text.Trim();
                    Proveedor.Direccion = txtEditDireccion.Text.Trim();
                    Proveedor.Telefono = txtEditTelefono.Text.Trim();


                    Proveedores.EditarProveedor(Proveedor);

                    System.Windows.Forms.MessageBox.Show("Proveedor Editado exitosamente!!! ", "Editar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    this.Hide();

                    Actualizar();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("El Proveedor no fue editado porque: " + ex.Message, "Editar Proveedor", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            
        }
    }
}