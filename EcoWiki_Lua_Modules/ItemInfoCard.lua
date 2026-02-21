local p = {}

local Utils = require('Module:Utils')
local RecipeUtils = require('Module:RecipeUtils')
local IconUtils = require('Module:IconUtils')

local Lang = Utils.getLanguageName()

function p.main(frame)
	local PageName = frame.args[1]
	if (Lang == 'English') then ItemName = PageName else ItemName = Utils.ItemSearch(PageName) end
	local ItemData = require( "Module:ItemData" )
    local Item = ItemData.items[ItemName]
	local WikiText =''
	
	WikiText =  WikiText ..'__NOTOC__'
	WikiText =  WikiText ..'Page name get test: ' .. PageName ..'</br>'
	WikiText =  WikiText ..'Item name get test: ' .. ItemName ..'</br>'
	
	WikiText =  WikiText .. '<div class="row gy-4 gx-5"><div class="col-md-6">'
	
	WikiText =  WikiText .. '</div>'
	WikiText =  WikiText ..'<div class="col-md-6"><div class="card border-primary"><div class="card-body">'
    WikiText =  WikiText ..'<h2 class="card-title fs-40">' .. IconUtils.main{name = Item.Name[Lang], id = Item.ID , size = 48, style = 1} .. '  ' .. Item.Name[Lang] .. '</h2>'
	WikiText =  WikiText ..'<p class="col-lg-10 card-text">' .. Item.Description[Lang] .. '</p>'
	
	WikiText =  WikiText ..'</div></div></div></div>'
	
	local RecipeItemCraft = RecipeUtils.ItemCraft(ItemName)
	if (RecipeItemCraft ~= "") then
		WikiText =  WikiText .. '<h3>' .. Utils.Translate("Crafted At") .. ':</h3>';
		WikiText =  WikiText .. RecipeUtils.CraftTable(RecipeItemCraft);
	end
	
	local RecipeItemIngredient = RecipeUtils.ItemIngredient(ItemName)
	if (RecipeItemIngredient ~= "") then
		WikiText =  WikiText .. '<h3>' .. Utils.Translate("Used in") .. ':</h3>';
		WikiText =  WikiText .. RecipeUtils.CraftTable(RecipeItemIngredient)
	end
	
	local ItemTagList = Utils.ItemTags(Item.Tags);
	if (ItemTagList ~= "") then
		WikiText =  WikiText .. '<h3>' .. Utils.Translate("Tags Applying to") .. ':</h3>';
		WikiText =  WikiText .. ItemTagList
	end
	
	local RecipeTagsIngredient = RecipeUtils.TagsIngredient(Item.Tags)
	if (RecipeTagsIngredient ~= "") then
		WikiText =  WikiText .. '<h3>Used as Tag item in:</h3>';
		WikiText =  WikiText .. RecipeUtils.CraftTable(RecipeTagsIngredient)
	end
	
	if (Item.WorldObjectItem == "True") then
		WikiText =  WikiText .. 'WorldObject test  => True';
	end	
	
	
	WikiText =  WikiText .. '<h3>How use Icon:</h3>'
	WikiText =  WikiText .. '<p>' .. Item.Name[Lang] .. ' icon can be used on any sign that has a text component, including on [[Vehicles]]:</br>'
	WikiText =  WikiText .. 'Icon with background: <icon name="' .. Item.ID .. '"></br>'
	WikiText =  WikiText .. 'Icon without background: <icon name="' .. Item.ID .. '" type="nobg"></p>'

	if (Lang ~= 'English') then WikiText =  WikiText .. '[[en:' .. Item.Name.English .. ']]' end
	if (Lang ~= 'Russian') then WikiText =  WikiText .. '[[ru:' .. Item.Name.Russian .. ']]' end
	if (Lang ~= 'German') then WikiText =  WikiText .. '[[de:' .. Item.Name.German .. ']]' end
	if (Lang ~= 'French') then WikiText =  WikiText .. '[[fr:' .. Item.Name.French .. ']]' end
	
	return WikiText
end

return p