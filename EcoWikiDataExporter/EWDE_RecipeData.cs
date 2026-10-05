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
using Eco.Gameplay.Utils;
using Eco.Shared;
using Eco.Shared.Icons;
using Eco.Shared.IoC;
using Eco.Shared.Localization;
using Eco.Shared.Logging;
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
using static Eco.Mods.EcoWikiDataExporter.WikiData;

namespace Eco.Mods.EcoWikiDataExporter
{
	public partial class WikiData
    {
        private static SortedDictionary<string, RecipeData> RecipesDataList = new SortedDictionary<string, RecipeData>();
        public static void ExportRecipeData()
        {

            var EcoRecipes = RecipeManager.AllRecipeFamilies;

            foreach (RecipeFamily recipe in EcoRecipes)
            {
                string BaseRecipeName = recipe.RecipeName;
                
                foreach (Recipe recipevariant in recipe.Recipes)
                {
                    string RecipeName = recipevariant.DisplayName.NotTranslated;
                    string RecipeID = RecipeName.Replace(" ", "") + "Recipe";
                    
                    if (!RecipesDataList.ContainsKey(RecipeID))
                    {
                        var skill = recipe.RequiredSkills.FirstOrDefault();
                        string requiredskill = skill != null ? Item.Get(skill.SkillType).Name : "nil";
                        int requiredskilllevel = skill?.Level ?? 0;

                        var module = recipe.RequiredModules.FirstOrDefault();
                        string requiredmodule = module != null ? Item.CreatingItem(module.ModuleType).DisplayName.NotTranslated : "nil";

                        SortedDictionary<string, RecipeIngredientData> IngredientsList = new SortedDictionary<string, RecipeIngredientData>();
                        foreach (var recipeingredient in recipevariant.Ingredients)
                        {
                            string Ingredienttype;
                            string Ingredientname;
                            string IngredientID;

                            if (recipeingredient.IsSpecificItem) {
                                Ingredienttype = "ITEM";
                                Ingredientname = recipeingredient.Item.DisplayName.NotTranslated;
                                IngredientID = recipeingredient.Item.Type.Name;
                            } else {
                                Ingredienttype = "TAG";
                                Ingredientname = recipeingredient.Tag.DisplayName.NotTranslated;
                                IngredientID = recipeingredient.Tag.Name;
                            }
                            string IngredientQuantity = WikiFloat(recipeingredient.Quantity.GetBaseValue);
                            bool IngredientIsStatic = false;
                            if (recipeingredient.Quantity is ConstantValue) { IngredientIsStatic = true; }

                            RecipeIngredientData recipeingredientdata = new RecipeIngredientData
                            {
                                Type = Ingredienttype,
                                Name = Ingredientname,
                                ID = IngredientID,
                                Quantity = IngredientQuantity,
                                IsStatic = IngredientIsStatic
                            };

                            IngredientsList.Add(Ingredientname, recipeingredientdata);
                        }

                        SortedDictionary<string, RecipeProductData> ProductsList = new SortedDictionary<string, RecipeProductData>();
                        foreach (var recipeproduct in recipevariant.Products)
                        {
                            string Productname = recipeproduct.Item.DisplayName.NotTranslated;
                            string ProductQuantity = WikiFloat(recipeproduct.Quantity.GetBaseValue);
                            string Producttype = "ITEM";
                            bool ProductIsStatic = false;
                            if (recipeproduct.Quantity is ConstantValue) { ProductIsStatic = true; }

                            RecipeProductData recipeproductdata = new RecipeProductData
                            {
                                Type = Producttype,
                                Name = Productname,
                                ID = recipeproduct.Item.Type.Name,
                                Quantity = ProductQuantity,
                                IsStatic = ProductIsStatic
                            };

                            ProductsList.Add(Productname, recipeproductdata);
                        }

                        SortedDictionary<string, RecipeGarbageData> GarbagesList = new SortedDictionary<string, RecipeGarbageData>();
                        foreach (var recipegarbage in recipevariant.TotalGarbages)
                        {
                            string Garbagename = recipegarbage.GarbageMaterialType.Name;
                            string GarbageQuantity = Percent(recipegarbage.Quantity.GetBaseValue);

                            RecipeGarbageData recipegarbagedata = new RecipeGarbageData
                            {
                                Name = Garbagename.AddSpacesBetweenCapitals(),
                                ID = recipegarbage.IconName,
                                Quantity = GarbageQuantity
                            };

                            GarbagesList.Add(Garbagename, recipegarbagedata);
                        }

                        RecipeData recipedata = new RecipeData
                        {
                            CraftTime = (Math.Round(recipe.CraftMinutes.GetBaseValue * 60)).ToString("G", CultureInfo.InvariantCulture),
                            Experience = WikiFloat(recipe.ExperienceOnCraft),
                            LaborInCalories = WikiFloat(recipe.LaborInCalories.GetBaseValue),
                            RequiredSkill = requiredskill + "," + requiredskilllevel,
                            RequiresModule = requiredmodule,
                            CraftingTable = recipe.CraftingTable.DisplayName.NotTranslated,
                            RequiresBlueprint = recipevariant.RequiresStrangeBlueprint,
                            Ingredients = IngredientsList,
                            Products = ProductsList,
                            Garbages = GarbagesList
                        };

                        RecipesDataList.Add(RecipeID, recipedata);
                    }
                }
            }

            // writes to json file
            string RecipesDataListjsonString = JsonConvert.SerializeObject(new { recipes = RecipesDataList }, Formatting.Indented);
            WriteDictionaryToJsonFile("Recipes", RecipesDataListjsonString);


        }



    }
}
