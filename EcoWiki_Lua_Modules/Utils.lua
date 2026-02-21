local p = {}

--- Trims and parses the args into a table, then returns the table
function p.normalise(args)

	for k, v in pairs(args) do
		v = mw.text.trim(tostring(v))
		if v ~= '' then
			args[k] = v
		end
	end

	return args
end

--- Trims and parses the args into a table, then returns the table
--- @author User:Avaren
function p.normaliseArgs(frame)
	local origArgs = frame:getParent().args
	local args = {}

	for k, v in pairs(origArgs) do
		v = mw.text.trim(tostring(v))
		if v ~= '' then
			args[k] = v
		end
	end

	return args
end

function p.CheckId(name)
	local itemData = mw.loadData("Module:ItemData")
    local ItemName = p.ItemSearch(name) 
    local itemTable = itemData.items[ItemName]
    if itemTable == nil then return 'NoItem' end
    local IconName = itemTable.ID
	
	return IconName
end

function p.CheckList(List)
	local NewList = ""
	if (List ~= "") then
		local TempList ={}
		for Name in string.gmatch(List, "([^,]+)") do
			TempList[Name] = Name
		end
		
		for Name,Data in pairs(TempList) do
			if (NewList == "") then NewList = NewList .. Name else NewList = NewList .. "," .. Name end
		end
	end
	
	return NewList
end

function p.getLanguageCode()
  local language = mw.language.getContentLanguage()
  local languageCode = language:getCode()
  return languageCode
end

function p.getLanguageName()
  local languageName = "English"
  local	language = mw.language.getContentLanguage()
  local languageCode = language:getCode()
  if languageCode == "ru" then languageName = "Russian" end
  if languageCode == "de" then languageName = "German" end
  if languageCode == "fr" then languageName = "French" end
  return languageName
end

function p.checkImage(filename)
	if filename then
		if mw.title.makeTitle('Media', filename).file.exists then return "Y" else return "N" end
	else return "Error name" end
end

function p.checkPage(pagename)
	local pagetitle = mw.title.new(pagename)
	if pagetitle and pagetitle.exists then return "Y" else return "N" end
end


function p.SkillSearch(PageName)
	local SkillName = ''
	local skillData = require( "Module:SkillData" )
    local skillList = skillData.skills
    local Lang = p.getLanguageName()
    	for Sname,Sdata in pairs(skillList) do
    		if (Sdata.Name[Lang] == PageName) then SkillName = Sname end
    	end
    return SkillName
end

function p.SkillSearchByID(SkillID)
	local SkillName = 'None'
	local skillData = require( "Module:SkillData" )
    local skillList = skillData.skills
    local Lang = p.getLanguageName()
    	for Sname,Sdata in pairs(skillList) do
    		if (Sdata.SkillID == SkillID) then SkillName = Sname end
    	end
	return SkillName
end

function p.ItemSearch(PageName)
	local ItemName = 'None'
	local ItemData = require( "Module:ItemData" )
    local ItemList = ItemData.items
    local Lang = p.getLanguageName()
    	for Iname,Idata in pairs(ItemList) do
    		if (Idata.Name[Lang] == PageName) then ItemName = Iname end
    	end
    return ItemName
end

function p.TagSearch(PageName)
	local TagName = 'None'
	local TagData = require( "Module:TagData" )
    local TagList = TagData.tags
    local Lang = p.getLanguageName()
    	for Tname,Tdata in pairs(TagList) do
    		if (Tdata.Name[Lang] == PageName) then TagName = Tname end
    	end
    return TagName
end

function p.BiomeSearch(PageName)
	local BiomeName = 'None'
	local BiomeData = require( "Module:BiomeData" )
    local BiomeList = BiomeData.biomes
    local Lang = p.getLanguageName()
    	for Bname,Bdata in pairs(BiomeList) do
    		if (Bdata.Name[Lang] == PageName) then BiomeName = Bname end
    	end
    return BiomeName    
end

function p.AnimalSearch(PageName)
	local AnimalName = 'None'
	local AnimalData = require( "Module:AnimalData" )
    local AnimalList = AnimalData.animals
    local Lang = p.getLanguageName()
    	for Aname,Adata in pairs(AnimalList) do
    		if (Adata.Name[Lang] == PageName) then AnimalName = Aname end
    	end
    return AnimalName
end

function p.SpecialtyXP(Tier)
	local WikiText =  ''
	WikiText =  WikiText ..'<table class="table table-striped table-bordered"><tr class="thead-dark"><th>Specialty Level</th><th>2</th><th>3</th><th>4</th><th>5</th><th>6</th><th>7</th></tr><tr><td>Specialty XP</td>'
	for i = 1, 6 do
		local XP = Tier * ( 25 * i) ^ 2
		WikiText =  WikiText .. '<td>' .. XP .. '</td>'
	end
	WikiText =  WikiText ..'</tr></table>'
    return WikiText
end

function p.Translate(String)
	local Translate = String
	local Lang = p.getLanguageName()
	local TranslateData = require( "Module:LocalizationData" )
	local TranslateList = TranslateData.locales
		for Tname,Tdata in pairs(TranslateList) do
    		if ( Tname == String) then Translate = Tdata.Translate[Lang] end
    	end
	
	return Translate
end

function p.ItemTags(TagsList)
	local WikiText = ""
	local WikiTagText = ""
	local Lang = p.getLanguageName()
	local TagString = p.Translate("{0} Tag");
	local TagData = require( "Module:TagData" )
	local IconUtils = require('Module:IconUtils')
		for Count,Tname in pairs(TagsList) do
			TagName = TagData.tags[Tname];
			TagNameLoc = TagName.Name[Lang];
			TagLink = p.VarSub(TagString,TagNameLoc);
			if (TagName.IsVisibleInTooltip == "True") then WikiTagText = IconUtils.main{ name = TagNameLoc, id = TagName.ID, size = 128, style = 4, link = TagLink } end
		end
	if (WikiTagText ~= "") then WikiText = '<div class="row">' .. WikiTagText .. '</div>' end
	return WikiText
end

-- variable substitution
function p.VarSub(String,Loc)
	return string.gsub(String,'{0}',Loc)
end

function p.VSTranslate(String,Loc)
	return p.VarSub(p.Translate(String),Loc)
end

function p.gallery(context)
	local gallery = frame:callParserFunction{ name = '#tag:gallery', args = { mode = 'slideshow', widths = '100%', ''.. context:getParent().args[1] .. '' , showthumbnails = 'true'} }
	return gallery
end
return p