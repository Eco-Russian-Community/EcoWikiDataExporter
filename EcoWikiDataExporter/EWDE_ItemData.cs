using Eco.Core.Controller;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Pipes.Gases;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.EcoMarketplace;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Mods.TechTree;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.IoC;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Networking;
using Eco.Shared.StrangeCloudShared;
using Eco.Shared.Utils;
using Eco.Simulation.Agents;
using Eco.World.Blocks;
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
using static Eco.Simulation.Types.PlantSpecies;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
    
        // dictionary of items and their dictionary of stats
        private static SortedDictionary<string, Dictionary<string, string>> ItemData = new SortedDictionary<string, Dictionary<string, string>>();
        private static SortedDictionary<string, Dictionary<string, string>> FoodData = new SortedDictionary<string, Dictionary<string, string>>();
        private static SortedDictionary<string, Dictionary<string, string>> SeedData = new SortedDictionary<string, Dictionary<string, string>>();
        private static SortedDictionary<string, Dictionary<string, string>> FertilizerData = new SortedDictionary<string, Dictionary<string, string>>();
        private static SortedDictionary<string, Dictionary<string, string>> FuelData = new SortedDictionary<string, Dictionary<string, string>>();
        private static SortedDictionary<string, Dictionary<string, string>> ToolData = new SortedDictionary<string, Dictionary<string, string>>();
        private static SortedDictionary<string, Dictionary<string, string>> ClothingData = new SortedDictionary<string, Dictionary<string, string>>();
        
        public static void ExportItemData()
        {
            // dictionary of item properties
            Dictionary<string, string> itemDetails = new Dictionary<string, string>()
            {
                { "ID", "nil" },
                { "Category", "nil" },
                { "Hidden", "nil" },
                { "Group", "nil" },
                { "Name", "nil" },
                { "Description", "nil" },
                { "Weight", "nil" },
                { "MaxStackSize", "nil" },
                { "Tags", "nil" },
                { "IsPaidItem", "nil" },
                { "IsTool", "nil" },
                { "CanBeCurrency", "nil" },
                { "Compostable", "nil" },
                { "IsWasteProduct", "nil" },
                { "IsFuel", "nil" },
                { "IsStackable", "nil" }

            };

            Dictionary<string, string> foodDetails = new Dictionary<string, string>()
            {
                { "Calories", "nil" },
                { "Carbs", "nil" },
                { "Protein", "nil" },
                { "Fat", "nil" },
                { "Vitamins", "nil" },
                { "ShelfLife", "nil" }
            };

            Dictionary<string, string> fertilizerDetails = new Dictionary<string, string>()
            {
                { "Nitrogen", "nil" },
                { "Phosphorus", "nil" },
                { "Potassium", "nil" }
            };

            Dictionary<string, string> seedDetails = new Dictionary<string, string>()
            {
                { "Species", "nil" }
            };

            Dictionary<string, string> fuelDetails = new Dictionary<string, string>()
            {
                { "Power", "nil" }
            };

            Dictionary<string, string> toolDetails = new Dictionary<string, string>()
            {
                { "ToolType", "nil" },
                { "Tier", "nil" },
                { "MaxTake", "nil" },
                { "Weapon", "'False'" },
                { "WeaponDamage", "'0'" }
            };

            Dictionary<string, string> clothingDetails = new Dictionary<string, string>()
            {
                { "ClothingSlot", "nil" },
                { "StartClothing", "nil" },
                { "FlatStats", "nil" },
            };

            string ItemName;
            foreach (Item item in Item.AllItemsIncludingHidden)
            {
                ItemName = item.DisplayName.NotTranslated;

                if (!ItemData.ContainsKey(ItemName) && (ItemName != "Chat Log") && (item.Group != "Skills") && (item.Group != "Talents") && (item.Group != "Actionbar Items"))
                {

                    ItemData.Add(ItemName, new Dictionary<string, string>(itemDetails));

                    ItemData[ItemName]["ID"] = $"'{item.Type.Name}'";

                    ItemData[ItemName]["Category"] = $"'{item.Category}'";
                    if (item.Category == "Hidden") { ItemData[ItemName]["Hidden"] = $"'True'"; }
                    ItemData[ItemName]["Group"] = $"'{item.Group}'";

                    ItemData[ItemName]["Name"] = WriteDictionaryAsSubObject(Localization(ItemName), 1);
                    ItemData[ItemName]["Description"] = WriteDictionaryAsSubObject(Localization(CleanText(item.GetDescription.NotTranslated)),1);

                    if (item.HasWeight) { ItemData[ItemName]["Weight"] = $"'{item.Weight}'"; }
                    ItemData[ItemName]["MaxStackSize"] = $"'{item.MaxStackSize}'";
                    ItemData[ItemName]["Tags"] = $"{GetItemTags(item)}";
                    if (MarketplaceExtensions.IsPaidItem(item)) { ItemData[ItemName]["IsPaidItem"] = $"'{MarketplaceExtensions.IsPaidItem(item)}'"; }


                    if (item.CanBeCurrency) { ItemData[ItemName]["CanBeCurrency"] = $"'True'"; }
                    if (item.Compostable) { ItemData[ItemName]["Compostable"] = $"'True'"; }
                    if (item.IsWasteProduct) { ItemData[ItemName]["IsWasteProduct"] = $"'True'"; }

                    if (item.IsFuel) { 
                        ItemData[ItemName]["IsFuel"] = $"'True'";

                        FuelData.Add(ItemName, new Dictionary<string, string>(fuelDetails));
                        FuelData[ItemName]["Power"] = $"'{item.Fuel}'";
                    }

                    if (item.IsStackable) { ItemData[ItemName]["IsStackable"] = $"'True'"; }

                    //item.IsCarried

                    if (item is FoodItem) {
                        ItemData[ItemName]["FoodItem"] = $"'True'";
                        if (item is FoodItem foodItem)
                        {
                            FoodData.Add(ItemName, new Dictionary<string, string>(foodDetails));
                            FoodData[ItemName]["Calories"] = $"'{foodItem.Calories}'";
                            FoodData[ItemName]["Carbs"] = $"'{foodItem.Nutrition.Carbs}'";
                            FoodData[ItemName]["Protein"] = $"'{foodItem.Nutrition.Protein}'";
                            FoodData[ItemName]["Fat"] = $"'{foodItem.Nutrition.Fat}'";
                            FoodData[ItemName]["Vitamins"] = $"'{foodItem.Nutrition.Vitamins}'";
                            FoodData[ItemName]["ShelfLife"] = $"'{foodItem.GetPropertyValueByName<float>("BaseShelfLife")}'";
                        }
                    }

                    if (item is BlockItem Block) { 
                        ItemData[ItemName]["BlockItem"] = $"'True'"; 
                        ItemData[ItemName]["HasForms"] = $"'{Block.HasForms}'";
                    }
                    if (item is SeedItem Seed) { 
                        ItemData[ItemName]["SeedItem"] = $"'True'";

                        SeedData.Add(ItemName, new Dictionary<string, string>(seedDetails));
                        SeedData[ItemName]["Species"] = $"'{Seed.SpeciesName.NotTranslated.AddSpacesBetweenCapitals()}'";
                    }

                    if (item is ModuleItem) { ItemData[ItemName]["ModuleItem"] = $"'True'"; }
                    if (item is PartItem Part) { 
                        ItemData[ItemName]["PartItem"] = $"'True'";
                        ItemData[ItemName]["MaxDurability"] = $"'{Part.IntegrityAmount}'"; 
                    }
                    if (item is ClothingItem Clothing) { 
                        ItemData[ItemName]["ClothingItem"] = $"'True'";

                        ClothingData.Add(ItemName, new Dictionary<string, string>(clothingDetails));
                        ClothingData[ItemName]["ClothingSlot"] = $"'{Clothing.Slot}'";
                        ClothingData[ItemName]["StartClothing"] = $"'{Clothing.Starter}'";
                        
                        Dictionary<UserStatType, float> сlothingStats = Clothing.GetFlatStats();
                        //ClothingData[ItemName]["FlatStats"] = $"{FlatStatString}";


                    }
                    if (item is VehicleToolItem) { ItemData[ItemName]["VehicleToolItem"] = $"'True'"; }
                    if (item is WorldObjectItem worldObjectItem) {
                        Type worldObjecttype = WorldObjectItem.worldObjectItem;
                        ItemData[ItemName]["WorldObjectItem"] = $"'True'";

                        var occupancy = WorldObject.GetOccupancy((item as WorldObjectItem).WorldObjectType).Select(x => x.Offset).ToList();
                        var size = Vector3i.One + new Vector3i(occupancy.Max(i => i.x) - occupancy.Min(i => i.x),
                                                               occupancy.Max(i => i.y) - occupancy.Min(i => i.y),
                                                               occupancy.Max(i => i.z) - occupancy.Min(i => i.z));
                        string fullsize = size.z + "," + size.x + "," + size.y;
                        ItemData[ItemName]["WorldObjectSize"] = $"'{fullsize}'";

                        //WorldObjec components


                    }

                    if (item is FertilizerItem Fertilizer) {
                        ItemData[ItemName]["FertilizerItem"] = $"'True'";

                        FertilizerData.Add(ItemName, new Dictionary<string, string>(fertilizerDetails));
                        float Nitrogen = Fertilizer.Nutrients.GetPropertyValueByName<float>("Nitrogen");
                        float Phosphorus = Fertilizer.Nutrients.GetPropertyValueByName<float>("Phosphorus");
                        float Potassium = Fertilizer.Nutrients.GetPropertyValueByName<float>("Potassium");
                        FertilizerData[ItemName]["Nitrogen"] = $"'{WikiFloat(Nitrogen)}'";
                        FertilizerData[ItemName]["Phosphorus"] = $"'{WikiFloat(Phosphorus)}'";
                        FertilizerData[ItemName]["Potassium"] = $"'{WikiFloat(Potassium)}'";
                    }

                    if (item is SkillBook) { ItemData[ItemName]["SkillBook"] = $"'True'"; }
                    if (item is SkillScroll) { ItemData[ItemName]["SkillScroll"] = $"'True'"; }
                    if (item is SuitItem) { ItemData[ItemName]["SuitItem"] = $"'True'"; }
                    if (item is ColorItem) { ItemData[ItemName]["ColorItem"] = $"'True'"; }

                    if (item is ToolItem toolItem ) {
                        ItemData[ItemName]["IsTool"] = $"'True'";

                        ToolData.Add(ItemName, new Dictionary<string, string>(toolDetails));

                        ToolData[ItemName]["ToolType"] = $"'Tool'";

                        ToolData[ItemName]["MaxTake"] = $"'{toolItem.MaxTake}'";
                        ToolData[ItemName]["Tier"] = $"'{toolItem.Tier.GetBaseValue}'";

                        if (item is AxeItem) { ToolData[ItemName]["ToolType"] = $"'Axe'"; }
                        if (item is PickaxeItem) { ToolData[ItemName]["ToolType"] = $"'Pickaxe'"; }
                        if (item is ShovelItem) { ToolData[ItemName]["ToolType"] = $"'Shovel'"; }
                        if (item is HammerItem) { ToolData[ItemName]["ToolType"] = $"'Hammer'"; }
                        if (item is HoeItem) { ToolData[ItemName]["ToolType"] = $"'Hoe'"; }
                        if (item is MacheteItem) { ToolData[ItemName]["ToolType"] = $"'Machete'"; }
                        if (item is PaintToolItem) { ToolData[ItemName]["ToolType"] = $"'PaintTool'"; }
                        if (item is DrillItem) { ToolData[ItemName]["ToolType"] = $"'Drill'"; }
                        if (item is DetonatorBaseItem) { ToolData[ItemName]["ToolType"] = $"'Detonator'"; }
                        if (item is BowItem) { ToolData[ItemName]["ToolType"] = $"'Bow'"; }
                        if (item is SickleItem) { ToolData[ItemName]["ToolType"] = $"'Sickle'"; }
                        if (item is RoadToolItem) { ToolData[ItemName]["ToolType"] = $"'RoadTool'"; }



                        if (item is WeaponItem Weapon) {
                            ToolData[ItemName]["Weapon"] = $"'True'";
                            ToolData[ItemName]["WeaponDamage"] = $"'{Weapon.Damage.GetBaseValue.ToString("G", CultureInfo.InvariantCulture)}'";
                        }

                        //if (item is BuildingToolItem) { ItemData[ItemName]["BuildingToolItem"] = $"'True'"; }



                    }
                }
            }

            // writes to txt file
            WriteDictionaryToFile("ItemData", "items", ItemData);
            WriteDictionaryToFile("FoodData", "foods", FoodData);
            WriteDictionaryToFile("SeedData", "seeds", SeedData);
            WriteDictionaryToFile("FuelData", "fuels", FuelData);
            WriteDictionaryToFile("ToolData", "tools", ToolData); 
            WriteDictionaryToFile("ClothingData", "clothing", ClothingData);
            WriteDictionaryToFile("FertilizerData", "fertilizers", FertilizerData);

        }
    }
}
