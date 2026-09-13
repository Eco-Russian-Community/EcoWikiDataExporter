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
using Eco.Gameplay.Property;
using Eco.Gameplay.Rooms;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Gameplay.Systems.NewTooltip;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Mods.TechTree;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.IoC;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using Newtonsoft.Json;
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
using System.Xml.Linq;
using static Eco.Gameplay.Housing.PropertyValues.Internal.RoomTierUtils;
using static Eco.Mods.EcoWikiDataExporter.WikiData;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        private static Dictionary<string, RoomData> RoomDataList = new Dictionary<string, RoomData>();
        public static void ExportRoomData()
        {

            IEnumerable<RoomCategory> rooms = HousingConfig.AllCategories;

            foreach (RoomCategory roomCategory in rooms)
            {
                string RoomName = roomCategory.DisplayName.NotTranslated;
                if (!RoomDataList.ContainsKey(RoomName) && (RoomName != "Uncategorized")) {

                    var SupportRooms = HousingConfig.AllCategories.Where(x => x.SupportingRoomCategoryNames?.Contains(roomCategory.Name) ?? false);
                    string canSupport = "";
                    foreach (var supportRoom in SupportRooms) { canSupport += "'" + supportRoom.DisplayName.NotTranslated + "',"; }
                    if (canSupport != "") 
                    { 
                        //RoomData[RoomName]["SupportingRooms"] = "{" + $"{canSupport}" + "}";
                    }

                    string RoomPropertyType = "";
                    foreach (var PropertyType in roomCategory.AffectsPropertyTypes) { RoomPropertyType += "'" + PropertyType.ToString() + "',"; }
                    //RoomData[RoomName]["PropertyType"] = "{" + $"{RoomPropertyType}" + "}";

                    RoomData roomsdata = new RoomData
                    {
                        Name = Localization(RoomName),
                        Color = roomCategory.DisplayNameColored.ToString().Substring(7, 9),
                        IsRoom = roomCategory.CanBeRoomCategory,
                        NegatesValue = roomCategory.NegatesValue

                    };

                    //RoomData[RoomName]["SupportForAnyRoom"] = $"'{roomCategory.SupportForAnyRoomType.ToString()}'";
                    //RoomData[RoomName]["MaxSupportPercentOfPrimary"] = $"'{(Math.Round(roomCategory.MaxSupportPercentOfPrimary * 100)).ToString("G", CultureInfo.InvariantCulture)}'";

                    //RoomData[RoomName]["CapFromMaterials"] = $"'{roomCategory.ShouldCapFromRoomMaterials.ToString()}'";

                    
                    RoomDataList.Add(RoomName, roomsdata);
                }
            }

            for (int i = 0; i <= 5; i++)
            {
                var Roomtier = HousingConfig.GetRoomTier(i);
                string t = i.ToString();

                RoomTierData roomtiersdata = new RoomTierData
                {
                    SoftCap = (Math.Round(Roomtier.SoftCap)).ToString("G", CultureInfo.InvariantCulture),
                    HardCap = (Math.Round(Roomtier.HardCap)).ToString("G", CultureInfo.InvariantCulture),
                    Percent = (Math.Round(Roomtier.DiminishingReturnPercent * 100)).ToString("G", CultureInfo.InvariantCulture)
                };


            }

            //RoomDataList.Add("Tiers", roomtiersdata);


            // writes to json file
            string jsonString = JsonConvert.SerializeObject(new { rooms = RoomDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("Rooms", jsonString);
        }
    }
}
