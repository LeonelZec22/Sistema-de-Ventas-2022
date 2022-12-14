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
using MaterialDesignThemes.Wpf;
using CapaNegocio;
using CapaEntidad;
using CapaDatos;
using CapaEntidad.Caches;
using System.Data;
using System.Net.Mail;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        CD_Usuarios user = new CD_Usuarios();
        CDo_Usuarios Usuarios = new CDo_Usuarios();
        CE_Usuarios Usuario = new CE_Usuarios();
        //Métodos para cambiar al modo oscuro del tema 
        public bool IsDarkTheme { get; set; }

        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txtUsername.Text != string.Empty)
                {
                    if(txtPassword.Password != string.Empty)
                    {
                        Usuario.Usuario = txtUsername.Text.Trim();
                        Usuario.Password = txtPassword.Password.Trim();
                        

                        DataTable User = Usuarios.LoginUsuario(Usuario);

                        //MainWindow FormularioPrincipal = new MainWindow();
                        //FormularioPrincipal.Show();
                        //this.Hide();

                        if (User.Rows.Count != 0)
                        {
                            MainWindow FormularioPrincipal = new MainWindow(txtUsername.Text);
                            FormularioPrincipal.Show();
                            this.Hide();


                        }

                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Usuario o Contraseña Incorrecto, \n Por favor Intentelo Otra Vez!!! ", "Iniciar Sesion", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

                            txtUsername.Clear();
                            txtPassword.Clear();
                            txtUsername.Focus();
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Debe completar el campo de Contraseña !!! ", "Iniciar Sesion", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }
                }

                else
                {
                    System.Windows.Forms.MessageBox.Show("Debe completar el campo de Usuario !!! ", "Iniciar Sesion", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
            }

            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("No se puede ingresar al sistema por que: " +ex.Message, "Iniciar Sesion", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loginBtn.Focus();
                e.Handled = true;
            }
        }

        private void TxtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
            }
        }

        int index = 0;

        private void RestaurarBtn_Click(object sender, RoutedEventArgs e)
        {
            string Usuario1 = txtUsername.Text;

            if (Usuario1 != string.Empty)
            {
                List<CE_Usuarios> Recuperacion = user.comprobarUsuario(txtUsername.Text);
                if (Recuperacion != null)
                {
                    if (Recuperacion.Count > 0)
                    {
                        try
                        {
                            Correos Cr = new CapaDatos.Correos();
                            MailMessage mnsj = new MailMessage();

                            mnsj.Subject = "Recuperacion de contraseña";

                            mnsj.To.Add(new MailAddress(Recuperacion.ElementAt(0).Correo));

                            mnsj.From = new MailAddress("thermalspa22@hotmail.com", "Soporte Tecnico");

                            /* Si deseamos Adjuntar algún archivo*/
                            //mnsj.Attachments.Add(new Attachment("C:\\archivo.pdf"));

                            mnsj.Body = @"Estimado " + Recuperacion.ElementAt(0).Nombre + "  " + Recuperacion.ElementAt(0).Apellido + " ,su contraseña es: " + Recuperacion.ElementAt(0).Password + "\n Por favor no compartirla con nadie ";

                            /* Enviar */
                            Cr.MandarCorreo(mnsj);

                            System.Windows.Forms.MessageBox.Show("La Contraseña se ha enviado correctamente al correo del Usuario Registrado.", "Confirmación", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("El usuario no existe.", "Aviso", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    }
                }

                else
                {
                    System.Windows.Forms.MessageBox.Show("El usuario no existe.", "Aviso", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("El usuario no puede estar vacío.", "Aviso", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
