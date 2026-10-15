CREATE TABLE [Inventory].[LethalDoseLimits] (
    [Id]                              INT             IDENTITY (1, 1) NOT NULL,
    [StartAge]                        INT             NOT NULL,
    [StartAgeUnit]                    INT             NOT NULL,
    [EndAge]                          INT             NOT NULL,
    [EndAgeUnit]                      INT             NOT NULL,
    [StartWeight]                     DECIMAL (5, 2)  NULL,
    [StartWeightUnit]                 INT             NULL,
    [EndWeight]                       DECIMAL (5, 2)  NULL,
    [EndWeightUnit]                   INT             NULL,
    [MaxDoseConcentration]            DECIMAL (12, 2) NOT NULL,
    [MaxDoseConcentrationUnitId]      INT             NOT NULL,
    [Max24HourConcentration]          DECIMAL (12, 2) NOT NULL,
    [Max24HourConcentrationUnitId]    INT             NOT NULL,
    [LethalDoseConcentration]         DECIMAL (12, 2) NOT NULL,
    [LethalDoseConcentrationUnitId]   INT             NOT NULL,
    [Lethal24HourConcentration]       DECIMAL (6, 2)  NULL,
    [Lethal24HourConcentrationUnitId] INT             NULL,
    [DCIId]                           INT             NOT NULL,
    CONSTRAINT [PK_LethalDoseLimits_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LethalDoseLimits_DCI] FOREIGN KEY ([DCIId]) REFERENCES [Inventory].[DCI] ([Id]),
    CONSTRAINT [FK_LethalDoseLimits_Lethal24HourConcentrationUnit] FOREIGN KEY ([Lethal24HourConcentrationUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_LethalDoseLimits_LethalDoseConcentrationUnit] FOREIGN KEY ([LethalDoseConcentrationUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_LethalDoseLimits_Max24HourConcentrationUnit] FOREIGN KEY ([Max24HourConcentrationUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_LethalDoseLimits_MaxDoseConcentrationUnit] FOREIGN KEY ([MaxDoseConcentrationUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida parametrizada en InventoryMeasurementUnit para la concentración letal por en 24 horas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Lethal24HourConcentrationUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentración letal en 24 horas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Lethal24HourConcentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida parametrizada en InventoryMeasurementUnit para la concentración letal por dosis', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'LethalDoseConcentrationUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentración letal por dosis', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'LethalDoseConcentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida parametrizada en InventoryMeasurementUnit para la concentración máxima por 24 horas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Max24HourConcentrationUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentración máxima por 24 horas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Max24HourConcentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida parametrizada en InventoryMeasurementUnit para la concentración máxima por dosis', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'MaxDoseConcentrationUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentración máxima por dosis', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'MaxDoseConcentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida del rango final del peso                   1-miligramos                   2-gramos                   3-kilogramos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndWeightUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rango final del peso', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndWeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida del rango inicial del peso                   1-miligramos                   2-gramos                   3-kilogramos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartWeightUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rango inicial del peso', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartWeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida del rango final de la edad                   1-Dias                   2-Mes                   3-Año', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndAgeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rango final de edad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida del rango inicial de la edad                   1-Dias                   2-Mes                   3-Año', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartAgeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rango inicial de edad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro de dosis letal o maxima', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Id';

