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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        //Creamos un nuevo constructor del objeto 

        public MainWindow(bool doNotMakeInvisible)
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            FrmProductos frmProductos = new FrmProductos();
            frmProductos.ShowDialog();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FrmInventario frmInventario = new FrmInventario();
            Hide();
            frmInventario.ShowDialog();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FrmProveedores Proveedor = new FrmProveedores();
            Hide();
            Proveedor.ShowDialog();
            Close();
        }
    }
}
