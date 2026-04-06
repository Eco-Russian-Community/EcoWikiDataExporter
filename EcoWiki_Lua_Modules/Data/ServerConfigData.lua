-- Eco Version : 0.13.0.0
-- EWDE Version : 0.7.0.0
-- Date of export : 30.03.2026 19:47:25

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
                    ['PollutionPerTailingPerTick'] = '0,016',
                    ['TailingsPollutionUndergroundHalvingDistance'] = '5',
                    ['MaxDebrisBlocks'] = '1',
                    ['MinTreeSpawnDistance'] = '4',
                    ['BaseSkillGainRate'] = '12',
                    ['TimeOfDayScale'] = '48',
        },
        ['WorldGenerator'] = {
                    ['WaterLevel'] = '60',
                    ['MaxGenerationHeight'] = '120',
                    ['MaxBuildHeight'] = '160',
        },
    },
}