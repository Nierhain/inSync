package de.nierhain.inSync.entities;

import de.nierhain.inSync.blocks.ScannerBlock;
import de.nierhain.inSync.menus.ScannerMenu;
import de.nierhain.inSync.registry.ModBlockEntities;
import de.nierhain.inSync.util.CONSTANTS;
import net.minecraft.core.BlockPos;
import net.minecraft.core.Direction;
import net.minecraft.nbt.CompoundTag;
import net.minecraft.network.chat.Component;
import net.minecraft.world.Containers;
import net.minecraft.world.MenuProvider;
import net.minecraft.world.SimpleContainer;
import net.minecraft.world.entity.player.Inventory;
import net.minecraft.world.entity.player.Player;
import net.minecraft.world.inventory.AbstractContainerMenu;
import net.minecraft.world.inventory.ContainerData;
import net.minecraft.world.item.ItemStack;
import net.minecraft.world.level.Level;
import net.minecraft.world.level.block.entity.BlockEntity;
import net.minecraft.world.level.block.state.BlockState;
import net.minecraftforge.common.capabilities.Capability;
import net.minecraftforge.common.capabilities.ForgeCapabilities;
import net.minecraftforge.common.util.LazyOptional;
import net.minecraftforge.energy.EnergyStorage;
import net.minecraftforge.energy.IEnergyStorage;
import net.minecraftforge.items.IItemHandler;
import net.minecraftforge.items.ItemStackHandler;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;

import java.util.ArrayList;
import java.util.HashMap;

public class ScannerBlockEntity extends BlockEntity implements MenuProvider {
    private final ItemStackHandler itemHandler = new ItemStackHandler(1) {
        @Override
        protected void onContentsChanged(int slot) {
            setChanged();
        }
    };

    private LazyOptional<IItemHandler> lazyItemHandler = LazyOptional.empty();

    private final IEnergyStorage energyStorage = new EnergyStorage(CONSTANTS.POWER_CAPACITY);
    private LazyOptional<IEnergyStorage> lazyEnergy = LazyOptional.empty();

    protected final ContainerData data;
    private int progress = 0;
    private HashMap<String, Integer> items = new HashMap<String, Integer>();


    public ScannerBlockEntity(BlockPos pos, BlockState state) {
        super(ModBlockEntities.SCANNER.get(), pos, state);
        this.data = new ContainerData(){

            @Override
            public int get(int pIndex) {
                if(pIndex == 0) return ScannerBlockEntity.this.progress;
                if(pIndex == 1) return ScannerBlockEntity.this.energyStorage.getEnergyStored();
                return 0;
            }

            @Override
            public void set(int pIndex, int pValue) {
                if(pIndex == 0) ScannerBlockEntity.this.progress = pValue;
                if(pIndex == 1) ScannerBlockEntity.this.energyStorage.receiveEnergy(pValue, false);
            }

            @Override
            public int getCount() {
                return 2;
            }
        };
    }

    @Override
    public Component getDisplayName() {
        return Component.empty();
    }

    @Nullable
    @Override
    public AbstractContainerMenu createMenu(int id, Inventory inventory, Player player) {
        return new ScannerMenu(id, inventory, this, this.data);
    }

    @Override
    public @NotNull <T> LazyOptional<T> getCapability(@NotNull Capability<T> cap, @Nullable Direction side) {
        if(cap == ForgeCapabilities.ITEM_HANDLER) {
            return lazyItemHandler.cast();
        }
        if(cap == ForgeCapabilities.ENERGY){
            return lazyEnergy.cast();
        }

        return super.getCapability(cap, side);
    }

    @Override
    public void load(CompoundTag tag) {
        super.load(tag);
        progress = tag.getInt(CONSTANTS.SCAN_PROGRESS);
        itemHandler.deserializeNBT(tag.getCompound(CONSTANTS.CURRENT_SCAN));
        CompoundTag itemsTag = tag.getCompound(CONSTANTS.ITEM_TAG);
        itemsTag.getAllKeys().forEach(item -> {
           items.put(item, itemsTag.getInt(item));
        });
    }

    @Override
    protected void saveAdditional(CompoundTag tag) {
        tag.put(CONSTANTS.CURRENT_SCAN, itemHandler.serializeNBT());
        tag.putInt(CONSTANTS.SCAN_PROGRESS, progress);
        CompoundTag itemsTag = new CompoundTag();
        items.forEach((item, amount) -> {
            itemsTag.putInt(item, amount);
        });
        tag.put(CONSTANTS.ITEM_TAG, itemsTag);
        super.saveAdditional(tag);
    }

    @Override
    public void onLoad() {
        super.onLoad();
        lazyItemHandler = LazyOptional.of(() -> itemHandler);
        lazyEnergy = LazyOptional.of(() -> energyStorage);
    }

    @Override
    public void invalidateCaps() {
        super.invalidateCaps();
        lazyItemHandler.invalidate();
    }

    public IEnergyStorage getEnergyStorage(){
        return energyStorage;
    }

    public void drops() {
        SimpleContainer inventory = new SimpleContainer(itemHandler.getSlots());
        for (int i = 0; i < itemHandler.getSlots(); i++) {
            inventory.setItem(i, itemHandler.getStackInSlot(i));
        }

        Containers.dropContents(this.level, this.worldPosition, inventory);
    }

    public static void tick(Level level, BlockPos pos, BlockState state, ScannerBlockEntity pEntity) {
        if(level.isClientSide()) {
            return;
        }

        if(pEntity.progress >= CONSTANTS.MAX_PROGRESS){
            pEntity.progress = 0;
            setChanged(level, pos, state);
            return;
        }

        if(pEntity.energyStorage.getEnergyStored() >= CONSTANTS.POWER_CAPACITY){
            pEntity.energyStorage.extractEnergy(CONSTANTS.POWER_CAPACITY, false);
            setChanged(level, pos, state);
        }
        System.out.println("ticking... [" + pEntity.progress + "/" + CONSTANTS.MAX_PROGRESS + "]");
        pEntity.progress++;
        pEntity.energyStorage.receiveEnergy(1000, false);
        setChanged(level, pos,state);
    }
}
