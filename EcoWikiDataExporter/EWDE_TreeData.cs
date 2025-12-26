using Eco.Core.Items;
using Eco.Core.Systems;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Civics.Districts;
using Eco.Gameplay.Civics.Misc;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Properties;
using Eco.Shared.Utils;
using Eco.Simulation;
using Eco.Simulation.Agents;
using Eco.Simulation.Types;
using Eco.Simulation.WorldLayers;
using Eco.World;
using Eco.World.Blocks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static Eco.Simulation.Types.PlantSpecies;
using Organism = Eco.Simulation.Agents.Organism;


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
                    // Info
                    TreeData[treeName]["ID"] = $"'{tree.Name}" + "Species'";
                    TreeData[treeName]["Name"] = WriteDictionaryAsSubObject(Localization(treeName), 1);
                    TreeData[treeName]["IsDecorative"] = $"'{tree.Decorative}'";
                    // Lifetime
                    TreeData[treeName]["MaturityAgeDays"] = $"'{tree.MaturityAgeDays}'";
                    TreeData[treeName]["TreeHealth"] = $"'{tree.TreeHealth}'";
                    TreeData[treeName]["BranchCount"] = $"'{tree.BranchingDef.Count}'";
                    TreeData[treeName]["Density"] = $"'{tree.Density}'";

                    // Seeding and spread customization
                    TreeData[treeName]["SeedingTime"] = $"'{tree.SeedingTime}'";
                    TreeData[treeName]["SeedingArea"] = $"'{tree.SeedingArea}'";
                    TreeData[treeName]["PlantAgeToSeed"] = $"'{tree.PlantAgeToSeed}'";
                    TreeData[treeName]["SeedsCount"] = $"'{tree.SeedsCount}'";
                    // Generation
                    TreeData[treeName]["Height"] = $"'{tree.Height}'";
                    TreeData[treeName]["ChanceToBeSpawnOutsideOfGroup"] = $"'{tree.GenerationDefinitions.ChanceToBeSpawnOutsideOfGroup}'";
                    TreeData[treeName]["MinDistanceBetweenGroups"] = $"'{tree.GenerationDefinitions.MinDistanceBetweenGroups}'";
                    TreeData[treeName]["PlantsInGroup"] = $"'{tree.GenerationDefinitions.PlantsInGroup}'";
                    TreeData[treeName]["CountOfClusters"] = $"'{tree.GenerationDefinitions.CountOfClusters}'";
                    TreeData[treeName]["RadiusOfGroup"] = $"'{tree.GenerationDefinitions.RadiusOfGroup}'";
                    TreeData[treeName]["ClusterRadiusInWorldSize"] = $"'{tree.GenerationDefinitions.ClusterRadiusInWorldSize}'";
                    TreeData[treeName]["StartBiomes"] = $"'{tree.GenerationDefinitions.StartBiomes}'";
                    // as Food
                    TreeData[treeName]["CalorieValue"] = $"'{tree.CalorieValue}'";
                    // Resources
                    TreeData[treeName]["PostHarvestingGrowth"] = $"'{tree.PostHarvestingGrowth}'";
                    TreeData[treeName]["PickableAtPercent"] = $"'{tree.PickableAtPercent}'";
                    TreeData[treeName]["ResourceBonusAtGrowth"] = $"'{tree.ResourceBonusAtGrowth}'";
                    TreeData[treeName]["LogHealth"] = $"'{tree.LogHealth}'";
                    TreeData[treeName]["ChanceToSpawnDebris"] = $"'{Percent(tree.ChanceToSpawnDebris)}'";
                    TreeData[treeName]["DebrisType"] = $"'{tree.DebrisType.Name}'";

                    if (tree.ResourceItemType != null) 
                    { 
                        TreeData[treeName]["ResourceItem"] = $"'{tree.ResourceItemType.Name}'";
                        TreeData[treeName]["ResourceMax"] = $"'{tree.ResourceRange.Max}'";
                    }

                    
                    foreach (var debrisresources in tree.DebrisResources)
                    {
                        
                        //debrisresources.Key
                        //debrisresources.Value.Min
                        //debrisresources.Value.Max
                    }

                    foreach (var trunkresources in tree.TrunkResources)
                    {
                        //trunkresources.Key
                    }

                    // WorldLayers
                    if (tree.ResourceConstraints != null)
                    {
                        foreach (ResourceConstraint resource in tree.ResourceConstraints)
                        {
                            string LayerName = resource.LayerName;
                            TreeData[treeName][LayerName + "HalfSpeed"] = $"'{Percent(resource.HalfSpeedConcentration)}'";
                            TreeData[treeName][LayerName + "MaxResource"] = $"'{Percent(resource.MaxResourceContent)}'";
                        }
                    }

                    TreeData[treeName]["BlockType"] = $"'{tree.BlockType.Name}'";
                    

                     





                     // Capacity
                    TreeData[treeName]["IdealTemperatureRangeMin"] = $"'{WorldTemp(tree.IdealTemperatureRange.Min)}'";
                    TreeData[treeName]["IdealTemperatureRangeMax"] = $"'{WorldTemp(tree.IdealTemperatureRange.Max)}'";
                    TreeData[treeName]["ExtremeTemperatureRangeMin"] = $"'{WorldTemp(tree.TemperatureExtremes.Min)}'";
                    TreeData[treeName]["ExtremeTemperatureRangeMax"] = $"'{WorldTemp(tree.TemperatureExtremes.Max)}'";

                    TreeData[treeName]["IdealMoistureRangeMin"] = $"'{Percent(tree.IdealMoistureRange.Min)}'";
                    TreeData[treeName]["IdealMoistureRangeMax"] = $"'{Percent(tree.IdealMoistureRange.Max)}'";
                    TreeData[treeName]["ExtremeMoistureRangeMin"] = $"'{Percent(tree.MoistureExtremes.Min)}'";
                    TreeData[treeName]["ExtremeMoistureRangeMax"] = $"'{Percent(tree.MoistureExtremes.Max)}'";

                    TreeData[treeName]["IdealWaterRangeMin"] = $"'{Percent(tree.IdealWaterRange.Min)}'";
                    TreeData[treeName]["IdealWaterRangeMax"] = $"'{Percent(tree.IdealWaterRange.Max)}'";
                    TreeData[treeName]["ExtremeWaterRangeMin"] = $"'{Percent(tree.WaterExtremes.Min)}'";
                    TreeData[treeName]["ExtremeWaterRangeMax"] = $"'{Percent(tree.WaterExtremes.Max)}'";

                    // Climate
                    TreeData[treeName]["ReleasesCO2TonsPerDay"] = $"'{tree.ReleasesCO2TonsPerDay.ToString("G", CultureInfo.InvariantCulture)}'";
                    TreeData[treeName]["PollutionDensityTolerance"] = $"'{Percent(tree.PollutionDensityTolerance)}'";
                    TreeData[treeName]["PollutionDensityMax"] = $"'{Percent(tree.MaxPollutionDensity)}'";
                }
            }
        // writes to txt file
        WriteDictionaryToFile("TreeData", "trees", TreeData);
        }
    }
}
