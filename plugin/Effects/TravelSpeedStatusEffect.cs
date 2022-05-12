using SideLoader;

namespace TravelSpeed.Effects {
	public class TravelSpeedStatusEffect : SL_StatusEffect {
		public TravelSpeedStatusEffect() {
			TargetStatusIdentifier = "Speed Up";
			NewStatusID = -12000;
			StatusIdentifier = TravelSpeed.CUSTOM_STATUS_IDENTIFIER;
			Name = "Travel Speed";
			Description = "Move faster when not in combat";
			Purgeable = true;
			DisplayedInHUD = true;
			IsMalusEffect = false;
			Lifespan = 300;
			RefreshRate = 1f;
			AmplifiedStatusIdentifier = string.Empty;
			FamilyMode = StatusEffect.FamilyModes.Bind;
			EffectBehaviour = EditBehaviours.Destroy;
			Effects = new SL_EffectTransform[] {
				new SL_EffectTransform {
					TransformName = "Effects",
					Effects = new SL_Effect[] {
						new TravelSpeedEffectTemplate()
					}
				}
			};
		}
	}
}