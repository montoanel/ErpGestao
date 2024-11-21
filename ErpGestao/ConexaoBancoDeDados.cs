using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ErpGestao
{
    public class ConexaoBancoDeDados
    {
        private readonly string connectionString;

        public ConexaoBancoDeDados()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ErpGestaoConnectionString"].ConnectionString;
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

        public List<Cidade> ExecuteQueryWithReader(string query, Action<SqlCommand> configureCommand)
        {
            var cidades = new List<Cidade>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                configureCommand?.Invoke(command);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cidades.Add(new Cidade
                        {
                            Id = reader.GetInt32(0),
                            Codigo = reader.GetString(1),
                            Nome = reader.GetString(2),
                            Uf = reader.GetString(3),
                            Estado = reader.GetString(4) // Certifique-se de que a coluna `Estado` é uma string no SQL
                        });
                    }
                }
            }
            return cidades;
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
    }
}
