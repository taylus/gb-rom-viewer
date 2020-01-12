using System.Diagnostics;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GBRomViewer
{
    public class Program
    {
        //TODO: load from command-line args
        private const string outputFileName = "output.png";
        private const int outputImageWidthInTiles = 16;
        private const int outputImageWidthInPixels = outputImageWidthInTiles * Tile.Width;
        private const string inputRom = @"C:\roms\gb\Pokemon - Blue Version (USA, Europe) (SGB Enhanced).gb";

        public static void Main()
        {
            var rom = File.ReadAllBytes(inputRom);
            var tiles = Tiles.LoadFrom(rom);

            //plot the tiles onto an output image using a given color palette
            var palette = GameBoyColorPalette.Dmg;
            int outputImageHeightInPixels = GetOutputImageHeightInTiles(outputImageWidthInTiles, rom) * Tile.Height;
            using (var image = new Image<Rgba32>(outputImageWidthInPixels, outputImageHeightInPixels))
            using (var stream = new FileStream(outputFileName, FileMode.OpenOrCreate))
            {
                for (int i = 0; i < tiles.Count; i++)
                {
                    int tileX = i % outputImageWidthInTiles;
                    int tileY = i / outputImageWidthInTiles;
                    tiles[i].DrawOnto(image, tileX, tileY, palette);
                }
                image.SaveAsPng(stream);
            }

            Process.Start(new ProcessStartInfo() { FileName = outputFileName, UseShellExecute = true });
        }

        private static int GetOutputImageHeightInTiles(int outputImageWidthInTiles, byte[] rom)
        {
            int tilesInRom = rom.Length / Tile.BytesPerTile;
            return tilesInRom / outputImageWidthInTiles;
        }
    }
}
