using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErpGestao
{
    public class ValidadorFormularioFCFO
    {
        public static bool VerificarCamposObrigatorios(

                TextBox txtboxcodigofcfo,
                MaskedTextBox cpfCnpj,
                TextBox rgIe,
                TextBox nomeFantasia,
                TextBox razaoSocial,
                TextBox endereco,
                TextBox numeroEndereco,
                TextBox bairro,
                TextBox cidade,
                MaskedTextBox cep,
                MaskedTextBox dataNascimento,
                MaskedTextBox dataCadastro,
                PictureBox fotoCliente,
                PictureBox qrcodeCliente)
        {
            if (string.IsNullOrWhiteSpace(UtilitariosRemoverMascaras.RemoverMascara(cpfCnpj)))
            {
                MessageBox.Show("Por favor, preencha o CPF ou CNPJ.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cpfCnpj.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(rgIe.Text))
            {
                MessageBox.Show("Por favor, preencha o RG ou IE.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rgIe.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(nomeFantasia.Text))
            {
                MessageBox.Show("Por favor, preencha o Nome Fantasia.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nomeFantasia.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(razaoSocial.Text))
            {
                MessageBox.Show("Por favor, preencha a Razão Social.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                razaoSocial.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cidade.Text))
            {
                MessageBox.Show("Por favor, preencha a Cidade.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cidade.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(endereco.Text))
            {
                MessageBox.Show("Por favor, preencha o Endereço.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                endereco.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(numeroEndereco.Text))
            {
                MessageBox.Show("Por favor, preencha o Número do Endereço.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numeroEndereco.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(bairro.Text))
            {
                MessageBox.Show("Por favor, preencha o Bairro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bairro.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(UtilitariosRemoverMascaras.RemoverMascara(cep)))
            {
                MessageBox.Show("Por favor, preencha o CEP.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cep.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(UtilitariosRemoverMascaras.RemoverMascara(dataNascimento)))
            {
                MessageBox.Show("Por favor, preencha a Data de Nascimento.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataNascimento.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(UtilitariosRemoverMascaras.RemoverMascara(dataCadastro)))
            {
                MessageBox.Show("Por favor, preencha a Data de Cadastro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dataCadastro.Focus();
                return false;
            }
            if (fotoCliente.Image == null)
            {
                MessageBox.Show("Por favor, adicione uma Foto do Cliente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fotoCliente.Focus();
                return false;
            }
            if (qrcodeCliente.Image == null)
            {
                MessageBox.Show("Por favor, adicione um QR Code do Cliente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                qrcodeCliente.Focus();
                return false;
            }
            return true;
        }
    }
}
