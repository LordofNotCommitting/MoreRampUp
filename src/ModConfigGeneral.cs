using MGSC;
using ModConfigMenu.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoreRampUp
{
    public class ModConfigGeneral
    {

        // ====== combined ======
        // default, min, max value respectively
        public static int[] RU_Duration_Set_Array = new int[] { 1, 1, 30 };
        public static int[] RU_Duration_Limit_Array = new int[] { 30, 1, 30 };
        public static int[] RU_Additional_Shot_Array = new int[] { 0, 0, 10 };
        public static int[] RU_Additional_Shot_Limit_Array = new int[] { 30, 0, 100 };

        public ModConfigGeneral(string ModName, string ConfigPath)
        {
            this.ModName = ModName;
            this.ModData = new ModConfigData(ConfigPath);
            this.ModData.AddConfigHeader("Ramp Up Duration Settings", "Ramp Up Duration Settings");

            this.ModData.AddConfigValue("Ramp Up Duration Settings", "RU_Stack_Per_Proc_Bool", false, "RU Duration Stack per Proc", "When you have Ramp Up, and you proc another ramp up. Duration Stacks.");
            this.ModData.AddConfigValue("Ramp Up Duration Settings", "RU_Duration_Set", RU_Duration_Set_Array[0], RU_Duration_Set_Array[1], RU_Duration_Set_Array[2], "New RU Duration", "Set duration of Ramp Up buff length (1 Per AP).");
            this.ModData.AddConfigValue("Ramp Up Duration Settings", "RU_Duration_Limit", RU_Duration_Limit_Array[0], RU_Duration_Limit_Array[1], RU_Duration_Limit_Array[2], "Max RU Duration Limit", "Maximum limit of Ramp up Stack. Use it to limit max Ramp Up stack with stacking duration.");



            this.ModData.AddConfigHeader("Ramp Up Fire Rate Settings", "Ramp Up Fire Rate Settings");
            this.ModData.AddConfigValue("Ramp Up Fire Rate Settings", "RU_Additional_Shot", RU_Additional_Shot_Array[0], RU_Additional_Shot_Array[1], RU_Additional_Shot_Array[2], "Ramp Up Additional Shot Per Stack", "More ammo fired per 1 proc of Ramp Up.");
            this.ModData.AddConfigValue("Ramp Up Fire Rate Settings", "RU_Additional_Shot_Limit", RU_Additional_Shot_Limit_Array[0], RU_Additional_Shot_Limit_Array[1], RU_Additional_Shot_Limit_Array[2], "Max RU Fire Rate Limit", "Maximum limit of Ramp up's Fire Rate.");
            this.ModData.AddConfigValue("Ramp Up Fire Rate Settings", "RU_Additional_Shot_Duration_Sync_Bool", false, "RU Fire Rate = Duration", "<color=#f51b1b>Ignore those fire rate setups.</color> Always set the current Ramp Up's fire rate to that of Ramp Up Buff Duration.");


            this.ModData.AddConfigValue("Ramp Up Fire Rate Settings", "about_final", "<color=#f51b1b>The game must be restarted after setting then saving this config to take effect.</color>\n");
            this.ModData.RegisterModConfigData(ModName);
        }

        private string ModName;

        public ModConfigData ModData;

    }
}
