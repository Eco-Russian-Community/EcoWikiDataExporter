using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;

namespace Eco.Mods.EcoWikiDataExporter
{
	[LocDisplayName(nameof(EcoWikiDataExporter))]
	public class EcoWikiDataExporter : IModKitPlugin, IServerPlugin, IInitializablePlugin
	{
		public const string Version = "0.1.0";

		public void Initialize(TimedTask timer)
		{
			
		}

		public string GetCategory()
		{
			return string.Empty;
		}

		public string GetStatus()
		{
			return string.Empty;
		}

		public override string ToString()
		{
			return "Eco.Mods.EcoWikiDataExporter";
		}
	}
}
