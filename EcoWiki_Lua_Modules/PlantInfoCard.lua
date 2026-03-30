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
	
	WikiText =  WikiText .. '<div class="row gy-4 gx-5"><div class="col-md-6">'
	local GalleryImagesList = PlantName .. "_Plant.jpg|" .. PlantName .."\n" .. PlantName .. "_Plant.jpg"
	WikiText =  WikiText .. frame:callParserFunction{ name = '#tag:gallery', args = { mode = 'slideshow', widths = '100%', ''.. GalleryImagesList .. '' , showthumbnails = 'true'} }
	WikiText =  WikiText .. '</div>'
	WikiText =  WikiText ..'<div class="col-md-6"><div class="card border-primary"><div class="card-body">'
    WikiText =  WikiText ..'<h2 class="card-title fs-40">' .. IconUtils.main{name = Plant.Name[Lang], id = Plant.ID , size = 48, style = 1} .. '  ' .. Plant.Name[Lang] .. '</h2>'
	--WikiText =  WikiText ..'<p class="col-lg-10 card-text">' .. Plant.Description[Lang] .. '</p>'
	WikiText =  WikiText .. '<table class="table table-striped table-bordered w-80"><tr class="thead-dark">';
	WikiText =  WikiText .. '<th>' .. Utils.Translate("Name") .. '</th><th>' .. Utils.Translate("Value") .. '</th>'
	
	WikiText =  WikiText .. '</table>'
	WikiText =  WikiText ..'</div></div></div></div>'

	if (Lang ~= 'English') then WikiText =  WikiText .. '[[en:' .. Plant.Name.English .. ']]' end
	if (Lang ~= 'Russian') then WikiText =  WikiText .. '[[ru:' .. Plant.Name.Russian .. ']]' end
	if (Lang ~= 'German') then WikiText =  WikiText .. '[[de:' .. Plant.Name.German .. ']]' end
	if (Lang ~= 'French') then WikiText =  WikiText .. '[[fr:' .. Plant.Name.French .. ']]' end
	
	return WikiText
end

return p