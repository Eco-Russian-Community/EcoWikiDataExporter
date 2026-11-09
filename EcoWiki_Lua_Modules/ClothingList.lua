local p = {}

local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')
local Lang = Utils.WikiLang

function p.main()

  local WikiText = ''
  -- load lists
  local ClothingData = mw.loadData('Module:ClothingData')
  local ClothingList = ClothingData.clothing

  

  return WikiText
end

return p