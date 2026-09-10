CREATE TABLE [Maintenance].[WorkOrderSupplies] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [WorkOrderId]      INT NOT NULL,
    [ProtocolSupplyId] INT NOT NULL,
    CONSTRAINT [PK_WorkOrderSupplies__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkOrderSupplies_ProtocolSupplier] FOREIGN KEY ([ProtocolSupplyId]) REFERENCES [Maintenance].[ProtocolSupplier] ([Id]),
    CONSTRAINT [FK_WorkOrderSupplies_WorkOrder] FOREIGN KEY ([WorkOrderId]) REFERENCES [Maintenance].[WorkOrder] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_WorkOrderSupplies]
    ON [Maintenance].[WorkOrderSupplies]([WorkOrderId] ASC, [ProtocolSupplyId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del insumo/material según protocolo de mantenimiento seleccionado. Referencia a tabla ProtocolSupplier para trazabilidad de suministros en órdenes de trabajo. Tipo: INT, FK.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderSupplies', @level2type = N'COLUMN', @level2name = N'ProtocolSupplyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del insumo de acuerdo al protocolo seleccionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderSupplies', @level2type = N'COLUMN', @level2name = N'ProtocolSupplyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderSupplies', @level2type = N'COLUMN', @level2name = N'ProtocolSupplyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de trabajo/mantenimiento. Referencia a tabla WorkOrder. Vincula insumos específicos a cada orden. Tipo: INT, FK.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderSupplies', @level2type = N'COLUMN', @level2name = N'WorkOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la orden de trabajo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderSupplies', @level2type = N'COLUMN', @level2name = N'WorkOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderSupplies', @level2type = N'COLUMN', @level2name = N'WorkOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y autoincrementable de la relación insumo-orden. Tipo: INT IDENTITY. Clave primaria de la tabla WorkOrderSupplies.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderSupplies', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderSupplies', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderSupplies', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los insumos o materiales asignados a cada orden de trabajo de mantenimiento, vinculando la orden con los suministros definidos en el protocolo correspondiente.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderSupplies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderSupplies';
