using Eco.Core.Controller;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Storage;
using Eco.Gameplay.Garbage;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Occupancy;
using Eco.Gameplay.Pipes.Gases;
using Eco.Gameplay.Pipes.LiquidComponents;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Rooms;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.EcoMarketplace;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Mods.TechTree;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.IoC;
using Eco.Shared.Localization;
using Eco.Shared.Logging;
using Eco.Shared.Math;
using Eco.Shared.Networking;
using Eco.Shared.Services;
using Eco.Shared.StrangeCloudShared;
using Eco.Shared.Utils;
using Eco.Simulation.Agents;
using Eco.World.Blocks;
using Newtonsoft.Json;
using StrangeCloud.Service.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Eco.Mods.EcoWikiDataExporter.WikiData;
using static Eco.Simulation.Types.PlantSpecies;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
	{

		// dictionary of items and their stats
        private static SortedDictionary<string, ItemData> ItemDataList = new SortedDictionary<string, ItemData>();
        private static SortedDictionary<string, FoodData> FoodDataList = new SortedDictionary<string, FoodData>();
        private static SortedDictionary<string, FertilizerData> FertilizerDataList = new SortedDictionary<string, FertilizerData>();
		private static SortedDictionary<string, ClothingData> ClothingDataList = new SortedDictionary<string, ClothingData>();
        private static SortedDictionary<string, WorldObjectData> WorldObjectDataList = new SortedDictionary<string, WorldObjectData>();
        private static SortedDictionary<string, ToolData> ToolDataList = new SortedDictionary<string, ToolData>();
        private static SortedDictionary<string, FuelData> FuelDataList = new SortedDictionary<string, FuelData>();
        private static SortedDictionary<string, SeedData> SeedDataList = new SortedDictionary<string, SeedData>();

        public static void ExportItemData()
		{

            WorldObjectInitializer objectInitializer = new WorldObjectInitializer();

			string ItemName;
			foreach (Item item in Item.AllItemsIncludingHidden)
			{
				ItemName = item.DisplayName.NotTranslated;

				if (!ItemDataList.ContainsKey(ItemName) && (ItemName != "Chat Log") && (item.Group != "Skills") && (item.Group != "Talents") && (item.Group != "Actionbar Items"))
				{
                    #region FoodItem
                    if (item is FoodItem foodItem)
                    {
                        //ItemData[ItemName]["FoodItem"] = $"'True'";

                        FoodData fooddata = new FoodData
                        {
                            Calories = WikiFloat(foodItem.Calories),
                            Carbs = WikiFloat(foodItem.Nutrition.Carbs),
                            Protein = WikiFloat(foodItem.Nutrition.Protein),
                            Fat = WikiFloat(foodItem.Nutrition.Fat),
                            Vitamins = WikiFloat(foodItem.Nutrition.Vitamins),
                            ShelfLife = WikiFloat(foodItem.GetPropertyValueByName<float>("BaseShelfLife")),
                        };

                        FoodDataList.Add(ItemName, fooddata);
                    }
                    #endregion

                    #region SeedItem
                    if (item is SeedItem Seed)
                    {
                        //ItemData[ItemName]["SeedItem"] = $"'True'";

                        SeedData seeddata = new SeedData
                        {
                            Species = Seed.SpeciesName.NotTranslated.AddSpacesBetweenCapitals(),
                        };

                        SeedDataList.Add(ItemName, seeddata);
                    }
                    #endregion

                    #region  FertilizerItem
                    if (item is FertilizerItem Fertilizer)
                    {
                        //ItemData[ItemName]["FertilizerItem"] = $"'True'";

                        FertilizerData fertilizerdata = new FertilizerData
                        {
                            Nitrogen = WikiFloat(Fertilizer.Nutrients.GetPropertyValueByName<float>("Nitrogen")),
                            Phosphorus = WikiFloat(Fertilizer.Nutrients.GetPropertyValueByName<float>("Phosphorus")),
                            Potassium = WikiFloat(Fertilizer.Nutrients.GetPropertyValueByName<float>("Potassium"))
                        };

                        FertilizerDataList.Add(ItemName, fertilizerdata);
                    }
                    #endregion

                    #region ClothingItem
                    if (item is ClothingItem Clothing)
                    {
                        //ItemData[ItemName]["ClothingItem"] = $"'True'";

                        Dictionary<UserStatType, float> сlothingStats = Clothing.GetFlatStats();
                        var FlatStats = new Dictionary<string, string>();
                        if (сlothingStats != null)
                        {
                            
                            foreach (var stat in Clothing.GetFlatStats()) { FlatStats.Add(stat.Key.ToString(), WikiFloat(stat.Value)); }
                        }

						ClothingData clothingdata = new ClothingData
						{
                            AvatarSlot = Clothing.Slot,
                            StartClothing = Clothing.Starter,
                            Hidden = item.Hidden,
                            FlatStats = FlatStats
                        };

                        ClothingDataList.Add(ItemName, clothingdata);
                    }
                    #endregion

                    if (item.IsFuel) 
					{
						//ItemData[ItemName]["IsFuel"] = $"'True'";

                        FuelData fueldata = new FuelData
                        {
                            Power = WikiFloat(item.Fuel)
                        };

                        FuelDataList.Add(ItemName, fueldata);
                    }

                    // -------------------------------------------------------------------------------------------------------

                    if (item.IsStackable)
					{ 
						//ItemData[ItemName]["IsStackable"] = $"'True'"; 
					}

					//item.IsCarried



					if (item is BlockItem Block)
					{
						//ItemData[ItemName]["BlockItem"] = $"'True'";
						//ItemData[ItemName]["HasForms"] = $"'{Block.HasForms}'";
                        //if (Block.HasTier) { ItemData[ItemName]["Tier"] = $"'{Block.Tier}'";  }

                    }


					if (item is ModuleItem)
					{ 
						//ItemData[ItemName]["ModuleItem"] = $"'True'";
					}

					if (item is PartItem Part)
					{
						//ItemData[ItemName]["PartItem"] = $"'True'";
						//ItemData[ItemName]["MaxDurability"] = $"'{Part.IntegrityAmount}'";
					}


                    //vehicleToolItem
                    if (item is VehicleToolItem vehicleToolItem)
					{ 
						//ItemData[ItemName]["VehicleToolItem"] = $"'True'";
                        

                    }

                    //Decontaminant
                    if (item is DecontaminantItem decontaminantItem)
					{
                        //decontaminantItem.TargetType
                        //decontaminantItem.Potency


                    }


					//SalvageCost
					if (item.IsWasteProduct)
					{


					}




                    //if (item is SkillBook) { ItemData[ItemName]["SkillBook"] = $"'True'"; }
                    //if (item is SkillScroll) { ItemData[ItemName]["SkillScroll"] = $"'True'"; }
                    //if (item is SuitItem) { ItemData[ItemName]["SuitItem"] = $"'True'"; }
                    //if (item is ColorItem) { ItemData[ItemName]["ColorItem"] = $"'True'"; }

                    // -------------------------------------------------------------------------------------------------------



                    ItemData itemdata = new ItemData
                    {
                        ID = item.Type.Name.ToString(),
                        Name = Localization(ItemName),
                        Description = Localization(CleanText(item.GetDescription.NotTranslated)),
                        Weight = item.Weight,
                        MaxStackSize = item.MaxStackSize,
						Tags = GetItemTags(item)
                    };

                    ItemDataList.Add(ItemName, itemdata);
                }
			}

            // writes to json file
            string ItemDataListjsonString = JsonConvert.SerializeObject(new { items = ItemDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("Items", ItemDataListjsonString);
            string FoodDataListjsonString = JsonConvert.SerializeObject(new { foods = FoodDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("Food", FoodDataListjsonString);
            string SeedDataListjsonString = JsonConvert.SerializeObject(new { seeds = SeedDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("Seeds", SeedDataListjsonString);
            string FertilizerDataListjsonString = JsonConvert.SerializeObject(new { fertilizers = FertilizerDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("Fertilizers", FertilizerDataListjsonString);
            string ClothingDataListjsonString = JsonConvert.SerializeObject(new { clothing = ClothingDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("Clothing", ClothingDataListjsonString);
            string FuelDataListjsonString = JsonConvert.SerializeObject(new { fuels = FuelDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("Fuels", FuelDataListjsonString);

            //WriteDictionaryToFile("ToolData", "tools", ToolData);
            //WriteDictionaryToFile("WorldObjectData", "WorldObjects", WorldObjectData);

        }
	}
}
