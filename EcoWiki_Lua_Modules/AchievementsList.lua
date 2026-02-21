local p = {}
local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')

function p.main()
	local Lang = Utils.getLanguageName()
	local wiki = ''
	
	-- import the required modules
	local AchievementsData = require( "Module:AchievementsData" )
	local ADescription = require('Module:AdvancedDescription')
	local achievements = AchievementsData.achievements
	
	wiki = '<div class="container px-3 py-3" id="icon-grid"><div class="row row-cols-1 row-cols-sm-1 row-cols-md-2 row-cols-lg-3 g-3 py-3">'

	for k,v in pairs(achievements) do
		local row = ''
		row = row .. '<div class="col d-flex align-items-start">'
		if (Utils.checkImage(v.IconName .. '_Icon.png') == "Y") then IconName = v.IconName else IconName = 'NoItem' end
		row = row .. '[[file:' .. IconName.. '_Icon.png|64px|link=|class=IconGrid]]'
		if (v.Name[Lang] == "") then achievementName = v.Name.English else achievementName = v.Name[Lang] end
		if (v.Description[Lang] == "") then achievementDescription = v.Description.English else achievementDescription = ADescription.main(v.Description[Lang]) end
		row = row .. '<div><h5 class="fw-bold mb-0 fs-4 text-body-emphasis">' .. achievementName .. '</h5><p>' .. achievementDescription .. '</p></div>'
		row = row .. '</div>'
		wiki = wiki .. row
	end

	wiki = wiki .. '</div></div>'
	return wiki
	end

return p