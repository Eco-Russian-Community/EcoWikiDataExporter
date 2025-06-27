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
                    TreeData[treeName]["IdealTemperatureRangeMin"] = $"'{EcoWikiDataManager.WorldTemp(tree.IdealTemperatureRange.Min)}'";
                    TreeData[treeName]["IdealTemperatureRangeMax"] = $"'{EcoWikiDataManager.WorldTemp(tree.IdealTemperatureRange.Max)}'";
                    TreeData[treeName]["ExtremeTemperatureRangeMin"] = $"'{EcoWikiDataManager.WorldTemp(tree.TemperatureExtremes.Min)}'";
                    TreeData[treeName]["ExtremeTemperatureRangeMax"] = $"'{EcoWikiDataManager.WorldTemp(tree.TemperatureExtremes.Max)}'";

                    TreeData[treeName]["IdealMoistureRangeMin"] = $"'{(Math.Round(tree.IdealMoistureRange.Min * 100)).ToString("G", CultureInfo.InvariantCulture)}'";
                    TreeData[treeName]["IdealMoistureRangeMax"] = $"'{(Math.Round(tree.IdealMoistureRange.Max * 100)).ToString("G", CultureInfo.InvariantCulture)}'";
                    TreeData[treeName]["ExtremeMoistureRangeMin"] = $"'{(Math.Round(tree.MoistureExtremes.Min * 100)).ToString("G", CultureInfo.InvariantCulture)}'";
                    TreeData[treeName]["ExtremeMoistureRangeMax"] = $"'{(Math.Round(tree.MoistureExtremes.Max * 100)).ToString("G", CultureInfo.InvariantCulture)}'";

                    TreeData[treeName]["IdealWaterRangeMin"] = $"'{(Math.Round(tree.IdealWaterRange.Min * 100)).ToString("G", CultureInfo.InvariantCulture)}'";
                    TreeData[treeName]["IdealWaterRangeMax"] = $"'{(Math.Round(tree.IdealWaterRange.Max * 100)).ToString("G", CultureInfo.InvariantCulture)}'";
                    TreeData[treeName]["ExtremeWaterRangeMin"] = $"'{(Math.Round(tree.WaterExtremes.Min * 100)).ToString("G", CultureInfo.InvariantCulture)}'";
                    TreeData[treeName]["ExtremeWaterRangeMax"] = $"'{(Math.Round(tree.WaterExtremes.Max * 100)).ToString("G", CultureInfo.InvariantCulture)}'";

                    TreeData[treeName]["PollutionDensityTolerance"] = $"'{(Math.Round(tree.PollutionDensityTolerance * 100)).ToString("G", CultureInfo.InvariantCulture)}'";
                    TreeData[treeName]["PollutionDensityMax"] = $"'{(Math.Round(tree.MaxPollutionDensity * 100)).ToString("G", CultureInfo.InvariantCulture)}'";

                    // Climate
                    TreeData[treeName]["ReleasesCO2TonsPerDay"] = $"'{tree.ReleasesCO2TonsPerDay}'";

                }
            }
        // writes to txt file
        EcoWikiDataManager.WriteDictionaryToFile("TreeData", "trees", TreeData);
        }
    }
}
