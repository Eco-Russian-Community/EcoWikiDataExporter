local p = {}


function p.GetVersionData()
    local VersionData = mw.loadData("Module:EcoVersionData")
    assert(VersionData.ecogame, "Failed to load Data from Module:EcoVersionData!")
    return VersionData.ecogame
end

function p.Version()
    local EcoVersionString = p.GetVersionData().VersionNumber
	return EcoVersionString
end

function p.VersionSL()
    local EcoVersionString = p.GetVersionData().VersionNumber

    if EcoVersionString then
        return '[https://steamcommunity.com/app/382310/announcements/ ' .. EcoVersionString .. ']'
    else
        return "<span style=\"color:red\">ERROR: Failed to load Eco version from Data.</span>"
    end
end

function p.FullVersion()
	local EcoVersion = {}
	local FullVersionList = {}
	local VersionNumberList = {}
	local VersionList = {}
	local FullVersion = p.GetVersionData().FullVersion
	EcoVersion["Version"] = p.GetVersionData().VersionNumber

	if FullVersion then
		for part in string.gmatch(FullVersion, "[^%s]+") do
			table.insert(FullVersionList, part)
		end
	end

	if FullVersionList[1] then
		for part in string.gmatch(FullVersionList[1], "[^%.]+") do
			table.insert(VersionNumberList, part)
		end
	end

	EcoVersion["Number"] = VersionNumberList[2]

	if FullVersionList[3] then
		for part in string.gmatch(FullVersionList[3], "[^%-]+") do
			table.insert(VersionList, part)
		end
	end

	EcoVersion["Iteration"] = FullVersionList[2]
	EcoVersion["Branch"] = VersionList[1]
	EcoVersion["Build"] = VersionList[2]
		
	return EcoVersion
end

return p