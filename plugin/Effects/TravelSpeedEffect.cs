using System;
using System.Linq;
using SideLoader;
using UnityEngine;

namespace TravelSpeed.Effects {
	public class TravelSpeedEffect : Effect, ICustomModel {
		
		private const float DefaultSpeed = 4.0f;
		
		public Type SLTemplateModel => typeof(TravelSpeedEffectTemplate);
		public Type GameModel => typeof(TravelSpeedEffect);

		private void ResetStatus(Character character) {
			character.Speed = DefaultSpeed;
			RemoveStaminaBurnMultiplier(character);
		}
		
		private void SetStaminaBurnMultiplier(Character character, float value) {
			character.Stats.GetStat(CharacterStats.StatType.StaminaBurn).AddMultiplierStack(TravelSpeed.CUSTOM_STATUS_IDENTIFIER, value);
		}

		private void RemoveStaminaBurnMultiplier(Character character) {
			character.Stats.GetStat(CharacterStats.StatType.StaminaBurn).RemoveMultiplierStack(TravelSpeed.CUSTOM_STATUS_IDENTIFIER);
		}

		public override void ActivateLocally(Character _affectedCharacter, object[] _infos) {
			if (_affectedCharacter.EngagedCharacters.Any(it => it)) {
				ResetStatus(_affectedCharacter);
			} else {
				_affectedCharacter.Speed = DefaultSpeed * TravelSpeed.TRAVEL_SPEED_MULTIPLIER;
				SetStaminaBurnMultiplier(_affectedCharacter, TravelSpeed.STAMINA_BURN_MULTIPLIER);
			}
		}
		
		public override void StopAffectLocally(Character _affectedCharacter) {
			ResetStatus(_affectedCharacter);
		}
	}
}