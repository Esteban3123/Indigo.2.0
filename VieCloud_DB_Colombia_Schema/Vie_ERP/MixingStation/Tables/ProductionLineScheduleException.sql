CREATE TABLE [MixingStation].[ProductionLineScheduleException] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductionLineId] INT           NOT NULL,
    [StopDate]         DATETIME      NOT NULL,
    [ReasonForStop]    VARCHAR (200) NOT NULL,
    CONSTRAINT [PK__ProductionLineScheduleException] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionLineScheduleException_ProductionLineId] FOREIGN KEY ([ProductionLineId]) REFERENCES [MixingStation].[ProductionLine] ([Id])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_ProductionLineScheduleException_ProductionLineId]
    ON [MixingStation].[ProductionLineScheduleException]([ProductionLineId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo, causa o razón por la cual la línea de producción se detiene o no labora (ej: mantenimiento, falla técnica, paro, evento externo). VARCHAR(200). Búsqueda: parada, detención, incidencia, causa de parada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'ReasonForStop';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo por el cual no se labora', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'ReasonForStop';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'ReasonForStop';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que inicia la parada o detención de labores de la línea de producción. DATETIME. Búsqueda: fecha de paro, fecha de detención, cuándo se paró la línea.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'StopDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que no se labora', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'StopDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'StopDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave foránea) de la línea de producción asociada a esta excepción de programación. INT. Referencia FK a [MixingStation].[ProductionLine]([Id]). Búsqueda: línea de producción, línea afectada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'ProductionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de línea de producción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'ProductionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'ProductionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la excepción de programación en la estación de mezcla. INT IDENTITY(1,1), clave primaria. Búsqueda: ID del registro, excepción.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Excepciones o paros programados en las líneas de producción de la estación de mezcla. Registra los días en que una línea de producción no operará y el motivo del paro o interrupción.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException';
