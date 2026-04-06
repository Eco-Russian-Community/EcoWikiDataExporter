local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')

local Lang = Utils.getLanguageName()
local p = {}


function p.WorldObjectModule(ItemName)
    local WorldObjectData = mw.loadData( "Module:WorldObjectData" )
    local WorldObject = WorldObjectData.WorldObjects[ItemName]
    local WikiText = ''

    if WorldObject.CraftingComponent == 'True' then WikiText = WikiText .. p.CraftingComponentModule(ItemName) end
    if WorldObject.ForSaleComponent == 'True' then WikiText = WikiText .. p.ForSaleComponentModule(ItemName) end
    if WorldObject.HousingComponent == 'True' then WikiText = WikiText .. p.HousingComponentModule(ItemName) end
    if WorldObject.BedComponent == 'True' then WikiText = WikiText .. p.BedComponentModule(ItemName) end
    if WorldObject.MintComponent == 'True' then WikiText = WikiText .. p.MintComponentModule(ItemName) end
    
    return WikiText
end

function p.CraftingComponentModule(ItemName)
    local RecipeUtils = require('Module:RecipeUtils')
    local WikiText = ''
    local CraftingTableRecipes = RecipeUtils.CraftingTableRecipes(ItemName)
	if (CraftingTableRecipes ~= "") then
		WikiText = WikiText .. '<h3>Crafting</h3>'
		WikiText =  WikiText .. RecipeUtils.CraftTable(CraftingTableRecipes);
	end
	
    return WikiText
end

function p.ForSaleComponentModule(ItemName)
    local WikiText = ''
    WikiText = WikiText .. '<h3>For Sale</h3>'
    return WikiText
end

function p.HousingComponentModule(ItemName)
    local WikiText = ''
    return WikiText
end

function p.BedComponentModule(ItemName)
    local WikiText = ''
    WikiText = WikiText .. '<h3>Bed</h3>'
    return WikiText
end

function p.MintComponentModule(ItemName)
    local WikiText = ''
    WikiText = WikiText .. '<h3>Mint</h3>'
    return WikiText
end

function p.FoodDetailsModule(ItemName)
    local WikiText = ''
    local FoodData = mw.loadData( "Module:FoodData" )
    local FoodItem = FoodData.foods[ItemName]
    WikiText = WikiText .. '<h3>Food</h3>'
    WikiText = WikiText .. '<p>' .. Utils.Translate("Calories") .. ': ' .. FoodItem.Calories .. '</p>'
    WikiText = WikiText .. '<p>' .. Utils.Translate("Protein") .. ': ' .. FoodItem.Protein .. '</p>'
    WikiText = WikiText .. '<p>' .. Utils.Translate("Carbs") .. ': ' .. FoodItem.Carbs .. '</p>'
    WikiText = WikiText .. '<p>' .. Utils.Translate("Fat") .. ': ' .. FoodItem.Fat .. '</p>'
    WikiText = WikiText .. '<p>' .. Utils.Translate("Vitamins") .. ': ' .. FoodItem.Vitamins .. '</p>'
    WikiText = WikiText .. '<p>' .. Utils.Translate("Shelf Life") .. ': ' .. Utils.FoodsShelfLife(FoodItem.ShelfLife) .. '</p>'

    return WikiText
end

return p