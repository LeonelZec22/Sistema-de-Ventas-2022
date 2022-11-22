namespace CapaPresentacion.Informes
{
    partial class FrmMostrar_Detalle_Venta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Datos_Generales3 = new CapaPresentacion.Informes.Datos_Generales3();
            this.SP_Mostrar_Informe_VentaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SP_Mostrar_Informe_VentaTableAdapter = new CapaPresentacion.Informes.Datos_Generales3TableAdapters.SP_Mostrar_Informe_VentaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Generales3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_Mostrar_Informe_VentaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Mostrar_Ventas";
            reportDataSource1.Value = this.SP_Mostrar_Informe_VentaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CapaPresentacion.Informes.Mostrar_Venta.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // Datos_Generales3
            // 
            this.Datos_Generales3.DataSetName = "Datos_Generales3";
            this.Datos_Generales3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SP_Mostrar_Informe_VentaBindingSource
            // 
            this.SP_Mostrar_Informe_VentaBindingSource.DataMember = "SP_Mostrar_Informe_Venta";
            this.SP_Mostrar_Informe_VentaBindingSource.DataSource = this.Datos_Generales3;
            // 
            // SP_Mostrar_Informe_VentaTableAdapter
            // 
            this.SP_Mostrar_Informe_VentaTableAdapter.ClearBeforeFill = true;
            // 
            // FrmMostrar_Detalle_Venta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmMostrar_Detalle_Venta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mostrar Detalle Venta";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMostrar_Detalle_Venta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Generales3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_Mostrar_Informe_VentaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource SP_Mostrar_Informe_VentaBindingSource;
        private Datos_Generales3 Datos_Generales3;
        private Datos_Generales3TableAdapters.SP_Mostrar_Informe_VentaTableAdapter SP_Mostrar_Informe_VentaTableAdapter;
    }
}