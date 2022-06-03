package de.nierhain.insync.blocks;


import de.nierhain.insync.entities.EntityPrinter;
import net.minecraft.core.BlockPos;
import net.minecraft.world.level.block.Block;
import net.minecraft.world.level.block.EntityBlock;
import net.minecraft.world.level.block.entity.BlockEntity;
import net.minecraft.world.level.block.state.BlockState;
import net.minecraft.world.level.material.Material;
import org.jetbrains.annotations.Nullable;

public class BlockPrinter extends Block implements EntityBlock {
    public BlockPrinter() {
        super(Properties.of(Material.DIRT));
    }

    @Nullable
    @Override
    public BlockEntity newBlockEntity(BlockPos pPos, BlockState pState) {
        return new EntityPrinter(pPos, pState);
    }
}
