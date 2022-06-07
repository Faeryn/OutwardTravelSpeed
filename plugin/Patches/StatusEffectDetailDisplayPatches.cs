using HarmonyLib;
using TravelSpeed.Extensions;

namespace TravelSpeed.Patches {
	[HarmonyPatch(typeof(StatusEffectDetailDisplay))]
	public static class StatusEffectDetailDisplayPatches {

		[HarmonyPatch(nameof(StatusEffectDetailDisplay.RefreshDisplay)), HarmonyPostfix]
		public static void StatusEffectDetailDisplay_RefreshDisplay_Postfix(StatusEffectDetailDisplay __instance) {
			if (__instance.m_statusEffect != null && __instance.m_statusEffect.IsTimerSuspended() && __instance.m_lblTimer) {
				__instance.m_lblTimer.text = "Suspended";
			}
		}
	}
}