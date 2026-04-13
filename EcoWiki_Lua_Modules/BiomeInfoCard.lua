local p = {}
local Utils = require('Module:Utils')
local Lang = Utils.WikiLang


function p.main(frame)
	local PageName = frame.args[1]
	local WikiText =''
	BiomeName = Utils.BiomeSearch(PageName)
	
	WikiText =  WikiText ..'page name get test: ' .. PageName ..'</br>'
	WikiText =  WikiText ..'Biome name get test: ' .. BiomeName ..'</br>'

	if (BiomeName ~= "None") then
		local BiomeData = mw.loadData( "Module:BiomeData" )
    	local Biome = BiomeData.biomes[BiomeName]
    	
    WikiText = WikiText .. '__NOTOC__'
	WikiText = WikiText .. '<div class="row gy-4 gx-5"><div class="col-md-6">'
	
	WikiText = WikiText .. '</div>'
	WikiText = WikiText .. '<div class="col-md-6"><div class="card border-primary"><div class="card-body">'
	WikiText = WikiText .. '<h2 class="card-title fs-40">' .. Biome.Name[Lang] .. '</h2>'
	WikiText = WikiText .. '<p class="col-lg-10 card-text">' .. Biome.Description[Lang] .. '</p>'
	
	-- Biome properties table
	WikiText = WikiText .. '<table class="table table-striped">'
	
	if Biome.Color then
		WikiText = WikiText .. '<tr><th>' .. Utils.Translate("Color") .. '</th><td><span style="display:inline-block;width:20px;height:20px;background-color:' .. Biome.Color .. ';border:1px solid #000;"></span> ' .. Biome.Color .. '</td></tr>'
	end
	
	if Biome.WorldLayer then
		WikiText = WikiText .. '<tr><th>' .. Utils.Translate("World Layer") .. '</th><td>' .. Biome.WorldLayer .. '</td></tr>'
	end
	
	if Biome.TemperatureRangeMin and Biome.TemperatureRangeMax then
		WikiText = WikiText .. '<tr><th>' .. Utils.Translate("Temperature") .. '</th><td>' .. Biome.TemperatureRangeMin .. ' - ' .. Biome.TemperatureRangeMax .. ' °C</td></tr>'
	end
	
	if Biome.MoistureRangeMin and Biome.MoistureRangeMax then
		WikiText = WikiText .. '<tr><th>' .. Utils.Translate("Moisture") .. '</th><td>' .. Biome.MoistureRangeMin .. ' - ' .. Biome.MoistureRangeMax .. ' %</td></tr>'
	end
	
	WikiText = WikiText .. '</table>'
	
	WikiText = WikiText .. '</div></div></div></div>'
	
	if (Lang ~= 'English') then WikiText = WikiText .. '[[en:' .. Biome.Name.English .. ']]' end
	if (Lang ~= 'Russian') then WikiText = WikiText .. '[[ru:' .. Biome.Name.Russian .. ']]' end
	if (Lang ~= 'German') then WikiText = WikiText .. '[[de:' .. Biome.Name.German .. ']]' end
	if (Lang ~= 'French') then WikiText = WikiText .. '[[fr:' .. Biome.Name.French .. ']]' end
	end
	
    return WikiText
end

return p
