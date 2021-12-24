package de.nierhain.insync.tile;

import net.minecraft.tileentity.TileEntity;
import net.minecraft.tileentity.TileEntityType;
import net.minecraftforge.registries.ObjectHolder;

public class TileScanner extends TileEntity {

    @ObjectHolder("insync:scanner")
    public static TileEntityType<TileScanner> SCANNER_TILE;

    public TileScanner() {
        super(SCANNER_TILE);
    }

}
