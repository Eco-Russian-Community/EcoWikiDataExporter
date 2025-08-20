using Eco.Core.Controller;
using Eco.Core.Items;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.EcoMarketplace;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Mods.WorldLayers;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.IoC;
using Eco.Shared.Localization;
using Eco.Shared.Logging;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using Eco.Simulation.Types;
using Eco.Simulation.WorldLayers;
using Eco.Simulation.WorldLayers.Layers;
using Eco.WorldGenerator;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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
        // Dictionary of tags
        private static SortedDictionary<string, Dictionary<string, string>> BiomeData = new SortedDictionary<string, Dictionary<string, string>>();

        // Dictionary of biomes properties
        private static Dictionary<string, string> biomeDetails = new Dictionary<string, string>()
        {
            { "ID","nil" },
            { "Name","nil" },
            { "Color","nil" },
            { "CanSpawnLake","nil" },
            { "IsOcean","nil" }

        };

        public static void ExportBiomeData()
        {
            foreach (var Biometype in typeof(Biome).DerivedTypes())
            {
                var BiomeItem = Activator.CreateInstance(Biometype) as Biome;
                string BiomeName = Biometype.GetLocDisplayName();
                string BiomeID = BiomeName.Replace(" ", "") + "Biome";
                //string BiomeNameLoc = BiomeName + " Biome";

                
                var WorldLayer = WorldLayerManager.Obj.GetLayer(BiomeID);
                BiomeLayerSettings WorldLayerSettings = WorldLayer.Settings as BiomeLayerSettings;
                string BiomeNameLoc = WorldLayerSettings.MinimapName;


                Log.WriteWarningLineLoc($"Biome Layer: {BiomeNameLoc}");

                BiomeData.Add(BiomeName, new Dictionary<string, string>(biomeDetails));
                
                BiomeData[BiomeName]["ID"] = $"'{BiomeID}'";
                BiomeData[BiomeName]["Color"] = $"'{BiomeItem.Color.Name}'";

                if (BiomeExtensions.CanSpawnLake(BiomeItem)) { BiomeData[BiomeName]["CanSpawnLake"] = $"'True'";  }
                if (BiomeExtensions.IsOcean(BiomeItem)) { BiomeData[BiomeName]["IsOcean"] = $"'True'"; }

                BiomeData[BiomeName]["Name"] = WriteDictionaryAsSubObject(Localization(BiomeNameLoc), 1);
                BiomeData[BiomeName]["PrevailingRockType"] = $"'{BiomeItem.PrevailingRockType.Name}'";
                BiomeData[BiomeName]["ElevationRangeMin"] = $"'{BiomeItem.ElevationRange.Min}'";
                BiomeData[BiomeName]["ElevationRangeMax"] = $"'{BiomeItem.ElevationRange.Max}'";
                BiomeData[BiomeName]["TemperatureRangeMin"] = $"'{WorldTemp(BiomeItem.TemperatureRange.Min)}'";
                BiomeData[BiomeName]["TemperatureRangeMax"] = $"'{WorldTemp(BiomeItem.TemperatureRange.Max)}'";
                BiomeData[BiomeName]["MoistureRangeMin"] = $"'{Percent(BiomeItem.MoistureRange.Min)}'";
                BiomeData[BiomeName]["MoistureRangeMax"] = $"'{Percent(BiomeItem.MoistureRange.Max)}'";
                BiomeData[BiomeName]["UpperHeight"] = $"'{BiomeItem.UpperHeight}'";
                //BiomeData[BiomeName]["BadNeighbors"] = $"'{BiomeItem.BadNeighbors}'";
            }

         // writes to txt file
        WriteDictionaryToFile("BiomeData", "biomes", BiomeData);
        }
    }
}
