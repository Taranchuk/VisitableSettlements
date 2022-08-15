using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System;
using UnityEngine;
using Verse;

namespace VisitableSettlements
{
    [HarmonyPatch(typeof(ThingOwner<Thing>), "TryAdd", new Type[]
    {
        typeof(Thing),
        typeof(bool)
    })]
    public static class ThingOwner_TryAdd_Patch
    {
        public static void Postfix(ThingOwner<Thing> __instance, bool __result, Thing item)
        {
            if (__result)
            {
                if (__instance.Owner is Pawn_ApparelTracker apparelTracker)
                {
                    TryCauseGoodwillImpactDueToLooting(item, apparelTracker.pawn.Faction);
                }
                else if (__instance.Owner is Pawn_EquipmentTracker equipmentTracker)
                {
                    TryCauseGoodwillImpactDueToLooting(item, equipmentTracker.pawn.Faction);
                }
                else if (__instance.Owner is Pawn_InventoryTracker inventoryTracker)
                {
                    TryCauseGoodwillImpactDueToLooting(item, inventoryTracker.pawn.Faction);
                }
            }

            void TryCauseGoodwillImpactDueToLooting(Thing item, Faction pawnFaction)
            {
                if (item.BelongsToAnotherFaction() && pawnFaction != null)
                {
                    item.MapHeld.ParentFaction.TryAffectGoodwillWith(pawnFaction,
                        -Mathf.RoundToInt((item.stackCount * item.MarketValue) / 10f), reason: VS_DefOf.VS_Looting);
                }
            }
        }
    }
}
