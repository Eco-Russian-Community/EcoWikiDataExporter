local p = {}

local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')
local InfoCardUtils = require('Module:InfoCardUtils')
local Lang = Utils.WikiLang

function p.main(frame)
	local PageName = frame.args[1]
	if (Lang == 'English') then TreeName = PageName else TreeName = Utils.TreeSearch(PageName) end
	local TreeData = mw.loadData( "Module:TreeData" )
    local Tree = TreeData.trees[TreeName]
	local WikiText =''
	
	WikiText =  WikiText ..'__NOTOC__'
	WikiText =  WikiText ..'Page name get test: ' .. PageName ..'</br>'
	WikiText =  WikiText ..'Tree name get test: ' .. TreeName ..'</br>'
	
	
	if (Lang ~= 'English') then WikiText =  WikiText .. '[[en:' .. Tree.Name.English .. ']]' end
	if (Lang ~= 'Russian') then WikiText =  WikiText .. '[[ru:' .. Tree.Name.Russian .. ']]' end
	if (Lang ~= 'German') then WikiText =  WikiText .. '[[de:' .. Tree.Name.German .. ']]' end
	if (Lang ~= 'French') then WikiText =  WikiText .. '[[fr:' .. Tree.Name.French .. ']]' end
	
	local descriptionpage = Tree.Name[Lang] .. " - it is Tree."
	WikiText =  WikiText .. frame:callParserFunction{ name = '#description2', args = { descriptionpage }}
	WikiText =  WikiText .. frame:callParserFunction{ name = '#setmainimage', args = { TreeName .. "_Tree.jpg" }}
	
	return WikiText
end

return p
