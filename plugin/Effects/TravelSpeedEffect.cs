using System;
using System.Linq;
using SideLoader;
using UnityEngine;

namespace TravelSpeed.Effects {
	public class TravelSpeedEffect : Effect, ICustomModel {
		
		private const float DefaultSpeed = 4.0f;
		
		public Type SLTemplateModel => typeof(TravelSpeedEffectTemplate);
		public Type GameModel => typeof(TravelSpeedEffect);

		public override void ActivateLocally(Character _affectedCharacter, object[] _infos) {
			if (_affectedCharacter.EngagedCharacters.Any(it => it)) {
				_affectedCharacter.Speed = DefaultSpeed;
			} else {
				_affectedCharacter.Speed = DefaultSpeed * TravelSpeed.TRAVEL_SPEED_MULTIPLIER;
			}
		}
		
		public override void StopAffectLocally(Character _affectedCharacter) {
			_affectedCharacter.Speed = DefaultSpeed;
		}
	}
}