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
using Eco.Shared.Icons;
using Eco.Shared.Localization;
using Eco.Shared.Logging;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
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
using static System.Net.Mime.MediaTypeNames;

namespace Eco.Mods.EcoWikiDataExporter
{
	[LocDisplayName(nameof(EcoWikiDataExporter))]
    public class EcoWikiDataExporter : IModKitPlugin, IServerPlugin, IInitializablePlugin, ICommandablePlugin
    {
        public static readonly string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public const string EWDEFolder = "EWDE";
        public const bool DebugMode = true;

        public void Initialize(TimedTask timer)
        {
            if (DebugMode is true) { ExportWiki(); }
        }

        public string GetStatus() => string.Empty;

        public string GetCategory() => Localizer.DoStr("Mods");
        public override string ToString() => Localizer.DoStr("EWDE");

        public void GetCommands(Dictionary<string, Action> nameToFunction)
        {
            nameToFunction.Add(Localizer.DoStr("Export Wiki Data"), this.ExportWiki);
        }

		void ExportWiki()
		{

            // Create EWDE Lang Folder
            //if (Directory.Exists(EWDEFolder))
            //    Directory.Delete(EWDEFolder, true);

            Directory.CreateDirectory(EWDEFolder);

            try { WikiData.ExportCommandData(); } catch (Exception e) { Log.WriteWarningLineLoc($"Export commands error: {e.Message}"); };
            try { WikiData.ExportVersionData(); } catch (Exception e) { Log.WriteWarningLineLoc($"Export version error: {e.Message}"); };
            try { WikiData.ExportSkillData(); } catch (Exception e) { Log.WriteWarningLineLoc($"Export skills error: {e.Message}"); };
            try { WikiData.ExportPlantData(); } catch (Exception e) { Log.WriteWarningLineLoc($"Export plants error: {e.Message}"); };
            try { WikiData.ExportTreeData(); } catch (Exception e) { Log.WriteWarningLineLoc($"Export trees error: {e.Message}"); };
            try { WikiData.ExportAnimalData(); } catch (Exception e) { Log.WriteWarningLineLoc($"Export animals error: {e.Message}"); };
            try { WikiData.ExportTagData(); } catch (Exception e) { Log.WriteWarningLineLoc($"Export tags error: {e.Message}"); };
            try { WikiData.ExportItemData(); } catch (Exception e) { Log.WriteWarningLineLoc($"Export items error: {e.Message}"); };
            try { WikiData.ExportRecipeData(); } catch (Exception e) { Log.WriteWarningLineLoc($"Export recipes error: {e.Message}"); };
            try { WikiData.ExportBiomeData(); } catch (Exception e) { Log.WriteWarningLineLoc($"Export biomes error: {e.Message}"); };
            try { WikiData.ExportEcopediaData(); } catch (Exception e) { Log.WriteWarningLineLoc($"Export ecopedia error: {e.Message}"); };
            try { WikiData.ExportAchievementsData(); } catch (Exception e) { Log.WriteWarningLineLoc($"Export achievement error: {e.Message}"); }
            ;

            if (DebugMode is true) { Eco.Server.PluginManager.Obj.FireShutdown();
            }
        }
    }
}