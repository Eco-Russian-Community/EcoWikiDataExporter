local p = {}

function p.main(param)
    local Utils = require('Module:Utils')

    local Icon = ''
    local IconStyle = ''
    local IconSize = ''
    local IconLink = ''

    local args = Utils.normalise(param)

    if args.id == nil or args.name == '' then return 'Module:IconUtils \'id\' must be specified.' end

    Icon = args.id .. '_Icon.png'

    if args.size == nil or args.size == '' then IconSize = 28 else IconSize = args.size end
    if args.style == nil or args.style == '' then IconStyle = '1' else IconStyle = args.style end
    if args.link == nil or args.link == '' then IconLink = '' else IconLink = '[[' .. args.link .. ']]' end   
    if args.link == '1' then IconLink = '[[' .. args.name .. ']]' end
    if IconLink == '' then IconTextLine = '  ' .. args.name IconTextBr = '<br>' .. args.name else IconTextLine = '  ' .. IconLink IconTextBr = '<br>' .. IconLink end

    if IconStyle == '1' then return '[[file:'.. Icon ..'|'.. IconSize ..'px|link='.. IconLink ..']]' end
    if IconStyle == '2' then return '[[file:'.. Icon ..'|'.. IconSize ..'px|link='.. IconLink ..']]' .. IconTextLine end
    if IconStyle == '3' then return '[[file:'.. Icon ..'|'.. IconSize ..'px|link='.. IconLink ..']]' .. IconTextBr end
    if IconStyle == '4' then return '<div class="IconFrame">[[file:'.. Icon ..'|'.. IconSize ..'px|link='.. IconLink ..']]' .. IconTextBr .. '</div>' end
end

return p