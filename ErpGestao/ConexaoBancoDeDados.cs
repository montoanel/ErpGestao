using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ErpGestao
{
    public class ConexaoBancoDeDados
    {
        private readonly string connectionString;
        private SqlConnection conexao;

        public ConexaoBancoDeDados()
        {
            var connectionStringSetting = ConfigurationManager.ConnectionStrings["ErpGestaoConnectionString"];
            if (connectionStringSetting == null || string.IsNullOrEmpty(connectionStringSetting.ConnectionString))
            {
                throw new Exception("A ConnectionString 'ErpGestaoConnectionString' não está configurada corretamente no arquivo de configuração.");
            }

            connectionString = connectionStringSetting.ConnectionString;
            Console.WriteLine($"ConnectionString inicializada: {connectionString}");
            conexao = new SqlConnection(connectionString);
        }

        public void ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string query, Action<SqlCommand> configureCommand)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                configureCommand?.Invoke(command);
                connection.Open();
                return command.ExecuteScalar();
            }
        }

        // Novo método para retornar DataTables
        public DataTable ExecuteQueryWithDataTable(string query, Action<SqlCommand> configureCommand)
        {
            var dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                configureCommand?.Invoke(command);
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }

        public bool AbrirConexao()
        {
            try
            {
                Console.WriteLine("Tentando abrir a conexão...");
                conexao.Open();
                Console.WriteLine("Conexão aberta com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao abrir a conexão: {ex.Message}");
                MessageBox.Show($"Erro ao abrir a conexão: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void FecharConexao()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
                Console.WriteLine("Conexão fechada.");
            }
        }

        public SqlConnection ObterConexao()
        {
            return conexao;
        }
    }
}
