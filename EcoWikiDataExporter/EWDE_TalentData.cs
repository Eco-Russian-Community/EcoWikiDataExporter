using Eco.Core.Controller;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Housing.PropertyValues;
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

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        private static SortedDictionary<string, TalentData> TalentDataList = new SortedDictionary<string, TalentData>();
        public static void ExportTalentData()
        {

            foreach (Talent talent in TalentManager.AllTalents)
            {
                TalentGroup talentGroup;
                if (talent.TalentGroupType != null)
                {
                    talentGroup = Item.Get(TalentManager.TypeToTalent[talent.GetType()].TalentGroupType) as TalentGroup;
                    string TalentName = talentGroup.DisplayName.NotTranslated;
                    if (!TalentDataList.ContainsKey(TalentName))
                    {
                        foreach (var talentType in talentGroup.Talents)
                        {
                            //if (!TalentManager.TypeToTalent.TryGetValue(talentType, out var talent)) continue;



                        }    

                            TalentData talentdata = new TalentData
                        {
                            Name = Localization(TalentName),
                            Description = Localization(CleanText(talentGroup.GetDescription.NotTranslated)),
                            IconName = talentGroup.IconName,
                            SkillID = talentGroup.OwningSkill.Name,
                            Level = talentGroup.Level
                            
                        };

                        TalentDataList.Add(TalentName, talentdata);
                    }
                }
            }
            // writes to json file
            string TalentDataListjsonString = JsonConvert.SerializeObject(new { talents = TalentDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("Talents", TalentDataListjsonString);
        }
    }
}
