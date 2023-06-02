using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SideLoader;
using TravelSpeed.Effects;
using TravelSpeed.Items;

namespace TravelSpeed {
	[BepInDependency(SL.GUID, BepInDependency.DependencyFlags.HardDependency)]
	[BepInPlugin(GUID, NAME, VERSION)]
	public class TravelSpeed : BaseUnityPlugin {
		public const string GUID = "faeryn.travelspeed";
		public const string NAME = "TravelSpeed";
		public const string VERSION = "1.2.3";

		internal static ManualLogSource Log;

		internal void Awake() {
			Log = this.Logger;
			Log.LogMessage($"Starting {NAME} {VERSION}");
			InitializeSL();
			new Harmony(GUID).PatchAll();
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
				UID = Constants.POTION_RECIPE_NAME,
				StationType = Recipe.CraftingType.Alchemy,
				Ingredients = {
					new SL_Recipe.Ingredient {
						Type = RecipeIngredient.ActionTypes.AddGenericIngredient,
						SelectorValue = "Water"
					},
					new SL_Recipe.Ingredient {
						Type = RecipeIngredient.ActionTypes.AddGenericIngredient,
						SelectorValue = "Egg"
					},
					new SL_Recipe.Ingredient {
						Type = RecipeIngredient.ActionTypes.AddSpecificIngredient,
						SelectorValue = "6400130" // Mana Stone
					}
				},
				Results = {
					new SL_Recipe.ItemQty {
						ItemID = potion.New_ItemID, 
						Quantity = 2
					}
				}
			};
			potionRecipe.ApplyTemplate();

			SL_RecipeItem potionRecipeItem = new SL_RecipeItem {
				RecipeUID = Constants.POTION_RECIPE_NAME,
				Target_ItemID = 5700110,
				New_ItemID = -12500,
				Name = "Alchemy: Travel Potion"
			};
			potionRecipeItem.ApplyTemplate();

			SL_DropTable potionAndRecipeDT = new SL_DropTable {
				UID = Constants.POTION_DROPTABLE_UID,
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
				SelectorTargets = {"-MSrkT502k63y3CV2j98TQ", "G_GyAVjRWkq8e2L8WP4TgA"}, // Soroborean Caravanner
				DropTableUIDsToAdd = {potionAndRecipeDT.UID}
				
			};
			potionAndRecipeForMerchants.ApplyTemplate();
		}

	}

}