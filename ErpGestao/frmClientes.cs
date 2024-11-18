using System;
using System.Data;
using Microsoft.Data.SqlClient;  // Certifique-se de que este namespace está incluído
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ErpGestao
{
    public partial class frmClientes : Form
    {
        private string connectionString = "Data Source=CAIXA\\SQLEXPRESS;Initial Catalog=erpgestao;Integrated Security=True;TrustServerCertificate=True";

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
            cmbbuscarpor.SelectedIndex = 0;

            // Carregar clientes ao abrir o formulário
            CarregarClientes();
        }

        private void CarregarClientes()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM fcfo";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvClientes.DataSource = dt;
                    }
                }
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
            // Verificar se um cliente está selecionado
            if (dgvClientes.SelectedRows.Count > 0)
            {
                int clienteId = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["fcfo_codigo"].Value);
                // Abrir formulário para alterar cadastro passando o ID do cliente
                frmcadastrofcfo frmCadastro = new frmcadastrofcfo(clienteId);
                frmCadastro.ShowDialog();
                // Após fechar o formulário de cadastro, atualizar a lista de clientes
                CarregarClientes();
            }
            else
            {
                MessageBox.Show("Selecione um cliente para alterar.");
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

        private void BuscarClientes(string coluna, string valor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = string.Empty;

                    if (coluna == "fcfo_codigo" || coluna == "fcfo_endereco_numero")
                    {
                        // Para colunas numéricas
                        query = $"SELECT * FROM fcfo WHERE {coluna} = @valor";
                    }
                    else
                    {
                        // Para colunas de texto
                        query = $"SELECT * FROM fcfo WHERE {coluna} COLLATE Latin1_General_CI_AI LIKE '%' + @valor + '%' COLLATE Latin1_General_CI_AI";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Adicionar parâmetro de valor
                        if (coluna == "fcfo_codigo" || coluna == "fcfo_endereco_numero")
                        {
                            cmd.Parameters.AddWithValue("@valor", int.Parse(valor));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@valor", valor);
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvClientes.DataSource = dt;
                    }
                }
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
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "";
                    SqlCommand cmd = new SqlCommand();

                    // Detectar se a entrada é um número ou texto
                    if (int.TryParse(valor, out int numero))
                    {
                        // Buscar por número (ID ou número de casa)
                        query = "SELECT * FROM fcfo WHERE fcfo_codigo = @valor OR fcfo_endereco_numero = @valor";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@valor", numero);
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
                                "OR REPLACE(REPLACE(REPLACE(fcfo_rgie, '-', ''), '.', ''), ' ', '') LIKE '%' + @valor + '%' ";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@valor", valor);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvClientes.DataSource = dt;
                }
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
