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
using static Eco.Shared.Utils.Singleton<T>;

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
                { "Description", "nil" },
                { "Weight", "nil" }
            };

            string ItemName;
            foreach (Item item in Item.AllItemsIncludingHidden)
            {
                ItemName = item.DisplayName;

                if (!ItemData.ContainsKey(ItemName))
                {

                    ItemData.Add(ItemName, new Dictionary<string, string>(itemDetails));

                    ItemData[ItemName]["ID"] = $"'{item.Type.Name}'";
                    ItemData[ItemName]["Category"] = $"'{item.Category}'";
                    ItemData[ItemName]["Group"] = $"'{item.Group}'";
                    ItemData[ItemName]["Description"] = $"'{EcoWikiDataManager.CleanText(item.GetDescription)}'";
                    if (item.HasWeight) { ItemData[ItemName]["Weight"] = $"'{item.Weight}'";

                }


            }

         // writes to txt file
         EcoWikiDataManager.WriteDictionaryToFile("ItemData", "items", ItemData);

        }
    }
}
