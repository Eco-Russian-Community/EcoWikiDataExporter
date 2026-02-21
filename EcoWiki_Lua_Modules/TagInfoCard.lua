local p = {}

local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')
local RecipeUtils = require('Module:RecipeUtils')

local Lang = Utils.getLanguageName()

function p.main(frame)
	local PageName = frame.args[1]
	local TagLoc = string.gsub(Utils.Translate("{0} Tag"),"{0}","")
	PageName = string.gsub(PageName,TagLoc,"")
	if (Lang == 'English') then TagName = PageName else TagName = Utils.TagSearch(PageName) end
	local TagData = require( "Module:TagData" )
	local ItemData = require( "Module:ItemData" )
    local Tag = TagData.tags[TagName]
    local TagID = Tag.ID
    local ItemList = Tag.Items
	local WikiText =''
	
	WikiText =  WikiText ..'__NOTOC__'
	WikiText =  WikiText ..'page name get test: ' .. PageName ..'</br>'
	WikiText =  WikiText ..'Tag name get test: ' .. TagName ..'</br>'
	
	WikiText =  WikiText .. "[[Category:" .. Utils.Translate("Tags") .. "]]"
	WikiText =  WikiText .. IconUtils.main{ name = Tag.Name[Lang], id = TagID, size = 128, style = 4, }
	
	WikiText =  WikiText ..  '<h3>' .. Utils.Translate("Items in Tag") .. ':</h3>'
	WikiText =  WikiText .. '<div class="row">'
	
	for Count, ItemName in pairs(ItemList) do
		Item = ItemData.items[ItemName]
		WikiText =  WikiText .. IconUtils.main{ name = Item.Name[Lang], id = Item.ID, size = 128, style = 4, link = Item.Name[Lang] }
	end
	
	WikiText =  WikiText .. '</div>'
	
	local RecipeTagIngredients = RecipeUtils.TagIngredient(TagName)

	if (RecipeTagIngredients ~= "") then
		WikiText =  WikiText .. '<h3>' .. Utils.Translate("Used in") .. ':</h3>';
		WikiText =  WikiText .. RecipeUtils.CraftTable(RecipeTagIngredients)
	end
	
	return WikiText
end

return p