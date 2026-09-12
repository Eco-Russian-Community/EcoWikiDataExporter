using Eco.Core.Controller;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.EcopediaRoot;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Rooms;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Mods.TechTree;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.IoC;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using System;
using System.Collections;
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
        public class EcoVersionData
        {
            public string Version { get; set; }
            public string VersionNumber { get; set; }
            public string FullVersion { get; set; }
        }
        public class TranslateData
        {
            public string English { get; set; }
            public string Russian { get; set; }
            public string German { get; set; }
            public string French { get; set; }
            public string Japanese { get; set; }
        }

        public class MarketplaceData
        {
            public string Category { get; set; }
            public string Price { get; set; }
            public string Quantity { get; set; }
            public string Achievement { get; set; }
        }

        public class AchievementData
        {
            public TranslateData Name { get; set; }
            public TranslateData Description { get; set; }
            public string IconName { get; set; }
        }

        public class TagData
        {
            public string ID { get; set; }
            public TranslateData Name { get; set; }
            public bool Hidden { get; set; }
            public bool IsVisibleInTooltip { get; set; }
            public bool IsVisibleInEcopedia { get; set; }
            public bool IsVisibleInFilter { get; set; }
            public List<string> Items { get; set; }
        }

        public class SkillData
        {
            public TranslateData Name { get; set; }
            public TranslateData Description { get; set; }
            public string SkillID { get; set; }
            public int MaxLevel { get; set; }
            public int Tier { get; set; }
            public int SpecialtyCost { get; set; }
            public bool IsRoot { get; set; }
            public string RootSkill { get; set; }
            public bool PlayerDefaultSkill { get; set; }
        }


    }
}