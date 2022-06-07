using HarmonyLib;
using TravelSpeed.Extensions;

namespace TravelSpeed.Patches {
	[HarmonyPatch(typeof(StatusEffect))]
	public static class StatusEffectPatches {

		[HarmonyPatch(nameof(StatusEffect.UpdateTotalData)), HarmonyPrefix]
		public static void StatusEffect_UpdateTotalData_Prefix(StatusEffect __instance, ref bool _updateDelta) {
			if (__instance.IsTimerSuspended()) {
				_updateDelta = false;
			}
		}
	}
}