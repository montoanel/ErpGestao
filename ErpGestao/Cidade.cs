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
                new Cidade { Nome = "Cuiabá", Estado = "MT" },
                new Cidade { Nome = "Várzea Grande", Estado = "MT" },
                new Cidade { Nome = "Rondonópolis", Estado = "MT" },
                new Cidade { Nome = "Sinop", Estado = "MT" },
                new Cidade { Nome = "Tangará da Serra", Estado = "MT" },
                new Cidade { Nome = "Sorriso", Estado = "MT" },
                new Cidade { Nome = "Lucas do Rio Verde", Estado = "MT" },
                new Cidade { Nome = "Barra do Garças", Estado = "MT" },
                new Cidade { Nome = "Alta Floresta", Estado = "MT" },
                new Cidade { Nome = "Cáceres", Estado = "MT" },
                new Cidade { Nome = "Primavera do Leste", Estado = "MT" },
                new Cidade { Nome = "Nova Mutum", Estado = "MT" },
                new Cidade { Nome = "Barra do Bugres", Estado = "MT" },
                new Cidade { Nome = "Campo Verde", Estado = "MT" },
                new Cidade { Nome = "Pontes e Lacerda", Estado = "MT" },
                new Cidade { Nome = "Peixoto de Azevedo", Estado = "MT" },
                new Cidade { Nome = "Juína", Estado = "MT" },
                new Cidade { Nome = "Colíder", Estado = "MT" },
                new Cidade { Nome = "Jaciara", Estado = "MT" },
                new Cidade { Nome = "Guarantã do Norte", Estado = "MT" },
                new Cidade { Nome = "Diamantino", Estado = "MT" },
                new Cidade { Nome = "Juara", Estado = "MT" },
                new Cidade { Nome = "Sapezal", Estado = "MT" },
                new Cidade { Nome = "Paranatinga", Estado = "MT" },
                new Cidade { Nome = "Rosário Oeste", Estado = "MT" },
                new Cidade { Nome = "Chapada dos Guimarães", Estado = "MT" },
                new Cidade { Nome = "Matupá", Estado = "MT" },
                new Cidade { Nome = "Água Boa", Estado = "MT" },
                new Cidade { Nome = "São Paulo", Estado = "SP"}
                // Adicione mais cidades conforme necessário
            };
        }
    }
}

