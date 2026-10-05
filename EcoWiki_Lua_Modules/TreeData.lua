-- mw.ext.data.get automatically fetches from the configured remote repository
-- Do not include the "Data:" namespace prefix in the filename argument
local data = mw.ext.data.get("Trees.json")
return data