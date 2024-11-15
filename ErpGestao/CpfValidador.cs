using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ErpGestao
{
    public class CpfValidador
    {
        public bool ValidarCPF(string cpf)
        {
            //remove caracteres nao numericos
            cpf = Regex.Replace(cpf, @"[^0-9]", "");

            //verifica se o tamanho do cpf é 11
            if (cpf.Length != 11)
                return false;

            //verifica se todos os digitos são iguais (isso torna o cpf invalido)
            bool todosDigitosIguais = true;
            for (int i = 1; i < cpf.Length && todosDigitosIguais; i++)
                if (cpf[i] != cpf[0])
                    todosDigitosIguais = false;

            if (todosDigitosIguais)
                return false;

            //calcula os digitos verificadores
            int[] pesos1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] pesos2 = {11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma1 = 0, soma2 = 0;
            for (int i = 0; i < 9; i++)
            {
                soma1 += (cpf[i] - '0') * pesos1[i];
                soma2 += (cpf[i] - '0') * pesos2[i];
            }
            int digito1 = soma1 % 11 < 2 ? 0 : 11 - (soma1 % 11);
            soma2 += digito1 * pesos2[9];
            int digito2 = soma2 % 11 < 2 ? 0 : 11 - (soma2 % 11);

            //verifica se os digitos verificadores sao validos
            return cpf[9] - '0' == digito1 && cpf[10] - '0' == digito2;
        }
    }
}
