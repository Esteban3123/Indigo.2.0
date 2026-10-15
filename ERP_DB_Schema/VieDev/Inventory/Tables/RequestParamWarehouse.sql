CREATE TABLE [Inventory].[RequestParamWarehouse] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [RequestParamId] INT NOT NULL,
    [WarehouseId]    INT NOT NULL,
    CONSTRAINT [PK_RequestParamWarehouse] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestParamWarehouse_RequestParam] FOREIGN KEY ([RequestParamId]) REFERENCES [Inventory].[RequestParam] ([Id]),
    CONSTRAINT [FK_RequestParamWarehouse_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[RequestParamWarehouse] NOCHECK CONSTRAINT [FK_RequestParamWarehouse_RequestParam];


GO
ALTER TABLE [Inventory].[RequestParamWarehouse] NOCHECK CONSTRAINT [FK_RequestParamWarehouse_Warehouse];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RequestParamWarehouse]
    ON [Inventory].[RequestParamWarehouse]([WarehouseId] ASC, [RequestParamId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamWarehouse', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del parámetro de solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamWarehouse', @level2type = N'COLUMN', @level2name = N'RequestParamId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamWarehouse', @level2type = N'COLUMN', @level2name = N'Id';

