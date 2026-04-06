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
using Eco.Simulation.Settings;
using Eco.WorldGenerator;
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

      private static SortedDictionary<string, Dictionary<string, string>> ServerConfigData = new SortedDictionary<string, Dictionary<string, string>>();

        public static void ExportServerConfigData()
        {
         
		    // dictionary of commands
            Dictionary<string, string> ServerConfigDetails = new Dictionary<string, string>() {};
            Dictionary<string, string> ServerSubConfigDetails = new Dictionary<string, string>() {};

            //BalanceConfig
            ServerConfigData.Add("Balance", new Dictionary<string, string>(ServerConfigDetails));
            ServerConfigData["Balance"]["CalorieMultiplierOnMove"] = $"'{BalanceConfig.Obj.CalorieMultiplierOnMove}'";
            ServerConfigData["Balance"]["CraftingQueueQuantity"] = $"'{BalanceConfig.Obj.CraftingQueueQuantity}'";
            ServerConfigData["Balance"]["MaintenanceDecayPercentage"] = $"'{BalanceConfig.Obj.MaintenanceDecayPercentage}'";
            ServerConfigData["Balance"]["VehicleMaintenanceDecayPercentage"] = $"'{BalanceConfig.Obj.VehicleMaintenanceDecayPercentage}'";
            ServerConfigData["Balance"]["ToolRepairPenalty"] = $"'{BalanceConfig.Obj.ToolRepairPenalty}'";
            ServerConfigData["Balance"]["ShowOilLayer"] = $"'{BalanceConfig.Obj.ShowOilLayer}'";

            ServerConfigData["Balance"]["SpecialtyExperiencePerLevelSquared"] = $"'{BalanceConfig.Obj.SpecialtyExperiencePerLevelSquared.ToString()}'";

            //


            //EcoSim
            ServerConfigData.Add("EcoSim", new Dictionary<string, string>(ServerConfigDetails));
            ServerConfigData["EcoSim"]["PPMPerTon"] = $"'{EcoDef.Obj.ClimateSettings.PPMPerTon}'";
            ServerConfigData["EcoSim"]["MinCO2ppm"] = $"'{EcoDef.Obj.ClimateSettings.MinCO2ppm}'";
            ServerConfigData["EcoSim"]["SeaLevelsRiseAtCO2ppm"] = $"'{EcoDef.Obj.ClimateSettings.SeaLevelsRiseAtCO2ppm}'";
            ServerConfigData["EcoSim"]["CO2ppmPerSeaLevelMeterRise"] = $"'{EcoDef.Obj.ClimateSettings.CO2ppmPerSeaLevelMeterRise}'";
            ServerConfigData["EcoSim"]["MaxSeaLevelRise"] = $"'{EcoDef.Obj.ClimateSettings.MaxSeaLevelRise}'";
            ServerConfigData["EcoSim"]["MaxTemperatureRise"] = $"'{EcoDef.Obj.ClimateSettings.MaxTemperatureRise}'";
            ServerConfigData["EcoSim"]["MaxSeaLevelRiseRatePerDay"] = $"'{EcoDef.Obj.ClimateSettings.MaxSeaLevelRiseRatePerDay}'";
            ServerConfigData["EcoSim"]["MaxTemperatureChangePerDay"] = $"'{EcoDef.Obj.ClimateSettings.MaxTemperatureChangePerDay}'";
            ServerConfigData["EcoSim"]["TemperaturesRiseAtCO2ppm"] = $"'{EcoDef.Obj.ClimateSettings.TemperaturesRiseAtCO2ppm}'";
            ServerConfigData["EcoSim"]["CO2ppmPerDegreeTemperatureRise"] = $"'{EcoDef.Obj.ClimateSettings.CO2ppmPerDegreeTemperatureRise}'";
            ServerConfigData["EcoSim"]["MaxCO2PerDayFromAnimals"] = $"'{EcoDef.Obj.ClimateSettings.MaxCO2PerDayFromAnimals}'";
            ServerConfigData["EcoSim"]["MinCO2PerDayFromPlants"] = $"'{EcoDef.Obj.ClimateSettings.MinCO2PerDayFromPlants}'";
            ServerConfigData["EcoSim"]["PollutionPerTailingPerTick"] = $"'{EcoDef.Obj.ClimateSettings.PollutionPerTailingPerTick}'";
            ServerConfigData["EcoSim"]["TailingsPollutionUndergroundHalvingDistance"] = $"'{EcoDef.Obj.ClimateSettings.TailingsPollutionUndergroundHalvingDistance}'";
            ServerConfigData["EcoSim"]["MaxDebrisBlocks"] = $"'{EcoDef.Obj.ClimateSettings.MaxDebrisBlocks}'";

            ServerConfigData["EcoSim"]["MinTreeSpawnDistance"] = $"'{EcoSim.Obj.EcoDef.MinTreeSpawnDistance}'";
            ServerConfigData["EcoSim"]["BaseSkillGainRate"] = $"'{EcoSim.Obj.EcoDef.BaseSkillGainRate}'";
            ServerConfigData["EcoSim"]["TimeOfDayScale"] = $"'{EcoSim.Obj.EcoDef.TimeOfDayScale}'";

            //WorldGenerator
            ServerConfigData.Add("WorldGenerator", new Dictionary<string, string>(ServerConfigDetails));
            ServerConfigData["WorldGenerator"]["WaterLevel"] = $"'{WorldGeneratorPlugin.Settings.WaterLevel}'";
            ServerConfigData["WorldGenerator"]["MaxGenerationHeight"] = $"'{WorldGeneratorPlugin.Settings.MaxGenerationHeight}'";
            ServerConfigData["WorldGenerator"]["MaxBuildHeight"] = $"'{WorldGeneratorPlugin.Settings.MaxBuildHeight}'";

            // writes to txt file
            WriteDictionaryToFile("ServerConfigData", "config", ServerConfigData);
        }
    }
}
