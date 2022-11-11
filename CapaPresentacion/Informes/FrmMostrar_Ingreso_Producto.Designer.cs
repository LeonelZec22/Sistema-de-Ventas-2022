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
            this.dB_Sistemas_v2DataSet = new CapaPresentacion.DB_Sistemas_v2DataSet();
            this.mostrarIngresoProductosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mostrar_Ingreso_ProductosTableAdapter = new CapaPresentacion.DB_Sistemas_v2DataSetTableAdapters.Mostrar_Ingreso_ProductosTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dB_Sistemas_v2DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mostrarIngresoProductosBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Datos_Generales";
            reportDataSource1.Value = this.mostrarIngresoProductosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CapaPresentacion.Informes.Mostrar_Ingreso_Producto.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // dB_Sistemas_v2DataSet
            // 
            this.dB_Sistemas_v2DataSet.DataSetName = "DB_Sistemas_v2DataSet";
            this.dB_Sistemas_v2DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mostrarIngresoProductosBindingSource
            // 
            this.mostrarIngresoProductosBindingSource.DataMember = "Mostrar_Ingreso_Productos";
            this.mostrarIngresoProductosBindingSource.DataSource = this.dB_Sistemas_v2DataSet;
            // 
            // mostrar_Ingreso_ProductosTableAdapter
            // 
            this.mostrar_Ingreso_ProductosTableAdapter.ClearBeforeFill = true;
            // 
            // FrmMostrar_Ingreso_Producto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmMostrar_Ingreso_Producto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Ingreso de Producto";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMostrar_Ingreso_Producto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dB_Sistemas_v2DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mostrarIngresoProductosBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource mostrarIngresoProductosBindingSource;
        private DB_Sistemas_v2DataSet dB_Sistemas_v2DataSet;
        private DB_Sistemas_v2DataSetTableAdapters.Mostrar_Ingreso_ProductosTableAdapter mostrar_Ingreso_ProductosTableAdapter;
    }
}