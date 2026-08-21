using MGSC;
using ModConfigMenu.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoreRampUp
{
    // Token: 0x02000006 RID: 6
    public class ModConfigGeneral
    {

        // ====== combined ======
        // default, min, max value respectively
        public static int[] RU_Duration_Set_Array = new int[] { 1, 1, 30 };
        public static int[] RU_Additional_Shot_Array = new int[] { 0, 0, 10 };

        public ModConfigGeneral(string ModName, string ConfigPath)
        {
            this.ModName = ModName;
            this.ModData = new ModConfigData(ConfigPath);
            this.ModData.AddConfigHeader("General Settings", "general");

            this.ModData.AddConfigValue("general", "RU_Stack_Per_Proc_Bool", false, "RA Duration Stack per Proc", "When you have Ramp Up, and you proc another ramp up. Duration Stacks.");
            this.ModData.AddConfigValue("general", "RU_Duration_Set", RU_Duration_Set_Array[0], RU_Duration_Set_Array[1], RU_Duration_Set_Array[2], "New RU Duration", "Set duration of Ramp Up buff length (1 Per AP).");
            this.ModData.AddConfigValue("general", "RU_Additional_Shot", RU_Additional_Shot_Array[0], RU_Additional_Shot_Array[1], RU_Additional_Shot_Array[2], "Ramp Up Additional Shot Per Stack", "More ammo fired per 1 proc of Ramp Up.");

            this.ModData.AddConfigValue("general", "about_final", "<color=#f51b1b>The game must be restarted after setting then saving this config to take effect.</color>\n");
            this.ModData.RegisterModConfigData(ModName);
        }

        private string ModName;

        public ModConfigData ModData;

    }
}
