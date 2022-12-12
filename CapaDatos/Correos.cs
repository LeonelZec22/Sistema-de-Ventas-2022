using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace CapaDatos
{
    public class Correos
    {
        /*
         * Cliente SMTP
         * Gmail:  smtp.gmail.com  puerto:587
         * Hotmail: smtp.live.com  puerto:25
         */

        //SmtpClient server = new SmtpClient("smtp.gmail.com", 587);
        SmtpClient server = new SmtpClient("smtp-mail.outlook.com", 587);

        public  Correos()
        {
            /*
             * Autenticacion en el Servidor
             * Utilizaremos nuestra cuenta de correo
             *
             * Direccion de Correo (Gmail o Hotmail)
             * y Contrasena correspondiente
             */
            
            server.Credentials = new System.Net.NetworkCredential("thermalspa22@hotmail.com", "+Thermal+SPA$2022");
            server.EnableSsl = true;
        }

        public void MandarCorreo(MailMessage mensaje)
        {
            server.Send(mensaje);
        }
    }
}
