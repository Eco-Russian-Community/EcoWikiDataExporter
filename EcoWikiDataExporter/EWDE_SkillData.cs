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
using Eco.Gameplay.Skills;
using Eco.Core.Items;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {

        // dictionary of skills and their dictionary of stats
        private static SortedDictionary<string, Dictionary<string, string>> SkillData = new SortedDictionary<string, Dictionary<string, string>>();
        
        public static void ExportSkillData()
        {

            Dictionary<string, string> skillsDetails = new Dictionary<string, string>()
            {
                { "Name", "nil" },
                { "Description", "nil" },
                { "SkillID", "nil" },
                { "MaxLevel", "nil" },
                { "Tier", "nil" },
                { "Root", "nil" },
                { "RootSkill", "nil" }
            };


            foreach (var skill in Skill.AllSkills)
            {
                string SkillName = skill.DisplayName;
                if (!SkillData.ContainsKey(SkillName))
                {                    
                    SkillData.Add(SkillName, new Dictionary<string, string>(skillsDetails));
                    SkillData[SkillName]["Name"] = EcoWikiDataManager.WriteDictionaryAsSubObject(EcoWikiDataManager.Localization(skill.DisplayName), 1);
                    SkillData[SkillName]["Description"] = EcoWikiDataManager.WriteDictionaryAsSubObject(EcoWikiDataManager.Localization(EcoWikiDataManager.CleanText(skill.GetDescription)), 1);
                    SkillData[SkillName]["MaxLevel"] = $"'{skill.MaxLevel}'";
                    SkillData[SkillName]["SkillID"] = $"'{skill.Type.Name}'";
                    SkillData[SkillName]["Tier"] = $"'{skill.Tier}'";
                    SkillData[SkillName]["IsRoot"] = $"'{skill.IsRoot}'";
                    SkillData[SkillName]["RootSkill"] = $"'{skill.RootSkillTree.StaticSkill}'";
                }
            }
        // writes to txt file
        EcoWikiDataManager.WriteDictionaryToFile("SkillData", "skills", SkillData);
        }


    }
}
