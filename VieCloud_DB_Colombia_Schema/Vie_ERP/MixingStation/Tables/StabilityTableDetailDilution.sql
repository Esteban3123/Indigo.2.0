CREATE TABLE [MixingStation].[StabilityTableDetailDilution] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [StabilityTableDetailId] INT             NOT NULL,
    [ATCId]                  INT             NOT NULL,
    [ConcentrationMaximum]   DECIMAL (18, 2) NOT NULL,
    [ConcentrationMinimum]   DECIMAL (18, 2) NOT NULL,
    [PhotoProtection]        BIT             NOT NULL,
    [InfusionTime]           INT             NOT NULL,
    [BibliographicReference] VARCHAR (300)   NULL,
    [Container]              VARCHAR (20)    NULL,
    [HourStability]          INT             CONSTRAINT [DF_StabilityTableDetailDilution_HourStability] DEFAULT ((0)) NOT NULL,
    [StorageTemperatureId]   INT             NULL,
    CONSTRAINT [PK_StabilityTableDetailDilution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StabilityTableDetailDilution_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_StabilityTableDetailDilution_StabilityTableDetail] FOREIGN KEY ([StabilityTableDetailId]) REFERENCES [MixingStation].[StabilityTableDetail] ([Id]),
    CONSTRAINT [FK_StabilityTableDetailDilution_StorageTemperature] FOREIGN KEY ([StorageTemperatureId]) REFERENCES [Inventory].[StorageTemperature] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estabilidad en horas. Duración en horas que el medicamento reconstituyente mantiene su eficacia y seguridad tras dilución. INT, rango 0-9999, default 0. Clave para validar tiempo de uso en farmacia clínica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'HourStability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estabilidad en horas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'HourStability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'HourStability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Envase, recipiente o tipo de presentación para almacenaje. VARCHAR(20). Ej: vial, bolsa, jeringa, frasco. Define compatibilidad con solución diluida.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'Container';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Envase', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'Container';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'Container';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia bibliográfica, fuente documental o literatura científica que respalda los parámetros de estabilidad registrados. VARCHAR(300), opcional. Para trazabilidad y validación farmacéutica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'BibliographicReference';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia bibliografica', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'BibliographicReference';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'BibliographicReference';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de infusión en minutos. Duración recomendada para administrar el medicamento diluyente vía intravenosa o infusión. INT, en minutos. Parámetro clínico crítico.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'InfusionTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de infusión en min', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'InfusionTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'InfusionTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protección fotosensible, indicador BIT (0=no requiere, 1=requiere protección luz). Determina si medicamento diluido debe resguardarse de luz solar o artificial durante almacenaje e infusión.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'PhotoProtection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Foto protección', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'PhotoProtection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'PhotoProtection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración mínima del principio activo tras dilución. DECIMAL(18,2). Límite inferior de eficacia farmacológica garantizada para el reconstituyente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'ConcentrationMinimum';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentración minima', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'ConcentrationMinimum';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'ConcentrationMinimum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración máxima del principio activo tras dilución. DECIMAL(18,2). Límite superior de seguridad y tolerabilidad del medicamento diluido.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'ConcentrationMaximum';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentración maxima', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'ConcentrationMaximum';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'ConcentrationMaximum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de tipo reconstituyente o mezcla. INT, FK a StabilityTableDetail. Vincula parámetros de dilución a protocolo de reconstitución específico.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'StabilityTableDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de tipo reconstituyente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'StabilityTableDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'StabilityTableDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de dilución. INT IDENTITY, clave primaria. Referencia para auditoría y control de calidad farmacéutica en MixingStation.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de diluciones asociadas a una tabla de estabilidad de medicamentos en la estación de mezclas; registra concentraciones, fotoprotección, tiempo de infusión, horas de estabilidad y referencia bibliográfica para cada diluyente o solución compatible con un medicamento.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del código ATC (clasificación anatómica, terapéutica y química) del medicamento o principio activo al que aplica esta dilución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la temperatura de almacenamiento requerida para conservar correctamente la dilución preparada (por ejemplo: ambiente, refrigeración, congelación).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'StorageTemperatureId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'StorageTemperatureId';
