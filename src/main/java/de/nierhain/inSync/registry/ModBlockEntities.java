package de.nierhain.inSync.registry;

import de.nierhain.inSync.entities.ScannerBlockEntity;
import de.nierhain.inSync.inSync;
import net.minecraft.world.level.block.entity.BlockEntityType;
import net.minecraftforge.eventbus.api.IEventBus;
import net.minecraftforge.registries.DeferredRegister;
import net.minecraftforge.registries.ForgeRegistries;
import net.minecraftforge.registries.RegistryObject;

public class ModBlockEntities {
    public static final DeferredRegister<BlockEntityType<?>> BLOCK_ENTITIES =
            DeferredRegister.create(ForgeRegistries.BLOCK_ENTITY_TYPES, inSync.MODID);

    public static final RegistryObject<BlockEntityType<ScannerBlockEntity>> SCANNER =
            BLOCK_ENTITIES.register("scanner", () ->
                    BlockEntityType.Builder.of(ScannerBlockEntity::new,
                            ModBlocks.SCANNER.get()).build(null));


    public static void register(IEventBus eventBus) {
        BLOCK_ENTITIES.register(eventBus);
    }
}
