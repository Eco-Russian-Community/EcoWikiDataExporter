local p = {}

local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')
local Lang = Utils.getLanguageName()

-- Main entry point for the Module
function p.main()
   	
    -- load lists
    local biomeData = require( "Module:BiomeData" )
    local biomesList = biomeData.biomes
    
	local text = '<div class="col-lg-12"><Center><h2 class="title">' .. Utils.Translate("Biomes") ..'</h2></Center><p>Biomes are the unique natural environments found within Eco. Each one has different characteristics that affect the growth of various crops in-game.</p></div>';
	
	for Bname,Bdata in pairs(biomesList) do
		
		local BiomeBannerName = Bdata.ID .. "Banner.png"
		if (Utils.checkImage(BiomeBannerName) == "N") then BiomeBannerName = "Banner4k.jpg" end
			
		text = text .. '<div class="col-lg-3"><div class="card">';
		text = text .. '[[File:' .. BiomeBannerName ..'|class=card-img-top|link=]]';
		text = text .. '<div class="card-body"><p class="card-title">';
		text = text .. '[[' .. Bdata.Name[Lang] .. ']]';
		text = text .. '</p><p class="card-subtitle mb-2 text-muted" style="min-height: 95px;">' .. Bdata.Description[Lang] .. '</p>';
		text = text .. '</div></div></div>';
	end

	return text
end

return p