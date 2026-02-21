local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')
local Lang = Utils.getLanguageName()

local p = {}

function p.main()
	local WikiText = ""
	local MarketplaceData = require( "Module:MarketplaceData" )
	local BlueprintsList = MarketplaceData.blueprints
	local ItemData = require( "Module:ItemData" )
	local ItemList = ItemData.items
	local AchievementsData = require( "Module:AchievementsData" )
	local AchievementsList = AchievementsData.achievements
	WikiText = WikiText .. '<div class="row">'
	
	for Bname,Bdata in pairs(BlueprintsList) do
		local ItemName = ItemList[Bname].Name[Lang]
		local ItemPrice = Bdata.Price
		local ItemQuantity = Bdata.Quantity
		local ItemAchievement = Bdata.Achievement
		local ItemText = '<br>' .. IconUtils.main{ name = 'EcoCredit', id = 'EcoCredit', size = 24, style = 1} .. ' ' .. ItemPrice .. ' [https://play.eco/buy ' .. Utils.Translate("Eco Credits") .. ']<br>Quantity ' .. ItemQuantity
		WikiText = WikiText ..'<div class="IconFrame">'
		WikiText = WikiText .. IconUtils.main{ name = ItemName, id = ItemList[Bname].ID, size = 128, style = 3, link = ItemName }
		WikiText = WikiText .. ItemText
		if (ItemAchievement ~= "") then WikiText = WikiText .. "<br>Requires [[Achievements|" .. ItemAchievement .."]]" end
		WikiText = WikiText ..'</div>'
	end
	
	WikiText = WikiText .. '</div>'
	return WikiText
end

return p