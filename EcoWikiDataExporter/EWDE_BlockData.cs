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
        // Dictionary of blocks
        private static SortedDictionary<string, Dictionary<string, string>> BlockData = new SortedDictionary<string, Dictionary<string, string>>();


        public static void ExportBlockData()
        {
            // Dictionary of blocks properties
            Dictionary<string, string> blockDetails = new Dictionary<string, string>()
            {
                { "ID","nil" },
                { "Name","nil" }
            };

            Dictionary<string, string> formgroupDetails = new Dictionary<string, string>()
            {
                { "ID","nil" },
                { "Name","nil" }
            };





            BlockFormData data = BlockFormManager.Data;

            foreach (FormGroup group in data.FormGroups)
            {
                string FormGroupName = group.Name;
                BlockData.Add(FormGroupName, new Dictionary<string, string>(formgroupDetails));

                BlockData[FormGroupName]["Name"] = WriteDictionaryAsSubObject(Localization(group.DisplayName.NotTranslated), 1);
                BlockData[FormGroupName]["IconName"] = $"'{group.IconName}'";
                
            }

            foreach (BlockForm blockform in data.BlockForms)
            {
                
                
                //blockform.DisplayName.NotTranslated
                //blockform.IconName
                //blockform.SortOrder

            }




                // writes to txt file
                WriteDictionaryToFile("BlockData", "blocks", BlockData);
        }
    }
}
