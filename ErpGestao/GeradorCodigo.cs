using System;
using Microsoft.Data.SqlClient;

namespace ErpGestao
{
    public static class GeradorCodigo
    {
        public static string GerarNovoCodigoCliente()
        {
            int novoCodigo = ObterProximoCodigoDisponivel();
            return novoCodigo.ToString("D6");
        }

        private static int ObterProximoCodigoDisponivel()
        {
            int codigo = 1;
            string connectionString = "Data Source=CAIXA\\SQLEXPRESS;Initial Catalog=erpgestao;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MAX(fcfo_codigo) FROM fcfo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        codigo = Convert.ToInt32(result) + 1;
                    }
                }
            }

            return codigo;
        }
    }
}
