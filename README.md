# Game Boy ROM Viewer

![Pokemon Red/Blue/Yellow](screenshots/pokemon_sample.png "Pokemon Red/Blue/Yellow") ![Zelda: Link's Awakening](screenshots/links_awakening_sample.png "Zelda: Link's Awakening") ![Final Fantasy Legend II](screenshots/ff_legend_2_sample.png "Final Fantasy Legend II") ![Harvest Moon 2 GBC](screenshots/harvest_moon_2_gbc_sample.png "Harvest Moon 2 GBC")

This program interprets a ROM or [VRAM dump](VRAM_dumping.md) as graphics data in the Game Boy's 2 bits-per-pixel color format and renders an output image. It was developed as part of my larger work in Game Boy emulation and development.

## Usage

```plain
gbromviewer [-o rom.png] [-tw 16] [-pal dmg] rom.gb
```

* `-o` specifies the output image
* `-tw` specifies the width of the output image in tiles (default 16)
* `-pal` specifies the palette name
  * `dmg` for the original green Game Boy
  * `mgb` for the grayscale Game Boy Pocket

(Arguments must be provided in this order, so if you want to specify the color palette you need to specify all the other arguments before it.)

## What is all the nonsense static in the image?

That's the game code! Only some parts of a ROM are graphics data, but this program can't tell which, so it displays everything.

## Why does game xyz not show the graphics I'm expecting?

The graphics may be compressed, so they aren't stored in the ROM the same way they're loaded into VRAM at runtime for displaying on the Game Boy's screen. This program isn't sophisticated enough to deal with that, but if you [dump the tile data from VRAM after it's loaded, you can load it that way.](VRAM_dumping.md)

The graphics may also not be "aligned" with the beginning of the ROM. This program assumes each 16 bytes of the ROM make up a tile, but if the graphics data is offset by some number of bytes and doesn't start at an address evenly divisible by 16, they will not be processed correctly. (TODO: add offset option for this?)

## Does it support Game Boy Color ROMs?

Kinda...! Try one and see. Some graphics load, but there may be something more to Game Boy Color tile data. I haven't gotten there yet.

## References + thanks to

* <https://www.huderlem.com/demos/gameboy2bpp.html>
* <http://imrannazar.com/GameBoy-Emulation-in-JavaScript:-Graphics>
