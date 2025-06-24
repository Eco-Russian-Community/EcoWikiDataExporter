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
        // dictionary of animals and their dictionary of stats
        private static SortedDictionary<string, Dictionary<string, string>> AchievementData = new SortedDictionary<string, Dictionary<string, string>>();

        public static void ExportAchievementsData()
        {
            // dictionary of plant properties
            Dictionary<string, string> achievementDetails = new Dictionary<string, string>()
                {
                    { "Name","nil" },
                    { "IconName","nil" },
                    { "Description","nil" },
                };

            foreach (AchievementDefinition achievement in AchievementManager.Obj.NameToAchievement.Values)
            {
                string achievementName = achievement.DisplayName.NotTranslated;

                if (!AchievementData.ContainsKey(achievementName))
                {
                    AchievementData.Add(achievementName, new Dictionary<string, string>(achievementDetails));
                    AchievementData[achievementName]["Name"] = EcoWikiDataManager.WriteDictionaryAsSubObject(EcoWikiDataManager.Localization(achievementName), 1);
                    AchievementData[achievementName]["Description"] = EcoWikiDataManager.WriteDictionaryAsSubObject(EcoWikiDataManager.Localization(achievement.Description.NotTranslated), 1);
                    AchievementData[achievementName]["IconName"] = $"'{achievement.IconName}'";
                }
            }

        // writes to txt file
        EcoWikiDataManager.WriteDictionaryToFile("AchievementsData", "achievements", AchievementData);


        }
    }
}
