CREATE TABLE [Cost].[CostInventoryGroupDetail] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostInventoryGroupId] INT             NOT NULL,
    [InventoryProductId]   INT             NOT NULL,
    [Quantity]             DECIMAL (24, 6) NOT NULL,
    CONSTRAINT [PK_CostInventoryGroupDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostInventoryGroupDetail_CostInventoryGroup] FOREIGN KEY ([CostInventoryGroupId]) REFERENCES [Cost].[CostInventoryGroup] ([Id]),
    CONSTRAINT [FK_CostInventoryGroupDetail_InventoryProduct] FOREIGN KEY ([InventoryProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [Cost].[CostInventoryGroupDetail] NOCHECK CONSTRAINT [FK_CostInventoryGroupDetail_CostInventoryGroup];


GO
ALTER TABLE [Cost].[CostInventoryGroupDetail] NOCHECK CONSTRAINT [FK_CostInventoryGroupDetail_InventoryProduct];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Producto', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'InventoryProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Grupo de Productos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'CostInventoryGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'Id';

