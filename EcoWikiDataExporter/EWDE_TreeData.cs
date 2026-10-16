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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using static Eco.Simulation.Types.PlantSpecies;
using Organism = Eco.Simulation.Agents.Organism;


namespace Eco.Mods.EcoWikiDataExporter
{
    public partial class WikiData
    {
        // Dictionary of trees
        private static SortedDictionary<string, TreeData> TreeDataList = new SortedDictionary<string, TreeData>();

        public static void ExportTreeData()
        {
            IEnumerable<Species> species = EcoSim.AllSpecies.Where(s => s is TreeSpecies);

            foreach (Species s in species)
            {
                TreeSpecies tree = s as TreeSpecies;
                string TreeName = tree.DisplayName.NotTranslated;
                if (!TreeDataList.ContainsKey(TreeName))
                {

                    // Lifetime

                    // TreeData[treeName]["BranchCount"] = $"'{tree.BranchingDef.Count}'";
                    // TreeData[treeName]["Density"] = $"'{tree.Density}'";

                    // Seeding and spread customization
                    // TreeData[treeName]["SeedingTime"] = $"'{WikiDouble(tree.SeedingTime)}'";
                    // TreeData[treeName]["SeedingArea"] = $"'{tree.SeedingArea}'";
                    // TreeData[treeName]["PlantAgeToSeed"] = $"'{WikiFloat(tree.PlantAgeToSeed)}'";
                    // TreeData[treeName]["SeedsCount"] = $"'{tree.SeedsCount}'";

                    // Generation
                    // TreeData[treeName]["Height"] = $"'{tree.Height}'";
                    // TreeData[treeName]["ChanceToBeSpawnOutsideOfGroup"] = $"'{WikiFloat(tree.GenerationDefinitions.ChanceToBeSpawnOutsideOfGroup)}'";
                    // TreeData[treeName]["MinDistanceBetweenGroupsMin"] = $"'{tree.GenerationDefinitions.MinDistanceBetweenGroups.Min}'";
                    // TreeData[treeName]["MinDistanceBetweenGroupsMax"] = $"'{tree.GenerationDefinitions.MinDistanceBetweenGroups.Max}'";
                    // TreeData[treeName]["PlantsInGroupMin"] = $"'{tree.GenerationDefinitions.PlantsInGroup.Min}'";
                    // TreeData[treeName]["PlantsInGroupMax"] = $"'{tree.GenerationDefinitions.PlantsInGroup.Max}'";
                    // TreeData[treeName]["CountOfClustersMin"] = $"'{tree.GenerationDefinitions.CountOfClusters.Min}'";
                    // TreeData[treeName]["CountOfClustersMax"] = $"'{tree.GenerationDefinitions.CountOfClusters.Max}'";
                    // TreeData[treeName]["RadiusOfGroupMin"] = $"'{tree.GenerationDefinitions.RadiusOfGroup.Min}'";
                    // TreeData[treeName]["RadiusOfGroupMax"] = $"'{tree.GenerationDefinitions.RadiusOfGroup.Max}'";
                    // TreeData[treeName]["ClusterRadiusInWorldSizeMin"] = $"'{WikiFloat(tree.GenerationDefinitions.ClusterRadiusInWorldSize.Min)}'";
                    // TreeData[treeName]["ClusterRadiusInWorldSizeMax"] = $"'{WikiFloat(tree.GenerationDefinitions.ClusterRadiusInWorldSize.Max)}'";

                    // TreeData[treeName]["StartBiomes"] = $"'{tree.GenerationDefinitions.StartBiomes}'";

                    // as Food
                    // TreeData[treeName]["CalorieValue"] = $"'{tree.CalorieValue}'";

                    // Resources
                    // TreeData[treeName]["PostHarvestingGrowth"] = $"'{tree.PostHarvestingGrowth}'";
                    // TreeData[treeName]["PickableAtPercent"] = $"'{tree.PickableAtPercent}'";
                    // TreeData[treeName]["ResourceBonusAtGrowth"] = $"'{Percent(tree.ResourceBonusAtGrowth)}'";
                    // TreeData[treeName]["LogHealth"] = $"'{tree.LogHealth}'";
                    // TreeData[treeName]["ChanceToSpawnDebris"] = $"'{Percent(tree.ChanceToSpawnDebris)}'";
                    // TreeData[treeName]["DebrisType"] = $"'{tree.DebrisType.Name}'";

                    if (tree.ResourceItemType != null)
                    {
                        // TreeData[treeName]["ResourceItem"] = $"'{tree.ResourceItemType.Name}'";
                        // TreeData[treeName]["ResourceMax"] = $"'{tree.ResourceRange.Max}'";
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
                        //trunkresources.Value.Min
                        //trunkresources.Value.Max
                    }

                    // WorldLayers
                    if (tree.ResourceConstraints != null)
                    {
                        foreach (ResourceConstraint resource in tree.ResourceConstraints)
                        {
                            string LayerName = resource.LayerName;
                            // TreeData[treeName][LayerName + "HalfSpeed"] = $"'{Percent(resource.HalfSpeedConcentration)}'";
                            // TreeData[treeName][LayerName + "MaxResource"] = $"'{Percent(resource.MaxResourceContent)}'";
                        }
                    }

                    // TreeData[treeName]["BlockType"] = $"'{tree.BlockType.Name}'";






                    TreeData treedata = new TreeData
                    {
                        ID = tree.Name + "Species",
                        Name = Localization(TreeName),
                        MaturityAgeDays = WikiFloat(tree.MaturityAgeDays),

                        TreeHealth = WikiFloat(tree.TreeHealth),

                        ExtremeTemperatureRangeMin = WorldTemp(tree.TemperatureExtremes.Min),
                        IdealTemperatureRangeMin = WorldTemp(tree.IdealTemperatureRange.Min),
                        IdealTemperatureRangeMax = WorldTemp(tree.IdealTemperatureRange.Max),
                        ExtremeTemperatureRangeMax = WorldTemp(tree.TemperatureExtremes.Max),

                        ExtremeMoistureRangeMin = Percent(tree.MoistureExtremes.Min),
                        IdealMoistureRangeMin = Percent(tree.IdealMoistureRange.Min),
                        IdealMoistureRangeMax = Percent(tree.IdealMoistureRange.Max),
                        ExtremeMoistureRangeMax = Percent(tree.MoistureExtremes.Max),

                        ExtremeWaterRangeMin = Percent(tree.WaterExtremes.Min),
                        IdealWaterRangeMin = Percent(tree.IdealWaterRange.Min),
                        IdealWaterRangeMax = Percent(tree.IdealWaterRange.Max),
                        ExtremeWaterRangeMax = Percent(tree.WaterExtremes.Max),

                        PollutionMin = Percent(tree.PollutionDensityTolerance),
                        PollutionMax = Percent(tree.MaxPollutionDensity),

                        CO2TonsPerDay = WikiFloat(tree.ReleasesCO2TonsPerDay)
                    };

                    TreeDataList.Add(TreeName, treedata);

                }
            }
            // writes to json file
            string TreeDataListjsonString = JsonConvert.SerializeObject(new { trees = TreeDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("Trees", TreeDataListjsonString);
        }
    }
}
