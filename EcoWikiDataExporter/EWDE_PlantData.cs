using Eco.Core.Controller;
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
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using Eco.Simulation;
using Eco.Simulation.Agents;
using Eco.Simulation.Types;
using Eco.World.Blocks;
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

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        // dictionary of plants and their dictionary of stats
        private static SortedDictionary<string, PlantData> PlantDataList = new SortedDictionary<string, PlantData>();

        public static void ExportPlantData()
        {

            IEnumerable<Species> species = EcoSim.AllSpecies.Where(s => s is PlantSpecies && s is not TreeSpecies);
            foreach (Species s in species)
            {
                PlantSpecies plant = s as PlantSpecies;
                string PlantName = plant.DisplayName.NotTranslated;
                if (!PlantDataList.ContainsKey(PlantName))
                {

                    // PlantData[plantName]["CalorieValue"] = $"'{plant.CalorieValue}'";

                    if (plant.ResourceItemType != null)
                    {
                        // TreeData[treeName]["ResourceItem"] = $"'{plant.ResourceItemType.Name}'";
                        // TreeData[treeName]["ResourceMax"] = $"'{plant.ResourceRange.Max}'";
                    }



                    PlantData plantdata = new PlantData
                    {
                        ID = plant.Name + "Species",
                        Name = Localization(PlantName),
                        Height = plant.Height,
                        MaturityAgeDays = WikiFloat(plant.MaturityAgeDays),
                        StartBiome = plant.GenerationDefinitions.StartBiomes,
                        WaterPlant = plant.Water ? true : false,
                        IsHarvestable = plant.RequireHarvestable ? true : false,
                        IsDiggable = Block.Is<Diggable>(plant.BlockType) ? true : false,
                        IsReapable = Block.Is<Reapable>(plant.BlockType) ? true : false,



                        ExtremeTemperatureRangeMin = WorldTemp(plant.TemperatureExtremes.Min),
                        IdealTemperatureRangeMin = WorldTemp(plant.IdealTemperatureRange.Min),
                        IdealTemperatureRangeMax = WorldTemp(plant.IdealTemperatureRange.Max),
                        ExtremeTemperatureRangeMax = WorldTemp(plant.TemperatureExtremes.Max),

                        ExtremeMoistureRangeMin = Percent(plant.MoistureExtremes.Min),
                        IdealMoistureRangeMin = Percent(plant.IdealMoistureRange.Min),
                        IdealMoistureRangeMax = Percent(plant.IdealMoistureRange.Max),
                        ExtremeMoistureRangeMax = Percent(plant.MoistureExtremes.Max),

                        ExtremeWaterRangeMin = Percent(plant.WaterExtremes.Min),
                        IdealWaterRangeMin = Percent(plant.IdealWaterRange.Min),
                        IdealWaterRangeMax = Percent(plant.IdealWaterRange.Max),
                        ExtremeWaterRangeMax = Percent(plant.WaterExtremes.Max),

                        PollutionMin = Percent(plant.PollutionDensityTolerance),
                        PollutionMax = Percent(plant.MaxPollutionDensity),

                        CO2TonsPerDay = WikiFloat(plant.ReleasesCO2TonsPerDay)
                    };

                    PlantDataList.Add(PlantName, plantdata);

                }

                // writes to json file
                string PlantDataListjsonString = JsonConvert.SerializeObject(new { plants = PlantDataList }, Formatting.Indented);
                WriteDictionaryToJsonFile("Plants", PlantDataListjsonString);

            }
        }
    }
}
