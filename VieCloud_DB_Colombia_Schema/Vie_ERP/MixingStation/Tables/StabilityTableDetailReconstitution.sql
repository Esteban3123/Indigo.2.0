CREATE TABLE [MixingStation].[StabilityTableDetailReconstitution] (
    [Id]                     INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [StabilityTableDetailId] INT            NOT NULL,
    [ATCId]                  INT            NOT NULL,
    [Volume]                 DECIMAL (5, 2) NOT NULL,
    [HourStability]          INT            NOT NULL,
    [Observations]           VARCHAR (1000) NULL,
    [StorageTemperatureId]   INT            NULL,
    CONSTRAINT [PK_StabilityTableDetailReconstitution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StabilityTableDetailReconstitution_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_StabilityTableDetailReconstitution_StabilityTableDetail] FOREIGN KEY ([StabilityTableDetailId]) REFERENCES [MixingStation].[StabilityTableDetail] ([Id]),
    CONSTRAINT [FK_StabilityTableDetailReconstitution_StorageTemperature] FOREIGN KEY ([StorageTemperatureId]) REFERENCES [Inventory].[StorageTemperature] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estabilidad en horas, tiempo máximo de viabilidad del medicamento reconstituido a temperatura controlada (INT, dominio farmacéutico)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'HourStability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estabilidad en horas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'HourStability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'HourStability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen en mililitros (mL) o unidad de medida del medicamento reconstituido (DECIMAL 5,2, parámetro de preparación)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'Volume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'Volume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'Volume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de referencia al detalle de tabla de estabilidad, vinculación con perfil de reconstitucion del medicamento (FK a StabilityTableDetail)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'StabilityTableDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'StabilityTableDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'StabilityTableDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de estabilidad en reconstitución, clave primaria (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de reconstitución dentro de las tablas de estabilidad de medicamentos en la estación de mezclas; registra para cada medicamento el volumen de reconstitución, las horas de estabilidad válidas y las condiciones de almacenamiento recomendadas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código ATC (Anatomical Therapeutic Chemical) del medicamento o principio activo al que aplica esta condición de reconstitución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas adicionales sobre el proceso de reconstitución, condiciones especiales o advertencias para el preparador.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura de almacenamiento requerida para conservar el medicamento reconstituido (por ejemplo: ambiente, refrigeración, congelación).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'StorageTemperatureId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'StorageTemperatureId';
