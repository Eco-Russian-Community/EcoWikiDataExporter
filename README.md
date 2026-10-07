# EcoWikiDataExporter
Data Exporter for Wiki from Eco

## How it works

1) EcoWikiDataExporter it is Eco server plugin (C#) that dumps live game data into strict JSON
2) JSON Data files Upload to central Eco Wiki store for use in all 

## Repository structure

| Path | Description |
| --- | --- |
| `EcoWikiDataExporter/` | C# source of the Eco server plugin (.NET 10, `Eco.ReferenceAssemblies`, Newtonsoft.Json). |
| `EcoWiki_Lua_Modules/` | MediaWiki (Scribunto) Lua modules used by the wiki. |
| `EcoWiki_Lua_Modules/Data/` | Exported JSON data files. |

## Localization

Data files are strict JSON with localized fields carried.
Supported languages: English, Russian, German, French, Japanese.

## Getting started

