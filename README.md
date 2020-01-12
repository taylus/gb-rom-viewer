# Game Boy ROM Viewer
## View Game Boy ROMs as images

This program interprets an entire Game Boy ROM as graphics in the Game Boy's 2 bits-per-pixel color format and renders it to an image for inspection. It was developed as part of my larger work in Game Boy emulation.

## What is all the nonsense static in the image?
That's the game code! Only some of the ROM is graphics data, but this program can't tell what, so it displays everything.

## Why does game xyz not show the graphics I'm expecting?
The graphics may be compressed, so they aren't stored in the ROM the same way they're loaded into VRAM at runtime for displaying on the Game Boy's screen. This program isn't sophisticated enough to deal with that.

## Does it support Game Boy Color ROMs?
Kinda...! Try one and see. Some graphics load, but there must be something more to Game Boy Color tile data. I haven't gotten there yet.

## Sample images
![Pokemon Red/Blue/Yellow](screenshots\pokemon_sample.png "Pokemon Red/Blue/Yellow")
===
![Zelda: Link's Awakening](screenshots\links_awakening_sample.png "Zelda: Link's Awakening")
===
![Final Fantasy Legend II](screenshots\ff_legend_2_sample.png "Final Fantasy Legend II")

## References + thanks to
* https://www.huderlem.com/demos/gameboy2bpp.html
* http://imrannazar.com/GameBoy-Emulation-in-JavaScript:-Graphics