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
	TableHeader = '<table class="table table-striped table-bordered sortable"><tr class="table-dark">' 
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
		local ARG1 = ''; ARG2 = ''; ARG3 = ''; ARG4 = ''; ARG5 = ''; ARG6 = ''; ARG7 = '';
		TableRow = TableRow .. '<tr>'

		if (CommandData.Parent ~= '') then CommandParent = CommandData.Parent .. " " end
		if (CommandData.Parameters == nil) then CommandParameters = '' 
		else
			-- if (CommandData.Parameters.Arg1 ~= null) then ARG1 = '<br><b>' .. CommandData.Parameters.Arg1[1] .. '</b> (' .. CommandData.Parameters.Arg1[2] .. ')' else ARG1 = '' end
			
			for Parameter, ParameterData in pairs(CommandData.Parameters) do
				local ParameterDataList = Utils.StringListToArray(ParameterData)
				
				if (Parameter == "Arg1") then ARG1 = '<br><b>' .. ParameterDataList[1] .. '</b> (' .. ParameterDataList[2] .. ')' end
				if (Parameter == "Arg2") then ARG2 = ', <b>' .. ParameterDataList[1] .. '</b> (' .. ParameterDataList[2] .. ')' end
				if (Parameter == "Arg3") then ARG3 = ', <b>' .. ParameterDataList[1] .. '</b> (' .. ParameterDataList[2] .. ')' end
				if (Parameter == "Arg4") then ARG4 = ', <b>' .. ParameterDataList[1] .. '</b> (' .. ParameterDataList[2] .. ')' end
				if (Parameter == "Arg5") then ARG5 = ', <b>' .. ParameterDataList[1] .. '</b> (' .. ParameterDataList[2] .. ')' end
				if (Parameter == "Arg6") then ARG6 = ', <b>' .. ParameterDataList[1] .. '</b> (' .. ParameterDataList[2] .. ')' end
				if (Parameter == "Arg7") then ARG7 = ', <b>' .. ParameterDataList[1] .. '</b> (' .. ParameterDataList[2] .. ')' end
			end
			CommandParameters = ARG1 .. ARG2 .. ARG3 .. ARG4 .. ARG5 .. ARG6 .. ARG7
		end

		if (CommandData.ShortCut ~= '') then CommandShortCut = '/' .. CommandData.ShortCut else CommandShortCut = '' end

		TableRow = TableRow .. '<td>' .. CommandData.Level.. ' </td>'
		TableRow = TableRow .. '<td><b>/' .. CommandParent .. CommandData.Command.. '</b></td>'
		TableRow = TableRow .. '<td><b>' .. CommandShortCut .. ' </td>'
		if (CommandData.Description[Lang] == '') then CommandDescription = CommandData.Description.English else CommandDescription = CommandData.Description[Lang] end
		TableRow = TableRow .. '<td>' .. CommandDescription .. CommandParameters .. '</b></td>'

		TableRow = TableRow .. '</tr>'

		if ( CommandData.Level == 'User' ) then wikiuser = wikiuser .. TableRow end
		if ( CommandData.Level == 'Admin' ) then wikiadmin = wikiadmin .. TableRow end
		if ( CommandData.Level == 'DevTier' ) then wikidev = wikidev .. TableRow end
	end
	WikiText = WikiText .. wikiuser .. wikiadmin .. wikidev
	WikiText = WikiText .. ' </table>'
	return WikiText
	end

return p