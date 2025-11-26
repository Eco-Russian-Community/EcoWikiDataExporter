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
using Eco.Gameplay.Players;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        private static SortedDictionary<string, Dictionary<string, string>> LocalizationData = new SortedDictionary<string, Dictionary<string, string>>();
        public static void ExportLocalizationData()
        {
            Dictionary<string, string> LocDetails = new Dictionary<string, string>() { };
            string[] LocStrings =
                {
                    "Tags",
                    "{0} Tag",
                    "Items in Tag",
                    "Tags Applying to",
                    "Used in",
                    "Crafted At",
                    "Crafting Table",
                    "Products",
                    "Ingredients",
                    "Craft time",
                    "Labor",
                    "Skill Requirements",
                    "Experience",
                    "None",
                    "Recipes",

                    "Eco Credits",
                    "Quantity",
                    "Requires",
                    "Host Cut",
                    "Modder Cut",
                    "Streamer Cut",
                    "Settlement Cut",
                    "Charity Cut",

                    "Produces",
                    "Harvested from Species",
                    "Plugs Into",
                    "Requirements",
                    "Pluggable Modules",
                    "Housing Value",
                    "Air Pollution",

                    "Calories",
                    "Carbs",
                    "Protein",
                    "Fat",
                    "Vitamins",

                    "Skills",
                    "Professions",
                    "{0} Profession",
                    "Specialties",
                    "{0} Specialty",
                    "Talents",
                    "Level",
                    "Specialty Experience Per Level",
                    "Skill Benefits",

                    "Biomes",
                };

            foreach (string Loc in LocStrings)
            {
                LocalizationData.Add(Loc, new Dictionary<string, string>(LocDetails));
                LocalizationData[Loc]["Translate"] = WriteDictionaryAsSubObject(Localization(Loc), 1);
            }
            // writes to txt file
                WriteDictionaryToFile("LocalizationData", "locales", LocalizationData);
        }
    }
}
