using Eco.Core.Controller;
using Eco.Core.Items;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.IoC;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using Newtonsoft.Json;
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
using System.Xml.Linq;
using static Eco.Mods.EcoWikiDataExporter.WikiData;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        private static Dictionary<string, SkillData> SkillDataList = new Dictionary<string, SkillData>();

        public static void ExportSkillData()
        {

            Dictionary<string, string> skillsDetails = new Dictionary<string, string>()
            {

            };

            IEnumerable<Type> PlayerDefaultSkills = PlayerDefaults.GetDefaultSkills();

            foreach (var skill in Skill.AllSkills)
            {
                string SkillName = skill.DisplayName;
                if (!SkillDataList.ContainsKey(SkillName))
                {

                    SkillData skilldata = new SkillData
                    {
                        Name = Localization(skill.DisplayName),
                        Description = Localization(CleanText(skill.GetDescription.NotTranslated)),
                        MaxLevel = skill.MaxLevel,
                        SkillID = skill.Type.Name,
                        Tier = skill.Tier,
                        SpecialtyCost = skill.SpecialtyCost,
                        IsRoot = skill.IsRoot,
                        RootSkill = skill.RootSkillTree.StaticSkill.ToString(),
                        PlayerDefaultSkill = PlayerDefaultSkills.Contains(skill.Type)
                    };

                    SkillDataList.Add(SkillName, skilldata);
                }
            }
            
            // writes to json file
            string jsonString = JsonConvert.SerializeObject(new { skills = SkillDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("Skills", jsonString);
        }
    }
}
