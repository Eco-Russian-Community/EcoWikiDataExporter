local p = {}

local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')
local Lang = Utils.WikiLang

function p.main()
	local WikiText = ""
	local MarketplaceData = mw.loadData( "Module:MarketplaceData" )
	local BlueprintsList = assert(MarketplaceData.blueprints, "Error: MarketplaceData") 
	local ItemData = mw.loadData( "Module:ItemData" )
	local ItemList = assert(ItemData.items, "Error: ItemData")
	local AchievementsData = mw.loadData( "Module:AchievementsData" )
	local AchievementsList = assert(AchievementsData.achievements, "Error: AchievementsData")
	WikiText = WikiText .. '<div class="row">'
	
	for Bname,Bdata in pairs(BlueprintsList) do
		local ItemPrice = Bdata.Price
		if (ItemPrice ~= "0") then
		local ItemName = ItemList[Bname].Name[Lang]
		local ItemQuantity = Bdata.Quantity
		local ItemAchievement = Bdata.Achievement
		local ItemText = '<br>' .. IconUtils.main{ name = 'EcoCredit', id = 'EcoCredit', size = 24, style = 1} .. ' ' .. ItemPrice .. ' [https://play.eco/buy ' .. Utils.Translate("Eco Credits") .. ']<br>' ..  Utils.Translate("Quantity") .. ' ' .. ItemQuantity
		WikiText = WikiText ..'<div class="IconFrame">'
		---WikiText = WikiText .. ItemName .. '<br>'
		---WikiText = WikiText .. Utils.ItemSearch(Bname)
		WikiText = WikiText .. IconUtils.main{ name = ItemName, id = ItemList[Bname].ID, size = 128, style = 3, link = ItemName }
		WikiText = WikiText .. ItemText
		if (ItemAchievement ~= "") then WikiText = WikiText .. "<br>" .. Utils.Translate("Requires") .. " [[Achievements|" .. AchievementsList[ItemAchievement].Name[Lang] .."]]" end
		WikiText = WikiText ..'</div>'
		end
	end
	
	WikiText = WikiText .. '</div>'
	
	local TranslateData = mw.loadData( "Module:LocalizationData" )
	local PageName = TranslateData.locales.Marketplace
	if (Lang ~= 'English') then WikiText =  WikiText .. '[[en:' .. PageName.English .. ']]' end
	if (Lang ~= 'Russian') then WikiText =  WikiText .. '[[ru:' .. PageName.Russian .. ']]' end
	if (Lang ~= 'German') then WikiText =  WikiText .. '[[de:' .. PageName.German .. ']]' end
	if (Lang ~= 'French') then WikiText =  WikiText .. '[[fr:' .. PageName.French .. ']]' end
	if (Lang ~= 'Japanese') then WikiText =  WikiText .. '[[ja:' .. PageName.Japanese .. ']]' end
	
	return WikiText
end

return p