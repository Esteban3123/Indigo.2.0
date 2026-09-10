CREATE TABLE [Cost].[CostActivityProductionCenter] (
    [Id]                     INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityId]         INT NOT NULL,
    [CostProductionCenterId] INT NOT NULL,
    CONSTRAINT [PK_CostActivityProductionCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityProductionCenter_CostActivity] FOREIGN KEY ([CostActivityId]) REFERENCES [Cost].[CostActivity] ([Id]),
    CONSTRAINT [FK_CostActivityProductionCenter_CostProductionCenter] FOREIGN KEY ([CostProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Centro de Producción (Unidad Funcional de Costo); referencia FK a [Cost].[CostProductionCenter]; INT; vincula actividades a centros de atención, departamentos o unidades operativas para asignación de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityProductionCenter', @level2type = N'COLUMN', @level2name = N'CostProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Centro de Producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityProductionCenter', @level2type = N'COLUMN', @level2name = N'CostProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityProductionCenter', @level2type = N'COLUMN', @level2name = N'CostProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Actividad de Costo; referencia FK a [Cost].[CostActivity]; INT; vincula la actividad (procedimiento, servicio, atención) con su centro de producción asociado para costeo y facturación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityProductionCenter', @level2type = N'COLUMN', @level2name = N'CostActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la Actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityProductionCenter', @level2type = N'COLUMN', @level2name = N'CostActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityProductionCenter', @level2type = N'COLUMN', @level2name = N'CostActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de asociación (Clave Primaria); INT IDENTITY; llave técnica que indexa la relación muchos-a-muchos entre actividades de costo y centros de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityProductionCenter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityProductionCenter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityProductionCenter', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre actividades de costo y centros de producción (o centros de costo). Permite asociar qué actividades de costos pertenecen a qué centro de producción dentro del módulo de costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityProductionCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityProductionCenter';
