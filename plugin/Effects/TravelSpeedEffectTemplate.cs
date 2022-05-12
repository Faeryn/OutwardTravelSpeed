using System;
using SideLoader;

namespace TravelSpeed.Effects {
	public class TravelSpeedEffectTemplate : SL_Effect, ICustomModel {
		public Type SLTemplateModel => typeof(TravelSpeedEffectTemplate);
		public Type GameModel => typeof(TravelSpeedEffect);

		public override void ApplyToComponent<T>(T component) { }
		public override void SerializeEffect<T>(T effect) { }
	}
}