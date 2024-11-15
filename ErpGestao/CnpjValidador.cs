using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ErpGestao
{
    public class CnpjValidador
    {
        public bool ValidarCNPJ(string cnpj)
        {
            // Remove caracteres não numéricos
            cnpj = Regex.Replace(cnpj, @"[^0-9]", "");

            // Verifica se o tamanho é 14
            if (cnpj.Length != 14)
                return false;

            // Verifica se todos os dígitos são iguais (isso torna o CNPJ inválido)
            bool todosDigitosIguais = true;
            for (int i = 1; i < cnpj.Length && todosDigitosIguais; i++)
                if (cnpj[i] != cnpj[0])
                    todosDigitosIguais = false;

            if (todosDigitosIguais)
                return false;

            // Calcula os dígitos verificadores
            int[] pesos1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] pesos2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma1 = 0, soma2 = 0;
            for (int i = 0; i < 12; i++)
            {
                soma1 += (cnpj[i] - '0') * pesos1[i];
                soma2 += (cnpj[i] - '0') * pesos2[i];
            }

            int digito1 = soma1 % 11 < 2 ? 0 : 11 - (soma1 % 11);
            soma2 += digito1 * pesos2[12];
            int digito2 = soma2 % 11 < 2 ? 0 : 11 - (soma2 % 11);

            // Verifica se os dígitos verificadores são válidos
            return cnpj[12] - '0' == digito1 && cnpj[13] - '0' == digito2;
        }
    }
}
