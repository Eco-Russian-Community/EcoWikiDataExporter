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
using System.Globalization;
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
using Eco.Gameplay.Systems.TextLinks;
using Eco.Gameplay.Systems.NewTooltip;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        private static SortedDictionary<string, Dictionary<string, string>> RoomData = new SortedDictionary<string, Dictionary<string, string>>();
        private static SortedDictionary<string, Dictionary<string, string>> RoomTierData = new SortedDictionary<string, Dictionary<string, string>>();
        public static void ExportRoomData()
        {
            Dictionary<string, string> roomDetails = new Dictionary<string, string>()
            {
                { "Name", "nil" },
                { "IsRoom", "nil" },
                { "SupportingRooms", "nil" },
            };

            Dictionary<string, string> roomTierDetails = new Dictionary<string, string>()
            {
                { "SoftCap", "nil" },
                { "HardCap", "nil" },
                { "Percent", "nil" },
            };

            IEnumerable<RoomCategory> rooms = HousingConfig.AllCategories;

            foreach (RoomCategory roomCategory in rooms)
            {
                string RoomName = roomCategory.DisplayName.NotTranslated;
                if (!RoomData.ContainsKey(RoomName) && (RoomName != "Uncategorized")) {
                    RoomData.Add(RoomName, new Dictionary<string, string>(roomDetails));
                    RoomData[RoomName]["Name"] = WriteDictionaryAsSubObject(Localization(RoomName), 1);
                    RoomData[RoomName]["Color"] = $"'{roomCategory.DisplayNameColored.ToString().Substring(7, 9)}'";
                    RoomData[RoomName]["IsRoom"] = $"'{roomCategory.CanBeRoomCategory.ToString()}'";
                    RoomData[RoomName]["SupportForAnyRoom"] = $"'{roomCategory.SupportForAnyRoomType.ToString()}'";
                    RoomData[RoomName]["MaxSupportPercentOfPrimary"] = $"'{(Math.Round(roomCategory.MaxSupportPercentOfPrimary * 100)).ToString("G", CultureInfo.InvariantCulture)}'";
                    RoomData[RoomName]["NegatesValue"] = $"'{roomCategory.NegatesValue.ToString()}'";
                    RoomData[RoomName]["CapFromMaterials"] = $"'{roomCategory.ShouldCapFromRoomMaterials.ToString()}'";
                    
                    var SupportRooms = HousingConfig.AllCategories.Where(x => x.SupportingRoomCategoryNames?.Contains(roomCategory.Name) ?? false);
                    string canSupport = "";
                    foreach (var supportRoom in SupportRooms) { canSupport += "'" + supportRoom.DisplayName.NotTranslated + "',"; }
                    if (canSupport != "") { RoomData[RoomName]["SupportingRooms"] = "{" + $"{canSupport}" + "}"; }

                    string RoomPropertyType = "";
                    foreach (var PropertyType in roomCategory.AffectsPropertyTypes) { RoomPropertyType += "'" + PropertyType.ToString() + "',"; }
                    RoomData[RoomName]["PropertyType"] = "{" + $"{RoomPropertyType}" + "}";
                    
                }
            }

            for (int i = 0; i <= 5; i++)
            {
                var Roomtier = HousingConfig.GetRoomTier(i);
                string t = i.ToString();
                RoomTierData.Add(t, new Dictionary<string, string>(roomTierDetails));
                RoomTierData[t]["SoftCap"] = $"'{(Math.Round(Roomtier.SoftCap)).ToString("G", CultureInfo.InvariantCulture)}'";
                RoomTierData[t]["HardCap"] = $"'{(Math.Round(Roomtier.HardCap)).ToString("G", CultureInfo.InvariantCulture)}'";
                RoomTierData[t]["Percent"] = $"'{(Math.Round(Roomtier.DiminishingReturnPercent * 100)).ToString("G", CultureInfo.InvariantCulture)}'";
            }
            
            // writes to txt file
            WriteDictionaryToFile("RoomData", "rooms", RoomData);
            WriteDictionaryToFile("RoomTierData", "roomstiers", RoomTierData);
        }
    }
}
