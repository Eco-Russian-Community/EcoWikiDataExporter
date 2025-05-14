using System;
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
using Eco.Core.Controller;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Shared.Icons;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Utils;
using Eco.Gameplay.Systems;
using Eco.Shared;
using Eco.Shared.IoC;
using Eco.Simulation.Types;
using Eco.Simulation;
using System.Globalization;
using Eco.Simulation.Agents;

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
            { "untranslated", "nil" }

        };

         IEnumerable<Species> species = EcoSim.AllSpecies.Where(s => s is AnimalSpecies);
            foreach (Species s in species)
            {
                AnimalSpecies animal = s as AnimalSpecies;
                string animalName = animal.DisplayName;
                if (!AnimalData.ContainsKey(animalName))
                {
                    AnimalData.Add(animalName, new Dictionary<string, string>(animalDetails));
                    AnimalData[animalName]["MaturityAgeDays"] = $"'{animal.MaturityAgeDays}'";
                    AnimalData[animalName]["isSwimming"] = $"'{animal.Swimming}'";
                    AnimalData[animalName]["isFlying"] = $"'{animal.Flying}'";
                    AnimalData[animalName]["IsPredator"] = $"'{animal.IsPredator}'";
                    AnimalData[animalName]["IsFishable"] = $"'{animal.IsFishable}'";


                    // Climate
                    AnimalData[animalName]["ReleasesCO2TonsPerDay"] = $"'{animal.ReleasesCO2TonsPerDay}'";
                }
            }
        // writes to txt file
        EcoWikiDataManager.WriteDictionaryToFile("AnimalData", "animals", AnimalData);
        }

    }
}
