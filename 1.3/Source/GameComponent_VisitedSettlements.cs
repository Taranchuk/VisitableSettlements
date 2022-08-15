using RimWorld.Planet;
using System.Collections.Generic;
using Verse;

namespace VisitableSettlements
{
    public class GameComponent_VisitedSettlements : GameComponent
    {
        public Dictionary<Settlement, string> visitedSettlementsWithPaths = new Dictionary<Settlement, string>();
        public Dictionary<Settlement, int> visitedSettlementsWithTicks = new Dictionary<Settlement, int>();
        public GameComponent_VisitedSettlements(Game game)
        {
            PreInit();
        }

        public void PreInit()
        {
            visitedSettlementsWithPaths ??= new Dictionary<Settlement, string>();
            visitedSettlementsWithTicks ??= new Dictionary<Settlement, int>();
        }

        public override void StartedNewGame()
        {
            base.StartedNewGame();
            PreInit();
        }
        public override void LoadedGame()
        {
            base.LoadedGame();
            PreInit();
        }
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref visitedSettlementsWithPaths, "visitedSettlements", LookMode.Reference, LookMode.Value, ref settlements, ref settlementNames);
            Scribe_Collections.Look(ref visitedSettlementsWithTicks, "visitedSettlementsWithTicks", LookMode.Reference, LookMode.Value, ref settlements2, ref visitedTicks);
            PreInit();
        }

        public List<Settlement> settlements = new List<Settlement>();
		public List<string> settlementNames = new List<string>();
        public List<Settlement> settlements2 = new List<Settlement>();
        public List<int> visitedTicks = new List<int>();
    }
}
