using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Policy;
using System.Diagnostics.Tracing;


namespace ErpGestao
{
    public class Cidade
    {
        public string Nome { get; set; }
        public string Estado { get; set; }

        public string NomeComEstado => $"{Nome} - {Estado}";

        public static List<Cidade> ObterTodasCidades()
        {
            return new List<Cidade>
            {
                new Cidade { Nome = "São Paulo", Estado = "SP" },
                new Cidade { Nome = "Rio de Janeiro", Estado = "RJ" },
                new Cidade { Nome = "Belo Horizonte", Estado = "MG" },
                new Cidade {Nome = "Tangará da Serra", Estado = "MT"},
                new Cidade { Nome = "Barra do Bugres", Estado = "MT"}
                // Adicione todas as outras cidades aqui
            };
        }
    }
}
