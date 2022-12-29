using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CapaDatos;
using System.Configuration;

namespace CapaPresentacion
{
    public partial class BackupBaseDatos : Form
    {
        public BackupBaseDatos()
        {
            InitializeComponent();
        }

        //nuestra conexion
        
        //SqlConnection con = new SqlConnection(@"Data Source=LEONEL\SISTEMADEVENTAS;Initial Catalog=DB_Sistemas_V2;Integrated Security=True");

         SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString);


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtUbicacion.Text = dlg.SelectedPath;
                btnBackup.Enabled = true;
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string database = con.Database.ToString();

            try
            {
                if (txtUbicacion.Text == string.Empty)
                {
                    MessageBox.Show("Por favor seleccione la ubicacion a guardar el archivo", "Crear Copia de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                else
                {
                    string cmd = "BACKUP DATABASE [" + database + "] TO DISK='" + txtUbicacion.Text + "\\" + "Backup-Thermal-SPA" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";

                    using (SqlCommand command = new SqlCommand(cmd, con))
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        command.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Copia de seguridad creada exitosamente", "Crear Copia de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnBackup.Enabled = false;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("No se pudo crear la copia de seguridad por " + ex, "Crear Copia de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SQL SERVER database backup files|*.bak";
            dlg.Title = "Database restore";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtUbicacion1.Text = dlg.FileName;
                btnRestaurar.Enabled = true;
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            string database = con.Database.ToString();

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            try
            {
                string str1 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");

                SqlCommand cmd1 = new SqlCommand(str1, con);
                cmd1.ExecuteNonQuery();

                string str2 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='" + txtUbicacion1.Text + "'WITH REPLACE;";
                SqlCommand cmd2 = new SqlCommand(str2, con);
                cmd2.CommandTimeout = 30;
                cmd2.ExecuteNonQuery();

                string str3 = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
                SqlCommand cmd3 = new SqlCommand(str3, con);
                cmd3.CommandTimeout = 30;
                cmd3.ExecuteNonQuery();

                MessageBox.Show("Restauración de la base de datos hecha exitosamente", "Restaurar la Copia de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("No se pudo restaurar la copia de seguridad por " + ex, "Restaurar la Copia de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
