ocal p = {}
local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')

local Lang = Utils.getLanguageName()

function p.main(frame)
	local PageName = frame.args[1]
	if (Lang == 'English') then AnimalName = PageName else AnimalName = Utils.AnimalSearch(PageName) end
	local AnimalData = require( "Module:AnimalData" )
    local Animal = AnimalData.animals[AnimalName]
	local WikiText =''
	
	WikiText =  WikiText ..'__NOTOC__'
	WikiText =  WikiText ..'page name get test: ' .. PageName ..'</br>'
	WikiText =  WikiText ..'Animal name get test: ' .. AnimalName ..'</br>'
	
	WikiText =  WikiText .. '<div class="row gy-4 gx-5"><div class="col-md-6">'
	local GalleryImagesList = AnimalName .. "_Animal.jpg|" .. AnimalName .."\n" .. AnimalName .. "_Animal.jpg"
	WikiText =  WikiText .. frame:callParserFunction{ name = '#tag:gallery', args = { mode = 'slideshow', widths = '100%', ''.. GalleryImagesList .. '' , showthumbnails = 'true'} }
	WikiText =  WikiText .. '</div>'
	WikiText =  WikiText ..'<div class="col-md-6"><div class="card border-primary"><div class="card-body">'
    WikiText =  WikiText ..'<h2 class="card-title fs-40">' .. IconUtils.main{name = Animal.Name[Lang], id = Animal.ID , size = 48, style = 1} .. '  ' .. Animal.Name[Lang] .. '</h2>'
	WikiText =  WikiText ..'<p class="col-lg-10 card-text">' .. Animal.Description[Lang] .. '</p>'
	WikiText =  WikiText .. '<table class="table table-striped table-bordered w-80"><tr class="thead-dark">';
	WikiText =  WikiText .. '<th>' .. Utils.Translate("Name") .. '</th><th>' .. Utils.Translate("Value") .. '</th>'
	
	WikiText =  WikiText .. '</table>'
	WikiText =  WikiText ..'</div></div></div></div>'
	
	if (Lang ~= 'English') then WikiText =  WikiText .. '[[en:' .. Animal.Name.English .. ']]' end
	if (Lang ~= 'Russian') then WikiText =  WikiText .. '[[ru:' .. Animal.Name.Russian .. ']]' end
	if (Lang ~= 'German') then WikiText =  WikiText .. '[[de:' .. Animal.Name.German .. ']]' end
	if (Lang ~= 'French') then WikiText =  WikiText .. '[[fr:' .. Animal.Name.French .. ']]' end
	
	return WikiText
end

return p