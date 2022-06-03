package de.nierhain.insync.entities;

import de.nierhain.insync.registry.Registration;
import net.minecraft.core.BlockPos;
import net.minecraft.world.level.block.entity.BlockEntity;
import net.minecraft.world.level.block.entity.BlockEntityType;
import net.minecraft.world.level.block.state.BlockState;

public class EntityPrinter extends BlockEntity {
    public EntityPrinter(BlockPos pWorldPosition, BlockState pBlockState) {
        super(Registration.PRINTER_ENTITY.get(), pWorldPosition, pBlockState);
    }
}
