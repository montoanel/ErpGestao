namespace ErpGestao
{
    partial class frmClientes
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
            btnnovofcfo = new Button();
            btnalterarfcfo = new Button();
            cmbbuscarpor = new ComboBox();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            dgvClientes = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            // 
            // btnnovofcfo
            // 
            btnnovofcfo.Location = new Point(12, 12);
            btnnovofcfo.Name = "btnnovofcfo";
            btnnovofcfo.Size = new Size(112, 34);
            btnnovofcfo.TabIndex = 0;
            btnnovofcfo.Text = "Novo";
            btnnovofcfo.UseVisualStyleBackColor = true;
            btnnovofcfo.Click += btnnovofcfo_Click;
            // 
            // btnalterarfcfo
            // 
            btnalterarfcfo.Location = new Point(130, 12);
            btnalterarfcfo.Name = "btnalterarfcfo";
            btnalterarfcfo.Size = new Size(112, 34);
            btnalterarfcfo.TabIndex = 1;
            btnalterarfcfo.Text = "Alterar";
            btnalterarfcfo.UseVisualStyleBackColor = true;
            btnalterarfcfo.Click += btnalterarfcfo_Click;
            // 
            // cmbbuscarpor
            // 
            cmbbuscarpor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbbuscarpor.FormattingEnabled = true;
            cmbbuscarpor.Location = new Point(12, 52);
            cmbbuscarpor.Name = "cmbbuscarpor";
            cmbbuscarpor.Size = new Size(156, 33);
            cmbbuscarpor.TabIndex = 2;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(174, 54);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(275, 31);
            txtBuscar.TabIndex = 3;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(455, 54);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(140, 33);
            btnBuscar.TabIndex = 4;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // dgvClientes
            // 
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(12, 102);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.RowHeadersWidth = 62;
            dgvClientes.Size = new Size(1900, 553);
            dgvClientes.TabIndex = 5;
            // 
            // frmClientes
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1898, 712);
            Controls.Add(dgvClientes);
            Controls.Add(btnBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(cmbbuscarpor);
            Controls.Add(btnalterarfcfo);
            Controls.Add(btnnovofcfo);
            MaximizeBox = false;
            Name = "frmClientes";
            Text = "Clientes";
            Load += frmClientes_Load;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnnovofcfo;
        private Button btnalterarfcfo;
        private ComboBox cmbbuscarpor;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private DataGridView dgvClientes;
    }
}