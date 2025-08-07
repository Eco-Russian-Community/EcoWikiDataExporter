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
using Eco.Gameplay.Players;
using Eco.Gameplay.Rooms;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.ModKit.Internal;
using Eco.Mods.TechTree;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.IoC;
using Eco.Shared.Localization;
using Eco.Shared.Logging;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using Eco.WorldGenerator;
using Newtonsoft.Json;
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

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        private static SortedDictionary<string, Dictionary<string, string>> GeologyData = new SortedDictionary<string, Dictionary<string, string>>();
        public static void ExportGeologyData()
        {

            string WGFile = "WorldGenerator.eco";
            string filePath = "Configs" + $@"\" + WGFile;

            // Read the JSON file
            string jsonArrayContent = File.ReadAllText(filePath);





        // Deserialize the JSON array into a List of objects
        var objectList = JsonConvert.DeserializeObject<List<WGMain>>(jsonArrayContent);

            foreach (var obj in objectList)
            {
                Log.WriteWarningLineLoc($"Test Export Geology: {obj.ConfigVersion}");
                // Add additional parsing logic as needed
            }

           

            // writes to txt file
            WriteDictionaryToFile("GeologyData", "geology", GeologyData);
        }

        class WGMain
        {
            public string ConfigVersion { get; set; }

            // Add additional properties as needed
        }
    }
}
