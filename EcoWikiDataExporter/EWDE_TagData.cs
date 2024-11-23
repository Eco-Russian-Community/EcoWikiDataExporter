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
using Eco.Simulation.Agents;
using Eco.Shared.Logging;
using Eco.Core.Items;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        // Dictionary of tags
        private static SortedDictionary<string, Dictionary<string, string>> TagData = new SortedDictionary<string, Dictionary<string, string>>();

        public static void ExportTagData()
        {
            // Dictionary of tags properties
            Dictionary<string, string> tagDetails = new Dictionary<string, string>()
            {
                { "Name","nil" },
                { "LocalizedName","nil" },
                { "Items","nil" }
            };

            IEnumerable<Tag> tags = TagManager.AllTags;

            foreach (Tag tag in tags )
            {
                string tagName = tag.Name;
                string LocalizedName = tag.DisplayName;
                
                if (!TagData.ContainsKey(tagName))
                {
                    TagData.Add(tagName, new Dictionary<string, string>(tagDetails));
                    TagData[tagName]["Name"] = $"'{tagName}'";
                    TagData[tagName]["LocalizedName"] = $"'{LocalizedName}'";

                    Log.WriteWarningLineLoc($"Export tag: {LocalizedName}");

					IEnumerable<Item> associatedItems = Item.AllItemsExceptHidden.Where(x => x.Tags().Contains(tag));
					string tagItems = "";
					foreach (Item item in associatedItems)
					{
						tagItems = tagItems + item.DisplayName;
						Log.WriteWarningLineLoc($"Export tag item: {item.DisplayName}");
					}
					TagData[tagName]["Items"] = $"'{tagItems}'";
					Log.WriteWarningLineLoc($"Export tag item: {ItemUtils.GetItemsByTag(tagName)}");

                }
            }

        // writes to txt file
        EcoWikiDataManager.WriteDictionaryToFile("TagData", "tags", TagData);
        }
    }
}
