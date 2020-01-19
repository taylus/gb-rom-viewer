# Collecting and viewing VRAM dump using the BGB emulator
## Save a memory dump file
Launch [BGB](http://bgb.bircd.org/) and load a ROM, then go to File -> save memory dump...

![Saving a memory dump in BGB](screenshots/bgb_save_memory_dump.png "Saving a memory dump in BGB")

Provide a start address of 8000 and length of 1800. This is where the tile data [lives in VRAM](http://gameboy.mongenel.com/dmg/asmmemmap.html):

![Tileset start address](screenshots/bgb_save_memory_dump_start_address.png "Tileset start address") ![Tileset size](screenshots/bgb_save_memory_dump_size.png "Tileset size")

## Run `gbromviewer` on the memory dump file

```
gbromviewer tetris.tileset.dump
```

This should produce an output image matching BGB's VRAM viewer (ignoring the palette):

*Tetris tileset viewed in BGB's VRAM viewer*

![Tetris tileset viewed in BGB's VRAM viewer](screenshots/bgb_vram_viewer_tileset.png "Tetris tileset viewed in BGB's VRAM viewer") 

*Tetris tileset produced by gbromviewer (scaled 2x)*

![Tetris tileset produced by gbromviewer (scaled 2x)](screenshots/tetris.tileset_2x.png "Tetris tileset produced by gbromviewer (scaled 2x)")
