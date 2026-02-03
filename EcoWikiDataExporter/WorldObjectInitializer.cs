using Eco.Gameplay.Utils;
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
        static Vector3i spawnPoint = new Vector3i(0, 75, 0);
        public WorldObject? Init(WorldObjectItem item)
		{
			ArgumentNullException.ThrowIfNull(item);
			Type worldObjectType = item.WorldObjectType;
			var WikiUser = TestUtils.TestUser;

            try
			{
				WorldObject worldObject = WorldObjectManager.ForceAdd(worldObjectType, WikiUser, spawnPoint, Quaternion.Identity, false);
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
