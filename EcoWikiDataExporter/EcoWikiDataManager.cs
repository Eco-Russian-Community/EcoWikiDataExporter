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
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using Eco.Simulation.Types;
using Eco.Stats;
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
    internal class EcoWikiDataManager
    {
        private static string space2 = "        ";
        private static string space3 = "            ";

        public static void WriteDictionaryToFile(string filename, string type, SortedDictionary<string, Dictionary<string, string>> dictionary, bool final = true)
        {
            //var lang = LocalizationPlugin.Config.Language;

            //string path = @EcoWikiDataExporter.EWDEFolder + $@"\" + $@"{lang}"  + $@"\" + $@"{lang}_" + filename + $@".txt";
            //string path = @EcoWikiDataExporter.EWDEFolder + $@"\" + $@"{lang}_" + filename + $@".txt";
            string path = @EcoWikiDataExporter.EWDEFolder + $@"\" + filename + $@".txt";

            using (StreamWriter streamWriter = new StreamWriter(path, false))
            {
                streamWriter.WriteLine("-- Eco Version : " + EcoVersion.VersionNumber);
                //streamWriter.WriteLine("-- Export Language: " + lang);
                streamWriter.WriteLine();
                streamWriter.WriteLine("return {\n    " + type + " = {");

                foreach (string key in dictionary.Keys)
                {
                    streamWriter.WriteLine(string.Format("{0}['{1}'] = {{", space2, key));
                    foreach (KeyValuePair<string, string> keyValuePair in dictionary[key])
                        streamWriter.WriteLine(string.Format("{0}{1}['{2}'] = {3},", space2, space3, keyValuePair.Key, keyValuePair.Value));
                    streamWriter.WriteLine(string.Format("{0}}},", space2));
                }
                streamWriter.Write("    },");
                if (final)
                    streamWriter.Write("\n}");
                streamWriter.Close();
            }
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

		public static string WriteDictionaryAsSubObject(SortedDictionary<string, Dictionary<string, string>> dictionary, int depth)
		{
			string spaces = space2 + space2 + space3;

			for (int i = 0; i < depth; i++)
			{
				spaces += space2;
			}

			StringBuilder sb = new StringBuilder();
			sb.AppendLine(" {");
			foreach (KeyValuePair<string, Dictionary<string, string>> kvp in dictionary)
			{
				sb.AppendLine(spaces + "['" + kvp.Key + "'] = {");
				foreach (KeyValuePair<string, string> innerKvp in kvp.Value)
				{
					sb.AppendLine(spaces + "['" + innerKvp.Key + "'] = {" + innerKvp.Value + "},");
				}
				sb.Append(spaces + "},");
			}
			sb.Append(spaces + "}");

			return sb.ToString();
		}

		public static string WriteDictionaryAsSubObject(Dictionary<string, string> dict, int depth)
        {
            string spaces = space2 + space3;

            for (int i = 0; i < depth; i++)
            {
                spaces += space2;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" {");
            foreach (KeyValuePair<string, string> kvp in dict)
            {
                sb.AppendLine(spaces + "['" + kvp.Key + "'] = {" + kvp.Value + "},");
            }
            sb.Append(spaces + "}");

            return sb.ToString();
        }

        public static string WriteDictionaryAsSub(string dict)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" {");
            sb.AppendLine(string.Join(", ", dict));
            sb.Append("}");
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

        public static Dictionary<string, string> Localization(string name)
        {
            var localizedString = new Dictionary<string, string>();

            localizedString.Add("English", name);
            localizedString.Add("Russian", Localizer.LocalizeString(name, SupportedLanguage.Russian));
            localizedString.Add("German", Localizer.LocalizeString(name, SupportedLanguage.German));
            localizedString.Add("French", Localizer.LocalizeString(name, SupportedLanguage.French));

            return localizedString;
        }
    }
}
