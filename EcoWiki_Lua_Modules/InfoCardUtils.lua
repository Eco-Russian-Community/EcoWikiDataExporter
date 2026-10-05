local p = {}

local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')
local Lang = Utils.WikiLang

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
    local WikiText = ''
    local RecipeUtils = require('Module:RecipeUtils')
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

function p.FoodListDetails()
	local WikiText = ''
	local TableHeader = ''
    local FoodData = mw.loadData( "Module:FoodData" )
    local FoodList = FoodData.foods
    
    -- Create the header of the table
	TableHeader = '<table class="table table-striped table-bordered sortable"><tr class="table-dark">' 
	TableHeader = TableHeader .. '<th>Food</th>'
	TableHeader = TableHeader .. '<th>Carbs</th>'
	TableHeader = TableHeader .. '<th>Protein</th>'
	TableHeader = TableHeader .. '<th>Fat</th>'
	TableHeader = TableHeader .. '<th>Vitamins</th>'
	TableHeader = TableHeader .. '<th>Nutrition</th>'
	TableHeader = TableHeader .. '<th>Shelf Life</th>'
	TableHeader = TableHeader .. '<th>Calories</th>'
	TableHeader = TableHeader .. '<th>Density</th>'
	TableHeader = TableHeader .. '<th>SP value</th>'
	
	WikiText = TableHeader
    
    for FoodName,FoodData in pairs(FoodList) do
		local TableRow = ''
		local Nutrition = FoodData.Carbs + FoodData.Protein + FoodData.Fat + FoodData.Vitamins
		local Calories = FoodData.Calories
		local Density = (Nutrition / Calories) * 100
		local ShelfLife = Utils.FoodsShelfLife(FoodData.ShelfLife)
		local SP = Nutrition * Calories
		
    	TableRow = TableRow .. '<tr>'
    	TableRow = TableRow .. '<td>[[' .. FoodName .. ']] </td>'
    	TableRow = TableRow .. '<td>' .. FoodData.Carbs .. ' </td>'
    	TableRow = TableRow .. '<td>' .. FoodData.Protein .. ' </td>'
    	TableRow = TableRow .. '<td>' .. FoodData.Fat .. ' </td>'
    	TableRow = TableRow .. '<td>' .. FoodData.Vitamins .. ' </td>'
    	TableRow = TableRow .. '<td>' .. Nutrition .. ' </td>'
    	TableRow = TableRow .. '<td>' .. ShelfLife .. ' </td>'
    	TableRow = TableRow .. '<td>' .. Calories .. ' </td>'
    	TableRow = TableRow .. '<td>' .. Density .. ' </td>'
    	TableRow = TableRow .. '<td>' .. SP .. ' </td>'
    	TableRow = TableRow .. '</tr>'
    	
    	WikiText = WikiText .. TableRow
    end
    
    WikiText = WikiText .. ' </table>'
    
    return WikiText
end

function p.PlantList()
	local WikiText = ''
	local TableHeader = ''
    local PlantData = mw.loadData( "Module:PlantData" )
    local PlantList = PlantData.plants
    
        -- Create the header of the table
	TableHeader = '<table class="table table-striped table-bordered sortable"><tr class="table-dark">'
	TableHeader = TableHeader .. '<tr class="table-dark">'
	TableHeader = TableHeader .. '<th rowspan="2">Plant</th>'
	TableHeader = TableHeader .. '<th rowspan="2">Habitat</th>'
	TableHeader = TableHeader .. '<th rowspan="2">Maturity Age<br>(Hours)</th>'
	TableHeader = TableHeader .. '<th colspan="4">Temperature (<sup>o</sup>C)</th>'
	TableHeader = TableHeader .. '<th colspan="4">Moisture (%)</th>'
	TableHeader = TableHeader .. '<th colspan="4">Water (%)</th>'
	TableHeader = TableHeader .. '<th colspan="2">Pollution (%)</th>'
	TableHeader = TableHeader .. '</tr>'
	
	TableHeader = TableHeader .. '<tr class="table-dark">'
	TableHeader = TableHeader .. '<th>Extreme<br>Min</th>'
	TableHeader = TableHeader .. '<th>Ideal<br>Min</th>'
	TableHeader = TableHeader .. '<th>Ideal<br>Max</th>'
	TableHeader = TableHeader .. '<th>Extreme<br>Max</th>'
	TableHeader = TableHeader .. '<th>Extreme<br>Min</th>'
	TableHeader = TableHeader .. '<th>Ideal<br>Min</th>'
	TableHeader = TableHeader .. '<th>Ideal<br>Max</th>'
	TableHeader = TableHeader .. '<th>Extreme<br>Max</th>'
	TableHeader = TableHeader .. '<th>Extreme<br>Min</th>'
	TableHeader = TableHeader .. '<th>Ideal<br>Min</th>'
	TableHeader = TableHeader .. '<th>Ideal<br>Max</th>'
	TableHeader = TableHeader .. '<th>Extreme<br>Max</th>'
	TableHeader = TableHeader .. '<th>Min</th>'
	TableHeader = TableHeader .. '<th>Max</th>'
	TableHeader = TableHeader .. '</tr>'
	
	WikiText = TableHeader
    
    for PlantName,PlantData in pairs(PlantList) do
		local TableRow = ''
		local Habitat = ''
		if (PlantData.IsWater == 'True') then Habitat = 'Water' else Habitat = 'Land' end
		
    	TableRow = TableRow .. '<tr>'
    	TableRow = TableRow .. '<td>[[' .. PlantData.Name[Lang] .. ']] </td>'
     	TableRow = TableRow .. '<td>' .. Habitat .. ' </td>'
     	TableRow = TableRow .. '<td>' .. PlantData.MaturityAgeDays * 24 .. ' </td>'
    	TableRow = TableRow .. '<td>' .. PlantData.ExtremeTemperatureRangeMin .. ' </td>'
    	TableRow = TableRow .. '<td>' .. PlantData.IdealTemperatureRangeMin .. ' </td>'
    	TableRow = TableRow .. '<td>' .. PlantData.IdealTemperatureRangeMax .. ' </td>'
    	TableRow = TableRow .. '<td>' .. PlantData.ExtremeTemperatureRangeMax .. ' </td>'
 
     	TableRow = TableRow .. '<td>' .. PlantData.ExtremeMoistureRangeMin .. ' </td>'
    	TableRow = TableRow .. '<td>' .. PlantData.IdealMoistureRangeMin .. ' </td>'
    	TableRow = TableRow .. '<td>' .. PlantData.IdealMoistureRangeMax .. ' </td>'
    	TableRow = TableRow .. '<td>' .. PlantData.ExtremeMoistureRangeMax .. ' </td>'
    	
    	TableRow = TableRow .. '<td>' .. PlantData.ExtremeWaterRangeMin .. ' </td>'
    	TableRow = TableRow .. '<td>' .. PlantData.IdealWaterRangeMin .. ' </td>'
    	TableRow = TableRow .. '<td>' .. PlantData.IdealWaterRangeMax .. ' </td>'
    	TableRow = TableRow .. '<td>' .. PlantData.ExtremeWaterRangeMax .. ' </td>'

    	TableRow = TableRow .. '<td>' .. PlantData.PollutionDensityMin .. ' </td>'
    	TableRow = TableRow .. '<td>' .. PlantData.PollutionDensityMax .. ' </td>'
    	TableRow = TableRow .. '</tr>'
    	
    	WikiText = WikiText .. TableRow
    end
    
    
    
    return WikiText
end

function p.TreeList()
	local WikiText = ''
	local TableHeader = ''
    local TreeData = mw.loadData( "Module:TreeData" )
    local TreeList = TreeData.trees

    -- Create the header of the table
	TableHeader = '<table class="table table-striped table-bordered sortable"><tr class="table-dark">'
    TableHeader = TableHeader .. '<tr class="table-dark">'
	TableHeader = TableHeader .. '<th>' .. Utils.Translate("Tree") ..'</th>'
	TableHeader = TableHeader .. '<th>Growth Time</th>'
	TableHeader = TableHeader .. '<th>Max Yield</th>'
	TableHeader = TableHeader .. '<th>Yield Per Day</th>'
	TableHeader = TableHeader .. '<th>CO2</th>'
	TableHeader = TableHeader .. '</tr>'
    
    WikiText = TableHeader
    
    for TreeName,TreeData in pairs(TreeList) do
		local TableRow = ''
    
    	TableRow = TableRow .. '<tr>'
    	TableRow = TableRow .. '<td>[[' .. TreeData.Name[Lang] .. ']] </td>'
    	if tonumber(TreeData.MaturityAgeDays) > 10 then TableRow = TableRow .. '<td>' .. Utils.Translate("None") ..' </td>' else TableRow = TableRow .. '<td>' .. TreeData.MaturityAgeDays .. ' </td>' end
    	TableRow = TableRow .. '<td>' .. TreeData.ResourceMax .. ' </td>'
    	if tonumber(TreeData.MaturityAgeDays) > 10 then TableRow = TableRow .. '<td>' .. Utils.Translate("None") ..' </td>' else TableRow = TableRow .. '<td>' .. TreeData.ResourceMax / TreeData.MaturityAgeDays .. ' </td>' end
    	TableRow = TableRow .. '<td>' .. TreeData.ReleasesCO2TonsPerDay .. ' </td>'
    	TableRow = TableRow .. '</tr>'
    
    	WikiText = WikiText .. TableRow
    end
    
    return WikiText
end

return p