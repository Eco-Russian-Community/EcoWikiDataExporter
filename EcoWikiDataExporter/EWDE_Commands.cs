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
using Eco.Shared.IoC;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {

     private static SortedDictionary<string, Dictionary<string, string>> EveryCommand = new SortedDictionary<string, Dictionary<string, string>>();

        public static void ExportCommands()
        {
            // dictionary of commands
            Dictionary<string, string> commandDetails = new Dictionary<string, string>()
            {
                { "command", "nil" },
                { "parent", "nil" },
                { "helpText", "nil" },
                { "shortCut", "nil" },
                { "level", "nil" },
                { "parameters", "nil" }
            };

            Regex regex = new Regex("\t\n\v\f\r");

            IEnumerable<ChatCommand> commands = Singleton<ChatManager>.Obj.ChatCommandService.GetAllCommands();

            foreach (var com in commands)
            {
                if (com.Key == "dumpdetails")
                    continue;

                var command = $"/{Localizer.DoStr(com.ParentKey)}{(Localizer.DoStr(com.ParentKey) == "" ? Localizer.DoStr(com.Name) : " " + Localizer.DoStr(com.Name))}";
                if (!EveryCommand.ContainsKey(command))
                {
                    EveryCommand.Add(command, new Dictionary<string, string>(commandDetails));
                    EveryCommand[command]["command"] = "'" + Localizer.DoStr(com.Key) + "'";

                    if (com.ParentKey != null && com.ParentKey != "")
                        EveryCommand[command]["parent"] = "'" + Localizer.DoStr(com.ParentKey) + "'";

                    EveryCommand[command]["helpText"] = "'" + Localizer.DoStr(EcoWikiDataManager.JSONStringSafe(com.HelpText)) + "'";
                    EveryCommand[command]["shortCut"] = "'" + Localizer.DoStr(com.ShortCut) + "'";
                    EveryCommand[command]["level"] = "'" + Localizer.DoStr(com.AuthLevel.ToString()) + "'";


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
                        pars[pos] = "'" + p.Name + "', '" + p.ParameterType.Name + "'";

                        if (p.HasDefaultValue)
                            pars[pos] += ", '" + p.DefaultValue + "'";
                    }
                    EveryCommand[command]["parameters"] = EcoWikiDataManager.WriteDictionaryAsSubObject(pars, 1);
                }
            }

            // writes to txt file
            EcoWikiDataManager.WriteDictionaryToFile("CommandData", "commands", EveryCommand);
        }


    }
}
