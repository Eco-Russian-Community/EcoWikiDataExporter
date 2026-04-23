local p = {}

local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')
local RecipeUtils = require('Module:RecipeUtils')
local Lang = Utils.WikiLang

function p.main(frame)
	local PageName = frame.args[1]
	local SkillName = ''
	if (Lang == 'English') then SkillName = PageName else SkillName = Utils.SkillSearch(PageName) end
	local SkillData = mw.loadData( "Module:SkillData" )
    local Skill = SkillData.skills[SkillName]
    local RootSkill = SkillData.skills[Skill.RootSkill]
	local SkillTier = Skill.Tier
    local ItemData = mw.loadData( "Module:ItemData" )
    local SkillBookName  = Skill.Name.English .. ' Skill Book'
    local SkillBook = ItemData.items[SkillBookName]
    local SkillScroll = Skill.Name.English .. ' Skill Scroll'
    local SkillScroll = ItemData.items[SkillScroll]
    local ClaimPaper = ItemData.items['Claim Paper Item']
	local WikiText =''
	
	WikiText =  WikiText ..'__NOTOC__'
	WikiText =  WikiText .. IconUtils.main{name = Skill.Name[Lang], id = Skill.SkillID , size = 32, style = 1} .. ' '
	if (Skill.IsRoot == 'True') then WikiText =  WikiText .. Skill.Name[Lang] .. ' is [[Skills|Profession]].<br>'   else WikiText =  WikiText .. Skill.Name[Lang] .. ' is [[Skills|Specialty]] related to the profession of [[' .. RootSkill.Name[Lang] ..']].<br>' end
	WikiText =  WikiText .. Skill.Description[Lang] .. '<br>'
	local SpecialtiesText = Utils.Translate('Specialties')
	
	if (Skill.IsRoot == 'True') then
		WikiText =  WikiText .. '<h3>' .. SpecialtiesText .. '</h3>'
		WikiText =  WikiText .. 'The '.. Skill.Name[Lang] .. ' Profession includes the following Specialties:<br>'
		WikiText =  WikiText .. '<div class="container-fluid" id="icon-grid"><div class="row row-cols-1 row-cols-sm-1 row-cols-md-2 row-cols-lg-2 g-4 py-5">'
		for Sname,Sdata in pairs(SkillData.skills) do
			if Sdata.IsRoot == 'False' and Sdata.RootSkill == SkillName then
				local SpecialtyName = ''
				local SpecialtyDescription = ''
				local Stars = Utils.Stars(Sdata.Tier)
				local IconName = Sdata.SkillID
				if (Sdata.Name[Lang] == "") then SpecialtyName = Sdata.Name.English else SpecialtyName = Sdata.Name[Lang] end
				if (Sdata.Description[Lang] == "") then SpecialtyDescription = Sdata.Description.English else SpecialtyDescription = Sdata.Description[Lang] end
				WikiText =  WikiText .. '<div class="col d-flex align-items-start">'
				WikiText =  WikiText .. '[[file:' .. IconName.. '_Icon.png|64px|link='.. SpecialtyName .. '|class=IconGrid]]'
				WikiText =  WikiText .. '<div><h5 class="fw-bold mb-0 fs-4 text-body-emphasis">[[' .. SpecialtyName .. ']]' .. Stars.. '</h5><p>' .. SpecialtyDescription .. '</p></div>'
				WikiText =  WikiText .. '</div>'
			end
		end
		WikiText =  WikiText .. '</div></div>'
	else
		WikiText =  WikiText .. '<h3>How to learn</h3>'
			if (Skill.PlayerDefaultSkill == 'True') then
				if (SkillName == 'Self Improvement') then WikiText =  WikiText .. Skill.Name[Lang] .. ' Specialty is learned and have level 1 from the start of the game.'
				else WikiText =  WikiText .. Skill.Name[Lang] .. ' Specialty is learned and available from the start of the game in the Skill Book (key <kbd class="keyboard-key nowrap">Z</kbd>)' end
			else
				WikiText =  WikiText .. Skill.Name[Lang] .. ' is learned by [[File:RMB.png|15px|alt=RMB|link=]] RMB on [[File:SkillScroll_Icon.png|32px|link=]] <b>' .. SkillScroll.Name[Lang] .. '</b>, what was obtained from the [[File:SkillBook_Icon.png|32px|link=]] <b>' .. SkillBook.Name[Lang] .. '</b>.</br>'
				WikiText =  WikiText .. 'When learning a [[File:SkillScroll_Icon.png|32px|link=]] <b>' ..  SkillScroll.Name[Lang] .. '</b>, depending on the server settings, the player can also receive several [[' .. ClaimPaper.Name[Lang] .. ']] to expand [[Residency|Homestead]].'
			end
		
			if (Skill.PlayerDefaultSkill == 'False') then 
				local RecipeItemCraft = RecipeUtils.ItemCraft(SkillBookName)
				if (RecipeItemCraft ~= "") then
					WikiText =  WikiText .. '</br>' .. '<h3>' .. Utils.Translate("Crafted At") .. ':</h3>';
					WikiText =  WikiText .. RecipeUtils.CraftTable(RecipeItemCraft);
				end
			end
		
		WikiText =  WikiText .. '<h3>Benefits and item availability by level:</h3>'
		if (SkillName ~= 'Self Improvement') then
			WikiText =  WikiText .. 'The study of specialization start from level 0'
			if (Skill.PlayerDefaultSkill == 'False') then WikiText =  WikiText .. ' when learning a [[File:SkillScroll_Icon.png|32px|link=]] <b>' ..  SkillScroll.Name[Lang] .. '</b>' end
			WikiText =  WikiText .. ' and increases to level 1 by spending a star.'
		else
			WikiText =  WikiText .. 'For increasing any specialization by 1 level you will receive 20 experience points of ' .. Skill.Name[Lang] .. ' Specialty.'
		end
		
		WikiText =  WikiText .. '</br>The maximum level of ' .. Skill.Name[Lang] .. ' specialization that can be achieved is level <b>' .. Skill.MaxLevel .. '</b>.'
		
		WikiText =  WikiText .. '<h4>Amount of Specialty XP Per Level:</h4>'
		WikiText =  WikiText .. Utils.SpecialtyXP(Skill.Tier)
		WikiText =  WikiText ..'* Based on default server settings (SpecialtyExperiencePerLevelSquared=25)'
		
		WikiText =  WikiText .. '<h3>Talents:</h3>'
		local TalentData = require( "Module:TalentData" )
		local TalentList = TalentData.talents
		local TalentThreeCount = 0
		local TalentSixCount = 0
		local TalentsThree = {}
		local TalentsSix = {}
		
		for Tname,Tdata in pairs(TalentList) do
			if (Tdata.SkillID == Skill.SkillID) then 
				if (Tdata.Level == '3') then TalentThreeCount = TalentThreeCount + 1; TalentsThree[TalentThreeCount] = Tname; end
				if (Tdata.Level == '6') then TalentSixCount = TalentSixCount + 1; TalentsSix[TalentSixCount] = Tname; end
			end
		end
		
		WikiText =  WikiText .. '<h4>At 3rd level, the player can choose one of two proposed talents:</h4>'
		WikiText =  WikiText .. '<div class="container-fluid" id="icon-grid"><div class="row row-cols-1 row-cols-sm-1 row-cols-md-2 row-cols-lg-2 g-4 py-4">'
		
		if (TalentList[TalentsThree[1]].Name[Lang] == "") then TalentThreeName = TalentList[TalentsThree[1]].Name.English else TalentThreeName = TalentList[TalentsThree[1]].Name[Lang] end
		if (TalentList[TalentsThree[1]].Description[Lang] == "") then TalentsThreeDescription = TalentList[TalentsThree[1]].Description.English else TalentsThreeDescription = TalentList[TalentsThree[1]].Description[Lang] end
		if (Utils.checkImage(TalentList[TalentsThree[1]].IconName .. '_Icon.png') == "Y") then IconName = TalentList[TalentsThree[1]].IconName else IconName = 'NoItem' end
		WikiText =  WikiText .. '<div class="col d-flex align-items-start">'
		WikiText =  WikiText .. '[[file:' .. IconName.. '_Icon.png|64px|link=|class=IconGrid]]'
		WikiText =  WikiText .. '<div><h5 class="fw-bold mb-0 fs-4 text-body-emphasis">' .. TalentThreeName .. '</h5><p>' .. TalentsThreeDescription .. '</p></div>'
		WikiText =  WikiText .. '</div>'
		
		if (TalentList[TalentsThree[2]].Name[Lang] == "") then TalentThreeName = TalentList[TalentsThree[2]].Name.English else TalentThreeName = TalentList[TalentsThree[2]].Name[Lang] end
		if (TalentList[TalentsThree[2]].Description[Lang] == "") then TalentsThreeDescription = TalentList[TalentsThree[2]].Description.English else TalentsThreeDescription = TalentList[TalentsThree[2]].Description[Lang] end
		if (Utils.checkImage(TalentList[TalentsThree[2]].IconName .. '_Icon.png') == "Y") then IconName = TalentList[TalentsThree[2]].IconName else IconName = 'NoItem' end
		WikiText =  WikiText .. '<div class="col d-flex align-items-start">'
		WikiText =  WikiText .. '[[file:' .. IconName.. '_Icon.png|64px|link=|class=IconGrid]]'
		WikiText =  WikiText .. '<div><h5 class="fw-bold mb-0 fs-4 text-body-emphasis">' .. TalentThreeName .. '</h5><p>' .. TalentsThreeDescription .. '</p></div>'
		WikiText =  WikiText .. '</div>'
		
		WikiText =  WikiText .. '</div></div>'
		WikiText =  WikiText .. '<h4>At 6th level, the player can choose one of two proposed talents:</h4>'
		WikiText =  WikiText .. '<div class="container-fluid" id="icon-grid"><div class="row row-cols-1 row-cols-sm-1 row-cols-md-2 row-cols-lg-2 g-4 py-4">'
		
		if (TalentList[TalentsSix[1]].Name[Lang] == "") then TalentsSixName = TalentList[TalentsSix[1]].Name.English else TalentsSixName = TalentList[TalentsSix[1]].Name[Lang] end
		if (TalentList[TalentsSix[1]].Description[Lang] == "") then TalentsSixDescription = TalentList[TalentsSix[1]].Description.English else TalentsSixDescription = TalentList[TalentsSix[1]].Description[Lang] end
		if (Utils.checkImage(TalentList[TalentsSix[1]].IconName .. '_Icon.png') == "Y") then IconName = TalentList[TalentsSix[1]].IconName else IconName = 'NoItem' end
		WikiText =  WikiText .. '<div class="col d-flex align-items-start">'
		WikiText =  WikiText .. '[[file:' .. IconName.. '_Icon.png|64px|link=|class=IconGrid]]'
		WikiText =  WikiText .. '<div><h5 class="fw-bold mb-0 fs-4 text-body-emphasis">' .. TalentsSixName .. '</h5><p>' .. TalentsSixDescription .. '</p></div>'
		WikiText =  WikiText .. '</div>'
		
		if (TalentList[TalentsSix[2]].Name[Lang] == "") then TalentsSixName = TalentList[TalentsSix[2]].Name.English else TalentsSixName = TalentList[TalentsSix[2]].Name[Lang] end
		if (TalentList[TalentsSix[2]].Description[Lang] == "") then TalentsSixDescription = TalentList[TalentsSix[2]].Description.English else TalentsSixDescription = TalentList[TalentsSix[2]].Description[Lang] end
		if (Utils.checkImage(TalentList[TalentsSix[2]].IconName .. '_Icon.png') == "Y") then IconName = TalentList[TalentsSix[2]].IconName else IconName = 'NoItem' end
		WikiText =  WikiText .. '<div class="col d-flex align-items-start">'
		WikiText =  WikiText .. '[[file:' .. IconName.. '_Icon.png|64px|link=|class=IconGrid]]'
		WikiText =  WikiText .. '<div><h5 class="fw-bold mb-0 fs-4 text-body-emphasis">' .. TalentsSixName .. '</h5><p>' .. TalentsSixDescription .. '</p></div>'
		WikiText =  WikiText .. '</div>'
		
		WikiText =  WikiText .. '</div></div>'
		
		WikiText =  WikiText .. '<h3>Upgrade modules:</h3>'
		WikiText =  WikiText .. '<h3>Clothing:</h3>'
	end
	
	WikiText =  WikiText .. '<h3>How use Icon</h3>'
	WikiText =  WikiText .. '<p>The ' .. Skill.Name[Lang] .. ' icon can be used on any sign that has a text component, including on [[Vehicles]]:</br>'
	WikiText =  WikiText .. 'Icon with background: <icon name="' .. Skill.SkillID .. '"></br>'

	if (Lang ~= 'English') then WikiText =  WikiText .. '[[en:' .. Skill.Name.English .. ']]' end
	if (Lang ~= 'Russian') then WikiText =  WikiText .. '[[ru:' .. Skill.Name.Russian .. ']]' end
	if (Lang ~= 'German') then WikiText =  WikiText .. '[[de:' .. Skill.Name.German .. ']]' end
	if (Lang ~= 'French') then WikiText =  WikiText .. '[[fr:' .. Skill.Name.French .. ']]' end
	
	local descriptionpage = Skill.Name[Lang] .. " - " .. Skill.Description[Lang]
	WikiText =  WikiText .. frame:callParserFunction{ name = '#description2', args = { descriptionpage }}
	
	return WikiText
end

return p
