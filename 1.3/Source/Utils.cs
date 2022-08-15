using EncounterFramework;
using RimWorld.Planet;
using System.IO;
using Verse;

namespace VisitableSettlements
{
    public static class Utils
	{
        public static string GetPath(string basePath, int visitedTick)
        {
            return Path.Combine(Path.Combine(GenFilePaths.ConfigFolderPath, "PersistentBases"),
                                        basePath + "_" + visitedTick + ".xml");
        }
        public static void SaveSettlement(Map map, Settlement settlement)
        {
            var saveEverything = new ContentSaver_SaveEverything();
            var basePath = Find.World.info.name + "_" + settlement.Name + "_" + map.Tile;
            var visitedTick = Find.TickManager.TicksGame;
            var path = GetPath(basePath, visitedTick);
            saveEverything.SaveAt(path, map);
            var comp = Current.Game.GetComponent<GameComponent_VisitedSettlements>();
            comp.visitedSettlementsWithPaths[settlement] = basePath;
            comp.visitedSettlementsWithTicks[settlement] = visitedTick;
        }

        public static bool TryInitiateLoadingFromPreset(Settlement ___settlement)
        {
            var comp = Current.Game.GetComponent<GameComponent_VisitedSettlements>();
            if (comp.visitedSettlementsWithPaths.TryGetValue(___settlement, out var basePath))
            {
                if (comp.visitedSettlementsWithTicks.TryGetValue(___settlement, out var visitedTick))
                {
                    var path = GetPath(basePath, visitedTick);
                    GenerationContext.locationData = new LocationData(GetLocationDef(___settlement, path), 
                        new FileInfo(Path.GetFullPath(path)), ___settlement);
                    return true;
                }
            }
            return false;
        }

        private static LocationDef GetLocationDef(Settlement ___settlement, string path)
        {
            return new LocationDef
            {
                defName = ___settlement.Name,
                factionBase = ___settlement.Faction.def,
                filePreset = path,
                despawnEverythingOnTheMapBeforeGeneration = true
            };
        }

        public static bool BelongsToAnotherFaction(this Thing item)
        {
            var map = item.MapHeld;
            if (map?.ParentFaction != null)
            {
                var comp = map.GetComponent<MapComponentGeneration>();
                if (comp.factionCells.Contains(item.Position))
                {
                    return true;
                }
            }
            return false;
        }


    }
}
