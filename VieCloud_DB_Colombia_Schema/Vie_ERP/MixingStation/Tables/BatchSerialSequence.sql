CREATE TABLE [MixingStation].[BatchSerialSequence] (
    [Id]                   INT      IDENTITY (1, 1) NOT NULL,
    [BatchSerialSettingId] INT      NOT NULL,
    [Next]                 BIGINT   NOT NULL,
    [SequenceDate]         DATETIME NULL,
    CONSTRAINT [PK_BatchSerialSequence] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BatchSerialSequence_BatchSerialSettingId] FOREIGN KEY ([BatchSerialSettingId]) REFERENCES [MixingStation].[BatchSerialSetting] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación de la secuencia numérica diaria; marca el día en que se inicia el contador de lotes para ese período (DATETIME, nullable, auditoria temporal)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence', @level2type = N'COLUMN', @level2name = N'SequenceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la secuencia numerica, se crea una secuencia por dia', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence', @level2type = N'COLUMN', @level2name = N'SequenceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence', @level2type = N'COLUMN', @level2name = N'SequenceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Siguiente número secuencial a asignar en la serie de lotes; contador BIGINT que incrementa por cada nuevo lote generado en esa secuencia diaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Siguente Secuencia numerica', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la configuración (cabecera) de parámetros del lote; clave foránea (FK) que referencia BatchSerialSetting.Id para vincular reglas y máscaras de numeración', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence', @level2type = N'COLUMN', @level2name = N'BatchSerialSettingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabcera de parametros de lote', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence', @level2type = N'COLUMN', @level2name = N'BatchSerialSettingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence', @level2type = N'COLUMN', @level2name = N'BatchSerialSettingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de secuencias de numeración para lotes y seriales en la estación de mezcla. Controla el consecutivo actual asignado a cada configuración de serie, permitiendo generar números únicos de lote o serial de forma ordenada y sin duplicados.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de secuencia (clave primaria autogenerada).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence', @level2type = N'COLUMN', @level2name = N'Id';
