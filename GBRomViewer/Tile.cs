using System;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GBRomViewer
{
    public class Tile
    {
        public const int BytesPerTile = 16;
        public const int Width = 8;
        public const int Height = 8;

        //4-color palette index numbers (0-3)
        public int[] Pixels { get; } = new int[Width * Height];

        int this[int x, int y]
        {
            get => Pixels[x * Width + y];
            set => Pixels[x * Width + y] = value;
        }

        /// <summary>
        /// Load a tile from 16 bytes @ 2 bits per pixel using the Game Boy's
        /// format in which each pair of bytes are the low + high bits of a tile row.
        /// </summary>
        public Tile(byte[] bytes)
        {
            if (bytes.Length != BytesPerTile)
                throw new ArgumentException($"Tile data must be {BytesPerTile} bytes.");

            //parse bytes into pixels one row (two bytes) at a time
            for (int y = 0; y < Height; y++)
            {
                byte lowByte = bytes[2 * y];
                byte highByte = bytes[2 * y + 1];

                //each pixel in each row is the corresponding bit of the above bytes combined, e.g.
                //pixel 0 is the MSB of the high byte and the MSB of the low byte,
                //pixel 1 is the 6th bit of the high byte and the 6th bit of the low byte, ...
                for (int x = 0; x < Width; x++)
                {
                    int highBit = (highByte & (0b10000000 >> x)) > 0? 1 : 0;
                    int lowBit = (lowByte & (0b10000000 >> x)) > 0 ? 1 : 0;
                    this[x, y] = 2 * highBit + lowBit;
                }
            }
        }

        public void DrawOnto(Image<Rgba32> image, int tileX, int tileY, GameBoyColorPalette palette)
        {
            var colors = palette.ToRgba32().ToArray();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var paletteIndex = this[x, y];
                    var color = colors[paletteIndex];
                    image[(tileX * Tile.Width) + x, (tileY * Tile.Height) + y] = color;
                }
            }
        }
    }
}
