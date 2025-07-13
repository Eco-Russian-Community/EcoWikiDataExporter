using Eco.Core.Controller;
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
        private static SortedDictionary<string, Dictionary<string, string>> TalentData = new SortedDictionary<string, Dictionary<string, string>>();

        public static void ExportTalentData()
        {
            Dictionary<string, string> TalentDetails = new Dictionary<string, string>()
            {
                { "Name" , "nil" },
                { "Description", "nil" },
                { "IconName", "nil" },
                { "SkillID", "nil" },
                { "Level", "nil" },
            };

            foreach (Talent talent in TalentManager.AllTalents)
            {
                TalentGroup talentGroup;
                if (talent.TalentGroupType != null)
                {
                    talentGroup = Item.Get(TalentManager.TypeToTalent[talent.GetType()].TalentGroupType) as TalentGroup;
                    string TalentName = talentGroup.DisplayName.NotTranslated;
                    if (!TalentData.ContainsKey(TalentName))
                    {
                        TalentData.Add(TalentName, new Dictionary<string, string>(TalentDetails));
                        TalentData[TalentName]["Name"] = WriteDictionaryAsSubObject(Localization(TalentName), 1);
                        TalentData[TalentName]["Description"] = WriteDictionaryAsSubObject(Localization(CleanText(talentGroup.GetDescription.NotTranslated)), 1);
                        TalentData[TalentName]["IconName"] = $"'{talentGroup.IconName}'";
                        TalentData[TalentName]["SkillID"] = $"'{talentGroup.OwningSkill.Name}'";
                        TalentData[TalentName]["Level"] = $"'{talentGroup.Level}'";
                    }
                }
            }


            // writes to txt file
            WriteDictionaryToFile("TalentData", "talents", TalentData);
        }
    }
}
