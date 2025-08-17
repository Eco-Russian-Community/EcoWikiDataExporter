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
using Eco.Server;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.IoC;
using Eco.Shared.Localization;
using Eco.Shared.Logging;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using Eco.World.Blocks;
using Eco.WorldGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
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
		private static LocStringBuilder lsb = new LocStringBuilder();

		private static SortedDictionary<string, Dictionary<string, string>> GeologyData = new SortedDictionary<string, Dictionary<string, string>>();

        // Dictionary of biomes properties
        private static Dictionary<string, string> geologybiomeDetails = new Dictionary<string, string>() {};

        public static void ExportGeologyData()
		{

			PluginConfig<WorldSettings> config = new PluginConfig<WorldSettings>("WorldGenerator", true);
			WorldSettings worldSettings = config.Config;
			TerrainModule terrainModule = worldSettings.TerrainModule as TerrainModule;

			foreach (ITerrainModule module in terrainModule.Modules)
			{

				switch (module)
				{
					case BiomeTerrainModule biomeTerrainModule:
						{

							string BiomeName = biomeTerrainModule.BiomeName;

                            GeologyData.Add(BiomeName, new Dictionary<string, string>(geologybiomeDetails));

                            TerrainDepthModule terrainDepthModule = biomeTerrainModule.Module;
							foreach (BlockDepthRange blockDepthRange in terrainDepthModule.BlockDepthRanges)
							{
								var BiomeLayer = new Dictionary<string, string>();
                                string BiomeLayerName = blockDepthRange.BlockType.ToString();
                                
                                //BiomeLayer["BlockType"] = $"'{BiomeLayerName}'";
                                BiomeLayer["DepthRangeMin"] = $"'{blockDepthRange.Min}'";
                                BiomeLayer["DepthRangeMax"] = $"'{blockDepthRange.Max}'";
                                //BiomeLayer["NoiseFrequency"] = $"'{blockDepthRange.NoiseFrequency}'";
								
								foreach (ITerrainModule subModule in blockDepthRange.SubModules)
								{
                                    SortedDictionary<string, Dictionary<string, string>> LayerResources = new SortedDictionary<string, Dictionary<string, string>>();
                                    
                                    switch (subModule)
                                    {
                                        case StandardTerrainModule standardTerrainModule:
                                            {
                                                string LayerResourceName = standardTerrainModule.BlockType.ToString();
                                                LayerResources.Add(LayerResourceName, new Dictionary<string, string>(geologybiomeDetails));
                                                LayerResources[LayerResourceName]["LayerResourceType"] = "'Standard'";
                                                LayerResources[LayerResourceName]["DepthRangeMin"] = $"'{standardTerrainModule.DepthRange.Min.ToString()}'";
                                                LayerResources[LayerResourceName]["DepthRangeMax"] = $"'{standardTerrainModule.DepthRange.Max.ToString()}'";
                                                LayerResources[LayerResourceName]["PercentChance"] = $"'{Percent(standardTerrainModule.PercentChance)}'";
                                            }
                                            break;
                                        case DepositTerrainModule depositTerrainModule:
                                            {
                                                string LayerResourceName = depositTerrainModule.BlockType.ToString();
                                                LayerResources.Add(LayerResourceName, new Dictionary<string, string>(geologybiomeDetails));
                                                LayerResources[LayerResourceName]["LayerResourceType"] = "'Deposit'";
                                                LayerResources[LayerResourceName]["DepthRangeMin"] = $"'{depositTerrainModule.DepthRange.Min.ToString()}'";
                                                LayerResources[LayerResourceName]["DepthRangeMax"] = $"'{depositTerrainModule.DepthRange.Max.ToString()}'";

                                                LayerResources[LayerResourceName]["DepositDepthRangeMin"] = $"'{depositTerrainModule.DepositDepthRange.Min.ToString()}'";
                                                LayerResources[LayerResourceName]["DepositDepthRangeMax"] = $"'{depositTerrainModule.DepositDepthRange.Max.ToString()}'";

                                                LayerResources[LayerResourceName]["BlocksCountRangeMin"] = $"'{depositTerrainModule.BlocksCountRange.Min.ToString()}'";
                                                LayerResources[LayerResourceName]["BlocksCountRangeMax"] = $"'{depositTerrainModule.BlocksCountRange.Max.ToString()}'";

                                                LayerResources[LayerResourceName]["SpawnAtLeastOne"] = $"'{depositTerrainModule.SpawnAtLeastOne}'";
                                                LayerResources[LayerResourceName]["PercentChance"] = $"'{Percent(depositTerrainModule.SpawnPercentChance)}'";
                                            }
                                            break;
                                        default: Log.WriteWarningLineLoc($"Unknown TerrainModule of type [{terrainModule.GetType()}]"); break;
                                    }
                                    BiomeLayer["LayerResources"] = WriteDictionaryAsSubObject(LayerResources, 2);
                                }

                                GeologyData[BiomeName][BiomeLayerName] = WriteDictionaryAsSubObject(BiomeLayer, 1);
                            }
						}
						break;
					default: Log.WriteWarningLineLoc($"Unknown TerrainModule of type [{module.GetType()}]"); break;
				}
			}

            // writes to txt file
            WriteDictionaryToFile("GeologyData", "geology", GeologyData);
		}
	}
}
