local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')
local RecipesData = require('Module:RecipeData')
local ItemsData = require('Module:ItemData')
local TagsData = require('Module:TagData')
local SkillsData = require('Module:SkillData')
local Lang = Utils.getLanguageName()
local p = {}

function p.ItemCraft(ItemName)
    local Recipes = ""
    for RecipeName,RecipeData in pairs(RecipesData.recipes) do
        for ProductName,ProductData in pairs(RecipeData.Products) do
            if ProductName == ItemName and ProductData.Type == "ITEM" then
                if (Recipes == "") then Recipes = Recipes .. RecipeName else Recipes = Recipes .. "," .. RecipeName end
            end
        end
    end
           
    return Recipes
end

function p.TagIngredient(TagName)
    local Recipes = ""
    for RecipeName,RecipeData in pairs(RecipesData.recipes) do
        for IngredientName,IngredientData in pairs(RecipeData.Ingredients) do
            if IngredientName == TagName and IngredientData.Type == "TAG" then
                if (Recipes == "") then Recipes = Recipes .. RecipeName else Recipes = Recipes .. "," .. RecipeName end
            end
        end
    end
    return Recipes
end

function p.TagsIngredient(TagsList)
    local Recipes = ""
	for Count, TagName in pairs(TagsList) do
		RecipeList = p.TagIngredient(TagName)
		if (Recipes == "") then Recipes = Recipes .. RecipeList else Recipes = Recipes .. "," .. RecipeList end
	end
	
	Recipes = Utils.CheckList(Recipes)
	
    return Recipes
end

function p.ItemIngredient(ItemName)
    local Recipes = ""
    for RecipeName,RecipeData in pairs(RecipesData.recipes) do
        for IngredientName,IngredientData in pairs(RecipeData.Ingredients) do
            if IngredientName == ItemName and IngredientData.Type == "ITEM" then
                if (Recipes == "") then Recipes = Recipes .. RecipeName else Recipes = Recipes .. "," .. RecipeName end
            end
        end
    end
    return Recipes
end

function p.CraftTable(RecipeList)
    local CraftTable = ""

    if (RecipeList ~= "") then
        CraftTable = CraftTable .. '<table class="table table-striped table-bordered sortable"><tr class="thead-dark">';
        CraftTable = CraftTable .. '<th>' .. Utils.Translate("Crafting Table") .. '</th><th class="unsortable">' .. Utils.Translate("Products") .. '</th><th class="unsortable">' .. Utils.Translate("Ingredients") .. '</th><th data-sort-type="mm:ss">' .. Utils.Translate("Craft time") .. '</th><th>' .. Utils.Translate("Labor") .. '</th><th>' .. Utils.Translate("Skill Requirements") .. '</th><th>' .. Utils.Translate("Experience") .. '</th></tr>';
        
        for RecipeName in string.gmatch(RecipeList, "([^,]+)") do
            local CraftTableRow = "";
            local RecipeData = RecipesData.recipes[RecipeName];
            local CraftTableData = ItemsData.items[RecipeData.CraftingTables]
            CraftTableRow = "<td>" .. IconUtils.main{ name = CraftTableData.Name[Lang], id = CraftTableData.ID, size = 48, style = 2, link = CraftTableData.Name[Lang] } .. "</td>";
            
            local RecipeProducts = "";
            for ProductName,ProductData in pairs(RecipeData.Products) do
            	local Item = ItemsData.items[ProductName]
                RecipeProducts = RecipeProducts .. "<span>" .. IconUtils.main{ name = Item.Name[Lang], id = Item.ID, size = 48, style = 2, link = Item.Name[Lang] } .. "</span>";
            end
            CraftTableRow = CraftTableRow .. "<td>" .. RecipeProducts .. "</td>";

            local RecipeIngredients = "";
            local TagString = Utils.Translate("{0} Tag");
            for IngredientName,IngredientData in pairs(RecipeData.Ingredients) do
            	if (IngredientData['Type'] == "TAG") then local Tag = TagsData.tags[IngredientName]; local TagLink = Utils.VSTranslate(TagString,Tag.Name[Lang]); RecipeIngredients = RecipeIngredients .. "<span>" .. IconUtils.main{ name = Tag.Name[Lang], id = Tag.ID, size = 48, style = 2, link = TagLink } .. "</span>"; 
            	else  local Item = ItemsData.items[IngredientName]; RecipeIngredients = RecipeIngredients .. "<span>" .. IconUtils.main{ name = Item.Name[Lang], id = Item.ID, size = 48, style = 2, link = Item.Name[Lang] } .. "</span>";
            	end
            	
            end
            if (RecipeData.RequiresStrangeBlueprint == "True") then RecipeIngredients = RecipeIngredients .. "<span>" .. IconUtils.main{ name = "Blueprint", id = "BlueprintItem", size = 48, style = 3, link = "Marketplace"} .. "</span>"; end
            CraftTableRow = CraftTableRow .. "<td>" .. RecipeIngredients .. "</td>";
            local CraftTime = tonumber(RecipeData.CraftTime)
			CraftTableRow = CraftTableRow .. "<td><span>" .. p.CraftTime(CraftTime) .. "</span></td>";
			CraftTableRow = CraftTableRow .. "<td><span>" .. RecipeData.LaborInCalories .. "</span></td>";

			CraftTableRow = CraftTableRow .. "<td>" .. p.RecipeRequiredSkill(RecipeData.RequiredSkill) .. "</td>";
			CraftTableRow = CraftTableRow .. "<td><span>" .. RecipeData.ExperienceOnCraft .. "</span></td>";

            CraftTable = CraftTable .. "<tr>" ..CraftTableRow .. "</tr>";
        end
        CraftTable = CraftTable .. "</table>";  
    end
    return CraftTable
end

function p.CraftTime(TimeInSeconds)
    local CraftTime = "";
    local Minutes = 0;
    local Seconds = 0;
    if (TimeInSeconds >= 60) then Minutes = math.floor(TimeInSeconds / 60); Seconds = TimeInSeconds - Minutes * 60; else Seconds = TimeInSeconds; end
    if (Minutes <10) then CraftTime = "0" .. Minutes .. ":" else CraftTime = Minutes .. ":" end
    if (Seconds < 10) then CraftTime = CraftTime .. "0" end
    CraftTime = "00:" .. CraftTime .. Seconds;
    return CraftTime
end

function p.RecipeRequiredSkill(SkillData)
	local SkillCell = ""
	local SkillID = SkillData[1]
	local SkillLevel = SkillData[2]
	if ((SkillID == "") or (SkillID == "nil")) then SkillName = 'None' else SkillName = Utils.SkillSearchByID(SkillID) end
	if (SkillName == "None") then SkillCell = IconUtils.main{ name = Utils.Translate("None"), id = 'NoSkillLabor', size = 48, style = 2 } else SkillCell = IconUtils.main{ name = SkillsData.skills[SkillName].Name[Lang], id = SkillsData.skills[SkillName].SkillID, size = 48, style = 2, link = SkillsData.skills[SkillName].Name[Lang]} .. " " .. SkillLevel end
	
	return SkillCell
end

function p.CraftingTableRecipes(ItemName)
    local Recipes = ""
    for RecipeName,RecipeData in pairs(RecipesData.recipes) do
            if RecipeData.CraftingTables == ItemName then
                if (Recipes == "") then Recipes = Recipes .. RecipeName else Recipes = Recipes .. "," .. RecipeName end
            end
    end
           
    return Recipes
end

return p