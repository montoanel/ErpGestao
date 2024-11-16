namespace ErpGestao
{
    partial class WebcamCaptureForm
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
            cmbwebcamlist = new ComboBox();
            btnstartrecord = new Button();
            btnstoprecord = new Button();
            btnsavepicture = new Button();
            pctcapturar = new PictureBox();
            pctsave = new PictureBox();
            btnvoltar = new Button();
            ((System.ComponentModel.ISupportInitialize)pctcapturar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctsave).BeginInit();
            SuspendLayout();
            // 
            // cmbwebcamlist
            // 
            cmbwebcamlist.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbwebcamlist.FormattingEnabled = true;
            cmbwebcamlist.Location = new Point(30, 271);
            cmbwebcamlist.Name = "cmbwebcamlist";
            cmbwebcamlist.Size = new Size(335, 33);
            cmbwebcamlist.TabIndex = 0;
            // 
            // btnstartrecord
            // 
            btnstartrecord.Location = new Point(73, 310);
            btnstartrecord.Name = "btnstartrecord";
            btnstartrecord.Size = new Size(257, 36);
            btnstartrecord.TabIndex = 1;
            btnstartrecord.Text = "Iniciar Captura de Imagem";
            btnstartrecord.UseVisualStyleBackColor = true;
            btnstartrecord.Click += btnstartrecord_Click;
            // 
            // btnstoprecord
            // 
            btnstoprecord.Location = new Point(73, 352);
            btnstoprecord.Name = "btnstoprecord";
            btnstoprecord.Size = new Size(257, 36);
            btnstoprecord.TabIndex = 2;
            btnstoprecord.Text = "Trocar Webcam";
            btnstoprecord.UseVisualStyleBackColor = true;
            btnstoprecord.Click += btnstoprecord_Click;
            // 
            // btnsavepicture
            // 
            btnsavepicture.Location = new Point(410, 271);
            btnsavepicture.Name = "btnsavepicture";
            btnsavepicture.Size = new Size(296, 36);
            btnsavepicture.TabIndex = 3;
            btnsavepicture.Text = "Salvar";
            btnsavepicture.UseVisualStyleBackColor = true;
            btnsavepicture.Click += btnsavepicture_Click;
            // 
            // pctcapturar
            // 
            pctcapturar.BorderStyle = BorderStyle.FixedSingle;
            pctcapturar.Location = new Point(65, 55);
            pctcapturar.Name = "pctcapturar";
            pctcapturar.Size = new Size(273, 156);
            pctcapturar.SizeMode = PictureBoxSizeMode.Zoom;
            pctcapturar.TabIndex = 4;
            pctcapturar.TabStop = false;
            // 
            // pctsave
            // 
            pctsave.BorderStyle = BorderStyle.FixedSingle;
            pctsave.Location = new Point(401, 55);
            pctsave.Name = "pctsave";
            pctsave.Size = new Size(273, 156);
            pctsave.SizeMode = PictureBoxSizeMode.Zoom;
            pctsave.TabIndex = 5;
            pctsave.TabStop = false;
            // 
            // btnvoltar
            // 
            btnvoltar.Location = new Point(410, 354);
            btnvoltar.Name = "btnvoltar";
            btnvoltar.Size = new Size(296, 34);
            btnvoltar.TabIndex = 6;
            btnvoltar.Text = "Voltar ";
            btnvoltar.UseVisualStyleBackColor = true;
            btnvoltar.Click += btnvoltar_Click;
            // 
            // WebcamCaptureForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnvoltar);
            Controls.Add(pctsave);
            Controls.Add(pctcapturar);
            Controls.Add(btnsavepicture);
            Controls.Add(btnstoprecord);
            Controls.Add(btnstartrecord);
            Controls.Add(cmbwebcamlist);
            MaximizeBox = false;
            Name = "WebcamCaptureForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Captura de Imagem";
            FormClosing += WebcamCaptureForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pctcapturar).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctsave).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbwebcamlist;
        private Button btnstartrecord;
        private Button btnstoprecord;
        private Button btnsavepicture;
        private PictureBox pctcapturar;
        private PictureBox pctsave;
        private Button btnvoltar;
    }
}