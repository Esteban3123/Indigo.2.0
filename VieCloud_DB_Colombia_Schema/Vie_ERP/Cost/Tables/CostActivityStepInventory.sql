CREATE TABLE [Cost].[CostActivityStepInventory] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityStepId]   INT             NOT NULL,
    [CostInventoryGroupId] INT             NOT NULL,
    [Quantity]             DECIMAL (24, 6) NOT NULL,
    CONSTRAINT [PK_CostActivityStepInventory__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityStepInventory_CostActivityStep] FOREIGN KEY ([CostActivityStepId]) REFERENCES [Cost].[CostActivityStep] ([Id]),
    CONSTRAINT [FK_CostActivityStepInventory_CostInventoryGroup] FOREIGN KEY ([CostInventoryGroupId]) REFERENCES [Cost].[CostInventoryGroup] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CostActivityStepInventory__CostActivityStepId__CostInventoryGroupId]
    ON [Cost].[CostActivityStepInventory]([CostActivityStepId] ASC, [CostInventoryGroupId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de productos, insumos o materiales asignados al paso de actividad (DECIMAL 24,6). Volumen, unidades, dosis o porción consumida en el procedimiento o atención.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) del Grupo de Inventario o Productos asociado. Referencia a [Cost].[CostInventoryGroup]. Clasifica insumos, medicamentos, dispositivos médicos o materiales de consumo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'CostInventoryGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Grupo de Productos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'CostInventoryGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'CostInventoryGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) del Paso de Actividad o tarea clínica dentro del proceso de costeo. Referencia a [Cost].[CostActivityStep]. Vincula el consumo de inventario a la fase operativa.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'CostActivityStepId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Paso de la Actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'CostActivityStepId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'CostActivityStepId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de asignación de inventario a paso de actividad. Clave primaria para relación muchos-a-muchos entre actividades y productos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del inventario o insumos asociados a cada paso de una actividad de costos, indicando el grupo de inventario utilizado y la cantidad consumida en dicho paso.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory';
