using EncounterFramework;
using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System;
using System.IO;
using Verse;

namespace VisitableSettlements
{
    [HarmonyPatch(typeof(GetOrGenerateMapUtility), nameof(GetOrGenerateMapUtility.GetOrGenerateMap), 
		new Type[] { typeof(int), typeof(IntVec3), typeof(WorldObjectDef) })]
	public static class GetOrGenerateMapUtility_GetOrGenerateMap_Patch
	{
		public static bool Prefix(ref Map __result, int tile, IntVec3 size, WorldObjectDef suggestedMapParentDef, out bool __state)
        {
            __state = false;
            Map map = Current.Game.FindMap(tile);
			if (map == null)
			{
				MapParent mapParent = Find.WorldObjects.MapParentAt(tile);
				if (mapParent is Settlement settlement)
				{
					var comp = Current.Game.GetComponent<GameComponent_VisitedSettlements>();
					if (comp.visitedSettlements.TryGetValue(settlement, out var path))
					{
						__state = true;
						GenerationContext.locationData = new LocationData(new LocationDef
						{
							defName = settlement.Name,
							factionBase = settlement.Faction.def,
							filePreset = path,
						}, new FileInfo(Path.GetFullPath(path)), settlement);
                    }
				}
			}
			return true;
		}

		public static void Postfix(Map __result, bool __state)
		{
			if (!__state && __result.Parent is Settlement settlement)
			{
				var saveEverything = new ContentSaver_SaveEverything();
				var path = Path.Combine(Path.Combine(GenFilePaths.ConfigFolderPath, "PersistentBases"),
					Find.World.info.name + "_" + settlement.Name + "_" + __result.Tile + ".xml");
				saveEverything.SaveAt(path, __result);
                var comp = Current.Game.GetComponent<GameComponent_VisitedSettlements>();
				comp.visitedSettlements[settlement] = path;
            }
        }
	}
}
