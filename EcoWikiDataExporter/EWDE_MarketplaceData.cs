using Eco.Core.Controller;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.EcopediaRoot;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Rooms;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.EcoMarketplace;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Mods.TechTree;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.IoC;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using System;
using System.Collections;
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
using StrangeCloud.Service.Client.Contracts;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        private static SortedDictionary<string, Dictionary<string, string>> MarketplaceData = new SortedDictionary<string, Dictionary<string, string>>();
        public static void ExportMarketplaceData()
        {
            // dictionary of marketplace item properties
            Dictionary<string, string> marketplaceitemDetails = new Dictionary<string, string>()
            {
                { "Category", "nil" },
                { "Price", "nil" },
                { "Quantity", "nil" },
                { "Achievement", "nil" }
            };

            foreach (MarketplaceCategory Category in GameData.Obj.EcoMarketplaceManager.Categories)
            {
                if ((Category.Name != "StrangeLoop") && (Category.Name != "Currency"))
                {
                    foreach (MarketplaceItem Item in Category.Items)
                    {
                        string MarketplaceItemName = Item.DisplayName;

                        MarketplaceData.Add(MarketplaceItemName, new Dictionary<string, string>(marketplaceitemDetails));

                        MarketplaceData[MarketplaceItemName]["Category"] = $"'{Category.Name}'";
                        MarketplaceData[MarketplaceItemName]["Price"] = $"'{Item.Price}'";
                        MarketplaceData[MarketplaceItemName]["Quantity"] = $"'{Item.Quantity}'";
                        MarketplaceData[MarketplaceItemName]["Achievement"] = $"'{Item.AchievementRequired}'";
                    }
                }
            }
                
            // writes to txt file
            WriteDictionaryToFile("MarketplaceData", "blueprints", MarketplaceData);
        }
    }
}
