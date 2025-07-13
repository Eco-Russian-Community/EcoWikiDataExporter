using Eco.Core.Controller;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
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
using Eco.Shared.Networking;
using Eco.Shared.Utils;
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
                    ItemData[ItemName]["Group"] = $"'{item.Group}'";
                    ItemData[ItemName]["Name"] = WriteDictionaryAsSubObject(Localization(ItemName), 1);
                    ItemData[ItemName]["Description"] = WriteDictionaryAsSubObject(Localization(CleanText(item.GetDescription.NotTranslated)),1);

                    if (item.HasWeight) { ItemData[ItemName]["Weight"] = $"'{item.Weight}'"; }
                    ItemData[ItemName]["MaxStackSize"] = $"'{item.MaxStackSize}'";
                    ItemData[ItemName]["Tags"] = $"{GetItemTags(item)}";
                    if (MarketplaceExtensions.IsPaidItem(item)) { ItemData[ItemName]["IsPaidItem"] = $"'{MarketplaceExtensions.IsPaidItem(item)}'"; }

                    if (item.IsTool) { ItemData[ItemName]["IsTool"] = $"'True'"; }
                    if (item.CanBeCurrency) { ItemData[ItemName]["CanBeCurrency"] = $"'True'"; }
                    if (item.Compostable) { ItemData[ItemName]["Compostable"] = $"'True'"; }
                    if (item.IsWasteProduct) { ItemData[ItemName]["IsWasteProduct"] = $"'True'"; }
                    if (item.IsFuel) { ItemData[ItemName]["IsFuel"] = $"'True'"; }
                    if (item.IsStackable) { ItemData[ItemName]["IsStackable"] = $"'True'"; }

                    if (item is FoodItem) { ItemData[ItemName]["FoodItem"] = $"'True'"; }
                    if (item is BlockItem) { ItemData[ItemName]["BlockItem"] = $"'True'"; }
                    if (item is SeedItem) { ItemData[ItemName]["SeedItem"] = $"'True'"; }
                    if (item is ModuleItem) { ItemData[ItemName]["ModuleItem"] = $"'True'"; }
                    if (item is PartItem) { ItemData[ItemName]["PartItem"] = $"'True'"; }
                    if (item is ClothingItem) { ItemData[ItemName]["ClothingItem"] = $"'True'"; }
                    if (item is VehicleToolItem) { ItemData[ItemName]["VehicleToolItem"] = $"'True'"; }
                    if (item is WorldObjectItem) { ItemData[ItemName]["WorldObjectItem"] = $"'True'"; }
                    if (item is FertilizerItem) { ItemData[ItemName]["FertilizerItem"] = $"'True'"; }
                    if (item is SkillBook) { ItemData[ItemName]["SkillBook"] = $"'True'"; }
                    if (item is SkillScroll) { ItemData[ItemName]["SkillScroll"] = $"'True'"; }
                    if (item is SuitItem) { ItemData[ItemName]["SuitItem"] = $"'True'"; }
                    if (item is ToolItem) { 
                        ItemData[ItemName]["ToolItem"] = $"'True'";

                        if (item is AxeItem) { ItemData[ItemName]["AxeItem"] = $"'True'"; }
                        if (item is PickaxeItem) { ItemData[ItemName]["PickaxeItem"] = $"'True'"; }
                        if (item is ShovelItem) { ItemData[ItemName]["ShovelItem"] = $"'True'"; }
                        if (item is HammerItem) { ItemData[ItemName]["HammerItem"] = $"'True'"; }
                        if (item is HoeItem) { ItemData[ItemName]["HoeItem"] = $"'True'"; }
                        if (item is MacheteItem) { ItemData[ItemName]["MacheteItem"] = $"'True'"; }
                        if (item is PaintToolItem) { ItemData[ItemName]["PaintToolItem"] = $"'True'"; }
                        if (item is DrillItem) { ItemData[ItemName]["DrillItem"] = $"'True'"; }
                        if (item is DetonatorBaseItem) { ItemData[ItemName]["DetonatorBaseItem"] = $"'True'"; }
                        if (item is BowItem) { ItemData[ItemName]["BowItem"] = $"'True'"; }
                        if (item is SickleItem) { ItemData[ItemName]["SickleItem"] = $"'True'"; }
                    }
                }
            }

            // writes to txt file
            WriteDictionaryToFile("ItemData", "items", ItemData);

        }
    }
}
