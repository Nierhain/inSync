package de.nierhain.inSync.registry;

import net.minecraft.world.item.CreativeModeTab;
import net.minecraft.world.item.ItemStack;

public class ModCreativeTab {
    public static final CreativeModeTab INSYNC_TAB = new CreativeModeTab("insync") {
        @Override
        public ItemStack makeIcon() {
            return new ItemStack(ModBlocks.SCANNER.get());
        }
    };
}
