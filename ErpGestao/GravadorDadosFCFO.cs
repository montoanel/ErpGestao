using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using Microsoft.Data.SqlClient;

namespace ErpGestao
{
    public class GravadorDadosFCFO
    {
        public static void GravarDados(
            string codigo, string nomeFantasia, string razaoSocial, string cpfCnpj, string rgIe,
            string endereco, string enderecoNumero, string enderecoComplemento, string idCidade,
            string coordenada, string dataNascimento, string dataCadastro, string nomeContato,
            string telefone1, string telefone2, string email, string instagram, Image foto, Image qrCode)
        {
            ConexaoBancoDeDados conexaoBancoDeDados = new ConexaoBancoDeDados();

            string query = @"
                INSERT INTO fcfo 
                (fcfo_codigo, fcfo_nome_fantasia, fcfo_razao_social, fcfo_cpfcnpj, fcfo_rgie, 
                 fcfo_endereco, fcfo_endereco_numero, fcfo_endereco_complemento, fcfo_id_cidade, 
                 fcfo_coordenada, fcfo_data_nascimento, fcfo_data_cadastro, fcfo_nome_contato, 
                 fcfo_telefone1, fcfo_telefone2, fcfo_email, fcfo_instagram, fcfo_foto, fcfo_qrcode, 
                 fcfo_cliente, fcfo_fornecedor, fcfo_funcionario, fcfo_membro) 
                VALUES 
                (@Codigo, @NomeFantasia, @RazaoSocial, @CpfCnpj, @RgIe, 
                 @Endereco, @EnderecoNumero, @EnderecoComplemento, @IdCidade, 
                 @Coordenada, @DataNascimento, @DataCadastro, @NomeContato, 
                 @Telefone1, @Telefone2, @Email, @Instagram, @Foto, @QrCode, 
                 @ClienteFlag, @FornecedorFlag, @FuncionarioFlag, @MembroFlag)";

            using (SqlConnection conn = conexaoBancoDeDados.ObterConexao())
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conexaoBancoDeDados.AbrirConexao();
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Codigo", int.Parse(codigo));
                    cmd.Parameters.AddWithValue("@NomeFantasia", nomeFantasia);
                    cmd.Parameters.AddWithValue("@RazaoSocial", razaoSocial);
                    cmd.Parameters.AddWithValue("@CpfCnpj", cpfCnpj);
                    cmd.Parameters.AddWithValue("@RgIe", rgIe);
                    cmd.Parameters.AddWithValue("@Endereco", endereco);
                    cmd.Parameters.AddWithValue("@EnderecoNumero", enderecoNumero);
                    cmd.Parameters.AddWithValue("@EnderecoComplemento", enderecoComplemento);
                    cmd.Parameters.AddWithValue("@IdCidade", int.Parse(idCidade));  // Utilizando diretamente o ID da cidade
                    cmd.Parameters.AddWithValue("@Coordenada", coordenada);
                    cmd.Parameters.AddWithValue("@DataNascimento", DateTime.Parse(dataNascimento));
                    cmd.Parameters.AddWithValue("@DataCadastro", DateTime.Parse(dataCadastro));
                    cmd.Parameters.AddWithValue("@NomeContato", nomeContato);
                    cmd.Parameters.AddWithValue("@Telefone1", telefone1);
                    cmd.Parameters.AddWithValue("@Telefone2", telefone2);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Instagram", instagram);
                    cmd.Parameters.AddWithValue("@Foto", foto != null ? ImageToByteArray(foto) : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@QrCode", qrCode != null ? ImageToByteArray(qrCode) : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ClienteFlag", 'S');
                    cmd.Parameters.AddWithValue("@FornecedorFlag", 'N');
                    cmd.Parameters.AddWithValue("@FuncionarioFlag", 'N');
                    cmd.Parameters.AddWithValue("@MembroFlag", 'N');
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Adiciona um log para a exceção
                    Console.WriteLine($"Erro ao gravar os dados: {ex.Message}");
                    throw;
                }
                finally
                {
                    conexaoBancoDeDados.FecharConexao();
                }
            }
        }

        private static byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
