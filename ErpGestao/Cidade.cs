using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient; // Use Microsoft.Data.SqlClient using System.Windows.Forms;

namespace ErpGestao
{
    public class Cidade
    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Uf { get; set; }
        public string Nome { get; set; }
        public string Estado { get; set; }

       public string NomeComEstado => $"{Nome} - {Estado}";

        // Método para obter todas as cidades do banco de dados
        public static List<Cidade> ObterTodasCidades()
        {
            var cidades = new List<Cidade>();

            using (SqlConnection conn = new SqlConnection("Data Source=CAIXA\\SQLEXPRESS;Initial Catalog=erpgestao;Integrated Security=True;TrustServerCertificate=True"))
            {
                conn.Open();
                string query = "SELECT Id, Nome, Uf FROM cidade";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cidades.Add(new Cidade
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Estado = reader.GetString(2)
                            });
                        }
                    }
                }
            }

            return cidades;
        }
    }
}
