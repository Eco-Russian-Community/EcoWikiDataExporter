using Eco.Core.Controller;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.EcopediaRoot;
using Eco.Gameplay.Housing.PropertyValues;
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

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        public class EcoVersionData
        {
            public string Version { get; set; }
            public string VersionNumber { get; set; }
            public string FullVersion { get; set; }
        }
        public class TranslateData
        {
            public string English { get; set; }
            public string Russian { get; set; }
            public string German { get; set; }
            public string French { get; set; }
            public string Japanese { get; set; }
        }

        public class MarketplaceData
        {
            public string Category { get; set; }
            public string Price { get; set; }
            public string Quantity { get; set; }
            public string Achievement { get; set; }
        }

        public class AchievementData
        {
            public TranslateData Name { get; set; }
            public TranslateData Description { get; set; }
            public string IconName { get; set; }
        }

        public class TagData
        {
            public string ID { get; set; }
            public TranslateData Name { get; set; }
            public bool Hidden { get; set; }
            public bool IsVisibleInTooltip { get; set; }
            public bool IsVisibleInEcopedia { get; set; }
            public bool IsVisibleInFilter { get; set; }
            public List<string> Items { get; set; }
        }

        public class SkillData
        {
            public TranslateData Name { get; set; }
            public TranslateData Description { get; set; }
            public string SkillID { get; set; }
            public int MaxLevel { get; set; }
            public int Tier { get; set; }
            public int SpecialtyCost { get; set; }
            public bool IsRoot { get; set; }
            public string RootSkill { get; set; }
            public bool PlayerDefaultSkill { get; set; }
        }

        public class RoomTierData
        {
            public string SoftCap { get; set; }
            public string HardCap { get; set; }
            public string Percent { get; set; }
        }

        public class RoomData
        {
            public TranslateData Name { get; set; }
            public bool IsRoom { get; set; }
            public bool NegatesValue { get; set; }
            public string Percent { get; set; }
            public string Color { get; set; }
        }

        // SupportingRooms

        public class CommandData
        {
            public string Command { get; set; }
            public string Level { get; set; }
            public string ShortCut { get; set; }
            public string Parent { get; set; }
            public TranslateData Description { get; set; }
            public Dictionary <string, string> Parameters { get; set; }
        }

        public class TalentData
        {
            public TranslateData Name { get; set; }
            public TranslateData Description { get; set; }
            public string IconName { get; set; }
            public string SkillID { get; set; }
            public int Level { get; set; }
        }

        public class ItemData
        {
            public string ID { get; set; }
            public TranslateData Name { get; set; }
            public TranslateData Description { get; set; }
            public bool Hidden { get; set; }
            public int Tier { get; set; }
            public int Weight { get; set; }
            public int MaxStackSize { get; set; }
            public List<string> Tags { get; set; }
        }

        public class FoodData
        {
            public string Calories { get; set; }
            public string Carbs { get; set; }
            public string Protein { get; set; }
            public string Fat { get; set; }
            public string Vitamins { get; set; }
            public string ShelfLife { get; set; }
        }

        public class FertilizerData
        {
            public string Nitrogen { get; set; }
            public string Phosphorus { get; set; }
            public string Potassium { get; set; }
        }

        public class SeedData
        {
            public string Species { get; set; }
        }

        public class ClothingData
        {
            public string AvatarSlot { get; set; }
            public bool StartClothing { get; set; }
            public bool Hidden { get; set; }
            public Dictionary<string, string> FlatStats { get; set; }
        }



        public class ToolData
        {
            public string ToolType { get; set; }
            public bool Hidden { get; set; }
            public string Tier { get; set; }
            public bool Weapon { get; set; }
        }

        public class FuelData
        {
            public string Power { get; set; }
        }

        public class WorldObjectData
        {
            public string Components { get; set; }
        }

        public class RecipeData
        {
            public string CraftTime { get; set; }
            public string Experience { get; set; }
            public string LaborInCalories { get; set; }
            public string RequiredSkill { get; set; }
            public string RequiresModule { get; set; }
            public string CraftingTable { get; set; }
            public bool RequiresBlueprint { get; set; }
            public SortedDictionary<string, RecipeIngredientData> Ingredients { get; set; }
            public SortedDictionary<string, RecipeProductData> Products { get; set; }
            public SortedDictionary<string, RecipeGarbageData> Garbages { get; set; }
        }

        public class RecipeIngredientData
        {
            public string Type { get; set; }
            public string Name { get; set; }
            public string ID { get; set; }
            public string Quantity { get; set; }
            public bool IsStatic { get; set; }
        }

        public class RecipeProductData
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string Type { get; set; }
            public string Quantity { get; set; }
            public bool IsStatic { get; set; }
        }

        public class RecipeGarbageData
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string Quantity { get; set; }
        }

        public class TreeData
        {
            public string ID { get; set; }
            public TranslateData Name { get; set; }
            public string MaturityAgeDays { get; set; }

        }

    }
}