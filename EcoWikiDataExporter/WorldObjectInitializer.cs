using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Objects;
using Eco.Shared.Logging;
using Eco.Shared.Math;
using System;
using System.Numerics;
using Quaternion = Eco.Shared.Math.Quaternion;

namespace Eco.Mods.EcoWikiDataExporter
{
	internal class WorldObjectInitializer
	{
        private static User dummy;
        static Vector3i spawnPoint = new Vector3i(0, 75, 0);
        public WorldObject? Init(WorldObjectItem item)
		{
			ArgumentNullException.ThrowIfNull(item);
			Type worldObjectType = item.WorldObjectType;
			//WorldObject worldObject = (WorldObject)Activator.CreateInstance(worldObjectType, true) ?? throw new Exception($"Error create instance of worldObject: {worldObjectType}");
			
			try
			{
				WorldObject worldObject = WorldObjectManager.ForceAdd(worldObjectType, dummy, spawnPoint, Quaternion.Identity, false);
				worldObject.DoInitializationSteps();
				return worldObject;
			}
			catch (Exception ex)
			{
				Log.WriteException(ex);
			}
			return null;
		}
	}
}
