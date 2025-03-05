using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnippingToolOcr;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Drawing;
using System.Drawing.Imaging;

namespace SnippingToolOcrTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isDataUrl = false;

            var ocr = new SnippingToolOcr.Ocr();
            if (!ocr.IsAvailable)
            {
                Console.Error.WriteLine("Snipping Tool OCR is not available.");
            }
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: SnippingToolOcrTest <image file path or data url>");
                return;
            }

            Line[] lines;

            if (args[0].StartsWith("data:image/")){
                lines = ocr.DataUrlToText(args[0]);
                isDataUrl = true;
            }
            else
            {
                lines = ocr.ConvertToText(args[0]);
            }
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine($"{i}: {lines[i]}");
            }

            // Output in JSON format
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(lines, options);
            Console.WriteLine(json);

            // Save the result as an image
            if (!isDataUrl)
            {
                SaveResultImage(args[0], lines);
            }

            return;
        }

        static void SaveResultImage(string imagePath, Line[] lines)
        {
            // Load the image
            using (var img = new System.Drawing.Bitmap(imagePath))
            {
                // Convert the image format to BGRA (also handles cases where rotation information is included in JPEG images, etc.)
                using (Bitmap imgRgba = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb))
                {
                    using (Graphics g = Graphics.FromImage(imgRgba))
                    {
                        g.DrawImage(img, 0, 0);
                    }
                    using (var g = System.Drawing.Graphics.FromImage(imgRgba))
                    {
                        Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red, 2);
                        Font font = new System.Drawing.Font("Arial", 30);

                        foreach (var line in lines)
                        {
                            var points = new System.Drawing.PointF[]
                            {
                            new System.Drawing.PointF(line.X1, line.Y1),
                            new System.Drawing.PointF(line.X2, line.Y2),
                            new System.Drawing.PointF(line.X3, line.Y3),
                            new System.Drawing.PointF(line.X4, line.Y4)
                            };
                            g.DrawPolygon(pen, points);
                            g.DrawString(line.Text, font, System.Drawing.Brushes.Blue, points[0]);
                        }

                        pen.Dispose();
                        font.Dispose();
                    }
                    imgRgba.Save(imagePath + "_result.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }
    }
}

