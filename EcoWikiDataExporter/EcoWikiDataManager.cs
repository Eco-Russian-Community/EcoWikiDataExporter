using Eco.Core.Controller;
using Eco.Core.Items;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.ModKit.Internal;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using Eco.Simulation.Types;
using Eco.Stats;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
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
        public static void WriteDictionaryToJsonFile(string filename, string Data)
        {
            string filepath = @EcoWikiDataExporter.EWDEFolder + $@"\" + filename + $@".json";
            File.WriteAllText(filepath, Data);
        }

        public static string JSONStringSafe(string s)
        {
            string[] NameSplit = Regex.Split(s, @"(?=['?])");
            var sb = new StringBuilder();
            foreach (string str in NameSplit)
            {
                sb.Append(str);
                if (str != NameSplit.Last())
                    sb.Append("\\");
            }

            return sb.ToString();
        }

        public static string CleanText(string Text)
        {
            Regex regexTag = new Regex("<[^>]*>");
            Text = regexTag.Replace(Text, "");
            Regex regexFeed = new Regex("[\t\n\v\f\r]");
            Text = regexFeed.Replace(Text, "");
            Text = Text.Replace("'", "\\'");
            return Text;
        }

        public static string CleanItemID(string ItemName)
        {
            return ItemName.ToString().Substring(ItemName.ToString().LastIndexOf('.') + 1);
        }

        public static string GetItemTags(Item Item)
        {
            StringBuilder tags = new StringBuilder();
            tags.Append('{');
            foreach (Tag tag in Item.Tags())
            {
                tags.Append($"'{tag.DisplayName}'");
                if (tag != Item.Tags().Last()) tags.Append(", ");
            }
            tags.Append('}');
            return tags.ToString();
        }

        public static string WorldTemp(float ServerTemp)
        {
            var temp = Math.Round(ServerTemp * 40 - 10);
            return temp.ToString("G", CultureInfo.InvariantCulture);
        }

        public static string Percent(float Percent)
        {
            var NewPercent = Math.Round(Percent * 100000) / 1000;
            return NewPercent.ToString("G", CultureInfo.InvariantCulture);
        }

        public static string WikiFloat(float Float)
        {
            return Float.ToString("G", CultureInfo.InvariantCulture);
        }

        public static string WikiDouble(double Double)
        {
            return Double.ToString("G", CultureInfo.InvariantCulture);
        }

        public static string WikiBool(bool Bool)
        {
            if (Bool) { return "True"; } else {  return "False"; }
        }

        public static TranslateData Localization(string text)
        {
            
            String EnglishLang = text;

            String RussianLang = Localizer.LocalizeString(EnglishLang, SupportedLanguage.Russian);
            String GermanLang = Localizer.LocalizeString(EnglishLang, SupportedLanguage.German);
            String FrenchLang = Localizer.LocalizeString(EnglishLang, SupportedLanguage.French);
            String JapaneseLang = Localizer.LocalizeString(EnglishLang, SupportedLanguage.Japanese);

            if (RussianLang     == "")  { RussianLang   = EnglishLang; }
            if (GermanLang      == "")  { GermanLang    = EnglishLang; }
            if (FrenchLang      == "")  { FrenchLang    = EnglishLang; }
            if (JapaneseLang    == "")  { JapaneseLang  = EnglishLang; }

            EnglishLang     = Shielding(EnglishLang);
            RussianLang     = Shielding(RussianLang);
            GermanLang      = Shielding(GermanLang);
            FrenchLang      = Shielding(FrenchLang);
            JapaneseLang    = Shielding(JapaneseLang);

            TranslateData translatedata = new TranslateData
            {
                English = EnglishLang,
                Russian = RussianLang,
                German = GermanLang,
                French = FrenchLang,
                Japanese = JapaneseLang
            };

            return translatedata;
        }

        public static string Shielding(string Text)
        {
            string SafeText = Text.Replace('"', '\'');
            return SafeText;
        }
    }
}
