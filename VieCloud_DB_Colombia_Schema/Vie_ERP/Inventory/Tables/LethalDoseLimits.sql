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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida (FK a InventoryMeasurementUnit) para la concentración letal acumulada en 24 horas; tipo INT, requerido para toxicología y farmacocinética.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Lethal24HourConcentrationUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida parametrizada en InventoryMeasurementUnit para la concentración letal por en 24 horas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Lethal24HourConcentrationUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Lethal24HourConcentrationUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración letal total acumulada en período de 24 horas; DECIMAL(6,2), umbral de toxicidad por exposición prolongada en medicamentos, vacunas, toxinas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Lethal24HourConcentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentración letal en 24 horas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Lethal24HourConcentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Lethal24HourConcentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida (FK a InventoryMeasurementUnit) para la concentración letal por dosis individual; tipo INT, requerido.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'LethalDoseConcentrationUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida parametrizada en InventoryMeasurementUnit para la concentración letal por dosis', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'LethalDoseConcentrationUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'LethalDoseConcentrationUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración letal por dosis única de medicamento o sustancia; DECIMAL(12,2), límite crítico de seguridad farmacéutica y toxicología clínica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'LethalDoseConcentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentración letal por dosis', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'LethalDoseConcentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'LethalDoseConcentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida (FK a InventoryMeasurementUnit) para el límite máximo permitido en 24 horas; tipo INT, requerido para control de farmacovigilancia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Max24HourConcentrationUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida parametrizada en InventoryMeasurementUnit para la concentración máxima por 24 horas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Max24HourConcentrationUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Max24HourConcentrationUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración máxima permitida acumulada en 24 horas; DECIMAL(12,2), límite seguro de exposición diaria en tratamientos farmacológicos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Max24HourConcentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentración máxima por 24 horas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Max24HourConcentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Max24HourConcentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida (FK a InventoryMeasurementUnit) para el límite máximo por dosis; tipo INT, requerido para prescripción segura.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'MaxDoseConcentrationUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida parametrizada en InventoryMeasurementUnit para la concentración máxima por dosis', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'MaxDoseConcentrationUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'MaxDoseConcentrationUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración máxima permitida por dosis individual; DECIMAL(12,2), límite seguro por administración en protocolos clínicos y recetas médicas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'MaxDoseConcentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentración máxima por dosis', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'MaxDoseConcentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'MaxDoseConcentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida del peso final del rango; tipo INT con valores: 1=miligramos, 2=gramos, 3=kilogramos; aplica a pacientes pediátricos y adultos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndWeightUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida del rango final del peso                   1-miligramos                   2-gramos                   3-kilogramos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndWeightUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndWeightUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Límite superior del rango de peso del paciente; DECIMAL(5,2), define el rango máximo para aplicar dosis letales y máximas en pediatría y medicina.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndWeight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rango final del peso', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndWeight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndWeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida del peso inicial del rango; tipo INT con valores: 1=miligramos, 2=gramos, 3=kilogramos; grupo etario según peso corporal.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartWeightUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida del rango inicial del peso                   1-miligramos                   2-gramos                   3-kilogramos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartWeightUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartWeightUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Límite inferior del rango de peso del paciente; DECIMAL(5,2), define el rango mínimo para cálculo de dosis según peso en pediatría.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartWeight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rango inicial del peso', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartWeight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartWeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la edad final del rango; tipo INT con valores: 1=días, 2=meses, 3=años; segmentación de grupos etarios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndAgeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida del rango final de la edad                   1-Dias                   2-Mes                   3-Año', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndAgeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndAgeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Límite superior del rango de edad; tipo INT, define el grupo etario máximo para aplicar límites de dosis letal y máxima en pediatría y geriatría.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rango final de edad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'EndAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la edad inicial del rango; tipo INT con valores: 1=días, 2=meses, 3=años; neonatos, lactantes, niños, adultos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartAgeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida del rango inicial de la edad                   1-Dias                   2-Mes                   3-Año', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartAgeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartAgeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Límite inferior del rango de edad; tipo INT, define el grupo etario mínimo para los límites de dosis según edad del paciente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rango inicial de edad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'StartAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de límites de dosis letal y dosis máxima por medicamento/DCI; tipo INT IDENTITY, clave primaria para auditoría farmacéutica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro de dosis letal o maxima', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Límites de dosis letales y máximas permitidas de medicamentos según rangos de edad y peso del paciente. Permite controlar que la administración de un principio activo no supere las concentraciones seguras o letales definidas clínicamente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del principio activo (DCI: Denominación Común Internacional) al que aplican estos límites de dosis; referencia el medicamento genérico controlado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'DCIId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LethalDoseLimits', @level2type = N'COLUMN', @level2name = N'DCIId';
