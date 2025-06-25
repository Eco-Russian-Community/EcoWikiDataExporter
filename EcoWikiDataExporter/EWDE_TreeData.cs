using Eco.Gameplay.Systems.Chat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Eco.Simulation.Types;
using Eco.Simulation;
using Eco.World.Blocks;
using Eco.Shared.Utils;
using System.Linq;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using System.Globalization;
using Eco.Core.Systems;
using Eco.Gameplay.Civics.Districts;
using Eco.Gameplay.Civics.Misc;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Shared.Math;
using Eco.Simulation.Agents;
using Eco.Simulation.WorldLayers;
using Eco.World;
using Organism = Eco.Simulation.Agents.Organism;
using static Eco.Simulation.Types.PlantSpecies;
using Eco.Core.Items;


namespace Eco.Mods.EcoWikiDataExporter
{
    public partial class WikiData
    {
        // Dictionary of trees
        private static SortedDictionary<string, Dictionary<string, string>> TreeData = new SortedDictionary<string, Dictionary<string, string>>();

        public static void ExportTreeData()
        {
            // Dictionary of trees properties
            Dictionary<string, string> treeDetails = new Dictionary<string, string>()
            {
                { "ID","nil" },
                { "Name","nil" },
                { "MaturityAgeDays","nil" },
                { "StartBiomes","nil" }               
            };

            IEnumerable<Species> species = EcoSim.AllSpecies.Where(s => s is TreeSpecies);

            foreach (Species s in species)
            {
                TreeSpecies tree = s as TreeSpecies;
                string treeName = tree.DisplayName.NotTranslated;
                if (!TreeData.ContainsKey(treeName))
                {
                    TreeData.Add(treeName, new Dictionary<string, string>(treeDetails));
                    TreeData[treeName]["ID"] = $"'{tree.Name}" + "Species'";
                    TreeData[treeName]["Name"] = EcoWikiDataManager.WriteDictionaryAsSubObject(EcoWikiDataManager.Localization(treeName), 1);
                    // Lifetime
                    TreeData[treeName]["MaturityAgeDays"] = $"'{tree.MaturityAgeDays}'";
                    // Seeding
                    TreeData[treeName]["SeedingTime"] = $"'{tree.SeedingTime}'";
                    TreeData[treeName]["SeedingArea"] = $"'{tree.SeedingArea}'";
                    TreeData[treeName]["PlantAgeToSeed"] = $"'{tree.PlantAgeToSeed}'";
                    TreeData[treeName]["SeedsCount"] = $"'{tree.SeedsCount}'";



                    TreeData[treeName]["StartBiomes"] = $"'{tree.GenerationDefinitions.StartBiomes}'";

                    //tree.ResourceConstraints.LayerName
                    //tree.ResourceConstraints.HalfSpeedConcentration
                    //tree.ResourceConstraints.MaxResourceContent


                    // Capacity
                    TreeData[treeName]["IdealTemperatureRangeMin"] = $"'{tree.IdealTemperatureRange.Min}'";
                    TreeData[treeName]["IdealTemperatureRangeMax"] = $"'{tree.IdealTemperatureRange.Max}'";
                    TreeData[treeName]["ExtremeTemperatureRangeMin"] = $"'{tree.TemperatureExtremes.Min}'";
                    TreeData[treeName]["ExtremeTemperatureRangeMax"] = $"'{tree.TemperatureExtremes.Max}'";

                    TreeData[treeName]["IdealMoistureRangeMin"] = $"'{tree.IdealMoistureRange.Min}'";
                    TreeData[treeName]["IdealMoistureRangeMax"] = $"'{tree.IdealMoistureRange.Max}'";
                    TreeData[treeName]["ExtremeMoistureRangeMin"] = $"'{tree.MoistureExtremes.Min}'";
                    TreeData[treeName]["ExtremeMoistureRangeMax"] = $"'{tree.MoistureExtremes.Max}'";

                    TreeData[treeName]["IdealWaterRangeMin"] = $"'{tree.IdealWaterRange.Min}'";
                    TreeData[treeName]["IdealWaterRangeMax"] = $"'{tree.IdealWaterRange.Max}'";
                    TreeData[treeName]["ExtremeWaterRangeMin"] = $"'{tree.WaterExtremes.Min}'";
                    TreeData[treeName]["ExtremeWaterRangeMax"] = $"'{tree.WaterExtremes.Max}'";

                    TreeData[treeName]["PollutionDensityTolerance"] = $"'{tree.PollutionDensityTolerance}'";
                    TreeData[treeName]["PollutionDensityMax"] = $"'{tree.MaxPollutionDensity}'";

                    // Climate
                    TreeData[treeName]["ReleasesCO2TonsPerDay"] = $"'{tree.ReleasesCO2TonsPerDay}'";

                }
            }
        // writes to txt file
        EcoWikiDataManager.WriteDictionaryToFile("TreeData", "trees", TreeData);
        }
    }
}
