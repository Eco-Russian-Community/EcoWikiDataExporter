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
using Eco.Simulation.Types;
using Eco.Simulation;
using Eco.Simulation.Agents;
using Eco.World.Blocks;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        // dictionary of plants and their dictionary of stats
        private static SortedDictionary<string, Dictionary<string, string>> PlantData = new SortedDictionary<string, Dictionary<string, string>>();

        public static void ExportPlantData()
        {
            // dictionary of plant properties
            Dictionary<string, string> plantDetails = new Dictionary<string, string>()
            {
                { "Name","nil" },
                { "MaturityAgeDays","nil" },
                { "StartBiomes","nil" },
                { "Height","nil" },
                { "isReapable","nil" },
                { "isDiggable","nil" }
            };

            IEnumerable<Species> species = EcoSim.AllSpecies.Where(s => s is PlantSpecies && s is not TreeSpecies);
            foreach (Species s in species)
            {
                PlantSpecies plant = s as PlantSpecies;
                string plantName = plant.DisplayName;
                if (!PlantData.ContainsKey(plantName))
                {
                        
                    PlantData.Add(plantName, new Dictionary<string, string>(plantDetails));
                    PlantData[plantName]["MaturityAgeDays"] = $"'{plant.MaturityAgeDays}'";
                    PlantData[plantName]["StartBiomes"] = $"'{plant.GenerationDefinitions.StartBiomes}'";
                    PlantData[plantName]["isWater"] = plant.Water ? $"'True'" : "nil";
                    PlantData[plantName]["isHarvestable"] = plant.RequireHarvestable ? $"'True'" : "nil";
                    if (Block.Is<Reapable>(plant.BlockType)) { PlantData[plantName]["isReapable"] = $"'True'"; }
                    if (Block.Is<Diggable>(plant.BlockType)) { PlantData[plantName]["isDiggable"] = $"'True'"; }
                    PlantData[plantName]["Height"] = $"'{plant.Height}'";
                    PlantData[plantName]["CalorieValue"] = $"'{plant.CalorieValue}'";

                    // Capacity
                    PlantData[plantName]["IdealTemperatureRangeMin"] = $"'{plant.IdealTemperatureRange.Min}'";
                    PlantData[plantName]["IdealTemperatureRangeMax"] = $"'{plant.IdealTemperatureRange.Max}'";
                    PlantData[plantName]["ExtremeTemperatureRangeMin"] = $"'{plant.TemperatureExtremes.Min}'";
                    PlantData[plantName]["ExtremeTemperatureRangeMax"] = $"'{plant.TemperatureExtremes.Max}'";

                    PlantData[plantName]["IdealMoistureRangeMin"] = $"'{plant.IdealMoistureRange.Min}'";
                    PlantData[plantName]["IdealMoistureRangeMax"] = $"'{plant.IdealMoistureRange.Max}'";
                    PlantData[plantName]["ExtremeMoistureRangeMin"] = $"'{plant.MoistureExtremes.Min}'";
                    PlantData[plantName]["ExtremeMoistureRangeMax"] = $"'{plant.MoistureExtremes.Max}'";

                    PlantData[plantName]["IdealWaterRangeMin"] = $"'{plant.IdealWaterRange.Min}'";
                    PlantData[plantName]["IdealWaterRangeMax"] = $"'{plant.IdealWaterRange.Max}'";
                    PlantData[plantName]["ExtremeWaterRangeMin"] = $"'{plant.WaterExtremes.Min}'";
                    PlantData[plantName]["ExtremeWaterRangeMax"] = $"'{plant.WaterExtremes.Max}'";

                    PlantData[plantName]["PollutionDensityTolerance"] = $"'{plant.PollutionDensityTolerance}'";
                    PlantData[plantName]["PollutionDensityMax"] = $"'{plant.MaxPollutionDensity}'";

                    // Climate
                    PlantData[plantName]["ReleasesCO2TonsPerDay"] = $"'{plant.ReleasesCO2TonsPerDay}'";
                }
                
             // writes to txt file
             EcoWikiDataManager.WriteDictionaryToFile("PlantData", "plants", PlantData);
            }
        }
    }
}
