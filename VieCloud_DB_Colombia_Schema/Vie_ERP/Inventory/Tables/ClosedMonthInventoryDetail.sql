CREATE TABLE [Inventory].[ClosedMonthInventoryDetail] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Quantity]               INT             NOT NULL,
    [WarehouseId]            INT             NOT NULL,
    [BatchSerialId]          INT             NULL,
    [ClosedMonthInventoryId] INT             NOT NULL,
    [CostTotal]              DECIMAL (18, 2) CONSTRAINT [DF__ClosedMon__CostT__45C97A6B] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Inventory_ClosedMonthInventoryDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthInventoryDetail_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_ClosedMonthInventoryDetail_ClosedMonthInventory] FOREIGN KEY ([ClosedMonthInventoryId]) REFERENCES [Inventory].[ClosedMonthInventory] ([Id]),
    CONSTRAINT [FK_ClosedMonthInventoryDetail_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[ClosedMonthInventoryDetail] NOCHECK CONSTRAINT [FK_ClosedMonthInventoryDetail_ClosedMonthInventory];




GO



GO
ALTER TABLE [Inventory].[ClosedMonthInventoryDetail] NOCHECK CONSTRAINT [FK_ClosedMonthInventoryDetail_ClosedMonthInventory];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo total del inventario en el mes cerrado; valor monetario acumulado (DECIMAL 18,2); default 0.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'CostTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Costo total', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'CostTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'CostTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de cierre mensual de inventario; relación con ClosedMonthInventory para agrupar detalle por período.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'ClosedMonthInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto en el mes cerrado de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'ClosedMonthInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'ClosedMonthInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del lote o número de serie del producto; trazabilidad y control de stock por lote/serial en almacén.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del lote o serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del almacén o bodega donde se registra el inventario; ubicación física del stock.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades inventariadas en el período cerrado; stock existente en almacén.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria de la tabla ClosedMonthInventoryDetail; identificador único del detalle de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del cierre mensual de inventario: registra la cantidad y costo total de cada ítem por bodega y lote/serial al momento del cierre de período, permitiendo conservar el histórico de existencias valorizadas mes a mes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail';
