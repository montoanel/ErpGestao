using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using System.Drawing;
using System.IO;

namespace ErpGestao
{
    internal class QRCodeGenerator
    {
        public static Bitmap GenerateQRCode(string data, int width, int height)
        {
            var qrCodeWriter = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = 1

                }
            };
            var pixelData = qrCodeWriter.Write(data);
            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                using (var ms = new MemoryStream(pixelData.Pixels))
                {
                    bitmap.SetResolution(72, 72);
                    for (int y = 0; y < pixelData.Height; y++)
                    {
                        for (int x = 0; x < pixelData.Width; x++)
                        {
                            Color pixelColor = Color.FromArgb(pixelData.Pixels[4 * (y * pixelData.Width + x) + 3],
                                                              pixelData.Pixels[4 * (y * pixelData.Width + x) + 2],
                                                              pixelData.Pixels[4 * (y * pixelData.Width + x) + 1],
                                                              pixelData.Pixels[4 * (y * pixelData.Width + x)]);
                            bitmap.SetPixel(x, y, pixelColor);
                        }
                    }
                    return new Bitmap(bitmap);
                }
            }
        }
    }
}

