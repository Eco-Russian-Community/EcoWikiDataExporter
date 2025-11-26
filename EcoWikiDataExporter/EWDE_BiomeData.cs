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
            { "Description","nil" },
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
                string BiomeID = Biometype.Name;
                string BiomeLayer = BiomeID + "Biome";
                if (BiomeLayer == "RainForestBiome") { BiomeLayer = "RainforestBiome"; }
                string BiomeNameLoc = "";

                if ((BiomeLayer != "CoastBiome") && (BiomeLayer != "ColdCoastBiome") && (BiomeLayer != "WarmCoastBiome") && (BiomeLayer != "HighDesertBiome") && (BiomeLayer != "SteppeBiome"))
                {
                    var WorldLayer = WorldLayerManager.Obj.GetLayer(BiomeLayer);
                    BiomeLayerSettings WorldLayerSettings = WorldLayer.Settings as BiomeLayerSettings;
                    BiomeNameLoc = WorldLayerSettings.MinimapName;
                }
                else
                {
                    BiomeNameLoc = BiomeName + " Biome";
                }
                
                BiomeData.Add(BiomeName, new Dictionary<string, string>(biomeDetails));
                
                BiomeData[BiomeName]["ID"] = $"'{BiomeID}'";
                BiomeData[BiomeName]["WorldLayer"] = $"'{BiomeLayer}'";
                BiomeData[BiomeName]["Color"] = $"'{BiomeItem.Color.Name}'";

                if (BiomeExtensions.CanSpawnLake(BiomeItem)) { BiomeData[BiomeName]["CanSpawnLake"] = $"'True'";  }
                if (BiomeExtensions.IsOcean(BiomeItem)) { BiomeData[BiomeName]["IsOcean"] = $"'True'"; }
                

                BiomeData[BiomeName]["Name"] = WriteDictionaryAsSubObject(Localization(BiomeNameLoc), 1);

                string BiomeDescription = "";
                if (BiomeID == "Coast") { BiomeDescription = "Beaches and shallow waters."; }
                if (BiomeID == "ColdCoast") { BiomeDescription = "Beaches and shallow waters in colder climates."; }
                if (BiomeID == "ColdForest") { BiomeDescription = "A biome wet enough to support dense tree cover, and cold enough that conniferous trees tend to dominate."; }
                if (BiomeID == "DeepOcean") { BiomeDescription = "The deepest parts of the ocean, bearing the largest marine species and migrating populations of fish."; }
                if (BiomeID == "Desert") { BiomeDescription = "The dryest biome, and in Eco currently the hottest as well."; }
                if (BiomeID == "Grassland") { BiomeDescription = "A biome where there is enough water to support grasses but not many trees."; }
                if (BiomeID == "HighDesert") { BiomeDescription = "A geologically uplifted part of the desert with species more tolerant to cold and that enjoy the steep cliffs caused by erosion."; }
                if (BiomeID == "Ice") { BiomeDescription = "The coldest areas of the land, covered by glacial ice."; }
                if (BiomeID == "Ocean") { BiomeDescription = "The parts of the ocean on the continental shelves, abundant with life."; }
                if (BiomeID == "RainForest") { BiomeDescription = "A wet and warm biome that supports a forest abundant with plant and animal life."; }
                if (BiomeID == "Steppe") { BiomeDescription = "A slightly colder grassland, often characterized by even fewer trees and larger open spaces."; }
                if (BiomeID == "Taiga") { BiomeDescription = "A cold and somewhat dry biome that supports a sparse forest of conniferous trees."; }
                if (BiomeID == "Tundra") { BiomeDescription = "A very cold biome that cannot support trees but is host to a unique plant and animal community."; }
                if (BiomeID == "WarmCoast") { BiomeDescription = "Beaches and shallow waters in warmer climates."; }
                if (BiomeID == "WarmForest") { BiomeDescription = "A biome wet enough to support dense tree cover, and warm enough that broadleaf trees tend to dominate."; }
                if (BiomeID == "Wetland") { BiomeDescription = "A unique biome characterized by a saturation of the soil with fresh or brackish water."; }

                BiomeData[BiomeName]["Description"] = WriteDictionaryAsSubObject(Localization(BiomeDescription), 1);
                BiomeData[BiomeName]["PrevailingRockType"] = $"'{BiomeItem.PrevailingRockType.Name}'";
                //BiomeData[BiomeName]["ElevationRangeMin"] = $"'{BiomeItem.ElevationRange.Min}'";
                //BiomeData[BiomeName]["ElevationRangeMax"] = $"'{BiomeItem.ElevationRange.Max}'";
                BiomeData[BiomeName]["TemperatureRangeMin"] = $"'{WorldTemp(BiomeItem.TemperatureRange.Min)}'";
                BiomeData[BiomeName]["TemperatureRangeMax"] = $"'{WorldTemp(BiomeItem.TemperatureRange.Max)}'";
                BiomeData[BiomeName]["MoistureRangeMin"] = $"'{Percent(BiomeItem.MoistureRange.Min)}'";
                BiomeData[BiomeName]["MoistureRangeMax"] = $"'{Percent(BiomeItem.MoistureRange.Max)}'";
                //BiomeData[BiomeName]["UpperHeight"] = $"'{BiomeItem.UpperHeight}'";
                //BiomeData[BiomeName]["BadNeighbors"] = $"'{BiomeItem.BadNeighbors}'";
            }

         // writes to txt file
        WriteDictionaryToFile("BiomeData", "biomes", BiomeData);
        }
    }
}
