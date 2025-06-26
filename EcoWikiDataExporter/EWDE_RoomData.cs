using Eco.Core.Controller;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Housing.PropertyValues.Internal;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Rooms;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Mods.TechTree;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.IoC;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using System;
using System.Collections;
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
using static Eco.Gameplay.Housing.PropertyValues.Internal.RoomTierUtils;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        private static SortedDictionary<string, Dictionary<string, string>> RoomData = new SortedDictionary<string, Dictionary<string, string>>();
        public static void ExportRoomData()
        {
            Dictionary<string, string> roomDetails = new Dictionary<string, string>()
            {
                { "Name", "nil" },
            };

            IEnumerable<RoomCategory> rooms = HousingConfig.AllCategories;

            foreach (RoomCategory roomCategory in rooms)
            {
                string RoomName = roomCategory.DisplayName.NotTranslated;
                if (!RoomData.ContainsKey(RoomName) && (RoomName != "Uncategorized")) {
                    RoomData.Add(RoomName, new Dictionary<string, string>(roomDetails));
                    RoomData[RoomName]["Name"] = EcoWikiDataManager.WriteDictionaryAsSubObject(EcoWikiDataManager.Localization(RoomName), 1);
                    RoomData[RoomName]["Color"] = roomCategory.DisplayNameColored.ToString();
                    RoomData[RoomName]["CanBeRoomCategory"] = roomCategory.CanBeRoomCategory.ToString();
                    RoomData[RoomName]["SupportForAnyRoomType"] = roomCategory.SupportForAnyRoomType.ToString();
                    RoomData[RoomName]["MaxSupportPercentOfPrimary"] = roomCategory.MaxSupportPercentOfPrimary.ToString();
                    RoomData[RoomName]["SupportingRooms"] = roomCategory.SupportingRoomCategoryNames.ToString();
                    RoomData[RoomName]["NegatesValue"] = roomCategory.NegatesValue.ToString();
                    RoomData[RoomName]["PropertyType"] = roomCategory.AffectsPropertyTypes.ToString();
                }
            }

        // writes to txt file
        EcoWikiDataManager.WriteDictionaryToFile("RoomData", "rooms", RoomData);
        }
    }
}
