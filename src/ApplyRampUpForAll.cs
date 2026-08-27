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
    [HarmonyPatch(typeof(Player), nameof(Player.OnTryHit))]
    public static class ApplyRampUpForAll
    {


        static bool RU_Apply_To_All_Bool = Plugin.ConfigGeneral.ModData.GetConfigValue<bool>("RU_Apply_To_All_Bool", false);

        public static void Prefix(bool isMeleeAttack, Player __instance)
        {
            WeaponComponent weaponComponent = __instance.CreatureData.Inventory.CurrentWeapon.Comp<WeaponComponent>();
            int ammoValue;
            if (RU_Apply_To_All_Bool && !isMeleeAttack && !ItemTraitSystem.TryGetValue<int>(weaponComponent.Traits, "IRampUpProjectiles", out ammoValue))
            {
                __instance.CreatureData.EffectsController.Add(new RampUpShotEffect(1, 2), true);
            }
        }


    }
}