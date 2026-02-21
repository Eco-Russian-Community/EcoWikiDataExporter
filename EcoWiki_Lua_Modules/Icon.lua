local p = {}

function p.main(param)
    local Utils = require('Module:Utils')
    local IconUtils = require('Module:IconUtils')

    local args = Utils.normaliseArgs(param)
    local itemID = args.id
    if (itemID == nil) then itemID = Utils.CheckId(args.name); end

    return IconUtils.main{ name =  args.name, id = itemID , size = args.size , style = args.style , link = args.link }
    
end

return p