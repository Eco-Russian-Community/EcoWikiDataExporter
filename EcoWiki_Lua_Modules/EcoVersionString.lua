local p = {}

function p.main()
	local VersionData = mw.loadData("Module:EcoVersionData")
	assert(VersionData.game, "Failed to load Data from Module:EcoVersionData!")

	local EcoVersionString = VersionData.game["eco"]["VersionNumber"]
	
	if EcoVersionString then
		return '[https://steamcommunity.com/app/382310/announcements/ ' .. EcoVersionString .. ']'
	else
		return "<span style=\"color:red\">ERROR: Failed to load Eco version from Data.</span>"
	end
end

return p