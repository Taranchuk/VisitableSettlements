using HarmonyLib;
using RimWorld.Planet;
using Verse;

namespace VisitableSettlements
{
    [HarmonyPatch(typeof(Game), nameof(Game.DeinitAndRemoveMap))]
	public static class Game_DeinitAndRemoveMap_Patch
	{
		public static void Prefix(Map map)
		{
            Log.Message("Removing map: " + map);
			if (map?.Parent is Settlement settlement)
			{
                Utils.SaveSettlement(map, settlement);
            }
		}
    }
}
