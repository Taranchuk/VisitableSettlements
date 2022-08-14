using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using Verse;

namespace VisitableSettlements
{
    [HarmonyPatch(typeof(SettlementDefeatUtility), nameof(SettlementDefeatUtility.CheckDefeated))]
	public static class SettlementDefeatUtility_CheckDefeated_Patch
	{
		public static bool Prefix(Settlement factionBase)
		{
			bool result = true;
			if (factionBase.HasMap && !IsDefeated(factionBase.Map, factionBase.Faction))
			{
				result = false;
			}
			return result;
		}
		private static bool IsDefeated(Map map, Faction faction)
		{
			List<Pawn> list = map.mapPawns.SpawnedPawnsInFaction(faction);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].RaceProps.Humanlike)
				{
					return false;
				}
			}
			return true;
		}
	}
}
