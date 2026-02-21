local p = {}

local Utils = require('Module:Utils')
local Lang = Utils.getLanguageName()

function p.SubIndexPagesList()
		local WikiText =''
		local Index = Utils.Translate("Index")
		local Items = Utils.Translate("Items")
		local WikiText = WikiText .. '[['.. Index .. '/' .. Items .. '|' .. Items .. ']]<br>'
		
		local Tags = Utils.Translate("Tags")
		local WikiText = WikiText .. '[['.. Index .. '/' .. Tags .. '|' .. Tags .. ']]<br>'
		
		local Skills = Utils.Translate("Skills")
		local WikiText = WikiText .. '[['.. Index .. '/' .. Skills .. '|' .. Skills .. ']]<br>'
		
		local Biomes = Utils.Translate("Biomes")
		local WikiText = WikiText .. '[['.. Index .. '/' .. Biomes .. '|' .. Biomes .. ']]<br>'

		local Animals = Utils.Translate("Animals")
		local WikiText = WikiText .. '[['.. Index .. '/' .. Animals .. '|' .. Animals .. ']]<br>'
		
		local Plants = Utils.Translate("Plants")
		local WikiText = WikiText .. '[['.. Index .. '/' .. Plants .. '|' .. Plants .. ']]<br>'
		
		local Trees = Utils.Translate("Trees")
		local WikiText = WikiText .. '[['.. Index .. '/' .. Trees .. '|' .. Trees .. ']]<br>'
		
		local Achievements = Utils.Translate("Achievements")
		local WikiText = WikiText .. '[['.. Index .. '/' .. Achievements .. '|' .. Achievements .. ']]<br>'
		
		return WikiText
end

function p.SubIndexItemsList()
		local WikiText =''
		
		local ItemData = mw.loadData("Module:ItemData")
		local ItemList = ItemData.items
		for Iname,Idata in pairs(ItemList) do
			if (Idata.Hidden ~= 'True') then
			local Color = "success"
			local Item = Idata.Name[Lang]
			local ItemEN = Idata.Name.English
			if ((Item == ItemEN) and (Lang ~= 'English')) then Color = "warning" end
			if (Item == '') then Color = "danger" end
			if (Color == 'danger') then String = ItemEN else String = '[[' .. Item .. ']]' end
			String = '<span class="bg-' .. Color .. '">' .. String .. '</span>'
			WikiText = WikiText .. '<div class="col-xs-12 col-sm-6 col-md-4 col-lg-3">' .. String .. '</div>'
			end
		end
		
		WikiText = '<div class="container-fluid"><div class="row g-2 mb-3">' .. WikiText .. '</div></div>'
		WikiText =  '<h2>Items Page Diagnostic</h2>' .. WikiText
		
		return WikiText
end

function p.SubIndexTagsList()
		local WikiText =''
		
		local TagData = mw.loadData("Module:TagData")
		local TagList = TagData.tags
		
		for Tname,Tdata in pairs(TagList) do
			if (Tdata.IsVisibleInTooltip == 'True') then
				local Color = "success"
				local Tag = Tdata.Name[Lang]
				local TagEN = Tdata.Name.English
				local TagString = Utils.Translate("{0} Tag");
				local TagLink = Utils.VarSub(TagString,Tag);
				if ((Tag == TagEN) and (Lang ~= 'English')) then Color = "warning" end
				if (Tag == '') then Color = "danger" end
				if (Color == 'danger') then String = TagEN else String = '[[' .. TagLink .. ']]' end
				String = '<span class="bg-' .. Color .. '">' .. String .. '</span>'
				WikiText = WikiText .. '<div class="col-xs-12 col-sm-6 col-md-4 col-lg-3">' .. String .. '</div>'
			end
		end
		
		WikiText = '<div class="container-fluid"><div class="row g-2 mb-3">' .. WikiText .. '</div></div>'
		WikiText =  '<h2>Tags Page Diagnostic</h2>' .. WikiText	
		
		return WikiText
end

function p.SubIndexSkillsList()
		local WikiText =''
		local Professions =''
		local Specialties =''
		
		local SkillData = mw.loadData("Module:SkillData")
		local SkillList = SkillData.skills
		
		for Sname,Sdata in pairs(SkillList) do
			local Color = "success"
			local Skill = Sdata.Name[Lang]
			local SkillEN = Sdata.Name.English
			if ((Skill == SkillEN) and (Lang ~= 'English')) then Color = "warning" end
			if (Skill == '') then Color = "danger" end
			if (Color == 'danger') then String = SkillEN else String = '[[' .. Skill .. ']]' end
			String = '<div class="col-xs-12 col-sm-6 col-md-4 col-lg-3"><span class="bg-' .. Color .. '">' .. String .. '</span></div>'
			if Sdata.IsRoot == 'True' then Professions = Professions .. String else Specialties = Specialties .. String end
		end
		
		WikiText = '<h2>Professions Page Diagnostic</h2><div class="container-fluid"><div class="row g-2 mb-3">' .. Professions .. '</div></div>'
		WikiText =  WikiText .. '<h2>Specialties Page Diagnostic</h2><div class="container-fluid"><div class="row g-2 mb-3">' .. Specialties .. '</div></div>'
		
		return WikiText
end

function p.SubIndexBiomesList()
		local WikiText =''
		
		local BiomeData = mw.loadData("Module:BiomeData")
		local BiomeList = BiomeData.biomes
		for Bname,Bdata in pairs(BiomeList) do
			local Color = "success"
			local Biome = Bdata.Name[Lang]
			local BiomeEN = Bdata.Name.English
			if ((Biome == BiomeEN) and (Lang ~= 'English')) then Color = "warning" end
			if (Biome == '') then Color = "danger" end
			if (Color == 'danger') then String = BiomeEN else String = '[[' .. Biome .. ']]' end
			String = '<span class="bg-' .. Color .. '">' .. String .. '</span>'
			WikiText = WikiText .. '<div class="col-xs-12 col-sm-6 col-md-4 col-lg-3">' .. String .. '</div>'
		end
		
		WikiText = '<div class="container-fluid"><div class="row g-2 mb-3">' .. WikiText .. '</div></div>'
		WikiText =  '<h2>Biomes Page Diagnostic</h2>' .. WikiText
		
		return WikiText
end

function p.SubIndexAnimalsList()
		local WikiText =''
		
		local AnimalData = mw.loadData("Module:AnimalData")
		local AnimalList = AnimalData.animals
		for Aname,Adata in pairs(AnimalList) do
			local Color = "success"
			local Animal = Adata.Name[Lang]
			local AnimalEN = Adata.Name.English
			if ((Animal == AnimalEN) and (Lang ~= 'English')) then Color = "warning" end
			if (Animal == '') then Color = "danger" end
			if (Color == 'danger') then String = AnimalEN else String = '[[' .. Animal .. ']]' end
			String = '<span class="bg-' .. Color .. '">' .. String .. '</span>'
			WikiText = WikiText .. '<div class="col-xs-12 col-sm-6 col-md-4 col-lg-3">' .. String .. '</div>'
		end
		
		WikiText = '<div class="container-fluid"><div class="row g-2 mb-3">' .. WikiText .. '</div></div>'
		WikiText =  '<h2>Animals Page Diagnostic</h2>' .. WikiText
		
		return WikiText
end

function p.SubIndexPlantsList()
		local WikiText =''
		
		local PlantData = mw.loadData("Module:PlantData")
		local PlantList = PlantData.plants
		for Pname,Pdata in pairs(PlantList) do
			local Color = "success"
			local Plant = Pdata.Name[Lang]
			local PlantEN = Pdata.Name.English
			if ((Plant == PlantEN) and (Lang ~= 'English')) then Color = "warning" end
			if (Plant == '') then Color = "danger" end
			if (Color == 'danger') then String = PlantEN else String = '[[' .. Plant .. ']]' end
			String = '<span class="bg-' .. Color .. '">' .. String .. '</span>'
			WikiText = WikiText .. '<div class="col-xs-12 col-sm-6 col-md-4 col-lg-3">' .. String .. '</div>'
		end
		
		WikiText = '<div class="container-fluid"><div class="row g-2 mb-3">' .. WikiText .. '</div></div>'
		WikiText =  '<h2>Plants Page Diagnostic</h2>' .. WikiText
		
		return WikiText
end

function p.SubIndexTreesList()
		local WikiText =''
		
		local TreeData = mw.loadData("Module:TreeData")
		local TreeList = TreeData.trees
		for Tname,Tdata in pairs(TreeList) do
			local Color = "success"
			local Tree = Tdata.Name[Lang]
			local TreeEN = Tdata.Name.English
			if ((Tree == TreeEN) and (Lang ~= 'English')) then Color = "warning" end
			if (Tree == '') then Color = "danger" end
			if (Color == 'danger') then String = TreeEN else String = '[[' .. Tree .. ']]' end
			String = '<span class="bg-' .. Color .. '">' .. String .. '</span>'
			WikiText = WikiText .. '<div class="col-xs-12 col-sm-6 col-md-4 col-lg-3">' .. String .. '</div>'
		end
		
		WikiText = '<div class="container-fluid"><div class="row g-2 mb-3">' .. WikiText .. '</div></div>'
		WikiText =  '<h2>Plants Page Diagnostic</h2>' .. WikiText
		
		return WikiText
end

function p.SubIndexAchievementsList()
		local WikiText =''
		
		local AchievementsData = mw.loadData("Module:AchievementsData")
		local AchievementList = AchievementsData.achievements
		for Aname,Adata in pairs(AchievementList) do
			local Color = "success"
			local Achievement = Adata.Name[Lang]
			local AchievementEN = Adata.Name.English
			if ((Achievement == AchievementEN) and (Lang ~= 'English')) then Color = "warning" end
			if (Achievement == '') then Color = "danger" end
			if (Color == 'danger') then String = AchievementEN else String = Achievement end
			String = '<span class="bg-' .. Color .. '">' .. String .. '</span>'
			WikiText = WikiText .. '<div class="col-xs-12 col-sm-6 col-md-4 col-lg-3">' .. String .. '</div>'
		end
		
		WikiText = '<div class="container-fluid"><div class="row g-2 mb-3">' .. WikiText .. '</div></div>'
		WikiText =  '<h2>Achievements Page Diagnostic</h2>' .. WikiText
		
		return WikiText
end

return p