using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows.Forms;
using System;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Diagnostics;
using ZXing;
using ZXing.Common;
using System.Drawing;
using Microsoft.Data.SqlClient;



namespace ErpGestao
{
    public partial class frmcadastrofcfo : Form
    {
        //validarcpf e cnpj
        private readonly CpfValidador cpfValidador = new CpfValidador();
        private readonly CnpjValidador cnpjValidador = new CnpjValidador();
        //private System.Windows.Forms.Timer timer;

        private static int codigoCliente = 1; //variavel estatica para auto incremento

        private int clienteId;
        private ConexaoBancoDeDados conexaoBancoDeDados;





        public frmcadastrofcfo()
        {
            InitializeComponent();
            GerarCodigoCliente();


            // Configurar propriedades de autocompletar combo box cidades
            cmbboxcidadefcfo.AutoCompleteMode = AutoCompleteMode.None;
            cmbboxcidadefcfo.AutoCompleteSource = AutoCompleteSource.ListItems;

        }
        // Construtor atualizado para aceitar clienteId e conexaoBancoDeDados
        public frmcadastrofcfo(int clienteId, ConexaoBancoDeDados conexaoBancoDeDados)
        {
            InitializeComponent();
            this.clienteId = clienteId;
            this.conexaoBancoDeDados = conexaoBancoDeDados;
            CarregarDadosCliente(clienteId);  // Chama o método para carregar os dados do cliente
        }
        // Método para carregar os dados do cliente
        public void CarregarDadosCliente(int clienteId)
        {
            string query = @"
        SELECT 
            f.fcfo_codigo, f.fcfo_tipo_pessoa, f.fcfo_cpfcnpj, f.fcfo_rgie, f.fcfo_isento,
            f.fcfo_nome_fantasia, f.fcfo_razao_social, f.fcfo_endereco, f.fcfo_endereco_numero,
            f.fcfo_endereco_complemento, f.fcfo_coordenada, f.fcfo_data_nascimento, f.fcfo_data_cadastro,
            f.fcfo_nome_contato, f.fcfo_telefone1, f.fcfo_telefone2, f.fcfo_email, f.fcfo_instagram,
            f.fcfo_foto, f.fcfo_qrcode, f.fcfo_cliente, f.fcfo_fornecedor, f.fcfo_funcionario,
            f.fcfo_membro, f.fcfo_id_cidade, c.nome AS cidade_nome, c.uf AS cidade_uf
        FROM 
            fcfo f
        LEFT JOIN 
            cidade c ON f.fcfo_id_cidade = c.id
        WHERE 
            f.fcfo_codigo = @clienteId";

            SqlCommand cmd = new SqlCommand(query, conexaoBancoDeDados.ObterConexao());
            cmd.Parameters.AddWithValue("@clienteId", clienteId);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                // Carregar os dados do cliente nos controles do formulário
                //txtboxcodigofcfo.Text = reader["id_fcfo"].ToString();
                txtboxnomefantasiafcfo.Text = reader["fcfo_nome_fantasia"].ToString();
                msktxtboxcpfcnpjfcfo.Text = reader["fcfo_cpfcnpj"].ToString();
                txtboxrgiefcfo.Text = reader["fcfo_rgie"].ToString();
                txtboxenderecofcfo.Text = reader["fcfo_endereco"].ToString();
                txtboxnumeroenderecofcfo.Text = reader["fcfo_endereco_numero"].ToString();
                txtboxcomplementoenderecofcfo.Text = reader["fcfo_endereco_complemento"].ToString();
               // cmbboxcidadefcfo.Text = reader["cidade_nome"].ToString() ;
                txtboxcoordenadasfcfo.Text = reader["fcfo_coordenada"].ToString();
                msktxtboxdatanascimentofcfo.Text = reader["fcfo_data_nascimento"] != DBNull.Value ? Convert.ToDateTime(reader["fcfo_data_nascimento"]).ToString("dd/MM/yyyy") : string.Empty;
                msktxtboxdatacadastrofcfo.Text = Convert.ToDateTime(reader["fcfo_data_cadastro"]).ToString("dd/MM/yyyy");
                txtboxnomecontatofcfo.Text = reader["fcfo_nome_contato"].ToString();
                msktxtboxtelefone1contatofcfo.Text = reader["fcfo_telefone1"].ToString();
                msktxtboxtelefone2contatofcfo.Text = reader["fcfo_telefone2"].ToString();
                txtboxemailfcfo.Text = reader["fcfo_email"].ToString();
                txtboxinstagramfcfo.Text = reader["fcfo_instagram"].ToString();

                if (reader["fcfo_foto"] != DBNull.Value)
                {
                    pctboxfcfo.Image = ByteArrayToImage((byte[])reader["fcfo_foto"]);
                }
                else
                {
                    pctboxfcfo.Image = null;
                }

                if (reader["fcfo_qrcode"] != DBNull.Value)
                {
                    pctqrcode.Image = ByteArrayToImage((byte[])reader["fcfo_qrcode"]);
                }
                else
                {
                    pctqrcode.Image = null;
                }

                chkboxcliente.Checked = reader["fcfo_cliente"].ToString() == "S";
                chkboxfornecedor.Checked = reader["fcfo_fornecedor"].ToString() == "S";
                chkboxfuncionario.Checked = reader["fcfo_funcionario"].ToString() == "S";
                chkboxmembro.Checked = reader["fcfo_membro"].ToString() == "S";
                cmbboxcidadefcfo.Text = $"{reader["cidade_nome"]} - {reader["cidade_uf"]}";
            }
            reader.Close();
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0) return null;
            using (var ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
                   
                  



        private void GerarCodigoCliente()
        {
            txtboxcodigofcfo.Text = GeradorCodigo.GerarNovoCodigoCliente();
        }
             

        private void cmbboxcidadefcfo_TextUpdate(object sender, EventArgs e)
        {
           
        }

        
        private void label1_Click(object sender, EventArgs e)
        {
            // Lógica do evento de clique no label
        }

        private void chkboxcliente_CheckedChanged(object sender, EventArgs e)
        {
            // Verificar se pelo menos um CheckBox está marcado
            if (!chkboxcliente.Checked && !chkboxfornecedor.Checked && !chkboxfuncionario.Checked && !chkboxmembro.Checked) 
            { //Marcar chkboxcliente se todos estiverem desmarcados
              chkboxcliente.Checked = true; 
            }
        }

        private void frmcadastrofcfo_Load(object sender, EventArgs e)
        {

            // Valores default combo box
            chkboxcliente.Checked = true;
            chkboxmembro.Checked = true;
            cmbtipofcfo.SelectedIndex = 0;

            // Máscaras
            msktxtboxdatanascimentofcfo.Mask = "00/00/0000";
            msktxtboxdatacadastrofcfo.Mask = "00/00/0000";
            msktxtboxcpfcnpjfcfo.Mask = "000,000,000-00";
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

            // Definir o índice 0 como o selecionado por padrão
            if (cmbboxcidadefcfo.Items.Count > 0)
            {
                cmbboxcidadefcfo.SelectedIndex = 0;
            }
        }




        private void cmbtipofcfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica o tipo de pessoa selecionado na ComboBox
            if (cmbtipofcfo.SelectedItem.ToString() == "Jurídica")
            {
                msktxtboxcpfcnpjfcfo.Mask = "00,000,000/0000-00"; // Máscara para CNPJ
                txtboxrazaosocialfcfo.Text = ""; // Limpa a razão social quando for pessoa jurídica
                msktxtboxcpfcnpjfcfo.Clear();// limpa cpf/cnpj ao trocar entre pessoa fisica e juridica para nao haver erro de cadastro
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
                string documento = UtilitariosRemoverMascaras.RemoverMascara(msktxtboxcpfcnpjfcfo);


                // Verifica se o campo não está vazio antes de validar
                if (!string.IsNullOrEmpty(documento))
                {
                    if (cmbtipofcfo.SelectedItem.ToString() == "Jurídica")
                    {
                        // Valida o CNPJ
                        if (!cnpjValidador.ValidarCNPJ(documento))
                        {
                            MessageBox.Show("CNPJ inválido!", "Validação de CNPJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            msktxtboxcpfcnpjfcfo.Focus(); // Volta o foco para o campo CNPJ
                        }
                    }
                    else if (cmbtipofcfo.SelectedItem.ToString() == "Física" || cmbtipofcfo.SelectedItem.ToString() == "Rural")
                    {
                        // Valida o CPF
                        if (!cpfValidador.ValidarCPF(documento))
                        {
                            MessageBox.Show("CPF inválido!", "Validação de CPF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            msktxtboxcpfcnpjfcfo.Focus(); // Volta o foco para o campo CPF
                        }
                    }
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
                txtboxuffcfo.Text = cidadeSelecionada.Uf;
            }

        }

        private void btncidadefcfo_Click(object sender, EventArgs e)
        {
            using (var frmSelecionarCidade = new frmSelecionarCidade())
            {
                if (frmSelecionarCidade.ShowDialog() == DialogResult.OK)
                {
                    var cidadeSelecionada = frmSelecionarCidade.CidadeSelecionada;
                    if (cidadeSelecionada != null)
                    {
                        // Atualizar o ComboBox apenas com o Nome da Cidade
                        cmbboxcidadefcfo.Text = cidadeSelecionada.Nome;

                        // Atualizar o TextBox com a UF do Estado
                        txtboxuffcfo.Text = cidadeSelecionada.Uf;

                        // Redefinir o DataSource para garantir a atualização
                        cmbboxcidadefcfo.DataSource = null;
                        cmbboxcidadefcfo.DataSource = Cidade.ObterTodasCidades();
                        cmbboxcidadefcfo.DisplayMember = "NomeComEstado"; // preencher Nome - UF
                        cmbboxcidadefcfo.ValueMember = "Id";

                        // Set the selected value explicitly
                        foreach (var item in cmbboxcidadefcfo.Items)
                        {
                            if (((Cidade)item).Id == cidadeSelecionada.Id)
                            {
                                cmbboxcidadefcfo.SelectedItem = item;
                                break;
                            }
                        }

                        // Remover mensagem de depuração
                        //MessageBox.Show($"Novo valor do ComboBox: {cmbboxcidadefcfo.Text}");
                    }
                }
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
            if (ValidadorFormularioFCFO.VerificarCamposObrigatorios(
                msktxtboxcpfcnpjfcfo,
                txtboxrgiefcfo,
                txtboxnomefantasiafcfo,
                txtboxrazaosocialfcfo,
                txtboxenderecofcfo,
                txtboxnumeroenderecofcfo,
               // txtboxcomplementoenderecofcfo,
                txtboxbairrofcfo,
               // txtboxreferenciaenderecofcfo,
                cmbboxcidadefcfo,
                msktxtboxcepfcfo,
               // txtboxcoordenadasfcfo,
                msktxtboxdatanascimentofcfo,
                msktxtboxdatacadastrofcfo,
                pctboxfcfo,
                pctqrcode))
            {
                // Lógica para imprimir

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
                    pessoa, cpfCnpj, rgIe, nome, endereco, numero, bairro, cidade, estado, telefone, email, fotoCliente, qrCodeImage);
            }
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

