using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Epic.OnlineServices;
using SideLoader;
using TravelSpeed.Effects;
using TravelSpeed.Items;
using UnityEngine;

namespace TravelSpeed {
	[BepInDependency(SL.GUID, BepInDependency.DependencyFlags.HardDependency)]
	[BepInPlugin(GUID, NAME, VERSION)]
	public class TravelSpeed : BaseUnityPlugin {
		public const string GUID = "faeryn.travelspeed";
		public const string NAME = "TravelSpeed";
		public const string VERSION = "1.0.1";
		
		public const string CUSTOM_STATUS_IDENTIFIER = "TravelSpeed";

		internal static ManualLogSource Log;

		public const float TRAVEL_SPEED_MULTIPLIER = 2.0f;
		

		internal void Awake() {
			Log = this.Logger;
			Log.LogMessage($"Starting {NAME} {VERSION}");
			InitializeSL();
		}

		private void InitializeSL() {
			SL.BeforePacksLoaded += SL_BeforePacksLoaded;
		}

		private void SL_BeforePacksLoaded() {
			TravelSpeedStatusEffect travelSpeedEffect = new TravelSpeedStatusEffect();
			travelSpeedEffect.ApplyTemplate();

			TravelPotion potion = new TravelPotion();
			potion.ApplyTemplate();
			
			SL_Recipe potionRecipe = new SL_Recipe {
				UID = TravelPotion.RECIPE_NAME,
				StationType = Recipe.CraftingType.Alchemy,
				Ingredients = {
					new SL_Recipe.Ingredient {
						Type = RecipeIngredient.ActionTypes.AddGenericIngredient,
						SelectorValue = "Water"
					},
					new SL_Recipe.Ingredient {
						Type = RecipeIngredient.ActionTypes.AddSpecificIngredient,
						SelectorValue = "4000230" // Bird Egg
					},
					new SL_Recipe.Ingredient {
						Type = RecipeIngredient.ActionTypes.AddSpecificIngredient,
						SelectorValue = "6400130" // Mana Stone
					}
				},
				Results = {
					new SL_Recipe.ItemQty {
						ItemID = potion.New_ItemID, 
						Quantity = 1
					}
				}
			};
			potionRecipe.ApplyTemplate();

			SL_RecipeItem potionRecipeItem = new SL_RecipeItem {
				RecipeUID = TravelPotion.RECIPE_NAME,
				Target_ItemID = 5700110,
				New_ItemID = -12500,
				Name = "Alchemy: Travel Potion"
			};
			potionRecipeItem.ApplyTemplate();

			SL_DropTable potionAndRecipeDT = new SL_DropTable {
				UID = TravelPotion.RECIPE_NAME+".droptable",
				RandomTables = {new SL_RandomDropGenerator {
					MinNumberOfDrops = 1,
					MaxNumberOfDrops = 3,
					NoDrop_DiceValue = 2,
					Drops = {
						new SL_ItemDropChance {
							DiceValue = 9,
							MinQty = 1,
							MaxQty = 3,
							DroppedItemID = potion.New_ItemID
						},
						new SL_ItemDropChance {
							DiceValue = 5,
							MinQty = 1,
							MaxQty = 5,
							DroppedItemID = potionRecipeItem.New_ItemID
						}
					}
					
				}}
			};
			potionAndRecipeDT.ApplyTemplate();
			
			SL_DropTableAddition potionAndRecipeForMerchants = new SL_DropTableAddition {
				SelectorTargets = {"-MSrkT502k63y3CV2j98TQ", "G_GyAVjRWkq8e2L8WP4TgA"},
				DropTableUIDsToAdd = {potionAndRecipeDT.UID}
				
			};
			potionAndRecipeForMerchants.ApplyTemplate();
		}

	}

}