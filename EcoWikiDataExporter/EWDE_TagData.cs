using Eco.Core.Controller;
using Eco.Core.Items;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.IoC;
using Eco.Shared.Localization;
using Eco.Shared.Logging;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using Eco.Simulation.Agents;
using Eco.Simulation.Types;
using Newtonsoft.Json;
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
using static Eco.Mods.EcoWikiDataExporter.WikiData;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
	{
        private static Dictionary<string, TagData> TagDataList = new Dictionary<string, TagData>();

		public static void ExportTagData()
		{
			IEnumerable<Tag> tags = TagManager.AllTags;

			foreach (Tag tag in tags)
			{
				string tagID = tag.Name;
				string tagName = tag.DisplayName.NotTranslated;

                string[] associatedItems = Item.AllItemsExceptHidden.Where(item => item.Tags().Contains(tag)).Select(item => item.DisplayName.NotTranslated).ToArray();

                List<string> itemlist = new List<string>(associatedItems);

                TagData tagdata = new TagData
                {
                    ID                  = tagID,
                    Name                = Localization(tagName),
                    Hidden              = tag.Hidden,
                    IsVisibleInTooltip  = tag.IsVisibleInTooltip,
                    IsVisibleInEcopedia = tag.IsVisibleInEcopedia,
                    IsVisibleInFilter   = tag.IsVisibleInFilter,
                    Items               = itemlist
                };
                          
                TagDataList.Add(tagName, tagdata);
            }

            // writes to json file
            string jsonString = JsonConvert.SerializeObject(new { tags = TagDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("Tags", jsonString);

        }
	}
}
