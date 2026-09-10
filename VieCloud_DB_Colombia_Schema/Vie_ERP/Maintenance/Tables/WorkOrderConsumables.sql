CREATE TABLE [Maintenance].[WorkOrderConsumables] (
    [Id]                   INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [WorkOrderId]          INT NOT NULL,
    [ProtocolConsumableId] INT NOT NULL,
    CONSTRAINT [PK_WorkOrderConsumables__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkOrderConsumables_ProtocolConsumables] FOREIGN KEY ([ProtocolConsumableId]) REFERENCES [Maintenance].[ProtocolConsumables] ([Id]),
    CONSTRAINT [FK_WorkOrderConsumables_WorkOrder] FOREIGN KEY ([WorkOrderId]) REFERENCES [Maintenance].[WorkOrder] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_WorkOrderConsumables]
    ON [Maintenance].[WorkOrderConsumables]([WorkOrderId] ASC, [ProtocolConsumableId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del consumible según protocolo de mantenimiento seleccionado. Referencia a ProtocolConsumables(Id). Tipo INT. Indica qué material, repuesto o insumo se utiliza en la orden de trabajo conforme al protocolo establecido.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderConsumables', @level2type = N'COLUMN', @level2name = N'ProtocolConsumableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del consumible de acuerdo al protocolo seleccionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderConsumables', @level2type = N'COLUMN', @level2name = N'ProtocolConsumableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderConsumables', @level2type = N'COLUMN', @level2name = N'ProtocolConsumableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de trabajo (trabajo, mantenimiento, servicio). Referencia a WorkOrder(Id). Tipo INT. Vincula el consumible a la orden de mantenimiento específica.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderConsumables', @level2type = N'COLUMN', @level2name = N'WorkOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la orden de trabajo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderConsumables', @level2type = N'COLUMN', @level2name = N'WorkOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderConsumables', @level2type = N'COLUMN', @level2name = N'WorkOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) de la relación entre orden de trabajo y consumible. Tipo INT. Clave primaria de la tabla WorkOrderConsumables.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderConsumables', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderConsumables', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderConsumables', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los insumos o consumibles utilizados en cada orden de trabajo de mantenimiento, vinculando la orden con el consumible definido en el protocolo correspondiente.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderConsumables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderConsumables';
