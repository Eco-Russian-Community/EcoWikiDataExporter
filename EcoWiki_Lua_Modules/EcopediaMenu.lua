local p = {}

local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')
local Lang = Utils.WikiLang

function p.MainMenu()
	local MainMenu = ""
	local MenuData = require('Module:EcopediaMenuData')
	local MenuList = MenuData.ecopediapages
	
	local Chapter = {}
	for Name,Data in pairs(MenuList) do
		if (Data["Type"] == "Chapter" ) then 
			MainMenu = MainMenu .. '<div class="col-md-3"><div class="card">'
			MainMenu = MainMenu .. '<div class="card-header">' .. Data.Name[Lang] .. '</div>'
			MainMenu = MainMenu .. '<ul class="list-group list-group-flush">'
			for CatName,CatData in pairs(MenuList) do
				if ((CatData["Type"] == "Category") and (CatData["Chapter"] == Name))  then
					MainMenu = MainMenu .. ' <li class="list-group-item">' .. IconUtils.main{name = CatData.Name[Lang], id = CatData.Icon , size = 32, style = 1} .. " [[" .. CatData.Name[Lang] .. ']]</li>'
				end
			end
			MainMenu = MainMenu .. '</ul></div></div>'
		end
	end

	MainMenu = '<div class="row">' .. MainMenu .. '</div>'
	return MainMenu
end

return p
