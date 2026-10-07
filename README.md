# EcoWikiDataExporter
EcoWikiDataExporter is a data processing pipeline for Eco Wiki that synchronizes Eco game data with the wiki, eliminating the need to manually edit numerous pages.

## How it works

1) EcoWikiDataExporter it is Eco server plugin (C#) that dumps live game data into strict JSON;
2) JSON Data files Upload to central Eco Wiki store for use in all other Eco wikis;
3) Lua modules read JSON Data files with mw.ext.data.get and mw.loadData for Wiki Pages;
4) Lua Util modules handle data processing and interlinking to provide comprehensive data output in the form of tables and lists.

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

**Prerequisites:** .NET 10 SDK, a local Eco server installation matching the
`Eco.ReferenceAssemblies` version referenced by the project.

1. Build the solution from `EcoWikiDataExporter.sln` in configuration `Release`.
2. Drop the resulting plugin assembly (EcoWikiDataExporter.dll) into the Eco server's `Mods` folder and start the server.
3. Run the **"Export Wiki Data"** plugin command (or enable the debug auto-export) to produce the `EWDE` folder with JSON files.
4. Publish the JSON files to the wiki's `Data:` namespace.
5. Import the modules from `EcoWiki_Lua_Modules/` into the wiki's `Module:` namespace (Scribunto must be enabled).

## Project links

* Wiki: <https://wiki.play.eco/>
* Eco site <https://play.eco/>
* Eco Localization Portal <https://localization.play.eco/>


   
