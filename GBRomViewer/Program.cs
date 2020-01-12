using System.Diagnostics;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GBRomViewer
{
    public class Program
    {
        private const string outputFileName = "test.png";
        private const int tileWidthInPixels = 8;
        private const int outputImageWidthInTiles = 64;
        private const int outputImageWidthInPixels = outputImageWidthInTiles * tileWidthInPixels;

        public static void Main()
        {
            var rom = File.ReadAllBytes(@"C:\roms\gb\Pokemon - Blue Version (USA, Europe) (SGB Enhanced).gb");
            int outputImageHeightInPixels = GetOutputImageHeight(outputImageWidthInTiles, rom);

            using (var image = new Image<Rgba32>(outputImageWidthInPixels, outputImageHeightInPixels))
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

        private static int GetOutputImageHeight(int outputImageWidthInTiles, byte[] rom)
        {
            //interpreting bytes as 2bpp pixel data:
            //each consecutive pair of bytes is one 8 pixel row of data in a tile
            //16 bytes make up one tile
            //given the desired width of the output image (in tiles),
            //the height of output image in pixels = # of bytes in ROM / (16 bytes per tile * width in tiles)
            return rom.Length / (16 * outputImageWidthInTiles);
        }
    }
}
