using RimWorld.Planet;
using System.Collections.Generic;
using Verse;

namespace VisitableSettlements
{
    public class GameComponent_VisitedSettlements : GameComponent
    {
		public Dictionary<Settlement, string> visitedSettlements = new Dictionary<Settlement, string>();
		public GameComponent_VisitedSettlements(Game game)
        {

        }

        public override void ExposeData()
        {
            base.ExposeData();
			Scribe_Collections.Look(ref visitedSettlements, "visitedSettlements", LookMode.Reference, LookMode.Value, ref settlements, ref settlementNames);
        }

		public List<Settlement> settlements = new List<Settlement>();
		public List<string> settlementNames = new List<string>();
    }
}
