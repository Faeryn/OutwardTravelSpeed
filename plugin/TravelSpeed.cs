using System;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using UnityEngine;

namespace TravelSpeed {
	[BepInPlugin(GUID, NAME, VERSION)]
	public class TravelSpeed : BaseUnityPlugin {
		public const string GUID = "faeryn.travelspeed";
		public const string NAME = "TravelSpeed";
		public const string VERSION = "0.1.0";
		private const string DISPLAY_NAME = "Travel Speed";
		internal static ManualLogSource Log;

		private float DefaultSpeed = 4.0f;
		
		public static ConfigEntry<float> TravelSpeedMult;

		internal void Awake() {
			Log = this.Logger;
			Log.LogMessage($"Starting {NAME} {VERSION}");
			InitializeConfig();
		}

		private void InitializeConfig() {
			TravelSpeedMult = Config.Bind(DISPLAY_NAME, "Travel Speed Multiplier", 2f, "The character moves this many times faster than normal when out of combat");
			TravelSpeedMult.SettingChanged += (sender, args) => {
				UpdateTravelSpeed();
			};
		}

		private void Update() {
			UpdateTravelSpeed();
		}

		private void UpdateTravelSpeed() {
			Character character = GetLocalCharacter(0);
			if (character != null) {
				UpdateTravelSpeed(character);
			}
		}

		private void UpdateTravelSpeed(Character character) {
			if (character.InCombat) {
				if (!Mathf.Approximately(character.Speed, DefaultSpeed)) {
					character.Speed = DefaultSpeed;
				}
			} else {
				if (!Mathf.Approximately(character.Speed, DefaultSpeed * TravelSpeedMult.Value)) {
					character.Speed = DefaultSpeed * TravelSpeedMult.Value;
				}
			}
		}
		
		private Character GetLocalCharacter(int playerID) {
			return SplitScreenManager.Instance.LocalPlayers[playerID].AssignedCharacter;
		}
	}
}