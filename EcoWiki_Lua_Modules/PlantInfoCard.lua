local p = {}

local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')
local InfoCardUtils = require('Module:InfoCardUtils')

local Lang = Utils.getLanguageName()

function p.main(frame)
	local PageName = frame.args[1]
	if (Lang == 'English') then PlantName = PageName else PlantName = Utils.PlantSearch(PageName) end
	local PlantData = mw.loadData( "Module:PlantData" )
    local Plant = PlantData.plants[PlantName]
	local WikiText =''
	
	WikiText =  WikiText ..'__NOTOC__'
	WikiText =  WikiText ..'Page name get test: ' .. PageName ..'</br>'
	WikiText =  WikiText ..'Plant name get test: ' .. PlantName ..'</br>'
	
	return WikiText
end

return p