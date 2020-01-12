using System.Collections.Generic;
using System.Linq;

namespace GBRomViewer
{
    public static class Tiles
    {
        /// <summary>
        /// Turn ROM data into a list of tiles by reading 16 bytes of tile data at a time.
        /// </summary>
        public static IList<Tile> LoadFrom(byte[] rom)
        {
            var tiles = new List<Tile>();

            for (int i = 0; i < rom.Length; i += Tile.BytesPerTile)
            {
                var tileBytes = rom.Skip(i).Take(Tile.BytesPerTile);
                tiles.Add(new Tile(tileBytes.ToArray()));
            }

            /*
            //test tile from https://www.huderlem.com/demos/gameboy2bpp.html
            tiles.Add(new Tile(new byte[] 
            {
                0xFF, 0x00, 0x7E, 0xFF, 0x85, 0x81, 0x89, 0x83, 
                0x93, 0x85, 0xA5, 0x8B, 0xC9, 0x97, 0x7E, 0xFF
            }));
            */

            return tiles;
        }
    }
}
