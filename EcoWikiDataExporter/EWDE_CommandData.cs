using Eco.Core.Controller;
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
using static Eco.Mods.EcoWikiDataExporter.WikiData;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        private static Dictionary<string, CommandData> CommandDataList = new Dictionary<string, CommandData>();
        public static void ExportCommandData()
        {

            Regex regex = new Regex("\t\n\v\f\r");

            IEnumerable<ChatCommand> commands = Singleton<ChatManager>.Obj.ChatCommandService.GetAllCommands();

            foreach (var com in commands)
            {
                if (com.Key == "dumpdetails")
                    continue;

                var CommandName = $"/{Localizer.DoStr(com.ParentKey)}{(Localizer.DoStr(com.ParentKey) == "" ? Localizer.DoStr(com.Name) : " " + Localizer.DoStr(com.Name))}";
                if (!CommandDataList.ContainsKey(CommandName))
                {

                    MethodInfo method = com.Method;
                    if (method == null)
                        continue;

                    ParameterInfo[] parameters = method.GetParameters();

                    if (parameters == null)
                        continue;

                    Dictionary<string, string> pars = new Dictionary<string, string>();

                    foreach (var p in parameters)
                    {
                        if (p.Name == "user")
                            continue;

                        string pos = "Arg" + p.Position.ToString();
                        pars[pos] = "{";
                        pars[pos] += "'" + p.Name + "', '" + p.ParameterType.Name + "'";

                        if (p.HasDefaultValue) { pars[pos] += ", '" + p.DefaultValue + "'"; }
                        pars[pos] += "}";
                    }

                    string Parent = "";
                    if (com.ParentKey != null && com.ParentKey != "") { Parent = com.ParentKey; }

                    CommandData commanddata = new CommandData
                    {
                        Command = com.Key,
                        Level = com.AuthLevel.ToString(),
                        Description = Localization(JSONStringSafe(com.HelpText)),
                        ShortCut = com.ShortCut,
                        Parent = Parent
                    };

                    
                    //CommandData[command]["parameters"] = WriteDictionaryAsSubObject(pars, 1);

                    CommandDataList.Add(CommandName, commanddata);
                }
            }

            // writes to json file
            string jsonString = JsonConvert.SerializeObject(new { commands = CommandDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("Commands", jsonString);
        }
    }
}
