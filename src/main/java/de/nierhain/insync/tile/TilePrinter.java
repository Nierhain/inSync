package de.nierhain.insync.tile;

import net.minecraft.tileentity.TileEntity;
import net.minecraft.tileentity.TileEntityType;
import net.minecraftforge.registries.ObjectHolder;

public class TilePrinter extends TileEntity {

    @ObjectHolder("insync:printer")
    public static TileEntityType<TilePrinter> PRINTER_TILE;
    public TilePrinter() {
        super(PRINTER_TILE);
    }
}
