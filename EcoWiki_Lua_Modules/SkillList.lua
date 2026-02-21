local p = {}

local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')

-- Main entry point for the Module
function p.main()
   	
    -- load lists
    local skillData = require( "Module:SkillData" )
    local skillList = skillData.skills
	local text = '<div class="col-lg-12"><Center><h2 class="title">' .. Utils.Translate("Skills") .. '</h2></Center></div>';
	local Lang = Utils.getLanguageName()
	
	for Pname,Pdata in pairs(skillList) do

		if Pdata.IsRoot == 'True' then 
		if (Pdata.Name[Lang] == "") then ProfessionName = Pdata.Name.English else ProfessionName = Pdata.Name[Lang] end
		
		text = text .. '<div class="col-lg-3"><div class="card">';
		text = text .. '[[File:Banner4k.jpg|class=card-img-top|link=]]';
		text = text .. '<div class="card-body"><p class="card-title">';
		text = text .. IconUtils.main{id = Pdata.SkillID , size = 48, style = 1, link = ProfessionName} .. '  '
		text = text .. '[[' .. ProfessionName .. '|' .. ProfessionName ..' Profession]]</p><p class="card-subtitle mb-2 text-muted">' .. Pdata.Description[Lang] .. '</p><p class="card-text">';
		text = text .. '<div class="card"> <div class="card-body">';

		for Sname,Sdata in pairs(skillList) do
			if Sdata.IsRoot == 'False' and Sdata.RootSkill == Pname then 
				if (Sdata.Name[Lang] == "") then SpecialtyName = Sdata.Name.English else SpecialtyName = Sdata.Name[Lang] end
				text = text .. IconUtils.main{id = Sdata.SkillID , size = 32, style = 1, link = SpecialtyName} .. '  ';
			end
		end
		text = text .. '</div></div></p></div></div></div>'; end
	end

	return text
end

return p