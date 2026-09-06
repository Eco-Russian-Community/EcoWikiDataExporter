-- Eco Version : 0.14.1.1
-- EWDE Version : 0.8.0.0
-- Date of export : 06.09.2026 16:23:12

return {
    config = {
        ['Balance'] = {
                    ['CalorieMultiplierOnMove'] = '5',
                    ['CraftingQueueQuantity'] = '5',
                    ['MaintenanceDecayPercentage'] = '5',
                    ['VehicleMaintenanceDecayPercentage'] = '10',
                    ['ToolRepairPenalty'] = '0,2',
                    ['ShowOilLayer'] = 'False',
                    ['SpecialtyExperiencePerLevelSquared'] = '25',
                    ['ExperienceGrowthPercentage'] = '1,05',
                    ['ExperienceIncreaseAdditive'] = '5',
                    ['StarsRequiredPerStage'] = '8',
        },
        ['EcoSim'] = {
                    ['PPMPerTon'] = '1,2860082',
                    ['MinCO2ppm'] = '325',
                    ['SeaLevelsRiseAtCO2ppm'] = '420',
                    ['CO2ppmPerSeaLevelMeterRise'] = '20',
                    ['MaxSeaLevelRise'] = '20',
                    ['MaxTemperatureRise'] = '30',
                    ['MaxSeaLevelRiseRatePerDay'] = '1',
                    ['MaxTemperatureChangePerDay'] = '1,5',
                    ['TemperaturesRiseAtCO2ppm'] = '340',
                    ['CO2ppmPerDegreeTemperatureRise'] = '20',
                    ['MaxCO2PerDayFromAnimals'] = '25',
                    ['MinCO2PerDayFromPlants'] = '-500',
                    ['BufferPollutionConvergenceDays'] = '3',
                    ['GroundPollutionGraceDays'] = '1',
                    ['HeavyMineralNaturalDecayCeiling'] = '1',
                    ['ChemicalNaturalDecayCeiling'] = '0,3',
                    ['GroundPollutionSoilRatio'] = '1',
                    ['GroundPollutionHeavyMineralRatio'] = '1',
                    ['GroundPollutionChemicalRatio'] = '1',
                    ['GroundPollutionAcidRainRatio'] = '1',
                    ['MaxDebrisBlocks'] = '1',
                    ['MinTreeSpawnDistance'] = '4',
                    ['BaseSkillGainRate'] = '12',
                    ['TimeOfDayScale'] = '48',
        },
        ['SpeciesLayeredCatch'] = {
                    ['BoatCatcher'] = { 'Salmon', 'Trout', 'Tuna', 'Pacific Sardine', 'Cod', 'Bass', 'Blue Shark', 'Alligator', 'Moon Jellyfish', 'Crab'},
                    ['FishingPoleCatcher'] = { 'Salmon', 'Trout', 'Tuna', 'Pacific Sardine', 'Cod', 'Bass'},
                    ['CrabCatcher'] = { 'Crab'},
                    ['FishCatcher'] = { 'Salmon', 'Trout', 'Tuna', 'Pacific Sardine', 'Cod', 'Bass'},
        },
        ['WorldGenerator'] = {
                    ['WaterLevel'] = '60',
                    ['MaxGenerationHeight'] = '120',
                    ['MaxBuildHeight'] = '160',
        },
    },
}