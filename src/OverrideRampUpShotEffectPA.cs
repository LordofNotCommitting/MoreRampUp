using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MoreRampUp
{


    [HarmonyPatch(typeof(RampUpShotEffect))]
    public static class RampUpShotEffect_Patch
    {


        static bool RU_Stack_Per_Proc_Bool = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("RU_Stack_Per_Proc_Bool", false);

        static int RU_Duration_Set = Plugin.ConfigGeneral.ModData.GetConfigValue<int>("RU_Duration_Set", ModConfigGeneral.RU_Duration_Set_Array[0]);
        static int RU_Additional_Shot = Plugin.ConfigGeneral.ModData.GetConfigValue<int>("RU_Additional_Shot", ModConfigGeneral.RU_Additional_Shot_Array[0]);

        
        static public int temp_ap_count = 1;
        static public bool turn_skip_factor = false;
        static public bool const_factor = false;

        private const int DecrementEveryXCalls = 2;


        // Define your scale factor here (e.g., x = 3 means +4 duration added so net is +3)
        private const int DurationScaleFactor = 3;

        // 1. Hook the constructor: RampUpShotEffect(int ammoValue, int duration)
        [HarmonyPatch(MethodType.Constructor, new[] { typeof(int), typeof(int) })]
        [HarmonyPostfix]
        public static void Constructor_Postfix(RampUpShotEffect __instance)
        {

            // Originally sets Duration = duration.
            // Override to add (x + 1) to the base duration value.
            int scaledDuration = __instance.Duration + RU_Duration_Set;
            //if (scaledDuration >= 2) scaledDuration++;

            __instance.Duration = scaledDuration;
            __instance.AmmoValue = (float)Mathf.RoundToInt(__instance.AmmoValue + RU_Additional_Shot);
            //this is visual and therefore we do not need
            //__instance.OriginalDuration = scaledDuration;
        }
        private static readonly FieldInfo CreatureField = AccessTools.Field(typeof(RampUpShotEffect), "_creature");

        [HarmonyPatch(nameof(RampUpShotEffect.ProcessActionPoint))]
        [HarmonyPrefix]
        public static void ProcessActionPoint_Prefix(RampUpShotEffect __instance)
        {
            if (CreatureField.GetValue(__instance) == null)
            {
                return;
            }
            //WHY. WHY IS UI LIKE THIS
            if (__instance.Duration == 2)
            {
                __instance.Duration = 0;
            }

            //Plugin.Logger.Log("__instance.Duration:" + __instance.Duration);
            __instance.OriginalDuration = __instance.Duration;
        }

        // 2. Hook the Merge method
        [HarmonyPatch(nameof(RampUpShotEffect.Merge))]
        [HarmonyPrefix]
        public static bool Merge_Prefix(RampUpShotEffect __instance, BaseEffect other)
        {

            RampUpShotEffect rampUpShotEffect = other as RampUpShotEffect;
            if (rampUpShotEffect == null) return true;

            if (RU_Stack_Per_Proc_Bool)
            {

                // In original game logic: Duration = (base.Duration + other.Duration - 1)
                // We override the added portion to append (DurationScaleFactor + 1)

                int newDuration = __instance.Duration + (RU_Duration_Set + 1);

                __instance.Duration = newDuration;
                //this is visual and therefore we do not need
                //__instance.OriginalDuration = newDuration;
            }
            else 
            {
                __instance.Duration = (RU_Duration_Set + 2);
            }

            // Handle AmmoValue stacking from original code
            __instance.AmmoValue = (float)Mathf.RoundToInt(__instance.AmmoValue + rampUpShotEffect.AmmoValue);
            // Return false to skip original Merge logic since we completely handled it
            return false;
        }

    }



}
