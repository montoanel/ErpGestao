using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace ErpGestao
{
    public class DataUpdater
    {
        private readonly ConexaoBancoDeDados _conexaoBancoDeDados;

        public DataUpdater(ConexaoBancoDeDados conexaoBancoDeDados)
        {
            _conexaoBancoDeDados = conexaoBancoDeDados;
        }

        public void SaveOrUpdate(string tableName, Dictionary<string, object> data, string primaryKeyColumn, int? id = null)
        {
            _conexaoBancoDeDados.AbrirConexao();
            try
            {
                if (id.HasValue)
                {
                    // Update logic
                    UpdateData(tableName, data, primaryKeyColumn, id.Value);
                }
                else
                {
                    // Insert logic
                    InsertData(tableName, data);
                }
            }
            finally
            {
                _conexaoBancoDeDados.FecharConexao();
            }
        }

        private void InsertData(string tableName, Dictionary<string, object> data)
        {
            string columns = string.Join(", ", data.Keys);
            string values = string.Join(", ", data.Keys.Select(key => $"@{key}"));

            string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
            Console.WriteLine($"Query de Inserção: {query}");

            using (SqlConnection connection = _conexaoBancoDeDados.ObterConexao())
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    foreach (var pair in data)
                    {
                        Console.WriteLine($"Parâmetro: @{pair.Key} = {pair.Value}");
                        cmd.Parameters.AddWithValue($"@{pair.Key}", pair.Value ?? DBNull.Value);
                    }

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Linhas Afectadas: {rowsAffected}");
                }
            }
        }

        private void UpdateData(string tableName, Dictionary<string, object> data, string primaryKeyColumn, int id)
        {
            List<string> updates = new List<string>();
            foreach (var key in data.Keys)
            {
                updates.Add($"{key} = @{key}");
            }

            string updateString = string.Join(", ", updates);
            string query = $"UPDATE {tableName} SET {updateString} WHERE {primaryKeyColumn} = @Id";
            Console.WriteLine($"Query de Atualização: {query}");

            using (SqlConnection connection = _conexaoBancoDeDados.ObterConexao())
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    foreach (var pair in data)
                    {
                        Console.WriteLine($"Parâmetro: @{pair.Key} = {pair.Value}");
                        cmd.Parameters.AddWithValue($"@{pair.Key}", pair.Value ?? DBNull.Value);
                    }

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Linhas Afectadas: {rowsAffected}");
                }
            }
        }
    }
}
