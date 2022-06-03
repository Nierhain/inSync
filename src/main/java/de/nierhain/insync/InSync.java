package de.nierhain.insync;


import de.nierhain.insync.registry.Registration;
import net.minecraftforge.common.MinecraftForge;
import net.minecraftforge.fml.common.Mod;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

@Mod("insync")
public class InSync {
    public static final String MODID = "insync";

    private static final Logger LOGGER = LogManager.getLogger();

    public InSync() {
        Registration.Init();
        MinecraftForge.EVENT_BUS.register(this);
    }

}
