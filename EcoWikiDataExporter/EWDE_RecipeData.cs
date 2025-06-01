using Eco.Core.Controller;
using Eco.Core.Items;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
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
        private static SortedDictionary<string, Dictionary<string, string>> RecipeData = new SortedDictionary<string, Dictionary<string, string>>();
        public static void ExportRecipeData()
        {
            // dictionary of recipe properties
            Dictionary<string, string> recipeDetails = new Dictionary<string, string>()
            {
                { "Name", "nil" },
                { "CraftTime", "nil" },
                { "ExperienceOnCraft", "nil" },
                { "LaborInCalories", "nil" },
                { "RequiredSkill", "nil" },
                { "Ingredients", "nil" },
                { "Products", "nil" },
            };

            Dictionary<string, string> recipeIngredientsDetails = new Dictionary<string, string>()
            {
                { "Type", "nil" },
                { "Name", "nil" },
                { "Quantity", "nil" },
            };


            var EcoRecipes = RecipeManager.AllRecipeFamilies;

            foreach (RecipeFamily recipe in EcoRecipes)
            {
                string BaseRecipeName = recipe.RecipeName;
                
                if (!RecipeData.ContainsKey(BaseRecipeName))
                {
                    foreach (Recipe recipevariant in recipe.Recipes)
                    {
                        string RecipeName = recipevariant.DisplayName.NotTranslated;
                        string RecipeID = RecipeName.Replace(" ", "");

                        RecipeData.Add(RecipeID, new Dictionary<string, string>(recipeDetails));
                        RecipeData[RecipeID]["Name"] = EcoWikiDataManager.WriteDictionaryAsSubObject(EcoWikiDataManager.Localization(RecipeName), 1);
                        RecipeData[RecipeID]["CraftTime"] = (recipe.CraftMinutes.GetBaseValue * 60).ToString("G", CultureInfo.InvariantCulture);
                        RecipeData[RecipeID]["ExperienceOnCraft"] = recipe.ExperienceOnCraft.ToString("G", CultureInfo.InvariantCulture);
                        RecipeData[RecipeID]["LaborInCalories"] = recipe.LaborInCalories.GetBaseValue.ToString("G", CultureInfo.InvariantCulture);

                        foreach (var recipeingredient in recipevariant.Ingredients)
                        {
                            
                            
                            if (recipeingredient.IsSpecificItem) { 
                                string ingredienttype = "ITEM"; 
                                string Ingredient = recipeingredient.Item.DisplayName.NotTranslated;
                                RecipeData[RecipeID]["Ingredients"].Add(Ingredient, new Dictionary<string, string>(recipeIngredientsDetails));
                            } else {
                                string ingredienttype = "TAG"; 
                            }

                            //RecipeData[RecipeID]["Ingredients"][""]["Name"] =
                            
                           
                            
                            RecipeData[RecipeID]["Ingredients"]["bb"]["Type"] = $"'{ingredienttype}'";
                            RecipeData[RecipeID]["Ingredients"]["bbb"]["Quantity"] = ;

                        }


                    }
                }
            }

            // writes to txt file
            EcoWikiDataManager.WriteDictionaryToFile("RecipesData", "recipes", RecipeData);

        }



    }
}
