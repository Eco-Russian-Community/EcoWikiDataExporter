local p = {}

local Utils = require('Module:Utils')
local Lang = Utils.WikiLang

function p.main()
	local WikiText = ''
	local TableHeader = ''
	local wikiuser = ''
	local wikiadmin = ''
	local wikidev = ''

	-- import the required modules
	local CommandData = mw.loadData( "Module:CommandData" )	
	local Commands = CommandData.commands

	-- Create the header of the table
	TableHeader = '<table class="table table-striped table-bordered sortable"><tr class="thead-dark">' 
	TableHeader = TableHeader .. '<th>Access<br>level</th>'
	TableHeader = TableHeader .. '<th>Command</th>'
	TableHeader = TableHeader .. '<th>Short<br>call</th>'
	TableHeader = TableHeader .. '<th class="unsortable">Description<br>Arguments</th>'
	
	WikiText = TableHeader

	for CommandName,CommandData in pairs(Commands) do
		local TableRow = ''
		local CommandDescription = ''
		local CommandParameters = ''
		local CommandParent = ''
		local CommandShortCut = ''
		TableRow = TableRow .. '<tr>'

		if (CommandData.parent == nil) then CommandParent = '' else CommandParent = CommandData.parent .. " " end
		if (CommandData.parameters == nil) then CommandParameters = '-'
		else
			if (CommandData.parameters.Arg1 ~= nil) then ARG1 = '<br><b>' .. CommandData.parameters.Arg1[1] .. '</b> (' .. CommandData.parameters.Arg1[2] .. ')' else ARG1 = '' end
			if (CommandData.parameters.Arg2 ~= nil) then ARG2 = ', <b>' .. CommandData.parameters.Arg2[1] .. '</b> (' .. CommandData.parameters.Arg2[2] .. ')' else ARG2 = '' end
			if (CommandData.parameters.Arg3 ~= nil) then ARG3 = ', <b>' .. CommandData.parameters.Arg3[1] .. '</b> (' .. CommandData.parameters.Arg3[2] .. ')' else ARG3 = '' end
			if (CommandData.parameters.Arg4 ~= nil) then ARG4 = ', <b>' .. CommandData.parameters.Arg4[1] .. '</b> (' .. CommandData.parameters.Arg4[2] .. ')' else ARG4 = '' end
			if (CommandData.parameters.Arg5 ~= nil) then ARG5 = ', <b>' .. CommandData.parameters.Arg5[1] .. '</b> (' .. CommandData.parameters.Arg5[2] .. ')' else ARG5 = '' end
			if (CommandData.parameters.Arg6 ~= nil) then ARG6 = ', <b>' .. CommandData.parameters.Arg6[1] .. '</b> (' .. CommandData.parameters.Arg6[2] .. ')' else ARG6 = '' end
			CommandParameters = ARG1 .. ARG2 .. ARG3 .. ARG4 .. ARG5 .. ARG6
		end

		if (CommandData.shortCut ~= '') then CommandShortCut = '/' .. CommandData.shortCut else CommandShortCut = '' end

		TableRow = TableRow .. '<td>' .. CommandData.level.. ' </td>'
		TableRow = TableRow .. '<td><b>/' .. CommandParent .. CommandData.command.. '</b></td>'
		TableRow = TableRow .. '<td><b>' .. CommandShortCut .. ' </td>'
		if (CommandData.helpText[Lang] == '') then CommandDescription = CommandData.helpText.English else CommandDescription = CommandData.helpText[Lang] end
		TableRow = TableRow .. '<td>' .. CommandDescription .. CommandParameters .. '</b></td>'

		TableRow = TableRow .. '</tr>'

		if ( CommandData.level == 'User' ) then wikiuser = wikiuser .. TableRow end
		if ( CommandData.level == 'Admin' ) then wikiadmin = wikiadmin .. TableRow end
		if ( CommandData.level == 'DevTier' ) then wikidev = wikidev .. TableRow end
	end
	WikiText = WikiText .. wikiuser .. wikiadmin .. wikidev
	WikiText = WikiText .. ' </table>'
	return WikiText
	end

return p
