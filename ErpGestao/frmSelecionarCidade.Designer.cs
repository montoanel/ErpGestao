
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
            btnSelecionar = new Button();
            SuspendLayout();
            // 
            // listBoxCidades
            // 
            listBoxCidades.FormattingEnabled = true;
            listBoxCidades.ItemHeight = 25;
            listBoxCidades.Location = new Point(12, 12);
            listBoxCidades.Name = "listBoxCidades";
            listBoxCidades.Size = new Size(555, 404);
            listBoxCidades.TabIndex = 0;
            // 
            // btnSelecionar
            // 
            btnSelecionar.Location = new Point(573, 12);
            btnSelecionar.Name = "btnSelecionar";
            btnSelecionar.Size = new Size(119, 48);
            btnSelecionar.TabIndex = 1;
            btnSelecionar.Text = "Selecionar";
            btnSelecionar.UseVisualStyleBackColor = true;
            // 
            // frmSelecionarCidade
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSelecionar);
            Controls.Add(listBoxCidades);
            Name = "frmSelecionarCidade";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Lista de Cidades";
            Load += frmSelecionarCidade_Load;
            ResumeLayout(false);
        }

        private void frmSelecionarCidade_Load(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        #endregion

        private ListBox listBoxCidades;
        private Button btnSelecionar;
    }
}