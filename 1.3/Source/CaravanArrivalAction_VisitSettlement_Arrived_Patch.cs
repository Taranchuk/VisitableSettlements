using HarmonyLib;
using RimWorld.Planet;
using Verse;

namespace VisitableSettlements
{
    [HarmonyPatch(typeof(CaravanArrivalAction_VisitSettlement), "Arrived")]
	public static class CaravanArrivalAction_VisitSettlement_Arrived_Patch
	{
		public static void Postfix(CaravanArrivalAction_VisitSettlement __instance, Settlement ___settlement, Caravan caravan)
		{
			if (!___settlement.HasMap)
			{
				LongEventHandler.QueueLongEvent(delegate ()
				{
					Map orGenerateMap = GetOrGenerateMapUtility.GetOrGenerateMap(___settlement.Tile, null);
					CaravanEnterMapUtility.Enter(caravan, orGenerateMap, CaravanEnterMode.Edge, 0, true, null);
				}, "GeneratingMapForNewEncounter", false, null, true);
				return;
			}
			Map orGenerateMap2 = GetOrGenerateMapUtility.GetOrGenerateMap(___settlement.Tile, null);
			CaravanEnterMapUtility.Enter(caravan, orGenerateMap2, CaravanEnterMode.Edge, 0, true, null);
		}
	}
}
