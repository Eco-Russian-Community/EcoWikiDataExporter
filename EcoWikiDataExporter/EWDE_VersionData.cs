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

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {

      private static SortedDictionary<string, Dictionary<string, string>> VersionData = new SortedDictionary<string, Dictionary<string, string>>();

        public static void ExportVersionData()
        {
         
		    // dictionary of commands
            Dictionary<string, string> EcoDetails = new Dictionary<string, string>()
            {
                { "Version", "nil" },
                { "VersionNumber", "nil" },
                { "FullInfo", "nil" },
            };

            VersionData["eco"] = EcoDetails;
            VersionData["eco"]["Version"] = $"'{EcoVersion.Version}'";
            VersionData["eco"]["VersionNumber"] = $"'{EcoVersion.VersionNumber}'";
            VersionData["eco"]["FullInfo"] = $"'{EcoVersion.FullInfo.Replace("\r\n", " ")}'";
            
            // writes to txt file
            EcoWikiDataManager.WriteDictionaryToFile("EcoVersionData", "eco", VersionData);
        }
    }
}
