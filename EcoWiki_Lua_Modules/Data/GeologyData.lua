-- Eco Version : 0.14.1.1
-- EWDE Version : 0.8.0.0
-- Date of export : 06.09.2026 16:23:12

return {
    geology = {
        ['ColdCoast'] = {
                    ['SandBlock'] =  {
                            ['DepthRangeMin'] = '5',
                            ['DepthRangeMax'] = '15',
                            },
                    ['LimestoneBlock'] =  {
                            ['DepthRangeMin'] = '18',
                            ['DepthRangeMax'] = '30',
                            },
                    ['SandstoneBlock'] =  {
                            ['DepthRangeMin'] = '28',
                            ['DepthRangeMax'] = '40',
                            ['LayerResources'] =  {
                                            ['IronOreBlock'] = {
                                                    ['LayerResourceType'] = 'Deposit',
                                                    ['DepthRangeMin'] = '34',
                                                    ['DepthRangeMax'] = '37',
                                                    ['DepositDepthRangeMin'] = '28',
                                                    ['DepositDepthRangeMax'] = '40',
                                                    ['BlocksCountRangeMin'] = '25',
                                                    ['BlocksCountRangeMax'] = '150',
                                                    ['SpawnAtLeastOne'] = 'False',
                                                    ['PercentChance'] = '0.03',
                                            },
                                            }
,
                            },
                    ['BasaltBlock'] =  {
                            ['DepthRangeMin'] = '55',
                            ['DepthRangeMax'] = '75',
                            },
                    ['GneissBlock'] =  {
                            ['DepthRangeMin'] = '70',
                            ['DepthRangeMax'] = '100',
                            },
        },
        ['ColdForest'] = {
                    ['DirtBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '0',
                            ['LayerResources'] =  {
                                            ['RockySoilBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '200',
                                                    ['PercentChance'] = '5',
                                            },
                                            }
,
                            },
                    ['RockySoilBlock'] =  {
                            ['DepthRangeMin'] = '1',
                            ['DepthRangeMax'] = '3',
                            },
                    ['BasaltBlock'] =  {
                            ['DepthRangeMin'] = '5',
                            ['DepthRangeMax'] = '15',
                            ['LayerResources'] =  {
                                            ['CrushedBasaltBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '2',
                                                    ['DepthRangeMax'] = '2',
                                                    ['PercentChance'] = '70',
                                            },
                                            }
,
                            },
                    ['GraniteBlock'] =  {
                            ['DepthRangeMin'] = '55',
                            ['DepthRangeMax'] = '65',
                            ['LayerResources'] =  {
                                            ['GoldOreBlock'] = {
                                                    ['LayerResourceType'] = 'Deposit',
                                                    ['DepthRangeMin'] = '35',
                                                    ['DepthRangeMax'] = '45',
                                                    ['DepositDepthRangeMin'] = '0',
                                                    ['DepositDepthRangeMax'] = '45',
                                                    ['BlocksCountRangeMin'] = '5',
                                                    ['BlocksCountRangeMax'] = '50',
                                                    ['SpawnAtLeastOne'] = 'False',
                                                    ['PercentChance'] = '0.1',
                                            },
                                            }
,
                            },
                    ['GneissBlock'] =  {
                            ['DepthRangeMin'] = '70',
                            ['DepthRangeMax'] = '100',
                            },
        },
        ['DeepOcean'] = {
                    ['DirtBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '0',
                            },
                    ['SandBlock'] =  {
                            ['DepthRangeMin'] = '2',
                            ['DepthRangeMax'] = '10',
                            },
                    ['BasaltBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '100',
                            ['LayerResources'] =  {
                                            ['CrushedBasaltBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '1',
                                                    ['PercentChance'] = '10',
                                            },
                                            }
,
                            },
        },
        ['Desert'] = {
                    ['DesertSandBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '0',
                            },
                    ['SandBlock'] =  {
                            ['DepthRangeMin'] = '1',
                            ['DepthRangeMax'] = '20',
                            ['LayerResources'] =  {
                                            ['CrushedIronOreBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '19',
                                                    ['DepthRangeMax'] = '20',
                                                    ['PercentChance'] = '30',
                                            },
                                            }
,
                            },
                    ['SandstoneBlock'] =  {
                            ['DepthRangeMin'] = '55',
                            ['DepthRangeMax'] = '60',
                            ['LayerResources'] =  {
                                            ['IronOreBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '200',
                                                    ['PercentChance'] = '1',
                                            },
                                            }
,
                            },
                    ['GneissBlock'] =  {
                            ['DepthRangeMin'] = '58',
                            ['DepthRangeMax'] = '65',
                            },
        },
        ['Grassland'] = {
                    ['DirtBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '0',
                            ['LayerResources'] =  {
                                            ['RockySoilBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '200',
                                                    ['PercentChance'] = '5',
                                            },
                                            }
,
                            },
                    ['RockySoilBlock'] =  {
                            ['DepthRangeMin'] = '2',
                            ['DepthRangeMax'] = '6',
                            ['LayerResources'] =  {
                                            ['PeatBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '2',
                                                    ['DepthRangeMax'] = '6',
                                                    ['PercentChance'] = '25',
                                            },
                                            }
,
                            },
                    ['LimestoneBlock'] =  {
                            ['DepthRangeMin'] = '2',
                            ['DepthRangeMax'] = '6',
                            ['LayerResources'] =  {
                                            ['CrushedSulfurBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '1',
                                                    ['PercentChance'] = '6',
                                            },
                                            }
,
                            },
                    ['SandstoneBlock'] =  {
                            ['DepthRangeMin'] = '55',
                            ['DepthRangeMax'] = '60',
                            ['LayerResources'] =  {
                                            ['EmptyBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '25',
                                                    ['DepthRangeMax'] = '70',
                                                    ['PercentChance'] = '5',
                                            },
                                            }
,
                            },
                    ['GraniteBlock'] =  {
                            ['DepthRangeMin'] = '60',
                            ['DepthRangeMax'] = '80',
                            ['LayerResources'] =  {
                                            ['CrushedGraniteBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '0',
                                                    ['PercentChance'] = '40',
                                            },
                                            }
,
                            },
                    ['GneissBlock'] =  {
                            ['DepthRangeMin'] = '70',
                            ['DepthRangeMax'] = '100',
                            },
        },
        ['Ice'] = {
                    ['IceBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '8',
                            },
                    ['BasaltBlock'] =  {
                            ['DepthRangeMin'] = '10',
                            ['DepthRangeMax'] = '30',
                            ['LayerResources'] =  {
                                            ['CrushedBasaltBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '1',
                                                    ['PercentChance'] = '80',
                                            },
                                            }
,
                            },
                    ['GraniteBlock'] =  {
                            ['DepthRangeMin'] = '60',
                            ['DepthRangeMax'] = '75',
                            ['LayerResources'] =  {
                                            ['CopperOreBlock'] = {
                                                    ['LayerResourceType'] = 'Deposit',
                                                    ['DepthRangeMin'] = '50',
                                                    ['DepthRangeMax'] = '60',
                                                    ['DepositDepthRangeMin'] = '0',
                                                    ['DepositDepthRangeMax'] = '60',
                                                    ['BlocksCountRangeMin'] = '60',
                                                    ['BlocksCountRangeMax'] = '200',
                                                    ['SpawnAtLeastOne'] = 'True',
                                                    ['PercentChance'] = '0.1',
                                            },
                                            }
,
                            },
                    ['GneissBlock'] =  {
                            ['DepthRangeMin'] = '70',
                            ['DepthRangeMax'] = '100',
                            },
        },
        ['Ocean'] = {
                    ['DirtBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '0',
                            },
                    ['SandBlock'] =  {
                            ['DepthRangeMin'] = '4',
                            ['DepthRangeMax'] = '15',
                            },
                    ['LimestoneBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '15',
                            ['LayerResources'] =  {
                                            ['CrushedLimestoneBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '0',
                                                    ['PercentChance'] = '30',
                                            },
                                            }
,
                            },
                    ['BasaltBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '100',
                            ['LayerResources'] =  {
                                            ['CrushedBasaltBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '1',
                                                    ['PercentChance'] = '30',
                                            },
                                            }
,
                            },
        },
        ['RainForest'] = {
                    ['DirtBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '0',
                            ['LayerResources'] =  {
                                            ['RockySoilBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '200',
                                                    ['PercentChance'] = '5',
                                            },
                                            }
,
                            },
                    ['ClayBlock'] =  {
                            ['DepthRangeMin'] = '1',
                            ['DepthRangeMax'] = '30',
                            ['LayerResources'] =  {
                                            ['GoldOreBlock'] = {
                                                    ['LayerResourceType'] = 'Deposit',
                                                    ['DepthRangeMin'] = '-1',
                                                    ['DepthRangeMax'] = '3',
                                                    ['DepositDepthRangeMin'] = '-1',
                                                    ['DepositDepthRangeMax'] = '3',
                                                    ['BlocksCountRangeMin'] = '3',
                                                    ['BlocksCountRangeMax'] = '8',
                                                    ['SpawnAtLeastOne'] = 'False',
                                                    ['PercentChance'] = '0.1',
                                            },
                                            }
,
                            },
                    ['ShaleBlock'] =  {
                            ['DepthRangeMin'] = '40',
                            ['DepthRangeMax'] = '45',
                            ['LayerResources'] =  {
                                            ['CoalBlock'] = {
                                                    ['LayerResourceType'] = 'Deposit',
                                                    ['DepthRangeMin'] = '40',
                                                    ['DepthRangeMax'] = '41',
                                                    ['DepositDepthRangeMin'] = '5',
                                                    ['DepositDepthRangeMax'] = '41',
                                                    ['BlocksCountRangeMin'] = '15',
                                                    ['BlocksCountRangeMax'] = '100',
                                                    ['SpawnAtLeastOne'] = 'False',
                                                    ['PercentChance'] = '0.1',
                                            },
                                            }
,
                            },
                    ['GraniteBlock'] =  {
                            ['DepthRangeMin'] = '60',
                            ['DepthRangeMax'] = '70',
                            ['LayerResources'] =  {
                                            ['GoldOreBlock'] = {
                                                    ['LayerResourceType'] = 'Deposit',
                                                    ['DepthRangeMin'] = '60',
                                                    ['DepthRangeMax'] = '80',
                                                    ['DepositDepthRangeMin'] = '0',
                                                    ['DepositDepthRangeMax'] = '80',
                                                    ['BlocksCountRangeMin'] = '5',
                                                    ['BlocksCountRangeMax'] = '25',
                                                    ['SpawnAtLeastOne'] = 'True',
                                                    ['PercentChance'] = '0.5',
                                            },
                                            }
,
                            },
                    ['GneissBlock'] =  {
                            ['DepthRangeMin'] = '65',
                            ['DepthRangeMax'] = '100',
                            },
        },
        ['Taiga'] = {
                    ['DirtBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '0',
                            ['LayerResources'] =  {
                                            ['RockySoilBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '200',
                                                    ['PercentChance'] = '10',
                                            },
                                            }
,
                            },
                    ['RockySoilBlock'] =  {
                            ['DepthRangeMin'] = '1',
                            ['DepthRangeMax'] = '3',
                            },
                    ['BasaltBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '20',
                            ['LayerResources'] =  {
                                            ['CrushedBasaltBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '1',
                                                    ['PercentChance'] = '50',
                                            },
                                            }
,
                            },
                    ['GraniteBlock'] =  {
                            ['DepthRangeMin'] = '65',
                            ['DepthRangeMax'] = '70',
                            ['LayerResources'] =  {
                                            ['GoldOreBlock'] = {
                                                    ['LayerResourceType'] = 'Deposit',
                                                    ['DepthRangeMin'] = '65',
                                                    ['DepthRangeMax'] = '75',
                                                    ['DepositDepthRangeMin'] = '0',
                                                    ['DepositDepthRangeMax'] = '75',
                                                    ['BlocksCountRangeMin'] = '3',
                                                    ['BlocksCountRangeMax'] = '15',
                                                    ['SpawnAtLeastOne'] = 'True',
                                                    ['PercentChance'] = '0.2',
                                            },
                                            }
,
                            },
                    ['GneissBlock'] =  {
                            ['DepthRangeMin'] = '70',
                            ['DepthRangeMax'] = '120',
                            },
        },
        ['Tundra'] = {
                    ['DirtBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '0',
                            ['LayerResources'] =  {
                                            ['SnowBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '200',
                                                    ['PercentChance'] = '20',
                                            },
                                            }
,
                            },
                    ['RockySoilBlock'] =  {
                            ['DepthRangeMin'] = '1',
                            ['DepthRangeMax'] = '3',
                            ['LayerResources'] =  {
                                            ['FrozenSoilBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '200',
                                                    ['PercentChance'] = '10',
                                            },
                                            }
,
                            },
                    ['BasaltBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '30',
                            ['LayerResources'] =  {
                                            ['CrushedBasaltBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '1',
                                                    ['PercentChance'] = '40',
                                            },
                                            }
,
                            },
                    ['GraniteBlock'] =  {
                            ['DepthRangeMin'] = '65',
                            ['DepthRangeMax'] = '75',
                            ['LayerResources'] =  {
                                            ['SulfurBlock'] = {
                                                    ['LayerResourceType'] = 'Deposit',
                                                    ['DepthRangeMin'] = '30',
                                                    ['DepthRangeMax'] = '100',
                                                    ['DepositDepthRangeMin'] = '0',
                                                    ['DepositDepthRangeMax'] = '100',
                                                    ['BlocksCountRangeMin'] = '15',
                                                    ['BlocksCountRangeMax'] = '68',
                                                    ['SpawnAtLeastOne'] = 'True',
                                                    ['PercentChance'] = '0.3',
                                            },
                                            }
,
                            },
                    ['GneissBlock'] =  {
                            ['DepthRangeMin'] = '70',
                            ['DepthRangeMax'] = '85',
                            },
        },
        ['WarmCoast'] = {
                    ['SandBlock'] =  {
                            ['DepthRangeMin'] = '4',
                            ['DepthRangeMax'] = '15',
                            },
                    ['LimestoneBlock'] =  {
                            ['DepthRangeMin'] = '18',
                            ['DepthRangeMax'] = '30',
                            },
                    ['SandstoneBlock'] =  {
                            ['DepthRangeMin'] = '37',
                            ['DepthRangeMax'] = '50',
                            ['LayerResources'] =  {
                                            ['IronOreBlock'] = {
                                                    ['LayerResourceType'] = 'Deposit',
                                                    ['DepthRangeMin'] = '31',
                                                    ['DepthRangeMax'] = '36',
                                                    ['DepositDepthRangeMin'] = '28',
                                                    ['DepositDepthRangeMax'] = '40',
                                                    ['BlocksCountRangeMin'] = '20',
                                                    ['BlocksCountRangeMax'] = '500',
                                                    ['SpawnAtLeastOne'] = 'True',
                                                    ['PercentChance'] = '0.01',
                                            },
                                            }
,
                            },
                    ['GneissBlock'] =  {
                            ['DepthRangeMin'] = '50',
                            ['DepthRangeMax'] = '55',
                            },
                    ['BasaltBlock'] =  {
                            ['DepthRangeMin'] = '55',
                            ['DepthRangeMax'] = '60',
                            },
        },
        ['WarmForest'] = {
                    ['DirtBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '0',
                            ['LayerResources'] =  {
                                            ['RockySoilBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '200',
                                                    ['PercentChance'] = '5',
                                            },
                                            }
,
                            },
                    ['RockySoilBlock'] =  {
                            ['DepthRangeMin'] = '6',
                            ['DepthRangeMax'] = '15',
                            ['LayerResources'] =  {
                                            ['ClayBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '2',
                                                    ['DepthRangeMax'] = '15',
                                                    ['PercentChance'] = '100',
                                            },
                                            }
,
                            },
                    ['BasaltBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '20',
                            ['LayerResources'] =  {
                                            ['CrushedBasaltBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '1',
                                                    ['PercentChance'] = '50',
                                            },
                                            }
,
                            },
                    ['GraniteBlock'] =  {
                            ['DepthRangeMin'] = '55',
                            ['DepthRangeMax'] = '65',
                            ['LayerResources'] =  {
                                            ['SulfurBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '20',
                                                    ['DepthRangeMax'] = '24',
                                                    ['PercentChance'] = '50',
                                            },
                                            }
,
                            },
                    ['GneissBlock'] =  {
                            ['DepthRangeMin'] = '70',
                            ['DepthRangeMax'] = '100',
                            },
        },
        ['Wetland'] = {
                    ['WetlandsSoilBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '0',
                            },
                    ['PeatBlock'] =  {
                            ['DepthRangeMin'] = '4',
                            ['DepthRangeMax'] = '6',
                            ['LayerResources'] =  {
                                            ['DirtBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '3',
                                                    ['PercentChance'] = '100',
                                            },
                                            }
,
                            },
                    ['ClayBlock'] =  {
                            ['DepthRangeMin'] = '1',
                            ['DepthRangeMax'] = '12',
                            ['LayerResources'] =  {
                                            ['CrushedCoalBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '19',
                                                    ['DepthRangeMax'] = '20',
                                                    ['PercentChance'] = '30',
                                            },
                                            }
,
                            },
                    ['DirtBlock'] =  {
                            ['DepthRangeMin'] = '0',
                            ['DepthRangeMax'] = '20',
                            ['LayerResources'] =  {
                                            ['CoalBlock'] = {
                                                    ['LayerResourceType'] = 'Deposit',
                                                    ['DepthRangeMin'] = '0',
                                                    ['DepthRangeMax'] = '1',
                                                    ['DepositDepthRangeMin'] = '-2',
                                                    ['DepositDepthRangeMax'] = '1',
                                                    ['BlocksCountRangeMin'] = '8',
                                                    ['BlocksCountRangeMax'] = '15',
                                                    ['SpawnAtLeastOne'] = 'False',
                                                    ['PercentChance'] = '2',
                                            },
                                            }
,
                            },
                    ['ShaleBlock'] =  {
                            ['DepthRangeMin'] = '55',
                            ['DepthRangeMax'] = '60',
                            ['LayerResources'] =  {
                                            ['GraniteBlock'] = {
                                                    ['LayerResourceType'] = 'Standard',
                                                    ['DepthRangeMin'] = '45',
                                                    ['DepthRangeMax'] = '60',
                                                    ['PercentChance'] = '80',
                                            },
                                            }
,
                            },
                    ['GneissBlock'] =  {
                            ['DepthRangeMin'] = '58',
                            ['DepthRangeMax'] = '65',
                            },
        },
    },
}