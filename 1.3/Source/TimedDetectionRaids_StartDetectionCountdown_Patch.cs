using HarmonyLib;
using RimWorld;
using RimWorld.Planet;

namespace VisitableSettlements
{
    [HarmonyPatch(typeof(TimedDetectionRaids), nameof(TimedDetectionRaids.StartDetectionCountdown))]
    public static class TimedDetectionRaids_StartDetectionCountdown_Patch
    {
        public static bool Prefix(TimedDetectionRaids __instance)
        {
            if (__instance.parent is Settlement settlement && settlement.Faction.PlayerRelationKind != FactionRelationKind.Hostile)
            {
                return false;
            }
            return true;
        }
    }
}
