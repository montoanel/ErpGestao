using ErpGestao;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;  // Certifique-se de que este namespace está incluído
using ErpGestao.Models;

namespace ErpGestao
{
    public partial class frmClientes : Form
    {
        private ConexaoBancoDeDados conexaoBancoDeDados = new ConexaoBancoDeDados(); //instancia conexao com o banco
        private DataGridViewSelector<Cliente> clienteSelector = new DataGridViewSelector<Cliente>(); // instancia datagridview seletor

        public frmClientes()
        {
            InitializeComponent();
        }

        private Dictionary<string, string> colunaMapeada = new Dictionary<string, string>()
        {
            {"fcfo_codigo", "ID" },
            {"fcfo_nome_fantasia", "Nome Fantasia" },
            { "fcfo_razao_social", "Razão Social" },
            {"fcfo_endereco", "Endereço" },
            {"fcfo_cpfcnpj", "CPF/CNPJ" },
            {"fcfo_rgie","RG/IE" },
            {"fcfo_telefone1", "Telefone" },
            {"fcfo_email", "E-mail" }
        };

        private void frmClientes_Load(object sender, EventArgs e)
        {
            // Configurar ComboBox Buscar Por com rótulos amigáveis e opção automática
            cmbbuscarpor.Items.Add("Automática");
            foreach (var item in colunaMapeada)
            {
                cmbbuscarpor.Items.Add(item.Value);
            }
            cmbbuscarpor.SelectedIndex = 2;

            // Carregar clientes ao abrir o formulário
            CarregarClientes();
        }

        private void CarregarClientes()
        {
            try
            {
                string query = @"
            SELECT 
                f.fcfo_codigo, f.fcfo_tipo_pessoa, f.fcfo_cpfcnpj, f.fcfo_rgie, f.fcfo_isento,
                f.fcfo_nome_fantasia, f.fcfo_razao_social, f.fcfo_endereco, f.fcfo_endereco_numero,
                f.fcfo_endereco_complemento, f.fcfo_coordenada, f.fcfo_data_nascimento, f.fcfo_data_cadastro,
                f.fcfo_nome_contato, f.fcfo_telefone1, f.fcfo_telefone2, f.fcfo_email, f.fcfo_instagram,
                f.fcfo_foto, f.fcfo_qrcode, f.fcfo_cliente, f.fcfo_fornecedor, f.fcfo_funcionario,
                f.fcfo_membro, f.fcfo_id_cidade, c.id as fcfo_id_cidade, c.nome AS cidade_nome, c.uf AS cidade_uf
            FROM 
                fcfo f
            LEFT JOIN 
                cidade c ON f.fcfo_id_cidade = c.id
            ORDER BY f.fcfo_codigo";  // Ordenar pelo ID do cliente para garantir que todos são carregados

                var dataTable = conexaoBancoDeDados.ExecuteQueryWithDataTable(query, null);

                // Adicionar depuração para exibir o conteúdo de cada linha
                Console.WriteLine("Dados carregados do banco de dados:");
                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine($"fcfo_codigo: {row["fcfo_codigo"]}, fcfo_tipo_pessoa: {row["fcfo_tipo_pessoa"]}, fcfo_isento: {row["fcfo_isento"]}, fcfo_cliente: {row["fcfo_cliente"]}, fcfo_fornecedor: {row["fcfo_fornecedor"]}, fcfo_funcionario: {row["fcfo_funcionario"]}, fcfo_membro: {row["fcfo_membro"]}");
                }

                var clientes = dataTable.AsEnumerable().Select(row => new Cliente
                {
                    Codigo = row.Field<int>("fcfo_codigo"),
                    TipoPessoa = row.Field<string>("fcfo_tipo_pessoa")[0],
                    CpfCnpj = row.Field<string>("fcfo_cpfcnpj"),
                    RgIe = row.Field<string>("fcfo_rgie"),
                    Isento = row.Field<string>("fcfo_isento")[0],
                    NomeFantasia = row.Field<string>("fcfo_nome_fantasia"),
                    RazaoSocial = row.Field<string>("fcfo_razao_social"),
                    Endereco = row.Field<string>("fcfo_endereco"),
                    EnderecoNumero = row.Field<string>("fcfo_endereco_numero"),
                    EnderecoComplemento = row.Field<string>("fcfo_endereco_complemento"),
                    Coordenada = row.Field<string>("fcfo_coordenada"),
                    DataNascimento = row.Field<DateTime?>("fcfo_data_nascimento"),
                    DataCadastro = row.Field<DateTime>("fcfo_data_cadastro"),
                    NomeContato = row.Field<string>("fcfo_nome_contato"),
                    Telefone1 = row.Field<string>("fcfo_telefone1"),
                    Telefone2 = row.Field<string>("fcfo_telefone2"),
                    Email = row.Field<string>("fcfo_email"),
                    Instagram = row.Field<string>("fcfo_instagram"),
                    Foto = row.Field<byte[]>("fcfo_foto"),
                    QrCode = row.Field<byte[]>("fcfo_qrcode"),
                    ClienteFlag = row.Field<string>("fcfo_cliente")[0],
                    FornecedorFlag = row.Field<string>("fcfo_fornecedor")?.FirstOrDefault() ?? '\0',
                    FuncionarioFlag = row.Field<string>("fcfo_funcionario")?.FirstOrDefault() ?? '\0',
                   MembroFlag = row.Field<string>("fcfo_membro")?.FirstOrDefault() ?? '\0',
                    IdCidade = row.Field<int>("fcfo_id_cidade"),
                    CidadeNome = row.Field<string>("cidade_nome"),
                    CidadeUf = row.Field<string>("cidade_uf")
                }).ToList();

                dgvClientes.DataSource = clientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar clientes: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnnovofcfo_Click(object sender, EventArgs e)
        {
            // Abrir formulário para cadastrar novo cliente frmcadastrofcfo
            frmcadastrofcfo frmCadastro = new frmcadastrofcfo();
            frmCadastro.ShowDialog();
            // Após fechar o formulário de cadastro, atualizar a lista de clientes
            CarregarClientes();
        }

        private void btnalterarfcfo_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente clienteSelecionado = clienteSelector.Selecionar(dgvClientes);

                if (clienteSelecionado != null)
                {
                    int clienteId = clienteSelecionado.Codigo;

                    if (conexaoBancoDeDados.AbrirConexao())
                    {
                        frmcadastrofcfo frmCadastro = new frmcadastrofcfo(clienteId, conexaoBancoDeDados);
                        frmCadastro.CarregarDadosCliente(clienteId);  // Chama o método para carregar os dados do cliente
                        frmCadastro.ShowDialog();

                        conexaoBancoDeDados.FecharConexao();
                        CarregarClientes();
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível conectar ao banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um cliente para alterar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao selecionar cliente: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Realizar busca com base no critério e valor informados
            string valor = txtBuscar.Text.Trim();
            string selectedColumn = cmbbuscarpor.SelectedItem.ToString();
            if (selectedColumn == "Automática")
            {
                BuscarClientesAutomaticamente(valor);
            }
            else
            {
                string columnName = colunaMapeada.FirstOrDefault(x => x.Value == selectedColumn).Key;
                BuscarClientes(columnName, valor);
            }
        }
        //testar
        private void BuscarClientes(string coluna, string valor)
        {
            try
            {
                string query = string.Empty;

                if (coluna == "fcfo_codigo" || coluna == "fcfo_endereco_numero")
                {
                    // Para colunas numéricas
                    query = $@"
                SELECT 
                    f.fcfo_codigo, f.fcfo_tipo_pessoa, f.fcfo_cpfcnpj, f.fcfo_rgie, f.fcfo_isento,
                    f.fcfo_nome_fantasia, f.fcfo_razao_social, f.fcfo_endereco, f.fcfo_endereco_numero,
                    f.fcfo_endereco_complemento, f.fcfo_coordenada, f.fcfo_data_nascimento, f.fcfo_data_cadastro,
                    f.fcfo_nome_contato, f.fcfo_telefone1, f.fcfo_telefone2, f.fcfo_email, f.fcfo_instagram,
                    f.fcfo_foto, f.fcfo_qrcode, f.fcfo_cliente, f.fcfo_fornecedor, f.fcfo_funcionario,
                    f.fcfo_membro, f.fcfo_id_cidade, c.id as fcfo_id_cidade, c.nome AS cidade_nome, c.uf AS cidade_uf
                FROM 
                    fcfo f
                LEFT JOIN 
                    cidade c ON f.fcfo_id_cidade = c.id
                WHERE {coluna} = @valor
                ORDER BY f.fcfo_codigo";
                }
                else
                {
                    // Para colunas de texto
                    query = $@"
                SELECT 
                    f.fcfo_codigo, f.fcfo_tipo_pessoa, f.fcfo_cpfcnpj, f.fcfo_rgie, f.fcfo_isento,
                    f.fcfo_nome_fantasia, f.fcfo_razao_social, f.fcfo_endereco, f.fcfo_endereco_numero,
                    f.fcfo_endereco_complemento, f.fcfo_coordenada, f.fcfo_data_nascimento, f.fcfo_data_cadastro,
                    f.fcfo_nome_contato, f.fcfo_telefone1, f.fcfo_telefone2, f.fcfo_email, f.fcfo_instagram,
                    f.fcfo_foto, f.fcfo_qrcode, f.fcfo_cliente, f.fcfo_fornecedor, f.fcfo_funcionario,
                    f.fcfo_membro, f.fcfo_id_cidade, c.id as fcfo_id_cidade, c.nome AS cidade_nome, c.uf AS cidade_uf
                FROM 
                    fcfo f
                LEFT JOIN 
                    cidade c ON f.fcfo_id_cidade = c.id
                WHERE {coluna} COLLATE Latin1_General_CI_AI LIKE '%' + @valor + '%' COLLATE Latin1_General_CI_AI
                ORDER BY f.fcfo_codigo";
                }

                var dataTable = conexaoBancoDeDados.ExecuteQueryWithDataTable(query, (cmd) =>
                {
                    if (coluna == "fcfo_codigo" || coluna == "fcfo_endereco_numero")
                    {
                        cmd.Parameters.AddWithValue("@valor", int.Parse(valor));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@valor", valor);
                    }
                });

                var clientes = dataTable.AsEnumerable().Select(row => new Cliente
                {
                    Codigo = row.Field<int>("fcfo_codigo"),
                    TipoPessoa = row.Field<string>("fcfo_tipo_pessoa")[0],
                    CpfCnpj = row.Field<string>("fcfo_cpfcnpj"),
                    RgIe = row.Field<string>("fcfo_rgie"),
                    Isento = row.Field<string>("fcfo_isento")[0],
                    NomeFantasia = row.Field<string>("fcfo_nome_fantasia"),
                    RazaoSocial = row.Field<string>("fcfo_razao_social"),
                    Endereco = row.Field<string>("fcfo_endereco"),
                    EnderecoNumero = row.Field<string>("fcfo_endereco_numero"),
                    EnderecoComplemento = row.Field<string>("fcfo_endereco_complemento"),
                    Coordenada = row.Field<string>("fcfo_coordenada"),
                    DataNascimento = row.Field<DateTime?>("fcfo_data_nascimento"),
                    DataCadastro = row.Field<DateTime>("fcfo_data_cadastro"),
                    NomeContato = row.Field<string>("fcfo_nome_contato"),
                    Telefone1 = row.Field<string>("fcfo_telefone1"),
                    Telefone2 = row.Field<string>("fcfo_telefone2"),
                    Email = row.Field<string>("fcfo_email"),
                    Instagram = row.Field<string>("fcfo_instagram"),
                    Foto = row.Field<byte[]>("fcfo_foto"),
                    QrCode = row.Field<byte[]>("fcfo_qrcode"),
                    ClienteFlag = row.Field<string>("fcfo_cliente")[0],
                    FornecedorFlag = row.Field<string>("fcfo_fornecedor")?.FirstOrDefault() ?? '\0',
                    FuncionarioFlag = row.Field<string>("fcfo_funcionario")?.FirstOrDefault() ?? '\0',
                    MembroFlag = row.Field<string>("fcfo_membro")?.FirstOrDefault() ?? '\0',
                    IdCidade = row.Field<int>("fcfo_id_cidade"),
                    CidadeNome = row.Field<string>("cidade_nome"),
                    CidadeUf = row.Field<string>("cidade_uf")
                }).ToList();

                dgvClientes.DataSource = clientes;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Erro de SQL: {ex.Message}", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void BuscarClientesAutomaticamente(string valor)
        {
            try
            {
                string query = "";
                Action<SqlCommand> configureCommand = null;

                if (int.TryParse(valor, out int numero))
                {
                    // Buscar por número (ID ou número de casa)
                    query = "SELECT * FROM fcfo WHERE fcfo_codigo = @valor OR fcfo_endereco_numero = @valor";
                    configureCommand = (cmd) => cmd.Parameters.AddWithValue("@valor", numero);
                }
                else
                {
                    // Buscar por texto (nome, endereço, etc.)
                    query = "SELECT * FROM fcfo WHERE " +
                            "REPLACE(REPLACE(REPLACE(fcfo_nome_fantasia, '-', ''), '.', ''), ' ', '') COLLATE Latin1_General_CI_AI LIKE '%' + @valor + '%' COLLATE Latin1_General_CI_AI " +
                            "OR REPLACE(REPLACE(REPLACE(fcfo_razao_social, '-', ''), '.', ''), ' ', '') COLLATE Latin1_General_CI_AI LIKE '%' + @valor + '%' COLLATE Latin1_General_CI_AI " +
                            "OR REPLACE(REPLACE(REPLACE(fcfo_endereco, '-', ''), '.', ''), ' ', '') COLLATE Latin1_General_CI_AI LIKE '%' + @valor + '%' COLLATE Latin1_General_CI_AI " +
                            "OR REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(fcfo_cpfcnpj, '-', ''), '.', ''), '/', ''), ' ', ''), '(', '') LIKE '%' + @valor + '%' " +
                            "OR REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(fcfo_telefone1, '-', ''), '(', ''), ')', ''), ' ', ''), '+', '') LIKE '%' + @valor + '%' " +
                            "OR REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(fcfo_telefone2, '-', ''), '(', ''), ')', ''), ' ', ''), '+', '') LIKE '%' + @valor + '%' " +
                            "OR REPLACE(REPLACE(REPLACE(REPLACE(fcfo_rgie, '-', ''), '.', ''), '/', ''), ' ', '') LIKE '%' + @valor + '%'";
                    configureCommand = (cmd) => cmd.Parameters.AddWithValue("@valor", valor);
                }

                var dataTable = conexaoBancoDeDados.ExecuteQueryWithDataTable(query, configureCommand);
                dgvClientes.DataSource = dataTable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Erro de SQL: {ex.Message}", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
