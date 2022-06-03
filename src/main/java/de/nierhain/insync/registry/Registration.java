package de.nierhain.insync.registry;

import de.nierhain.insync.blocks.BlockPrinter;
import de.nierhain.insync.blocks.BlockScanner;
import de.nierhain.insync.entities.ContainerPrinter;
import de.nierhain.insync.entities.ContainerScanner;
import de.nierhain.insync.entities.EntityPrinter;
import de.nierhain.insync.entities.EntityScanner;
import net.minecraft.world.inventory.MenuType;
import net.minecraft.world.item.BlockItem;
import net.minecraft.world.item.Item;
import net.minecraft.world.level.block.Block;
import net.minecraft.world.level.block.entity.BlockEntity;
import net.minecraft.world.level.block.entity.BlockEntityType;
import net.minecraftforge.common.extensions.IForgeMenuType;
import net.minecraftforge.eventbus.api.IEventBus;
import net.minecraftforge.fml.javafmlmod.FMLJavaModLoadingContext;
import net.minecraftforge.registries.DeferredRegister;
import net.minecraftforge.registries.ForgeRegistries;
import net.minecraftforge.registries.RegistryObject;

import static de.nierhain.insync.InSync.MODID;

public class Registration {

    private static final DeferredRegister<Block> BLOCKS = DeferredRegister.create(ForgeRegistries.BLOCKS, MODID);
    private static final DeferredRegister<BlockEntityType<?>> BLOCK_ENTITIES = DeferredRegister.create(ForgeRegistries.BLOCK_ENTITIES, MODID);
    private static final DeferredRegister<MenuType<?>> CONTAINERS = DeferredRegister.create(ForgeRegistries.CONTAINERS, MODID);
    private static final DeferredRegister<Item> ITEMS = DeferredRegister.create(ForgeRegistries.ITEMS, MODID);

    public static final Item.Properties ITEM_PROPERTIES = new Item.Properties().tab(ModSetup.ITEM_GROUP);
    public static void Init(){
        IEventBus bus = FMLJavaModLoadingContext.get().getModEventBus();
        BLOCK_ENTITIES.register(bus);
        CONTAINERS.register(bus);
        BLOCKS.register(bus);
    }

    public static final RegistryObject<Block> SCANNER = BLOCKS.register("scanner", BlockScanner::new);
    public static final RegistryObject<Item> SCANNER_ITEM = fromBlock(SCANNER);
    public static final RegistryObject<BlockEntityType<EntityScanner>> SCANNER_ENTITY = BLOCK_ENTITIES.register("scanner", () -> BlockEntityType.Builder.of(EntityScanner::new, SCANNER.get()).build(null));
    public static final RegistryObject<MenuType<ContainerScanner>> SCANNER_CONTAINER = CONTAINERS.register("scanner",
            () -> IForgeMenuType.create((windowId, inv, data) -> new ContainerScanner(windowId, data.readBlockPos(), inv, inv.player)));
    public static final RegistryObject<Block> PRINTER = BLOCKS.register("printer", BlockPrinter::new);
    public static final RegistryObject<BlockEntityType<EntityPrinter>> PRINTER_ENTITY = BLOCK_ENTITIES.register("printer", () -> BlockEntityType.Builder.of(EntityPrinter::new, PRINTER.get()).build(null));
    public static final RegistryObject<Item> PRINTER_ITEM = fromBlock(PRINTER);
    public static final RegistryObject<MenuType<ContainerPrinter>> PRINTER_CONTAINER = CONTAINERS.register("printer",
            () -> IForgeMenuType.create((windowId, inv, data) -> new ContainerPrinter(windowId, data.readBlockPos(), inv, inv.player)));
    public static <B extends Block> RegistryObject<Item> fromBlock(RegistryObject<B> block) {
        return ITEMS.register(block.getId().getPath(), () -> new BlockItem(block.get(), ITEM_PROPERTIES));
    }
}
