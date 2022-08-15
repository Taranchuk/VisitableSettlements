using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Linq;
using Verse;

namespace VisitableSettlements
{
    [HarmonyPatch(typeof(Faction), "TryAffectGoodwillWith")]
    public static class Faction_TryAffectGoodwillWith_Patch
    {
        public static void Prefix(Faction __instance, Faction other, out bool __state)
        {
            __state = false;
            if (__instance.IsPlayer || other.IsPlayer)
            {
                if (!__instance.HostileTo(other))
                {
                    __state = true;
                }
            }
        }

        public static void Postfix(Faction __instance, Faction other, bool __state)
        {
            if (__instance.IsPlayer || other.IsPlayer)
            {
                if (__state && __instance.HostileTo(other))
                {
                    var faction = __instance != Faction.OfPlayer ? __instance : other;
                    foreach (var settlement in Find.World.worldObjects.Settlements.Where(x => x.HasMap && x.Faction == faction))
                    {
                        settlement.GetComponent<TimedDetectionRaids>().StartDetectionCountdown(240000);
                    }
                }
            }
        }
    }
}
