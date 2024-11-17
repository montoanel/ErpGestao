using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System.IO;
using System.Diagnostics;
using System.Drawing;

namespace ErpGestao
{
    internal class PDFGeneratorCadastroFCFO
    {
        public static void ImprimirFrmCadastrofcfo(
            string pessoa, string cpfCnpj, string rgIe, string nome,
            string endereco, string numero, string bairro, string cidade, string estado,
            System.Drawing.Image fotoCliente, System.Drawing.Image qrCodeImage)
        {
            string tempPath = Path.GetTempPath();
            int fileIndex = 1;
            string caminhoArquivo;
            do
            {
                caminhoArquivo = Path.Combine(tempPath, $"CadastroCliente_{DateTime.Now:yyyyMMdd}_{fileIndex}.pdf");
                fileIndex++;
            } while (File.Exists(caminhoArquivo));

            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(242.65f, 153.53f);

            iTextSharp.text.Document doc = new iTextSharp.text.Document(pageSize, 10f, 10f, 10f, 10f);
            iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(caminhoArquivo, FileMode.Create));
            doc.Open();

            var titulo = new iTextSharp.text.Paragraph("Membro Canábico", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD));
            titulo.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            doc.Add(titulo);

            // Adicionar imagem do cliente e QR Code do lado esquerdo, com a foto em cima e o QR Code embaixo
iTextSharp.text.pdf.PdfPTable imageTable = new iTextSharp.text.pdf.PdfPTable(1);
imageTable.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT; // Alinhar a tabela à esquerda

if (fotoCliente != null)
{
    var imageCliente = iTextSharp.text.Image.GetInstance(ImageToByteArray(fotoCliente));
    imageCliente.ScaleToFit(30f, 30f); // Ajustar o tamanho da imagem
    iTextSharp.text.pdf.PdfPCell imageCell = new iTextSharp.text.pdf.PdfPCell(imageCliente)
    {
        Border = iTextSharp.text.Rectangle.NO_BORDER,
        HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, // Alinhar a imagem à esquerda
        PaddingBottom = 2f
    };
    imageTable.AddCell(imageCell);
}

if (qrCodeImage != null)
{
    var imageQRCode = iTextSharp.text.Image.GetInstance(ImageToByteArray(qrCodeImage));
    imageQRCode.ScaleToFit(30f, 30f); // Ajustar o tamanho do QR Code
    iTextSharp.text.pdf.PdfPCell qrCodeCell = new iTextSharp.text.pdf.PdfPCell(imageQRCode)
    {
        Border = iTextSharp.text.Rectangle.NO_BORDER,
        HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT // Alinhar o QR Code à esquerda
    };
    imageTable.AddCell(qrCodeCell);
}

iTextSharp.text.pdf.PdfPTable mainTable = new iTextSharp.text.pdf.PdfPTable(2);
mainTable.WidthPercentage = 100;
mainTable.SetWidths(new float[] { 1f, 3f }); // Ajustar as larguras relativas das colunas

mainTable.AddCell(imageTable);

// Adicionar dados pessoais
iTextSharp.text.pdf.PdfPTable dataTable = new iTextSharp.text.pdf.PdfPTable(2);
dataTable.WidthPercentage = 100; // Usar largura completa disponível

AddCellToTable(dataTable, "Pessoa:", pessoa);
AddCellToTable(dataTable, "CPF/CNPJ:", cpfCnpj);
AddCellToTable(dataTable, "RG/IE:", rgIe);
AddCellToTable(dataTable, "Nome:", nome);
AddCellToTable(dataTable, "Endereço:", endereco + ", Nº " + numero);
AddCellToTable(dataTable, "Bairro:", bairro);
AddCellToTable(dataTable, "Cidade:", cidade);
AddCellToTable(dataTable, "Estado:", estado);

iTextSharp.text.pdf.PdfPCell dataCell = new iTextSharp.text.pdf.PdfPCell(dataTable)
{
    Border = iTextSharp.text.Rectangle.NO_BORDER,
    HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, // Alinhar a célula à esquerda
    PaddingLeft = 5f // Adicionar espaçamento para afastar da borda esquerda
};

mainTable.AddCell(dataCell);

doc.Add(mainTable);

            doc.Close();

            try
            {
                Process.Start(new ProcessStartInfo(caminhoArquivo) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Erro ao abrir o PDF: {ex.Message}");
            }
        }

        private static void AddCellToTable(iTextSharp.text.pdf.PdfPTable table, string header, string content)
        {
            var headerFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD);
            var contentFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL);

            iTextSharp.text.pdf.PdfPCell headerCell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(header, headerFont))
            {
                Border = iTextSharp.text.Rectangle.NO_BORDER,
                PaddingBottom = 1f
            };
            iTextSharp.text.pdf.PdfPCell contentCell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(content, contentFont))
            {
                Border = iTextSharp.text.Rectangle.NO_BORDER,
                PaddingBottom = 1f
            };

            table.AddCell(headerCell);
            table.AddCell(contentCell);
        }

        private static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
