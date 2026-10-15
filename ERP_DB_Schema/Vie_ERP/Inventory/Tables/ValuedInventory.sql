CREATE TABLE [Inventory].[ValuedInventory] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [Year]          INT             NOT NULL,
    [Month]         INT             NOT NULL,
    [WareHouseId]   INT             NULL,
    [ProductId]     INT             NOT NULL,
    [BatchSerialId] INT             NULL,
    [Quantity]      INT             NOT NULL,
    [ProductCost]   DECIMAL (18, 2) NOT NULL,
    [MovementType]  INT             NOT NULL,
    [EntityName]    VARCHAR (250)   NOT NULL,
    [EntityCode]    VARCHAR (20)    NOT NULL,
    [DocumentDate]  DATETIME        NOT NULL,
    [CreationUser]  VARCHAR (20)    NOT NULL,
    [CreationDate]  DATETIME        NOT NULL,
    CONSTRAINT [PK_ValuedInventory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ValuedInventory_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_ValuedInventory_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ValuedInventory_WareHouse] FOREIGN KEY ([WareHouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ValuedInventory', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ValuedInventory', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ValuedInventory', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ValuedInventory', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ValuedInventory', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de moviento 1 - Entrada ; 2 - Salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ValuedInventory', @level2type = N'COLUMN', @level2name = N'MovementType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo Promedio Ponderado ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ValuedInventory', @level2type = N'COLUMN', @level2name = N'ProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ValuedInventory', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'lote del inventario valorizado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ValuedInventory', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'producto del inventario valorizado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ValuedInventory', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacen del inventario valorizado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ValuedInventory', @level2type = N'COLUMN', @level2name = N'WareHouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes del cierre mensual', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ValuedInventory', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año del cierre mensual', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ValuedInventory', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ValuedInventory', @level2type = N'COLUMN', @level2name = N'Id';

