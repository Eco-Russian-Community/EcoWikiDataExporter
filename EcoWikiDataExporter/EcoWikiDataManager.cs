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
using Eco.Core.Controller;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Shared.Icons;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using Eco.Gameplay.Systems;
using Eco.Shared;
using Eco.Simulation.Types;

namespace Eco.Mods.EcoWikiDataExporter
{
	internal class EcoWikiDataManager
	{
        private static string space2 = "        ";
        private static string space3 = "            ";

        public static void WriteDictionaryToFile(string filename, string type, SortedDictionary<string, Dictionary<string, string>> dictionary, bool final = true)
        {
            var lang = LocalizationPlugin.Config.Language;
            
            string path = @EcoWikiDataExporter.EWDEFolder + $@"\" + $@"{lang}_" + filename + $@".txt";

            using (StreamWriter streamWriter = new StreamWriter(path, false))
            {
                streamWriter.WriteLine("-- Eco Version : " + EcoVersion.VersionNumber);
                streamWriter.WriteLine("-- Export Language: " + lang);
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

    }
}
