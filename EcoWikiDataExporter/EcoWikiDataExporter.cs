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
using Eco.Shared.Icons;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Utils;

namespace Eco.Mods.EcoWikiDataExporter
{
	[LocDisplayName(nameof(EcoWikiDataExporter))]
    public class EcoWikiDataExporter : IModKitPlugin, IServerPlugin, IInitializablePlugin, ICommandablePlugin
    {
		public const string Version = "0.0.2";
        public const string EWDEFolder = "EWDE";

        public void Initialize(TimedTask timer)
		{
			
		}

        public string GetStatus() => string.Empty;

        public string GetCategory() => Localizer.DoStr("Mods");
        public override string ToString() => Localizer.DoStr("EWDE");

        public void GetCommands(Dictionary<string, Action> nameToFunction)
        {
            nameToFunction.Add(Localizer.DoStr("Export Wiki"), this.Exportwiki);
        }

		void Exportwiki()
		{
            // Create EWDE Folder
            if (Directory.Exists(EWDEFolder))
                Directory.Delete(EWDEFolder, true);

            Directory.CreateDirectory(EWDEFolder);

        }
    }
}
