package de.nierhain.insync.entities;

import de.nierhain.insync.registry.Registration;
import net.minecraft.core.BlockPos;
import net.minecraft.world.entity.player.Inventory;
import net.minecraft.world.entity.player.Player;
import net.minecraft.world.inventory.AbstractContainerMenu;
import net.minecraft.world.inventory.MenuType;
import org.jetbrains.annotations.Nullable;

public class ContainerPrinter extends AbstractContainerMenu {

    public ContainerPrinter(int windowId, BlockPos pos, Inventory inventory, Player player) {
        super(Registration.PRINTER_CONTAINER.get(), windowId);
    }

    @Override
    public boolean stillValid(Player pPlayer) {
        return false;
    }
}
