using System;
using System.Linq;
using SideLoader;
using TravelSpeed.Extensions;
using UnityEngine;

namespace TravelSpeed.Effects {
	public class TravelSpeedEffect : Effect, ICustomModel {
		
		private const float DefaultSpeed = 4.0f;
		private float lastCombatTime = Time.time;
		
		public Type SLTemplateModel => typeof(TravelSpeedEffectTemplate);
		public Type GameModel => typeof(TravelSpeedEffect);

		private void ResetStatus(Character character) {
			lastCombatTime = Time.time;
			character.Speed = DefaultSpeed;
			RemoveStaminaBurnMultiplier(character);
		}

		private float GetActualSpeedMultiplier() {
			float timeRatio = Mathf.Clamp((Time.time - lastCombatTime) / Constants.TRAVEL_SPEED_TIME_TO_MAX, 0.0f, 1.0f);
			return Constants.TRAVEL_SPEED_MULTIPLIER_MIN + timeRatio * (Constants.TRAVEL_SPEED_MULTIPLIER_MAX - Constants.TRAVEL_SPEED_MULTIPLIER_MIN);
		}
		
		private void SetStaminaBurnMultiplier(Character character, float value) {
			character.Stats.GetStat(CharacterStats.StatType.StaminaBurn).AddMultiplierStack(Constants.CUSTOM_STATUS_IDENTIFIER, value);
		}

		private void RemoveStaminaBurnMultiplier(Character character) {
			character.Stats.GetStat(CharacterStats.StatType.StaminaBurn).RemoveMultiplierStack(Constants.CUSTOM_STATUS_IDENTIFIER);
		}

		public override void ActivateLocally(Character _affectedCharacter, object[] _infos) {
			if (_affectedCharacter.EngagedCharacters.Any(it => it)) {
				ResetStatus(_affectedCharacter);
				m_parentStatusEffect.SetTimerSuspended(true);
			} else {
				float speedMult = GetActualSpeedMultiplier();
				_affectedCharacter.Speed = DefaultSpeed * speedMult;
				SetStaminaBurnMultiplier(_affectedCharacter, speedMult);
				m_parentStatusEffect.SetTimerSuspended(false);
			}
		}
		
		public override void StopAffectLocally(Character _affectedCharacter) {
			ResetStatus(_affectedCharacter);
			m_parentStatusEffect.SetTimerSuspended(false);
		}
	}
}