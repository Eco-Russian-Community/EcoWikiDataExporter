local p = {}

local Utils = require('Module:Utils')
local Lang = Utils.getLanguageName()

function p.main()
	local wiki = ''
	local wikiuser = ''
	local wikiadmin = ''
	local wikidev = ''

	-- import the required modules
	local commandsData = require( "Module:CommandData" )	
	
	local commands = commandsData.commands

	-- create the header of the table
	local header = "<table class=\"wikitable sortable\"> \n " 
	header = header .. "<tr> \n "
	header = header .. "<th>Access<br>level</th> \n "
	header = header .. "<th>Command</th> \n "
	header = header .. "<th>Short<br>call</th> \n "
	header = header .. "<th>Description<br>Arguments</th> \n "
	header = header .. "</tr> \n "
	
	wiki = header

	for k,v in pairs(commands) do
		local row = ''
		row = row .. "<tr> \n "

		if (v.parent == nil) then v.parent = '' else v.parent = v.parent .. " " end
		if (v.parameters == nil) then v.parameters= '-'
		else
			if (v.parameters.Arg1 ~= nil) then ARG1 = "<br><b>" .. v.parameters.Arg1[1] .. "</b> (" .. v.parameters.Arg1[2] .. ")" else ARG1 = "" end
			if (v.parameters.Arg2 ~= nil) then ARG2 = ", <b>" .. v.parameters.Arg2[1] .. "</b> (" .. v.parameters.Arg2[2] .. ")" else ARG2 = "" end
			if (v.parameters.Arg3 ~= nil) then ARG3 = ", <b>" .. v.parameters.Arg3[1] .. "</b> (" .. v.parameters.Arg3[2] .. ")" else ARG3 = "" end
			if (v.parameters.Arg4 ~= nil) then ARG4 = ", <b>" .. v.parameters.Arg4[1] .. "</b> (" .. v.parameters.Arg4[2] .. ")" else ARG4 = "" end
			if (v.parameters.Arg5 ~= nil) then ARG5 = ", <b>" .. v.parameters.Arg5[1] .. "</b> (" .. v.parameters.Arg5[2] .. ")" else ARG5 = "" end
			if (v.parameters.Arg6 ~= nil) then ARG6 = ", <b>" .. v.parameters.Arg6[1] .. "</b> (" .. v.parameters.Arg6[2] .. ")" else ARG6 = "" end
			v.parameters= ARG1 .. ARG2 .. ARG3 .. ARG4 .. ARG5 .. ARG6
		end

		if (v.shortCut ~= '') then v.shortCut = "/".. v.shortCut end

		row = row .. "<td>" .. v.level.. " </td> \n"
		row = row .. "<td><b>/" .. v.parent .. v.command.. "</b></td> \n"
		row = row .. "<td><b>" .. v.shortCut .. " </td> \n"
		if (v.helpText[Lang] == '') then CommandDescription = v.helpText.English else CommandDescription = v.helpText[Lang] end
		row = row .. "<td>" .. CommandDescription .. v.parameters .. "</b></td> \n"

		row = row .. "</tr>"
	if ( v.level == 'User' ) then wikiuser = wikiuser .. row end
	if ( v.level == 'Admin' ) then wikiadmin = wikiadmin .. row end
	if ( v.level == 'DevTier' ) then wikidev = wikidev .. row end

	end
	wiki = wiki .. wikiuser .. wikiadmin .. wikidev
	wiki = wiki .. " </table>"
	return wiki
	end

return p