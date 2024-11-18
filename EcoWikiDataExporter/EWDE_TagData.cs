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
                { "Name","nil" }
            };


            IEnumerable<Tag> tags = TagManager.AllTags;

            foreach (Tag tag in tags)
            {

                if (!TreeData.ContainsKey(tag.Name))
                {
                    string tagName = tag.Name;
                    
                }

            }

        // writes to txt file
        EcoWikiDataManager.WriteDictionaryToFile("TagData", "tag", TagData);
        }
    }
}
