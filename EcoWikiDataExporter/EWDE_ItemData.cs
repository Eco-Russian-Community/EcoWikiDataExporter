using Eco.Core.Controller;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Components.Storage;
using Eco.Gameplay.Components.Store;
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
using Eco.Gameplay.Settlements.Components;
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
                    int Tier = 0;
                    List<int> WorldObjectComponentsList = [];

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

                    #region FuelItem
                    if (item.IsFuel)
					{
						//ItemData[ItemName]["IsFuel"] = $"'True'";

                        FuelData fueldata = new FuelData
                        {
                            Power = WikiFloat(item.Fuel)
                        };

                        FuelDataList.Add(ItemName, fueldata);
                    }
                    #endregion

                    #region BlockItem
                    if (item is BlockItem Block)
					{
                        //ItemData[ItemName]["HasForms"] = $"'{Block.HasForms}'";
                        Tier = Block.Tier;

                    }
                    #endregion

                    #region ModuleItem
                    if (item is ModuleItem Module)
					{
                        //ItemData[ItemName]["ModuleItem"] = $"'True'";


                    }
                    #endregion

                    #region PartItem
                    if (item is PartItem Part)
					{
						//ItemData[ItemName]["PartItem"] = $"'True'";
						//ItemData[ItemName]["MaxDurability"] = $"'{Part.IntegrityAmount}'";
					}
                    #endregion

                    #region VehicleToolItem
                    if (item is VehicleToolItem vehicleToolItem)
					{ 
						//ItemData[ItemName]["VehicleToolItem"] = $"'True'";
                        

                    }
                    #endregion

                    #region DecontaminantItem
                    if (item is DecontaminantItem decontaminantItem)
					{
                        //decontaminantItem.TargetType
                        //decontaminantItem.Potency


                    }
                    #endregion

                    #region ToolItem
                    if (item is ToolItem toolItem)
                    {
                        string ToolType = "Tool";
                        bool DevTool = false;

                        if (item is AxeItem) { ToolType = "Axe"; }
                        if (item is PickaxeItem) { ToolType = "Pickaxe"; }
                        if (item is ShovelItem) { ToolType = "Shovel"; }
                        if (item is HammerItem) { ToolType = "Hammer"; }
                        if (item is HoeItem) { ToolType = "Hoe"; }
                        if (item is SickleItem) { ToolType = "Sickle"; }
                        if (item is ScytheItem) { ToolType = "Sickle"; }
                        if (item is MacheteItem) { ToolType = "Machete"; }
                        if (item is PaintToolItem) { ToolType = "PaintTool"; }
                        if (item is DrillItem) { ToolType = "Drill"; }
                        if (item is BlastingChargeItem) { ToolType = "BlastingCharge"; }
                        if (item is DetonatorBaseItem) { ToolType = "Detonator"; }
                        if (item is BowItem) { ToolType = "Bow"; }
                        if (item is RoadToolItem) { ToolType = "RoadTool"; }
                        if (item is TorchItem) { ToolType = "Torch"; }
                        if (item is ClaimToolItem) { ToolType = "ClaimTool"; }
                        if (item is CameraItem) { ToolType = "Camera"; }
                        if (ItemName == "Fishing Pole") { ToolType = "FishingPole"; }
                        if (ItemName == "Soil Sampler") { ToolType = "SoilSampler"; }

                        //if (item is BuildingToolItem) { ItemData[ItemName]["BuildingToolItem"] = $"'True'"; }
                        //if (item is BlockHarvestItem) { ToolData[ItemName]["ToolType"] = $"'BlockHarvest'"; }

                        if (item is WeaponItem Weapon)
                        {

                        }

                        if (ItemName == "Dev Tool" | ItemName == "Dev Flood Tool" | ItemName == "Hydrometer" | ItemName == "Dev Paint Tool") { DevTool = true; }

                        ToolData tooldata = new ToolData
                        {
                            ToolType = ToolType,
                            Hidden = item.Hidden,
                            Tier = WikiFloat(toolItem.Tier.GetBaseValue),
                            CaloriesBurn = WikiFloat(toolItem.CaloriesBurn.GetBaseValue),
                            Weapon = item is WeaponItem,
                            Damage = WikiFloat(toolItem.Damage.GetBaseValue),
                            MaxTake = WikiFloat(toolItem.MaxTake),
                            DevTool = DevTool
                        };

                        ToolDataList.Add(ItemName, tooldata);

                    }
                    #endregion

                    #region WorldObjectItem

                    if (item is WorldObjectItem worldObjectItem)
                    {
                        Type worldObjecttype = worldObjectItem.WorldObjectType;

                        var occupancy = WorldObject.GetOccupancy(worldObjecttype).Select(x => x.Offset).ToList();
                        var size = Vector3i.One + new Vector3i(occupancy.Max(i => i.x) - occupancy.Min(i => i.x),
                                                               occupancy.Max(i => i.y) - occupancy.Min(i => i.y),
                                                               occupancy.Max(i => i.z) - occupancy.Min(i => i.z));
                        string FullSize = size.z + "," + size.x + "," + size.y;

                        if (item.Category == "WorldObject")
                        {
                            try
                            {
                                WorldObject? worldObject = objectInitializer.Init(worldObjectItem) ?? throw new Exception($"Initializer error for WorldObjectItem: {worldObjectItem.Name}");

                                if (worldObject != null)
                                {
                                    Tier = worldObject.Tier;


                                    WorldObjectComponentsList =
                                    [
                                        worldObject.HasComponent<CraftingComponent>() ? 1 : 0,
                                        worldObject.HasComponent<RoomRequirementsComponent>() ? 1 : 0,
                                        worldObject.HasComponent<HousingComponent>() ? 1 : 0,
                                        worldObject.HasComponent<DoorComponent>() ? 1 : 0,
                                        worldObject.HasComponent<BedComponent>() ? 1 : 0,
                                        worldObject.HasComponent<MountComponent>() ? 1 : 0,
                                        worldObject.HasComponent<ForSaleComponent>() ? 1 : 0,
                                        worldObject.HasComponent<MintComponent>() ? 1 : 0,
                                        worldObject.HasComponent<PropertyAuthComponent>() ? 1 : 0,
                                        worldObject.HasComponent<LinkComponent>() ? 1 : 0,
                                        worldObject.HasComponent<MinimapComponent>() ? 1 : 0,
                                        worldObject.HasComponent<StoreComponent>() ? 1 : 0,
                                        worldObject.HasComponent<SharedLinkComponent>() ? 1 : 0,
                                        worldObject.HasComponent<OnOffComponent>() ? 1 : 0,
                                        worldObject.HasComponent<VehicleComponent>() ? 1 : 0,
                                        worldObject.HasComponent<BankComponent>() ? 1 : 0,
                                        worldObject.HasComponent<AirPollutionComponent>() ? 1 : 0,
                                        worldObject.HasComponent<JurisdictionComponent>() ? 1 : 0,
                                        worldObject.HasComponent<ConstitutionComponent>() ? 1 : 0,
                                        worldObject.HasComponent<CivicObjectComponent>() ? 1 : 0,
                                        worldObject.HasComponent<AnnexationComponent>() ? 1 : 0,
                                        worldObject.HasComponent<CultureControlComponent>() ? 1 : 0,
                                        worldObject.HasComponent<PowerGridComponent>() ? 1 : 0,
                                        worldObject.HasComponent<PowerGridNetworkComponent>() ? 1 : 0,
                                        worldObject.HasComponent<PublicStorageComponent>() ? 1 : 0,
                                        worldObject.HasComponent<AnimalTrapComponent>() ? 1 : 0,
                                        worldObject.HasComponent<ExchangeComponent>() ? 1 : 0,
                                        worldObject.HasComponent<FilterComponent>() ? 1 : 0,
                                        worldObject.HasComponent<CustomTextComponent>() ? 1 : 0,
                                        worldObject.HasComponent<MustBeOwnedComponent>() ? 1 : 0,
                                        worldObject.HasComponent<ItemDistributionComponent>() ? 1 : 0,
                                        worldObject.HasComponent<ElevatorCallPostComponent>() ? 1 : 0,
                                        worldObject.HasComponent<StatusComponent>() ? 1 : 0,
                                        worldObject.HasComponent<SortingComponent>() ? 1 : 0,
                                        worldObject.HasComponent<LiquidTankComponent>() ? 1 : 0,
                                        worldObject.HasComponent<StockpileComponent>() ? 1 : 0,
                                        worldObject.HasComponent<WorldStockpileComponent>() ? 1 : 0,
                                        worldObject.HasComponent<DeedManagementComponent>() ? 1 : 0,
                                        worldObject.HasComponent<DeedSalesComponent>() ? 1 : 0,
                                        worldObject.HasComponent<RegistrarComponent>() ? 1 : 0,
                                        worldObject.HasComponent<PumpComponent>() ? 1 : 0,
                                        worldObject.HasComponent<LiquidProducerComponent>() ? 1 : 0,
                                        worldObject.HasComponent<TollComponent>() ? 1 : 0,
                                        worldObject.HasComponent<TreasuryComponent>() ? 1 : 0,
                                        worldObject.HasComponent<AttachmentComponent>() ? 1 : 0,
                                    ];
                                }
                                else
                                {
                                    Log.WriteLineLoc($"{worldObject.Name} Not Init");
                                }
                                WorldObjectManager.DestroyPermanently(worldObject);
                            }
                            catch (Exception ex) { Log.WriteException(ex); }
                        }

                        string WorldObjectComponents = string.Join(",", WorldObjectComponentsList);

                        WorldObjectData worldobjectdata = new WorldObjectData
                        {
                            Name = worldObjecttype.Name,
                            Size = FullSize,
                            Tier = Tier,
                            Components = WorldObjectComponents,
                        };

                        WorldObjectDataList.Add(ItemName, worldobjectdata);

                    }
                    #endregion

                        // -------------------------------------------------------------------------------------------------------

                        //SalvageCost
                        if (item.IsWasteProduct)
					{


					}


                    if (item.IsStackable)
                    {
                        //ItemData[ItemName]["IsStackable"] = $"'True'"; 
                    }

                    //item.IsCarried


                    //if (item is SuitItem) { ItemData[ItemName]["SuitItem"] = $"'True'"; }
                    //if (item is ColorItem) { ItemData[ItemName]["ColorItem"] = $"'True'"; }






                    // ------------------   Item Types List  -------------------------------------------------------------------------------------

                    List<int> ItemTypesList = 
                    [
                        item is FoodItem ? 1 : 0, 
                        item is SeedItem ? 1 : 0,
                        item is BlockItem ? 1 : 0,
                        item is ClothingItem ? 1 : 0,
                        item is FertilizerItem ? 1 : 0,
                        item is SkillBook ? 1 : 0,
                        item is SkillScroll ? 1 : 0,
                        item is ModuleItem ? 1 : 0,
                        item is PartItem ? 1 : 0,
                        item is ToolItem ? 1 : 0,
                        item is DecontaminantItem ? 1 : 0,
                        item is WorldObjectItem ? 1 : 0,
                        item is VehicleToolItem ? 1 : 0,
                        item.IsFuel ? 1 : 0,
                        item.IsWasteProduct ? 1 : 0,
                        item.CanBeCurrency ? 1 : 0,
                        MarketplaceExtensions.IsPaidItem(item) ? 1 : 0,
                    ];

                    string ItemTypes = string.Join(",", ItemTypesList);

                    ItemData itemdata = new ItemData
                    {
                        ID = item.Type.Name.ToString(),
                        Name = Localization(ItemName),
                        Description = Localization(CleanText(item.GetDescription.NotTranslated)),
                        Hidden = item.Hidden,
                        Tier = Tier,
                        Weight = item.Weight,
                        MaxStackSize = item.MaxStackSize,
                        Types = ItemTypes,
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
            string ToolDataListjsonString = JsonConvert.SerializeObject(new { tools = ToolDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("Tools", ToolDataListjsonString);
            string WorldObjectDataListjsonString = JsonConvert.SerializeObject(new { WorldObjects = WorldObjectDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("WorldObjects", WorldObjectDataListjsonString);

        }
	}
}
