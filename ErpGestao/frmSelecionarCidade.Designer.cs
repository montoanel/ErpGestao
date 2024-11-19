
namespace ErpGestao
{
    partial class frmSelecionarCidade
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
            listBoxCidades = new ListBox();
            btnselecionar = new Button();
            lblbuscarcodade = new Label();
            cmbfiltrocidades = new ComboBox();
            btncancelar = new Button();
            btnfiltrarcidade = new Button();
            txtboxfiltrarcidade = new TextBox();
            SuspendLayout();
            // 
            // listBoxCidades
            // 
            listBoxCidades.FormattingEnabled = true;
            listBoxCidades.ItemHeight = 25;
            listBoxCidades.Location = new Point(12, 50);
            listBoxCidades.Name = "listBoxCidades";
            listBoxCidades.Size = new Size(541, 354);
            listBoxCidades.TabIndex = 0;
            // 
            // btnselecionar
            // 
            btnselecionar.Location = new Point(125, 410);
            btnselecionar.Name = "btnselecionar";
            btnselecionar.Size = new Size(119, 48);
            btnselecionar.TabIndex = 1;
            btnselecionar.Text = "Selecionar";
            btnselecionar.UseVisualStyleBackColor = true;
            // 
            // lblbuscarcodade
            // 
            lblbuscarcodade.AutoSize = true;
            lblbuscarcodade.Location = new Point(12, 12);
            lblbuscarcodade.Name = "lblbuscarcodade";
            lblbuscarcodade.Size = new Size(63, 25);
            lblbuscarcodade.TabIndex = 2;
            lblbuscarcodade.Text = "Buscar";
            // 
            // cmbfiltrocidades
            // 
            cmbfiltrocidades.FormattingEnabled = true;
            cmbfiltrocidades.Location = new Point(81, 12);
            cmbfiltrocidades.Name = "cmbfiltrocidades";
            cmbfiltrocidades.Size = new Size(146, 33);
            cmbfiltrocidades.TabIndex = 3;
            // 
            // btncancelar
            // 
            btncancelar.Location = new Point(326, 410);
            btncancelar.Name = "btncancelar";
            btncancelar.Size = new Size(119, 48);
            btncancelar.TabIndex = 4;
            btncancelar.Text = "Cancelar";
            btncancelar.UseVisualStyleBackColor = true;
            btncancelar.Click += btncancelar_Click;
            // 
            // btnfiltrarcidade
            // 
            btnfiltrarcidade.Location = new Point(468, 12);
            btnfiltrarcidade.Name = "btnfiltrarcidade";
            btnfiltrarcidade.Size = new Size(85, 33);
            btnfiltrarcidade.TabIndex = 5;
            btnfiltrarcidade.Text = "Filtrar";
            btnfiltrarcidade.UseVisualStyleBackColor = true;
            // 
            // txtboxfiltrarcidade
            // 
            txtboxfiltrarcidade.Location = new Point(233, 12);
            txtboxfiltrarcidade.Name = "txtboxfiltrarcidade";
            txtboxfiltrarcidade.Size = new Size(229, 31);
            txtboxfiltrarcidade.TabIndex = 6;
            // 
            // frmSelecionarCidade
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 464);
            Controls.Add(txtboxfiltrarcidade);
            Controls.Add(btnfiltrarcidade);
            Controls.Add(btncancelar);
            Controls.Add(cmbfiltrocidades);
            Controls.Add(lblbuscarcodade);
            Controls.Add(btnselecionar);
            Controls.Add(listBoxCidades);
            Name = "frmSelecionarCidade";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lista de Cidades";
            Load += frmSelecionarCidade_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void frmSelecionarCidade_Load(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        #endregion

        private ListBox listBoxCidades;
        private Button btnselecionar;
        private Label lblbuscarcodade;
        private ComboBox cmbfiltrocidades;
        private Button btncancelar;
        private Button btnfiltrarcidade;
        private TextBox txtboxfiltrarcidade;
    }
}