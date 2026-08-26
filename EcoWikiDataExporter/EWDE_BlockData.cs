using Eco.Core.Items;
using Eco.Core.Systems;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Civics.Districts;
using Eco.Gameplay.Civics.Misc;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Shared.Localization;
using Eco.Shared.Logging;
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
            Dictionary<string, string> blockDetails = new Dictionary<string, string>() { };

            Dictionary<string, string> formgroupDetails = new Dictionary<string, string>()
            {
                { "Name","nil" },
                { "IconName","nil" },
                { "SortOrder","nil" }
            };





            BlockFormData data = BlockFormManager.Data;
            SortedDictionary<string, Dictionary<string, string>> FormGroups = new SortedDictionary<string, Dictionary<string, string>>();
            SortedDictionary<string, Dictionary<string, string>> FormTypes = new SortedDictionary<string, Dictionary<string, string>>();
            SortedDictionary<string, Dictionary<string, string>> BlockForms = new SortedDictionary<string, Dictionary<string, string>>();

            BlockData.Add("Data", new Dictionary<string, string>(blockDetails));

            foreach (FormGroup formgroup in data.FormGroups)
            {
                string FormGroupName = formgroup.Name;
                FormGroups.Add(FormGroupName, new Dictionary<string, string>(formgroupDetails));

                FormGroups[FormGroupName]["Name"] = WriteDictionaryAsSubObject(Localization(formgroup.DisplayName.NotTranslated), 1);
                FormGroups[FormGroupName]["IconName"] = $"'{formgroup.IconName}'";
                FormGroups[FormGroupName]["SortOrder"] = $"'{formgroup.SortOrder}'";
            }

            BlockData["Data"]["FormGroups"] = WriteDictionaryAsSubObject(FormGroups, 1);


            foreach (FormType formtype in data.FormTypes) 
            {
                string FormTypeName = formtype.Name;
                FormGroups.Add(FormTypeName, new Dictionary<string, string>(formgroupDetails));

                FormGroups[FormTypeName]["Name"] = WriteDictionaryAsSubObject(Localization(formtype.DisplayName.NotTranslated), 1);
                FormGroups[FormTypeName]["IconName"] = $"'{formtype.IconName}'";
                FormGroups[FormTypeName]["SortOrder"] = $"'{formtype.SortOrder}'";
            }

            BlockData["Data"]["FormTypes"] = WriteDictionaryAsSubObject(FormTypes, 1);

            foreach (BlockForm blockform in data.BlockForms)
            {
                string BlockFormName = blockform.Name;
                //BlockForms.Add(BlockFormName, new Dictionary<string, string>(formgroupDetails));
                Log.WriteLineLoc($"Block Form: {BlockFormName} Icon Name: {blockform.IconName} Form Type: {blockform.FormType.DisplayName.NotTranslated}");
                Log.WriteLineLoc($"Block Types: {blockform.BlockTypes}");
                Log.WriteLineLoc($"Block Types: {blockform.BlockTypeIDs}");
                //Log.WriteLineLoc($"Block Types: {blockform.}");
                //BlockForms[BlockFormName]["Name"] = WriteDictionaryAsSubObject(Localization(blockform.DisplayName.NotTranslated), 1);
                //BlockForms[BlockFormName]["IconName"] = $"'{blockform.IconName}'";
                //BlockForms[BlockFormName]["SortOrder"] = $"'{blockform.SortOrder}'";
            }

            BlockData["Data"]["BlockForms"] = WriteDictionaryAsSubObject(BlockForms, 1);


            // writes to txt file
            WriteDictionaryToFile("BlockData", "blocks", BlockData);
        }
    }
}
