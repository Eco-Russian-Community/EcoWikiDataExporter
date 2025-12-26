using System;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Shared.Logging;

namespace Eco.Mods.EcoWikiDataExporter
{
	internal class WorldObjectInitializer
	{
		public WorldObject Init(WorldObjectItem item)
		{
			//ArgumentNullException.ThrowIfNull(item);
			Type worldObjectType = item.WorldObjectType;
			WorldObject worldObject = (WorldObject)Activator.CreateInstance(worldObjectType, true) ?? throw new Exception($"Error create instance of worldObject: {worldObjectType}");
			try
			{
				worldObject.DoInitializationSteps();
			}
			catch (Exception ex)
			{
				Log.WriteException(ex);
			}
			return worldObject;
		}
	}
}
