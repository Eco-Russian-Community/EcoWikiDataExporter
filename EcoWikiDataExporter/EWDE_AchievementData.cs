using Eco.Core.Controller;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Achievements;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.IoC;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using Eco.Simulation.Agents;
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
using static Eco.Mods.EcoWikiDataExporter.WikiData;

namespace Eco.Mods.EcoWikiDataExporter
{
    public partial class WikiData
    {
        // dictionary of animals and their dictionary of stats
        private static Dictionary<string, AchievementData> AchievementDataList = new Dictionary<string, AchievementData>();

        public static void ExportAchievementsData()
        {

            foreach (AchievementDefinition achievement in AchievementManager.Obj.NameToAchievement.Values)
            {
                string achievementName = achievement.Name;

                AchievementData achievementdata = new AchievementData
                {
                    Name = Localization(achievement.DisplayName.NotTranslated),
                    Description = Localization(achievement.Description.NotTranslated),
                    IconName = achievement.IconName
                };

                AchievementDataList.Add(achievementName, achievementdata);
            }

            // writes to json file
            string jsonString = JsonConvert.SerializeObject(new { achievements = AchievementDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("Achievements", jsonString);
        }
    }
}
