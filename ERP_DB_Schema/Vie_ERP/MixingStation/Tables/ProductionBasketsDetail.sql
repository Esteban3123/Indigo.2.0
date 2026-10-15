CREATE TABLE [MixingStation].[ProductionBasketsDetail] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductionBasketsId] INT             NOT NULL,
    [ComponentType]       TINYINT         NOT NULL,
    [ProductId]           INT             NULL,
    [AtcId]               INT             NULL,
    [SupplieId]           INT             NULL,
    [Quantity]            DECIMAL (18, 2) NOT NULL,
    [MeasurementUnitId]   INT             NOT NULL,
    CONSTRAINT [PK_ProductionBasketsDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionBasketsDetail_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_ProductionBasketsDetail_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_ProductionBasketsDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ProductionBasketsDetail_InventorySupplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id]),
    CONSTRAINT [FK_ProductionBasketsDetail_ProductionBaskets] FOREIGN KEY ([ProductionBasketsId]) REFERENCES [MixingStation].[ProductionBaskets] ([Id])
);


GO
ALTER TABLE [MixingStation].[ProductionBasketsDetail] NOCHECK CONSTRAINT [FK_ProductionBasketsDetail_ATC];


GO
ALTER TABLE [MixingStation].[ProductionBasketsDetail] NOCHECK CONSTRAINT [FK_ProductionBasketsDetail_InventoryMeasurementUnit];


GO
ALTER TABLE [MixingStation].[ProductionBasketsDetail] NOCHECK CONSTRAINT [FK_ProductionBasketsDetail_InventoryProduct];


GO
ALTER TABLE [MixingStation].[ProductionBasketsDetail] NOCHECK CONSTRAINT [FK_ProductionBasketsDetail_InventorySupplie];


GO
ALTER TABLE [MixingStation].[ProductionBasketsDetail] NOCHECK CONSTRAINT [FK_ProductionBasketsDetail_ProductionBaskets];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida del componente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del componente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de insumo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de componente:   1: Medicamento  2: Insumo  3: Producto de tipo otro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'ProductionBasketsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'Id';

