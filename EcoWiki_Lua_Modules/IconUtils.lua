local p = {}

function p.main(param)
    local Utils = require('Module:Utils')

    local Icon = ''
    local IconStyle = ''
    local IconSize = ''
    local IconLink = ''
    local IconBorder = ''
    local IconCount = ''

    local args = Utils.normalise(param)

    if args.id == nil or args.name == '' then return 'Module:IconUtils \'id\' must be specified.' end

    local Icon = args.id .. '_Icon.png'

    if args.size == nil or args.size == '' then IconSize = 28 else IconSize = args.size end
    if args.style == nil or args.style == '' then IconStyle = '1' else IconStyle = args.style end
    if args.link == nil or args.link == '' then IconLink = '' else IconLink = '[[' .. args.link .. ']]' end   
    if args.link == '1' then IconLink = '[[' .. args.name .. ']]' end
    if IconLink == '' then IconTextLine = '  ' .. args.name IconTextBr = '<br>' .. args.name else IconTextLine = '  ' .. IconLink IconTextBr = '<br>' .. IconLink end
    if args.count == nil or args.count == '' then IconCount = '1' else IconCount = args.count end
    if args.border == nil or args.border == '' then IconBorder = ' borderwhite' else IconBorder = ' border' .. args.border end

    if IconStyle == '1' then return '[[file:'.. Icon ..'|'.. IconSize ..'px|link='.. IconLink ..']]' end
    if IconStyle == '2' then return '[[file:'.. Icon ..'|'.. IconSize ..'px|link='.. IconLink ..']]' .. IconTextLine end
    if IconStyle == '3' then return '[[file:'.. Icon ..'|'.. IconSize ..'px|link='.. IconLink ..']]' .. IconTextBr end
    if IconStyle == '4' then return '<div class="IconFrame">[[file:'.. Icon ..'|'.. IconSize ..'px|link='.. IconLink ..']]' .. IconTextBr .. '</div>' end
    if IconStyle == '5' then return '<div class="recipeitem withcount' .. IconBorder .. '" style="font-size:' .. IconSize .. 'px">' .. '<div class="recipeitemicon">[[file:'.. Icon ..'|'.. IconSize ..'px|link=]]</div><span class="recipeitemcount">' .. IconCount .. '</span>' .. '[[' .. args.link .. '|<div class="recipeitemlink" title="' .. args.link .. '"></div>]]</div>' end
    if IconStyle == '6' then return '<div class="recipeitem' .. IconBorder .. '" style="font-size:' .. IconSize .. 'px">' .. '<div class="recipeitemicon">[[file:'.. Icon ..'|'.. IconSize ..'px|link=]]</div>[[' .. args.link .. '|<div class="recipeitemlink" title="' .. args.link .. '"></div>]]</div>' end
end

return p
