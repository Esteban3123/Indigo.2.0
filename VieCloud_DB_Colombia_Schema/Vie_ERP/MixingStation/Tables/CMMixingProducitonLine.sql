CREATE TABLE [MixingStation].[CMMixingProducitonLine] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Id_CMConfiguration] INT NOT NULL,
    [Id_ProductionLine]  INT NOT NULL,
    [StatePl]            BIT NULL,
    CONSTRAINT [PK_CMMixingProducitonLine] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMMixingProducitonLine_CMConfiguration] FOREIGN KEY ([Id_CMConfiguration]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [FK_CMMixingProducitonLine_ProductionLine] FOREIGN KEY ([Id_ProductionLine]) REFERENCES [MixingStation].[ProductionLine] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de la línea de producción: 1=Activo, 0=Inactivo. Bit que indica si la asociación entre configuración de mezclado y línea de producción está habilitada o deshabilitada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'StatePl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro: 1:Activo, 0:Inactivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'StatePl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'StatePl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la línea de producción vinculada. Clave foránea que referencia ProductionLine.Id. Identifica qué línea de producción participa en la mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'Id_ProductionLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID linea de produccón', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'Id_ProductionLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'Id_ProductionLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la configuración de mezclado (CM Configuration). Clave foránea que referencia CMConfiguration.Id. Define los parámetros de mezclado aplicados a la línea de producción.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'Id_CMConfiguration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id CM Configuration', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'Id_CMConfiguration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'Id_CMConfiguration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (IDENTITY) único de la tabla CMMixingProducitonLine. Clave primaria que registra cada asociación entre configuración de mezclado y línea de producción.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre configuraciones de la estación de mezcla y las líneas de producción habilitadas. Indica qué líneas de producción están activas o inactivas para cada configuración de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine';
