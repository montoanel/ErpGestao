﻿using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows.Forms;
using System;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Diagnostics;
using ZXing;
using ZXing.Common;
using System.Drawing;



namespace ErpGestao
{
    public partial class frmcadastrofcfo : Form
    {
        private readonly CpfValidador cpfValidador = new CpfValidador();
        private readonly CnpjValidador cnpjValidador = new CnpjValidador();
        private System.Windows.Forms.Timer timer;

        private static int codigoCliente = 1; //variavel estatica para auto incremento

        private int clienteId;




        public frmcadastrofcfo()
        {
            InitializeComponent();
            GerarCodigoCliente();


            // Configurar propriedades de autocompletar
            cmbboxcidadefcfo.AutoCompleteMode = AutoCompleteMode.None;
            cmbboxcidadefcfo.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        public frmcadastrofcfo(int clienteId)
        {
            InitializeComponent();
            this.clienteId = clienteId;

        }


        private void GerarCodigoCliente()
        {
            txtboxcodigofcfo.Text = codigoCliente.ToString("D6"); //FORMATA o codigo para 6 digitos, exemplo: 000001
            codigoCliente++;
        }

        

        private void cmbboxcidadefcfo_TextUpdate(object sender, EventArgs e)
        {
           
        }

        
        private void label1_Click(object sender, EventArgs e)
        {
            // Lógica do evento de clique no label
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // Lógica do evento CheckedChanged para checkBox2
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            // Lógica do evento CheckedChanged para checkBox4
        }

        private void chkboxcliente_CheckedChanged(object sender, EventArgs e)
        {
            // Lógica do evento CheckedChanged para chkboxcliente
        }

        private void frmcadastrofcfo_Load(object sender, EventArgs e)
        {



            //valore default para teste

            cmbtipofcfo.Text = "Física";
            msktxtboxcpfcnpjfcfo.Text = "000.000.000-00";
            txtboxrgiefcfo.Text = "00000000";
            txtboxnomefantasiafcfo.Text = "Jõao do Chá Verde";
            txtboxrazaosocialfcfo.Text = "Jõao do Chá Verde";
            txtboxenderecofcfo.Text = "Alameda do Chá";
            txtboxnumeroenderecofcfo.Text = "420";
            txtboxcomplementoenderecofcfo.Text = " w";
            txtboxbairrofcfo.Text = "Bairro dos Magos";
            txtboxreferenciaenderecofcfo.Text = "Esquina 420";
            cmbboxcidadefcfo.Text = "Tangará da Serra";
            txtboxuffcfo.Text = "MT";
            msktxtboxcepfcfo.Text = "78303-609";
            msktxtboxdatanascimentofcfo.Text = "04/20/1420";
            msktxtboxdatacadastrofcfo.Text = "17/11/2024";
            msktxtboxtelefone1contatofcfo.Text = "(65)99999-9999";






            // Valores default combo box
            chkboxcliente.Checked = true;
            chkboxmembro.Checked = true;
            cmbtipofcfo.SelectedIndex = 0;

            // Máscara datas
            msktxtboxdatanascimentofcfo.Mask = "00/00/0000";
            msktxtboxdatacadastrofcfo.Mask = "00/00/0000";

            // Máscara CPF
            msktxtboxcpfcnpjfcfo.Mask = "000,000,000-00";

            //mascara cep
            msktxtboxcepfcfo.Mask = "00000-000";

            try
            {
                PreencherComboBoxCidades();
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar as cidades:{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PreencherComboBoxCidades()
        {
            // Obter a lista de cidades
            List<Cidade> cidades = Cidade.ObterTodasCidades(); 
            // Configurar as propriedades do ComboBox
            cmbboxcidadefcfo.DisplayMember = "NomeComEstado";
            cmbboxcidadefcfo.ValueMember = "Id";
            cmbboxcidadefcfo.DataSource = cidades;
        }

        private void cmbtipofcfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica o tipo de pessoa selecionado na ComboBox
            if (cmbtipofcfo.SelectedItem.ToString() == "Jurídica")
            {
                msktxtboxcpfcnpjfcfo.Mask = "00,000,000/0000-00"; // Máscara para CNPJ
                txtboxrazaosocialfcfo.Text = ""; // Limpa a razão social quando for pessoa jurídica
            }
            else
            {
                msktxtboxcpfcnpjfcfo.Mask = "000,000,000-00"; // Máscara para CPF
            }
        }

        private void txtboxnomefantasiafcfo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxnomefantasiafcfo_Leave(object sender, EventArgs e)
        {
            //Verifica se o ComboBox está selecionado como "Física" ou "Rural"
            if (cmbtipofcfo.SelectedItem != null && (cmbtipofcfo.SelectedItem.ToString() == "Física" || cmbtipofcfo.SelectedItem.ToString() == "Rural"))
            {
                //Define o valor do txtboxrazaosocialfcfo como o mesmo valor do txtboxnomefantasiafcfo

                txtboxrazaosocialfcfo.Text = txtboxnomefantasiafcfo.Text;
                txtboxnomecontatofcfo.Text = txtboxnomefantasiafcfo.Text;
            }
        }

        private void msktxtboxcpfcnpjfcfo_Leave(object sender, EventArgs e)
        {
            // Verifica o tipo de pessoa selecionada no ComboBox
            if (cmbtipofcfo.SelectedItem != null)
            {
                // Remove a máscara do campo
                string documento = msktxtboxcpfcnpjfcfo.Text.Replace(",", "").Replace("-", "").Replace("/", "").Replace(".", "").Trim();

                // Verifica se o campo não está vazio antes de validar
                if (!string.IsNullOrEmpty(documento))
                {
                    if (cmbtipofcfo.SelectedItem.ToString() == "Jurídica")
                    {
                        // Valida o CNPJ
                        if (!cnpjValidador.ValidarCNPJ(documento))
                        {
                            MessageBox.Show("CNPJ inválido!", "Validação de CNPJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // msktxtboxcpfcnpjfcfo.Focus(); // Volta o foco para o campo CNPJ
                        }
                    }
                    else if (cmbtipofcfo.SelectedItem.ToString() == "Física" || cmbtipofcfo.SelectedItem.ToString() == "Rural")
                    {
                        // Valida o CPF
                        if (!cpfValidador.ValidarCPF(documento))
                        {
                            MessageBox.Show("CPF inválido!", "Validação de CPF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // msktxtboxcpfcnpjfcfo.Focus(); // Volta o foco para o campo CPF
                        }
                    }
                }
            }
        }

        private void btncidadefcfo_Click(object sender, EventArgs e)
        {
            frmSelecionarCidade formSelecionarCidade = new frmSelecionarCidade();
            if (formSelecionarCidade.ShowDialog() == DialogResult.OK)
            {
                Cidade cidadeSelecionada = formSelecionarCidade.CidadeSelecionada;
                if (cidadeSelecionada != null)
                {
                    cmbboxcidadefcfo.Text = cidadeSelecionada.Nome;
                    txtboxuffcfo.Text = cidadeSelecionada.Estado;
                }
            }
        }

        // Evento DropDown do ComboBox
        private void cmbboxcidadefcfo_DropDown(object sender, EventArgs e)
        {
            if (cmbboxcidadefcfo.DataSource == null)
            {
                PreencherComboBoxCidades();
            }
        }
        private void cmbboxcidadefcfo_TextChanged(object sender, EventArgs e)
        {
            AtualizarEstadoCidade();
        }

        private void cmbboxcidadefcfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarEstadoCidade();
        }

        private void AtualizarEstadoCidade()
        {
            if (cmbboxcidadefcfo.SelectedItem is Cidade cidadeSelecionada)
            {
                txtboxuffcfo.Text = cidadeSelecionada.Estado;
            }

        }

        private void btninserirfotofcfo_Click(object sender, EventArgs e)
        {
            //verifica se ja existe uma foto adicionada

            if (pctboxfcfo.Image != null)
            {
                MessageBox.Show("Já existe uma foto selecionada. " +
                    "Remova a foto atual para inserir uma nova!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Criar um OpenFileDialog para escolher a imagem
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            // se o usuario escolher um arquivo e clicar em Ok
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                //carrega a imagem selecionada para a picturebox
                pctboxfcfo.Image = Image.FromFile(ofd.FileName);
                //ajusta o tamanho da picturebox para a imagem
                pctboxfcfo.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btncleanpicturefcfo_Click(object sender, EventArgs e)
        {
            pctboxfcfo.Image = null;

        }

        private void btntakepicturefcfo_Click(object sender, EventArgs e)
        {
            if (pctboxfcfo.Image != null)
            {
                MessageBox.Show("Já existe uma foto inserida. Por favor, clique em 'Limpar' antes de tirar uma nova foto.");
                return;
            }
            using (WebcamCaptureForm webcamCaptureForm = new WebcamCaptureForm())
            {
                webcamCaptureForm.ShowDialog();
                if (webcamCaptureForm.GetCapturedImage() != null)
                {
                    pctboxfcfo.Image = webcamCaptureForm.GetCapturedImage();
                    pctboxfcfo.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }



        }

        private void GerarQRCode()
        {
            string data = $"Pessoa: {cmbtipofcfo.Text}\n" +
                          $"CPF/CNPJ: {msktxtboxcpfcnpjfcfo.Text}\n" +
                          $"RG/IE: {txtboxrgiefcfo.Text}\n" +
                          $"Nome: {txtboxnomefantasiafcfo.Text}\n" +
                          $"Endereço: {txtboxenderecofcfo.Text}\n" +
                          $"Número: {txtboxnumeroenderecofcfo.Text}\n" +
                          $"Bairro: {txtboxbairrofcfo.Text}\n" +
                          $"Cidade: {cmbboxcidadefcfo.Text}\n" +
                          $"Estado: {txtboxuffcfo.Text}"+
                          $"Telefone: {msktxtboxtelefone1contatofcfo.Text}\n" +
                          $"E-mail: {txtboxemailfcfo.Text}";

            // Gere o QR Code usando a classe QRCodeGenerator
            Bitmap qrCode = QRCodeGenerator.GenerateQRCode(data, 250, 250); // Tamanho do QR Code
            pctqrcode.Image = qrCode;
            pctqrcode.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnGerarQRCode_Click(object sender, EventArgs e)
        {
            GerarQRCode();
        }

        private void btnimprimirfcfo_Click(object sender, EventArgs e)
        {
            string pessoa = cmbtipofcfo.Text;
            string cpfCnpj = msktxtboxcpfcnpjfcfo.Text;
            string rgIe = txtboxrgiefcfo.Text;
            string nome = txtboxnomefantasiafcfo.Text;
            string endereco = txtboxenderecofcfo.Text;
            string numero = txtboxnumeroenderecofcfo.Text;
            string bairro = txtboxbairrofcfo.Text;
            string cidade = cmbboxcidadefcfo.Text;
            string estado = txtboxuffcfo.Text;
            string telefone = msktxtboxtelefone1contatofcfo.Text;
            string email = txtboxemailfcfo.Text;



            System.Drawing.Image fotoCliente = pctboxfcfo.Image;
            System.Drawing.Image qrCodeImage = pctqrcode.Image;

            PDFGeneratorCadastroFCFO.ImprimirFrmCadastrofcfo(
                pessoa, cpfCnpj, rgIe, nome, endereco, numero, bairro, cidade, estado,telefone,email, fotoCliente, qrCodeImage);
        }

        private void btncancelarfcfo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnimprimirfcfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.I)
            {
                btnimprimirfcfo.PerformClick();//executa a ação do botao
            }
        }

        private void btncancelarfcfo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Alt && e.KeyCode == Keys.C)
            {
                btncancelarfcfo.PerformClick();//executa a acao do botao
            }
        }
    }
}

