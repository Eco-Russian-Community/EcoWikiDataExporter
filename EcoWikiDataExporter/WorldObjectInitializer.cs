using System;
using System.Numerics;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Shared.Logging;
using Quaternion = Eco.Shared.Math.Quaternion;

namespace Eco.Mods.EcoWikiDataExporter
{
	internal class WorldObjectInitializer
	{
		public WorldObject? Init(WorldObjectItem item)
		{
			ArgumentNullException.ThrowIfNull(item);
			Type worldObjectType = item.WorldObjectType;
			//WorldObject worldObject = (WorldObject)Activator.CreateInstance(worldObjectType, true) ?? throw new Exception($"Error create instance of worldObject: {worldObjectType}");
			
			try
			{
				WorldObject worldObject = WorldObjectManager.ForceAdd(worldObjectType, null, Vector3.Zero, Quaternion.Identity);
				return worldObject;
				//worldObject.DoInitializationSteps();
			}
			catch (Exception ex)
			{
				Log.WriteException(ex);
			}
			return null;
		}
	}
}
