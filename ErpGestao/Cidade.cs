using System;
using System.Collections.Generic;

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
            var cidadeDataAccess = new CidadeDataAccess();

            string query = @"
                SELECT c.id, c.codigo, c.nome, c.uf, e.nome AS estado
                FROM cidade c
                JOIN estado e ON c.estadoid = e.id";

            return cidadeDataAccess.ObterCidades(query, (cmd) =>
            {
                // Configurações adicionais no comando podem ser feitas aqui, se necessário
            });
        }
    }
}
