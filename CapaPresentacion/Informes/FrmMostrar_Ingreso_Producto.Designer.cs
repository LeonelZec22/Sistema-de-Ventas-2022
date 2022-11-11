namespace CapaPresentacion.Informes
{
    partial class FrmMostrar_Ingreso_Producto
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
            this.Mostrar_Ingreso_ProductosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Mostrar_Ingreso_ProductosTableAdapter = new CapaPresentacion.Informes.Datos_Generales3TableAdapters.Mostrar_Ingreso_ProductosTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Generales3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mostrar_Ingreso_ProductosBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Mostrar_Ingreso_Productos";
            reportDataSource1.Value = this.Mostrar_Ingreso_ProductosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CapaPresentacion.Informes.Mostrar_Ingreso_Productos2.rdlc";
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
            // Mostrar_Ingreso_ProductosBindingSource
            // 
            this.Mostrar_Ingreso_ProductosBindingSource.DataMember = "Mostrar_Ingreso_Productos";
            this.Mostrar_Ingreso_ProductosBindingSource.DataSource = this.Datos_Generales3;
            // 
            // Mostrar_Ingreso_ProductosTableAdapter
            // 
            this.Mostrar_Ingreso_ProductosTableAdapter.ClearBeforeFill = true;
            // 
            // FrmMostrar_Ingreso_Producto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmMostrar_Ingreso_Producto";
            this.Text = "FrmMostrar_Ingreso_Producto";
            this.Load += new System.EventHandler(this.FrmMostrar_Ingreso_Producto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Generales3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mostrar_Ingreso_ProductosBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource Mostrar_Ingreso_ProductosBindingSource;
        private Datos_Generales3 Datos_Generales3;
        private Datos_Generales3TableAdapters.Mostrar_Ingreso_ProductosTableAdapter Mostrar_Ingreso_ProductosTableAdapter;
    }
}