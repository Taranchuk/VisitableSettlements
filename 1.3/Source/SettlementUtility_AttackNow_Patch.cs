using HarmonyLib;
using RimWorld.Planet;

namespace VisitableSettlements
{
    [HarmonyPatch(typeof(SettlementUtility), "AttackNow")]
    public static class SettlementUtility_AttackNow_Patch
    {
        public static void Prefix(Caravan caravan, Settlement settlement)
        {
            Utils.TryInitiateLoadingFromPreset(settlement);
        }
    }
}
