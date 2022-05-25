using SideLoader;

namespace TravelSpeed.Items {
	public class TravelPotion : SL_Item {
		public TravelPotion() {
			Target_ItemID = 4300130;
			New_ItemID = -12000;
			Name = "Travel Potion";
			Description = "Potion that makes you move faster when not in combat";
			StatsHolder = new SL_ItemStats {
				BaseValue = 25,
				RawWeight = 0.3f
			};
			EffectBehaviour = EditBehaviours.Destroy;
			EffectTransforms = new SL_EffectTransform[] {
				new SL_EffectTransform {
					TransformName = "Effects",
					Effects = new SL_Effect[] {
						new SL_AddStatusEffect {
							StatusEffect = Constants.CUSTOM_STATUS_IDENTIFIER
						}
					}
				}
			};
		}
	}
}