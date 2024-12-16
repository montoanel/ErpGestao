using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace ErpGestao
{
    public class CidadeDataAccess
    {
        private readonly ConexaoBancoDeDados conexaoBancoDeDados;

        public CidadeDataAccess()
        {
            conexaoBancoDeDados = new ConexaoBancoDeDados();
        }

        public List<Cidade> ObterCidades(string query, Action<SqlCommand> configureCommand)
        {
            var cidades = new List<Cidade>();
            using (SqlConnection connection = new SqlConnection(conexaoBancoDeDados.ObterConexao().ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                configureCommand?.Invoke(command);

                try
                {
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
                                Estado = reader.GetString(4)
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao obter cidades: {ex.Message}");
                    throw; // Relança a exceção para que possamos ver onde o problema ocorre
                }
                finally
                {
                    connection.Close();
                    Console.WriteLine("Conexão fechada.");
                }
            }
            return cidades;
        }
    }
}
