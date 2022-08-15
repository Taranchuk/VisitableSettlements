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
    [HarmonyPatch(typeof(Settlement), "GetCaravanGizmos")]
	public static class Settlement_GetCaravanGizmos_Patch
	{
		public static void Postfix(Settlement __instance, ref IEnumerable<Gizmo> __result, Caravan caravan)
		{
			Command_Action command_Action = new Command_Action
			{
				icon = SettleUtility.SettleCommandTex,
				defaultLabel = "VisitSettlement".Translate(__instance.Name),
                action = delegate ()
                {
					Action action = delegate ()
					{
						Utils.TryEnterOrGenerateMap(caravan, __instance);
                    };
					LongEventHandler.QueueLongEvent(action, "GeneratingMapForNewEncounter", false, null, true);
				}
			};
			__result = __result.AddItem(command_Action);
		}
	}
}
