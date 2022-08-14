using HarmonyLib;
using UnityEngine;
using Verse;
using Verse.Noise;
using static System.Net.WebRequestMethods;

namespace VisitableSettlements
{
	public class VisitableSettlementsMod : Mod
	{
		public static VisitableSettlementsSettings settings;
		public VisitableSettlementsMod(ModContentPack pack) : base(pack)
		{
			settings = GetSettings<VisitableSettlementsSettings>();
			new Harmony("VisitableSettlements.Mod").PatchAll();
		}
		public override void DoSettingsWindowContents(Rect inRect)
		{
			base.DoSettingsWindowContents(inRect);
			settings.DoSettingsWindowContents(inRect);
		}

		public override string SettingsCategory()
		{
			return this.Content.Name;
		}
	}

	public class VisitableSettlementsSettings : ModSettings
	{
		public override void ExposeData()
		{
			base.ExposeData();
		}
		public void DoSettingsWindowContents(Rect inRect)
		{
			Rect rect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
			Listing_Standard listingStandard = new Listing_Standard();
			listingStandard.Begin(rect);

			listingStandard.End();
		}
	}
}
