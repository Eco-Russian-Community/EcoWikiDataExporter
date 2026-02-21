local p = {}

local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')
local Lang = Utils.getLanguageName()

-- Main entry point for the Module
function p.main()
  
  -- load lists
  local TagData = mw.loadData("Module:TagData")
  local tagList = TagData.tags
  local text = ''
  
  text = '<div class="row">\n'

  for k,v in pairs(tagList) do
            
    if v.IsVisibleInTooltip == 'True' then
        local TagName = v.Name[Lang]
        local tagLink = Utils.VSTranslate("{0} Tag",TagName)
        local tagID = v.ID
            	
        text = text .. '<div class="col-lg-3">\n'
        text = text .. IconUtils.main{ name = TagName, id = tagID, size = 32, style = 2, link = tagLink }
        text = text .. '</div>\n'
		end
    end

    text = text .. '</div>'

  return text
end

return p