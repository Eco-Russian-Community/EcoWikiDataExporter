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
using Eco.Shared.Logging;
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
        private static SortedDictionary<string, Dictionary<string, string>> EcopediaData = new SortedDictionary<string, Dictionary<string, string>>();
        public static void ExportEcopediaData()
        {
            // dictionary of recipe properties
            Dictionary<string, string> EcopediaMenuDetails = new Dictionary<string, string>()
            {
                { "Name", "nil" },
            };

            foreach (var Chapter in Ecopedia.Obj.Chapters.Values)
            {
                string ChapterName = Chapter.DisplayName.NotTranslated;
                if (ChapterName == "Development") continue;
                EcopediaData.Add(ChapterName, new Dictionary<string, string>(EcopediaMenuDetails));
                EcopediaData[ChapterName]["Name"] = WriteDictionaryAsSubObject(Localization(ChapterName), 1);
                EcopediaData[ChapterName]["Type"] = $"'Chapter'";
                EcopediaData[ChapterName]["Chapter"] = $"'{ChapterName}'";

                foreach (var Category in Chapter.Categories)
                {
                    string CategoryName = Category.DisplayName.NotTranslated;
                    if (CategoryName == "World Index") continue;
                    EcopediaData.Add(CategoryName, new Dictionary<string, string>(EcopediaMenuDetails));
                    EcopediaData[CategoryName]["Name"] = WriteDictionaryAsSubObject(Localization(CategoryName), 1);
                    EcopediaData[CategoryName]["Type"] = $"'Category'";
                    EcopediaData[CategoryName]["Chapter"] = $"'{ChapterName}'";
                    EcopediaData[CategoryName]["Icon"] = $"'{Category.IconName}'";

                    //string PagesList = "";
                    //foreach (var Page in Category.Pages.Values)
                    //{
                    //    string PageName = Page.DisplayName.NotTranslated;
                    //    if (PagesList == "") { PagesList = PageName; } else { PagesList = PagesList + ", " + PageName; }
                    //}
                    //PagesList = "{" + PagesList + "}";
                    //EcopediaData[CategoryName]["Pages"] = $"{PagesList}";

                }
            }

         // writes to txt file
         WriteDictionaryToFile("EcopediaMenuData", "ecopediapages", EcopediaData);
        }
    }
}
