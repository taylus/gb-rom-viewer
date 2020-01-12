using System.Diagnostics;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GBRomViewer
{
    public class Program
    {
        private const string outputFileName = "test.png";

        public static void Main()
        {
            using (var image = new Image<Rgba32>(256, 256))
            using (var stream = new FileStream(outputFileName, FileMode.OpenOrCreate))
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        image[x, y] = new Rgba32((byte)x, (byte)y, 255);
                    }
                }

                image.SaveAsPng(stream);
            }

            Process.Start(new ProcessStartInfo() { FileName = outputFileName, UseShellExecute = true });
        }
    }
}
