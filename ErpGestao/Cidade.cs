using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace ErpGestao
{
    public class Cidade
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Uf { get; set; }
        public string Nome { get; set; }
        public string Estado { get; set; } // Usaremos `Estado` para guardar o `codigouf`
        public string NomeComEstado => $"{Nome} - {Uf}";

        // Método para obter todas as cidades do banco de dados
        public static List<Cidade> ObterTodasCidades()
        {
            var cidades = new List<Cidade>();
            var conexaoBancoDeDados = new ConexaoBancoDeDados();

            string query = @"
        SELECT c.id, c.codigo, c.nome, c.uf, e.nome AS estado
        FROM cidade c
        JOIN estado e ON c.estadoid = e.id";

            cidades = conexaoBancoDeDados.ExecuteQueryWithReader(query, (cmd) =>
            {
                // Configurações adicionais no comando podem ser feitas aqui, se necessário
            });

            return cidades;
        }



    }
}
