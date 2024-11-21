using System;
using Microsoft.Data.SqlClient;

namespace ErpGestao
{
    public static class GeradorCodigo
    {
        private static ConexaoBancoDeDados conexaoBancoDeDados = new ConexaoBancoDeDados();

        public static string GerarNovoCodigoCliente()
        {
            int novoCodigo = ObterProximoCodigoDisponivel();
            return novoCodigo.ToString("D6");
        }

        private static int ObterProximoCodigoDisponivel()
        {
            int codigo = 1;

            string query = "SELECT MAX(fcfo_codigo) FROM fcfo";

            var resultado = conexaoBancoDeDados.ExecuteScalar(query, null);
            if (resultado != DBNull.Value)
            {
                codigo = Convert.ToInt32(resultado) + 1;
            }

            return codigo;
        }
    }
}
