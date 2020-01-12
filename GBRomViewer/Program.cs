using System;
using System.Diagnostics;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GBRomViewer
{
    public class Program
    {
        private static int outputImageWidthInTiles = 16;

        public static void Main(string[] args)
        {
            ParseCommandLineArgs(args, out string outFile, out string inFile);
            if (inFile == null || outFile == null) return;

            var rom = File.ReadAllBytes(inFile);
            var tiles = Tiles.LoadFrom(rom);

            //plot the tiles onto an output image using a given color palette
            var palette = GameBoyColorPalette.Dmg;
            int outputImageWidthInPixels = outputImageWidthInTiles * Tile.Width;
            int outputImageHeightInPixels = GetOutputImageHeightInTiles(outputImageWidthInTiles, rom) * Tile.Height;
            using (var image = new Image<Rgba32>(outputImageWidthInPixels, outputImageHeightInPixels))
            using (var stream = new FileStream(outFile, FileMode.OpenOrCreate))
            {
                for (int i = 0; i < tiles.Count; i++)
                {
                    int tileX = i % outputImageWidthInTiles;
                    int tileY = i / outputImageWidthInTiles;
                    tiles[i].DrawOnto(image, tileX, tileY, palette);
                }
                image.SaveAsPng(stream);
            }

            Process.Start(new ProcessStartInfo() { FileName = outFile, UseShellExecute = true });
        }

        private static int GetOutputImageHeightInTiles(int outputImageWidthInTiles, byte[] rom)
        {
            int tilesInRom = rom.Length / Tile.BytesPerTile;
            return tilesInRom / outputImageWidthInTiles;
        }

        private static void ParseCommandLineArgs(string[] args, out string outFile, out string inFile)
        {
            if (args.Length == 1)
            {
                inFile = args[0];
                outFile = Path.ChangeExtension(inFile, ".png");
            }
            else if (args.Length == 3 && args[0] == "-o")
            {
                outFile = args[1];
                inFile = args[2];
            }
            else if (args.Length == 5 && args[0] == "-o" && args[2] == "-tw" && int.TryParse(args[3], out outputImageWidthInTiles))
            {
                outFile = args[1];
                inFile = args[4];
            }
            else
            {
                outFile = inFile = null;
                Console.WriteLine("Usage: gbromviewer [-o output.png] [-tw 16] rom.gb");
            }
        }
    }
}
