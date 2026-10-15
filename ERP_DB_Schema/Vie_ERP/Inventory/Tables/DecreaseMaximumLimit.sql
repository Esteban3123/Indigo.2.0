CREATE TABLE [Inventory].[DecreaseMaximumLimit] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Quantity]         INT           NOT NULL,
    [Justification]    VARCHAR (500) NOT NULL,
    [ProductId]        INT           NOT NULL,
    [BatchSerialId]    INT           NULL,
    [WarehouseId]      INT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_DecreaseMaximumLimit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DecreaseMaximumLimit_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_DecreaseMaximumLimit_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_DecreaseMaximumLimit_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id]),
    CONSTRAINT [UK_DecreaseMaximumLimit] UNIQUE NONCLUSTERED ([WarehouseId] ASC, [ProductId] ASC)
);


GO
ALTER TABLE [Inventory].[DecreaseMaximumLimit] NOCHECK CONSTRAINT [FK_DecreaseMaximumLimit_BatchSerial];


GO
ALTER TABLE [Inventory].[DecreaseMaximumLimit] NOCHECK CONSTRAINT [FK_DecreaseMaximumLimit_InventoryProduct];


GO
ALTER TABLE [Inventory].[DecreaseMaximumLimit] NOCHECK CONSTRAINT [FK_DecreaseMaximumLimit_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DecreaseMaximumLimit', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DecreaseMaximumLimit', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DecreaseMaximumLimit', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DecreaseMaximumLimit', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lote del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DecreaseMaximumLimit', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DecreaseMaximumLimit', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Jutificación de la disminució del limite', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DecreaseMaximumLimit', @level2type = N'COLUMN', @level2name = N'Justification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad a disminuir al limite maximo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DecreaseMaximumLimit', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la Disminucion del Limited Maximo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DecreaseMaximumLimit', @level2type = N'COLUMN', @level2name = N'Id';

