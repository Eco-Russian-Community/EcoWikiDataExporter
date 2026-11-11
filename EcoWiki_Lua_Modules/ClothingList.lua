local p = {}

local Utils = require('Module:Utils')
local IconUtils = require('Module:IconUtils')
local Lang = Utils.WikiLang

function p.main()

  local WikiText = ''

  local ClothingBack = ''
  local ClothingHead = ''
  local ClothingShirt = ''
  local ClothingShoes = ''
  local ClothingFace = ''
  local ClothingPants = ''
  local ClothingHands = ''
  local ClothingWaist = ''

  -- load lists
  local ClothingData = mw.loadData('Module:ClothingData')
  local ClothingList = ClothingData.clothing

  for Cname,Cdata in pairs(ClothingList) do
    if (Cdata.Hidden == false) then 
      if (Cdata.AvatarSlot == "Back") then if (ClothingBack == '') then ClothingBack = Cname else ClothingBack = ClothingBack .. ',' .. Cname end end
      if (Cdata.AvatarSlot == "Head") then if (ClothingHead == '') then ClothingHead = Cname else ClothingHead = ClothingHead .. ',' .. Cname end end
      if (Cdata.AvatarSlot == "Shirt") then if (ClothingShirt == '') then ClothingShirt = Cname else ClothingShirt = ClothingShirt .. ',' .. Cname end end
      if (Cdata.AvatarSlot == "Shoes") then if (ClothingShoes == '') then ClothingShoes = Cname else ClothingShoes = ClothingShoes .. ',' .. Cname end end
      if (Cdata.AvatarSlot == "Face") then if (ClothingFace == '') then ClothingFace = Cname else ClothingFace = ClothingFace .. ',' .. Cname end end
      if (Cdata.AvatarSlot == "Pants") then if (ClothingPants == '') then ClothingPants = Cname else ClothingPants = ClothingPants .. ',' .. Cname end end
      if (Cdata.AvatarSlot == "Hands") then if (ClothingHands == '') then ClothingHands = Cname else ClothingHands = ClothingHands .. ',' .. Cname end end
      if (Cdata.AvatarSlot == "Waist") then if (ClothingWaist == '') then ClothingWaist = Cname else ClothingWaist = ClothingWaist .. ',' .. Cname end end
    end
  end

  
  WikiText = WikiText .. '<h3>' .. Utils.Translate("Head") .. '</h3>'
  local ItemData = mw.loadData('Module:ItemData')
  local ItemList = Utils.StringListToArray(ClothingHead)
  for Count, ItemName in pairs(ItemList) do
		local Item = ItemData.items[ItemName]
		WikiText =  WikiText .. IconUtils.main{ name = Item.Name[Lang], id = Item.ID, size = 128, style = 4, link = Item.Name[Lang] }
	end
  
  WikiText = WikiText .. '<h3>' .. Utils.Translate("Face") .. '</h3>'
  local ItemData = mw.loadData('Module:ItemData')
  local ItemList = Utils.StringListToArray(ClothingFace)
  for Count, ItemName in pairs(ItemList) do
		local Item = ItemData.items[ItemName]
		WikiText =  WikiText .. IconUtils.main{ name = Item.Name[Lang], id = Item.ID, size = 128, style = 4, link = Item.Name[Lang] }
	end

  WikiText = WikiText .. '<h3>' .. Utils.Translate("Shirt") .. '</h3>'
  local ItemData = mw.loadData('Module:ItemData')
  local ItemList = Utils.StringListToArray(ClothingShirt)
  for Count, ItemName in pairs(ItemList) do
		local Item = ItemData.items[ItemName]
		WikiText =  WikiText .. IconUtils.main{ name = Item.Name[Lang], id = Item.ID, size = 128, style = 4, link = Item.Name[Lang] }
	end

  WikiText = WikiText .. '<h3>' .. Utils.Translate("Hands") .. '</h3>'
  local ItemData = mw.loadData('Module:ItemData')
  local ItemList = Utils.StringListToArray(ClothingHands)
  for Count, ItemName in pairs(ItemList) do
		local Item = ItemData.items[ItemName]
		WikiText =  WikiText .. IconUtils.main{ name = Item.Name[Lang], id = Item.ID, size = 128, style = 4, link = Item.Name[Lang] }
	end

  WikiText = WikiText .. '<h3>' .. Utils.Translate("Back") .. '</h3>'
  local ItemData = mw.loadData('Module:ItemData')
  local ItemList = Utils.StringListToArray(ClothingBack)
  for Count, ItemName in pairs(ItemList) do
		local Item = ItemData.items[ItemName]
		WikiText =  WikiText .. IconUtils.main{ name = Item.Name[Lang], id = Item.ID, size = 128, style = 4, link = Item.Name[Lang] }
	end

  WikiText = WikiText .. '<h3>' .. Utils.Translate("Waist") .. '</h3>'
  local ItemData = mw.loadData('Module:ItemData')
  local ItemList = Utils.StringListToArray(ClothingWaist)
  for Count, ItemName in pairs(ItemList) do
		local Item = ItemData.items[ItemName]
		WikiText =  WikiText .. IconUtils.main{ name = Item.Name[Lang], id = Item.ID, size = 128, style = 4, link = Item.Name[Lang] }
	end
  
  WikiText = WikiText .. '<h3>' .. Utils.Translate("Pants") .. '</h3>'
  local ItemData = mw.loadData('Module:ItemData')
  local ItemList = Utils.StringListToArray(ClothingPants)
  for Count, ItemName in pairs(ItemList) do
		local Item = ItemData.items[ItemName]
		WikiText =  WikiText .. IconUtils.main{ name = Item.Name[Lang], id = Item.ID, size = 128, style = 4, link = Item.Name[Lang] }
	end

  WikiText = WikiText .. '<h3>' .. Utils.Translate("Shoes") .. '</h3>'
  local ItemData = mw.loadData('Module:ItemData')
  local ItemList = Utils.StringListToArray(ClothingShoes)
  for Count, ItemName in pairs(ItemList) do
		local Item = ItemData.items[ItemName]
		WikiText =  WikiText .. IconUtils.main{ name = Item.Name[Lang], id = Item.ID, size = 128, style = 4, link = Item.Name[Lang] }
	end


  return WikiText
end

return p