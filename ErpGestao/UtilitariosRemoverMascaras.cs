using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpGestao
{
    public static class UtilitariosRemoverMascaras
    {
        public static string RemoverMascara(MaskedTextBox maskedTextBox)
        {
            maskedTextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string textoSemMascara = maskedTextBox.Text;
            maskedTextBox.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            return textoSemMascara;
        }
    }
}

