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
using Eco.Simulation.Types;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
	{
		// Dictionary of tags
		private static SortedDictionary<string, Dictionary<string, string>> TagData = new SortedDictionary<string, Dictionary<string, string>>();

		// Dictionary of tags properties
		private static Dictionary<string, string> tagDetails = new Dictionary<string, string>()
		{
			{ "ID","nil" },
            { "Name","nil" },
            { "Hidden","nil" },
            { "IsVisibleInTooltip","nil" },
            { "IsVisibleInEcopedia","nil" },
            { "IsVisibleInFilter","nil" },
            { "Items","nil" }
        };

		public static void ExportTagData()
		{
			IEnumerable<Tag> tags = TagManager.AllTags;

			foreach (Tag tag in tags)
			{
				string tagID = tag.Name;
				string tagName = tag.DisplayName.NotTranslated;

                if (!TagData.ContainsKey(tagName))
                {
                    TagData.Add(tagName, new Dictionary<string, string>(tagDetails));

                    TagData[tagName]["ID"] = $"'{tagID}'";
                    TagData[tagName]["Name"] = WriteDictionaryAsSubObject(Localization(tagName), 1);
                    TagData[tagName]["IsHidden"] = $"'{tag.Hidden}'";
                    TagData[tagName]["IsVisibleInTooltip"] = $"'{tag.IsVisibleInTooltip}'";
                    TagData[tagName]["IsVisibleInEcopedia"] = $"'{tag.IsVisibleInEcopedia}'";
                    TagData[tagName]["IsVisibleInFilter"] = $"'{tag.IsVisibleInFilter}'";

					string[] associatedItems = Item.AllItemsExceptHidden.Where(item => item.Tags().Contains(tag)).Select(item => $"'{item.DisplayName.NotTranslated}'").ToArray();

					if (associatedItems.Any())
					{
                        TagData[tagName]["Items"] = WriteDictionaryToLine(string.Join(", ", associatedItems));
                    }                   
				}
			}

			// writes to txt file
			WriteDictionaryToFile("TagData", "tags", TagData);
		}
	}
}
