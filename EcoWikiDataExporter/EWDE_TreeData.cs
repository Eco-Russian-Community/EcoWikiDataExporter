using Eco.Gameplay.Systems.Chat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Eco.Simulation.Types;
using Eco.Simulation;
//using static Eco.Simulation.Types.PlantSpecies;
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
                { "Name","nil" },
                { "MaturityAgeDays","nil" },
                { "StartBiomes","nil" },
                { "Height","nil" }
            };

        IEnumerable<Species> species = EcoSim.AllSpecies;

            foreach (Species s in species)
            {
                if (s is TreeSpecies)
                {
                    TreeSpecies tree = s as TreeSpecies;
                    if (!TreeData.ContainsKey(tree.DisplayName))
                    {
                        string treeName = tree.DisplayName;
                        TreeData.Add(treeName, new Dictionary<string, string>(treeDetails));

                        TreeData[treeName]["Name"] = $"'{tree.DisplayName.NotTranslated}'";
                        TreeData[treeName]["MaturityAgeDays"] = "'" + tree.MaturityAgeDays.ToString("F1", CultureInfo.InvariantCulture) + "'";
                        TreeData[treeName]["Height"] = "'" + tree.Height.ToString("F1", CultureInfo.InvariantCulture) + "'";
                        TreeData[treeName]["StartBiomes"] = $"'{tree.GenerationDefinitions.StartBiomes}'";


                    }
                }
            }
        // writes to txt file
        EcoWikiDataManager.WriteDictionaryToFile("TreeData", "trees", TreeData);
        }
    }
}
