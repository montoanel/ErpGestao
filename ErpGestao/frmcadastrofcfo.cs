using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows.Forms;
using System;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Diagnostics;


namespace ErpGestao
{
    public partial class frmcadastrofcfo : Form
    {
        private readonly CpfValidador cpfValidador = new CpfValidador();
        private readonly CnpjValidador cnpjValidador = new CnpjValidador();
        private System.Windows.Forms.Timer timer;

       




        public frmcadastrofcfo()
        {
            InitializeComponent();
            //cmbboxcidadefcfo.DropDown += new System.EventHandler(this.cmbboxcidadefcfo_DropDown);
            //cmbboxcidadefcfo.TextUpdate += new System.EventHandler(this.cmbboxcidadefcfo_TextUpdate);
            //cmbboxcidadefcfo.SelectedIndexChanged += new System.EventHandler(this.cmbboxcidadefcfo_SelectedIndexChanged);
            //cmbboxcidadefcfo.TextChanged += new System.EventHandler(this.cmbboxcidadefcfo_TextChanged);

            // Configurar propriedades de autocompletar
            cmbboxcidadefcfo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbboxcidadefcfo.AutoCompleteSource = AutoCompleteSource.CustomSource;

            // Inicializar o temporizador
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // Intervalo em milissegundos (1 segundos)
            timer.Tick += new EventHandler(Timer_Tick);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop(); // Parar o temporizador para evitar execuções repetidas

            ComboBox comboBox = cmbboxcidadefcfo;
            string textoDigitado = comboBox.Text;

            // Filtrar a lista de cidades com base no texto digitado
            List<Cidade> cidadesFiltradas = Cidade.ObterTodasCidades()
                .Where(c => c.Nome.StartsWith(textoDigitado, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Configurar a lista de autocompletar
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            foreach (var cidade in cidadesFiltradas)
            {
                collection.Add($"{cidade.Nome} - {cidade.Estado}");
            }

            // Atualizar a fonte de autocompletar da ComboBox
            cmbboxcidadefcfo.AutoCompleteCustomSource = collection;

            // Limpar e adicionar itens filtrados na ComboBox
            cmbboxcidadefcfo.Items.Clear();
            foreach (var cidade in cidadesFiltradas)
            {
                cmbboxcidadefcfo.Items.Add($"{cidade.Nome} - {cidade.Estado}");
            }

            // Ajustar a seleção do cursor
            comboBox.SelectionStart = textoDigitado.Length;
        }


        private void cmbboxcidadefcfo_TextUpdate(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Start(); // Reiniciar o temporizador para atualizar a lista após um intervalo
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
            // Obter a lista de cidades
            List<Cidade> cidades = Cidade.ObterTodasCidades();

            // Limpar os itens existentes na ComboBox
            cmbboxcidadefcfo.Items.Clear();

            // Adicionar as cidades na ComboBox
            foreach (var cidade in cidades)
            {
                cmbboxcidadefcfo.Items.Add($"{cidade.Nome} - {cidade.Estado}");
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
            string textoDigitado = cmbboxcidadefcfo.Text;

            //verifica se a entrada contem um hífen Cuiaba '-'MT

            if (textoDigitado.Contains(" - "))
            {
                string[] partes = textoDigitado.Split(new string[] { " - " }, StringSplitOptions.None);
                if (partes.Length == 2)
                {
                    string nomeCidade = partes[0].Trim();
                    string estado = partes[1].Trim();

                    // Verifica se a cidade realmente existe na lista de cidades
                    Cidade cidadeSelecionada = Cidade.ObterTodasCidades().FirstOrDefault(c => c.Nome.Equals(nomeCidade, StringComparison.OrdinalIgnoreCase) && c.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase));
                    if (cidadeSelecionada != null)
                    {
                        txtboxuffcfo.Text = cidadeSelecionada.Estado;
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

       



    }
}

