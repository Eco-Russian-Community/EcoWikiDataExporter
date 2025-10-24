using Eco.Core.Controller;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Civics.GameValues.Values;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Gameplay.Utils;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.IoC;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using Eco.Simulation;
using Eco.Simulation.Agents;
using Eco.Simulation.Types;
using System;
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

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        // dictionary of animals and their dictionary of stats
        private static SortedDictionary<string, Dictionary<string, string>> AnimalData = new SortedDictionary<string, Dictionary<string, string>>();

        public static void ExportAnimalData()
        {

        Dictionary<string, string> animalDetails = new Dictionary<string, string>()
        {
            { "ID", "nil" },
            { "Name", "nil" },
            { "Description", "nil" }
        };

         IEnumerable<Species> species = EcoSim.AllSpecies.Where(s => s is AnimalSpecies);
            foreach (Species s in species)
            {
                AnimalSpecies animal = s as AnimalSpecies;
                string animalName = animal.DisplayName.NotTranslated;
                if (!AnimalData.ContainsKey(animalName))
                {
                    AnimalData.Add(animalName, new Dictionary<string, string>(animalDetails));
                    AnimalData[animalName]["ID"] = $"'{animal.Name}" + "Species'";
                    AnimalData[animalName]["Name"] = WriteDictionaryAsSubObject(Localization(animalName), 1);
                    AnimalData[animalName]["Description"] = WriteDictionaryAsSubObject(Localization(animal.DisplayDescription.NotTranslated), 1);
                    
                    // Behavior
                    AnimalData[animalName]["MaturityAgeDays"] = $"'{WikiFloat(animal.MaturityAgeDays)}'";
                    AnimalData[animalName]["IsSwimming"] = $"'{animal.Swimming}'";
                    AnimalData[animalName]["IsFishable"] = $"'{animal.IsFishable}'";
                    AnimalData[animalName]["Nocturnal"] = $"'{animal.Nocturnal}'";
                    AnimalData[animalName]["Aquatic"] = $"'{animal.Aquatic}'"; 


                    AnimalData[animalName]["Health"] = $"'{WikiFloat(animal.Health)}'";
                    AnimalData[animalName]["Flags"] = $"'{animal.Flags}'";
                    

                    AnimalData[animalName]["TooCloseDistance"] = $"'{WikiFloat(animal.TooCloseDistance)}'";
                    AnimalData[animalName]["MaxVisibilityDistance"] = $"'{WikiFloat(animal.MaxVisibilityDistance)}'";
                    AnimalData[animalName]["MaxVisibilityAngle"] = $"'{WikiFloat(animal.MaxVisibilityAngle)}'";
                    AnimalData[animalName]["AttackRange"] = $"'{WikiFloat(animal.AttackRange)}'";
                    AnimalData[animalName]["ChanceToAttack"] = $"'{WikiFloat(animal.ChanceToAttack)}'";
                    AnimalData[animalName]["ChanceToAttackUnprovoked"] = $"'{WikiFloat(animal.ChanceToAttackUnprovoked)}'";
                    AnimalData[animalName]["AttackUnprovokedDistance"] = $"'{WikiFloat(animal.AttackUnprovokedDistance)}'";
                    AnimalData[animalName]["ChanceOfAlertNoise"] = $"'{WikiFloat(animal.ChanceOfAlertNoise)}'";
                    AnimalData[animalName]["AnimalDamage"] = $"'{WikiFloat(animal.AnimalDamage)}'";
                    AnimalData[animalName]["PlayerDamage"] = $"'{WikiFloat(animal.PlayerDamage)}'";
                    AnimalData[animalName]["HerdSizeMin"] = $"'{animal.HerdSize.Min}'";
                    AnimalData[animalName]["HerdSizeMax"] = $"'{animal.HerdSize.Max}'";

                    //Food
                    AnimalData[animalName]["CalorieValue"] = $"'{WikiFloat(animal.CalorieValue)}'";
                    AnimalData[animalName]["EatTags"] = $"'{animal.EatTags}'";

                    //Food Sources
                    var FoodSource = animal.FoodSourcesSpecies;
                    string FoodSourceList = "";
                    foreach (Species sp in FoodSource)
                    {
                        if (FoodSourceList == "") { FoodSourceList = "'" + sp.DisplayName.NotTranslated + "'"; }
                        else { FoodSourceList = FoodSourceList + ",'" + sp.DisplayName.NotTranslated + "'";  }
                    }
                    FoodSourceList = "{" + FoodSourceList + "}";
                    AnimalData[animalName]["FoodSourcesSpecies"] = $"{FoodSourceList}";

                    // Movement
                    AnimalData[animalName]["CanSwimNearCoast"] = $"'{animal.CanSwimNearCoast}'";

                    // Resources
                    if (animal.ResourceItemType != null)
                    {
                        AnimalData[animalName]["ResourceItem"] = $"'{animal.ResourceItemType.Name}'";
                        AnimalData[animalName]["ResourceMin"] = $"'{animal.ResourceRange.Min}'";
                        AnimalData[animalName]["ResourceMax"] = $"'{animal.ResourceRange.Max}'";
                        AnimalData[animalName]["ResourceBonusAtGrowth"] = $"'{WikiFloat(animal.ResourceBonusAtGrowth)}'";
                    }

                    // Population Growth
                    AnimalData[animalName]["TimeTill50PercentCloserToMaxInHours"] = $"'{WikiFloat(animal.TimeTill50PercentCloserToMaxInHours)}'";
                    AnimalData[animalName]["MaxGrowthRatePerHour"] = $"'{WikiFloat(animal.MaxGrowthRatePerHour)}'";
                    AnimalData[animalName]["TimeToSpread1ToNeighborInHours"] = $"'{WikiFloat(animal.TimeToSpread1ToNeighborInHours)}'";

                    // Climate
                    AnimalData[animalName]["ReleasesCO2TonsPerDay"] = $"'{WikiFloat(animal.ReleasesCO2TonsPerDay)}'";
                }
            }
        // writes to txt file
        WriteDictionaryToFile("AnimalData", "animals", AnimalData);
        }

    }
}
