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
using Eco.World.Blocks;
using StrangeCloud.Service.Client;
using System;
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

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
    
        // dictionary of items and their dictionary of stats
        private static SortedDictionary<string, Dictionary<string, string>> ItemData = new();

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
                    if (item.IsFuel) { ItemData[ItemName]["IsFuel"] = $"'True'"; }
                    if (item.IsStackable) { ItemData[ItemName]["IsStackable"] = $"'True'"; }

                    //item.IsCarried

                    
                    
                    if (item is FoodItem) { 
                        ItemData[ItemName]["FoodItem"] = $"'True'";
                        if (item is FoodItem foodItem)
                        {
                            ItemData[ItemName]["Calories"] = $"'{foodItem.Calories}'";
                            ItemData[ItemName]["Carbs"] = $"'{foodItem.Nutrition.Carbs}'";
                            ItemData[ItemName]["Protein"] = $"'{foodItem.Nutrition.Protein}'";
                            ItemData[ItemName]["Fat"] = $"'{foodItem.Nutrition.Fat}'";
                            ItemData[ItemName]["Vitamins"] = $"'{foodItem.Nutrition.Vitamins}'";
                            //ItemData[ItemName]["BaseShelfLife"] = $"'{foodItem.BaseShelfLife}'";


                        }
                    }

                    if (item is BlockItem) { ItemData[ItemName]["BlockItem"] = $"'True'"; }
                    if (item is SeedItem) { ItemData[ItemName]["SeedItem"] = $"'True'"; }
                    if (item is ModuleItem) { ItemData[ItemName]["ModuleItem"] = $"'True'"; }
                    if (item is PartItem) { ItemData[ItemName]["PartItem"] = $"'True'"; }
                    if (item is ClothingItem) { ItemData[ItemName]["ClothingItem"] = $"'True'"; }
                    if (item is VehicleToolItem) { ItemData[ItemName]["VehicleToolItem"] = $"'True'"; }
                    if (item is WorldObjectItem) { 
                        ItemData[ItemName]["WorldObjectItem"] = $"'True'";

                        var occupancy = WorldObject.GetOccupancy((item as WorldObjectItem).WorldObjectType).Select(x => x.Offset).ToList();
                        var size = Vector3i.One + new Vector3i(occupancy.Max(i => i.x) - occupancy.Min(i => i.x),
                                                               occupancy.Max(i => i.y) - occupancy.Min(i => i.y),
                                                               occupancy.Max(i => i.z) - occupancy.Min(i => i.z));
                        string fullsize = size.z + "," + size.x + "," + size.y;
                        ItemData[ItemName]["WorldObjectSize"] = $"'{fullsize}'";

                        //WorldObject.GetOccupancy((item as WorldObjectItem).WorldObjectType).Select(x => x.Rotation).ToList();
                        //WorldObject.GetOccupancyInfo((item as WorldObjectItem).WorldObjectType);
                    }

                    if (item is FertilizerItem fertilizerItem) 
                    {
                        ItemData[ItemName]["FertilizerItem"] = $"'True'";
                        ItemData[ItemName]["FertilizerNutrients"] = $"'{fertilizerItem.Nutrients}'";
                        float nitrogen = fertilizerItem.Nutrients.GetPropertyValueByName<float>("Nitrogen");
                        float phosphorus = fertilizerItem.Nutrients.GetPropertyValueByName<float>("Phosphorus");
                        float potassium = fertilizerItem.Nutrients.GetPropertyValueByName<float>("Potassium");

					}
                    if (item is SkillBook) { ItemData[ItemName]["SkillBook"] = $"'True'"; }
                    if (item is SkillScroll) { ItemData[ItemName]["SkillScroll"] = $"'True'"; }
                    if (item is SuitItem) { ItemData[ItemName]["SuitItem"] = $"'True'"; }
                    if (item is ColorItem) { ItemData[ItemName]["ColorItem"] = $"'True'"; }

                if (item.IsTool) {
                        ItemData[ItemName]["IsTool"] = $"'True'";
                        ItemData[ItemName]["ToolType"] = $"'ToolItem'";

                        if (item is AxeItem) { ItemData[ItemName]["ToolType"] = $"'AxeItem'"; }
                        if (item is PickaxeItem) { ItemData[ItemName]["ToolType"] = $"'PickaxeItem'"; }
                        if (item is ShovelItem) { ItemData[ItemName]["ToolType"] = $"'ShovelItem'"; }
                        if (item is HammerItem) { ItemData[ItemName]["ToolType"] = $"'HammerItem'"; }
                        if (item is HoeItem) { ItemData[ItemName]["ToolType"] = $"'HoeItem'"; }
                        if (item is MacheteItem) { ItemData[ItemName]["ToolType"] = $"'MacheteItem'"; }
                        if (item is PaintToolItem) { ItemData[ItemName]["ToolType"] = $"'PaintToolItem'"; }
                        if (item is DrillItem) { ItemData[ItemName]["ToolType"] = $"'DrillItem'"; }
                        if (item is DetonatorBaseItem) { ItemData[ItemName]["ToolType"] = $"'DetonatorBaseItem'"; }
                        if (item is BowItem) { ItemData[ItemName]["ToolType"] = $"'BowItem'"; }
                        if (item is SickleItem) { ItemData[ItemName]["ToolType"] = $"'SickleItem'"; }
                        if (item is RoadToolItem) { ItemData[ItemName]["ToolType"] = $"'RoadToolItem'"; }

                        if (item is WeaponItem) { ItemData[ItemName]["WeaponItem"] = $"'True'"; }

                        //if (item is BuildingToolItem) { ItemData[ItemName]["BuildingToolItem"] = $"'True'"; }



                    }
                }
            }

            // writes to txt file
            WriteDictionaryToFile("ItemData", "items", ItemData);

        }
    }
}
