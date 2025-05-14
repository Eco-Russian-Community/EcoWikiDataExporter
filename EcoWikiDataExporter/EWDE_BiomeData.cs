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
using Eco.WorldGenerator;
using Eco.Shared.Logging;
using Eco.Core.Items;
using Eco.Gameplay.Systems.EcoMarketplace;
using Eco.Mods.WorldLayers;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        // Dictionary of tags
        private static SortedDictionary<string, Dictionary<string, string>> BiomeData = new SortedDictionary<string, Dictionary<string, string>>();

        // Dictionary of tags properties
        private static Dictionary<string, string> biomeDetails = new Dictionary<string, string>()
        {
            { "ID","nil" },
            { "Color","nil" },
            { "CanSpawnLake","nil" },
            { "IsOcean","nil" }

        };

        public static void ExportBiomeData()
        {


            foreach (var Biometype in typeof(Biome).DerivedTypes())
            {
                var BiomeItem = Activator.CreateInstance(Biometype) as Biome;
                string BiomeName = Biometype.Name;
                
                BiomeData.Add(BiomeName, new Dictionary<string, string>(biomeDetails));
                
                BiomeData[BiomeName]["ID"] = $"'{BiomeItem.GetType}'";
                BiomeData[BiomeName]["Color"] = $"'{BiomeItem.Color}'";

                if (BiomeExtensions.CanSpawnLake(BiomeItem)) { BiomeData[BiomeName]["CanSpawnLake"] = $"'True'";  }
                if (BiomeExtensions.IsOcean(BiomeItem)) { BiomeData[BiomeName]["IsOcean"] = $"'True'"; }

                BiomeData[BiomeName]["PrevailingRockType"] = $"'{BiomeItem.PrevailingRockType}'";
                BiomeData[BiomeName]["ElevationRangeMin"] = $"'{BiomeItem.ElevationRange.Min}'";
                BiomeData[BiomeName]["ElevationRangeMax"] = $"'{BiomeItem.ElevationRange.Max}'";
                BiomeData[BiomeName]["TemperatureRangeMin"] = $"'{BiomeItem.TemperatureRange.Min}'";
                BiomeData[BiomeName]["TemperatureRangeMax"] = $"'{BiomeItem.TemperatureRange.Max}'";
                BiomeData[BiomeName]["MoistureRangeMin"] = $"'{BiomeItem.MoistureRange.Min * 100 }'";
                BiomeData[BiomeName]["MoistureRangeMax"] = $"'{BiomeItem.MoistureRange.Max * 100 }'";
                BiomeData[BiomeName]["UpperHeight"] = $"'{BiomeItem.UpperHeight}'";
                //BiomeData[BiomeName]["BadNeighbors"] = $"'{BiomeItem.BadNeighbors}'";

                
            }

         // writes to txt file
        EcoWikiDataManager.WriteDictionaryToFile("BiomeData", "biomes", BiomeData);
        }
    }
}
