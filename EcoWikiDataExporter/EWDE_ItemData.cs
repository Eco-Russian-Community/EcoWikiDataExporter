using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
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
using Eco.Core.Controller;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Shared.Icons;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using Eco.Gameplay.Systems;
using Eco.Shared;
using Eco.Shared.IoC;
using Eco.Gameplay.Systems.EcoMarketplace;
using System.Globalization;

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
                { "PaidItem", "nil" },
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
                ItemName = item.Name;

                if (!ItemData.ContainsKey(ItemName) && (item.DisplayName != "Chat Log") && (item.Group != "Skills") && (item.Group != "Talents") && (item.Group != "Actionbar Items"))
                {

                    ItemData.Add(ItemName, new Dictionary<string, string>(itemDetails));

                    ItemData[ItemName]["ID"] = $"'{item.Type.Name}'";
                    ItemData[ItemName]["Category"] = $"'{item.Category}'";
                    ItemData[ItemName]["Group"] = $"'{item.Group}'";
                    ItemData[ItemName]["Name"] = EcoWikiDataManager.WriteDictionaryAsSubObject(EcoWikiDataManager.Localization(item.DisplayName.NotTranslated), 1);

                    ItemData[ItemName]["Description"] = EcoWikiDataManager.WriteDictionaryAsSubObject(EcoWikiDataManager.Localization(item.GetDescription.NotTranslated),1);

                    if (item.HasWeight) { ItemData[ItemName]["Weight"] = $"'{item.Weight}'"; }
                    ItemData[ItemName]["MaxStackSize"] = $"'{item.MaxStackSize}'";
                    ItemData[ItemName]["Tags"] = $"{EcoWikiDataManager.GetItemTags(item)}";
                    if (MarketplaceExtensions.IsPaidItem(item)) { ItemData[ItemName]["PaidItem"] = $"'{MarketplaceExtensions.IsPaidItem(item)}'"; }

                    if (item.IsTool) { ItemData[ItemName]["IsTool"] = $"'True'"; }
                    if (item.CanBeCurrency) { ItemData[ItemName]["CanBeCurrency"] = $"'True'"; }
                    if (item.Compostable) { ItemData[ItemName]["Compostable"] = $"'True'"; }
                    if (item.IsWasteProduct) { ItemData[ItemName]["IsWasteProduct"] = $"'True'"; }
                    if (item.IsFuel) { ItemData[ItemName]["IsFuel"] = $"'True'"; }
                    if (item.IsStackable) { ItemData[ItemName]["IsStackable"] = $"'True'"; }

                    if (item is FoodItem foodItem)
                    {
                        ItemData[ItemName]["Calories"] = $"'{foodItem.Calories}'";
                        ItemData[ItemName]["Carbs"] = $"'{foodItem.Nutrition.Carbs}'";
                        ItemData[ItemName]["Protein"] = $"'{foodItem.Nutrition.Protein}'";
                        ItemData[ItemName]["Fat"] = $"'{foodItem.Nutrition.Fat}'";
                        ItemData[ItemName]["Vitamins"] = $"'{foodItem.Nutrition.Vitamins}'";
                    }
                }
            }

            // writes to txt file
            EcoWikiDataManager.WriteDictionaryToFile("ItemData", "items", ItemData);

        }
    }
}
