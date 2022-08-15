using EncounterFramework;
using HarmonyLib;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Tilemaps;
using Verse;

namespace VisitableSettlements
{
    [HarmonyPatch(typeof(CaravanArrivalAction_VisitSettlement), "Arrived")]
    public static class CaravanArrivalAction_VisitSettlement_Arrived_Patch
    {
        public static void Prefix(CaravanArrivalAction_VisitSettlement __instance, Caravan caravan, Settlement ___settlement)
        {
            if (___settlement.HasMap is false)
            {
                Utils.TryInitiateLoadingFromPreset(___settlement);
            }
        }

    }

    [HarmonyPatch(typeof(Settlement), "GetCaravanGizmos")]
	public static class Settlement_GetCaravanGizmos_Patch
	{
		public static void Postfix(Settlement __instance, ref IEnumerable<Gizmo> __result, Caravan caravan)
		{
			Command_Action command_Action = new Command_Action
			{
				icon = SettleUtility.SettleCommandTex,
				defaultLabel = "VisitSettlement".Translate(__instance.Name),
				defaultDesc = "VisitSettlementDesc".Translate(),
                action = delegate ()
                {
					Action action = delegate ()
					{
                        Map map = Current.Game.FindMap(__instance.Tile);
                        if (map == null)
                        {
                            MapParent mapParent = Find.WorldObjects.MapParentAt(__instance.Tile);
                            if (mapParent is Settlement settlement)
                            {
                                Utils.TryInitiateLoadingFromPreset(settlement);
                            }
                        }
                        Map orGenerateMap = GetOrGenerateMapUtility.GetOrGenerateMap(__instance.Tile, null);
						CaravanEnterMapUtility.Enter(caravan, orGenerateMap, CaravanEnterMode.Edge, 0, true, null);
                        Log.Message(caravan + " enters " + orGenerateMap);
                    };
					LongEventHandler.QueueLongEvent(action, "GeneratingMapForNewEncounter", false, null, true);
				}
			};
			__result = __result.AddItem(command_Action);
		}
	}
}
