using Eco.Core.Controller;
using Eco.Core.Items;
using Eco.Core.Plugins;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.DynamicValues;
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
using Eco.Shared.Logging;
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
using System.Xml.Linq;

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
                { "CraftingTables", "nil" },
                { "Ingredients", "nil" },
                { "Products", "nil" },
            };

            Dictionary<string, string> recipeIngredientsDetails = new Dictionary<string, string>()
            {
                { "Type", "nil" },
                { "Name", "nil" },
                { "ID", "nil" },
                { "Quantity", "nil" },
                { "isStatic", "'False'" },
            };

            Dictionary<string, string> recipeProductsDetails = new Dictionary<string, string>()
            {
                { "Name", "nil" },
                { "ID", "nil" },
                { "Quantity", "nil" },
                { "isStatic", "'False'" },
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
                        RecipeData[RecipeID]["CraftTime"] = $"'{(recipe.CraftMinutes.GetBaseValue * 60).ToString("G", CultureInfo.InvariantCulture)}'";
                        RecipeData[RecipeID]["ExperienceOnCraft"] = $"'{recipe.ExperienceOnCraft.ToString("G", CultureInfo.InvariantCulture)}'";
                        RecipeData[RecipeID]["LaborInCalories"] = $"'{recipe.LaborInCalories.GetBaseValue.ToString("G", CultureInfo.InvariantCulture)}'";

                        var skill = recipe.RequiredSkills.FirstOrDefault();
                        string RequiredSkill = skill != null ? Item.Get(skill.SkillType).Name : "nil";
                        int RequiredSkillLevel = skill?.Level ?? 0;

                        RecipeData[RecipeID]["RequiredSkill"] = "{" + $"'{RequiredSkill}'" + "," + $"'{RequiredSkillLevel}'" + "}";
                        RecipeData[RecipeID]["CraftingTables"] = $"'{recipe.CraftingTable}'";

                        SortedDictionary<string, Dictionary<string, string>> Ingredients = new SortedDictionary<string, Dictionary<string, string>>();
                        foreach (var recipeingredient in recipevariant.Ingredients)
                        {

                            //var Ingredient = new Dictionary<string, string>();
                            string Ingredienttype;
                            string Ingredientname;
                            bool isStatic = false;

                            if (recipeingredient.IsSpecificItem) { 
                                Ingredienttype = "ITEM"; 
                                Ingredientname = recipeingredient.Item.DisplayName.NotTranslated;     
                            } else {
                                Ingredienttype = "TAG";
                                Ingredientname = recipeingredient.Tag.DisplayName.NotTranslated;
                            }
                            string IngredientQuantity = recipeingredient.Quantity.GetBaseValue.ToString("G", CultureInfo.InvariantCulture);

                            Ingredients.Add(Ingredientname, new Dictionary<string, string>(recipeIngredientsDetails));

                            Ingredients[Ingredientname]["Type"] = $"'{Ingredienttype}'";
                            Ingredients[Ingredientname]["Name"] = $"'{Ingredientname}'";
                            Ingredients[Ingredientname]["Quantity"] = $"'{IngredientQuantity}'";
                            if (recipeingredient.Quantity is ConstantValue) { Ingredients[Ingredientname]["isStatic"] = $"'True'";  }

                            RecipeData[RecipeID]["Ingredients"] = EcoWikiDataManager.WriteDictionaryAsSubObject(Ingredients, 1);
                        }

                        SortedDictionary<string, Dictionary<string, string>> Products = new SortedDictionary<string, Dictionary<string, string>>();
                        foreach (var recipeproduct in recipevariant.Products)
                        {
                            string Productname = recipeproduct.Item.DisplayName.NotTranslated;
                            string ProductQuantity = recipeproduct.Quantity.GetBaseValue.ToString("G", CultureInfo.InvariantCulture);
                            Products.Add(Productname, new Dictionary<string, string>(recipeProductsDetails));

                            Products[Productname]["Type"] = $"'ITEM'";
                            Products[Productname]["Name"] = $"'{Productname}'";
                            Products[Productname]["ID"] = $"'{recipeproduct.Item.Type.Name}'";
                            Products[Productname]["Quantity"] = $"'{ProductQuantity}'";
                            if (recipeproduct.Quantity is ConstantValue) { Products[Productname]["isStatic"] = $"'True'"; }

                            RecipeData[RecipeID]["Products"] = EcoWikiDataManager.WriteDictionaryAsSubObject(Products, 1);
                        }
                    }
                }
            }

            // writes to txt file
            EcoWikiDataManager.WriteDictionaryToFile("RecipesData", "recipes", RecipeData);

        }



    }
}
