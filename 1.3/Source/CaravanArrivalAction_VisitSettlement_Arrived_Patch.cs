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
            Utils.TryEnterOrGenerateMap(caravan, ___settlement);
        }
    }
}
