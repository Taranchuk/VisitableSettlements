using EncounterFramework;
using HarmonyLib;
using RimWorld.Planet;
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
                Log.Message("Entering " + ___settlement);
                if (!Utils.TryInitiateLoadingFromPreset(___settlement))
                {
                    LongEventHandler.QueueLongEvent(delegate
                    {
                        Map orGenerateMap = GetOrGenerateMapUtility.GetOrGenerateMap(___settlement.Tile, null);
                        CaravanEnterMapUtility.Enter(caravan, orGenerateMap, CaravanEnterMode.Edge, 0, true, null);
                        orGenerateMap.GetComponent<MapComponentGeneration>().factionCells = EncounterFramework.Utils.GetFactionCells(orGenerateMap, null, orGenerateMap.listerThings.ThingsInGroup(ThingRequestGroup.BuildingArtificial), out _);
                    }, "GeneratingMapForNewEncounter", false, null, true);
                }
            }
        }
    }
}
