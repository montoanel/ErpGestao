using System;
using System.Data;
using System.Windows.Forms;

namespace ErpGestao
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            // Configurar ComboBox Buscar Por
            cmbbuscarpor.Items.AddRange(new object[] { "fcfo_codigo", "fcfo_nome_fantasia", "fcfo_endereco", "fcfo_cpfcnpj" });
            cmbbuscarpor.SelectedIndex = 0;

            // Buscar clientes ao carregar o formulário
            BuscarClientes();
        }

        private void btnnovofcfo_Click(object sender, EventArgs e)
        {
            // Abrir formulário para cadastrar novo cliente frmcadastrofcfo
            frmcadastrofcfo frmCadastro = new frmcadastrofcfo();
            frmCadastro.ShowDialog();
            // Após fechar o formulário de cadastro, atualizar a lista de clientes
            BuscarClientes();
        }

        private void btnalterarfcfo_Click(object sender, EventArgs e)
        {
            // Verificar se um cliente está selecionado
            if (dgvClientes.SelectedRows.Count > 0)
            {
                int clienteId = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["fcfo_codigo"].Value);
                // Abrir formulário para alterar cadastro passando o ID do cliente
                frmcadastrofcfo frmCadastro = new frmcadastrofcfo(clienteId);
                frmCadastro.ShowDialog();
                // Após fechar o formulário de cadastro, atualizar a lista de clientes
                BuscarClientes();
            }
            else
            {
                MessageBox.Show("Selecione um cliente para alterar.");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Realizar busca com base no critério e valor informados
            BuscarClientes();
        }

        private void BuscarClientes()
        {
            // Esta função deve buscar os clientes no banco de dados com base no critério e valor informados
            string criterio = cmbbuscarpor.SelectedItem.ToString();
            string valor = txtBuscar.Text.Trim();

            // Exemplo de preenchimento do DataGridView com dados fictícios
            DataTable dtClientes = new DataTable();
            dtClientes.Columns.Add("fcfo_codigo", typeof(int));
            dtClientes.Columns.Add("fcfo_nome_fantasia", typeof(string));
            dtClientes.Columns.Add("fcfo_endereco", typeof(string));
            dtClientes.Columns.Add("fcfo_cpfcnpj", typeof(string));

            // Adicionar dados fictícios
            dtClientes.Rows.Add(1, "João do Chá Verde", "Rua Exemplo, 123", "123.456.789-00");
            dtClientes.Rows.Add(2, "Maria das Couves", "Av. Principal, 456", "987.654.321-00");

            // Filtrar os dados fictícios (simulando a busca no banco de dados)
            DataRow[] filteredRows;

            if (!string.IsNullOrEmpty(valor))
            {
                if (criterio == "fcfo_codigo")
                {
                    int codigo;
                    if (int.TryParse(valor, out codigo))
                    {
                        filteredRows = dtClientes.Select($"{criterio} = {codigo}");
                    }
                    else
                    {
                        MessageBox.Show("Por favor, insira um valor válido para o código.");
                        return;
                    }
                }
                else
                {
                    filteredRows = dtClientes.Select($"{criterio} LIKE '%{valor}%'");
                }
            }
            else
            {
                filteredRows = dtClientes.Select();
            }

            // Atualizar DataGridView com os dados filtrados
            dgvClientes.DataSource = filteredRows.Length > 0 ? filteredRows.CopyToDataTable() : dtClientes.Clone();
        }


    }
}
