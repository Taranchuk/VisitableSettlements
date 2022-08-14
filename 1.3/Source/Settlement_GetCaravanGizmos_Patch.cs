using HarmonyLib;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
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
				defaultLabel = Translator.Translate("VisitSettlement"),
				defaultDesc = Translator.Translate("VisitSettlementDesc"),
				action = delegate ()
				{
					Action action = delegate ()
					{
						Map orGenerateMap = GetOrGenerateMapUtility.GetOrGenerateMap(__instance.Tile, null);
						CaravanEnterMapUtility.Enter(caravan, orGenerateMap, CaravanEnterMode.Edge, 0, true, null);
					};
					LongEventHandler.QueueLongEvent(action, "GeneratingMapForNewEncounter", false, null, true);
				}
			};
			__result = __result.AddItem(command_Action);
		}
	}
}
