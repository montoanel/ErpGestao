using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ErpGestao.Models
{
    public class Cliente
    {
        public int Codigo { get; set; }
        public char TipoPessoa { get; set; }
        public string CpfCnpj { get; set; }
        public string RgIe { get; set; }
        public char Isento { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Endereco { get; set; }
        public string EnderecoNumero { get; set; }
        public string EnderecoComplemento { get; set; }
        public string Coordenada { get; set; }
        public DateTime? DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public string NomeContato { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Email { get; set; }
        public string Instagram { get; set; }
        public byte[] Foto { get; set; }
        public byte[] QrCode { get; set; }
        public char ClienteFlag { get; set; }  // Ajustado para char
        public char? FornecedorFlag { get; set; }
        public char? FuncionarioFlag { get; set; }
        public char? MembroFlag { get; set; }
        public int IdCidade { get; set; }
        public string CidadeNome { get; set; }
        public string CidadeUf { get; set; }
    }
}








